using Hashicon.Enums;

namespace Hashicon {

	class Sprite {

		internal float X { get; set; }

		internal float Y { get; set; }

		internal bool Shape { get; set; }

		internal bool Hidden { get; set; } = false;

		internal SpriteLightEnum Light { get; set; } = SpriteLightEnum.Hidden;
	}
}
