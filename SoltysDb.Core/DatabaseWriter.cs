namespace SoltysDb.Core
{
    internal class DatabaseWriter
    {
        private readonly DatabaseData data;

        public DatabaseWriter(DatabaseData data)
        {
            this.data = data;
        }

        public void Write(Page page)
        {
            this.data.Write(page);
        }
    }
}