namespace Heroes.Element.Serialization.Tests;

public static class SerializerSettings
{
    public static JsonSerializerOptions GetJsonSerializerDataOptions()
    {
        return new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters =
            {
                new JsonStringEnumConverter(),
                new DoubleRoundingConverter(),
                new LinkIdConverter(),
                new AbilityLinkIdConverter(),
                new TalentLinkIdConverter(),
                new GameStringTextConverter(gameStringTextType: GameStringTextType.RawText),
                new HeroesDataVersionConverter(),
            },
            TypeInfoResolver = new HeroesElementResolver()
            {
                Modifiers =
                {
                    typeInfo => JsonTypeInfoModifiers.SerializationModifiers(typeInfo, LocalizedTextOption.None, []),
                },
            },
        };
    }
}
