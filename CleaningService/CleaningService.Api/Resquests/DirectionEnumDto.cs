using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CleaningService.Api.Requests;

[JsonConverter(typeof(StringEnumConverter))]
public enum DirectionEnumDto
{
    North,
    South,
    East,
    West
}