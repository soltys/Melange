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

        /// <summary>
        /// Write writes page and returns page position
        /// </summary>
        public long Write(IPage page)
        {
            if (page.Position == -1)
            {
                page.Position = this.dataStream.Length;
            }

            this.dataStream.Position = page.Position;
            this.dataStream.Write(page.RawData, 0, page.RawData.Length);
            return page.Position;
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

            return dataPage;
        }

        public IPage Read(long location)
        {
            this.dataStream.Position = location;

            var page = new Page();
            int bytesRead = this.dataStream.Read(page.RawData, 0, Page.PageSize);

            //Do not attempt to project a page since no data has been read
            //TODO - throw exception here
            if (bytesRead == 0)
            {
                return null;
            }

            return page;
        }

        public IEnumerable<IPage> ReadAll()
        {
            var pageAmount = this.dataStream.Length / Page.PageSize;
            for (int i = 0; i < pageAmount; i++)
            {
                yield return Read(i + 1);
            }
        }

        public IPage FindFirst(PageType pageType) 
        {
            var allPages = ReadAll().ToArray();
            foreach (var page in allPages)
            {
                if (page?.PageType == pageType)
                {
                    return page;
                }
            }

            return null;
        }

        public void Dispose()
        {
            this.dataStream?.Dispose();
        }

        public byte[] ReadDataBlockBytes(IPage dataPage)
        {
            using var ms = new MemoryStream();
            var currentDataPage = dataPage;

            while (true)
            {
                var data = currentDataPage.DataBlock.Data;
                ms.Write(data);

                if (currentDataPage.DataBlock.NextPageLocation > 0)
                {
                    currentDataPage = Read(currentDataPage.DataBlock.NextPageLocation);
                }
                else
                {
                    break;
                }
            }

            return ms.ToArray();
        }

        public void SaveDataInPages(IPage firstDataPage, byte[] newBytes)
        {
            PageType pageType = firstDataPage.PageType;
            int bytesToBeWritten = newBytes.Length;
            IPage currentPage = firstDataPage;
            while (bytesToBeWritten > 0)
            {
                int startIndex = newBytes.Length - bytesToBeWritten;
                int copyLength = Math.Min(bytesToBeWritten, currentPage.DataBlock.Data.Length);
                newBytes.AsSpan().Slice(startIndex, copyLength).CopyTo(currentPage.DataBlock.Data);

                Write(currentPage);

                bytesToBeWritten -= currentPage.DataBlock.Data.Length;

                if (bytesToBeWritten > 0)
                {
                    if (currentPage.DataBlock.NextPageLocation > 0)
                    {
                        currentPage = Read(currentPage.DataBlock.NextPageLocation);
                    }
                    else
                    {
                        var newKvPage = new Page(pageType);
                        Write(newKvPage);

                        currentPage.DataBlock.NextPageLocation = newKvPage.Position;
                        Write(currentPage);

                        currentPage = newKvPage;
                    }
                }
            }
        }
    }
}