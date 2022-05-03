using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Threading;

namespace ResXTranslator.TranslationServices
{
    public class GoogleTranslation : ITranslator
    {
        TranslationClient _client;

        public GoogleTranslation(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Api key not found.");
                throw new FileNotFoundException($"File {path} was not found.");
            }
            var credential = GoogleCredential.FromFile(path);
            _client = TranslationClient.Create(credential);
        }

        public GoogleTranslation()
        {
            _client = TranslationClient.Create();
        }

        static object ListLanguages(string targetLanguageCode)
        {
            TranslationClient client = TranslationClient.Create();
            foreach (var language in client.ListLanguages(targetLanguageCode))
            {
                Console.WriteLine("{0}\t{1}", language.Code, language.Name);
            }
            return 0;
        }
        public async Task<string> TranslateAsync(string text, string language, string sourceLanguage = null)
        {
            string translation = string.Empty;
            try
            {
                Console.WriteLine($"Translating {text} to {language}");
                Type type = typeof(LanguageCodes);
                var langCode = type.GetFields(BindingFlags.Static | BindingFlags.Public).Select(l => l.GetValue(null).ToString())
                    .Where(lge => language == lge);
                if (!langCode.Any())
                {
                    Console.WriteLine("The language code you entered could not be handled.");
                    Environment.Exit(-1);
                }
                var result = await _client.TranslateTextAsync(text, langCode.First(), sourceLanguage);
                Console.WriteLine($"Translated {text} from {result.DetectedSourceLanguage} to {langCode.First()}");
                translation = result.TranslatedText;
                await Task.Delay(1000).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured while translating. {e.Message}");
                Environment.Exit(-1);
            }

            return translation;
        }

        public async Task<IEnumerable<string>> TranslateAsync(IEnumerable<string> texts, string language, string sourceLanguage = null)
        {
            List<string> translations = new List<string>();
            foreach (var t in  texts)
            {
                var res = await TranslateAsync(t, language, sourceLanguage);
                translations.Add(res);
            }

            return translations;
        }
    }
}