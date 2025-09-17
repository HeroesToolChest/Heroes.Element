namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to convert hero talents to and from JSON.
/// </summary>
public class HeroTalentsConverter : JsonConverter<IDictionary<TalentTier, IList<Talent>>>
{
    /// <inheritdoc/>
    public override IDictionary<TalentTier, IList<Talent>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        IDictionary<TalentTier, IList<Talent>>? talentsByTier = JsonSerializer.Deserialize<IDictionary<TalentTier, IList<Talent>>>(ref reader, options);

        if (talentsByTier is null)
            return null;

        foreach (KeyValuePair<TalentTier, IList<Talent>> tierTalents in talentsByTier)
        {
            foreach (Talent talent in tierTalents.Value)
            {
                talent.Tier = tierTalents.Key;
            }
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
