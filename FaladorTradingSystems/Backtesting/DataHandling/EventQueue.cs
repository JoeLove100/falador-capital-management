using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Events;

namespace FaladorTradingSystems.Backtesting.DataHandling
{
    public class EventQueue : SortedDictionary<DateTime, IEvent>
    {

    }
}
