using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;

namespace BusinessInsights.Services
{
    public interface IFacebookService
    {
        string Profile(string id);
        void SearchForPage(string name);

        IFacebookService SetToken(string token);
    }
    public class FacebookService : IFacebookService
    {
        private FacebookClient _client;

        public FacebookService()
        {
            _client = new FacebookClient();
        }

        public string Profile(string id)
        {
            var result = _client.Get("user?id=552284611502336");

            return result.ToString();
        }

        public void SearchForPage(string name)
        {
            
        }

        public IFacebookService SetToken(string token)
        {
            _client.AccessToken = token;
            return this;
        }
    }
}