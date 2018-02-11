using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace regeh
{
	class Program
	{
		static void Main(string[] args)
		{
			var pattern = @"\[[a-zA-Z]+<(\d+)REGEH(\d+)>[a-zA-Z]+\]";
			var input = Console.ReadLine();
			Regex regex = new Regex(pattern);
			var indexes = new List<int>();

			var matches = regex.Matches(input);
			foreach (Match match in matches)
			{
				indexes.Add(int.Parse(match.Groups[1].Value));
				indexes.Add(int.Parse(match.Groups[2].Value));
			}
			var currentIndex = 0;
			foreach (var index in indexes)
			{
				currentIndex += index;
				var indexToPrint = currentIndex % (input.Length - 1);
				Console.Write(input[indexToPrint]);
			}
			Console.WriteLine();
		}
	}
}