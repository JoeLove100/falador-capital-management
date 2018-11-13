using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Events
{
    /// <summary>
    /// Event triggered when an order is executed
    /// containing info on the execution
    /// </summary>
    
    public class FillEvent : IEvent
    {
        #region constructor
        public FillEvent(DateTime dateTimeFilled, OrderType direction, string ticker,
            double quantity, Exchange exchange, double transactionCost)
        {
            Type = EventType.FillEvent;
            DateTimeFilled = dateTimeFilled;
            OrderType = direction;
            Ticker = ticker;
            Quantity = quantity;
            Exchange = exchange;
            TransactionCost = transactionCost;
        }
        #endregion

        #region properties
        public EventType Type { get;}
        public DateTime DateTimeFilled { get; }
        public OrderType OrderType { get; }
        public string Ticker { get; }
        public double Quantity { get; }
        public Exchange Exchange { get; }
        public double TransactionCost { get; }
        #endregion 

    }

}
