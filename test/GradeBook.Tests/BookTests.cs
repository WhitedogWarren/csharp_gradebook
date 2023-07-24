namespace GradeBook.Tests;

public class BookTests
{
    [Fact]
    public void BookComputeStatistics()
    {
        // arrange
        var book = new InnerMemoryBook("Whitedog", "myBook", new List<double>() { 3.4, 7.2, 5.5 });
        
        // act
        var result = book.GetStatistics();

        // assert
        Assert.Equal(5.37, result.Avg, 2);
        Assert.Equal(7.2, result.Highest);
        Assert.Equal(3.4, result.Lowest);

    }

    [Fact]
    public void CannotAddInvalidGrade()
    {
        var book = new InnerMemoryBook("Whitedog", "Test Book", new List<double>() { 3.4, 7.2, 5.5 });
        /*book.AddGrade(107.7);
        book.AddGrade(-2.3);*/
        Assert.Throws<ArgumentException>(() => book.AddGrade(107.7));
        Assert.Throws<ArgumentException>(() => book.AddGrade(-2.3));

        Assert.Equal(3, book.GetGrades().Count);
    }
}