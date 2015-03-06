using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvilGeniusUtilities
{
    public class EvilGenius
    {
        private List<string> previousHenchmen = new List<string>();

        public string Name { get; }

        public EvilGenius(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "The Evil Genius must have a name");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name), "The Evil Genius must have a non-blank name");
            Name = name;
        }

        public string EvilPoints()
        {
            double rebelBase = 19;
            double rebelAlliance = 23;
            return ToGerman($"{rebelAlliance} / {rebelBase} is {rebelAlliance / rebelBase}");
        }

        private string ToGerman(FormattableString src)
        {
            return string.Format(
                CultureInfo.CreateSpecificCulture("de-de"),
                src.Format,
                src.GetArguments());
        }

        public Henchman Assistant => minion;
        private Henchman minion;

        public string CatchPhrase { get; set; }

        public override string ToString() => $"{Name}, {minion?.Name}";

        public void ReplaceHenchman(Henchman newHenchman)
        {
            var oldMinion = Interlocked.Exchange(ref minion, newHenchman);
            if (oldMinion != null)
                previousHenchmen.Add(oldMinion.Name);
            (oldMinion as IDisposable)?.Dispose();
        }
        public string EvilHistory()
        {
            if (!previousHenchmen.Any())
                return $"{Name} has had no previous henchman.";
            else
                return $"{Name} has had {previousHenchmen.Count} previous henchman: {previousHenchmen.Aggregate((memo, current) => $"{memo}, {current}")}";
        }

        public static JArray ToJson(IEnumerable<EvilGenius> evilness)
        {
            var result = new JArray();
            if (evilness != null)
                foreach(var evil in evilness)
                {
                    var json = new JObject
                    {
                        [nameof(EvilGenius.Name)] = evil.Name,
                        [nameof(EvilGenius.CatchPhrase)] = evil.CatchPhrase ?? string.Empty,
                    };
                    if (evil.Assistant != null)
                    {
                        json[nameof(EvilGenius.Assistant)] = new JObject
                        {
                            [nameof(Henchman.Name)] = evil?.Assistant?.Name ?? string.Empty
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
                    var rVal = new EvilGenius((string)json[nameof(EvilGenius.Name)])
                    {
                        CatchPhrase = (string)json[nameof(EvilGenius.CatchPhrase)]
                    };
                    var minion = (JToken)json[nameof(EvilGenius.Assistant)];
                    if (minion != null)
                    {
                        rVal.ReplaceHenchman(new Henchman
                        {
                            Name = (string)minion[nameof(Henchman.Name)]
                        });
                    }
                    yield return rVal;
                }
            }
        }
    }
}
