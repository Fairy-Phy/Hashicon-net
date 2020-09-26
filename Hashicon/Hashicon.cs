using Hashicon.Params;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Hashicon {

	public class Hashicon {

		private static byte[] HashStringToBytes(string Hash) {
			if (Hash.Length % 2 == 1) throw new ArgumentException("Hash string length must be an even number");

			List<byte> ResultBytes = new List<byte>();

			for (int i = 0; i < Hash.Length; i+=2) {
				string HashParts = new StringBuilder().Append(Hash[i]).Append(Hash[i + 1]).ToString();

				if (!byte.TryParse(HashParts, NumberStyles.HexNumber, null, out byte ParseByte)) {
					throw new FormatException();
				}

				ResultBytes.Add(ParseByte);
			}

			return ResultBytes.ToArray();
		}

		public static Stream GenerateAsStream(string Hash, Param Param_) {
			byte[] HashBytes = HashStringToBytes(Hash);

			return Image.Render(HashBytes, Param_);
		}

		public static string GenerateAsBase64(string Hash, Param Param_) {
			byte[] HashBytes = HashStringToBytes(Hash);

			using (MemoryStream HashStream = Image.Render(HashBytes, Param_) as MemoryStream) {
				return Convert.ToBase64String(HashStream.ToArray());
			}
		}
	}
}
