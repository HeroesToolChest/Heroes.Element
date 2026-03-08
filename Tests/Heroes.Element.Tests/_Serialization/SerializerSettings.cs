namespace Heroes.Element.Serialization.Tests;

public class SerializerSettings
{
    internal SerializerSettings()
    {
    }

    public GameStringItemDictionary ItemDictionary { get; } = [];

    public static SerializerSettings Create()
    {
        return new SerializerSettings();
    }

    public JsonSerializerOptions GetJsonSerializerDataOptions()
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
                new GameStringTextConverter(new GameStringTextConverterOptions() { GameStringTextType = GameStringTextType.RawText }),
                new HeroesDataVersionConverter(),
            },
            TypeInfoResolver = new HeroesElementResolver()
            {
                Modifiers =
                {
                    typeInfo => JsonTypeInfoModifiers.SerializationModifiers(typeInfo, LocalizedTextOption.Copy, ItemDictionary),
                },
            },
        };
    }
}
