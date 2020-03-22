using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Westworld.Models
{
    abstract class Article
    {
        public string Name { get; protected set; }
        public Uri Link { get; protected set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
