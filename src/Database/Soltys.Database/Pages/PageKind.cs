namespace Soltys.Database;

public enum PageKind : byte
{
    Undefined = 0,
    Header,
    DataPage,
    KeyValue
}