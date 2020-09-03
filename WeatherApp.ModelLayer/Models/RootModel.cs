namespace WeatherApp.ModelLayer.Models
{
    #region using

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// This is root class model representing the weather report for api's model
    /// </summary>
    public class RootModel
    {
        public string Name
        {
            get;
            set;
        }

        public SysModel Sys
        {
            get;
            set;
        }

        public MainModel Main
        {
            get;
            set;
        }
    }
}
