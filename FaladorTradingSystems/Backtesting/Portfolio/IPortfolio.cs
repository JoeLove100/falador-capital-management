using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Backtesting.Events;

namespace FaladorTradingSystems.Backtesting.Portfolio
{
    /// <summary>
    /// basic portfolio contract governing
    /// the portfolios interaction with 
    /// strategies
    /// </summary>
    public interface IPortfolio
    {
        void GetTradesForSignal(SignalEvent singalEvent);
        void UpdateHoldingsForFill(FillEvent fillEvent);
        void UpdateForMarketData(MarketEvent marketEvent);

        SortedList<DateTime, decimal> GetReturnSeries();
    }
}
