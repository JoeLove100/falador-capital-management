using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Backtesting.Events
{
    public class TradeEvent : IEvent
    {
        #region constructor
        public TradeEvent(DateTime dateTimeGenerated, string ticker, OrderType orderType,
            decimal quantity)
        {
            Type = EventType.TradeEvent;
            DateTimeGenerated = dateTimeGenerated;
            Ticker = ticker;
            OrderType = orderType;
            Quantity = quantity;
        }
        #endregion

        #region properties
        public EventType Type { get; }
        public DateTime DateTimeGenerated { get; }
        public string Ticker { get; }
        public OrderType OrderType { get; }
        public decimal Quantity { get; }
        #endregion 
    }
}
