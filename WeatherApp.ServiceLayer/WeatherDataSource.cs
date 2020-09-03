namespace WeatherApp.ServiceLayer
{
    #region using

    using WeatherApp.ModelLayer;

    #endregion

    /// <summary>
    /// The abstract class to represent weather data source
    /// </summary>
    public abstract class WeatherDataSource
    {
        /// <summary>
        /// Get weather data from open map weather Api.
        /// </summary>
        public abstract WeatherDataViewModel GetWeatherDataFromApi(WeatherDataViewModel weatherDataViewModel);
    }
}
