namespace AlchemyLanguage
{
    public class DocSentiment
    {
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