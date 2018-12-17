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

    public class AssetAllocation : Dictionary<string, decimal>
    {
        #region constructors

        public AssetAllocation(List<string> assets, decimal initialCapital)
        {
            foreach (string asset in assets)
            {
                this.Add(asset, 0);
            }

            FreeCash = initialCapital;

        }

        public AssetAllocation(List<string> assets, List<decimal> positions)
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

        #endregion

        #region properties

        public decimal FreeCash { get; set; }

        #endregion 

        #region methods

        public AssetAllocation Clone()
        {
            List<string> assets = new List<string>();
            List<decimal> values = new List<decimal>();

            foreach(KeyValuePair<string, decimal> kvp in this)
            {
                assets.Add(kvp.Key);
                values.Add(kvp.Value);
            }



            var assetAllocations = new  AssetAllocation(assets, values);

            assetAllocations.FreeCash = FreeCash;

            return assetAllocations;
        }

        #endregion 
    }
}
