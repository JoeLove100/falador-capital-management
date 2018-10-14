using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class AssetPriceSeriesCollection : KeyedCollection<string, AssetPriceSeries>
    {

        public AssetPriceSeriesCollection()
        {

        }

        protected override string GetKeyForItem(AssetPriceSeries series)
        {
            return series.Name;
        }
    }
}
