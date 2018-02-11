using System;
using System.Linq;

namespace cryptoMaster
{
    class Program
    {
        static void Main(string[] args)
        {
			var nums = Console.ReadLine()
				.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToArray();

			var max = 0;
			for (int step = 1; step < nums.Length; step++)
			{

				for (int index = 0; index < nums.Length; index++)
				{
					var sequence = 1;
					var currentIndex = index;
					var nextIndex = (index + step) % nums.Length;  //1, -2, -3, 4, -5, 6, -7, -8
					while (nums[currentIndex] < nums[nextIndex])
					{
						currentIndex = nextIndex;
						nextIndex = (nextIndex + step) % nums.Length;
						sequence++;
					}

					if (sequence>max)
					{
						max = sequence;
					}
				}
			}
			Console.WriteLine(max);
        }
    }
}