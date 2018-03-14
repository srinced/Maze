using System;
using System.IO;
using System.Linq;

namespace Maze
{
    class MazeSolver
    {
        /// <summary>
        /// The number of rows.
        /// </summary>
        private int rowNum;

        /// <summary>
        /// The number of columns.
        /// </summary>
        private int colNum;

        /// <summary>
        /// The solved maze.
        /// </summary>
        private string[,] solvedMaze;

        /// <summary>
        /// The maze.
        /// </summary>
        private int[,] maze;

        /// <summary>
        /// The column of end point.
        /// </summary>
        private int endPointCol;

        /// <summary>
        /// The row of end point.
        /// </summary>
        private int endPointRow;

        /// <summary>
        /// The column of start point.
        /// </summary>
        private int startPointCol;

        /// <summary>
        /// The row of start point.
        /// </summary>
        private int startPointRow;

        /// <summary>
        /// The possible movements.
        /// </summary>
        private readonly object[] Move = { new Movement(Direction.North, Direction.South, 0, -1), new Movement(Direction.South, Direction.North, 0, 1), new Movement(Direction.West, Direction.East, -1, 0), new Movement(Direction.East, Direction.West, 1, 0) };

        /// <summary>
        /// Solve the maze.
        /// </summary>
        /// <param name="fileName">The input file name.</param>
        public void Solve(string fileName)
        {
            ReadMaze(fileName);
            
            var dir = GetInitialDir(startPointRow, startPointCol);

            solvedMaze[startPointRow, startPointCol] = "S";
            var result = FindPath(startPointCol, startPointRow, dir);
            if (!result)
                Console.WriteLine("no result found");
            else
            {
                solvedMaze[endPointRow, endPointCol] = "E";
                PrintMaze("output.txt");
                Console.WriteLine("Solution found. See the ouput file.");
            }
        }

        /// <summary>
        /// Reads the input file.
        /// </summary>
        /// <param name="fileName">The input file name.</param>
        private void ReadMaze(string fileName)
        {
            try
            {
                StreamReader reader = new StreamReader(fileName);
                var arraySize = reader.ReadLine().Split(' ').ToArray();
                var start = reader.ReadLine().Split(' ').ToArray();
                var end = reader.ReadLine().Split(' ').ToArray();

                startPointCol = Int32.Parse(start[0]);
                startPointRow = Int32.Parse(start[1]);
                
                endPointCol = Int32.Parse(end[0]);
                endPointRow = Int32.Parse(end[1]);

                colNum = Int32.Parse(arraySize[0]);
                rowNum = Int32.Parse(arraySize[1]);

                maze = new int[rowNum, colNum];
                solvedMaze = new string[rowNum, colNum];

                for (int row = 0; row < rowNum; row++)
                {
                    var line = reader.ReadLine();
                    var rowData = line.Split(' ');
                    int col = 0;
                    foreach (var point in rowData)
                    {
                        var val = Int32.Parse(point);
                        maze[row, col] = val;
                        if (val == 1)
                            solvedMaze[row, col] = "#";
                        else
                            solvedMaze[row, col] = " ";
                        col++;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed to read the maze from inout file {0}", fileName, exception);
            }
        }

        /// <summary>
        /// Find the correct path given a point and the Maze.
        /// </summary>
        /// <param name="x"> x coordinate of the point.</param>
        /// <param name="y"> y coordinate of the point.</param>
        /// <param name="direction">The direction of the move.</param>
        /// <returns>True if the path is found.</returns>
        private bool FindPath(int x, int y, Direction direction)
        {
            if (solvedMaze[y, x] == "X")
                return false;

            //are we on the Maze boundaries?
            if (y < 1 || x < 1 || x == colNum - 1 || y == rowNum - 1 || maze[y, x] == 1)
            {
                return false;
            }

            // have we reached the end?
            if (x == endPointCol && y == endPointRow && maze[y, x] == 0)
            {
                solvedMaze[y, x] = "E";
                return true;
            }

            foreach (Movement dirInfo in Move)
            {
                if (dirInfo.OppositeDir != direction && maze[y + dirInfo.Y, x + dirInfo.X] == 0)
                {
                    var res = FindPath(x + dirInfo.X, y + dirInfo.Y, dirInfo.Dir);
                    if (res)
                    {
                        solvedMaze[y + dirInfo.Y, x + dirInfo.X] = "X";
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Prints the maze to the output file.
        /// </summary>
        /// <param name="fileName">The output file name.</param>
        private void PrintMaze(string fileName)
        {
            try
            {
                StreamWriter writer = new StreamWriter(fileName);

                for (int row = 0; row < rowNum; row++)
                {
                    for (int col = 0; col < colNum; col++)
                    {
                        writer.Write(solvedMaze[row, col]);
                    }
                    writer.Write("\r\n");
                }
                writer.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed to write the result to output file {0}", fileName, exception);
            }
        }

        /// <summary>
        /// Get the initial possible direction of move.
        /// </summary>
        /// <param name="x">The start column.</param>
        /// <param name="y">The start row.</param>
        /// <returns>The possible direction of move.</returns>
        private Direction GetInitialDir(int x, int y)
        {
            if (maze[x - 1, y] == 0)
                return Direction.North;
            if (maze[x + 1, y] == 0)
                return Direction.South;
            if (maze[x, y - 1] == 0)
                return Direction.West;
            if (maze[x, y + 1] == 0)
                return Direction.East;
            return Direction.South;
        }
    }
}
