using System.Text.Json.Serialization;
namespace WebAppRazorSandwitchClient
{
    public record class SandwitchModel(
        
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("price")] double Price
        );
    
}
