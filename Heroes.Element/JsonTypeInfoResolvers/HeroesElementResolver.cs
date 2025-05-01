using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes.Element.JsonTypeInfoResolvers;

public class HeroesElementResolver : DefaultJsonTypeInfoResolver
{
    /// <inheritdoc/>
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        //if (type == typeof(Hero))
        //{
        //    JsonPropertyInfo? propertyToRemove = jsonTypeInfo.Properties.FirstOrDefault(p => p.Name == nameof(Unit.UnitPortraits));

        //    if (propertyToRemove != null)
        //        jsonTypeInfo.Properties.Remove(propertyToRemove);
        //}

        return jsonTypeInfo;
    }
}
