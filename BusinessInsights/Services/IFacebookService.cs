using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessInsights.Models;

namespace BusinessInsights.Services
{
    public interface IFacebookService
    {
        Task<FacebookProfileViewModel> Profile(string Id);
        Task<IEnumerable<FacebookSearchPagesViewModel>> Search(string query);
        Task<IEnumerable<FacebookPostViewModel>> Post(string id);
    }
}