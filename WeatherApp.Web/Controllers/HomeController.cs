namespace WeatherApp.Web.Controllers
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using WeatherApp.ModelLayer;
    using WeatherApp.ServiceLayer.WeatherServices;

    #endregion

    /// <summary>
    /// The home controller representing weather app
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new WeatherDataViewModel());
        }

        [HttpPost]
        public ActionResult GetWeatherReport(WeatherDataViewModel weatherDataViewModel)
        {
            WeatherDataService weatherDataService = new WeatherDataService(new ExceptionService());
            var weatherDataResult = weatherDataService.GetWeatherDataFromApi(weatherDataViewModel);

            return View("Index", weatherDataResult);
        }
    }
}