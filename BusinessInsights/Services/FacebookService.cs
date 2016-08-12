using System;
using System.Collections.Generic;
using BusinessInsights.Models;
using Facebook;

namespace BusinessInsights.Services
{
    public class FacebookService : IFacebookService
    {
        private readonly FacebookClient _client = new FacebookClient();
        public FacebookService() { }
        public IFacebookService SetToken(string token)
        {
            _client.AccessToken = token;
            return this;
        }

        public FacebookProfileViewModel Profile(string Id)
        {
            string request = String.Format("/{0}?fields=about,picture", Id);

            var result = _client.Get(request);
            return new FacebookProfileViewModel();
        }

        public IEnumerable<FacebookSearchPagesViewModel> Search(string searchQuery)
        {
            string request = String.Format("search?q={0}&type=page&fields=name,picture", searchQuery);
            dynamic result = _client.Get(request);

            List<FacebookSearchPagesViewModel> searchPages = new List<FacebookSearchPagesViewModel>();
            foreach (dynamic page in result.data)
            {
                searchPages.Add(new FacebookSearchPagesViewModel()
                {
                    Name = page.name,
                    PictureUrl = page.picture.data.url
                });
            }

            return searchPages;
        }

    }
}