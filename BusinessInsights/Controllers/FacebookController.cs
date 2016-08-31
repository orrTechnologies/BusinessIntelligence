using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AlchemyLanguage;
using BusinessInsights.Factories;
using BusinessInsights.Filters;
using BusinessInsights.Models;
using BusinessInsights.Services;
using Newtonsoft.Json;

namespace BusinessInsights.Controllers
{
    [Authorize]
    [FacebookAccessToken]
    public class FacebookController : Controller
    {
        private readonly IFacebookServiceFactory _serviceFactory;
        private readonly IAlchemyLanguageClient _alchemyClient;

        //TODO: Using poor mans IOC, replace with proper IOC Container.
        /// <summary>
        /// Uses default FacebookServiceFactory, and AlchemyLanguageClient implementations 
        /// new AlchemyLanguageClient("0ea4a12f30bf7745d366f69a95deff2c478d6257")
        /// </summary>
        public FacebookController()
            : this(new FacebookServiceFactory(), new AlchemyLanguageClient(ConfigurationManager.AppSettings["AlchemyApiKey"]))
        {
            
        }
        /// <summary>
        /// Allows for injection of dependencies 
        /// </summary>
        /// <param name="serviceFactory">IFacebookServiceFactory implementation</param>
        /// <param name="alchemyClient">IAlchemyLanguageClient implementation</param>
        public FacebookController(IFacebookServiceFactory serviceFactory = null, IAlchemyLanguageClient alchemyClient = null)
        {
            _serviceFactory = serviceFactory;
            _alchemyClient = alchemyClient;
        }

        [HttpGet]
        public async Task<ActionResult> Search(string query)
        {
            if (String.IsNullOrWhiteSpace(query))
                return new ViewResult();

            IEnumerable<FacebookSearchPagesViewModel> results = await _serviceFactory.CreateService().Search(query);
            return View("SearchResultList", results);
        }

        #region Dashboard
        public async Task<ActionResult> Dashboard(string id)
        {
            Task<IEnumerable<FacebookPostViewModel>> taggedPostTask = _serviceFactory.CreateService().Post(id);
            Task<FacebookProfileViewModel> pageTask = _serviceFactory.CreateService().Profile(id);

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

            var areaChartData = GetSerializedAreaChartData(analysedPostByDay);
#endregion
            var dashboardViewModal = new FacebookDashboardViewModel()
            {
                Page = page,
                Posts = analysedPosts,
                AreaChartSerializedData = areaChartData,
                DonoutChartDataViewModel = donutChartData
            };
            return View("Dashboard", dashboardViewModal);
        }

        [ChildActionOnly]
        private string GetSerializedAreaChartData(IEnumerable<IGrouping<DateTime, FacebookPostAnalysed>> data )
        {
            List<FacebookAreaChartViewModel> chartDataObject = new List<FacebookAreaChartViewModel>();
            foreach(var postGroup in data)
            {
                var chartGroup = new FacebookAreaChartViewModel()
                {
                    Day = postGroup.Key,
                    PositivePostCount = postGroup.Count(p => p.Sentiment.Type == SentimentType.Positive),
                    NegativePostCount = postGroup.Count(p => p.Sentiment.Type == SentimentType.Negative),
                    NeutralPostCount = postGroup.Count(p => p.Sentiment.Type == SentimentType.Neutral),
                };
                chartDataObject.Add(chartGroup);
            }
            return JsonConvert.SerializeObject(chartDataObject.ToArray());
        }
        #endregion
    }
}