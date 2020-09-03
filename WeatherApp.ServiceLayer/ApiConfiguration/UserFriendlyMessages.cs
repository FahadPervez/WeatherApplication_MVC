using System;

namespace WeatherApp.ServiceLayer.ApiConfiguration
{
    /// <summary>
    /// This class represents exception messages to be displayed for api.
    /// </summary>
    public static class UserFriendlyMessages
    {
        public const string  ValidLocationMessage = "Not found, please enter the valid location";

        public const string UnAuthorizeMessage = "You do not have permission or authentication key is invalid, please check configuration (ie api url, key)";
        
        public const string InternalServerMessage = "There is an issue with the service, please try later";

        public const string ServiceUnAvaialbeMessage = "The weather service is not available";

        public const string DefaultMessage = "There is an issue while getting weatther data, Please check configuration (ie api url, key) or try later";
    }
}
