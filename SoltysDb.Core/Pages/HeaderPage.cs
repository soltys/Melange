using System;
using System.Text;

namespace SoltysDb.Core
{
    internal class HeaderPage : Page
    {
        public HeaderPage()
        {
            var byteHeader = Encoding.ASCII.GetBytes("SOLTYSDB");
            Array.Copy(byteHeader, RawData, byteHeader.Length);
        }
    }
}