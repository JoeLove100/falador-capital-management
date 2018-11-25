using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace FaladorTradingSystems.Backtesting.DataHandling
{
    /// <summary>
    /// abstract class dictating basic functionality
    /// for propagating market data to other objects
    /// in the model
    /// </summary>
    
    public interface IDataHandler
    {
        #region properties

        DateTime CurrentDate { get; set; }
        List<string> AllAssets { get; set; }
        bool ContinueBacktest { get; set; }

        #endregion 

        #region methods

        Bar[] GetLatestBars(string ticker, int n = 1);

        void UpdateBars();

        Dictionary<string, decimal> GetLastPrices();
        
        #endregion 
    }
}
