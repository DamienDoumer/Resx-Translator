using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResXTranslator.TranslationServices
{
    public interface ITranslator
    {
        Task<string> TranslateAsync(string text, string language, string currentLge = null);
        Task<IEnumerable<string>> TranslateAsync(IEnumerable<string> texts, string language, string currentLge = null);
    }
}
