namespace SoltysDb.Core
{
    internal interface IPage
    {
        byte[] RawData { get; }

        int Position { get; set; }
    }
}