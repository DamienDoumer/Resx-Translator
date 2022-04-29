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
                        if (!File.Exists(Path.Combine(Environment.CurrentDirectory, o.FilePath)))
                        {
                            Console.WriteLine("The file path precised for this resource doesn't exist.");
                            Environment.Exit(-1);
                        }

                        if (string.IsNullOrEmpty(o.OutPutPath) || !Directory.Exists(Path.Combine(Environment.CurrentDirectory, o.OutPutPath)))
                        {
                            o.OutPutPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            Console.WriteLine("The output directory precised doesn't exist. Translations will be created in your documents directory.");
                        }
                        else
                        {
                            o.OutPutPath = Path.Combine(Environment.CurrentDirectory, o.OutPutPath);
                        }

                        if (!string.IsNullOrEmpty(o.APIKeyPath) || Directory.Exists(Path.Combine(Environment.CurrentDirectory, o.APIKeyPath)))
                        {
                            o.APIKeyPath = Path.Combine(Environment.CurrentDirectory, o.APIKeyPath);
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
