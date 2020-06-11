using System;
using SoltysDb.Core.Pages;
using Xunit;

namespace SoltysDb.Core.Test.Pages
{
    public class KeyValuePageTests
    {

        [Fact]
        public void Constructor_NullPassed_ExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new KeyValuePage(null));
        }

        
    }
}
