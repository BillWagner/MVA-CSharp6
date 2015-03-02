using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilGeniusUtilities
{
    public class EvilGenius
    {
        public string Name { get; set; }

        public Henchman Minion{ get; set; }

        public string CatchPhrase { get; set; }

        public override string ToString() => Name + ", " + Minion?.Name;
    }
}
