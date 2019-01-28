using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using ResXTranslator.Parsers;
using ResXTranslator.ResxHandler;
using ResXTranslator.TranslationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResXTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            CLIParser.Parse(new List<string>(args), Run);
        }

        static async void Run(CLIOptions options)
        {
            IResxHandler resxHandler = new SimpleResxHandler();
            ITranslator translator = new GoogleTranslation();

            var resources = resxHandler.Read(options.FilePath);
            var words = resources.Select(kv => kv.Value);
            foreach (var lge in options.TranslationLanguages)
            {
                var res = await translator.Translate(new List<string>(words), lge);
                var dic = new Dictionary<string, string>();
                var outputFile = $"{Path.GetFileName(options.FilePath)}-{lge}.{Path.GetExtension(options.FilePath)}";
                for (int i = 0; i < res.Count(); i++)
                {
                    dic.Add(resources.Keys.ElementAt(i), res.ElementAt(i));
                }
                resxHandler.Create(dic, outputFile, options.OutPutPath);
                Console.WriteLine($"Translations finished and file created: ${outputFile}");
            }
        }
    }
}