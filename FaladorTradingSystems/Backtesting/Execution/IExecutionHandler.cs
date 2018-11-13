using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Events;

namespace FaladorTradingSystems.Execution
{
    /// <summary>
    /// base class for execution handler - ie
    /// a class for simulating placing 
    /// trades in the market
    /// </summary>
    public interface IExecutionHandler
    {
        FillEvent GetFillEventForTrade(TradeEvent tradeEvent);
    }
}
