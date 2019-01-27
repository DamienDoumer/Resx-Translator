using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ResXTranslator.Parsers
{
    public static class CLIParser
    {
        public static void Parse(List<string> args, Action<CLIOptions> func)
        {
            var options = Parser.Default.ParseArguments<CLIOptions>(args)
                .WithNotParsed(err =>
                {
                    Console.WriteLine("An error occured while parsing your command.");
                    foreach (var item in err)
                    {
                        Console.WriteLine(item.Tag.ToString());
                    }
                    Environment.Exit(-1);
                })
                .WithParsed(o =>
                {
                    try
                    {
                        if (!File.Exists(o.FilePath))
                        {
                            Console.WriteLine("The file path precised for this resource doesn't exist.");
                            Environment.Exit(-1);
                        }
                        if (string.IsNullOrEmpty(o.OutPutPath))
                            o.OutPutPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        if (!Directory.Exists(o.OutPutPath))
                        {
                            Console.WriteLine("The output directory precised doesn't exist.");
                            Environment.Exit(-1);
                        }
                        func(o);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Environment.Exit(-1);
                    }
                });
        }
    }
}
