using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VVapp.CustomJsonConverters;
using VVapp.Models;

namespace VVapp.JsonConverters;

public class OuterwearMetaConverter : JsonConverter<OuterwearMeta>
{
    public override void WriteJson(JsonWriter writer, OuterwearMeta? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override OuterwearMeta? ReadJson(JsonReader reader, Type objectType, 
        OuterwearMeta? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);
        var gender = obj[JsonConstants.Gender]!;
        var seasonsJArray = (JArray)obj[JsonConstants.Seasons]!;
        var stylesJArray = (JArray)obj[JsonConstants.Styles]!;
        var colorsJArray = (JArray)obj[JsonConstants.Colors]!;
        var outerwear = obj[JsonConstants.Subtype]!;
        Console.WriteLine($"Outerwear Jtoken: " + outerwear);
        var meta = new OuterwearMeta
        {
            Id = (int)(obj[JsonConstants.Id] ?? 0),
            Budget = (int)(obj[JsonConstants.Budget] ?? 0),
            Gender = Enum.TryParse<Gender>(gender.ToString(), out var parsedGender) ? parsedGender : Gender.Unisex,
            Colors = colorsJArray.Select(color => 
                Enum.TryParse<ClothColors>(color.ToString(), out var parsed) ? parsed : ClothColors.None)
                .ToArray(),
            Styles = stylesJArray.Select(style => 
                Enum.TryParse<Style>(style.ToString(), out var parsed) ? parsed : Style.None)
                .ToArray(),
            Seasons = seasonsJArray.Select(season => 
                Enum.TryParse<Season>(season.ToString(), out var parsed) ? parsed : Season.Autumn)
                .ToArray(),
            Outerwear = Enum.TryParse<Outerwear>(outerwear.ToString(), out var parsedOuterwear) 
                ? parsedOuterwear : Outerwear.None,
        };

        return meta;
    }
}