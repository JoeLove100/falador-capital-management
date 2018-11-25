using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Backtesting.Portfolio
{
    public class PortfolioValuation : Dictionary<string, decimal>
    {

        #region constructors

        public PortfolioValuation(List<string> assets, decimal initialCapital)
        {
            foreach(string asset in assets)
            {
                this.Add(asset, 0);
            }

            FreeCash = initialCapital;
            Commision = 0;
        }

        public PortfolioValuation(List<string> assets, 
            List<decimal> values,
            decimal freeCash,
            decimal commission)
        {
            if (assets.Count != values.Count)
            {
                throw new ArgumentException("must supply equal number of" +
                    "assets and allocations");
            }

            for (int i = 0; i < assets.Count; i++)
            {
                this.Add(assets[i], values[i]);
            }

            FreeCash = freeCash;
            Commision = commission;
        }

        #endregion

        #region properties

        public decimal FreeCash { get; set; }
        public decimal Commision { get; set; }

        #endregion

        #region methods

        public decimal GetTotal()
        {
            decimal output = 0;

            foreach(KeyValuePair<string, decimal> kvp in this)
            {
                output += kvp.Value;
            }

            output += FreeCash;
            output += Commision;

            return output;
        }

        public PortfolioValuation Clone()
        {
            List<string> assets = new List<string>();
            List<decimal> values = new List<decimal>();

            foreach(KeyValuePair<string, decimal> kvp in this)
            {
                assets.Add(kvp.Key);
                values.Add(kvp.Value);
            }

            return new PortfolioValuation(assets, values, FreeCash, Commision);
        }

        #endregion 
    }
}
