using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace AlchemyLanguage
{
    public class AlchemyLanguageService : IAlchemyLanguageService
    {
        private readonly IRestClient _client;

        /// <summary>
        /// Allows for dependency injection if needed. 
        /// </summary>
        /// <param name="client"></param>
        public AlchemyLanguageService(IRestClient client)
        {
            _client = client;
        }

       /// <summary>
       /// This is the constructor that should be used.
       /// </summary>
        /// <param name="apiKey">bluemix api key</param>
        public AlchemyLanguageService(string apiKey)
        {
            if(String.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException("Api key must not be null");

            _client = new RestClient("https://gateway-a.watsonplatform.net/calls/text/");
            _client.AddDefaultParameter(new Parameter() { Name = "apikey", Value = apiKey });
        }

        /// <summary>
        /// Returns a SentimentAnalysis for the text. 
        /// </summary>
        /// <param name="text">Text used for analysis</param>
        /// <returns>SentimentAnalysis</returns>
        public SentimentAnalysis GetSentiment(string text)
        {
            var request = new RestRequest("TextGetTextSentiment");
            request.AddParameter("text", text);

            var response = _client.Execute<SentimentAnalysis>(request);
            var content = response.Data;

            return content;
        }
    }
}
