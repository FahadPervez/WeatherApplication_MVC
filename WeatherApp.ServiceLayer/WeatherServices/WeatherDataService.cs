namespace WeatherApp.ServiceLayer.WeatherServices
{
    #region using

    using WeatherApp.ModelLayer;
    using System.Net;
    using Newtonsoft.Json;
    using WeatherApp.ModelLayer.Models;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Net.Http;
    using System.IO;
    using System.Net.Http.Headers;
    using WeatherApp.ServiceLayer.IWeatherServices;
    using WeatherApp.ServiceLayer.ApiConfiguration;

    #endregion

    /// <summary>
    /// The class representing weather service
    /// </summary>
    public class WeatherDataService : WeatherDataSource
    {
        #region fields

        private IExceptionService exceptionService;

        #endregion

        /// <summary>
        /// Weather data service constructor
        /// </summary>
        public WeatherDataService(IExceptionService exceptionService)
        {
            //this.weatherDataViewModel = weatherDataViewModel;
            this.exceptionService = exceptionService;
        }

        /// <summary>
        /// Get weather data from api
        /// </summary>
        public override WeatherDataViewModel GetWeatherDataFromApi(WeatherDataViewModel weatherDataViewModel)
        {
            if (string.IsNullOrWhiteSpace(weatherDataViewModel.Location) || weatherDataViewModel.Location.Contains(","))
            {
                weatherDataViewModel.ErrorMessage = UserFriendlyMessages.ValidLocationMessage;
                return weatherDataViewModel;
            }

            string ApiUrl = ApiConfiguration.GenerateApiUrl(weatherDataViewModel.Location);

            if (!string.IsNullOrWhiteSpace(ApiUrl))
            {
                return ConnectAndDeserilizeWeatherData(ApiUrl, weatherDataViewModel);
            }

            weatherDataViewModel.ErrorMessage = UserFriendlyMessages.DefaultMessage;
            weatherDataViewModel.DisplayWeatherReport = false;
            return weatherDataViewModel;

        }

        /// <summary>
        /// Connect weather data api and get data 
        /// </summary>        
        private WeatherDataViewModel ConnectAndDeserilizeWeatherData(string ApiUrl, WeatherDataViewModel weatherDataViewModel)
        {
            using (HttpClient weatherClient = new HttpClient())
            {
                weatherClient.BaseAddress = new Uri(ApiUrl);
                weatherClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    using (HttpResponseMessage weatherDataResponse = weatherClient.GetAsync(ApiUrl).Result)
                    {
                        if (weatherDataResponse.IsSuccessStatusCode)
                        {
                            var dataContentResult = weatherDataResponse.Content.ReadAsStringAsync().Result;
                            var deserilizeJsonObject = JsonConvert.DeserializeObject<RootModel>(dataContentResult.ToString());
                            RootModel root = deserilizeJsonObject;
                            MapWeatherDataModel(root, weatherDataViewModel);
                        }
                        else
                        {
                            weatherDataViewModel.ErrorMessage = exceptionService.UserFriendlyExceptionMessage(weatherDataResponse);
                        }
                    }
                }
                catch (Exception exception)
                {
                    weatherDataViewModel.ErrorMessage = UserFriendlyMessages.DefaultMessage;
                    weatherDataViewModel.ErrorDetails = exception.StackTrace.ToString();
                    weatherDataViewModel.DisplayWeatherReport = false;
                }

                return weatherDataViewModel;
            }
        }

        /// <summary>
        /// Mapping the root model into weather data view model.
        /// </summary>        
        private void MapWeatherDataModel(RootModel root, WeatherDataViewModel weatherDataViewModel)
        {
            weatherDataViewModel.Location = root.Name ?? string.Empty;
            weatherDataViewModel.CurrentTemperature = string.Format("{0} C\u00B0", root.Main.Temp) ?? string.Empty;
            weatherDataViewModel.MaximumTemperature = string.Format("{0} C\u00B0", root.Main.Temp_max) ?? string.Empty;
            weatherDataViewModel.MinimumTemperature = string.Format("{0} C\u00B0", root.Main.Temp_min) ?? string.Empty;
            weatherDataViewModel.Pressure = string.Format("{0}", root.Main.Pressure) ?? string.Empty;
            weatherDataViewModel.Humidity = string.Format("{0} %", root.Main.Humidity) ?? string.Empty;
            weatherDataViewModel.Sunrise = ConvertUnixToTime(root.Sys.Sunrise) ?? string.Empty;
            weatherDataViewModel.Sunset = ConvertUnixToTime(root.Sys.Sunset) ?? string.Empty;
            weatherDataViewModel.DisplayWeatherReport = true;
        }

        /// <summary>
        /// Converts the unix time into date time.
        /// </summary>
        private string ConvertUnixToTime(double unixTime)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            return dateTime.AddSeconds(unixTime).ToLocalTime().ToShortTimeString();
        }
    }
}
