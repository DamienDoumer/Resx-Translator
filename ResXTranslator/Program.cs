using ResXTranslator.Parsers;
using ResXTranslator.ResxHandler;
using ResXTranslator.TranslationServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ResxTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            CLIParser.Parse(new List<string>(args), Run);
            Console.ReadKey();
        }
        static async void Run(CLIOptions options)
        {
            IResxHandler resxHandler = new SimpleResxHandler();
            ITranslator translator;
            if (options.APIKeyPath == null)
                translator = new GoogleTranslation();
            else
                translator = new GoogleTranslation(options.APIKeyPath);

            var resources = resxHandler.Read(options.FilePath);
            var words = resources.Select(kv => kv.Value);
            foreach (var lge in options.TranslationLanguages)
            {
                var res = await translator.TranslateAsync(new List<string>(words), lge);
                var dic = new Dictionary<string, string>();
                var outputFile = $"{Path.GetFileNameWithoutExtension(options.FilePath)}.{lge}{Path.GetExtension(options.FilePath)}";
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
