using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Westworld.Models
{
    class Artist : Person
    {
        public Profession Profession { get; protected set; }

        public Artist(string firstname, string lastname, Gender gender, Profession profession, Uri link = null) : base(firstname, lastname, gender, link)
        {
            Profession = profession;
        }
    }
}
