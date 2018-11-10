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

        public static IList<double> Subtract(this IList<double> items, double x) 
        {
            ///<summary>
            ///subtract the given x elementwise from
            ///the extended IList
            ///</summary>
            double[] output = new double[items.Count()];

            for(int i = 0; i < items.Count(); i++)
            {
                output[i] = items[i] - x;
            }

            return output;
        }


        public static IList<double> Cap(this IList<double> items, double cap) 
        {
            ///<summary>
            ///take each element of the array as the smaller
            ///of the cap and the element
            ///</summary>

            double[] output = new double[items.Count()];

            for(int i = 0; i < items.Count(); i++)
            {
                output[i] = Math.Min(items[i], cap);
            }

            return output;
        }

        public static bool IsAlmostEqual(this IList<double> seqOne, 
            IList<double> seqTwo, double tol = 0.00001)
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
