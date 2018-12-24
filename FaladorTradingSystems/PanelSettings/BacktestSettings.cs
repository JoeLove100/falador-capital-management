using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using FaladorTradingSystems.Backtesting.Strategies;

namespace FaladorTradingSystems.PanelSettings
{
    public class BacktestSettings : IIsSameAs
    {

        #region constructors

        public BacktestSettings(DateRange period,
                                StrategyType strategy,
                                List<string> assetUniverse)
        {
            Period = period;
            Strategy = strategy;
            AssetUniverse = assetUniverse;
        }

        #endregion 

        #region properties

        public DateRange Period { get; set; }
        public StrategyType Strategy { get; set; }
        public List<string> AssetUniverse { get; set; }

        #endregion

        #region methods

        public bool IsSameAs(IIsSameAs comparison)
        {
            BacktestSettings comparator = comparison as BacktestSettings;
            if (comparator is null) return false;

            if (comparator.Strategy != Strategy) return false;
            if (!comparator.Period.IsSameAs(Period)) return false;
            if (!Compare.AreSameAs(comparator.AssetUniverse,
                AssetUniverse)) return false;

            return true;
        }


        public BacktestSettings Clone()
        {
            List<string> assetUniverse = new List<string>(AssetUniverse);
            DateRange period = Period.Clone();

            return new BacktestSettings(period,
                Strategy, assetUniverse);
        }

        #endregion
    }
}
