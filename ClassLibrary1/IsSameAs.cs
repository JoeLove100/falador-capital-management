using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Compare
    {
        public static bool AreSameAs<T>(IList<T> listOne, IList<T> listTwo)
        {
            if (listOne is null || listTwo is null) return false;
            if(listOne.Count() != listTwo.Count()) return false;

            var comp = EqualityComparer<T>.Default;

            for(int i = 0; i < listOne.Count(); i++)
            {
                if (!comp.Equals(listOne[i], listTwo[i])) return false;
            }

            return true;
        }

        public static bool AreSameAs<TKey, TValue>(IDictionary<TKey, TValue> dictOne, IDictionary<TKey, TValue> dictTwo)
        {
            if (dictOne == dictTwo) return true;
            if (dictOne is null || dictTwo is null) return false;
            if (dictOne.Count != dictTwo.Count) return false;

            var valueComparer = EqualityComparer<TValue>.Default;

            foreach(KeyValuePair<TKey, TValue> kvp in dictOne)
            {
                TValue valueTwo;
                if (!dictTwo.TryGetValue(kvp.Key, out valueTwo)) return false;
                if (!valueComparer.Equals(kvp.Value, valueTwo)) return false;
            }

            return true;
        }

        public static bool AreSameAs(IIsSameAs objectOne, IIsSameAs objectTwo)
        {
            if (objectOne is null || objectTwo is null) return false;
            if (objectOne.GetType() != objectTwo.GetType()) return false;

            return objectOne.IsSameAs(objectTwo);
        }

    }

    public interface IIsSameAs
    {
        bool IsSameAs(IIsSameAs comparison);
    }

    public class TestIsSameAs : IIsSameAs
    {
        public bool IsSameAs(IIsSameAs comparison)
        {
            return false;
        }
    }
    


}
