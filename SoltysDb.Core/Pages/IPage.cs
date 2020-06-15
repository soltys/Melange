namespace SoltysDb.Core
{
    internal interface IPage
    {
        byte[] RawData { get; }

        long Position { get; set; }
    }
}