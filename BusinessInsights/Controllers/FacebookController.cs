using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using AlchemyLanguage;
using BusinessInsights.Extensions;
using BusinessInsights.Factories;
using BusinessInsights.Filters;
using BusinessInsights.Models;
using BusinessInsights.Services;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Facebook;
using Newtonsoft.Json;

namespace BusinessInsights.Controllers
{
    [Authorize]
    [FacebookAccessToken]
    public class FacebookController : Controller
    {
        private readonly IFacebookServiceFactory _serviceFactory;
        private readonly IAlchemyLanguageClient _alchemyClient;

        private IFacebookService FacebookService
        {
            get
            {
                return _serviceFactory.CreateService(HttpContext.Items["access_token"].ToString());
            }
        }
 
        //new AlchemyLanguageClient("0ea4a12f30bf7745d366f69a95deff2c478d6257")
        public FacebookController()
            : this(new FacebookServiceFactory(), new MockAlchemyClient())
        {
            
        }

        public FacebookController(IFacebookServiceFactory serviceFactory = null, IAlchemyLanguageClient alchemyClient = null)
        {
            _serviceFactory = serviceFactory;
            _alchemyClient = alchemyClient;
        }

        [HttpGet]
        public ActionResult Search(string query)
        {
            if (String.IsNullOrWhiteSpace(query))
                return new ViewResult();

            IEnumerable<FacebookSearchPagesViewModel> results = FacebookService.Search(query);
            return View("SearchResultList", results);
        }

        private IEnumerable<FacebookPostViewModel> SampleData()
        {
            return new List<FacebookPostViewModel>();
        } 

        #region Dashboard
        public async Task<ActionResult> Dashboard(string id)
        {
            Task<IEnumerable<FacebookPostViewModel>> taggedPostTask = FacebookService.Post(id);
            Task<FacebookProfileViewModel> pageTask = FacebookService.Profile(id);

            IEnumerable<FacebookPostViewModel> taggedPost = await taggedPostTask;
            //Page: Get page view models directly from facebookService
            FacebookProfileViewModel page = await pageTask;

            //Get Post where the page is not the poster (Vistor post), and message is there
            var sortedPost =
                taggedPost.Where(p => p.FromId != id && p.Message != null)
                    .Take(20);

            List<FacebookPostAnalysed> analysedPosts = new List<FacebookPostAnalysed>();

            //Post: Use alchemy Language to analyse post. Convert to view models.
            foreach (FacebookPostViewModel post in sortedPost)
            {
                DocSentiment sentiment = _alchemyClient.GetSentiment(post.Message).Sentiment;
                var analysedPost = new FacebookPostAnalysed()
                {
                    Post = post,
                    Sentiment = new FacebookPostSentimentViewModel()
                    {
                        Mixed = sentiment.Mixed,
                        Score = sentiment.Score,
                        Type = sentiment.Sentiment
                    }
                };
                analysedPost.Post = post;

                analysedPosts.Add(analysedPost);
            }
#region Charts
            var js = new JavaScriptSerializer();
            //ChartData

            //Donut Chart Data:
            var positivePostCount = analysedPosts.Count(p => p.Sentiment.Type == SentimentType.Positive);
            var negativePostCount = analysedPosts.Count(p => p.Sentiment.Type == SentimentType.Negative);
            var neutralPostCount = analysedPosts.Count(p => p.Sentiment.Type == SentimentType.Neutral);

            FacebookDonutChartViewModel donutChartData = new FacebookDonutChartViewModel()
            {
                PositivePostCount = positivePostCount,
                NegativePostCount = negativePostCount,
                NeutralPostCount = neutralPostCount
            }; 

            //Area Chart Data
            IEnumerable<IGrouping<DateTime, FacebookPostAnalysed>> analysedPostByDay =
                analysedPosts.GroupBy(analysed => analysed.Post.CreatedTime.Date);

            var areaChartData = analysedPostByDay.Select(post => new FacebookAreaChartViewModel()
            {
                Day = post.Key, 
                NegativePostCount = post.Count(p => p.Sentiment.Type == SentimentType.Negative), 
                NeutralPostCount = post.Count(p => p.Sentiment.Type == SentimentType.Neutral), 
                PositivePostCount = post.Count(p => p.Sentiment.Type == SentimentType.Positive)
            }).ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("asdf{0}", 1);
            #endregion
            var dashboardViewModal = new FacebookDashboardViewModel()
            {
                Page = page,
                Posts = analysedPosts,
                AreaChartDataViewModel = areaChartData,
                DonoutChartDataViewModel = donutChartData
            };
            return View("Dashboard", dashboardViewModal);
        }

        #endregion
    }
}