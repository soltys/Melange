using System.Collections.Generic;
using System.ComponentModel;
using SoltysDb.Core.Test.TestUtils;
using Xunit;

namespace SoltysDb.Core.Test.Features
{
    public class KeyValueStoreTests
    {
        [Fact]
        public void Insert_Get_AllowsToInsertStringUnderKey_ReturnsEqualsValueToInserted()
        {
            using var sut = new SoltysDb();
            sut.KV.Add("key3", "value3");
            sut.KV.Add("key2", "value2");
            sut.KV.Add("key1", "value1");

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
                sut.KV.Add(pair.Key, pair.Value);
                lastPair = pair;
            }

            //just checking if KV store is not only write-only :)
            var dbValue = sut.KV.Get(lastPair.Key);
            Assert.Equal(lastPair.Value, dbValue);
        }

        [Fact]
        public void AsDirectory_WorksAsExpected()
        {
            using var sut = new SoltysDb();

            sut.KV.Add("foo", "bar");
            sut.KV.Add("key", "value");
            sut.KV.Add("Answer", "42");

            var dict = sut.KV.AsDictionary();

            Assert.Equal(3, dict.Count);
            Assert.Equal("value", dict["key"]);
        }

        [Fact]
        public void AsDirectory_EmptyKeyValueStore_ReturnsEmptyDictionary()
        {
            using var sut = new SoltysDb();

            var dict = sut.KV.AsDictionary();

            Assert.NotNull(dict);
            Assert.Empty(dict);
        }


        [Fact]
        public void Remove_WhenRemoveSuccessfully_ReturnsTrue()
        {
            using var sut = new SoltysDb();

            sut.KV.Add("foo", "bar");

            var wasRemoved = sut.KV.Remove("foo");

            Assert.True(wasRemoved);
        }

        [Fact]
        public void Remove_WhenRemoveOnNotExistingKey_ReturnsFalse()
        {
            using var sut = new SoltysDb();
            var wasRemoved = sut.KV.Remove("foo");
            Assert.False(wasRemoved);
        }

        [Fact]
        public void ChangeCollection_AllowsToHaveDifferentDictionaries()
        {
            using var sut = new SoltysDb();

            sut.KV.Add("foo", "bar");
            sut.KV.ChangeCollection("myOwn");
            sut.KV.Add("foo", "baz");

            Assert.Equal("baz", sut.KV.Get("foo"));

            sut.KV.ChangeCollection(sut.KV.DefaultCollection);
            Assert.Equal("bar", sut.KV.Get("foo"));
        }
    }
}
