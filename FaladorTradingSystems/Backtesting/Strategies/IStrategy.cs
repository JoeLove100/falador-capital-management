using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Backtesting.Events;

namespace FaladorTradingSystems.Backtesting.Strategies
{
    /// <summary>
    /// Base class for defining strategies - has
    /// as single method to generate trading signals
    /// to be consumed by the main engine
    /// </summary>
    
    public interface IStrategy
    {
        void GenerateSignals(IEvent ev);
        SortedList<DateTime, double> GetPerformanceHistory();
    }
}
