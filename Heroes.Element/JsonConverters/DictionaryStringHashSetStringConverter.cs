using System.Collections.Immutable;
using System.Text.Json;

namespace Heroes.Element.JsonConverters;

public class DictionaryStringHashSetStringConverter : JsonConverter<IDictionary<string, SortedSet<string>>>
{
    private static readonly JsonConverter<IDictionary<string, SortedSet<string>>> _defaultConverter = (JsonConverter<IDictionary<string, SortedSet<string>>>)JsonSerializerOptions.Default.GetConverter(typeof(IDictionary<string, SortedSet<string>>));

    public override IDictionary<string, SortedSet<string>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            return _defaultConverter.Read(ref reader, typeToConvert, options);
        }
        else
        {
            // old way so need to custom parse it out
            // "skins": [
            //  {
            //    "Raynor": [
            //        "RaynorPatriot"
            //    ]
            //  },
            //  {
            //    "Azmodan": [
            //        "AzmodunkBundleProduct"
            //    ]
            //  }
            // ]
            Dictionary<string, SortedSet<string>>? dictionary = [];

            using JsonDocument document = JsonDocument.ParseValue(ref reader);

            JsonElement rootElement = document.RootElement;

            foreach (JsonElement arrayElement in rootElement.EnumerateArray())
            {
                foreach (JsonProperty propertyElement in arrayElement.EnumerateObject())
                {
                    dictionary.Add(propertyElement.Name, new SortedSet<string>(propertyElement.Value.EnumerateArray().Select(x => x.GetString() ?? string.Empty)));
                }
            }

            return dictionary;
        }
    }

    public override void Write(Utf8JsonWriter writer, IDictionary<string, SortedSet<string>> value, JsonSerializerOptions options)
    {
        _defaultConverter.Write(writer, value, options);
    }
}
