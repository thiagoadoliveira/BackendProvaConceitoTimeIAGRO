using CatalogoLivros.Shared.Utils;
using System.Text.Json.Serialization;

namespace CatalogoLivros.Domain.Entities;

public class Specification
{
    [JsonPropertyName("Originally published")]
    public string OriginallyPublished { get; set; }
    public string Author { get; set; }
    [JsonPropertyName("Page count")]
    public int PageCount { get; set; }
    [JsonConverter(typeof(JsonHelper))]
    public List<string> Illustrator { get; set; }
    [JsonConverter(typeof(JsonHelper))]
    public List<string> Genres { get; set; }
}
