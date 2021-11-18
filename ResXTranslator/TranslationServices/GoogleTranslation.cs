using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;
using Google.Cloud.Translate.V3;
using Google.Api.Gax.ResourceNames;
using System.Diagnostics;

namespace ResXTranslator.TranslationServices
{
    public class GoogleTranslation : ITranslator
    {
        TranslationServiceClient _client;

        public GoogleTranslation(string path)
        {
            //Note: enforce the google credentials in env variables
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            if (!File.Exists(path))
            {
                Debug.WriteLine("Api key not found.");
                throw new FileNotFoundException($"File {path} was not found.");
            }

            _client = TranslationServiceClient.Create();
        }

        public GoogleTranslation()
        {
            _client = TranslationServiceClient.Create();
        }

        public async Task<string> TranslateAsync(string text, string language, string sourceLanguage = null)
        {
            string translationText = string.Empty;
            try
            {
                Debug.WriteLine($"Translating {text} to {language}");

                var request = new TranslateTextRequest
                {
                    Contents = { text },
                    TargetLanguageCode = language,
                    Parent = new ProjectName("//TODO: add project id here for this to work").ToString()
                };
                if (!string.IsNullOrEmpty(sourceLanguage))
                    request.SourceLanguageCode = sourceLanguage;

                var result = await _client.TranslateTextAsync(request);
                var translation = result.Translations[0];
                Debug.WriteLine($"Translated {text} from {translation.DetectedLanguageCode} to {language}");
                translationText = translation.TranslatedText;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"An error occured while translating. {e.Message}");
                Environment.Exit(-1);
            }

            return translationText;
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