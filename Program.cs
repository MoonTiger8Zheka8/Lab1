using System;

class Program
{
    const int Size = 19;
    // Напрямки перевірки: горизонталь, вертикаль, діагональ вниз, діагональ вгору
    static int[] dr = { 0, 1, 1, -1 };
    static int[] dc = { 1, 0, 1, 1 };
    static void Main()
    {
        int testCases = int.Parse(Console.ReadLine()!);
        for (int t = 0; t < testCases; t++)
        {
            int[,] board = new int[Size, Size];
            // Зчитуємо дошку 19x19
            for (int row = 0; row < Size; row++)
            {
                string[] parts = Console.ReadLine()!
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < Size; col++)
                {
                    board[row, col] = int.Parse(parts[col]);
                }
            }
            int winner = 0;
            int answerRow = -1;
            int answerCol = -1;
            FindWinner(board, ref winner, ref answerRow, ref answerCol);
            Console.WriteLine(winner);
            if (winner != 0)
            {
                Console.WriteLine($"{answerRow} {answerCol}");
            }
        }
    }
    static void FindWinner(int[,] board, ref int winner, ref int answerRow, ref int answerCol)
    {
        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                // Пропуск порожніx клітинок
                if (board[row, col] == 0)
                {
                    continue;
                }
                int stone = board[row, col];
                for (int dir = 0; dir < 4; dir++)
                {
                    if (IsWinningLine(board, row, col, stone, dr[dir], dc[dir]))
                    {
                        winner = stone;
                        // Перетворюємо індекси з 0-based у 1-based для виводу
                        answerRow = row + 1;
                        answerCol = col + 1;
                        return;
                    }
                }
            }
        }
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
        for (int i = 0; i < 5; i++)
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
        int nextRow = row + rowDir * 5;
        int nextCol = col + colDir * 5;
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