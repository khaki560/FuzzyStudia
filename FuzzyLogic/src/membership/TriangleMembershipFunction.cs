using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic.src.membership
{
    class TriangleMembershipFunction : IMembershipFunction
    {
        private double mA;
        private double mB;
        private double mC;
        public TriangleMembershipFunction(double a, double b, double c)
        {
            if((a > b) || (b > c))
            {
                throw new Exception("a,b and c must be true for a < b < c");
            }
            mA = a;
            mB = b;
            mC = c;

            Console.WriteLine("{0}, {1}, {2}", mA, mB, mC);
        }
        public TriangleMembershipFunction(List<double> a)
        {
            var values = new List<double>();

            foreach(var el in a)
            {
                values.Add(el);
            }

            values.Sort();
            mA = values[0];
            mB = values[(values.Count - 1) / 2];
            mC = values[values.Count - 1];

            Console.WriteLine("{0}, {1}, {2}", mA, mB, mC);
        }
        double IMembershipFunction.calc(double x)
        {
            double el1 = 0;
            double el2 = ((x - mA) / (mB - mA));
            double el3 = ((mC - x) / (mC - mB));

            double el23 = Math.Min(el2, el3);
            return Math.Round(Math.Max(el1, el23), 2);
        }
    }
}
