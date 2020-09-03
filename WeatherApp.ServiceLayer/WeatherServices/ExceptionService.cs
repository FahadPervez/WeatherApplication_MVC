namespace WeatherApp.ServiceLayer.WeatherServices
{
    #region using

    using System.Net.Http;
    using WeatherApp.ServiceLayer.ApiConfiguration;
    using WeatherApp.ServiceLayer.IWeatherServices;

    #endregion

    /// <summary>
    /// The service to manage exception messages.
    /// </summary>
    public class ExceptionService : IExceptionService
    {
        #region methods
        public string UserFriendlyExceptionMessage(HttpResponseMessage httpResponseMessage)
        {
            string userFriendlyMessage = string.Empty;

            switch (httpResponseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    userFriendlyMessage = UserFriendlyMessages.ValidLocationMessage;
                    break;

                case System.Net.HttpStatusCode.Unauthorized:
                case System.Net.HttpStatusCode.Forbidden:
                    userFriendlyMessage = UserFriendlyMessages.UnAuthorizeMessage;
                    break;

                case System.Net.HttpStatusCode.InternalServerError:
                    userFriendlyMessage = UserFriendlyMessages.InternalServerMessage;
                    break;

                case System.Net.HttpStatusCode.ServiceUnavailable:
                    userFriendlyMessage = UserFriendlyMessages.ServiceUnAvaialbeMessage;
                    break;

                default:
                    userFriendlyMessage = UserFriendlyMessages.DefaultMessage;
                    break;
            }

            return userFriendlyMessage;
        }

        #endregion
    }
}