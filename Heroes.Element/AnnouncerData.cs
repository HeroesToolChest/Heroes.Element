using Heroes.Element.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Heroes.Element;

public class AnnouncerData
{
    private JsonDocument _document;

    private AnnouncerData(JsonDocument document)
    {
        _document = document;
    }

    public static AnnouncerData Parse(JsonDocument jsonDocument)
    {
        return new AnnouncerData(jsonDocument);
    }

    public bool TryGetAnnouncerById(string? id, [NotNullWhen(true)] out Announcer? value)
    {
        value = null;

        if (id is null)
            return false;

        if (_document.RootElement.TryGetProperty(id, out JsonElement element))
        {
            value = element.Deserialize<Announcer>(new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter(),
                    new TooltipDescriptionReadConverter(StormLocale.ENUS),
                },
            });

            if (value is not null)
                value.Id = id;


            //value = JsonSerializer.Deserialize<Announcer>(element.Deserialize);
            // value = GetAnnouncerData(id, element);

            return true;
        }

        return false;
    }
}
