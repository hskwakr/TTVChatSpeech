using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChatSpeech.src
{
    class IgnoreUser : IComparable<IgnoreUser>
    {
        public string Name { get; set; }

        public IgnoreUser(string name)
        {
            Name = name;
        }

        public int CompareTo(IgnoreUser other)
        {
            if (String.Compare(this.Name, other.Name) == 0)
            {
                return 0;
            }

            return 1;
        }
    }

    class IgnoreUsersFile : JsonFileIO
    {
        private static string file = "ignoreUser.json";

        public static IList<IgnoreUser> Read()
        {
            return ReadFile<IgnoreUser>(file);
        }

        public static void Add(string name)
        {
            IList<IgnoreUser> users = new List<IgnoreUser>()
            {
                new IgnoreUser(name)
            };
            WriteFile<IgnoreUser>(file, users);
        }
    }
}
