using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResXTranslator.Parsers
{
    public class CLIOptions
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

        /// <summary>
        /// Path where your Resx will be created. The default path is in your documents folder.
        /// </summary>
        [Option('p', "output", HelpText = "Path where your Resx will be created. The default path is in your documents folder.")]
        public string OutPutPath { get; set; }

        [Option('k', "key", HelpText = "Google translation API Key.")]
        public string APIKeyPath { get; set; }

        [Option('e', "existing", HelpText = "If flag is present, then it will look for an existing translation and only update new occurances.", Max = 0)]
        public bool UseExistingTranslation { get; set; }
    }
}