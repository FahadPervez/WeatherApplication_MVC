namespace WeatherApp.ServiceLayer.IWeatherServices
{
    #region using

    using System.Net.Http;

    #endregion
    /// <summary>
    /// The service to manage exception.
    /// </summary>
    public interface IExceptionService
    {
        /// <summary>
        /// user friendly exception message
        /// </summary>        
        string UserFriendlyExceptionMessage(HttpResponseMessage httpResponseMessage);
    }
}
