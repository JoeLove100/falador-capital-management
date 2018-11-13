using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using FaladorTradingSystems.Backtesting.Events;
using FaladorTradingSystems.Backtesting.DataHandling;

namespace FaladorTradingSystems.Backtesting.Strategies
{
    /// <summary>
    /// Basic strategy which goes LONG an asset as soon
    /// as the corresponding bar is received - primarily a
    /// test/benchmarking strategy
    /// </summary>
    
    public class StrategyBuyAndHold : IStrategy
    {
        #region constructors
        public StrategyBuyAndHold(IDataHandler dataHandler)
        {
            _boughtAssets = dataHandler.AllAssets.ToDictionary(v => v, v => false);
        }

        #endregion

        #region properties

        protected Dictionary<string, bool> _boughtAssets { get; set; }
        protected IDataHandler _handler { get; }
        public SortedList<DateTime, SignalEvent> Signals { get; set; }

        #endregion

        #region methods

        public void GenerateSignals(IEvent ev)
        {
            if (ev.Type != EventType.MarktetEvent) return;

            foreach(string asset in _boughtAssets.Keys)
            {
                if (_boughtAssets[asset]) continue;

                Bar[] bars = _handler.GetLatestBars(asset);
                if (bars == null || bars.Length == 0) continue;

                SignalEvent signal = new SignalEvent(DateTime.Now, asset,
                    SignalDirection.Long, 0); //need to update the strength

                _boughtAssets[asset] = true;

                Signals.Add(signal.DateTimeGenerated, signal);
            }
            
        }

        #endregion 
    }
}
