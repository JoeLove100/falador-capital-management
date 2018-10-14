using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class AssetPriceSeries : SortedDictionary<DateTime, double>
    {

        #region constructors
        public AssetPriceSeries(string name)
        {
            Name = name;
        }

        public AssetPriceSeries(List<DateTime> dates, List<double> assetPrices, string name)
        {
            Name = name;
            AddDataToSeries(dates, assetPrices);
        }

        #endregion

        #region properties

        public string Name { get; set; }
        public DateTime FirstDate => this.Keys.First<DateTime>();
        public DateTime LastDate => this.Keys.Last<DateTime>();

        #endregion

        #region methods

        public void AddDataToSeries(List<DateTime> dates, List<double> assetPrices)
        {
            for(int i =0; i < dates.Count; i++)
            {
                this.Add(dates[i], assetPrices[i]);
            }
        }

        public AssetPriceSeries GetSubset(DateRange range)
        {
            var output = new AssetPriceSeries(Name);
            var dates = new List<DateTime>();
            var prices = new List<double>();

            foreach(KeyValuePair<DateTime, double> point in this)
            {
                if(point.Key >= range.Start & point.Key <= range.End)
                {
                    dates.Add(point.Key);
                    prices.Add(point.Value);
                }
            }

            output.AddDataToSeries(dates, prices);

            return output;
        }

        #endregion
    }
}