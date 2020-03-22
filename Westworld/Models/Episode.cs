using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Westworld.Models
{
    class Episode : Article
    {
        public int Number { get; protected set; }
        public int Serie { get; protected set; }
        public string Code { get { return Serie + "x" + Number; } }

        public Episode(string name, int serie, int number, Uri link = null)
        {
            Name = name;
            Serie = serie;
            Number = number;
            Link = link;
        }
    }
}
