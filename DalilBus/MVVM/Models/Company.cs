using System.Globalization;
using System.Text.Json.Serialization;

namespace DalilBus.MVVM.Models
{
    /// <summary>
    /// Represents a company with multilingual support
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Unique identifier for the company
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Arabic name of the company
        /// </summary>
        [JsonPropertyName("nameAr")]
        public string NameAr { get; set; } = string.Empty;

        /// <summary>
        /// English name of the company
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
