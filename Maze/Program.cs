using System;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the full input file name:");

            var fileName = Console.ReadLine();

            MazeSolver solver = new MazeSolver();
            solver.Solve(fileName);

            Console.ReadLine();

        }
    }

}
