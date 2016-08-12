using System;
using System.Collections.Generic;
using System.Web.Services.Description;
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
            string request = String.Format("search?q={0}&type=page&fields=name,id,picture", searchQuery);
            dynamic result = _client.Get(request);

            List<FacebookSearchPagesViewModel> searchPages = new List<FacebookSearchPagesViewModel>();
            foreach (dynamic page in result.data)
            {
                searchPages.Add(new FacebookSearchPagesViewModel()
                {
                    Name = page.name,
                    Id = page.id,
                    PictureUrl = page.picture.data.url
                });
            }

            return searchPages;
        }

        public IEnumerable<FacebookPostViewModel> Post(string id)
        {
            string request = String.Format("{0}/feed?fields=to,message,from{{name, picture}}", id);
            dynamic result = _client.Get(request);

            var posts = new List<FacebookPostViewModel>();
            foreach (dynamic post in result.data)
            {
                var postViewModel = new FacebookPostViewModel()
                {
                    Message = post.message,
                    NameName = post.from.name,
                    PictureUrl = post.from.picture.data.url
                };
                if (post.to.data.name != null)
                {
                    postViewModel.ToName = post.to.data.name;
                }
                posts.Add(postViewModel);
            }
            return posts;
        } 

    }
}