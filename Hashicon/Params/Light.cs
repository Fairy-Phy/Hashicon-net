using Hashicon.Enums;
using System;

namespace Hashicon.Params {

	public class Light {

		public double Top { get; set; } = 10;

		public double Right { get; set; } = -8;

		public double Left { get; set; } = -4;

		public bool Enabled { get; set; } = true;

		public Light() {}

		internal double this[SpriteLightEnum LightEnum] {
			get {
				if (LightEnum == SpriteLightEnum.Left) return Left;
				else if (LightEnum == SpriteLightEnum.Right) return Right;
				else if (LightEnum == SpriteLightEnum.Top) return Top;
				else throw new ArgumentException("Unknown SpriteLight");
			}
		}
	}
}
