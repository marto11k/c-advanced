using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace greedyTimes
{
	class Program
	{
		static void Main(string[] args)
		{
			long bagCapacity = long.Parse(Console.ReadLine());
			var input = Console.ReadLine().Split(new[] { " " },StringSplitOptions.RemoveEmptyEntries);
			var goldDict = new Dictionary<string, long>();
			long goldQuantity = 0;

			var gemDict = new Dictionary<string, long>();
			long gemQuantity = 0;

			var cashDict = new Dictionary<string, long>();
			long cashQuantity = 0;

			for (int i = 0; i < input.Length; i += 2)
			{
				var item = input[i];
				var itemValue = long.Parse(input[i + 1]);

				string itemType = GetItemType(item);

				bool canAddItemToBag = CanAddToBag(goldQuantity, gemQuantity, cashQuantity, itemValue, bagCapacity, itemType);

				if (item == "trash" || !canAddItemToBag)
				{
					continue;
				}

				switch (itemType)
				{
					case "Gold":
						AddItemToBag(goldDict, itemValue, item);
						goldQuantity += itemValue;
						break;
					case "Gem":
						AddItemToBag(gemDict, itemValue, item);
						gemQuantity += itemValue;
						break;
					case "Cash":
						AddItemToBag(cashDict, itemValue, item);
						cashQuantity += itemValue;
						break;
				}
			}
			if (goldDict.Any())
			{
				PrintItemInfo(goldDict, "Gold", goldQuantity);
				if (gemDict.Any())
				{
					PrintItemInfo(gemDict, "Gem", gemQuantity);
				}
				if (cashDict.Any())
				{
					PrintItemInfo(cashDict, "Cash", cashQuantity);
				}
			}
		}

		private static void PrintItemInfo(Dictionary<string, long> bag, string item, long amount)
		{
			var sb = new StringBuilder();

			sb.AppendLine($"<{item}> ${amount}");

			foreach (var pair in bag.OrderByDescending(p => p.Key).ThenBy(p => p.Value))
			{
				sb.AppendLine($"##{pair.Key} - {pair.Value}");
			}
			Console.Write(sb);
		}

		private static void AddItemToBag(Dictionary<string, long> bag, long itemValue, string itemType)
		{
			if (!bag.ContainsKey(itemType))
				bag[itemType] = 0;

			bag[itemType] += itemValue;


		}

		private static bool CanAddToBag(long goldQuantity, long gemQuantity, long cashQuantity, long itemValue, long bagCapacity, string itemType)
		{
			long currentQuantity = goldQuantity + gemQuantity + cashQuantity + itemValue;

			if (currentQuantity > bagCapacity)
			{
				return false;
			}

			switch (itemType)
			{
				case "Gold":
					return true;
				case "Gem":
					return gemQuantity + itemValue <= goldQuantity;
				case "Cash":
					return cashQuantity + itemValue <= gemQuantity;
			}
			return false;

		}

		private static string GetItemType(string item)
		{
			if (item.Length == 3)
			{
				return "Cash";
			}
			else if (item.ToLower().EndsWith("gem"))
			{
				return "Gem";
			}
			else if (item.ToLower() == "gold")
			{
				return "Gold";
			}
			else
			{
				return "trash";
			}
		}
	}
}