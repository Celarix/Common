using System;
using System.Text;
using ChrisAkridge.Common.Text.CustomCodepages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class CodepageTests
	{
		[TestMethod]
		public void CanConstructCodepageConverter()
		{
			// A failure causes an exception here, which fails the test
			// No assert needed
			var converter = new CodepageConverter(0);
		}

		[TestMethod]
		public void InvalidCodepageIndexThrows()
		{
			try
			{
				var badConverter = new CodepageConverter(-1);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
				Assert.IsTrue(ex.Message.Contains("-1"));
			}
		}

		[TestMethod]
		public void Codepage0_NameCorrect()
		{
			var converter = new CodepageConverter(0);
			Assert.AreEqual("CelarianAllPrintable", converter.CodepageName);
		}

		[TestMethod]
		public void Codepage0_ExistingCharacterConversionLeavesInputUnchanged()
		{
			string input = "This string is valid Latin-1 and valid Celarian All-Printable.";
			var converter = new CodepageConverter(0);

			string output = converter.Convert(Encoding.UTF8.GetBytes(input));
			Assert.AreEqual(input, output);
		}

		[TestMethod]
		public void Codepage0_00to0FConversionWorksCorrectly()
		{
			var input = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
			var converter = new CodepageConverter(0);

			string output = converter.Convert(input);
			Assert.AreEqual("∅∑∞∲≈≝⊕⋆⧗≟⏎⟰♠♣♡♢", output);
		}

		[TestMethod]
		public void Codepage0_10to1FConversionWorksCorrectly()
		{
			var input = new byte[] { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
			var converter = new CodepageConverter(0);

			string output = converter.Convert(input);
			Assert.AreEqual("⇒⇔◇×÷✓✗‽⋈#♭♩♫♬♂♀", output);
		}

		[TestMethod]
		public void Codepage0_7Fto8FConversionWorksCorrectly()
		{
			var input = new byte[] { 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143 };
			var converter = new CodepageConverter(0);

			string output = converter.Convert(input);
			Assert.AreEqual("⌫🎈🎀🧶☎📘💵📊🧬💡👧🚦🌇🌃💠🔄💙", output);
		}

		[TestMethod]
		public void Codepage0_90to9FConversionWorksCorrectly()
		{
			var input = new byte[] { 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159 };
			var converter = new CodepageConverter(0);

			string output = converter.Convert(input);
			Assert.AreEqual("⚠☢☣⛔↯⛆⚤♕♵⏺⚪🔴⭐🌙👑🌠", output);
		}
	}
}
