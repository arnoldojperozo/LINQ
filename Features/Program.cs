using System;
using System.Collections.Generic;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lambda Delegates
            //Lambda for a one line square function 
            Func<int, int> square = x => x * x;
            //Lambda for a multiline function
            Func<int, int, int> add = (x, y) =>
                    {
                        int temp = x + y;
                        return temp;
                    };

            // Action Delegates, always return void
            Action<int> write = x => Console.WriteLine(x);

            write(square(add(5, 3)));

            //Employee[] developers = new Employee[]
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee{Id=1,Name="Scott"},
                new Employee{Id=2,Name="Sample"}
            };

            //List<Employee> sales = new List<Employee>()
            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee{Id=3,Name="Alex"}
            };

            IEnumerator<Employee> enumerator = sales.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }
        }
    }
}
