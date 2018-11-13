using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Backtesting.Portfolio
{
    /// <summary>
    /// describes the holdings in a 
    /// portfolio (ie number of securities/contracts
    /// as opposed to value thereof)
    /// </summary>

    public class AssetAllocation : Dictionary<string, double>
    {
        #region constructors

        public AssetAllocation(List<string> assets)
        {
            foreach (string asset in assets)
            {
                this.Add(asset, 0);
            }

        }

        public AssetAllocation(List<string> assets, List<double> positions)
        {
            if(assets.Count != positions.Count)
            {
                throw new ArgumentException("must supply equal number of" +
                    "assets and allocations");
            }

            for(int i = 0; i < assets.Count; i++)
            {
                this.Add(assets[i], positions[i]);
            }
        }

        public AssetAllocation Clone()
        {
            List<string> assets = new List<string>();
            List<double> values = new List<double>();

            foreach(KeyValuePair<string, double> kvp in this)
            {
                assets.Add(kvp.Key);
                values.Add(kvp.Value);
            }

            return new AssetAllocation(assets, values);
        }

        #endregion 
    }
}
