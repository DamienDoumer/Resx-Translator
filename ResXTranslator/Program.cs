using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using System;

namespace ResXTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            var credential = GoogleCredential.FromFile("APIKey.json");
            TranslationClient client = TranslationClient.Create(credential);
            TranslationResult result = client.TranslateText("It is raining.", LanguageCodes.French);
            Console.WriteLine($"Result: {result.TranslatedText}; detected language {result.DetectedSourceLanguage}");
            Console.ReadKey();

        }
    }
}