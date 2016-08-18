using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessInsights.Models
{
    public class FacebookPostAnalysed
    {
        public FacebookPostViewModel Post { get; set; }
        public FacebookPostSentimentViewModel Sentiment { get; set; }
    }
}