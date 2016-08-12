using System.Collections.Generic;
using BusinessInsights.Models;

namespace BusinessInsights.Services
{
    public interface IFacebookService
    {
        IFacebookService SetToken(string token);
        FacebookProfileViewModel Profile(string Id);

        IEnumerable<FacebookSearchPagesViewModel> Search(string query);
    }
}