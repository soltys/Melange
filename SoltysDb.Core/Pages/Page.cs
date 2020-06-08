namespace SoltysDb.Core
{
    internal abstract class Page
    {
        public const int PageSize = 128;
        protected byte[] rawData = new byte[PageSize];

        public byte[] RawData => rawData;
    }
}