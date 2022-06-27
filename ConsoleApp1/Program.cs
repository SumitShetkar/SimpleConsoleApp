using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Employee emp = new Employee();
                emp.acceptInput(emp);
                Console.WriteLine("=================================================================================================");
                emp.calculateHRA(emp);
                emp.storeToDatabase(emp);

                Console.WriteLine("=================================================================================================");
                Console.WriteLine();

                var employees = emp.retreiveFromDatabase();

                Dictionary<Guid, double> EmpData = new Dictionary<Guid, double>();
                foreach (var Emp in employees)
                {
                    var netsal = Emp.Basic + Emp.HRA;
                    EmpData.Add(Emp.EmpId, netsal);
                }

                Console.WriteLine("=================================================================================================");
                Console.WriteLine();

                var maxValue = EmpData.Values.Max();
                var keyOfMaxValue = EmpData.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                Console.WriteLine("Emp with highest net salary is = {0}", maxValue);

                Console.WriteLine("=================================================================================================");
                Console.WriteLine();

                Console.WriteLine("Emp details with highest net salary is");

                var context = new AppDbcontext();
                var MaxPayedEmp = context.Employees.ToList().Where(o => o.EmpId == keyOfMaxValue);
                var table = new ConsoleTable("Name", "Designation", "Basic", "HRA");
                foreach (var emp1 in MaxPayedEmp)
                {
                    table.AddRow(emp1.EmpName, emp1.Designation, emp1.Basic, emp1.HRA);
                }
                Console.WriteLine(table);


                Console.WriteLine("=================================================================================================");
                Console.WriteLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
