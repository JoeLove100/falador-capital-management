using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class MarketData 
    {

        #region constructor

        public MarketData(AssetDataSeriesCollection collection = null)
        {
            Timeline = new SortedSet<DateTime>();
            Data = new AssetDataSeriesCollection();
            
            if(collection != null)
            {
                AddRange(collection);
            }
        }

        #endregion

        #region properties 

        public SortedSet<DateTime> Timeline { get; set; }
        public AssetDataSeriesCollection Data { get; set; }

        #endregion

        #region methods

        protected void AddRange(AssetDataSeriesCollection collection)
        {
            foreach(AssetDataSeries series in collection)
            {
                Add(series);
            }
        }

        protected void Add(AssetDataSeries series)
        {
            Data.Add(series);
            UpdateTimelineForData(series);
        }

        protected void UpdateTimelineForData(AssetDataSeries series)
        {
            List<DateTime> keys = series.Keys.ToList();
            foreach(DateTime date in keys)
            {
                Timeline.Add(date);
            }
        }

        public IEnumerator<DateTime> GetNextDate()
        {
            foreach(DateTime date in Timeline)
            {
                yield return date;
            }

        }

        #endregion 
    }

    public class AssetDataSeriesCollection : KeyedCollection<string, AssetDataSeries>
    {
        protected override string GetKeyForItem(AssetDataSeries item)
        {
            return item.Name;
        }
    }
}
