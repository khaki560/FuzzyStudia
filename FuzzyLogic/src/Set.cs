using FuzzyLogic.src.membership;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyLogic.src
{
    class Set
    {
        private List<Tuple<Double, Double>> mSet = new List<Tuple<Double, Double>>();
        private IMembershipFunction mMemberShipFunction;

        private void init(List<Double> input, List<Double> value, IMembershipFunction memberShipFunction)
        {
            mSet.Clear();
            for (int i = 0; i < input.Count; i++)
            {
                mSet.Add(new Tuple<double, double>(input[i], memberShipFunction.calc(value[i])));
            }

            mMemberShipFunction = memberShipFunction;
        }
        public Set(List<Tuple<Double, Double>> set, IMembershipFunction memberShipFunction)
        {
            mMemberShipFunction = memberShipFunction;
            mSet.Clear();
            foreach (var el in set)
            {
                mSet.Add(new Tuple<double, double>(el.Item1, el.Item2));
            }
        }
        public Set(List<Double> input, List<Double> value, IMembershipFunction memberShipFunction)
        {
            init(input, value, memberShipFunction);
        }
        public List<Tuple<Double, Double>> Get()
        {
            return mSet;
        }

        public int Length()
        {
            return mSet.Count;
        }

        public Set complement() 
        {
            var set = new List<Tuple<Double, Double>>();

            foreach (var x in mSet)
            {
                set.Add( new Tuple<Double, Double>(x.Item1, Math.Round(1 - x.Item2, 2)));
            }

            return new Set(set, mMemberShipFunction);
        }

        public Set Intersection(Set b)
        {
            var set = new List<Tuple<Double, Double>>();

            for (int i = 0; i < this.Length(); i++)
            {
                if( Math.Abs(this.mSet[i].Item1 - b.mSet[i].Item1) > 0.01)
                {
                    throw new Exception("Elements not match");
                }
                set.Add(new Tuple<Double, Double>(this.mSet[i].Item1, Math.Min(this.mSet[i].Item2, b.mSet[i].Item2)));
            }

            return new Set(set, mMemberShipFunction);
        }

        public Set Union(Set b)
        {
            var set = new List<Tuple<Double, Double>>();

            for (int i = 0; i < this.Length(); i++)
            {
                if (Math.Abs(this.mSet[i].Item1 - b.mSet[i].Item1) > 0.01)
                {
                    throw new Exception("Elements not match");
                }
                set.Add(new Tuple<Double, Double>(this.mSet[i].Item1, Math.Max(this.mSet[i].Item2, b.mSet[i].Item2)));
            }

            return new Set(set, mMemberShipFunction);
        }

        public double Height()
        {
            double max_el = -1.0;
            foreach(var el in mSet)
            {
                double value = el.Item2;

                if(value > max_el)
                {
                    max_el = value;
                }
            }
            return max_el;
        }

        public bool IsNormal()
        {
            return Math.Abs(1 - Height()) < 0.01;
        }

        public bool IsEmpty()
        {
            bool IsEmpty = true;

            foreach (var el in mSet)
            {
                double value = el.Item2;
                if(!(Math.Abs(0 - value) < 0.01))
                {
                    IsEmpty = false;
                    break;
                }
            }
            return IsEmpty;
        }

        public bool IsConcave()
        {
            throw new NotImplementedException("No idea how");
        }

        public bool IsConvex()
        {
            throw new NotImplementedException("No idea how");
        }
    }
}
