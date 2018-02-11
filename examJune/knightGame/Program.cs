using System;

namespace knightGame
{
	class Program
	{
		static void Main(string[] args)
		{
			var boardSize = int.Parse(Console.ReadLine());
			char[][] board = new char[boardSize][];

			for (int i = 0; i < boardSize; i++)
			{
				board[i] = Console.ReadLine().ToCharArray();
			}

			var maxRow = 0;
			var maxCol = 0;
			var maxHits = 0;
			var countOfRemovedKnights = 0;

			do
			{
				if (maxHits > 0)
				{
					board[maxRow][maxCol] = '0';
					maxHits = 0;
					countOfRemovedKnights++;
				}
				for (int row = 0; row < boardSize; row++)
				{
					for (int column = 0; column < boardSize; column++)
					{
						if (board[row][column] == 'K')
						{
							int currentKnightHits = CalculateHits(row, column, board);

							if (currentKnightHits > maxHits)
							{
								maxHits = currentKnightHits;
								maxRow = row;
								maxCol = column;
							}
						}
					}
				}

			} while (maxHits > 0);

			Console.WriteLine(countOfRemovedKnights);
		}

		private static int CalculateHits(int row, int column, char[][] board)
		{
			var hits = 0;
			if (isAttacked(row - 2, column - 1, board)) hits++;
			if (isAttacked(row - 2, column + 1, board)) hits++;
			if (isAttacked(row - 1, column - 2, board)) hits++;
			if (isAttacked(row - 1, column + 2, board)) hits++;

			if (isAttacked(row + 1, column + 2, board)) hits++;
			if (isAttacked(row + 1, column - 2, board)) hits++;
			if (isAttacked(row + 2, column + 1, board)) hits++;
			if (isAttacked(row + 2, column - 1, board)) hits++;
			return hits;
		}

		static bool isAttacked(int row, int col, char[][] board)
		{
			return isWithinBorder(row, col, board.GetLength(0)) && board[row][col] == 'K';
		}

		static bool isWithinBorder(int row, int column, int boardSize)
		{
			return row >= 0 && row < boardSize && column >= 0 && column < boardSize;
		}
	}
}