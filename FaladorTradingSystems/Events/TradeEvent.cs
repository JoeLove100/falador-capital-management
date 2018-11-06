using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Events
{
    public class TradeEvent : IEvent
    {
        #region constructor
        public TradeEvent(DateTime dateTimeGenerated, string ticker, OrderType direction,
            double quantity)
        {
            Type = EventType.TradeEvent;
            DateTimeGenerated = dateTimeGenerated;
            Ticker = ticker;
            Direction = direction;
            Quantity = quantity;
        }
        #endregion

        #region properties
        public EventType Type { get; }
        public DateTime DateTimeGenerated { get; }
        public string Ticker { get; }
        public OrderType Direction { get; }
        public double Quantity { get; }
        #endregion 
    }
}
