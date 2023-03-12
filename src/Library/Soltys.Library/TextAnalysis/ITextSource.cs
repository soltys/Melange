namespace Soltys.Library.TextAnalysis;

public interface ITextSource
{
    ///<summary> Returns true if read pointer reached end of text stream; otherwise false </summary>
    bool IsEnded { get; }
    char Current { get; }
    char Next { get; }
    Position GetPosition();
    void AdvanceChar();
    void AdvanceChar(int amount);
    ReadOnlySpan<char> Slice();
}
