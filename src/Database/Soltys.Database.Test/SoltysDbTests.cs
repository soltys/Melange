using Xunit;

namespace Soltys.Database.Test
{
    public class SoltysDbTests
    {
        [Fact]
        public void ParameterlessConstructor_CreatesInMemoryDatabase_ReturnsTrue()
        {
            var sut = new Soltys.SoltysDb();
            Assert.True(sut.IsInMemoryDatabase);
        }

        [Fact]
        public void FilenameConstructor_SetsFilenameProperty_EqualToFileNameInParameter()
        {
            var sut = new Soltys.SoltysDb("file.db");
            Assert.False(sut.IsInMemoryDatabase);
            Assert.Equal("file.db", sut.FileName);
        }

    }
}
