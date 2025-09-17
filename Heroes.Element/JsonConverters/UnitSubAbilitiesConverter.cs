namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert the unit subabilities to and from JSON.
/// </summary>
public class UnitSubAbilitiesConverter : JsonConverter<IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>>
{
    /// <inheritdoc/>
    public override IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>? subAbilitiesByLinkId = JsonSerializer.Deserialize<IDictionary<LinkId, IDictionary<AbilityTier, IList<Ability>>>>(ref reader, options);

        if (subAbilitiesByLinkId is null)
            return null;

        foreach (IDictionary<AbilityTier, IList<Ability>> subAbilitiesByTier in subAbilitiesByLinkId.Values)
        {
            foreach (KeyValuePair<AbilityTier, IList<Ability>> tierAbilities in subAbilitiesByTier)
            {
                foreach (Ability ability in tierAbilities.Value)
                {
                    ability.Tier = tierAbilities.Key;
                }
            }
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
}
