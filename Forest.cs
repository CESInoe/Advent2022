using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Forest
    {
        internal static void Visible()
        {
            int visible = 0;
            var input = File.ReadAllText("../../../input8.txt");
            var forest = input.Split('\n');
            visible += forest.Length * 2;
            visible+= (forest[0].Length - 2) * 2;

            for(int i = 1; i< forest.Length - 1; i++)
            {
                for(int y = 1; y < forest[0].Length -1; y++)
                {
                    int tree = forest[i][y] - 48;
                    if (!IsHidden(forest, tree, i, y))
                    {
                        visible++;
                    }
                }
            }

            Console.WriteLine("nombre d'arbre visible = " + visible);
        }

        internal static bool IsHidden(string[] forest, int tree, int row, int column)
        {
            int direction = 0;
            for(int i = 0; i < column; i++)
            {
                if (forest[row][i] - 48 >= tree)
                {
                    direction++;
                    break;
                }
            }
            for (int i = column + 1; i < forest.Length; i++)
            {
                if (forest[row][i] - 48 >= tree)
                {
                    direction++;
                    break;
                }
            }
            for (int i = 0; i < row; i++)
            {
                if (forest[i][column] - 48 >= tree)
                {
                    direction++;
                    break;
                }
            }
            for (int i = row + 1; i < forest[row].Length; i++)
            {
                if (forest[i][column] - 48 >= tree)
                {
                    direction++;
                    break;
                }
            }

            return direction >= 4;
        }

        internal static void Score()
        {
            int maxScore = 0;
            var input = File.ReadAllText("../../../input8.txt");
            var forest = input.Split('\n');
            for (int i = 0; i < forest.Length; i++)
            {
                for (int y = 0; y < forest[0].Length; y++)
                {
                    int tree = forest[i][y];
                    int score = CalculateScore(forest, tree, i, y);
                    if (score > maxScore)
                    {
                        maxScore = score;
                    }
                }
            }
            Console.WriteLine(maxScore.ToString());
        }

        private static int CalculateScore(string[] forest, int tree, int row, int column)
        {
            int score = 1;
            for (int i = column -1; i >= 0; i--)
            {
                if (i == 0 || forest[row][i] >= tree)
                {
                    score *= column - i;
                    break;
                }
            }
            for (int i = column + 1; i < forest.Length; i++)
            {
                if (i == forest.Length-1 ||forest[row][i] >= tree)
                {
                    score *= i - column;
                    break;
                }
            }
            for (int i = row - 1; i >= 0; i--)
            {
                if (i == 0 || forest[i][column] >= tree)
                {
                    score *= row - i;
                    break;
                }
            }
            for (int i = row + 1; i < forest[row].Length; i++)
            {
                if (i == forest[row].Length-1 || forest[i][column] >= tree)
                {
                    score *= i - row;
                    break;
                }
            }
            return score;
        }
    }
}
