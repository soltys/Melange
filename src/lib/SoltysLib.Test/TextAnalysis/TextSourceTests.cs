using SoltysLib.TextAnalysis;
using Xunit;

namespace SoltysLib.Test.TextAnalysis
{
    public class TextSourceTests
    {
        [Fact]
        public void Current_ReturnsFirstCharFromString()
        {
            var sut = new TextSource("abcdef");
            Assert.Equal('a', sut.Current);
        }

        [Fact]
        public void Next_ReturnsSecondCharFromString()
        {
            var sut = new TextSource("abcdef");
            Assert.Equal('b', sut.Next);
        }

        [Fact]
        public void AdvanceChar_MovesCurrentToNextChar()
        {
            var sut = new TextSource("abcdef");
            sut.AdvanceChar();
            Assert.Equal('b', sut.Current);
            Assert.Equal('c', sut.Next);
        }

        [Fact]
        public void AdvanceChar_MovesGivenAmountOfChars()
        {
            var sut = new TextSource("abcdef");
            sut.AdvanceChar(3);
            Assert.Equal('d', sut.Current);
            Assert.Equal('e', sut.Next);
        }

        [Fact]
        public void GetPosition_Initial_IsColumn1Line1()
        {
            var sut = new TextSource("abcdef");
            var actualPosition = sut.GetPosition();

            Assert.Equal(1, actualPosition.Column);
            Assert.Equal(1, actualPosition.Line);
        }

        [Fact]
        public void GetPosition_AdvanceCharCalled_IsUpdated()
        {
            var sut = new TextSource("abcdef");
            sut.AdvanceChar();
            var actualPosition = sut.GetPosition();

            Assert.Equal(2, actualPosition.Column);
            Assert.Equal(1, actualPosition.Line);
        }

        [Fact]
        public void GetPosition_AdvanceCharAfterNewLine_NewLineCounterIsUpdated()
        {
            var sut = new TextSource("a\ncdef");
            sut.AdvanceChar(2);
            var actualPosition = sut.GetPosition();

            Assert.Equal('c', sut.Current);
            Assert.Equal(1, actualPosition.Column);
            Assert.Equal(2, actualPosition.Line);

            sut.AdvanceChar();

            actualPosition = sut.GetPosition();
            Assert.Equal(2, actualPosition.Column);
            Assert.Equal(2, actualPosition.Line);
        }

        [Fact]
        public void IsEnded_WhenNoAdvancementMade_ReturnsFalse()
        {
            var sut = new TextSource("abcdef");
            Assert.False(sut.IsEnded);
        }

        [Fact]
        public void IsEnded_WhenStringIsEmpty_ReturnsTrue()
        {
            var sut = new TextSource("");
            Assert.True(sut.IsEnded);
        }

        [Fact]
        public void IsEnded_WhenAdvancementIsMadeAfterString_ReturnsTrue()
        {
            const string input = "abcdef";
            var sut = new TextSource(input);
            sut.AdvanceChar(input.Length);
            Assert.True(sut.IsEnded);
        }

        [Fact]
        public void AsSlice_ReturnsCharArrayFromCurrentCharacter()
        {
            var sut = new TextSource("abcdef");
            sut.AdvanceChar(2);

            Assert.Equal(4, sut.Slice().Length);
            Assert.Equal("cdef", new string(sut.Slice()));
        }
    }
}
