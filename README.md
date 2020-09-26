# Hashicon-net

[![Apache 2.0 License](https://img.shields.io/badge/License-Apache%202.0-red.svg)](https://github.com/Fairy-Phy/Hashicon-net/blob/master/LICENSE)

Hashicon-net is a .NET port of [Hashicon](https://github.com/emeraldpay/hashicon). Generates a beautiful representation of any hash.

## Usage

### Version require

* .NET Standard 1.3 or higher
* .NET Framework 4.6 or higher
* .NET Core 1.0 or higher

### Create a hashicon image with default params

```cs
using System.IO;
using Hashicon.Params;
using Hashicon;

================================

string hash = "Hex Hash Text";
Param param = new Param();
using (MemoryStream image = Hashicon.GenerateAsStream(hash, param) as MemoryStream) {
	// Stream Process //
}
```

### Custom params are based on the original configuration

```cs
using Hashicon.Params;

================================

// These are the default values.
Param TestParam = new Param {
	// size px (HiDPI/Retina aware)
	Size = 100,
	// RenderMode supported Png and Jpg
	RenderMode = Enums.RenderModeEnum.Png,
	// primary color range radius ( 0=red, 60=yellow, 120=green, ..., 360=red )
	Hue = new Hue{
		Max = 360,
		Min = 0
	},
	// saturation ( 0=grey, 100=colorfull )
	Saturation = new Saturation{
		Max = 100,
		Min = 70
	},
	// lightness ( 0=extremlydark, 50=optimal, 100=extremlybright )
	Lightness = new Lightness{
		Max = 65,
		Min = 45
	},
	// hue variation for individual triangles
	Variation = new Variation{
		Max = 20,
		Min = 5,
		Enabled = true
	},
	// color shift from primary hue to secondary hue ( the pattern )
	Shift = new Shift{
		Max = 300,
		Min = 60
	},
	// the pattern opacity
	FigureAlpha = new FigureAlpha{
		Max = 1.0,
		Min = 0.7
	},
	// simulate a 3d cube by different areas gets some more/less light applyed
	Light = new Light{
		Top = 10,
		Right = -8,
		Left = -4,
		Enabled = true,
	}
};
```

### There is also method to save in Base64

```cs
string Base64Text = Hashicon.GenerateAsBase64(Hash, Param);
```

## License

Apache License 2.0

[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FFairy-Phy%2FHashicon-net.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FFairy-Phy%2FHashicon-net?ref=badge_large)
