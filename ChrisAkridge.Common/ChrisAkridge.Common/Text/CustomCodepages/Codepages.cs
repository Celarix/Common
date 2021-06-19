using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common.Text.CustomCodepages
{
	internal static class Codepages
	{
		private static readonly string[] CelarianAllPrintable =
		{
            "∅", "∑", "∞", "∲", "≈", "≝", "⊕", "⋆", "⧗", "≟", "⏎", "⟰", "♠", "♣", "♡",
		    "♢", "⇒", "⇔", "◇", "×", "÷", "✓", "✗", "‽", "⋈", "#", "♭", "♩", "♫",
		    "♬", "♂", "♀", " ", "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+",
		    ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":",
		    ";", "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I",
		    "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X",
		    "Y", "Z", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g",
		    "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v",
		    "w", "x", "y", "z", "{", "|", "}", "~", "⌫", "🎈", "🎀", "🧶", "☎",
		    "📘", "💵", "📊", "🧬", "💡", "👧", "🚦", "🌇", "🌃", "💠", "🔄",
		    "💙", "⚠", "☢", "☣", "⛔", "↯", "⛆", "⚤", "♕", "♵", "⏺", "⚪",
		    "🔴", "⭐", "🌙", "👑", "🌠", " ", "¡", "¢", "£", "¤", "¥", "¦", "§",
		    "¨", "©", "ª", "«", "¬", "­", "®", "¯", "°", "±", "²", "³", "´", "µ", "¶",
		    "·", "¸", "¹", "º", "»", "¼", "½", "¾", "¿", "À", "Á", "Â", "Ã", "Ä", "Å",
		    "Æ", "Ç", "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ð", "Ñ", "Ò", "Ó", "Ô",
		    "Õ", "Ö", "×", "Ø", "Ù", "Ú", "Û", "Ü", "Ý", "Þ", "ß", "à", "á", "â", "ã",
		    "ä", "å", "æ", "ç", "è", "é", "ê", "ë", "ì", "í", "î", "ï", "ð", "ñ", "ò",
		    "ó", "ô", "õ", "ö", "÷", "ø", "ù", "ú", "û", "ü", "ý", "þ", "ÿ"
        };

		public static IReadOnlyList<string> GetCodepage(int index)
		{
			switch (index)
			{
				case 0:
					return CelarianAllPrintable;
				default:
					throw new ArgumentOutOfRangeException($"No codepage at index {index} exists.");
			}
		}

		public static string GetCodepageName(int index)
		{
			switch (index)
			{
				case 0: return nameof(CelarianAllPrintable);
				default:
					throw new ArgumentOutOfRangeException($"No codepage at index {index} exists.");
			}
		}
	}
}
