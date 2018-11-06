using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Events;

namespace FaladorTradingSystems.Portfolio
{
    /// <summary>
    /// basic portfolio contract governing
    /// the portfolios interaction with 
    /// strategies
    /// </summary>
    public interface IPortfolio
    {
        void UpdateForSignals(SignalEvent singalEvent);
        void UpdateHoldingsForFill(FillEvent fillEvent);
    }
}
