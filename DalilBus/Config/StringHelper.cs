using System.Globalization;

namespace DalilBus.Config
{
    public static class StringHelper
    {
        public static string GetLocalizedString(string arValue, string enValue)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return arValue;
            else
                return enValue;
        }
    }
}
