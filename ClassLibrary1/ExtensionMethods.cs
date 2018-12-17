using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ExtensionMethods
    {
        public static bool IsBetween<T>(this T item, T start, T end) 
            where T : IComparable
        {
            ///<summary>
            ///method for testing whether an Icomparable lies between
            ///two values, as defined by its default comparer
            ///</summary>
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }

        public static IList<decimal> Subtract(this IList<decimal> items, decimal x) 
        {
            ///<summary>
            ///subtract the given x elementwise from
            ///the extended IList
            ///</summary>
            decimal[] output = new decimal[items.Count()];

            for(int i = 0; i < items.Count(); i++)
            {
                output[i] = items[i] - x;
            }

            return output;
        }


        public static IList<decimal> Cap(this IList<decimal> items, decimal cap) 
        {
            ///<summary>
            ///take each element of the array as the smaller
            ///of the cap and the element
            ///</summary>

            decimal[] output = new decimal[items.Count()];

            for(int i = 0; i < items.Count(); i++)
            {
                output[i] = Math.Min(items[i], cap);
            }

            return output;
        }

        public static SortedList<DateTime, decimal> PriceToReturns(
            this SortedList<DateTime, decimal> items)
        {
            ///<summary>
            ///turns a series of prices into a 
            ///cumulative return series
            ///</summary>

            var output = new SortedList<DateTime, decimal>();
            var enumerator = items.GetEnumerator();

            bool keepGoing = enumerator.MoveNext();

            if (!keepGoing) return output;

            var prev = enumerator.Current.Value;
            keepGoing = enumerator.MoveNext();

            while (keepGoing)
            {
                decimal ret = enumerator.Current.Value / prev - 1;
                output.Add(enumerator.Current.Key, ret);
                prev = enumerator.Current.Value;
                keepGoing = enumerator.MoveNext();
            }

            return output;
        }

        public static SortedList<DateTime, decimal> PriceToScaledCumulativeReturns(
    this SortedList<DateTime, decimal> items, decimal scaling = 1)
        {
            ///<summary>
            ///turns a series of prices into a 
            ///cumulative return series
            ///</summary>

            var output = new SortedList<DateTime, decimal>();
            var enumerator = items.GetEnumerator();

            bool keepGoing = enumerator.MoveNext();

            while (keepGoing)
            {
                decimal ret = enumerator.Current.Value / items.First().Value - 1;
                ret *= scaling;
                output.Add(enumerator.Current.Key, ret);
                keepGoing = enumerator.MoveNext();
            }

            return output;
        }

        public static bool IsAlmostEqual(this IList<decimal> seqOne, 
            IList<decimal> seqTwo, decimal tol = 0.00001m)
        {
            if (seqOne.Count() != seqTwo.Count()) return false;

            for(int i = 0; i < seqOne.Count(); i++)
            {
                if (Math.Abs(seqOne[i] - seqTwo[i]) > tol) return false;
            }

            return true;
        }
    }
}
