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
    }
}
