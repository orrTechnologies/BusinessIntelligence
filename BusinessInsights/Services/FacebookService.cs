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
        /// <summary>
        /// Not in use.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public FacebookProfileViewModel Profile(string Id)
        {
            var request = String.Format("/{0}?fields=about,picture", Id);

            dynamic result = _client.Get(request);
            return new FacebookProfileViewModel();
        }
        /// <summary>
        /// Searchs facebook pages
        /// </summary>
        /// <param name="searchQuery">Term to search</param>
        /// <returns>A collection of pages that match search query</returns>
        public IEnumerable<FacebookSearchPagesViewModel> Search(string searchQuery)
        {
            var request = String.Format("search?q={0}&type=page&fields=name,id,picture", searchQuery);
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
        /// <summary>
        /// Loads a list of post on page facebook page.
        /// </summary>
        /// <param name="id">Id of facebook page</param>
        /// <returns>A collection of facebook post</returns>
        public IEnumerable<FacebookPostViewModel> Post(string id)
        {
            string request = String.Format("{0}/feed?fields=to,message,from{{name, picture}}", id);
            var result = _client.Get(request);

            var posts = new List<FacebookPostViewModel>();
            //foreach (dynamic post in result.data)
            //{
            //    var postViewModel = new FacebookPostViewModel()
            //    {
            //        Message = post.message,
            //        NameName = post.from.name,
            //        PictureUrl = post.from.picture.data.url
            //    };
            //    if (post.to.data.name != null)
            //    {
            //        postViewModel.ToName = post.to.data.name;
            //    }
            //    posts.Add(postViewModel);
            //}
            return posts;
        } 

    }
}