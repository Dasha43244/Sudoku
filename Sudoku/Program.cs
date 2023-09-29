using System;

class SudokuGame
{
    private static int[,] sudokuBoard = new int[9, 9];

    static void Main()
    {
        Console.WriteLine("Добро пожаловать в игру Судоку!");
        GenerateRandomSudoku();
        PrintBoard();

        while (!IsSudokuSolved())
        {
            Console.Write("Введите строку (1-9) и столбец (1-9) через пробел, а затем введите число (1-9) для заполнения (или 0 для удаления): ");
            string input = Console.ReadLine();
            if (input.Length == 3 && int.TryParse(input[0].ToString(), out int row) && int.TryParse(input[2].ToString(), out int col))
            {
                if (row >= 1 && row <= 9 && col >= 1 && col <= 9)
                {
                    Console.Write("Введите число (1-9 или 0 для удаления): ");
                    if (int.TryParse(Console.ReadLine(), out int number) && number >= 0 && number <= 9)
                    {
                        if (number == 0 || (!IsNumberInRow(row - 1, number) && !IsNumberInCol(col - 1, number) && !IsNumberInBlock(row - 1, col - 1, number)))
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
                        Console.WriteLine("Пожалуйста, введите число от 0 до 9.");
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

    static bool IsNumberInBlock(int row, int col, int number)
    {
        int blockStartRow = (row / 3) * 3;
        int blockStartCol = (col / 3) * 3;

        for (int i = blockStartRow; i < blockStartRow + 3; i++)
        {
            for (int j = blockStartCol; j < blockStartCol + 3; j++)
            {
                if (sudokuBoard[i, j] == number)
                {
                    return true;
                }
            }
        }

        return false;
    }
    static bool SolveSudoku(int[,] board)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == 0)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (IsSafe(board, row, col, num))
                        {
                            board[row, col] = num;

                            if (SolveSudoku(board))
                            {
                                return true; // Нашли решение
                            }

                            board[row, col] = 0; // Отменяем выбор, если не получилось
                        }
                    }
                    return false; // Не можем разместить число, возвращаем false
                }
            }
        }
        return true; // Доска полностью заполнена
    }

    static bool IsSafe(int[,] board, int row, int col, int num)
    {
        // Проверяем, что число num не встречается в строке, столбце и блоке 3x3
        return !UsedInRow(board, row, num) && !UsedInCol(board, col, num) && !UsedInBox(board, row - row % 3, col - col % 3, num);
    }

    static bool UsedInRow(int[,] board, int row, int num)
    {
        for (int col = 0; col < 9; col++)
        {
            if (board[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    static bool UsedInCol(int[,] board, int col, int num)
    {
        for (int row = 0; row < 9; row++)
        {
            if (board[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    static bool UsedInBox(int[,] board, int boxStartRow, int boxStartCol, int num)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row + boxStartRow, col + boxStartCol] == num)
                {
                    return true;
                }
            }
        }
        return false;
    }
    static void GenerateRandomSudoku()
    {
        // Создаем пустую доску
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                sudokuBoard[row, col] = 0;
            }
        }

        // Решаем судоку (получаем полностью решенную доску)
        SolveSudoku(sudokuBoard);

        // Удаляем некоторые числа, чтобы создать начальную доску
        Random random = new Random();
        for (int i = 0; i < 40; i++) // Можете изменить количество чисел для удаления
        {
            int row, col;
            do
            {
                row = random.Next(0, 9);
                col = random.Next(0, 9);
            } while (sudokuBoard[row, col] == 0);

            sudokuBoard[row, col] = 0;
        }
    }

    static void PrintBoard()
    {
        Console.WriteLine("Судоку:\n");
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
    static bool IsValidBoard()
    {
        for (int i = 1; i <= 9; i++)
        {
            for (int row = 0; row < 9; row++)
            {
                if (!IsNumberInRow(row, i))
                    return false;
            }

            for (int col = 0; col < 9; col++)
            {
                if (!IsNumberInCol(col, i))
                    return false;
            }

            for (int row = 0; row < 9; row += 3)
            {
                for (int col = 0; col < 9; col += 3)
                {
                    if (!IsNumberInBlock(row, col, i))
                        return false;
                }
            }
        }
        return true;
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
        return IsValidBoard();
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
