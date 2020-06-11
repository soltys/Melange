using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SoltysDb.Core.Pages
{
    class KeyValuePage 
    {

        public KeyValuePage(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            //var binFormatter = new BinaryFormatter();
            //var mStream = new MemoryStream();
            //binFormatter.Serialize(mStream, keyValueData);
            //mStream.ToArray().AsSpan().CopyTo(Data);
        }
    }
}
