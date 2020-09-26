using Hashicon.Params.Interface;

namespace Hashicon.Params {

	public class Variation : IParamValue {

		public double Max { get; set; } = 20;

		public double Min { get; set; } = 5;

		public bool Enabled { get; set; } = true;

		public Variation() {}
	}
}
