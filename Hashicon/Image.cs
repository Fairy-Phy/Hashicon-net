using Hashicon.Params;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace Hashicon {

	public class Image {

		private static (byte, byte, byte) HslToRgb(double Hue, double SaturationPercent, double LightnessPercent) {
			SaturationPercent /= 100;
			LightnessPercent /= 100;

			if (Hue < 0) {
				Hue += 360;
			}
			else if (Hue > 360) {
				Hue -= 360;
			}

			byte R;
			byte G;
			byte B;
			if (SaturationPercent == 0) {
				R = (byte) (LightnessPercent * 255);
				G = (byte) (LightnessPercent * 255);
				B = (byte) (LightnessPercent * 255);
			}
			else {

				double C = (1.0f - Math.Abs(2.0f * LightnessPercent - 1)) * SaturationPercent;
				double H_d = Hue / 60.0f;
				double X = C * (1 - Math.Abs((H_d % 2) - 1));

				double R1, G1, B1;
				if (0 <= H_d && H_d < 1) {
					R1 = C;
					G1 = X;
					B1 = 0;
				}
				else if (1 <= H_d && H_d < 2) {
					R1 = X;
					G1 = C;
					B1 = 0;
				}
				else if (2 <= H_d && H_d < 3) {
					R1 = 0;
					G1 = C;
					B1 = X;
				}
				else if (3 <= H_d && H_d < 4) {
					R1 = 0;
					G1 = X;
					B1 = C;
				}
				else if (4 <= H_d && H_d < 5) {
					R1 = X;
					G1 = 0;
					B1 = C;
				}
				else if (5 <= H_d && H_d < 6) {
					R1 = C;
					G1 = 0;
					B1 = X;
				}
				else {
					R1 = 0;
					G1 = 0;
					B1 = 0;
				}


				double M = LightnessPercent - C * 0.5f;
				R = (byte) ((R1 + M) * 255);
				G = (byte) ((G1 + M) * 255);
				B = (byte) ((B1 + M) * 255);
			}

			return (R,G,B);
		}

		internal static Stream Render(byte[] HashBytes, Param Param_) {
			double
				Hue = Param.ProcessParam(Param_.Hue, HashBytes[0]),
				Saturation = Param.ProcessParam(Param_.Saturation, HashBytes[1]),
				Lightness = Param.ProcessParam(Param_.Lightness, HashBytes[2]),
				Shift = Param.ProcessParam(Param_.Shift, HashBytes[3]),
				FigureAlpha = Param.ProcessParam(Param_.FigureAlpha, HashBytes[4]);

			int FigureIndex = HashBytes[5] % Assets.StandardFigures.Length;

			using (Image<Rgba32> HashImage = new Image<Rgba32>(Param_.Size, Param_.Size)) {
				for (int i = 0; i < Assets.Sprites.Length; i++) {
					Sprite Sprite_ = Assets.Sprites[i];
					double Light = Param_.Light.Enabled ? !Sprite_.Hidden ? Param_.Light[Sprite_.Light] : 0 : 1;

					double X = Math.Round(HashBytes[6] / (double) (i + 1));
					double Variation = Param_.Variation.Enabled && !Sprite_.Hidden ? Param.ProcessParam(Param_.Variation, X) : 0;

					PathBuilder PathBuilder_ = new PathBuilder();
					if (!Sprite_.Hidden) {
						var Shape = Assets.Shapes[Sprite_.Shape ? 1 : 0];
						PointF Point1 = new PointF(Param_.Size * (Shape.X1 + Sprite_.X), Param_.Size * (Shape.Y1 + Sprite_.Y));
						PointF Point2 = new PointF(Param_.Size * (Shape.X2 + Sprite_.X), Param_.Size * (Shape.Y2 + Sprite_.Y));
						PointF Point3 = new PointF(Param_.Size * (Shape.X3 + Sprite_.X), Param_.Size * (Shape.Y3 + Sprite_.Y));

						PathBuilder_.AddLine(Point1, Point2);
						PathBuilder_.AddLine(Point2, Point3);
						PathBuilder_.AddLine(Point3, Point1);
					}

					IPath Path = PathBuilder_.Build();
					(byte R, byte G, byte B) = HslToRgb(Hue + Variation, Saturation, Lightness + Light);
					HashImage.Mutate(Ctx =>
						Ctx.Fill(Color.FromRgba(R,G,B,255), Path)
					);

					int[] Figure = Assets.StandardFigures[FigureIndex];
					if (Figure[i] > 0) {
						byte Alpha = (byte) (Figure[i] * FigureAlpha / 10 * 255);

						(byte RF, byte GF, byte BF) = HslToRgb(Hue + Shift + Variation, Saturation, Lightness + Light);
						HashImage.Mutate(Ctx =>
							Ctx.Fill(Color.FromRgba(RF,GF,BF,Alpha), Path)
						);
					}
				}

				Stream ResultStream = new MemoryStream();
				if (Param_.RenderMode == Enums.RenderModeEnum.Png) HashImage.SaveAsPng(ResultStream);
				else if (Param_.RenderMode == Enums.RenderModeEnum.Jpg) HashImage.SaveAsJpeg(ResultStream);

				return ResultStream;
			}
		}
	}
}
