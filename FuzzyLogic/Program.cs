using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FuzzyLogic
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            PrintCountries();
        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("settings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }

        static void PrintCountries()
        {
            var countryDAL = new FuzzySql(_iconfiguration);
            var listCountryModel = countryDAL.GetList();
            listCountryModel.ForEach(item =>
            {
                Console.WriteLine(item.FormattedDate);
            });
            Console.WriteLine("Press any key to stop.");
            Console.ReadKey();
        }
    }
}


  //"ConnectionStrings": {
  //  "Default": "Server=DESKTOP-2B7NGKL;Database=FuzzyDataSet;UserId=YOUR_USER;Password=YOUR_PASSWORD ;MultipleActiveResultSets=true"