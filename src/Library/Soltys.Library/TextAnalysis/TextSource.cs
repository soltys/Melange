namespace Soltys.Library.TextAnalysis;

public class TextSource : ITextSource
{
    private int line;
    private int column;

    private readonly char[] textData;
    private int readIndex;

    public TextSource(string input)
    {
        this.textData = input.ToCharArray();
        this.line = 1;
        this.column = 1;
        this.readIndex = 0;

    }

    ///<summary> Returns true if read pointer reached end of text stream; otherwise false </summary>
    public bool IsEnded => this.readIndex >= this.textData.Length;
    private char Previous => IsIndexOutOfBounds(this.readIndex - 1) ? default(char) : this.textData[this.readIndex - 1];
    public char Current => this.textData[this.readIndex];
    public char Next => IsIndexOutOfBounds(this.readIndex + 1) ? default(char) : this.textData[this.readIndex + 1];
    public Position GetPosition() => new Position(this.line, this.column);
    private bool IsIndexOutOfBounds(int value) => value < 0 || value >= this.textData.Length;

    public ReadOnlySpan<char> Slice() => this.textData.AsSpan(this.readIndex);

    public void AdvanceChar(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            AdvanceChar();
        }
    }

    public void AdvanceChar()
    {
        this.readIndex += 1;

        if (Previous == '\n')
        {
            this.column = 0;
            this.line += 1;
        }

        this.column += 1;
    }
}
