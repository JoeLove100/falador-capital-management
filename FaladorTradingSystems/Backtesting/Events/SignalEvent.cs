using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Backtesting.Events
{
    /// <summary>
    /// Event which represents a signal being generated
    /// by a trading strategy
    /// </summary>
    
    public class SignalEvent : IEvent
    {
        public SignalEvent(DateTime dateTimeGenerated, 
            string ticker, 
            SignalDirection direction,
            decimal strength)
        {
            Type = EventType.SignalEvent;
            DateTimeGenerated = dateTimeGenerated;
            Ticker = ticker;
            Direction = direction;
            Strenth = strength;
        }

        public EventType Type { get; }
        public DateTime DateTimeGenerated { get; }
        public string Ticker { get; }
        public SignalDirection Direction { get; }
        public decimal Strenth { get; }

    }
}
