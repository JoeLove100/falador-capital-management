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
        public StrategyBuyAndHold(IDataHandler dataHandler, EventStack events)
        {
            _handler = dataHandler;
            _allAssets = _handler.AllAssets;
            _boughtAssets = _allAssets.ToDictionary(v => v, v => false);
            _events = events;

            Name = @"buy and hold strategy";
            TypeOfStrategy = StrategyType.BuyAndHold;
        }

        #endregion

        #region properties

        protected Dictionary<string, bool> _boughtAssets { get; set; }
        protected List<string> _allAssets { get; }
        protected IDataHandler _handler { get; }
        protected EventStack _events { get; set; }

        public string Name { get; }
        public StrategyType TypeOfStrategy {get;}

        #endregion

        #region methods

        public void GenerateSignals(IEvent ev)
        {
            if (ev.Type != EventType.MarketEvent) return;

            foreach(string asset in _allAssets)
            {
                if (_boughtAssets[asset]) continue;

                Bar[] bars = _handler.GetLatestBars(asset);
                if (bars == null || bars.Length == 0) continue;

                SignalEvent signal = new SignalEvent(DateTime.Now, asset,
                    SignalDirection.Long, 100m); 

                _boughtAssets[asset] = true;

                _events.PutEvent(signal);
            }
            
        }

        #endregion 
    }
}
