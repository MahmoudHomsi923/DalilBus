using System.Text.Json.Serialization;

namespace DalilBus.MVVM.Models
{
    public class Place
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
