using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Bar : IIsSameAs
    {
        #region constructors

        public Bar(double price)
        {
            Price = price;
        }

        #endregion

        #region properties

        public double Price { get; }

        #endregion

        #region methods

        public bool IsSameAs(IIsSameAs comparator)
        {
            Bar comparison = comparator as Bar;
            if (comparison == null) return false;

            if (comparison.Price != Price) return false;

            return true;
        }

        #endregion 
    }
}
