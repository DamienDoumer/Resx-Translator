using NUnit.Framework;
using ResxTranslator;
using ResXTranslator.ResxHandler;
using ResXTranslator.TranslationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class TranslationTests
    {
        IResxHandler _resxHandler = new SimpleResxHandler();
        ITranslator _translator = new GoogleTranslation(@"\ApiKey.json");

        [Test]
        public async Task SingleTranslationTest()
        {
            var translation = await _translator.TranslateAsync("salut", "en");
            Assert.Equals(translation, "Hi");
            translation = await _translator.TranslateAsync("salut", "ru");
            Assert.Equals(translation, "привет");
            translation = await _translator.TranslateAsync("I'm trying to test this application, Does it work ?", "pt", "en");
            Assert.Equals(translation, "Estou tentando testar esse aplicativo, funciona?");
        }

        [Test]
        public void ResxTranslationTest()
        {
            Dictionary<string, string> currentResource = new Dictionary<string, string>();
            Dictionary<string, string> translatedResource = new Dictionary<string, string>();
            var spanishRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resources.es.resx");
            var frenchRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resources.fr.resx");
            var portugeseRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resources.pt.resx");
            var russianRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resources.ru.resx");
            Program.Main(new string[]
            {
                "-t", "fr,pt,ru,es",
                "-f", "Resource.resx",
                "-k", @"\ApiKey.json"
            });

            Assert.True(File.Exists(frenchRes));
            Assert.True(File.Exists(spanishRes));
            Assert.True(File.Exists(portugeseRes));
            Assert.True(File.Exists(russianRes));

            translatedResource = _resxHandler.Read(spanishRes);
            currentResource = _resxHandler.Read("Resources.es.resx");
            foreach (var kv in translatedResource)
            {
                Assert.Equals(kv.Value, currentResource[kv.Key]);
            }

            translatedResource = _resxHandler.Read(frenchRes);
            currentResource = _resxHandler.Read("Resources.fr.resx");
            foreach (var kv in translatedResource)
            {
                Assert.Equals(kv.Value, currentResource[kv.Key]);
            }

            translatedResource = _resxHandler.Read(portugeseRes);
            currentResource = _resxHandler.Read("Resources.pt.resx");
            foreach (var kv in translatedResource)
            {
                Assert.Equals(kv.Value, currentResource[kv.Key]);
            }

            translatedResource = _resxHandler.Read(russianRes);
            currentResource = _resxHandler.Read("Resources.ru.resx");
            foreach (var kv in translatedResource)
            {
                Assert.Equals(kv.Value, currentResource[kv.Key]);
            }
        }
    }
}
