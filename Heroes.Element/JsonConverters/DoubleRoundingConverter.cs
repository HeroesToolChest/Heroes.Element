namespace Heroes.Element.JsonConverters;

/// <summary>
/// Converter to round <see cref="double"/> types to 4 decimal places.
/// </summary>
public class DoubleRoundingConverter : JsonConverter<double>
{
    /// <inheritdoc/>
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDouble();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(Math.Round(value, 4));
    }
}
