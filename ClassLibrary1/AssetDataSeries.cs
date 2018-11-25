using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class AssetDataSeries : SortedDictionary<DateTime, Bar>, IIsSameAs
    {

        #region constructors
        public AssetDataSeries(string name)
        {
            Name = name;
        }

        public AssetDataSeries(IList<DateTime> dates, IList<Bar> marketData, string name,
            DateTime? currentDate = null)
        {
            Name = name;
            AddDataToSeries(dates, marketData);
        }

        #endregion

        #region properties

        public string Name { get; set; }
        public DateTime FirstDate => this.Keys.First<DateTime>();
        public DateTime LastDate => this.Keys.Last<DateTime>();

        #endregion

        #region methods

        public void AddDataToSeries(IList<DateTime> dates, IList<Bar> marketData)
        {
            for (int i = 0; i < dates.Count; i++)
            {
                this.Add(dates[i], marketData[i]);
            }
        }

        public AssetDataSeries GetSubset(DateRange range)
        {

            if (range.Start < FirstDate || range.End > LastDate)
            {
                throw new ArgumentOutOfRangeException("date range is not a subset of the dates in this series");
            }

            var output = new AssetDataSeries(Name);
            var dates = new List<DateTime>();
            var bars = new List<Bar>();

            foreach (KeyValuePair<DateTime, Bar> point in this)
            {
                if (point.Key >= range.Start & point.Key <= range.End)
                {
                    dates.Add(point.Key);
                    bars.Add(point.Value);
                }
            }

            output.AddDataToSeries(dates, bars);

            return output;
        }
    
        public List<decimal> GetPricesInOrder()
        {
            List<decimal> output = new List<decimal>();

            foreach(KeyValuePair<DateTime, Bar> kvp in this.OrderBy(key => key.Key))
            {
                output.Add(kvp.Value.Price);
            }
            return output;
        }

        public IEnumerable<KeyValuePair<DateTime, Bar>> GetNextEntry()
        {
            foreach(KeyValuePair<DateTime, Bar> kvp in this)
            {
                yield return kvp;
            }
        }

        public Bar[] GetLatestBars(DateTime currentDate, int n)
        {
            ///<summary>
            ///returns the latest n bars in the dictionary, or
            ///the last k bars if there are only k remaining
            ///(k less than n)
            ///</summary>

            if (!ContainsKey(currentDate))
            {
                throw new KeyNotFoundException($"No market " +
                    $"data for {currentDate.ToShortDateString()}");
            }

            List<Bar> output = new List<Bar>();
            int count = 0;

            foreach(KeyValuePair<DateTime, Bar> kvp in this.Reverse())
            {
                if (kvp.Key > currentDate) continue;
                output.Add(kvp.Value);
                count = count++;
                if (count >= n) break;
            }

            return output.ToArray();
        }

        public IEnumerable<DateTime> GetNextDate()
        {
            foreach(DateTime date in Keys)
            {
                yield return date;
            }
        }

        #endregion

        #region implementations

        public bool IsSameAs(IIsSameAs comparator)
        {
            AssetDataSeries comparison = comparator as AssetDataSeries;
            if (comparison is null) return false;

            if (Name != comparison.Name) return false;
            return Compare.AreSameAs((IDictionary<DateTime, Bar>) this,  comparison);
        }

        #endregion 
    }
}