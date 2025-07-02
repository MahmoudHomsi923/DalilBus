using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilBus.Config
{
    internal class ApiConfig
    {
        // API key for authentication
        public const string ApiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRseGpudmRkbW5penBkcGpucm5iIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mzk2NjAyODEsImV4cCI6MjA1NTIzNjI4MX0.59EryWixS4bafMWKMQd8buAMXLnfduIEOCdmnuPqGEc";

        // Base URL for the API
        public const string BaseUrl = "https://dlxjnvddmnizpdpjnrnb.supabase.co/rest/v1/";

        // Endpoint for fetching places
        public const string PlacesEndpoint = "places?select=id,nameEn,nameAr";
    }
}
