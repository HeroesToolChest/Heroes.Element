namespace Heroes.Element.JsonTypeInfoResolvers;

/// <summary>
/// Custom json type modifiers.
/// </summary>
public class JsonTypeInfoModifiers
{
    /// <summary>
    /// The default modifiers.
    /// </summary>
    /// <param name="typeInfo">The <see cref="JsonTypeInfo"/>.</param>
    /// <param name="localizedTextOption">Option to indicate the type of the localized text.</param>
    /// <param name="gameStringItemDictionary">The collection for the saved gamestrings.</param>
    public static void SerializationModifiers(JsonTypeInfo typeInfo, LocalizedTextOption localizedTextOption, GameStringItemDictionary gameStringItemDictionary)
    {
        foreach (JsonPropertyInfo propertyInfo in typeInfo.Properties)
        {
            IEnumerableModifier(propertyInfo);
            GameStringTextModifier(propertyInfo, localizedTextOption, gameStringItemDictionary);

            if (typeInfo.Type == typeof(Hero) || typeInfo.Type == typeof(Unit))
            {
                UnitLifeEnergyShieldModifiers(propertyInfo);
            }
        }
    }

    // only serialize collections if they have items
    private static void IEnumerableModifier(JsonPropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType.GetInterface(nameof(IEnumerable)) is not null &&
            propertyInfo.PropertyType != typeof(string) &&
            !propertyInfo.Name.Equals(nameof(HeroPortrait.PartyFrames), StringComparison.OrdinalIgnoreCase))
        {
            propertyInfo.ShouldSerialize = static (_, value) =>
            {
                return value is not IEnumerable enumerable || enumerable.GetEnumerator().MoveNext();
            };
        }
    }

    // only serialize UnitLife, UnitEnergy, and UnitShield if they have "values"
    private static void UnitLifeEnergyShieldModifiers(JsonPropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType == typeof(UnitLife))
        {
            propertyInfo.ShouldSerialize = static (_, value) =>
            {
                return value is not null && value is UnitLife unitLife && unitLife.LifeMax > 0;
            };
        }
        else if (propertyInfo.PropertyType == typeof(UnitEnergy))
        {
            propertyInfo.ShouldSerialize = static (_, value) =>
            {
                return value is not null && value is UnitEnergy unitEnergy && unitEnergy.EnergyMax > 0;
            };
        }
        else if (propertyInfo.PropertyType == typeof(UnitShield))
        {
            propertyInfo.ShouldSerialize = static (_, value) =>
            {
                return value is not null && value is UnitShield unitShield && unitShield.ShieldMax > 0;
            };
        }
    }

    private static void GameStringTextModifier(JsonPropertyInfo propertyInfo, LocalizedTextOption localizedTextOption, GameStringItemDictionary gameStringItemDictionary)
    {
        if (propertyInfo.PropertyType != typeof(GameStringText) && propertyInfo.PropertyType != typeof(ISet<GameStringText>))
            return;

        propertyInfo.ShouldSerialize = (element, value) =>
        {
            if (value is null)
                return false;

            if (localizedTextOption == LocalizedTextOption.None)
                return true;

            if (value is GameStringText gameStringText)
            {
                GameStringTextExtractor.AddGameStringText(gameStringItemDictionary, element, propertyInfo, gameStringText);
            }
            else if (value is ISet<GameStringText> gameStringTextCollection)
            {
                foreach (GameStringText gameStringTextItem in gameStringTextCollection)
                    GameStringTextExtractor.AddGameStringText(gameStringItemDictionary, element, propertyInfo, gameStringTextItem, isArray: true);
            }

            return localizedTextOption == LocalizedTextOption.Copy;
        };
    }
}
