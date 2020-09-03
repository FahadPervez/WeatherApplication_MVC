using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.ModelLayer
{
    /// <summary>
    /// The model class representing a weather app data view model.
    /// </summary>
    public class WeatherDataViewModel
    {
        #region properties
        public string Location
        {
            get;
            set;
        }

        public string CurrentTemperature
        {
            get;
            set;
        }

        public string MaximumTemperature
        {
            get;
            set;
        }

        public string MinimumTemperature
        {
            get;
            set;
        }

        public string Pressure
        {
            get;
            set;
        }

        public string Humidity
        {
            get;
            set;
        }

        public string Sunrise
        {
            get;
            set;
        }

        public string Sunset
        {
            get;
            set;
        }

        public bool DisplayWeatherReport
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public string ErrorDetails
        {
            get;
            set;
        }
        #endregion
    }
}