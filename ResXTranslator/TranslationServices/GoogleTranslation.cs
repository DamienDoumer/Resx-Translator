using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace ResXTranslator.TranslationServices
{
    public class GoogleTranslation : ITranslator
    {
        TranslationClient _client;
        public GoogleTranslation()
        {
            var credential = GoogleCredential.FromFile("APIKey.json");
            TranslationClient _client = TranslationClient.Create(credential);
        }

        public async Task<string> Translate(string text, string language)
        {
            string translation = string.Empty;
            try
            {
                Console.WriteLine($"Translating {text} to {language}");
                Type type = typeof(LanguageCodes);
                var langCode = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic).Select(l => l.GetValue(null).ToString())
                    .Where(lge => language == lge);
                if (!langCode.Any())
                {
                    Console.WriteLine("The language code you entered could not be handled.");
                    Environment.Exit(-1);
                }
                var result = _client.TranslateText(text, langCode.First());
                Console.WriteLine($"Translated {text} from {result.DetectedSourceLanguage} to {langCode.First()} :: {result.TranslatedText}");
                translation = result.TranslatedText;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while translating.");
                Environment.Exit(-1);
            }

            return translation;
        }
        public async Task<IEnumerable<string>> Translate(IEnumerable<string> texts, string language)
        {
            List<string> translations = new List<string>();
            foreach (var t in  texts)
            {
                var res = await Translate(t, language);
                translations.Add(t);
            }
            return translations;
        }
    }
}
