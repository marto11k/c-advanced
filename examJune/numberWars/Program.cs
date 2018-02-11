using System;
using System.Collections.Generic;
using System.Linq;

namespace numberWars
{
	class Program
	{
		static void Main(string[] args)
		{
			var firstPlayerCards = new Queue<string>(Console.ReadLine().Split());
			var secondPlayerCards = new Queue<string>(Console.ReadLine().Split());
			var turns = 0;
			bool canContinuePlaying = true;
			while (turns < 1000000 && firstPlayerCards.Count > 0 && secondPlayerCards.Count > 0 && canContinuePlaying)
			{
				turns++;
				var player1Card = firstPlayerCards.Dequeue();
				var player2Card = secondPlayerCards.Dequeue();

				var compareCards = GetNumber(player1Card).CompareTo(GetNumber(player2Card));
				if (compareCards == 1)
				{
					firstPlayerCards.Enqueue(player1Card);
					firstPlayerCards.Enqueue(player2Card);
				}
				else if (compareCards == -1)
				{
					secondPlayerCards.Enqueue(player2Card);
					secondPlayerCards.Enqueue(player1Card);
				}
				else
				{
					var cardsOnTable = new List<string> { player1Card, player2Card };
					while (canContinuePlaying)
					{
						if (firstPlayerCards.Count >= 3 && secondPlayerCards.Count >= 3)
						{
							var player1Sum = 0;
							var player2Sum = 0;
							for (int i = 0; i < 3; i++)
							{
								var firstCard = firstPlayerCards.Dequeue();
								var secondCard= secondPlayerCards.Dequeue();
								player1Sum += GetCharValue(firstCard);
								player2Sum += GetCharValue(secondCard);
								cardsOnTable.Add(firstCard);
								cardsOnTable.Add(secondCard);
							}
							if (player1Sum>player2Sum)
							{
								AddCardsToWinner(cardsOnTable,firstPlayerCards);
								break;
							}
							else if (player2Sum>player1Sum)
							{
								AddCardsToWinner(cardsOnTable, secondPlayerCards);
								break;
							}
						}
						else
						{
							canContinuePlaying = false;
						}
						
					}
				}
			}
			var result = string.Empty;
			if (firstPlayerCards.Count == secondPlayerCards.Count)
				result = "Draw";
			else if (firstPlayerCards.Count > secondPlayerCards.Count)
				result = "First player wins";
			else result = "Second player wins";
			Console.WriteLine($"{result} after {turns} turns");
		}

		private static void AddCardsToWinner(List<string> cardsOnTable, Queue<string> playerCards)
		{
			foreach (var card in cardsOnTable.OrderByDescending(c => GetNumber(c)).ThenByDescending(c=> GetCharValue(c)))
			{
				playerCards.Enqueue(card);
			}
		}

		static int GetCharValue(string card )
		{
			return card[card.Length - 1];
		}

		static int GetNumber(string card)
		{
			return int.Parse(card.Substring(0, card.Length - 1));
		}
	}
}