using System;
using System.Collections.Generic;
using System.Linq;

namespace CSTypes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Example of default values for an initialized class");
            var exampleClass = new ExampleClass();

            Console.WriteLine("default int");
            Console.WriteLine(exampleClass.initNumber);

            Console.WriteLine("default decimal");
            Console.WriteLine(exampleClass.decimalNumber);

            Console.WriteLine("default boolean");
            Console.WriteLine(exampleClass.boolean);

            Console.WriteLine("default string list");
            Console.WriteLine(exampleClass.stringList);
            Console.WriteLine("is it null value");
            Console.WriteLine(exampleClass.stringList is null);

            Console.WriteLine("default int list");
            Console.WriteLine(exampleClass.intList);
            Console.WriteLine("is it null value");
            Console.WriteLine(exampleClass.intList is null);

            Console.WriteLine("default decimal list");
            Console.WriteLine(exampleClass.decimalList);
            Console.WriteLine("is it null value");
            Console.WriteLine(exampleClass.decimalList is null);

            Console.WriteLine("============================");
            Console.WriteLine("=======LINQ WHERE====================");
            List<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
            var singleResult = intList.Where(x => x == 1).ToList();
            var multipleResult = intList.Where(x => x < 4).ToList();
            var noResult = intList.Where(x => x == 9).ToList();

            Console.WriteLine("Single result list"); // returns list
            Console.WriteLine(singleResult.GetType());
            Console.WriteLine("Count");
            Console.WriteLine(singleResult.Count);

            Console.WriteLine("Multiple result list"); // returns list
            Console.WriteLine(multipleResult.GetType());
            Console.WriteLine("Count");
            Console.WriteLine(multipleResult.Count);

            Console.WriteLine("No result list");// returns list
            Console.WriteLine(noResult.GetType());
            Console.WriteLine("Count");
            Console.WriteLine(noResult.Count); // returns 0

            Console.WriteLine("=======LINQ SELECT====================");

            var singleResultSelect = intList.Select(x => x == 1).ToList();
            var multipleResultSelect = intList.Select(x => x < 4).ToList();
            var noResultSelect = intList.Select(x => x == 9).ToList();

            Console.WriteLine("Single result list"); // returns list
            Console.WriteLine(singleResultSelect.GetType());
            Console.WriteLine("Count");
            Console.WriteLine(singleResult.Count);

            Console.WriteLine("Multiple result list"); // returns list
            Console.WriteLine(multipleResultSelect.GetType());
            Console.WriteLine("Count");
            Console.WriteLine(multipleResult.Count);

            Console.WriteLine("No result list");// returns list
            Console.WriteLine(noResultSelect.GetType());
            Console.WriteLine("Count");
            Console.WriteLine(noResult.Count); // returns 0

            Console.WriteLine(Films.Malo);

            Console.WriteLine("Enum console insert");
            Console.WriteLine("Insert movie: Feo, Malo,");
            try
            {
                // Only works with Feo
                bool v = Films.TryParse(Console.ReadLine(), out Films value);
                Console.WriteLine(v);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae);
            }

            // It aint no case sensitive
            Console.WriteLine("Boolean console insert");
            Console.WriteLine("Insert an option: true or false");
            try
            {
                bool v = Boolean.TryParse(Console.ReadLine(), out bool value);
                Console.WriteLine(v);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae);
            }

            Console.ReadLine();
            Console.WriteLine("Press a key to continue");
        }
    }
}
