using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResXTranslator.TranslationServices
{
    public interface ITranslator
    {
        Task<string> Translate(string text, string language);
        Task<string> Translate(string text);
        Task<IEnumerable<string>> Translate(IEnumerable<string> texts, string language);
        Task<IEnumerable<string>> Translate(IEnumerable<string> texts);
    }
}
