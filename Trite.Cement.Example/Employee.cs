using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trite.Cement.Example
{
    public class Employee
    {
        private Employee()
        {
        }

        public Employee(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
