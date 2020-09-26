using Hashicon.Params;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Hashicon.Test {
	public class UnitTest1 {

		private readonly ITestOutputHelper Output;

		private readonly static string HashText = "Hello";

		private readonly static string ResultDirectory = @"./TestResult";

		private readonly static string FilePathPngStream = @"./TestResult/StreamTest.png";

		private readonly static string FilePathPngBase64 = @"./TestResult/Base64Test.png";

		private readonly static string FilePathJpgStream = @"./TestResult/StreamTest.jpg";

		private readonly static string FilePathJpgBase64 = @"./TestResult/Base64Test.jpg";

		public UnitTest1(ITestOutputHelper Output) {
			this.Output = Output;

			if (!Directory.Exists(ResultDirectory)) {
				Directory.CreateDirectory(ResultDirectory);
			}
		}

		static string TextToHash(string Text) {
			using SHA256 SHA256_ = SHA256.Create();
			byte[] TextBytes = SHA256_.ComputeHash(Encoding.UTF8.GetBytes(Text));

			StringBuilder Result = new StringBuilder();
			foreach (byte TextByte in TextBytes) {
				Result.Append(TextByte.ToString("x2"));
			}

			return Result.ToString();
		}

		[Fact]
		public void GeneratePngStreamTest() {
			string TestHash = TextToHash(HashText);
			Param TestParam = new Param { RenderMode = Enums.RenderModeEnum.Png, Size = 1000 };

			using MemoryStream TestStream = Hashicon.GenerateAsStream(TestHash, TestParam) as MemoryStream;
			using FileStream TestFileStream = new FileStream(FilePathPngStream, FileMode.OpenOrCreate, FileAccess.Write);

			byte[] TestBytes = TestStream.ToArray();
			TestFileStream.Write(TestBytes, 0, TestBytes.Length);
		}

		[Fact]
		public void GeneratePngBase64Test() {
			string TestHash = TextToHash(HashText);
			Param TestParam = new Param { RenderMode = Enums.RenderModeEnum.Png, Size = 1000 };

			string TestBase64 = Hashicon.GenerateAsBase64(TestHash, TestParam);
			Output.WriteLine(TestBase64);

			byte[] TestBytes = Convert.FromBase64String(TestBase64);
			using FileStream TestFileStream = new FileStream(FilePathPngBase64, FileMode.OpenOrCreate, FileAccess.Write);

			TestFileStream.Write(TestBytes, 0, TestBytes.Length);
		}

		[Fact]
		public void GenerateJpgStreamTest() {
			string TestHash = TextToHash(HashText);
			Param TestParam = new Param { RenderMode = Enums.RenderModeEnum.Jpg, Size = 1000 };

			using MemoryStream TestStream = Hashicon.GenerateAsStream(TestHash, TestParam) as MemoryStream;
			using FileStream TestFileStream = new FileStream(FilePathJpgStream, FileMode.OpenOrCreate, FileAccess.Write);

			byte[] TestBytes = TestStream.ToArray();
			TestFileStream.Write(TestBytes, 0, TestBytes.Length);
		}

		[Fact]
		public void GenerateJpgBase64Test() {
			string TestHash = TextToHash(HashText);
			Param TestParam = new Param { RenderMode = Enums.RenderModeEnum.Jpg, Size = 1000 };

			string TestBase64 = Hashicon.GenerateAsBase64(TestHash, TestParam);
			Output.WriteLine(TestBase64);

			byte[] TestBytes = Convert.FromBase64String(TestBase64);
			using FileStream TestFileStream = new FileStream(FilePathJpgBase64, FileMode.OpenOrCreate, FileAccess.Write);

			TestFileStream.Write(TestBytes, 0, TestBytes.Length);
		}

		[Fact]
		public void ParamTest() {
			Param TestParam = new Param {
				Size = 1000,
				RenderMode = Enums.RenderModeEnum.Png,
				Hue = new Hue(),
				Saturation = new Saturation(),
				Lightness = new Lightness(),
				Variation = new Variation(),
				Shift = new Shift(),
				FigureAlpha = new FigureAlpha(),
				Light = new Light()
			};

			StringBuilder TestStringBuilder = new StringBuilder();

			TestStringBuilder.AppendFormat("Size: {0}\n", TestParam.Size);
			TestStringBuilder.AppendFormat("RenderMode: {0}\n", Enum.GetName(typeof(Enums.RenderModeEnum), TestParam.RenderMode));
			TestStringBuilder.AppendFormat("Hue: Max {0}, Min {1}\n", TestParam.Hue.Max, TestParam.Hue.Min);
			TestStringBuilder.AppendFormat("Saturation: Max {0}, Min {1}\n", TestParam.Saturation.Max, TestParam.Saturation.Min);
			TestStringBuilder.AppendFormat("Lightness: Max {0}, Min {1}\n", TestParam.Lightness.Max, TestParam.Lightness.Min);
			TestStringBuilder.AppendFormat("Variation: Max {0}, Min {1}, Enabled {2}\n", TestParam.Variation.Max, TestParam.Variation.Min, TestParam.Variation.Enabled);
			TestStringBuilder.AppendFormat("Shift: Max {0}, Min {1}\n", TestParam.Shift.Max, TestParam.Shift.Min);
			TestStringBuilder.AppendFormat("FigureAlpha: Max {0}, Min {1}\n", TestParam.FigureAlpha.Max, TestParam.FigureAlpha.Min);
			TestStringBuilder.AppendFormat("Light: Top {0}, Right {1}, Left {2}, Enabled {3}\n", TestParam.Light.Top, TestParam.Light.Right, TestParam.Light.Left, TestParam.Light.Enabled);

			Output.WriteLine(TestStringBuilder.ToString());
		}

		// Under is throw tests.

		[Fact]
		public void HashThrowTest() {
			string TestHash = TextToHash(HashText).Remove(0, 1);
			Param TestParam = new Param();

			Assert.Throws<ArgumentException>(delegate {
				using Stream _ = Hashicon.GenerateAsStream(TestHash, TestParam);
			});
		}
	}
}
