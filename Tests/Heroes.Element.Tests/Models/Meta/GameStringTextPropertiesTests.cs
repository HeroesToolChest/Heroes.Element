namespace Heroes.Element.Models.Meta.Tests;

[TestClass]
public class GameStringTextPropertiesTests
{
    [TestMethod]
    public void Equals_SameReference_ReturnsTrue()
    {
        // arrange
        GameStringTextProperties props = new();

        // act
        bool result = props.Equals(props);

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Equals_NullOther_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props = new();

        // act
        bool result = props.Equals(null);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_DefaultInstances_ReturnsTrue()
    {
        // arrange
        GameStringTextProperties props1 = new();
        GameStringTextProperties props2 = new();

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Equals_DifferentLocale_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props1 = new() { Locale = StormLocale.ENUS };
        GameStringTextProperties props2 = new() { Locale = StormLocale.DEDE };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_DifferentGameStringTextType_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props1 = new() { GameStringTextType = GameStringTextType.RawText };
        GameStringTextProperties props2 = new() { GameStringTextType = GameStringTextType.ColoredText };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_NullAndNonNullGameStringTextType_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props1 = new() { GameStringTextType = null };
        GameStringTextProperties props2 = new() { GameStringTextType = GameStringTextType.RawText };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_BothNullGameStringTextType_ReturnsTrue()
    {
        // arrange
        GameStringTextProperties props1 = new() { GameStringTextType = null };
        GameStringTextProperties props2 = new() { GameStringTextType = null };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Equals_DifferentConstantVarsReplaced_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props1 = new() { ConstantVars = new() { Replaced = true } };
        GameStringTextProperties props2 = new() { ConstantVars = new() { Replaced = false } };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_DifferentConstantVarsPreserved_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props1 = new() { ConstantVars = new() { Preserved = true } };
        GameStringTextProperties props2 = new() { ConstantVars = new() { Preserved = false } };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_DifferentStyleVarsReplaced_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props1 = new() { StyleVars = new() { Replaced = true } };
        GameStringTextProperties props2 = new() { StyleVars = new() { Replaced = false } };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_DifferentStyleVarsPreserved_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props1 = new() { StyleVars = new() { Preserved = true } };
        GameStringTextProperties props2 = new() { StyleVars = new() { Preserved = false } };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_AllPropertiesEqual_ReturnsTrue()
    {
        // arrange
        GameStringTextProperties props1 = new()
        {
            Locale = StormLocale.FRFR,
            GameStringTextType = GameStringTextType.PlainText,
            ConstantVars = new() { Replaced = true, Preserved = false },
            StyleVars = new() { Replaced = false, Preserved = true },
        };

        GameStringTextProperties props2 = new()
        {
            Locale = StormLocale.FRFR,
            GameStringTextType = GameStringTextType.PlainText,
            ConstantVars = new() { Replaced = true, Preserved = false },
            StyleVars = new() { Replaced = false, Preserved = true },
        };

        // act
        bool result = props1.Equals(props2);

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Equals_ObjectNull_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props = new();

        // act
        bool result = props.Equals((object?)null);

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_ObjectDifferentType_ReturnsFalse()
    {
        // arrange
        GameStringTextProperties props = new();

        // act
        bool result = props.Equals("not a GameStringTextProperties");

        // assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_ObjectSameValues_ReturnsTrue()
    {
        // arrange
        GameStringTextProperties props1 = new();
        GameStringTextProperties props2 = new();

        // act
        bool result = props1.Equals((object)props2);

        // assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void GetHashCode_EqualObjects_ReturnsSameHashCode()
    {
        // arrange
        GameStringTextProperties props1 = new()
        {
            Locale = StormLocale.ENUS,
            GameStringTextType = GameStringTextType.RawText,
            ConstantVars = new() { Replaced = true, Preserved = false },
            StyleVars = new() { Replaced = false, Preserved = true },
        };

        GameStringTextProperties props2 = new()
        {
            Locale = StormLocale.ENUS,
            GameStringTextType = GameStringTextType.RawText,
            ConstantVars = new() { Replaced = true, Preserved = false },
            StyleVars = new() { Replaced = false, Preserved = true },
        };

        // act
        int hashCode1 = props1.GetHashCode();
        int hashCode2 = props2.GetHashCode();

        // assert
        hashCode1.Should().Be(hashCode2);
    }

    [TestMethod]
    public void GetHashCode_DifferentObjects_ReturnsDifferentHashCode()
    {
        // arrange
        GameStringTextProperties props1 = new() { Locale = StormLocale.ENUS };
        GameStringTextProperties props2 = new() { Locale = StormLocale.DEDE };

        // act
        int hashCode1 = props1.GetHashCode();
        int hashCode2 = props2.GetHashCode();

        // assert
        hashCode1.Should().NotBe(hashCode2);
    }
}