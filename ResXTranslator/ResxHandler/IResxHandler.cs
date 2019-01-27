using System.Collections.Generic;

namespace ResXTranslator.ResxHandler
{
    public interface IResxHandler
    {
        Dictionary<string, string> Read(string path);
        void Create(Dictionary<string, string> keyValuePairs, string fileName, string path = null);
    }
}
