using System;

namespace SoltysDb.Core
{
    internal class DataPage : Page
    {
        public DataPage()
        {

        }

        public DataPage(byte[] data)
        {
            Array.Copy(data, rawData, data.Length);
        }
    }
}