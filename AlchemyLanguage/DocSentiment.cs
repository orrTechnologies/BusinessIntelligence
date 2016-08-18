namespace AlchemyLanguage
{
    public class DocSentiment
    {
        /// <summary>
        /// Returns enum version of the type property
        /// </summary>
        public SentimentType Sentiment
        {
            get {
                switch (Type)
                {
                    case "positive":
                        return SentimentType.Positive;
                    case "negative":
                        return SentimentType.Negative;
                    case "neutral":
                        return SentimentType.Neutral;
                    default:
                        return SentimentType.Neutral;
                } 
            }
        }

        /// <summary>
        /// Sentiment polarity: Posity, negative, or neutral
        /// </summary>
        public string Type { get; set; }
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