//namespace Heroes.Element.JsonConverters;

///// <summary>
///// Converter to convert <see cref="ISet{TalentId}"/> to and from JSON.
///// </summary>
//public class TooltipAppenderTalentIdConverter : JsonConverter<ISet<TalentId>>
//{
//    /// <inheritdoc/>
//    public override ISet<TalentId>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        HashSet<TalentId> items = [];

//        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
//        {
//            string? value = reader.GetString();
//            if (value is null)
//                continue;

//            string[] parts = value.Split('|');
//            if (parts.Length == 2)
//            {
//                items.Add(new TalentId(parts[0], parts[1]));
//            }
//        }

//        return items;
//    }

//    /// <inheritdoc/>
//    public override void Write(Utf8JsonWriter writer, ISet<TalentId> value, JsonSerializerOptions options)
//    {
//        writer.WriteStartArray();

//        foreach (TalentId item in value)
//        {
//            writer.WriteStringValue(item.ToString());
//        }

//        writer.WriteEndArray();
//    }
//}
