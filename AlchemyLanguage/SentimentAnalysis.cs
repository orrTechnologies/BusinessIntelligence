using RestSharp.Deserializers;

namespace AlchemyLanguage
{
    public class SentimentAnalysis
    {
        /// <summary>
        /// OK or ERROR Indicates whether the request was successful
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// HTTP URL that was passed in the input
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Detected language of the source text (text with fewer than 15 characters is assumed to be English).
        /// </summary>
        public string Language { get; set; }

        [DeserializeAs(Name = "docSentiment")]
        public DocSentiment Sentiment { get; set; }
    }
}