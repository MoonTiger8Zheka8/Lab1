using System;

class Program
{
    const int Size = 19;
    const int Empty = 0;
    const int WinLength = 5;

    // Напрямки перевірки: горизонталь, вертикаль, діагональ вниз, діагональ вгору
    static readonly int[] dr = { 0, 1, 1, -1 };
    static readonly int[] dc = { 1, 0, 1, 1 };

    static void Main()
    {
        string? firstLine = Console.ReadLine();

        if (!int.TryParse(firstLine, out int testCases))
        {
            Console.WriteLine("Invalid number of test cases.");
            return;
        }

        for (int t = 0; t < testCases; t++)
        {
            int[,] board = new int[Size, Size];

            // Зчитуємо дошку 19x19
            for (int row = 0; row < Size; row++)
            {
                string? line = Console.ReadLine();

                if (line == null)
                {
                    Console.WriteLine("Invalid input: missing board row.");
                    return;
                }

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != Size)
                {
                    Console.WriteLine("Invalid input: each board row must contain 19 numbers.");
                    return;
                }

                for (int col = 0; col < Size; col++)
                {
                    if (!int.TryParse(parts[col], out int value))
                    {
                        Console.WriteLine("Invalid input: board contains a non-number value.");
                        return;
                    }

                    if (value < 0 || value > 2)
                    {
                        Console.WriteLine("Invalid input: board values must be 0, 1, or 2.");
                        return;
                    }

                    board[row, col] = value;
                }
            }

            (int winner, int answerRow, int answerCol) = FindWinner(board);

            Console.WriteLine(winner);

            if (winner != Empty)
            {
                Console.WriteLine($"{answerRow} {answerCol}");
            }
        }
    }

    static (int winner, int row, int col) FindWinner(int[,] board)
    {
        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                // Пропускаємо порожні клітинки
                if (board[row, col] == Empty)
                {
                    continue;
                }

                int stone = board[row, col];

                for (int dir = 0; dir < dr.Length; dir++)
                {
                    if (IsWinningLine(board, row, col, stone, dr[dir], dc[dir]))
                    {
                        // Перетворюємо індекси з 0-based у 1-based для виводу
                        return (stone, row + 1, col + 1);
                    }
                }
            }
        }

        return (Empty, 0, 0);
    }

    static bool IsWinningLine(int[,] board, int row, int col, int stone, int rowDir, int colDir)
    {
        // Перевіряємо, що це початок лінії, а не її середина
        int previousRow = row - rowDir;
        int previousCol = col - colDir;

        if (IsInside(previousRow, previousCol) && board[previousRow, previousCol] == stone)
        {
            return false;
        }

        // Перевіряємо рівно 5 однакових каменів підряд
        for (int i = 0; i < WinLength; i++)
        {
            int currentRow = row + rowDir * i;
            int currentCol = col + colDir * i;

            if (!IsInside(currentRow, currentCol))
            {
                return false;
            }

            if (board[currentRow, currentCol] != stone)
            {
                return false;
            }
        }

        // Якщо після п'яти є ще один такий самий камінь, це не рівно п'ять
        int nextRow = row + rowDir * WinLength;
        int nextCol = col + colDir * WinLength;

        if (IsInside(nextRow, nextCol) && board[nextRow, nextCol] == stone)
        {
            return false;
        }

        return true;
    }

    static bool IsInside(int row, int col)
    {
        // Перевіряємо, чи координати знаходяться в межах дошки
        return row >= 0 && row < Size && col >= 0 && col < Size;
    }
}