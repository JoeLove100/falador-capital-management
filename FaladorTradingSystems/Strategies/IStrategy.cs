using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Events;

namespace FaladorTradingSystems.Strategies
{
    /// <summary>
    /// Base class for defining strategies - has
    /// as single method to generate trading signals
    /// to be consumed by the main engine
    /// </summary>
    
    public interface IStrategy
    {
        void GenerateSignals(IEvent ev);
    }
}
