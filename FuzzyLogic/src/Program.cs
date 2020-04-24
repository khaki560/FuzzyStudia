using Microsoft.Extensions.Configuration;
using System;
using System.IO;

using FuzzyLogic.src.membership;
using FuzzyLogic.src;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyLogic
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingsFile();

            // Reading database
            var fuzzyDatabase = new FuzzySql(_iconfiguration);
            var listFuzzyModel = fuzzyDatabase.GetList();
            var dayNumber = new List<double>();
            var temperature = new List<double>();
            var huminidity = new List<double>();
            int i = 1;
            foreach(var fuzzyModel in listFuzzyModel)
            {
                dayNumber.Add(Convert.ToDouble(i++));
                temperature.Add(fuzzyModel.Temperature);
                huminidity.Add(fuzzyModel.Humidity);
            }

            // narrow down entries to better formating
            var temperature30 = temperature.GetRange(0, 5);
            var huminidity30 = huminidity.GetRange(0, 5);
            var dayNumber30 = dayNumber.GetRange(0, 5);


            // Creating "is day is cold" Classical Set
            var membershipTemperatureCold = new ClassicMembershipFunction("<=", 10);
            var setTemperatureCold = new FuzzyLogic.src.Set(dayNumber30, temperature30, membershipTemperatureCold);


            // Creating "is day very steamy"  Classical Set
            var membershipHuminidity = new ClassicMembershipFunction(">", 0.86);
            var setHuminidity = new FuzzyLogic.src.Set(dayNumber30, huminidity30, membershipHuminidity);


            PrintSet("Is day Cold?", setTemperatureCold);
            PrintSet("Is day NOT Cold ?", setTemperatureCold.complement());
            PrintSet("Is day very steamy", setHuminidity);
            PrintSet("Is day Cold AND very steamy", setTemperatureCold.Intersection(setHuminidity));
            PrintSet("Is day Cold OR very steamy", setTemperatureCold.Union(setHuminidity));
        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("settings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }

        static void PrintSet(string message, Set set)
        {
            Console.WriteLine(message);
            foreach (var x in set.Get())
            {
                Console.WriteLine(x);
            }
        }
    }
}