using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResxTranslator;
using ResXTranslator.ResxHandler;
using ResXTranslator.TranslationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AppTests
{
    [TestClass]
    public class Tests
    {
        IResxHandler _resxHandler;
        ITranslator _translator;
        string spanishRes;
        string frenchRes;
        string portugeseRes;
        string russianRes;
        string keyPath;

        [TestInitialize]
        public void Initialize()
        {
            spanishRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resources.es.resx");
            frenchRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resources.fr.resx");
            portugeseRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resources.pt.resx");
            russianRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Resources.ru.resx");
            keyPath = Path.Combine(Environment.CurrentDirectory, "ApiKey.json");
            
            _resxHandler = new SimpleResxHandler();
            _translator = new GoogleTranslation(keyPath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(spanishRes);
            File.Delete(frenchRes);
            File.Delete(portugeseRes);
            File.Delete(russianRes);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsFalse(false);
        }

        [TestMethod]
        public async Task SingleTranslationTest()
        {
            var translation1 = await _translator.TranslateAsync("Salut, je suis un test.", "en");
            Assert.AreEqual(translation1, "Hi, I'm a test.");

            var translation2 = await _translator.TranslateAsync("Hi, I'm testing this software", "ru", "en");
            Assert.AreEqual(translation2, "Привет, я тестирую это программное обеспечение");
            var translation3 = await _translator.TranslateAsync("Soy el fuego que arde tu piel Soy el agua que mata tu sed El castillo, la torre yo soy La espada que guarda el caudal", "fr", "es");
            Assert.AreEqual(translation3, "Je suis le feu qui brûle ta peau Je suis l'eau qui tue ta soif Le château, la tour Je suis L'épée qui maintient le flux");
        }

        [TestMethod]
        public void ResxTranslationTest()
        {
            Dictionary<string, string> currentResource = new Dictionary<string, string>();
            Dictionary<string, string> translatedResource = new Dictionary<string, string>();
            Program.Main(new string[]
            {
                "-t", "fr,pt,ru,es",
                "-f", "Resource.resx",
                "-k", keyPath
            });

            Assert.IsTrue(File.Exists(frenchRes));
            Assert.IsTrue(File.Exists(spanishRes));
            Assert.IsTrue(File.Exists(portugeseRes));
            Assert.IsTrue(File.Exists(russianRes));

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