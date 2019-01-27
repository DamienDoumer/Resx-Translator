using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResXTranslator.TranslationServices
{
    public class GoogleTranslation : ITranslator
    {
        public Task<string> Translate(string text, string language)
        {
            throw new NotImplementedException();
        }

        public Task<string> Translate(string text)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> Translate(IEnumerable<string> texts, string language)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> Translate(IEnumerable<string> texts)
        {
            throw new NotImplementedException();
        }
    }
}
