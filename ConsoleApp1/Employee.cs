using ConsoleTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Employee
    {
        [Key]
        public Guid EmpId { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }

        public double Basic { get; set; }

        public double HRA { get; set; }

        public void acceptInput(Employee employee)
        {
           
                Console.WriteLine("Please enter details");
                Console.WriteLine("Name");
                var name = Console.ReadLine();
                Console.WriteLine("=================================================================================================");

                Console.WriteLine("Designation");
                var designation = Console.ReadLine();
                Console.WriteLine("=================================================================================================");

                Console.WriteLine("Basic");
                var basic = Double.Parse(Console.ReadLine());
                if (basic < 500)
                    throw new LowSalException(name);

                employee.EmpId = Guid.NewGuid();
                employee.EmpName = name;
                employee.Designation = designation;
                employee.Basic = basic;
                employee.HRA = 0;
           

        }

        public void storeToDatabase(Employee employee)
        {
            using (var context = new AppDbcontext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }
        public IEnumerable<Employee> retreiveFromDatabase()
        {
            IEnumerable<Employee> employees;
            using (var context = new AppDbcontext())
            {
                employees = context.Employees.ToList();

                var table = new ConsoleTable("Name", "Designation", "Basic","HRA");
                foreach (var emp in employees)
                {
                    table.AddRow(emp.EmpName, emp.Designation, emp.Basic, emp.HRA);
                }

                Console.WriteLine(table);
            }

            return employees;
        }

        public void calculateHRA(Employee employee)
        {
            double HRA = 0;
            if (employee.Designation == "Manager")
                HRA = employee.Basic * 0.10;
            if (employee.Designation == "Officer")
                HRA = employee.Basic * 0.12;
            if (employee.Designation == "Cleark")
                HRA = employee.Basic * 0.05;

            employee.HRA = HRA;
            Console.WriteLine("Calculated HRA is = {0}", employee.HRA);
           

        }
    }
}
