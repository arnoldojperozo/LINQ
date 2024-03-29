﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");

            var manufacturers = ProcessManufacturers("manufacturers.csv");

            //JOIN - Query Syntax
            //var query = from car in cars
            //            join manufacturer in manufacturers
            //                on car.Manufacturer equals manufacturer.Name
            //            orderby car.Combined descending, car.Name ascending
            //            select new
            //            {
            //                manufacturer.Headquarters,
            //                car.Name,
            //                car.Combined
            //            };

            //var query = from car in cars
            //            join manufacturer in manufacturers
            //                on new { car.Manufacturer, car.Year }
            //                equals
            //                new { Manufacturer = manufacturer.Name, manufacturer.Year }
            //            orderby car.Combined descending, car.Name ascending
            //            select new
            //            {
            //                manufacturer.Headquarters,
            //                car.Name,
            //                car.Combined
            //            };

            //var query2 = cars.Join(manufacturers,
            //    c => new { c.Manufacturer, c.Year },
            //    m => new { Manufacturer = m.Name, m.Year },
            //    (c, m) => new
            //    {
            //        //m.Headquarters,
            //        //c.Name,
            //        //c.Combined,
            //        Car = c,
            //        Manufacturer = m
            //    })
            //    .OrderByDescending(c => c.Car.Combined)
            //    .ThenBy(c => c.Car.Name)
            //    .Select(c => new
            //    {
            //        c.Manufacturer.Headquarters,
            //        c.Car.Name,
            //        c.Car.Combined,
            //    });

            var query3 = from car in cars
                        group car by car.Manufacturer;

            var query4 = from manufacturer in manufacturers
                         join car in cars on manufacturer.Name equals car.Manufacturer
                            into carGroup
                         select new
                         {
                             Manufacturer = manufacturer,
                             Cars = carGroup
                         };

            var query4 = manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
                (m, g) => new
                {
                    Manufacturer = m,
                    Cars = g
                }).OrderBy(m => m.Manufacturer.Name);

            foreach (var group in query4)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderByDescending(c=>c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            //var query2 = cars.Join(manufacturers,
            //    c => c.Manufacturer,
            //    m => m.Name,
            //    (c, m) => new
            //    {
            //        //m.Headquarters,
            //        //c.Name,
            //        //c.Combined,
            //        Car = c,
            //        Manufacturer = m
            //    })
            //    .OrderByDescending(c => c.Car.Combined)
            //    .ThenBy(c => c.Car.Name)
            //    .Select(c => new
            //    {
            //        c.Manufacturer.Headquarters,
            //        c.Car.Name,
            //        c.Car.Combined,
            //    });

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //}


            //var query = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name);

            //var query = from car in cars
            //            orderby car.Combined descending, car.Name
            //            select car;

            //var top = cars//.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
            //    .OrderByDescending(c => c.Combined)
            //    .ThenBy(c => c.Name)
            //    .Select(c => c)
            //    .First(c => c.Manufacturer == "BMW" && c.Year == 2016);

            //var result = cars.Select(c => c.Name)
            //    .OrderBy(c=>c);

            //foreach(var character in result)
            //{
            //    Console.WriteLine(character);
            //}

            //foreach(var name in result)
            //{
            //    Console.WriteLine(name);
            //    foreach(var character in name)
            //    {
            //        Console.WriteLine(character);
            //    }
            //}

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Name} : {car.Combined}");
            //}
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query = File.ReadAllLines(path)
                .Where(l => l.Length > 1)
                .Select(l =>
                {
                    var columns = l.Split(',');
                    return new Manufacturer
                    {
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2])
                    };
                });

            return query.ToList();
        }

        private static List<Car> ProcessFile(string path)
        {
            //var carsList = File.ReadAllLines(path)
            //    .Skip(1)
            //    .Where(line => line.Length > 1)
            //    //.Select(TransformToCar)
            //    .Select(Car.ParseFromCsv)
            //    .ToList();

            //var query = from line in File.ReadAllLines(path).Skip(1)
            //            where line.Length > 1
            //            select Car.ParseFromCsv(line);

            //return query.ToList();

            var query = File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                //.Select(l => Car.ParseFromCsv(l));
                .ToCar();

            return query.ToList();

            //return carsList;
        }

        private static Car TransformToCar(string arg1, int arg2)
        {
            throw new NotImplementedException();
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
