using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Westworld.Models
{
    abstract class Person : Article
    {
        public string Firstname { get; protected set; }
        public string Lastname { get; protected set; }
        public Gender Gender { get; protected set; }

        public Person(string firstname, string lastname, Gender gender, Uri link = null)
        {
            Firstname = firstname;
            Lastname = lastname;
            Gender = gender;
            if (Firstname != "") Name = Firstname + " " + Lastname; else Name = Lastname;
            Link = link;
        }
    }
}
