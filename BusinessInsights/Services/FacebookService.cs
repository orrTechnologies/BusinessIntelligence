using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessInsights.Extensions;
using BusinessInsights.Models;
using Facebook;

namespace BusinessInsights.Services
{
    public class FacebookService : IFacebookService
    {
        private readonly FacebookClient _client = new FacebookClient();

        //TODO: This class is curently directly returning view models. We should be returning domain specific facebook models, and converting them to view models in the controller. 
        public FacebookService(string token)
        {
            _client.AccessToken = token;
        }
        /// <summary>
        /// Not in use.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<FacebookProfileViewModel> Profile(string Id)
        {
            var request = String.Format("/{0}?fields=about,picture.height(160)", Id);

            dynamic result = await _client.GetTaskAsync(request);
            FacebookProfileViewModel page = DynamicExtension.ToStatic<FacebookProfileViewModel>(result);

            return page;
        }
        /// <summary>
        /// Searchs facebook pages
        /// </summary>
        /// <param name="searchQuery">Term to search</param>
        /// <returns>A collection of pages that match search query</returns>
        public async Task<IEnumerable<FacebookSearchPagesViewModel>> Search(string searchQuery)
        {
            var request = String.Format("search?q={0}&type=page&fields=name,id,picture", searchQuery);
            dynamic result = await _client.GetTaskAsync(request);

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
        public async Task<IEnumerable<FacebookPostViewModel>> Post(string id)
        {
            string request = String.Format("{0}/feed?fields=id,from {{id, name, picture{{url}} }},story,picture,link,name,description,message,created_time&limit=100", id);
            dynamic result = await _client.GetTaskAsync(request);

            var posts = new List<FacebookPostViewModel>();

            foreach (dynamic resultFeed in result.data)
            {
                FacebookPostViewModel post = DynamicExtension.ToStatic<FacebookPostViewModel>(resultFeed);
                posts.Add(post);
            }

            return posts;
        }

    }
}