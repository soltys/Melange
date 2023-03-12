namespace Soltys.Library.Bson;

public readonly struct Element
{
    public string Name
    {
        get;
    }

    public BsonValue Value
    {
        get;
    }

    public Element(string name, BsonValue value)
    {
        Name = name;
        Value = value;
    }

    public ReadOnlySpan<byte> GetBytes()
    {
        var elementBytes = new List<byte>();
        elementBytes.Add((byte)Value.Type);
        elementBytes.AddRange(BsonEncoder.EncodeAsCString(Name));
        elementBytes.AddRange(Value.GetBytes().ToArray());
        return elementBytes.ToArray();
    }

    public override string ToString() => $"\"{Name}\": {Value}";
}
