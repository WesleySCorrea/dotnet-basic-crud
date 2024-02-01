using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrud.People
{

    public class Person
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public bool Active { get; private set; }

        public Person(string name)
        {
            this.Name = name;
            Id = Guid.NewGuid();
            Active = true;
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }

        public void Desactive()
        {
            this.Active = false;
        }
    }
}