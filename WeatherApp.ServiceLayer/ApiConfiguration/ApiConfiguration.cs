namespace WeatherApp.ServiceLayer.ApiConfiguration
{
    /// <summary>
    /// This class represents configuration for api.
    /// </summary>
    public static class ApiConfiguration
    {
        #region properties

        private const string ApiKey = "";

        private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?q";

        #endregion

        #region methods

        /// <summary>
        /// Generate weather service api url by city name.
        /// </summary>
        public static string GenerateApiUrl(string CityName)
        {
            string resultUrl = string.Empty;

            if (!string.IsNullOrWhiteSpace(ApiUrl) && !string.IsNullOrWhiteSpace(ApiKey))
            {
                resultUrl = string.Format("{0}={1}&APPID={2}&units=metric", ApiUrl, CityName, ApiKey);
            }

            return resultUrl;
        }

        #endregion

    }
}
