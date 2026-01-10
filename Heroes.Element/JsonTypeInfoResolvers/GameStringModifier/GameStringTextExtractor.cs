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

    public static void AddGameStringText(GameStringItemDictionary gameStringElements, object @object, JsonPropertyInfo propertyInfo, GameStringText gameStringText, bool isArray = false)
    {
        string elementName;
        string propertyName;
        string id;

        if (@object is IElementObject elementObject)
        {
            elementName = elementObject switch
            {
                Hero => JsonNamingPolicy.CamelCase.ConvertName(nameof(Hero)),
                Announcer => JsonNamingPolicy.CamelCase.ConvertName(nameof(Announcer)),
                Banner => JsonNamingPolicy.CamelCase.ConvertName(nameof(Banner)),
                Bundle => JsonNamingPolicy.CamelCase.ConvertName(nameof(Bundle)),
                Boost => JsonNamingPolicy.CamelCase.ConvertName(nameof(Boost)),
                Mount => JsonNamingPolicy.CamelCase.ConvertName(nameof(Mount)),
                Spray => JsonNamingPolicy.CamelCase.ConvertName(nameof(Spray)),
                Skin => JsonNamingPolicy.CamelCase.ConvertName(nameof(Skin)),
                VoiceLine => JsonNamingPolicy.CamelCase.ConvertName(nameof(VoiceLine)),
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

            if (elementName == "map" && (propertyName == GstInnerProperty.MapObjectiveTitle.PropertyName || propertyName == GstInnerProperty.MapObjectiveDescription.PropertyName))
                isArray = true;
        }

        // set the gamestring text in the collection
        AddOrUpdateGameStringText(gameStringElements, gameStringText, id, elementName, propertyName, isArray);
    }

    private static void AddOrUpdateGameStringText(GameStringItemDictionary gameStringElements, GameStringText gameStringText, string id, string elementName, string propertyName, bool isArray)
    {
        if (gameStringElements.TryGetValue(elementName, out GameStringFilePropertyName? gameStringPropertyName))
        {
            if (gameStringPropertyName.TryGetValue(propertyName, out GameStringFilePropertyId? gameStringFilePropertyId))
            {
                if (gameStringFilePropertyId.KeyArrayPairs.Count > 0)
                {
                    if (gameStringFilePropertyId.KeyArrayPairs.TryGetValue(id, out List<GameStringText>? existingGstList))
                        existingGstList.Add(gameStringText);
                    else
                        gameStringFilePropertyId.KeyArrayPairs[id] = [gameStringText];
                }
                else
                {
                    gameStringFilePropertyId.KeyValuePairs[id] = gameStringText;
                }
            }
            else
            {
                gameStringFilePropertyId = new();

                if (isArray)
                    gameStringFilePropertyId.KeyArrayPairs[id] = [gameStringText];
                else
                    gameStringFilePropertyId.KeyValuePairs[id] = gameStringText;

                gameStringPropertyName[propertyName] = gameStringFilePropertyId;
            }
        }
        else
        {
            gameStringElements[elementName] = new GameStringFilePropertyName()
            {
                [propertyName] = new GameStringFilePropertyId()
                {
                    KeyValuePairs =
                    {
                        [id] = gameStringText,
                    },
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
            "MapObjective" when propertyInfo.Name == "title" => GstInnerProperty.MapObjectiveTitle.PropertyName,
            "MapObjective" when propertyInfo.Name == "description" => GstInnerProperty.MapObjectiveDescription.PropertyName,
            _ => "Unknown",
        };
    }
}
