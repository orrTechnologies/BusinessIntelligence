using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessInsights.Models;

namespace BusinessInsights.Services
{
    public interface IFacebookService
    {
        IFacebookService SetToken(string token);
        Task<FacebookProfileViewModel> Profile(string Id);
        IEnumerable<FacebookSearchPagesViewModel> Search(string query);
        Task<IEnumerable<FacebookPostViewModel>> Post(string id);
    }
}