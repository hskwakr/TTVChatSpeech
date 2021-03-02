using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace TTVChatSpeech
{
    abstract class JsonFileIO
    {
        protected static void WriteFile<T>(string fileName, IList<T> additional)
            where T : IComparable<T>
        {
            IList<T> original;

            if (!File.Exists(fileName))
            {
                original = new List<T>();
            }
            else
            {
                original = ReadFile<T>(fileName);
            }

            IList<T> unique = new List<T>();

            // If the File have nothing
            if (original.Count == 0)
            {
                foreach (var y in additional)
                {
                    unique.Add(y);
                }
            }

            // when a origanl item and a additional item are same
            // a additional item takes priority over a original item
            IList<T> duplicated = new List<T>();
            foreach (var x in original)
            {
                foreach (var y in additional)
                {
                    if (x.CompareTo(y) == 0)
                    {
                        duplicated.Add(y);
                    }
                }
            }

            bool canAdd = false;
            foreach (var x in original)
            {
                canAdd = true;
                foreach (var z in duplicated)
                {
                    if (x.CompareTo(z) == 0)
                    {
                        canAdd = false;
                        break;
                    }
                }

                if (canAdd)
                {
                    unique.Add(x);
                }
            }

            foreach (var y in additional)
            {
                canAdd = true;
                foreach (var z in duplicated)
                {
                    if (y.CompareTo(z) == 0)
                    {
                        canAdd = false;
                        break;
                    }
                }

                if (canAdd)
                {
                    unique.Add(y);
                }
            }

            foreach (var z in duplicated)
            {
                unique.Add(z);
            }

            try
            {
                var jsonSettings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                };

                File.WriteAllText(fileName, JsonConvert.SerializeObject(unique.ToArray(), jsonSettings));
            }
            catch(Exception)
            {
                throw;
            }
        }

        protected static IList<T> ReadFile<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new List<T>();
            }

            try
            {
                string json = File.ReadAllText(fileName);
                var list = JsonConvert.DeserializeObject<IList<T>>(json);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
