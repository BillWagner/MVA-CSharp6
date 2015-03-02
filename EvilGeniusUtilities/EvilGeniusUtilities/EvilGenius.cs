using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvilGeniusUtilities
{
    public class EvilGenius
    {
        public string Name { get; }

        public EvilGenius(string name)
        {
            Name = name;
        }

        public Henchman Minion => minion;
        private Henchman minion;

        public string CatchPhrase { get; set; }

        public override string ToString() => Name + ", " + Minion?.Name;

        public void ReplaceHenchman(Henchman newHenchman)
        {
            var oldMinion = Interlocked.Exchange(ref minion, newHenchman);
            (oldMinion as IDisposable)?.Dispose();
        }

        public static JArray ToJson(IEnumerable<EvilGenius> evilness)
        {
            var result = new JArray();
            if (evilness != null)
                foreach(var evil in evilness)
                {
                    var json = new JObject
                    {
                        ["Name"] = evil.Name,
                        ["CatchPhrase"] = evil.CatchPhrase ?? string.Empty,
                    };
                    if (evil.Minion != null)
                    {
                        json["Minion"] = new JObject
                        {
                            ["Name"] = evil?.Minion?.Name ?? string.Empty
                        };
                    }
                    result.Add(json);
                }
            return result;
        }

        public static IEnumerable<EvilGenius> FromJson(JArray array)
        {
            foreach (var json in array)
            {
                if (json != null)
                {
                    var rVal = new EvilGenius((string)json["Name"])
                    {
                        CatchPhrase = (string)json["CatchPhrase"]
                    };
                    var minion = (JToken)json["Minion"];
                    if (minion != null)
                    {
                        rVal.ReplaceHenchman(new Henchman
                        {
                            Name = (string)minion["Name"]
                        });
                    }
                    yield return rVal;
                }
            }
        }
    }
}
