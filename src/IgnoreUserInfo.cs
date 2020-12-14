using System;
using System.Collections.Generic;

namespace TTVChatSpeech.src
{
    class IgnoreUserInfo : IComparable<IgnoreUserInfo>
    {
        public string Name { get; set; }

        public IgnoreUserInfo(string name)
        {
            Name = name;
        }

        public int CompareTo(IgnoreUserInfo other)
        {
            if (String.Compare(this.Name, other.Name) == 0)
            {
                return 0;
            }

            return 1;
        }
    }

    class IgnoreUserFile : JsonFileIO
    {
        private static string filePath = "ignoreUser.json";

        public IList<IgnoreUserInfo> Read()
        {
            return ReadFile<IgnoreUserInfo>(filePath);
        }

        public void Add(string name)
        {
            IList<IgnoreUserInfo> users = new List<IgnoreUserInfo>()
            {
                new IgnoreUserInfo(name)
            };
            WriteFile<IgnoreUserInfo>(filePath, users);
        }
    }

    class IgnoreUser
    {
        private IgnoreUserFile users = new IgnoreUserFile();

        public void Ignore()
        { }

        public void Add()
        { }
    }
}
