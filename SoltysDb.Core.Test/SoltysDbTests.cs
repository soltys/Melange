using Xunit;

namespace SoltysDb.Core.Test
{
    public class SoltysDbTests
    {
        [Fact]
        public void ParameterlessContructor_CreatesInMemoryDatabase_ReturnsTrue()
        {
            var sut = new SoltysDb();
            Assert.True(sut.IsInMemoryDatabase);
        }

        [Fact]
        public void FilenameConstructor_SetsFilenameProperty_EqualToFileNameInParameter()
        {
            var sut = new SoltysDb("file.db");
            Assert.False(sut.IsInMemoryDatabase);
            Assert.Equal("file.db", sut.FileName);
        }

        [Fact]
        public void Insert_Get_AllowsToInsertStringUnderKey_ReturnsEqualsValueToInserted()
        {
            var sut = new SoltysDb();
            sut.Insert("key3", "value3"); 
            sut.Insert("key2", "value2");
            sut.Insert("key1", "value1");

            var value = sut.Get("key1");

            Assert.Equal("value1", value);
        }

        [Fact]
        public void Get_NotInsertedValue_ThrowsKeyNotFoundException()
        {
            var sut = new SoltysDb();
            Assert.Throws<DbKeyNotFoundException>(() => sut.Get("key"));
        }
    }
}
