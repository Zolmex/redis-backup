using Newtonsoft.Json;
using System.IO;

namespace RedisBackup
{
    public class Settings
    {
        public static Settings Load(string fileName)
            => JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));

        public int TimeLimit { get; set; }
        public string DestFolder { get; set; }
        public string RedisFolder { get; set; }
    }
}
