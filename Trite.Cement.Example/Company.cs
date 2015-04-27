using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trite.Cement.Example
{
    public class Company
    {
        private Company()
        {
        }

        public Company(string name)
        {
            this.Name = name;
            this.Employees = new List<Employee>();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
