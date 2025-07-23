

namespace DalilBus.Config
{
    public class ApiConfig
    {
        // API key for authentication
        public const string ApiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRseGpudmRkbW5penBkcGpucm5iIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mzk2NjAyODEsImV4cCI6MjA1NTIzNjI4MX0.59EryWixS4bafMWKMQd8buAMXLnfduIEOCdmnuPqGEc";

        // Base URL for the API
        public const string BaseUrl = "https://dlxjnvddmnizpdpjnrnb.supabase.co/rest/v1/";

        // Endpoint for fetching places
        public const string PlacesEndpoint = "places?select=id,nameEn,nameAr";

        // Endpoint for fetching companies
        public const string CompaniesEndpoint = "companies?select=id,nameEn,nameAr";

        // Endpoint for fetching travels
        public const string TravelsEndpoint = "travels?select=id,startPlaceID,destinationPlaceID,depatureDate,arrivalDate,depatureTime,arrivalTime,companyID";
    }
}
