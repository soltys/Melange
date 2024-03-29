namespace Soltys.Library.Bson;

public class BsonDocument : BsonValue
{
    internal override ElementType Type => ElementType.Document;
    private readonly List<Element> elements;

    public IReadOnlyCollection<Element> Elements => this.elements;

    public BsonDocument()
    {
        this.elements = new List<Element>();
    }

    public BsonDocument(params Element[] elements) : this()
    {
        this.elements.AddRange(elements);
    }

    public BsonDocument(Dictionary<string, BsonValue> dictionary) : this()
    {
        foreach (var entry in dictionary)
        {
            this.elements.Add(new Element(entry.Key, entry.Value));
        }
    }

    public void Add(Element element) => this.elements.Add(element);

    public void AddRange(List<Element> list) => this.elements.AddRange(list);

    public override ReadOnlySpan<byte> GetBytes() => BsonEncoder.EncodeAsDocument(this.elements);

    public override string ToString() => $"{{ {ToString(this.elements)} }}";
    private static string ToString(IEnumerable<Element> elements) => string.Join(", ", elements.Select(x => x.ToString()));

    public Dictionary<string, BsonValue> ToDictionary() => this.elements.ToDictionary(x => x.Name, x => x.Value);
}
