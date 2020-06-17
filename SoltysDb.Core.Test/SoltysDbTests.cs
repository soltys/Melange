using System.Collections.Generic;
using System.ComponentModel;
using SoltysDb.Core.Test.TestUtils;
using Xunit;

namespace SoltysDb.Core.Test
{
    public class SoltysDbTests
    {
        [Fact]
        public void ParameterlessConstructor_CreatesInMemoryDatabase_ReturnsTrue()
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

    }
}
