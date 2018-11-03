using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace FaladorTradingSystems.DataHandling
{
    /// <summary>
    /// data handler to use market data from
    /// the rune wiki stored in a csv - useful for
    /// backtesting strategies
    /// </summary>
    
    public class HistoricSeriesDataHandler : DataHandler
    {
        #region constructor
        public HistoricSeriesDataHandler(MarketData marketData)
        {
            _marketData = marketData;
            _dateEnumerator = _marketData.GetNextDate();

        }
        #endregion

        #region properties
        private bool _continueBacktest { get; set; }
        private List<string> _symbolList { get; set; }
        private MarketData _marketData { get; set; }
        private IEnumerator<DateTime> _dateEnumerator { get; }

        public EventQueue Events { get; set; }
        #endregion 

        #region methods
        
        public override Bar[] GetLatestBars(string ticker, int n= 1)
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

        public override void UpdateBars()
        {
            _continueBacktest = _dateEnumerator.MoveNext();
        }

        #endregion 

    }
}
