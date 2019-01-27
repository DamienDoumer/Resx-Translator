using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResXTranslator.Parsers
{
    public class CLIParser
    {
        /// <summary>
        /// The path to the resx file you wish to translate.
        /// </summary>
        [Option('f', "file", Required = true, HelpText = "The path to the resx file you wish to translate.")]
        public string FilePath { get; set; }

        /// <summary>
        /// Languages the resource file will be translated to
        /// </summary>
        [Option('t', "translations", Separator = ',', Required = true, HelpText = "Languages to which you want your resource to be translated to.")]
        public IEnumerable<string> TranslationLanguages { get; set; }

        /// <summary>
        /// The language of your current resource file.
        /// </summary>
        [Option('l', "language", HelpText = "The language of your current resource file.")]
        public string ResourceLanguage { get; set; }
    }
}
