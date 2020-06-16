using System.Collections.Generic;
using System.ComponentModel;
using SoltysDb.Core.Test.TestUtils;
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
            using var sut = new SoltysDb();
            sut.KV.Insert("key3", "value3");
            sut.KV.Insert("key2", "value2");
            sut.KV.Insert("key1", "value1");

            var value = sut.KV.Get("key1");

            Assert.Equal("value1", value);
        }

        [Fact]
        public void Get_NotInsertedValue_ThrowsKeyNotFoundException()
        {
            var sut = new SoltysDb();
            Assert.Throws<DbKeyNotFoundException>(() => sut.KV.Get("key"));
        }

        [Fact]
        [Description("Maybe long running due to big data set needed to fill pages")]
        public void Insert_BiggerThanPageSizeKeyValueData_DataSplitIntoMultiplePages()
        {
            using var sut = new SoltysDb();
            const int numberOfItems = Page.PageSize / (2 * 16);
            KeyValuePair<string, string> lastPair = new KeyValuePair<string, string>();
            foreach (var pair in Generator.GenerateKeyValuesPairs(numberOfItems))
            {
                sut.KV.Insert(pair.Key, pair.Value);
                lastPair = pair;
            }

            //just checking if KV store is not only write-only :)
            var dbValue =  sut.KV.Get(lastPair.Key);
            Assert.Equal(lastPair.Value, dbValue);
        }
    }
}
