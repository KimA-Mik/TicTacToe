using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    enum GameState{
        Continue,
        Draw,
        PlusWons,
        MinusWons
    }

    

    internal class TheGame
    {
        //internal class Move
        //{
        //    public int where;
        //    public int score;
        //}
        public TheGame(int gameGridSize)
        {
            gridSize = gameGridSize;
            field = new char[gridSize * gridSize];
            ClearField();
        }

        public void Restart()
        {
            ClearField();
        }

        public int GetCell(int ind)
        {
            return field[ind];
        }

        public GameState GetGameState()
        {
            return state;
        }

        public void MakeMove(int cell)
        {
            field[cell] = '+';
            UpdateGameState();
        }

        Random rnd = new Random();
        public void EnemyMove()
        {
            RunMinMax(minusCell,0,1,3);
            UpdateGameState();

        }

        private int RunMinMax(char gameSide, int recursiveLevel, int alpha, int beta)
        {
            int bestMove = -1;
            int minMax = (gameSide == plusCell) ? int.MinValue : int.MaxValue;

            if (recursiveLevel >= searhDepth)
                return Algorithm.GetBaseMinMaxScore(field, gridSize);

            //List<Move> moves = new List<Move>();

            for (int i = 0; i < field.Length; i++)
            {
                if (field[i] == emptyCell)
                {
                    field[i] = gameSide;
                    var test = RunMinMax((gameSide == minusCell) ? plusCell : minusCell, recursiveLevel + 1, alpha, beta);
                    field[i] = emptyCell;

                    if ((test > minMax && gameSide == plusCell) ||
                        (test <= minMax && gameSide == minusCell))
                    {
                        //moves.Add(new Move { score = test, where = bestMove});
                        minMax = test;
                        bestMove = i;
                    }

                    //if (gameSide == plusCell)
                    //    alpha = Math.Max(alpha, test);
                    //else
                    //    beta = Math.Min(beta, test);
                    //if (beta < alpha)
                    //    break;
                }
            }
            if(bestMove == -1)
                return Algorithm.GetBaseMinMaxScore(field, gridSize);

            if (recursiveLevel == 0 && bestMove != -1)
            {
                field[bestMove] = minusCell;
            }
            return minMax;
        }


        private void UpdateGameState()
        {
            int stepsCount = 0;
            int x, y, i;
            for (i = 0; i < gridSize; i++)
            {
                stepsCount = 0;
                x = i;
                for (y = 0; y < gridSize; y++)
                {
                    if (field[x + y * gridSize] == '-')
                    {
                        stepsCount--;
                    }
                    if (field[x + y * gridSize] == '+')
                    {
                        stepsCount++;
                    }
                }
                if(stepsCount == gridSize)
                {
                    state = GameState.PlusWons;
                    return;
                }
                if(stepsCount == -gridSize)
                {
                    state = GameState.MinusWons;
                    return;
                }

                stepsCount = 0;
                y = i;
                for (x = 0; x < gridSize; x++)
                {
                    if (field[x + y * gridSize] == '-')
                    {
                        stepsCount--;
                    }
                    if (field[x + y * gridSize] == '+')
                    {
                        stepsCount++;
                    }
                }
                if (stepsCount == gridSize)
                {
                    state = GameState.PlusWons;
                    return;
                }
                if (stepsCount == -gridSize)
                {
                    state = GameState.MinusWons;
                    return;
                }
            }
            
            stepsCount = 0;
            for (i = 0; i < gridSize; i++)
            {
                if (field[i + i * gridSize] == '-')
                {
                    stepsCount--;
                }
                if (field[i + i * gridSize] == '+')
                {
                    stepsCount++;
                }
            }
            if (stepsCount == gridSize)
            {
                state = GameState.PlusWons;
                return;
            }
            if (stepsCount == -gridSize)
            {
                state = GameState.MinusWons;
                return;
            }

            stepsCount = 0;
            for (i = 0; i < gridSize; i++)
            {
                if (field[gridSize - i - 1 + i * gridSize] == '-')
                {
                    stepsCount--;
                }
                if (field[gridSize - i - 1 + i * gridSize] == '+')
                {
                    stepsCount++;
                }
            }
            if (stepsCount == gridSize)
            {
                state = GameState.PlusWons;
                return;
            }
            if (stepsCount == -gridSize)
            {
                state = GameState.MinusWons;
                return;
            }

            int freeCells = field.Length;
            for (i = 0; i < field.Length; i++)
            {
                if (field[i] != emptyCell)
                {
                    freeCells--;
                }
            }
            if(freeCells == 0)
            {
                state = GameState.Draw;
                return;
            }

            state = GameState.Continue;
            return;
        }

        private void ClearField()
        {
            for (int i = 0; i < field.Length; i++)
            {
                field[i] = emptyCell;
            }
        }

        public const char emptyCell = '0';
        public const char plusCell = '+';
        public const char minusCell = '-';
        char[] field;
        int searhDepth = 2;
        int gridSize;
        GameState state = GameState.Continue;
    }
}
