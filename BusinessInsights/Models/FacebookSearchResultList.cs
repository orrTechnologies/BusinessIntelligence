using System.Collections.Generic;

namespace BusinessInsights.Models
{
    public class FacebookSearchResultList
    {
        public IEnumerable<FacebookSearchPagesViewModel> Data { get; set; }
    }
}