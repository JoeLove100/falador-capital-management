using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Events
{
    /// <summary>
    /// Event which represents a signal being generated
    /// by a trading strategy
    /// </summary>
    
    public class SignalEvent : IEvent
    {
        public SignalEvent(DateTime dateTimeGenerated, string ticker, SignalDirection direction)
        {
            Type = EventType.SignalEvent;
            DateTimeGenerated = dateTimeGenerated;
            Ticker = ticker;
            Direction = direction;
        }

        public EventType Type { get; }
        DateTime DateTimeGenerated { get; }
        string Ticker { get; }
        SignalDirection Direction { get; }

    }
}
