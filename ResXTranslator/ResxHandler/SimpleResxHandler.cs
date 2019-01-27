using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.IO;

namespace ResXTranslator.ResxHandler
{
    public class SimpleResxHandler : IResxHandler
    {
        public void Create(Dictionary<string, string> keyValuePairs, string fileName, string path = null)
        {
            if (path == null)
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            foreach (var kv in keyValuePairs)
            {
                using (ResourceWriter resx = new ResourceWriter(Path.Combine(path, fileName)))
                {
                    resx.AddResource(kv.Key, kv.Value);
                }
            }
        }
         
        public Dictionary<string, string> Read(string path)
        {
            var resources = new Dictionary<string, string>();
            using (ResourceReader reader = new ResourceReader(path))
            {
                do
                {
                    resources.Add((reader.GetEnumerator().Key as string), (reader.GetEnumerator().Value as string));
                }
                while (reader.GetEnumerator().MoveNext());
            }

            return resources;
        }
    }
}
