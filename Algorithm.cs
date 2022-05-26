using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal static class Algorithm
    {
        public static int GetBaseMinMaxScore(char[] field, int gridSize)
        {
            //cout number of available columns, rows and diagonals for player
            //assume that field is square matrix
            //int gridSize = (int)Math.Sqrt(field.Length);
            //gameSize rows and colums plus 2 diagonals
            int availableVariants = gridSize * 2 + 2;

            int x, y;
            bool isDiagOneFree = true;
            bool isDiagTwoFree = true;
            for (int i = 0; i < gridSize; i++)
            {
                x = i;
                for (y = 0; y < gridSize; y++)
                {
                    if (field[x + y * gridSize] == '-')
                    {
                        availableVariants--;
                        break;
                    }
                }

                y = i;
                for (x = 0; x < gridSize; x++)
                {
                    if (field[x + y * gridSize] == '-')
                    {
                        availableVariants--;
                        break;
                    }
                }

                if (field[i + i * gridSize] == '-')
                {
                    isDiagOneFree = false;
                }

                if (field[gridSize - i - 1 + i * gridSize] == '-')
                {
                    isDiagTwoFree = false;
                }
            }

            if (!isDiagOneFree)
            {
                availableVariants--;
            }
            if (!isDiagTwoFree)
            {
                availableVariants--;
            }

            return availableVariants;
        }
    }
}
