using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.IO;
using System.Collections;

namespace ResXTranslator.ResxHandler
{
    public class SimpleResxHandler : IResxHandler
    {
        public void Create(Dictionary<string, string> keyValuePairs, string fileName, string path = null)
        {
            if (path == null)
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (ResXResourceWriter resx = new ResXResourceWriter(Path.Combine(path, fileName)))
            {
                foreach (var kv in keyValuePairs)
                {
                    resx.AddResource(kv.Key, kv.Value);
                }
            }
        }

        public Dictionary<string, string> Read(string path)
        {
            var resources = new Dictionary<string, string>();
            using (ResXResourceReader reader = new ResXResourceReader(path))
            {
                foreach (DictionaryEntry item in reader)
                {
                    resources.Add(item.Key.ToString(), item.Value.ToString());
                }
            }

            return resources;
        }
    }
}
