using System.Globalization;
using System.Text.Json.Serialization;

namespace DalilBus.MVVM.Models
{
    /// <summary>
    /// Represents a location/place with multilingual support
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Unique identifier for the place
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Arabic name of the place
        /// </summary>
        [JsonPropertyName("nameAr")]
        public string NameAr { get; set; } = string.Empty;

        /// <summary>
        /// English name of the place
        /// </summary>
        [JsonPropertyName("nameEn")]
        public string NameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets the combined display name in culture-specific order.
        /// - Format: "Arabic English" for Arabic culture (ar)
        /// - Format: "English Arabic" for all other cultures
        /// Note: Calculated once during initialization (no runtime updates)
        /// </summary>
        public string Name => CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{NameAr} {NameEn}" : $"{NameEn} {NameAr}";

    }
}