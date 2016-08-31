using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using AlchemyLanguage;

namespace BusinessInsights.Services
{
    public class MockAlchemyClient : IAlchemyLanguageClient
    {
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// Generates randon sentiment. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns>A Sentiment Analysis with randon values. </returns>
        public SentimentAnalysis GetSentiment(string text)
        {
            var docSentiment = new DocSentiment();

            var sentimentAnalysis = new SentimentAnalysis()
            {
                Status = "OK",
            };

            docSentiment.Score = Math.Round((_rnd.NextDouble() * (1.0 - (-1.0)) + -1.0), 2);
            sentimentAnalysis.Sentiment = docSentiment;
            sentimentAnalysis.Sentiment.Type = _rnd.Next(-2, 4) > 1 ? "positive" : "negative";

            return sentimentAnalysis;
        }


    }
}