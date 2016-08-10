namespace AlchemyLanguage
{
    public interface IAlchemyLanguageService
    {
        /// <summary>
        /// Returns a SentimentAnalysis for the text. 
        /// </summary>
        /// <param name="text">Text used for analysis</param>
        /// <returns>SentimentAnalysis</returns>
        SentimentAnalysis GetSentiment(string text);
    }
}