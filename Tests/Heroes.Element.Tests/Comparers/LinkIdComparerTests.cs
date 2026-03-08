namespace Heroes.Element.Comparers.Tests;

[TestClass]
public class LinkIdComparerTests
{
    private readonly LinkIdComparer _comparer = new();

    [TestMethod]
    public void Compare_BothNull_ReturnsZero()
    {
        // act
        int result = _comparer.Compare(null, null);

        // assert
        result.Should().Be(0);
    }

    [TestMethod]
    public void Compare_FirstIsNull_ReturnsNegative()
    {
        // arrange
        AbilityLinkId y = new("Element", "Button", AbilityType.Q);

        // act
        int result = _comparer.Compare(null, y);

        // assert
        result.Should().BeNegative();
    }

    [TestMethod]
    public void Compare_SecondIsNull_ReturnsPositive()
    {
        // arrange
        AbilityLinkId x = new("Element", "Button", AbilityType.Q);

        // act
        int result = _comparer.Compare(x, null);

        // assert
        result.Should().BePositive();
    }

    [TestMethod]
    public void Compare_TalentLinkIdVsAbilityLinkId_ReturnsPositive()
    {
        // arrange
        TalentLinkId x = new("Element", "Button", AbilityType.Q, TalentTier.Level1);
        AbilityLinkId y = new("Element", "Button", AbilityType.Q);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().BePositive();
    }

    [TestMethod]
    public void Compare_AbilityLinkIdVsTalentLinkId_ReturnsNegative()
    {
        // arrange
        AbilityLinkId x = new("Element", "Button", AbilityType.Q);
        TalentLinkId y = new("Element", "Button", AbilityType.Q, TalentTier.Level1);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().BeNegative();
    }

    [TestMethod]
    public void Compare_BothTalentLinkIds_DifferentTier_SortsByTier()
    {
        // arrange
        TalentLinkId x = new("Element", "Button", AbilityType.Q, TalentTier.Level1);
        TalentLinkId y = new("Element", "Button", AbilityType.Q, TalentTier.Level10);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().BeNegative();
    }

    [TestMethod]
    public void Compare_BothTalentLinkIds_SameTierDifferentAbilityType_SortsByAbilityType()
    {
        // arrange
        TalentLinkId x = new("Element", "Button", AbilityType.W, TalentTier.Level1);
        TalentLinkId y = new("Element", "Button", AbilityType.Q, TalentTier.Level1);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().BePositive();
    }

    [TestMethod]
    public void Compare_BothAbilityLinkIds_DifferentAbilityType_SortsByAbilityType()
    {
        // arrange
        AbilityLinkId x = new("Element", "Button", AbilityType.Q);
        AbilityLinkId y = new("Element", "Button", AbilityType.E);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().BeNegative();
    }

    [TestMethod]
    public void Compare_SameAbilityType_DifferentElementId_SortsByElementId()
    {
        // arrange
        AbilityLinkId x = new("Alpha", "Button", AbilityType.Q);
        AbilityLinkId y = new("Beta", "Button", AbilityType.Q);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().BeNegative();
    }

    [TestMethod]
    public void Compare_SameAbilityTypeAndElementId_DifferentButtonElementId_SortsByButtonElementId()
    {
        // arrange
        AbilityLinkId x = new("Element", "ButtonB", AbilityType.Q);
        AbilityLinkId y = new("Element", "ButtonA", AbilityType.Q);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().BePositive();
    }

    [TestMethod]
    public void Compare_AllPropertiesEqual_ReturnsZero()
    {
        // arrange
        AbilityLinkId x = new("Element", "Button", AbilityType.Q);
        AbilityLinkId y = new("Element", "Button", AbilityType.Q);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().Be(0);
    }

    [TestMethod]
    public void Compare_BothTalentLinkIds_AllPropertiesEqual_ReturnsZero()
    {
        // arrange
        TalentLinkId x = new("Element", "Button", AbilityType.Q, TalentTier.Level4);
        TalentLinkId y = new("Element", "Button", AbilityType.Q, TalentTier.Level4);

        // act
        int result = _comparer.Compare(x, y);

        // assert
        result.Should().Be(0);
    }
}
