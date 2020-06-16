using System;
using System.Text;

namespace SoltysDb.Core
{

    internal class HeaderMetadata : BinaryClass
    {
        private readonly BinaryStringNVarField databaseNameField;
        public string DatabaseName => this.databaseNameField.GetValue();

        public int End { get; }
        public HeaderMetadata(byte[] memory, int offset) : base(memory)
        {
            this.databaseNameField = new BinaryStringNVarField(memory, offset, "SOLTYSDB".Length);
            this.databaseNameField.SetValue("SOLTYSDB");


            End = this.databaseNameField.FieldEnd;
        }
    }

    internal class HeaderPage : Page
    {
        private HeaderMetadata headerMetadata;
        public HeaderPage()
        {
            this.headerMetadata = new HeaderMetadata(RawData, this.PageMetadata.End);
            DataBlock = new DataBlock(RawData, this.headerMetadata.End, Page.PageSize - this.headerMetadata.End);
            PageType = PageType.Header;
        }
    }
}