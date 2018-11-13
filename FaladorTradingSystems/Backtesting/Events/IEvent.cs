using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Backtesting.Events
{
    /// <summary>
    /// Basic interface for all event types
    /// </summary>
    public interface IEvent
    {
        EventType Type { get; }
    }
}
