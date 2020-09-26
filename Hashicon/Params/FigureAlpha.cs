using Hashicon.Params.Interface;

namespace Hashicon.Params {

	public class FigureAlpha : IParamValue {

		public double Max { get; set; } = 1.2;

		public double Min { get; set; } = .7;

		public FigureAlpha() {}
	}
}
