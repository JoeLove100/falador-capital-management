using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Events;

namespace FaladorTradingSystems.Execution
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

        public NaiveExecutionHandler(SortedList<DateTime, IEvent> eventQueue)
        {
            _eventQueue = eventQueue;
        }

        #endregion

        #region properties

        SortedList<DateTime, IEvent> _eventQueue { get; set; }

        public FillEvent GetFillEventForTrade(TradeEvent tradeEvent)
        {
            ///<summary>
            ///Naive fill assumed to be on 
            ///grand exchange with 0 slippage
            ///</summary>
            FillEvent fillEvent = new FillEvent(DateTime.Now, tradeEvent.OrderType,
                tradeEvent.Ticker, tradeEvent.Quantity, Exchange.GrandExchange, 0);
            return fillEvent;
        }

        #endregion


    }
}
