namespace Heroes.Element.JsonConverters;

/// <summary>
/// A converter to convert <see cref="GameStringItemDictionary"/> to and from JSON.
/// </summary>
public class GameStringItemDictionaryConverter : JsonConverter<GameStringItemDictionary>
{
    /// <inheritdoc/>
    public override GameStringItemDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        GameStringItemDictionary dictionary = [];

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return dictionary;

            string itemKey = reader.GetString()!;

            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            GameStringFilePropertyName propertyName = [];

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                string propertyNameKey = reader.GetString()!;

                reader.Read();
                if (reader.TokenType != JsonTokenType.StartObject)
                    throw new JsonException();

                GameStringFilePropertyId propertyId = new();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                        break;

                    string propertyIdKey = reader.GetString()!;

                    reader.Read();

                    if (reader.TokenType == JsonTokenType.StartArray)
                    {
                        List<GameStringText> list = JsonSerializer.Deserialize<List<GameStringText>>(ref reader, options)!;
                        propertyId.KeyArrayPairs[propertyIdKey] = list;
                    }
                    else
                    {
                        GameStringText text = JsonSerializer.Deserialize<GameStringText>(ref reader, options)!;
                        propertyId.KeyValuePairs[propertyIdKey] = text;
                    }
                }

                propertyName[propertyNameKey] = propertyId;
            }

            dictionary[itemKey] = propertyName;
        }

        throw new JsonException();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, GameStringItemDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, GameStringFilePropertyName> item in value)
        {
            writer.WritePropertyName(item.Key);
            writer.WriteStartObject();

            foreach (KeyValuePair<string, GameStringFilePropertyId> propertyName in item.Value)
            {
                writer.WritePropertyName(propertyName.Key);
                writer.WriteStartObject();

                GameStringFilePropertyId propertyId = propertyName.Value;

                if (propertyId.KeyArrayPairs.Count > 0)
                {
                    foreach (KeyValuePair<string, List<GameStringText>> kvp in propertyId.KeyArrayPairs)
                    {
                        writer.WritePropertyName(kvp.Key);
                        JsonSerializer.Serialize(writer, kvp.Value, options);
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, GameStringText> kvp in propertyId.KeyValuePairs)
                    {
                        writer.WritePropertyName(kvp.Key);
                        JsonSerializer.Serialize(writer, kvp.Value, options);
                    }
                }

                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }

        writer.WriteEndObject();
    }
}
