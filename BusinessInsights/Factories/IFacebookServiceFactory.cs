using BusinessInsights.Services;

namespace BusinessInsights.Factories
{
    public interface IFacebookServiceFactory
    {
        IFacebookService CreateService(string token);
    }
}