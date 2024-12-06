using System.Text.RegularExpressions;

namespace AOC2024.Day04
{
    internal class Day04
    {
        string path = @"2024\day04\Input.txt";

        public string ReadFile()
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public int Part1() 
        {
            string[] lines = ReadFile().Split("\n", StringSplitOptions.RemoveEmptyEntries)
                                       .Select(line => line.Trim()).ToArray();
            string search = "XMAS";
            int searchLength = search.Length, sum = 0;
            
            for (int i = 0; i < lines.Length; i++)
            {             
                // horizontal search
                string horizontal = lines[i];
                string reversed = Reverse(horizontal);
                sum += Regex.Matches(horizontal, "XMAS").Count;
                sum += Regex.Matches(reversed, "XMAS").Count;

                // vertical search
                string vertical = new string(lines.Select(l => l[i]).ToArray());
                reversed = Reverse(vertical);
                sum += Regex.Matches(vertical, "XMAS").Count;
                sum += Regex.Matches(reversed, "XMAS").Count;

                // diagonal low-bot upper half search ⬊
                int tmp = i;
                string diagonal = new string(
                    lines.Select(l =>
                    {
                        if (tmp < l.Length) return l[tmp++];
                        else return '\0';
                    }).ToArray());
                reversed = Reverse(diagonal);
                sum += Regex.Matches(diagonal, "XMAS").Count;
                sum += Regex.Matches(reversed, "XMAS").Count;

                // diagonal low-bot lower half search ⬊
                if (i != 0) // dup
                {
                    tmp = 0;
                    string diagonal2 = "";
                    for (int a = i; a < lines[i].Length; a++)
                    {
                        if (tmp < lines[a].Length) diagonal2 += lines[a][tmp++];
                    }
                    reversed = Reverse(diagonal2);
                    sum += Regex.Matches(diagonal2, "XMAS").Count;
                    sum += Regex.Matches(reversed, "XMAS").Count;
                }

                // diagonal bot-low upper half search ⬈
                tmp = lines[i].Length - 1 - i;
                string diagonal3 = new string(
                    lines.Select(l =>
                    {
                        if (tmp >= 0) return l[tmp--];
                        else return '\0';
                    }).ToArray());
                reversed = Reverse(diagonal3);
                sum += Regex.Matches(diagonal3, "XMAS").Count;
                sum += Regex.Matches(reversed, "XMAS").Count;

                // diagonal bot-low lower half search ⬈
                if (i != 0) // dup
                {
                    tmp = lines[i].Length - 1;
                    string diagonal4 = "";
                    for (int a = i; a < lines[i].Length; a++)
                    {
                        if (tmp >= 0) diagonal4 += lines[a][tmp--];
                    }
                    reversed = Reverse(diagonal4);
                    sum += Regex.Matches(diagonal4, "XMAS").Count;
                    sum += Regex.Matches(reversed, "XMAS").Count;
                }
            }

            return sum;
        }

        public int Part2()
        {
            char[][] lines = ReadFile().Split("\n", StringSplitOptions.RemoveEmptyEntries)
                           .Select(line => line.Trim().ToCharArray()).ToArray();
            int sum = 0;
            for (int i = 0; i <= lines.Length - 3; i++)
            {
                for (int j = 0; j <= lines[i].Length - 3; j++)
                {
                    // build 3x3 matrix
                    char[][] matrix = new char[3][];
                    for (int row = 0; row < 3; row++)
                    {
                        matrix[row] = new char[3];
                        for (int col = 0; col < 3; col++)
                        {
                            matrix[row][col] = lines[i + row][j + col];
                        }
                    }

                    if (IsXShapedMAS(matrix)) sum++;
                }
            }

            return sum;
        }

        private bool IsXShapedMAS(char[][] matrix)
        {
            if (matrix.Length != 3 || matrix[0].Length != 3)
            {
                return false;
            }

            string diag1 = matrix[0][0].ToString() + 
                           matrix[1][1].ToString() +
                           matrix[2][2].ToString();
            string diag2 = matrix[0][2].ToString() +
                           matrix[1][1].ToString() +
                           matrix[2][0].ToString();
            string search = "MAS";

            return (diag1 == search || Reverse(diag1) == search) &&
                   (diag2 == search || Reverse(diag2) == search);
        }

        private string Reverse(string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());
        }
    }
}
