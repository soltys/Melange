using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using SoltysDb.Core.Pages;

namespace SoltysDb.Core
{
    class KeyValuePage : DataPage
    {
        public Dictionary<string, string> KeyValuesStore
        {
            get
            {
                var binFormatter = new BinaryFormatter();
                var mStream = new MemoryStream(this.Data.ToArray());
                var o = binFormatter.Deserialize(mStream);
                return (Dictionary<string, string>) o;
            }
            set
            {
                var binFormatter = new BinaryFormatter();
                var mStream = new MemoryStream();
                binFormatter.Serialize(mStream, value);
                mStream.ToArray().AsSpan().CopyTo(Data);
            }
        }

        public KeyValuePage(Page page) : base(page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.PageType = PageType.KeyValue;
        }
    }
}
