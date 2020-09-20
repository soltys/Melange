using System;

namespace SoltysLib.TextAnalysis
{
    public interface ITextSource
    {
        bool IsEnded { get; }
        char Current { get; }
        char Next { get; }
        Position GetPosition();
        void AdvanceChar();
        void AdvanceChar(int amount);
        ReadOnlySpan<char> Slice();
    }
}
