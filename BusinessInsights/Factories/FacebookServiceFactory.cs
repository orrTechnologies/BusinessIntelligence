using BusinessInsights.Controllers;
using BusinessInsights.Services;

namespace BusinessInsights.Factories
{
    public class FacebookServiceFactory : IFacebookServiceFactory
    {
        public IFacebookService CreateService(string token)
        {
            return new FacebookService().SetToken(token);
        }
    }
}