using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrisAkridge.Common.Numerics;
using System.Collections.Generic;
using ChrisAkridge.Common.Extensions;

namespace ChrisAkridge.Common.Tests
{
	[TestClass]
	public class BigDecimalTests
	{
		private const string TwoToThe1800th = "71448348576730208360402604523024658663907311448489024669693316988935593287322878666163481950176220037593478347105937422686501991894419788796088422137966026262523598150372719976137911322484446114613284904383977643176193557817897027023063420124852033989626806764509137929914787205373413116077254242653423277386226627159120168223623660139965116969572411841665962582988716865792650075294655252525257343163566042824495509307872827973214736884381496689456792434150079470111661811761376161068055664012337698456291039551943299284254570579952324837376";
		private const string AboutOneGoogol = "10000000000000000159028911097599180468360808563945281389781327557747838772170381060813469985856815104";
		private const string ManThatsABigMantissa = "108175534980943878884257501546692322429347307343217923116436909799126306696758767175126102453872120161545947783582616746490021962007980197244533859048340157582858917675687732736094299032273758795050109050651939955498996978709770476595066204397355462329000096567725972239394905552091045414537525542850044131791501864147457847855716785152836647402162332356164063957604824786160586749935306288466695263892630206842500107693511551614061872261671032958793201788051332291390024568294309447190422789171911173809624596550051511910248804654708226624649486459767987213764949589318189714089615488914473457753290320176718875078321923401439413279458559359817200485654245940043041992724424503607461551059552416402";
		private const int ManThatsASmallExponent = -696;

		private const string GameCash = "538437127.6885624128534910641217728402198359240468586590941685041824626303457064763881241742881658983536931930686654161047875956955570297142562156688348779691316235515151564826986236851388676597509";
		private const string MineCost = "665606408.78719159452260744238782271597801640759531413409058314958175373696542935236118758257118341016463068069313345838952124043044429702857437843311651220308683764484848435173013763148611323402491";

		[TestMethod]
		public void NormalizeTests()
		{
			var bd0 = new BigDecimal(1, 10);			// 1e10
			var bd1 = new BigDecimal(100000, 10);		// 1e15
			var bd2 = new BigDecimal(43904832084, 24);  // 43904832084e24
			var bd3 = new BigDecimal(29390000, -35);	// 2939e-31

			Assert.AreEqual(1, bd0.Mantissa);
			Assert.AreEqual(10, bd0.Exponent);

			Assert.AreEqual(1, bd1.Mantissa);
			Assert.AreEqual(15, bd1.Exponent);

			Assert.AreEqual(43904832084, bd2.Mantissa);
			Assert.AreEqual(24, bd2.Exponent);

			Assert.AreEqual(2939, bd3.Mantissa);
			Assert.AreEqual(-31, bd3.Exponent);
		}

		[TestMethod]
		public void TryParseTests()
		{
			BigDecimal bd0, bd1, bd2, bd3, bd4, bd5, bdBad;

			Assert.IsTrue(BigDecimal.TryParse("100", out bd0));
			Assert.IsTrue(BigDecimal.TryParse("3.14159", out bd1));
			Assert.IsTrue(BigDecimal.TryParse("2.71828", out bd2));
			Assert.IsTrue(BigDecimal.TryParse("892.", out bd3));
			Assert.IsTrue(BigDecimal.TryParse(".00993922", out bd4));
			Assert.IsTrue(BigDecimal.TryParse("-247.246", out bd5));
			Assert.IsFalse(BigDecimal.TryParse("not a number", out bdBad));
			Assert.IsFalse(BigDecimal.TryParse(".", out bdBad));
			Assert.IsFalse(BigDecimal.TryParse("1<<27", out bdBad));
			Assert.IsFalse(BigDecimal.TryParse("10.254.1.107", out bdBad));

			Assert.AreEqual(1, bd0.Mantissa);
			Assert.AreEqual(2, bd0.Exponent);
			Assert.AreEqual(314159, bd1.Mantissa);
			Assert.AreEqual(-5, bd1.Exponent);
			Assert.AreEqual(271828, bd2.Mantissa);
			Assert.AreEqual(-5, bd2.Exponent);
			Assert.AreEqual(892, bd3.Mantissa);
			Assert.AreEqual(0, bd3.Exponent);
			Assert.AreEqual(993922, bd4.Mantissa);
			Assert.AreEqual(-8, bd4.Exponent);
			Assert.AreEqual(-247246, bd5.Mantissa);
			Assert.AreEqual(-3, bd5.Exponent);
			Assert.AreEqual(0, bdBad.Mantissa);
			Assert.AreEqual(0, bdBad.Exponent);
		}

		[TestMethod]
		public void ToStringTests()
		{
			string[] roundTripStrings = {
				"3.14159",
				"2.71828",
				"100000",
				"1048576",
				"340282366920938463463374607431768211456",
				".00000000001",
				"-246",
				"137",
				"892.49845085",
				"1.02040801603206401280256051201024",
				".0628318"
			};

			foreach (var str in roundTripStrings)
			{
				Assert.AreEqual(str, BigDecimal.Parse(str).ToString());
			}
		}

		[TestMethod]
		public void ToInternalRepresentationStringTests()
		{
			Dictionary<string, string> strings = new Dictionary<string, string>
			{
				{ "3.14159", "314159e-5"},
				{ "2.71828", "271828e-5"},
				{ "100000", "1e5"},
				{ "1048576", "1048576e0"},
				{ "340282366920938463463374607431768211456", "340282366920938463463374607431768211456e0"},
				{ ".00000000001", "1e-11"},
				{ "-246", "-246e0"},
				{ "137", "137e0"},
				{ "892.49845085", "89249845085e-8"},
				{ "1.02040801603206401280256051201024", "102040801603206401280256051201024e-32"}
			};

			foreach (var kvp in strings)
			{
				Assert.AreEqual(kvp.Value, BigDecimal.Parse(kvp.Key).ToInternalRepresentationString());
			}
		}

		[TestMethod]
		public void CompareToTests()
		{
			Assert.AreEqual(0, BigDecimal.Parse("12").CompareTo(BigDecimal.Parse("12")));
			Assert.AreEqual(1, BigDecimal.Parse("12").CompareTo(BigDecimal.Parse("6")));
			Assert.AreEqual(-1, BigDecimal.Parse("12").CompareTo(BigDecimal.Parse("24")));

			Assert.AreEqual(0, BigDecimal.Parse("-12").CompareTo(BigDecimal.Parse("-12")));
			Assert.AreEqual(1, BigDecimal.Parse("12").CompareTo(BigDecimal.Parse("-12")));
			Assert.AreEqual(-1, BigDecimal.Parse("-12").CompareTo(BigDecimal.Parse("12")));

			Assert.AreEqual(0, BigDecimal.Parse("10").CompareTo(BigDecimal.Parse("10")));
			Assert.AreEqual(1, BigDecimal.Parse("12").CompareTo(BigDecimal.Parse("0.00012")));
			Assert.AreEqual(-1, BigDecimal.Parse("12").CompareTo(BigDecimal.Parse("120000")));
		}

		[TestMethod]
		public void IdentityTests()
		{
			Assert.AreEqual(BigDecimal.Parse("4"), +BigDecimal.Parse("4"));
		}

		[TestMethod]
		public void InverseTests()
		{
			Assert.AreEqual(BigDecimal.Parse("-4"), -BigDecimal.Parse("4"));
			Assert.AreEqual(BigDecimal.Parse("4"), -BigDecimal.Parse("-4"));
		}

		[TestMethod]
		public void AdditionTests()
		{
			// Same exponent
			Assert.AreEqual((BigDecimal)8, (BigDecimal)5 + 3);
			Assert.AreEqual((BigDecimal)1000, (BigDecimal)100 + 900);
			Assert.AreEqual((BigDecimal)2246, (BigDecimal)1274 + 972);

			// Different exponent
			Assert.AreEqual((BigDecimal)53, (BigDecimal)50 + 3);
			Assert.AreEqual((BigDecimal)3.16159, (BigDecimal)3.14159d + 0.02d);
			Assert.AreEqual((BigDecimal)27, (BigDecimal)26.9999 + 0.0001);

			// Different sign
			Assert.AreEqual((BigDecimal)2, (BigDecimal)(-2) + 4);
			Assert.AreEqual((BigDecimal)(-2), (BigDecimal)2 + -4);
			Assert.AreEqual((BigDecimal)(-6), (BigDecimal)(-2) + -4);
		}

		[TestMethod]
		public void SubtractionTests()
		{
			// Same exponent
			Assert.AreEqual((BigDecimal)2, (BigDecimal)5 - 3);
			Assert.AreEqual((BigDecimal)(-800), (BigDecimal)100 - 900);
			Assert.AreEqual((BigDecimal)302, (BigDecimal)1274 - 972);

			// Different exponent
			Assert.AreEqual((BigDecimal)47, (BigDecimal)50 - 3);
			Assert.AreEqual((BigDecimal)3.12159, (BigDecimal)3.14159d - 0.02d);
			Assert.AreEqual((BigDecimal)26.9998, (BigDecimal)26.9999 - 0.0001);

			// Different sign
			Assert.AreEqual((BigDecimal)(-6), (BigDecimal)(-2) - 4);
			Assert.AreEqual((BigDecimal)6, (BigDecimal)2 - -4);
			Assert.AreEqual((BigDecimal)2, (BigDecimal)(-2) - -4);
		}

		[TestMethod]
		public void MultiplicationTests()
		{
			// Same exponent
			Assert.AreEqual((BigDecimal)15, (BigDecimal)5 * 3);
			Assert.AreEqual((BigDecimal)90000, (BigDecimal)100 * 900);
			Assert.AreEqual((BigDecimal)1238328, (BigDecimal)1274 * 972);

			// Different exponent
			Assert.AreEqual((BigDecimal)150, (BigDecimal)50 * 3);
			Assert.AreEqual(BigDecimal.Parse("0.0628318"), (BigDecimal)3.14159d * 0.02d);
			Assert.AreEqual((BigDecimal)0.00269999, (BigDecimal)26.9999 * 0.0001);

			// Different sign
			Assert.AreEqual((BigDecimal)(-8), (BigDecimal)(-2) * 4);
			Assert.AreEqual((BigDecimal)(-8), (BigDecimal)2 * -4);
			Assert.AreEqual((BigDecimal)8, (BigDecimal)(-2) * -4);
		}

		[TestMethod]
		public void DivisionTests()
		{
			// Same exponent
			Assert.AreEqual(5, (BigDecimal)15 / 3);
			Assert.AreEqual(100, (BigDecimal)90000 / 900);
			Assert.AreEqual(1274, (BigDecimal)1238328 / 972);

			// Different exponent
			Assert.AreEqual(3, (BigDecimal)150 / 50);
			Assert.AreEqual(3.14159d, BigDecimal.Parse("0.0628318") / 0.02d);
			Assert.AreEqual(26.9999d, (BigDecimal)0.00269999 / 0.0001);

			// Different sign
			Assert.AreEqual(-6, (BigDecimal)(12) / -2);
			Assert.AreEqual(-10000, (BigDecimal)(-20000) / 2);
		}

		[TestMethod]
		public void PowTests()
		{
			Assert.AreEqual(1048576, BigDecimal.Pow(2, 20));
			Assert.AreEqual(BigDecimal.Parse(TwoToThe1800th), BigDecimal.Pow(2, 1800));

			Assert.AreEqual(BigDecimal.Divide(2, 8), BigDecimal.Pow(2, -2));
			Assert.AreEqual(BigDecimal.Divide(10, 100), BigDecimal.Pow(10, -1));
		}

		[TestMethod]
		public void ComparisonTests()
		{
			Assert.IsTrue(3 < (BigDecimal)5);
			Assert.IsFalse(5 < (BigDecimal)3);

			Assert.IsTrue(5 > (BigDecimal)3);
			Assert.IsFalse(3 > (BigDecimal)5);

			Assert.IsTrue(3 == (BigDecimal)3);
			Assert.IsFalse(5 == (BigDecimal)3);

			Assert.IsTrue(3 != (BigDecimal)5);
			Assert.IsFalse(5 != (BigDecimal)5);

			Assert.IsFalse(BigDecimal.Parse(AboutOneGoogol) < 100);

			Assert.IsFalse(BigDecimal.Parse(GameCash) > BigDecimal.Parse(MineCost));
		}

		[TestMethod]
		public void BigDecimalToDoubleTests()
		{
			Assert.AreEqual(1d, (double)(BigDecimal)1);
			Assert.AreEqual(0d, (double)(BigDecimal)0);
			Assert.AreEqual(1e10, (double)(BigDecimal)10_000_000_000);

			Assert.IsFalse(double.IsNaN(
				(double)new BigDecimal(BigDecimal.Parse(ManThatsABigMantissa).Mantissa, ManThatsASmallExponent)));
		}

		[TestMethod]
		public void LogTests()
		{
			Assert.IsTrue(BigDecimal.Log(8, 2).NearlyEqual(3d, 0.001d));
			Assert.IsTrue(BigDecimal.Log(222, 12).NearlyEqual(2.17419d, 0.00001d));
			Assert.IsTrue(BigDecimal.Log(2048, 16).NearlyEqual(2.75d, 0.0001d));
		}

		[TestMethod]
		public void Log10Tests()
		{
			Assert.IsTrue(BigDecimal.Log10(1000).NearlyEqual(3d, 0.001d));
			Assert.IsTrue(BigDecimal.Log10(1048576).NearlyEqual(6.0206d, 0.0001d));
			Assert.IsTrue(BigDecimal.Log10(1000).NearlyEqual(BigDecimal.Log(1000, 10d), 0.01d));
		}

		// Exp tests
		// Abs tests
	}
}
