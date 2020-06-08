namespace SoltysDb.Core
{
    internal abstract class Page
    {
        public const int PageSize = 4096;
        public byte[] RawData { get; } = new byte[PageSize];
    }
}