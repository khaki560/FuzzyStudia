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
            //Test of databse with classical set
            //ClassicalTest();

            //Test of databse with Fuzzy set with Triangle MemberShip Function
            //TriangleFuzzyTest();

            //Test of databse with Fuzzy set with trapezoid MemberShip Function
            //TrapezoidFuzzyTest();

            //Test of databse with Fuzzy set with Gauss MemberShip Function
            //GaussFuzzyTest();

        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("settings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }

        static void PrintBool(string message, bool value)
        {
            Console.WriteLine(message);
            Console.WriteLine(value);
        }

        static void PrintDouble(string message, double value)
        {
            Console.WriteLine(message);
            Console.WriteLine(value);
        }
        static void PrintSet(string message, Set set)
        {
            Console.WriteLine(message);
            foreach (var x in set.Get())
            {
                Console.WriteLine(x);
            }
        }

        static void ClassicalTest()
        {
            // Reading database
            var fuzzyDatabase = new FuzzySql(_iconfiguration);
            var listFuzzyModel = fuzzyDatabase.GetList();
            var dayNumber = new List<double>();
            var temperature = new List<double>();
            var huminidity = new List<double>();
            int i = 1;
            foreach (var fuzzyModel in listFuzzyModel)
            {
                dayNumber.Add(Convert.ToDouble(i++));
                temperature.Add(fuzzyModel.Temperature);
                huminidity.Add(fuzzyModel.Humidity);
            }

            // narrow down entries for better formating
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
            PrintDouble("Height of setTemperatureCold", setTemperatureCold.Height());
            PrintBool("is setTemperatureCold Empty", setTemperatureCold.IsEmpty());
            PrintBool("is setTemperatureCold Normal", setTemperatureCold.IsNormal());
            // Not implement
            //PrintBool("is setTemperatureCold Concave", setTemperatureCold.IsConcave());
            //PrintBool("is setTemperatureCold Convex", setTemperatureCold.IsConvex());
        }

        static void TriangleFuzzyTest()
        {
            // Reading database
            var fuzzyDatabase = new FuzzySql(_iconfiguration);
            var listFuzzyModel = fuzzyDatabase.GetList();
            var dayNumber = new List<double>();
            var temperature = new List<double>();
            var huminidity = new List<double>();
            int i = 1;
            foreach (var fuzzyModel in listFuzzyModel)
            {
                dayNumber.Add(Convert.ToDouble(i++));
                temperature.Add(fuzzyModel.Temperature);
                huminidity.Add(fuzzyModel.Humidity);
            }

            // narrow down entries for better formating
            int lowerLimit = 0;
            int upperLimit = 30;
            var temperature30 = temperature.GetRange(lowerLimit, upperLimit);
            var huminidity30 = huminidity.GetRange(lowerLimit, upperLimit);
            var dayNumber30 = dayNumber.GetRange(lowerLimit, upperLimit);

            // Creating "is day Temperature around 7"  Set
            var membershipTemperatureCold = new TriangleMembershipFunction(6, 7, 8);
            var setTemperature7 = new FuzzyLogic.src.Set(dayNumber30, temperature30, membershipTemperatureCold);

            // Creating "is day Huminidity around 0.85 "  Classical Set
            var membershipHuminidity = new TriangleMembershipFunction(0.80, 0.85, 0.90);
            var setHuminidity085 = new FuzzyLogic.src.Set(dayNumber30, huminidity30, membershipHuminidity);

            PrintSet("Is Temperature around 7?", setTemperature7);
            PrintSet("Is Temperature NOT around 7?", setTemperature7.complement());
            PrintSet("Is Huminidity around 0.85", setHuminidity085);
            PrintSet("Is Temperature around 7 AND Huminidity around 0.85", setTemperature7.Intersection(setHuminidity085));
            PrintSet("Is Temperature around 7 AND Huminidity around 0.85", setTemperature7.Union(setHuminidity085));
            PrintDouble("Height of setTemperature7", setTemperature7.Height());
            PrintBool("is setTemperature7 Empty", setTemperature7.IsEmpty());
            PrintBool("is setTemperature7 Normal", setTemperature7.IsNormal());
            // Not implement
            //PrintBool("is setTemperature7 Concave", setTemperature7.IsConcave());
            //PrintBool("is setTemperature7 Convex", setTemperature7.IsConvex());
        }
    
        static void TrapezoidFuzzyTest()
        {
            // Reading database
            var fuzzyDatabase = new FuzzySql(_iconfiguration);
            var listFuzzyModel = fuzzyDatabase.GetList();
            var dayNumber = new List<double>();
            var temperature = new List<double>();
            var huminidity = new List<double>();
            int i = 1;
            foreach (var fuzzyModel in listFuzzyModel)
            {
                dayNumber.Add(Convert.ToDouble(i++));
                temperature.Add(fuzzyModel.Temperature);
                huminidity.Add(fuzzyModel.Humidity);
            }

            // narrow down entries for better formating
            int lowerLimit = 0;
            int upperLimit = 30;
            var temperature30 = temperature.GetRange(lowerLimit, upperLimit);
            var huminidity30 = huminidity.GetRange(lowerLimit, upperLimit);
            var dayNumber30 = dayNumber.GetRange(lowerLimit, upperLimit);

            // Creating "is day Temperature  7-8"  Set
            var membershipTemperatureCold = new TrapezoidMembershipFunction(6, 7, 8, 9);
            var setTemperature7_8 = new FuzzyLogic.src.Set(dayNumber30, temperature30, membershipTemperatureCold);

            // Creating "is day Huminidity around 0.85 "  Classical Set
            var membershipHuminidity = new TrapezoidMembershipFunction(0.80, 0.85, 0.90, 0.95);
            var setHuminidity085_090 = new FuzzyLogic.src.Set(dayNumber30, huminidity30, membershipHuminidity);

            PrintSet("Is Temperature around 7?", setTemperature7_8);
            PrintSet("Is Temperature NOT around 7?", setTemperature7_8.complement());
            PrintSet("Is Huminidity around 0.85", setHuminidity085_090);
            PrintSet("Is Temperature around 7 AND Huminidity around 0.85", setTemperature7_8.Intersection(setHuminidity085_090));
            PrintSet("Is Temperature around 7 AND Huminidity around 0.85", setTemperature7_8.Union(setHuminidity085_090));
            PrintDouble("Height of setTemperature7_8", setTemperature7_8.Height());
            PrintBool("is setTemperature7_8 Empty", setTemperature7_8.IsEmpty());
            PrintBool("is setTemperature7_8 Normal", setTemperature7_8.IsNormal());
            // Not implement
            //PrintBool("is setTemperature7_8 Concave", setTemperature7_8.IsConcave());
            //PrintBool("is setTemperature7_8 Convex", setTemperature7_8.IsConvex());
        }

        static void GaussFuzzyTest()
        {
            // Reading database
            var fuzzyDatabase = new FuzzySql(_iconfiguration);
            var listFuzzyModel = fuzzyDatabase.GetList();
            var dayNumber = new List<double>();
            var temperature = new List<double>();
            var huminidity = new List<double>();
            int i = 1;
            foreach (var fuzzyModel in listFuzzyModel)
            {
                dayNumber.Add(Convert.ToDouble(i++));
                temperature.Add(fuzzyModel.Temperature);
                huminidity.Add(fuzzyModel.Humidity);
            }

            // narrow down entries for better formating
            int lowerLimit = 0;
            int upperLimit = 30;
            var temperature30 = temperature.GetRange(lowerLimit, upperLimit);
            var huminidity30 = huminidity.GetRange(lowerLimit, upperLimit);
            var dayNumber30 = dayNumber.GetRange(lowerLimit, upperLimit);

            // Creating "is day Temperature around 7"  Set
            var membershipTemperatureCold = new GaussMembershipFunction(7, 1);
            var setTemperature7 = new FuzzyLogic.src.Set(dayNumber30, temperature30, membershipTemperatureCold);

            // Creating "is day Huminidity around 0.85 "  Classical Set
            var membershipHuminidity = new GaussMembershipFunction(0.85, 0.05);
            var setHuminidity085 = new FuzzyLogic.src.Set(dayNumber30, huminidity30, membershipHuminidity);

            PrintSet("Is Temperature around 7?", setTemperature7);
            PrintSet("Is Temperature NOT around 7?", setTemperature7.complement());
            PrintSet("Is Huminidity around 0.85", setHuminidity085);
            PrintSet("Is Temperature around 7 AND Huminidity around 0.85", setTemperature7.Intersection(setHuminidity085));
            PrintSet("Is Temperature around 7 AND Huminidity around 0.85", setTemperature7.Union(setHuminidity085));
            PrintDouble("Height of setTemperature7", setTemperature7.Height());
            PrintBool("is setTemperature7 Empty", setTemperature7.IsEmpty());
            PrintBool("is setTemperature7 Normal", setTemperature7.IsNormal());
            // Not implement
            //PrintBool("is setTemperature7 Concave", setTemperature7.IsConcave());
            //PrintBool("is setTemperature7 Convex", setTemperature7.IsConvex());
        }
    }
}