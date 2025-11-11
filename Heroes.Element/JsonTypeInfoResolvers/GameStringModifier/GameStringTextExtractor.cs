using System.Runtime.CompilerServices;
using GstInnerProperty = Heroes.Element.JsonTypeInfoResolvers.GameStringModifier.GameStringTextExtractorProperties;

namespace Heroes.Element.JsonTypeInfoResolvers.GameStringModifier;

internal static class GameStringTextExtractor
{
    // child (e.g., UnitLife/UnitEnergy/UnitShield) -> owning element (Unit/Hero)
    private static readonly ConditionalWeakTable<object, IElementObject> _ownerByUnitChildObjects = [];

    public static void SetOwner(object child, IElementObject owner)
    {
        _ownerByUnitChildObjects.TryAdd(child, owner);
    }

    public static void AddGameStringText(GameStringItemDictionary gameStringElements, object @object, JsonPropertyInfo propertyInfo, GameStringText gameStringText)
    {
        string elementName;
        string propertyName;
        string id;

        if (@object is IElementObject elementObject)
        {
            if (elementObject is Hero)
                elementName = "Hero";
            else
                elementName = propertyInfo.DeclaringType.Name;

            propertyName = propertyInfo.Name;
            id = elementObject.Id;
        }
        else if (@object is Ability ability)
        {
            elementName = "AbilTalent";
            propertyName = propertyInfo.Name;
            id = ability.LinkId.ToString();
        }
        else if (@object is Talent talent)
        {
            elementName = "AbilTalent";
            propertyName = propertyInfo.Name;
            id = talent.LinkId.ToString();
        }
        else
        {
            // inner object property
            (elementName, propertyName, id) = GetPropertyNames(@object, propertyInfo);
        }

        // set the gamestring text in the collection
        if (gameStringElements.TryGetValue(elementName, out GameStringFilePropertyName? gameStringPropertyName))
        {
            if (gameStringPropertyName.TryGetValue(propertyName, out GameStringFilePropertyId? gameString))
            {
                // if exists, append to it with comma
                if (gameString.TryGetValue(id, out GameStringText? existingGameStringText))
                    gameStringText = new GameStringText($"{existingGameStringText.RawText},{gameStringText.RawText}");

                gameString[id] = gameStringText;
            }
            else
            {
                gameStringPropertyName[propertyName] = new GameStringFilePropertyId()
                {
                    [id] = gameStringText,
                };
            }
        }
        else
        {
            gameStringElements[elementName] = new GameStringFilePropertyName()
            {
                [propertyName] = new GameStringFilePropertyId()
                {
                    [id] = gameStringText,
                },
            };
        }
    }

    private static (string ElementName, string PropertyName, string Id) GetPropertyNames(object @object, JsonPropertyInfo propertyInfo)
    {
        if (_ownerByUnitChildObjects.TryGetValue(@object, out IElementObject? owner))
        {
            return ("Unit", GetPropertyName(propertyInfo), owner.Id);
        }

        return ("Unknown", GetPropertyName(propertyInfo), @object.GetHashCode().ToString());
    }

    private static string GetPropertyName(JsonPropertyInfo propertyInfo)
    {
        return propertyInfo.DeclaringType.Name switch
        {
            "UnitLife" => GstInnerProperty.UnitLife.PropertyName,
            "UnitEnergy" => GstInnerProperty.UnitEnergy.PropertyName,
            "UnitShield" => GstInnerProperty.UnitShield.PropertyName,
            _ => "Unknown",
        };
    }
}
