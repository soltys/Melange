using Xunit;

namespace Soltys.Database.Test;

public class DatabaseDataTests
{
    private readonly DatabaseData sut;
    private DatabaseData SutFactory => new DatabaseData(new MemoryStream());

    public DatabaseDataTests()
    {
        this.sut = SutFactory;
    }

    [Fact]
    public void IsNew_WithEmptyMemoryStream_ReturnsTrue()
    {
        Assert.True(this.sut.IsNew());
    }

    [Fact]
    public void Write_WritingHeaderFile_IsNewReturnsFalse()
    {
        this.sut.WriteToDb(new Page(PageKind.Header));
        Assert.False(this.sut.IsNew());
    }

    [Fact]
    public void Read_AfterWrite_ReturnsEqualData()
    {

        var dataPage = new Page(PageKind.DataPage);
        dataPage.DataBlock.Data = new byte[] { 1, 2, 3 };
        this.sut.WriteToDb(dataPage);

        var page = this.sut.ReadFromDb(0);

        Assert.Equal(1, page.DataBlock.Data[0]);
        Assert.Equal(2, page.DataBlock.Data[1]);
        Assert.Equal(3, page.DataBlock.Data[2]);
    }

    [Fact]
    public void Write_ReturnsPagePosition()
    {
        var dataPage1 = new Page(PageKind.DataPage);
        var dataPage2 = new Page(PageKind.DataPage);

        var position1 = this.sut.WriteToDb(dataPage1);
        Assert.Equal(0, dataPage1.PageId);
        Assert.Equal(position1, dataPage1.PageId);

        var position2 = this.sut.WriteToDb(dataPage2);
        Assert.Equal(1, dataPage2.PageId);
        Assert.Equal(position2, dataPage2.PageId);
    }

    [Fact]
    public void FindFirst_ReturnsCorrectTypeForHeaderPageType()
    {
        this.sut.WriteToDb(new HeaderPage());
        var page = this.sut.FindFirst(PageKind.Header);
        Assert.IsType<HeaderPage>(page);
    }

    [Fact]
    public void FindFirst_IfNoPagesReturnsNull()
    {
        Assert.Null(this.sut.FindFirst(PageKind.Header));
    }
}
