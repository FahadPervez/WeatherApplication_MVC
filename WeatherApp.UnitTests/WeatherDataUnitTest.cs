namespace WeatherApp.UnitTests
{
    #region using

    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using WeatherApp.ModelLayer;
    using WeatherApp.ModelLayer.Models;
    using WeatherApp.ServiceLayer.ApiConfiguration;
    using WeatherApp.ServiceLayer.WeatherServices;

    #endregion

    [TestClass]
    public class WeatherDataUnitTest
    {
        [TestMethod]
        public void WeatherDataJsonResultUnitTest()
        {   
            RootModel root = null;

            string jsonFileLocation = @"C:\DELL\Weather.json";

            using (StreamReader reader = new StreamReader(jsonFileLocation))
            {
                string json = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<RootModel>(json);

                 root = result;
            }

            Assert.IsNotNull(root);
        }

        [TestMethod]
        public void ExtractingWeatherViewModelUnitTest()
        {
            RootModel root = null;

            string jsonFileLocation = @"C:\DELL\Weather.json";

            using (StreamReader reader = new StreamReader(jsonFileLocation))
            {
                string json = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<RootModel>(json);

                root = result;
            }

            WeatherDataViewModel weatherDataViewModel = new WeatherDataViewModel();
            weatherDataViewModel.Location = root.Name;
            weatherDataViewModel.CurrentTemperature = string.Format("{0} C\u00B0",  root.Main.Temp);
            weatherDataViewModel.MaximumTemperature = string.Format("{0} C\u00B0", root.Main.Temp_max);
            weatherDataViewModel.MinimumTemperature = string.Format("{0} C\u00B0", root.Main.Temp_min);
            weatherDataViewModel.Pressure = string.Format("{0}", root.Main.Pressure);
            weatherDataViewModel.Humidity = string.Format("{0} %", root.Main.Humidity);
            weatherDataViewModel.Sunrise = ConvertUnixToTime(root.Sys.Sunrise);
            weatherDataViewModel.Sunset = ConvertUnixToTime(root.Sys.Sunset);
            
            string ConvertUnixToTime(double unixTime)
            {
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return dt.AddSeconds(unixTime).ToLocalTime().ToShortTimeString();
            }
            
            Assert.IsNotNull(weatherDataViewModel);
        }

        [TestMethod]
        public void GenerateUrlUnitTest()
        {
            string cityName = "Cambridge";
            string url = ApiConfiguration.GenerateApiUrl(cityName);

            Assert.IsNotNull(url);
        }

        [TestMethod]
        public void WeatherDataApiUnitTest()
        { 
            WeatherDataViewModel weatherViewModel = new WeatherDataViewModel();
            weatherViewModel.Location = "Cambridge";

            WeatherDataService weatherDataService = new WeatherDataService(new ExceptionService());
            var weatherDataResult = weatherDataService.GetWeatherDataFromApi(weatherViewModel);

            Assert.IsNotNull(weatherDataResult);
        }

        [TestMethod]
        public void ExtractingEmptyWeatherDataUnitTest()
        {
            RootModel root = null;
            WeatherDataViewModel weatherDataViewModel = new WeatherDataViewModel();

            string jsonFileLocation = @"";

            if (!string.IsNullOrWhiteSpace(jsonFileLocation))
            {
                using (StreamReader reader = new StreamReader(jsonFileLocation))
                {
                    string json = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<RootModel>(json);

                    root = result;
                }
                                
                weatherDataViewModel.Location = root.Name;
                weatherDataViewModel.CurrentTemperature = string.Format("{0} C\u00B0", root.Main.Temp);
                weatherDataViewModel.MaximumTemperature = string.Format("{0} C\u00B0", root.Main.Temp_max);
                weatherDataViewModel.MinimumTemperature = string.Format("{0} C\u00B0", root.Main.Temp_min);
                weatherDataViewModel.Pressure = string.Format("{0}", root.Main.Pressure);
                weatherDataViewModel.Humidity = string.Format("{0} %", root.Main.Humidity);
                weatherDataViewModel.Sunrise = ConvertUnixToTime(root.Sys.Sunrise);
                weatherDataViewModel.Sunset = ConvertUnixToTime(root.Sys.Sunset);

                string ConvertUnixToTime(double unixTime)
                {
                    DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    return dt.AddSeconds(unixTime).ToLocalTime().ToShortTimeString();
                }
            }
            Assert.AreEqual(weatherDataViewModel.DisplayWeatherReport, false);
        }
    }
}
