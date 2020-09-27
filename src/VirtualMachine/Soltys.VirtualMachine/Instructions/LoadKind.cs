namespace Soltys.VirtualMachine
{
    public enum LoadKind : byte
    {
        Local,
        Argument,
        StaticField,
        String,
        Integer,
        Double
    }
}
