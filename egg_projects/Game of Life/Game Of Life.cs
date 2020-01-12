using System;
using static System.Math;
using static System.Console;

namespace GameOfLife
{
    // Class for GameOfLife
    static class GameOfLife
    {
        // Variable to store the grid
        static int[,] grid = new int[,]
                        {
                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        { 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        { 0, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0},
                        { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        { 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                        { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                        { 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0},
                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        { 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0},
                        { 0, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                        { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                        { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                        { 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0},
                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        { 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0},
                        { 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                        };
        // Variable to hold generation number
        static int generation = 1;

        // Prints the grid to the user
        public static void PrintGrid()
        {
            // Clears the console
            Clear();
            WriteLine();

            // TODO: Write the grid to the console
            // Live cell -> "#"
            // Dead cell -> " "
            // Left and right boundaries -> "|"
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                Write("|");
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 1) Write("#");
                    else Write(" ");
                }
                WriteLine("|");
            }
            // TODO: Print the generation variable
            WriteLine();
            WriteLine("Generation: {0}", generation);
        }
        // Updates the grid according to GameOfLife ruleset.
        public static void Update()
        {
            // Create a padded grid to account for boundaries, makes comparisons much easier 
            int[,] paddedGrid = new int[grid.GetLength(0) + 2, grid.GetLength(1) + 2];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    paddedGrid[i + 1, j + 1] = grid[i, j];
                }
            }
            // New grid to be populated in the next generation
            int[,] newGrid = new int[grid.GetLength(0), grid.GetLength(1)];

            // TODO: Populate the newGrid with the next generation according to the rules of the game:
            //~ Any live cell with fewer than two live neighbors dies, as if by underpopulation.
            //~ Any live cell with two or three live neighbors lives on to the next generation.
            //~ Any live cell with more than three live neighbors dies, as if by overpopulation.
            //~ Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
            for (int i = 1; i < paddedGrid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < paddedGrid.GetLength(1) - 1; j++)
                {
                    int neighbors = 0;
                    if (paddedGrid[i, j] == 1) // Two cases, cell is alive / dead 
                    {
                        for (int a = i - 1; a < i + 2; a++)
                        {
                            for (int b = j - 1; b < j + 2; b++)
                            {
                                if (paddedGrid[a, b] == 1) neighbors++;
                            }
                        }
                        neighbors--; // Accounts for the fact that the cell in question is counted as a neighbor 

                        if (neighbors == 2 || neighbors == 3) newGrid[i - 1, j - 1] = 1; //Cell lives 
                        if (neighbors < 2 || neighbors > 3) newGrid[i - 1, j - 1] = 0; //Cell dies 
                    }
                    else // if current cell is dead 
                    {
                        for (int a = i - 1; a < i + 2; a++)
                        {
                            for (int b = j - 1; b < j + 2; b++)
                            {
                                if (paddedGrid[a, b] == 1) neighbors++;
                            }
                        }
                        if (neighbors == 3) newGrid[i - 1, j - 1] = 1;
                        else newGrid[i - 1, j - 1] = 0;
                    }
                }
            }
            // Assign the grid variable to the new grid and increment the generation variable.
            grid = newGrid;
            generation++;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Print the grid to the user.
            GameOfLife.PrintGrid();
            WriteLine("Press ENTER for newer generation");

            // Update the grid everytime the user hits enter.
            while (ReadLine() == "")
            {
                GameOfLife.Update();
                GameOfLife.PrintGrid();
                WriteLine("Press ENTER for newer generation");
            }
        }
    }
}