using DalilBus.Config;
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

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("nameAr")]
        public string NameAr { get; set; }

        [JsonPropertyName("nameEn")]
        public string NameEn { get; set; }

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

        [JsonPropertyName("durationAr")]
        public string DurationAr { get; set; }

        [JsonPropertyName("durationEn")]
        public string DurationEn { get; set; }

        [JsonPropertyName("stops")]
        public int Stops { get; set; }

        public string DepatureTimeDisplay => DateTime.Today.Add(DepatureTime).ToString("h:mm tt");

        public string ArrivalTimeDisplay => DateTime.Today.Add(ArrivalTime).ToString("h:mm tt");

        public string TimeNote =>
            (ArrivalDate.Date - DepatureDate.Date).TotalDays > 0
            ? $"+{(ArrivalDate.Date - DepatureDate.Date).TotalDays:0}" + StringHelper.GetLocalizedString("يوم", "day")
            : string.Empty;

        public string DurationDisplay => StringHelper.GetLocalizedString(DurationAr, DurationEn);

        public string StopsDisplay => Stops == 0 
            ? StringHelper.GetLocalizedString("مباشر", "Direct") 
            : StringHelper.GetLocalizedString($"{Stops} تبديل/تبديلات", $"{Stops} change(s)");

        public string NameDisplay => StringHelper.GetLocalizedString(NameAr, NameEn);
    }
}
