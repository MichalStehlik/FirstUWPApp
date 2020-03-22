using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Westworld.Models
{
    class Character : Person
    {
        public Artist Actor { get; protected set; }

        public Character(string firstname, string lastname, Gender gender, Artist actor, Uri link = null) : base(firstname, lastname, gender, link)
        {
            Actor = actor;
        }
    }
}
