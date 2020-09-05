namespace SoltysDb
{
    internal interface IPage
    {
        DataBlock DataBlock { get; }
        PageType PageType { get; }
        byte[] RawData { get; }

        long Position { get; set; }
    }
}
