using System.Globalization;
using System.Text.Json.Serialization;

namespace DalilBus.MVVM.Models
{
    /// <summary>
    /// Represents a travle
    /// </summary>
    public class Travel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("startPlaceID")]
        public int StartPlaceID { get; set; }

        [JsonPropertyName("destinationPlaceID")]
        public int DestinationPlaceID { get; set; }

        [JsonPropertyName("depatureDate")]
        public DateTime DepatureDate { get; set; }

        [JsonPropertyName("arrivalDate")]
        public DateTime ArrivalDate { get; set; }

        [JsonPropertyName("depatureTime")]
        public TimeSpan DepatureTime { get; set; }

        [JsonPropertyName("arrivalTime")]
        public TimeSpan ArrivalTime { get; set; }

        [JsonPropertyName("companyID")]
        public int CompanyID { get; set; }
    }
}
