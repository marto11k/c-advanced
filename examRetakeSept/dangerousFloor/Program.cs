using System;
using System.Linq;

namespace dangerousFloor
{
	class Program
	{
		static char[][] board;
		static void Main(string[] args)
		{
			board = new char[8][];
			for (int i = 0; i < 8; i++)
			{
				board[i] = Console.ReadLine().Split(',')
					.Select(char.Parse).ToArray();
			}

			string move;
			while ((move = Console.ReadLine()) != "END")
			{
				var figureType = move[0];
				var startRow = int.Parse(move[1].ToString());
				var startCol = int.Parse(move[2].ToString());
				var targetRow = int.Parse(move[4].ToString());
				var targetCol = int.Parse(move[5].ToString());

				if (!isInPosition(startRow, startCol, figureType))
				{
					Console.WriteLine($"There is no such a piece!");
					continue;
				}
				if (!isValidMove(startRow, startCol, targetRow, targetCol, figureType))
				{
					Console.WriteLine("Invalid move!");
					continue;
				}
				if (!isInBoard(targetRow,targetCol))
				{
					Console.WriteLine("Move go out of board!");
					continue;
				}

				board[targetRow][targetCol] = figureType;
				board[startRow][startCol] = 'x';

			}
		}

		private static bool isInBoard(int targetRow, int targetCol)
		{
			return targetRow >= 0 && targetRow < 8 && targetCol >= 0 && targetCol < 8;
		}

		private static bool isValidMove(int startRow, int startCol, int targetRow, int targetCol, char figureType)
		{
			switch (figureType)
			{
				case 'P':
					return isValidPawnMove(startRow, startCol, targetRow, targetCol);
				case 'R':
					return isStraightLine(startRow, startCol, targetRow, targetCol);
				case 'B':
					return isDiagonalMove(startRow, startCol, targetRow, targetCol);
				case 'Q':
					return isDiagonalMove(startRow, startCol, targetRow, targetCol) ||
						isStraightLine(startRow, startCol, targetRow, targetCol);
				case 'K':
					return isValidKingMove(startRow, startCol, targetRow, targetCol);
				default:
					throw new NotImplementedException();
			}
		}

		private static bool isValidKingMove(int startRow, int startCol, int targetRow, int targetCol)
		{
			bool isValidRow = Math.Abs(startRow - targetRow) == 1 || Math.Abs(startRow - targetRow) == 0;
			bool isValidCol = Math.Abs(startCol - targetCol) == 1 || Math.Abs(startCol - targetCol) == 0;
			return isValidCol && isValidRow;
		}

		private static bool isDiagonalMove(int startRow, int startCol, int targetRow, int targetCol)
		{
			return Math.Abs(startRow - targetRow) == Math.Abs(startCol - targetCol);
		}

		private static bool isStraightLine(int startRow, int startCol, int targetRow, int targetCol)
		{
			return startRow == targetRow || startCol == targetCol;
		}

		private static bool isValidPawnMove(int startRow, int startCol, int targetRow, int targetCol)
		{
			return startRow - 1 == targetRow && startCol == targetCol;
		}

		static bool isInPosition(int row, int col, char figureType)
		{
			return board[row][col] == figureType;
		}
	}
}