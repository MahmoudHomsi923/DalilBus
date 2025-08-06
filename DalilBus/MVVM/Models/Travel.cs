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


        [JsonPropertyName("startPlaceNameAr")]
        public string StartPlaceNameAr { get; set; }


        [JsonPropertyName("startPlaceNameEn")]
        public string startPlaceNameEn { get; set; }


        [JsonPropertyName("destinationPlaceID")]
        public int DestinationPlaceID { get; set; }


        [JsonPropertyName("destinationPlaceNameAr")]
        public string DestinationPlaceNameAr { get; set; }


        [JsonPropertyName("destinationPlaceNameEn")]
        public string DestinationPlaceNameEn { get; set; }


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


        [JsonPropertyName("changes")]
        public int Changes { get; set; }


        [JsonPropertyName("stopPlaceNameAr1")]
        public string? StopPlaceNameAr1 { get; set; }


        [JsonPropertyName("stopPlaceNameEn1")]
        public string? StopPlaceNameEn1 { get; set; }


        [JsonPropertyName("stopTime1")]
        public TimeSpan? StopTime1 { get; set; }


        [JsonPropertyName("stopDuration1")]
        public int? StopDuration1 { get; set; }


        [JsonPropertyName("stopPlaceNameAr2")]
        public string? StopPlaceNameAr2 { get; set; }

        [JsonPropertyName("stopPlaceNameEn2")]
        public string? StopPlaceNameEn2 { get; set; }


        [JsonPropertyName("stopTime2")]
        public TimeSpan? StopTime2 { get; set; }


        [JsonPropertyName("stopDuration2")]
        public int? StopDuration2 { get; set; }


        [JsonPropertyName("stopPlaceNameAr3")]
        public string? StopPlaceNameAr3 { get; set; }

        [JsonPropertyName("stopPlaceNameEn3")]
        public string? StopPlaceNameEn3 { get; set; }


        [JsonPropertyName("stopTime3")]
        public TimeSpan? StopTime3 { get; set; }


        [JsonPropertyName("stopDuration3")]
        public int? StopDuration3 { get; set; }

        public string DepatureDateDisplay => DepatureDate.ToString("dd/MM/yyyy");

        public string DepatureTimeDisplay => DateTime.Today.Add(DepatureTime).ToString("h:mm tt");

        public string ArrivalTimeDisplay => DateTime.Today.Add(ArrivalTime).ToString("h:mm tt");

        public string TimeNote =>
            (ArrivalDate.Date - DepatureDate.Date).TotalDays > 0
            ? $"+{(ArrivalDate.Date - DepatureDate.Date).TotalDays:0}" + StringHelper.GetLocalizedString("يوم", "day")
            : string.Empty;

        public string DurationDisplay => StringHelper.GetLocalizedString(DurationAr, DurationEn);

        public string ChangesDisplay => Changes == 0 
            ? StringHelper.GetLocalizedString("مباشر", "Direct") 
            : StringHelper.GetLocalizedString($"{Changes} تبديل/تبديلات", $"{Changes} change(s)");

        public string NameDisplay => StringHelper.GetLocalizedString(NameAr, NameEn);

        public string StartPlaceDisplay => StringHelper.GetLocalizedString(StartPlaceNameAr, startPlaceNameEn);

        public string DestinationPlaceDisplay => StringHelper.GetLocalizedString(DestinationPlaceNameAr, DestinationPlaceNameEn);

        public string StopDuration1Display => StopDuration1.HasValue ? $"{StopDuration1} {StringHelper.GetLocalizedString("دقيقة", "minutes")}" : string.Empty;

        public string StopDuration2Display => StopDuration2.HasValue ? $"{StopDuration2} {StringHelper.GetLocalizedString("دقيقة", "minutes")}" : string.Empty;

        public string StopDuration3Display => StopDuration3.HasValue ? $"{StopDuration3} {StringHelper.GetLocalizedString("دقيقة", "minutes")}" : string.Empty;
    }
}
