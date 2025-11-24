using System.Runtime.CompilerServices;
using GstInnerProperty = Heroes.Element.JsonTypeInfoResolvers.GameStringModifier.GameStringTextExtractorProperties;

namespace Heroes.Element.JsonTypeInfoResolvers.GameStringModifier;

internal static class GameStringTextExtractor
{
    // child (e.g., UnitLife/UnitEnergy/UnitShield) -> owning element (Unit/Hero)
    private static readonly ConditionalWeakTable<object, IElementObject> _ownerByUnitChildObjects = [];
    private static readonly ConditionalWeakTable<object, string> _typeNameByUnitChildObjects = [];

    public static void SetOwner(object child, IElementObject owner, string typeName)
    {
        _ownerByUnitChildObjects.TryAdd(child, owner);
        _typeNameByUnitChildObjects.TryAdd(child, typeName);
    }

    public static void AddGameStringText(GameStringItemDictionary gameStringElements, object @object, JsonPropertyInfo propertyInfo, GameStringText gameStringText, bool appendValue = false)
    {
        string elementName;
        string propertyName;
        string id;

        if (@object is IElementObject elementObject)
        {
            elementName = elementObject switch
            {
                Hero => "hero",
                Announcer => "announcer",
                _ => JsonNamingPolicy.CamelCase.ConvertName(propertyInfo.DeclaringType.Name),
            };

            propertyName = JsonNamingPolicy.CamelCase.ConvertName(propertyInfo.Name);
            id = elementObject.Id;
        }
        else if (@object is Ability ability)
        {
            elementName = "ability";
            propertyName = JsonNamingPolicy.CamelCase.ConvertName(propertyInfo.Name);
            id = ability.LinkId.ToString();
        }
        else if (@object is Talent talent)
        {
            elementName = "talent";
            propertyName = JsonNamingPolicy.CamelCase.ConvertName(propertyInfo.Name);
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
                if (appendValue && gameString.TryGetValue(id, out GameStringText? existingGameStringText))
                    gameStringText = new GameStringText($"{existingGameStringText.RawText}|{gameStringText.RawText}");

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
        if (_ownerByUnitChildObjects.TryGetValue(@object, out IElementObject? owner) && _typeNameByUnitChildObjects.TryGetValue(@object, out string? declaringTypeName))
        {
            return (declaringTypeName, GetPropertyName(propertyInfo), owner.Id);
        }

        return ("unknown", GetPropertyName(propertyInfo), @object.GetHashCode().ToString());
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
