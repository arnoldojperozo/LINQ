using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movie>()
            {
                new Movie { Title = "Clone wars", Year = 2002, Rating = 9.5f },
                new Movie { Title = "Phantom Menace", Year = 1999, Rating = 9.0f },
                new Movie { Title = "Revenge of the Siths", Year = 2005, Rating = 9.0f },
                new Movie { Title = "The Last Jedi", Year = 2017, Rating = 9.5f },
                new Movie { Title = "Star Wars", Year = 1977, Rating = 9.0f },
                new Movie { Title = "The Force Awakens", Year = 2015, Rating = 9.0f },
                new Movie { Title = "Return of the Jedi", Year = 1983, Rating = 9.0f },
                new Movie { Title = "The Rise of Skywalker", Year = 2019, Rating = 9.0f },
                new Movie { Title = "Empire Strikes Back", Year = 1980, Rating = 9.5f },
            };

            var query = movies.Where(m => m.Year >= 2000);
            //var query = movies.Filter(m => m.Year >= 2000);

            Console.WriteLine(query.Count());
            //foreach (var movie in movies)
            //{
            //    System.Console.WriteLine(movie.Title);
            //}
            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }
}
