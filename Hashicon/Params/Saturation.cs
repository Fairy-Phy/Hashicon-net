using Hashicon.Params.Interface;

namespace Hashicon.Params {

	public class Saturation : IParamValue {

		public double Max { get; set; } = 100;

		public double Min { get; set; } = 70;

		public Saturation() {}
	}
}
