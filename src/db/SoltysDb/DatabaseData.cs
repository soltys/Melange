using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SoltysDb
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
        /// Write writes page and returns page id
        /// </summary>
        public int Write(Page page)
        {
            if (page.PageId == -1)
            {
                page.PageId = (int)(this.dataStream.Length / Page.PageSize);
            }

            this.dataStream.Position = page.PageId * Page.PageSize;
            this.dataStream.Write(page.RawData, 0, page.RawData.Length);
            return page.PageId;
        }
        
        public Page Read(int pageOffset)
        {
            var offset = Page.PageSize * pageOffset;
            this.dataStream.Position = offset;

            var dataPage = new Page();
            int bytesRead = this.dataStream.Read(dataPage.RawData, 0, Page.PageSize);

            if (dataPage.PageKind == PageKind.Header)
            {
                dataPage = new HeaderPage(dataPage.RawData);
            }

            //Do not attempt to project a page since no data has been read
            //TODO - throw exception here
            if (bytesRead == 0)
            {
                return null;
            }

            return dataPage;
        }

        public IEnumerable<Page> ReadAll()
        {
            var pageAmount = this.dataStream.Length / Page.PageSize;
            for (int i = 0; i < pageAmount; i++)
            {
                yield return Read(i);
            }
        }

        public Page FindFirst(PageKind pageKind)
        {
            var allPages = ReadAll().ToArray();
            foreach (var page in allPages)
            {
                if (page.PageKind == pageKind)
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

        public byte[] ReadDataBlockBytes(Page dataPage)
        {
            using var ms = new MemoryStream();
            var currentDataPage = dataPage;

            while (true)
            {
                var data = currentDataPage.DataBlock.Data;
                ms.Write(data);

                if (currentDataPage.DataBlock.NextPageId > 0)
                {
                    currentDataPage = Read(currentDataPage.DataBlock.NextPageId);
                }
                else
                {
                    break;
                }
            }

            return ms.ToArray();
        }

        public void SaveDataInPages(Page firstDataPage, byte[] newBytes)
        {
            PageKind pageKind = firstDataPage.PageKind;
            int bytesToBeWritten = newBytes.Length;
            Page currentPage = firstDataPage;
            while (bytesToBeWritten > 0)
            {
                int startIndex = newBytes.Length - bytesToBeWritten;
                int copyLength = Math.Min(bytesToBeWritten, currentPage.DataBlock.Data.Length);
                newBytes.AsSpan().Slice(startIndex, copyLength).CopyTo(currentPage.DataBlock.Data);

                Write(currentPage);

                bytesToBeWritten -= currentPage.DataBlock.Data.Length;

                if (bytesToBeWritten > 0)
                {
                    if (currentPage.DataBlock.NextPageId > 0)
                    {
                        currentPage = Read(currentPage.DataBlock.NextPageId);
                    }
                    else
                    {
                        var newKvPage = new Page(pageKind);
                        Write(newKvPage);

                        currentPage.DataBlock.NextPageId = newKvPage.PageId;
                        Write(currentPage);

                        currentPage = newKvPage;
                    }
                }
            }
        }
    }
}
