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

        [JsonPropertyName("changes")]
        public int Changes { get; set; }

        public string DepatureTimeDisplay => DateTime.Today.Add(DepatureTime).ToString("h:mm tt");

        public string ArrivalTimeDisplay => DateTime.Today.Add(ArrivalTime).ToString("h:mm tt");

        public string TimeNote =>
            (ArrivalDate.Date - DepatureDate.Date).TotalDays > 0
            ? $"+{(ArrivalDate.Date - DepatureDate.Date).TotalDays:0}" + StringHelper.GetLocalizedString("يوم", "day")
            : string.Empty;


        public string DurationDisplay
        {
            get
            {
                var duration = ArrivalTime - DepatureTime;
                if (duration < TimeSpan.Zero)
                    duration += TimeSpan.FromDays(1); // falls Ankunft am nächsten Tag

                // Format: 4:45 hrs
                return $"{(int)duration.TotalHours}:{duration.Minutes:D2} " + StringHelper.GetLocalizedString("س", "hrs");
            }
        }

        public string ChangesDisplay => Changes == 0 
            ? StringHelper.GetLocalizedString("مباشر", "Direct") 
            : StringHelper.GetLocalizedString($"{Changes} تبديل/تبديلات", $"{Changes} change(s)");
    
    }
}
