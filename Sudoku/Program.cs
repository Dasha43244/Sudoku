using System;

class SudokuGame
{
    static int[,] sudokuBoard = new int[,]
    {
        {5, 3, 0, 0, 7, 0, 0, 0, 0},
        {6, 0, 0, 1, 9, 5, 0, 0, 0},
        {0, 9, 8, 0, 0, 0, 0, 6, 0},
        {8, 0, 0, 0, 6, 0, 0, 0, 3},
        {4, 0, 0, 8, 0, 3, 0, 0, 1},
        {7, 0, 0, 0, 2, 0, 0, 0, 6},
        {0, 6, 0, 0, 0, 0, 2, 8, 0},
        {0, 0, 0, 4, 1, 9, 0, 0, 5},
        {0, 0, 0, 0, 8, 0, 0, 7, 9}
    };

    static void Main()
    {
        Console.WriteLine("Добро пожаловать в игру Судоку!");
        PrintBoard();

        while (!IsSudokuSolved())
        {
            Console.Write("Введите строку (1-9) и столбец (1-9) через пробел, а затем введите число (1-9) для заполнения: ");
            string input = Console.ReadLine();
            if (input.Length == 3 && int.TryParse(input[0].ToString(), out int row) && int.TryParse(input[2].ToString(), out int col))
            {
                if (row >= 1 && row <= 9 && col >= 1 && col <= 9)
                {
                    Console.Write("Введите число (1-9): ");
                    if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= 9)
                    {
                        if (sudokuBoard[row - 1, col - 1] == 0 && !IsNumberInRow(row - 1, number) && !IsNumberInCol(col - 1, number))
                        {
                            sudokuBoard[row - 1, col - 1] = number;
                            PrintBoard();
                        }
                        else
                        {
                            Console.WriteLine("Невозможно разместить число в этой ячейке. Попробуйте снова.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Пожалуйста, введите число от 1 до 9.");
                    }
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите строку и столбец от 1 до 9.");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ввода. Попробуйте снова.");
            }
        }

        Console.WriteLine("Поздравляем! Вы решили Судоку!");
    }

    static void PrintBoard()
    {
        Console.WriteLine("Судоку:");
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                Console.Write(sudokuBoard[row, col] == 0 ? " . " : $" {sudokuBoard[row, col]} ");
                if (col == 2 || col == 5)
                    Console.Write("|");
            }
            Console.WriteLine();
            if (row == 2 || row == 5)
                Console.WriteLine("---------|---------|---------");
        }
        Console.WriteLine();
    }

    static bool IsSudokuSolved()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (sudokuBoard[row, col] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    static bool IsNumberInRow(int row, int number)
    {
        for (int col = 0; col < 9; col++)
        {
            if (sudokuBoard[row, col] == number)
                return true;
        }
        return false;
    }

    static bool IsNumberInCol(int col, int number)
    {
        for (int row = 0; row < 9; row++)
        {
            if (sudokuBoard[row, col] == number)
                return true;
        }
        return false;
    }
}