using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trite.Cement.Example
{
    class Program
    {
        static string GetCompanyXml()
        {
            Company microsoft = new Company("Microsoft");
            microsoft.Address = "On the moon";

            microsoft.Employees.Add(new Employee("Peter", "Schroeder"));
            microsoft.Employees.Add(new Employee("Doris", "Becker"));

            return Cementery.ToXml(microsoft);
        }
        static void Main(string[] args)
        {
            Company company = Cementery.FromXml<Company>(GetCompanyXml());

            Console.WriteLine("Name: {0}", company.Name);
            Console.WriteLine("Employees: {0}", company.Employees.Count);

            Console.ReadKey();
        }
    }
}
