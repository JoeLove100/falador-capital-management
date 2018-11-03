using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace FaladorTradingSystems.DataHandling
{
    /// <summary>
    /// abstract class dictating basic functionality
    /// for propagating market data to other objects
    /// in the model
    /// </summary>
    
    public abstract class DataHandler
    {
        #region properties

        public virtual DateTime CurrentDate { get; set; }
        public List<string> AllAssets { get; set; }

        #endregion 

        #region methods

        public virtual Bar[] GetLatestBars(string ticker, int n = 1)
        {
            ///<summary>
            ///gets the latest n bars, or fewer if less than
            ///n are available
            ///</summary>
            throw new NotImplementedException("data handler must provide" +
                "functionality for GetLatestBars");
        }

        public virtual void UpdateBars()
        {
            ///<summary>
            ///update to latest bars
            ///</summary>
            throw new NotImplementedException("data handler must provide" +
                "functionality for UpdateBars");
        }

        #endregion 
    }
}
