using Hashicon.Params.Interface;

namespace Hashicon.Params {

	public class Lightness : IParamValue {

		public double Max { get; set; } = 65;

		public double Min { get; set; } = 45;

		public Lightness() {}
	}
}
