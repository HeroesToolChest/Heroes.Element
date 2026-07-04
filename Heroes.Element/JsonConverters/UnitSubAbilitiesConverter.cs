namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert the unit subabilities to and from JSON.
/// </summary>
public class UnitSubAbilitiesConverter : JsonConverter<IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>>
{
    /// <inheritdoc/>
    public override IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException($"Expected StartObject, got {reader.TokenType}.");

        SortedDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>> subAbilitiesByLinkId = new(new LinkIdComparer());

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            LinkId linkId = ParseLinkId(reader.GetString());
            reader.Read(); // -> inner StartObject

            SortedDictionary<AbilityTier, IList<Ability>> tierAbilities = [];

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                AbilityTier tier = Enum.Parse<AbilityTier>(reader.GetString()!);
                reader.Read(); // -> array start

                IList<Ability>? abilities = JsonSerializer.Deserialize<IList<Ability>>(ref reader, options);
                if (abilities is not null)
                {
                    foreach (Ability ability in abilities)
                        ability.Tier = tier;

                    tierAbilities[tier] = abilities;
                }
            }

            subAbilitiesByLinkId[linkId] = tierAbilities;
        }

        return subAbilitiesByLinkId;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<LinkId, IDictionary<AbilityTier, IList<Ability>>> subAbility in value)
        {
            writer.WritePropertyName(subAbility.Key.ToString());
            writer.WriteStartObject();

            foreach (KeyValuePair<AbilityTier, IList<Ability>> tierAbilities in subAbility.Value)
            {
                writer.WritePropertyName(tierAbilities.Key.ToString());

                IEnumerable<Ability> sortedTierAbilities = tierAbilities.Value.OrderBy(x => x.AbilityType);

                JsonSerializer.Serialize(writer, sortedTierAbilities, options);
            }

            writer.WriteEndObject();
        }

        writer.WriteEndObject();
    }

    private static LinkId ParseLinkId(string? key)
    {
        if (string.IsNullOrEmpty(key))
            throw new JsonException("LinkId key was null or empty.");

        ReadOnlySpan<char> spanKey = key.AsSpan();

        Span<Range> ranges = stackalloc Range[4];

        int count = spanKey.Split(ranges, '|');

        if (count == 3)
        {
            return new AbilityLinkId(
                spanKey[ranges[0]].ToString(),
                spanKey[ranges[1]].ToString(),
                Enum.Parse<AbilityType>(spanKey[ranges[2]]));
        }

        if (count == 4)
        {
            return new TalentLinkId(
                spanKey[ranges[0]].ToString(),
                spanKey[ranges[1]].ToString(),
                Enum.Parse<AbilityType>(spanKey[ranges[2]]),
                Enum.Parse<TalentTier>(spanKey[ranges[3]]));
        }

        throw new JsonException($"Unable to parse LinkId from '{key}' — expected 3 or 4 pipe-delimited segments, got {count}.");
    }
}
