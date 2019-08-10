using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisAkridge.Common
{
	public static class GlobalRandom
	{
		// TODO: implement Eric Lippert's better Random series
		// https://ericlippert.com/2019/01/31/fixing-random-part-1/

		private static Random random = new Random();

		public static int Next() => random.Next();
		public static int Next(int maxValue) => random.Next(maxValue);
		public static int Next(int minValue, int maxValue) => random.Next(minValue, maxValue);

		public static double NextDouble() => random.NextDouble();

		public static void NextBytes(byte[] buffer) => random.NextBytes(buffer);
	}
}
