using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using BaseLib;
using LinqDemo.Models;
using Newtonsoft.Json;

namespace LinqDemo
{
    // class Name
    // {
    //     public string First { get; set; }
    //     public string Last { get; set; }
    // }

    public class LinqExamples
    {
        public static void Run01()
        {
            var persons = LoadPersons();

            // var list1 = persons
            //     .Select(p => p.FirstName);


            var list1 = persons
                .Where(p => p.Gender == "Male")
                .Select(p => new
                {
                    First = p.FirstName,
                    Last = p.LastName,
                    p.Gender
                })
                .OrderBy(q => q.Last)
                .ThenBy(q => q.First)
                .Take(20);

            Console.WriteLine();
            // var list1 = persons
            //     .Select((p, i) => new
            //     {
            //         i,
            //         FirstName = p.FirstName,
            //         LastName = p.LastName,
            //         Email = p.Email
            //     });

            // var list1 = persons
            //     .Where(p => p.Gender == "Male")
            //     .Select((p, i) => new
            //     {
            //         i,
            //         FirstName = p.FirstName,
            //         LastName = p.LastName,
            //         Email = p.Email
            //     })
            //     .OrderBy(p => p.LastName)
            //     .ThenBy(p => p.FirstName)
            //     .Select(p => $"{p.LastName} {p.FirstName} {p.i}");

            Console.WriteLine(list1.ToCsv("\n"));

            // Console.WriteLine(persons.ToCsv("\n"));
        }

        public static void Run02()
        {
            var persons = LoadPersons();

            var groups = persons
                .GroupBy(p => p.Gender);

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Key}");
                Console.WriteLine("-----------------------------------");

                foreach (var person in group.Take(5))
                {
                    Console.WriteLine(person);
                }

                Console.WriteLine();
            }
        }


        static IEnumerable<Person> LoadPersons()
        {
            var exePath = Path.GetDirectoryName(
                typeof(LinqExamples).Assembly.Location);

            var filename = Path.Combine(exePath, "Data", "MOCK_DATA.json");

            var json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<List<Person>>(json);
        }

    }
}
