using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Text.CustomCodepages
{
	public sealed class CodepageConverter
	{
		private IReadOnlyList<string> codepage;
		
		public string CodepageName { get; }

		public CodepageConverter(int codepageIndex)
		{
			codepage = Codepages.GetCodepage(codepageIndex);
			CodepageName = Codepages.GetCodepageName(codepageIndex);
		}

		public string Convert(IEnumerable<byte> sequence)
		{
			var builder = new StringBuilder();

			foreach (byte b in sequence)
			{
				builder.Append(codepage[b]);
			}

			return builder.ToString();
		}
	}
}
