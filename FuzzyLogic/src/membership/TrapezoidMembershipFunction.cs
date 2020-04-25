using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic.src.membership
{
    class TrapezoidMembershipFunction : IMembershipFunction
    {
        private double mA;
        private double mB;
        private double mC;
        private double mD;

        public TrapezoidMembershipFunction(double a, double b, double c, double d)
        {
            mA = a;
            mB = b;
            mC = c;
            mD = d;
        }
        double IMembershipFunction.calc(double x)
        {
            double el1 = 0;
            double el2 = 1;
            double el3 = (x - mA) / (mB - mA);
            double el4 = (mD - x) / (mD - mC);

            double el23 = Math.Min(el2, el3);
            double el234 = Math.Min(el23, el4);
            double el1234 = Math.Max(el1, el23);

            return Math.Round(el1234, 2);
        }
    }
}
