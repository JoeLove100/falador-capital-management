using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using FaladorTradingSystems.Backtesting;
using FaladorTradingSystems.Backtesting.DataHandling;
using FaladorTradingSystems.Backtesting.Execution;
using FaladorTradingSystems.Backtesting.Portfolio;
using FaladorTradingSystems.Backtesting.Events;

namespace FaladorTradingSystems
{
    public class ModelEngine
    {
        #region constructor

        public ModelEngine()
        {
            MarketData = LoadMarketData();
            BacktestingEngine = new BacktestingEngine(new EventStack(), MarketData);
        }

        #endregion

        #region properties

        public MarketData MarketData { get; set; }
        public BacktestingEngine BacktestingEngine { get; }

        #endregion

        #region methods

        protected MarketData LoadMarketData()
        {
            MarketData data 
                = AssetPriceDataLoader.LoadData(ProjectParameters.DataFileLocation);
            return data;
        }


        #endregion
    }
}
