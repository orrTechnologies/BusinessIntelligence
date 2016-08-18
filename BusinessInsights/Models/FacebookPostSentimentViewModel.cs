using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlchemyLanguage;

namespace BusinessInsights.Models
{
    public class FacebookPostSentimentViewModel
    {
        /// <summary>
        /// Sentiment polarity: Posity, negative, or neutral
        /// </summary>
        public SentimentType Type { get; set; }
        /// <summary>
        /// Indicates that the sentiment is both positive and negative
        /// </summary>
        public bool Mixed { get; set; }

        /// <summary>
        /// 0.0 == neutral
        /// </summary>
        public double Score { get; set; }
    }
}