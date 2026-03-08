namespace Heroes.Element.Comparers.Tests;

[TestClass]
public class GameStringTextEqualityComparerTests
{
    private readonly GameStringTextEqualityComparer _comparer = new();

    [TestMethod]
    public void Equals_BothNull_ReturnsTrue()
    {
        // act
        bool result = _comparer.Equals(null, null);

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Equals_FirstIsNull_ReturnsFalse()
    {
        // arrange
        GameStringText y = new("some text");

        // act
        bool result = _comparer.Equals(null, y);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_SecondIsNull_ReturnsFalse()
    {
        // arrange
        GameStringText x = new("some text");

        // act
        bool result = _comparer.Equals(x, null);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_SameRawText_ReturnsTrue()
    {
        // arrange
        GameStringText x = new("same text");
        GameStringText y = new("same text");

        // act
        bool result = _comparer.Equals(x, y);

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Equals_DifferentRawText_ReturnsFalse()
    {
        // arrange
        GameStringText x = new("text one");
        GameStringText y = new("text two");

        // act
        bool result = _comparer.Equals(x, y);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_SameTextDifferentCase_ReturnsFalse()
    {
        // arrange
        GameStringText x = new("Some Text");
        GameStringText y = new("some text");

        // act
        bool result = _comparer.Equals(x, y);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_SameRawText_ReturnsSameHashCode()
    {
        // arrange
        GameStringText x = new("same text");
        GameStringText y = new("same text");

        // act
        int hashX = _comparer.GetHashCode(x);
        int hashY = _comparer.GetHashCode(y);

        // assert
        hashX.Should().Be(hashY);
    }

    [TestMethod]
    public void GetHashCode_DifferentRawText_ReturnsDifferentHashCode()
    {
        // arrange
        GameStringText x = new("text one");
        GameStringText y = new("text two");

        // act
        int hashX = _comparer.GetHashCode(x);
        int hashY = _comparer.GetHashCode(y);

        // assert
        hashX.Should().NotBe(hashY);
    }
}
