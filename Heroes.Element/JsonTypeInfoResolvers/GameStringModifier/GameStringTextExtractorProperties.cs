namespace Heroes.Element.JsonTypeInfoResolvers.GameStringModifier;

internal static class GameStringTextExtractorProperties
{
    public static (Type Type, string PropertyName) UnitLife => (typeof(UnitLife), "lifeType");

    public static (Type Type, string PropertyName) UnitEnergy => (typeof(UnitEnergy), "energyType");

    public static (Type Type, string PropertyName) UnitShield => (typeof(UnitShield), "shieldType");
}
