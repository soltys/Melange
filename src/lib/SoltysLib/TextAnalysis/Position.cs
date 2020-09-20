namespace SoltysLib.TextAnalysis
{
    public readonly struct Position
    {
        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }
        public int Line
        {
            get;
        }

        public int Column
        {
            get;
        }
    }
}
