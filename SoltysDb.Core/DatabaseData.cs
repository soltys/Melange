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

        public void Write(IPage page)
        {
            //move to end of stream
            this.dataStream.Position = this.dataStream.Length;

            this.dataStream.Write(page.RawData, 0, page.RawData.Length);
        }

        public IPage Read(int pageOffset)
        {
            var offset = Page.PageSize * pageOffset;
            this.dataStream.Position = offset;

            var dataPage = new Page();
            int bytesRead = this.dataStream.Read(dataPage.RawData, 0, Page.PageSize);

            //Do not attempt to project a page since no data has been read
            //TODO - throw exception here
            if (bytesRead == 0)
            {
                return null;
            }

            return ProjectPage(dataPage);
        }

        private IPage ProjectPage(Page page)
        {
            switch (page.PageType)
            {
                case PageType.DataPage:
                    return new DataPage(page);
                case PageType.KeyValue:
                    return new KeyValuePage(page);
                case PageType.Header:
                    return new HeaderPage(page);
                default:
                    throw new DbInvalidOperationException("Cannot identified page type");
            }
        }

        public IEnumerable<IPage> ReadAll()
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