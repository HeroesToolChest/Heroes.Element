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
    /// <param name="gameStringElements">The collection for the saved gamestrings.</param>
    public static void SerialiazationModifiers(JsonTypeInfo typeInfo, LocalizedTextOption localizedTextOption, GameStringElementName gameStringElements)
    {
        foreach (JsonPropertyInfo propertyInfo in typeInfo.Properties)
        {
            IEnumerableModifier(propertyInfo);
            GameStringTextModifier(propertyInfo, localizedTextOption, gameStringElements);

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

    private static void GameStringTextModifier(JsonPropertyInfo propertyInfo, LocalizedTextOption localizedTextOption, GameStringElementName gameStringElements)
    {
        if (propertyInfo.PropertyType == typeof(GameStringText))
        {
            propertyInfo.ShouldSerialize = (element, value) =>
            {
                if (value is null)
                    return false;

                if (localizedTextOption != LocalizedTextOption.None)
                {
                    if (value is GameStringText gst)
                        AddGameStringText(gameStringElements, element, propertyInfo, gst);

                    return localizedTextOption == LocalizedTextOption.Copy;
                }

                return true;
            };
        }
    }

    private static void AddGameStringText(GameStringElementName gameStringElements, object @object, JsonPropertyInfo propertyInfo, GameStringText gameStringText)
    {
        string elementName;
        string id;

        if (@object is IElementObject elementObject)
        {
            if (elementObject is Hero)
            {
                elementName = "Hero";
            }
            else
            {
#if NET9_0_OR_GREATER
                elementName = propertyInfo.DeclaringType.Name;
#else
                elementName = "NotSupportedInNet8";
#endif
            }

            id = elementObject.Id;
        }
        else if (@object is Ability ability)
        {
            elementName = "AbilTalent";
            id = ability.LinkId.ToString();
        }
        else if (@object is Talent talent)
        {
            elementName = "AbilTalent";
            id = talent.LinkId.ToString();
        }
        else
        {
            return;
        }

        if (gameStringElements.TryGetValue(elementName, out GameStringPropertyName? gameStringPropertyName))
        {
            if (gameStringPropertyName.TryGetValue(propertyInfo.Name, out GameStringPropertyId? gameString))
            {
                gameString[id] = gameStringText;
            }
            else
            {
                gameStringPropertyName[propertyInfo.Name] = new GameStringPropertyId()
                {
                    [id] = gameStringText,
                };
            }
        }
        else
        {
            gameStringElements[elementName] = new GameStringPropertyName()
            {
                [propertyInfo.Name] = new GameStringPropertyId()
                {
                    [id] = gameStringText,
                },
            };
        }
    }
}
