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
            int move = rnd.Next(field.Length);
            while (field[move] != emptyCell)
            {
                move = rnd.Next(field.Length);
            }
            field[move] = '-';
            UpdateGameState();
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

        const char emptyCell = '0';
        char[] field;
        int searhDepth = 3;
        int gridSize;
        GameState state = GameState.Continue;
    }
}
