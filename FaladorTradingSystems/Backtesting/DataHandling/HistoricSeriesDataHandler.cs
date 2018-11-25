using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using FaladorTradingSystems.Backtesting.Events;

namespace FaladorTradingSystems.Backtesting.DataHandling
{
    /// <summary>
    /// data handler to use market data from
    /// the rune wiki stored in a csv - useful for
    /// backtesting strategies
    /// </summary>
    
    public class HistoricSeriesDataHandler : IDataHandler
    {
        #region constructor
        public HistoricSeriesDataHandler(MarketData marketData, EventStack eventStack)
        {
            _marketData = marketData;
            _dateEnumerator = _marketData.GetNextDate();
            AllAssets = _marketData.GetAllNames();
            _eventStack = eventStack;
            ContinueBacktest = true;
        }
        #endregion

        #region properties
        private List<string> _symbolList { get; set; }
        private MarketData _marketData { get; set; }
        private IEnumerator<DateTime> _dateEnumerator { get; }
        private EventStack _eventStack { get; }

        public DateTime CurrentDate { get; set; } 
        public List<string> AllAssets { get; set; }
        public bool ContinueBacktest { get; set; }
        #endregion 

        #region methods
        
        public Bar[] GetLatestBars(string ticker, int n= 1)
        {
            AssetDataSeries series;

            try
            {
                series = _marketData.Data[ticker];
            } 
            catch(KeyNotFoundException)
            {
                throw new KeyNotFoundException($"{ticker} not found in data series");
            }

            Bar[] output = series.GetLatestBars(_dateEnumerator.Current, n);
            return output;
        }

        public void UpdateBars()
        {
            ContinueBacktest = _dateEnumerator.MoveNext();
            CurrentDate = _dateEnumerator.Current;
            MarketEvent newDataArrived= new MarketEvent();
            _eventStack.PutEvent(newDataArrived);
        }

        public Dictionary<string, decimal> GetLastPrices()
        {
            Dictionary<string, decimal> output = new Dictionary<string, decimal>();

            foreach(string asset in AllAssets)
            {
                Bar lastBar = GetLatestBars(asset, 1)[0];
                output.Add(asset, lastBar.Price);
            }

            output.Add("Free cash", 1);

            return output;
        }

        #endregion 

    }
}
