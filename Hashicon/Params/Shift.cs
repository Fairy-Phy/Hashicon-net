using Hashicon.Params.Interface;

namespace Hashicon.Params {

	public class Shift : IParamValue {

		public double Max { get; set; } = 300;

		public double Min { get; set; } = 60;

		public Shift() {}
	}
}
