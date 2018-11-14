using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Backtesting.Events;

namespace FaladorTradingSystems.Backtesting.Execution
{
    /// <summary>
    /// Basic execution handler
    /// which simply fills orders at
    /// zero cost at the previous 
    /// close price
    /// </summary>
    class NaiveExecutionHandler : IExecutionHandler
    {

        #region constructor

        public NaiveExecutionHandler(EventStack eventStack)
        {
            _eventStack = eventStack;
        }

        #endregion

        #region properties

        EventStack _eventStack { get; set; }

        public void GetFillEventForTrade(TradeEvent tradeEvent)
        {
            ///<summary>
            ///Naive fill assumed to be on 
            ///grand exchange with 0 slippage
            ///</summary>
            FillEvent fillEvent = new FillEvent(DateTime.Now, tradeEvent.OrderType,
                tradeEvent.Ticker, tradeEvent.Quantity, Exchange.GrandExchange, 0);
            _eventStack.PutEvent(fillEvent);
        }

        #endregion


    }
}
