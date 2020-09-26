using Hashicon.Params.Interface;

namespace Hashicon.Params {

	public class Hue : IParamValue {

		public double Max { get; set; } = 360;

		public double Min { get; set; } = 0;

		public Hue() {}
	}
}
