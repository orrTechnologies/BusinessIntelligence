using System.Web;
using BusinessInsights.Services;

namespace BusinessInsights.Factories
{
    public class FacebookServiceFactory : IFacebookServiceFactory
    {
        /// <summary>
        /// Creates a facebook service. 
        /// Uses token stored in HttpContext.
        /// </summary>
        /// <returns>IFacebookService</returns>
        public IFacebookService CreateService()
        {
            return new FacebookService(HttpContext.Current.Items["access_token"].ToString());
        }
    }
}