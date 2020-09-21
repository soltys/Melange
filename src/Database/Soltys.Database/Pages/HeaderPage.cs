using Soltys.Library;

namespace Soltys.Database
{
    internal class HeaderMetadata : BinaryClass
    {
        private readonly BinaryStringNVarField databaseNameField;
        public string DatabaseName => this.databaseNameField.GetValue();
        public int End { get; }
        public HeaderMetadata(byte[] memory, int offset) : base(memory, offset)
        {
            const string dbId = "SOLTYSDB";
            this.databaseNameField = AddStringNVarField(dbId.Length);
            this.databaseNameField.SetValue(dbId);

            End = this.databaseNameField.FieldEnd;
        }
    }

    internal class HeaderPage : Page
    {
        private readonly HeaderMetadata headerMetadata;
        public HeaderPage()
        {
            this.headerMetadata = new HeaderMetadata(RawData, this.PageMetadata.End);
            DataBlock = new DataBlock(RawData, this.headerMetadata.End, Page.PageSize - this.headerMetadata.End);
            PageKind = PageKind.Header;
        }

        public HeaderPage(byte[] rawData) : base(rawData)
        {
            this.headerMetadata = new HeaderMetadata(RawData, this.PageMetadata.End);
            DataBlock = new DataBlock(RawData, this.headerMetadata.End, Page.PageSize - this.headerMetadata.End);
        }
    }
}
