using Hashicon.Enums;
using Hashicon.Params.Interface;

namespace Hashicon.Params {

	public class Param {

		public int Size { get; set; } = 100;

		public RenderModeEnum RenderMode { get; set; } = RenderModeEnum.Png;

		public Hue Hue { get; set; } = new Hue();

		public Saturation Saturation { get; set; } = new Saturation();

		public Lightness Lightness { get; set; } = new Lightness();

		public Variation Variation { get; set; } = new Variation();

		public Shift Shift { get; set; } = new Shift();

		public FigureAlpha FigureAlpha { get; set; } = new FigureAlpha();

		public Light Light { get; set; } = new Light();

		internal static double ProcessParam(IParamValue Param_, double Value) =>
			Param_.Min + (Value % (Param_.Max - Param_.Min));
	}
}
