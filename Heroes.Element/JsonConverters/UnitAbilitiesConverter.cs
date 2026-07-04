namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert the unit abilities dictionary to and from JSON.
/// </summary>
public class UnitAbilitiesConverter : JsonConverter<IDictionary<AbilityTier, IList<Ability>>>
{
    /// <inheritdoc/>
    public override IDictionary<AbilityTier, IList<Ability>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException($"Expected StartObject, got {reader.TokenType}.");

        SortedDictionary<AbilityTier, IList<Ability>> abilitiesByTier = [];

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException($"Expected PropertyName, got {reader.TokenType}.");

            AbilityTier tier = Enum.Parse<AbilityTier>(reader.GetString()!);

            reader.Read(); // -> array start

            IList<Ability>? abilities = JsonSerializer.Deserialize<IList<Ability>>(ref reader, options);

            if (abilities is not null)
            {
                foreach (Ability ability in abilities)
                    ability.Tier = tier;

                abilitiesByTier[tier] = abilities;
            }
        }

        return abilitiesByTier;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IDictionary<AbilityTier, IList<Ability>> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<AbilityTier, IList<Ability>> tierAbilities in value)
        {
            writer.WritePropertyName(tierAbilities.Key.ToString());

            IEnumerable<Ability> sortedTierAbilities = tierAbilities.Value.OrderBy(x => x.AbilityType);

            JsonSerializer.Serialize(writer, sortedTierAbilities, options);
        }

        writer.WriteEndObject();
    }
}
