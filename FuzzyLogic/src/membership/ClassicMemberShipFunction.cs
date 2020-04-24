using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic.src.membership
{
    class ClassicMembershipFunction : IMembershipFunction
    {
        private delegate bool MembershipFunction(double x);

        private double mThreshold;
        MembershipFunction mMembershipFunction;

        public ClassicMembershipFunction(string sign, double threshold)
        {
            mThreshold = threshold;

            switch(sign)
            {
                case ">":
                    mMembershipFunction = new MembershipFunction(gt);
                    break;
                case ">=":
                    mMembershipFunction = new MembershipFunction(ge);
                    break;
                case "<":
                    mMembershipFunction = new MembershipFunction(lt);
                    break;
                case "<=":
                    mMembershipFunction = new MembershipFunction(le);
                    break;
                case "==":
                    mMembershipFunction = new MembershipFunction(eq);
                    break;
                case "!=":
                    mMembershipFunction = new MembershipFunction(ne);
                    break;
            }
        }
        private bool gt(double x)
        {
            return x > mThreshold;
        }
        private bool ge(double x)
        {
            return x >= mThreshold;
        }
        private bool lt(double x)
        {
            return x < mThreshold;
        }
        private bool le(double x)
        {
            return x <= mThreshold;
        }

        private bool eq(double x)
        {
            return Math.Abs(x - mThreshold) < 0.001;
        }
        private bool ne(double x)
        {
            return !eq(x);
        }

        public double calc(double x)
        {
            double result = -1;

            if (mMembershipFunction(x))
            {
                result = 1.0;
            }
            else
            {
                result = 0.0;
            }

            return result;
        }
    }
}
