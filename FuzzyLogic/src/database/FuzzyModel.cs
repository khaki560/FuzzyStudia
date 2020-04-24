using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic
{
    class FuzzyModel
    {
        public string FormattedDate { get; set; }
        public string Summary { get; set; }
        public string PrecipType { get; set; }
        public double Temperature { get; set; }
        public double ApparentTemperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeedKmH { get; set; }
        public double WindBearingDegrees { get; set; }
        public double VisibilityKm { get; set; }
        public double LoudCover { get; set; }
        public double PressureMillibars { get; set; }
        public string DailySummary { get; set; }
    }
}
