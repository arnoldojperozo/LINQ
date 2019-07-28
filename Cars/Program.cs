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

            foreach(var car in cars)
            {
                Console.WriteLine(car.Name);
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            //var carsList = File.ReadAllLines(path)
            //    .Skip(1)
            //    .Where(line => line.Length > 1)
            //    //.Select(TransformToCar)
            //    .Select(Car.ParseFromCsv)
            //    .ToList();

            var query = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select Car.ParseFromCsv(line);

            return query.ToList();

            //return carsList;
        }

        private static Car TransformToCar(string arg1, int arg2)
        {
            throw new NotImplementedException();
        }
    }
}
