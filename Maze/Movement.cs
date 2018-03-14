namespace Maze
{
    class Movement
    {
        /// <summary>
        /// Initialises a new instance of <see cref="Movement"/>.
        /// </summary>
        /// <param name="dir">The direction of move.</param>
        /// <param name="oppsiteDir">The opposite direction of move.</param>
        /// <param name="x">The step in columns.</param>
        /// <param name="y">The step in rows.</param>
        public Movement(Direction dir, Direction oppsiteDir, int x, int y)
        {
            this.Dir = dir;
            this.OppositeDir = oppsiteDir;
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// The direction of move.
        /// </summary>
        public Direction Dir { get; }

        /// <summary>
        /// The opposite direction of move.
        /// </summary>
        public Direction OppositeDir { get; }

        /// <summary>
        /// The step in columns.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The step in rows.
        /// </summary>
        public int Y { get; }
    }
}
