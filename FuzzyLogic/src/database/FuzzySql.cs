using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace FuzzyLogic
{ 
    class FuzzySql
    {
        private string _connectionString;

        public FuzzySql(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public List<FuzzyModel> GetList()
        {
            var listFuzzyModel = new List<FuzzyModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Select * from weather", con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listFuzzyModel.Add(new FuzzyModel
                        {
                            FormattedDate = rdr[0].ToString(),
                            Summary = rdr[1].ToString(),
                            PrecipType = rdr[2].ToString(),
                            Temperature = Math.Round(Convert.ToDouble(rdr[3]), 2),
                            ApparentTemperature = Math.Round(Convert.ToDouble(rdr[4]), 2),
                            Humidity = Math.Round(Convert.ToDouble(rdr[5]), 2),
                            WindSpeedKmH = Math.Round(Convert.ToDouble(rdr[6]), 2),
                            WindBearingDegrees = Math.Round(Convert.ToDouble(rdr[7]), 2),
                            VisibilityKm = Math.Round(Convert.ToDouble(rdr[8]), 2),
                            LoudCover = Math.Round(Convert.ToDouble(rdr[9]), 2),
                            PressureMillibars = Math.Round(Convert.ToDouble(rdr[10]), 2),
                            DailySummary = rdr[11].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listFuzzyModel;
        }
    }
}
