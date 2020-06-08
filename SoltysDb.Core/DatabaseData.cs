using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SoltysDb.Core
{
    internal class DatabaseData : IDisposable
    {
        private readonly Stream dataStream;

        public DatabaseData(Stream dataStream)
        {
            this.dataStream = dataStream;
        }

        public bool IsNew()
        {
            return this.dataStream.Length == 0;
        }

        public void Write(Page page)
        {
            //move to end of stream
            this.dataStream.Position = this.dataStream.Length;

            this.dataStream.Write(page.RawData, 0, page.RawData.Length);
        }

        public Page Read(int pageOffset)
        {
            var offset = Page.PageSize * pageOffset;
            this.dataStream.Position = offset;

            var dataPage = new DataPage();
            this.dataStream.Read(dataPage.RawData, 0, Page.PageSize);

            return dataPage;
        }

        public IEnumerable<Page> ReadAll()
        {
            var pageAmount = this.dataStream.Length / Page.PageSize;
            for (int i = 0; i < pageAmount; i++)
            {
                yield return Read(i + 1);
            }
        }

        public void Dispose()
        {
            dataStream?.Dispose();
        }
    }
}