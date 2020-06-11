using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            if (page.Position == -1)
            {
                page.Position = (int)this.dataStream.Length;
            }

            this.dataStream.Position = page.Position;
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

        public T FindFirst<T>() where T : class
        {
            var allPages = ReadAll().ToArray();
            foreach (var page in allPages)
            {
                if (page is T pageFound)
                {
                    return pageFound;
                }
            }

            return null;
        }

        public void Dispose()
        {
            dataStream?.Dispose();
        }
    }
}