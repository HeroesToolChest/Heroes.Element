namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert hero talents to and from JSON.
/// </summary>
public class HeroTalentsConverter : JsonConverter<IDictionary<TalentTier, IList<Talent>>>
{
    /// <inheritdoc/>
    public override IDictionary<TalentTier, IList<Talent>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token.");

        Dictionary<TalentTier, IList<Talent>> talentsByTier = [];

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException("Expected PropertyName token.");

            TalentTier tier = Enum.Parse<TalentTier>(reader.GetString()!);

            reader.Read();

            IList<Talent> talents = JsonSerializer.Deserialize<IList<Talent>>(ref reader, options) ?? [];

            foreach (Talent talent in talents)
            {
                talent.Tier = tier;
            }

            talentsByTier[tier] = talents;
        }

        return talentsByTier;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IDictionary<TalentTier, IList<Talent>> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<TalentTier, IList<Talent>> tierTalents in value)
        {
            writer.WritePropertyName(tierTalents.Key.ToString());

            IEnumerable<Talent> sortedTierTalents = tierTalents.Value.OrderBy(x => x.Column);

            JsonSerializer.Serialize(writer, sortedTierTalents, options);
        }

        writer.WriteEndObject();
    }
}
