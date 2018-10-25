using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Events
{
    /// <summary>
    /// enum describing the four types of events considered
    /// in the model:
    /// - market events for receiving market data
    /// - signal events for generating trade signal
    /// - trade event for issuing order to trade
    /// - fill event for trade being completed
    /// </summary>
    public enum EventType
    {
        MarktetEvent,
        SignalEvent,
        TradeEvent,
        FillEvent
    }
}
