namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert the unit abilities dictionary to and from JSON.
/// </summary>
public class UnitAbilitiesConverter : JsonConverter<IDictionary<AbilityTier, IList<Ability>>>
{
    /// <inheritdoc/>
    public override IDictionary<AbilityTier, IList<Ability>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        IDictionary<AbilityTier, IList<Ability>>? abilitiesByTier = JsonSerializer.Deserialize<IDictionary<AbilityTier, IList<Ability>>>(ref reader, options);

        if (abilitiesByTier is null)
            return null;

        foreach (KeyValuePair<AbilityTier, IList<Ability>> tierAbilities in abilitiesByTier)
        {
            foreach (Ability ability in tierAbilities.Value)
            {
                ability.Tier = tierAbilities.Key;
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
