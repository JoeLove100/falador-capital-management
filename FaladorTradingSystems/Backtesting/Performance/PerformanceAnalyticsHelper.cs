using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace FaladorTradingSystems.Backtesting.Performance
{
    /// <summary>
    /// static class for carrying out basic 
    /// performance analytics calcs on trading
    /// strategy results
    /// </summary>
    
    public static class PerformanceAnalyticsHelper
    {

        #region static constants

        private readonly static int dailyConst = 252;
        private readonly static double dailyLower = 0.9;
        private readonly static double dailyUpper = 1.1;

        private readonly static int weeklyConst = 52;
        private readonly static double weeklyLower = 6;
        private readonly static double weeklyUpper = 7;

        private readonly static int monthlyConst = 12;
        private readonly static double monthlyLower = 28;
        private readonly static double monthlyUpper = 32;

        private readonly static int yearlyConst = 1;
        private readonly static double yearlyLower = 355;
        private readonly static double yearlyUpper = 375;

        private readonly static double tol = 0.05;
        #endregion 

        public static decimal GetStandardDeviation(IList<decimal> data)
        {
            ///<summary>
            ///Compute the standard deviation of a set of 
            ///numbers using Welfords method
            ///</summary>

            double m = (double) data[0];
            double tempM;
            double s = 0;
            int count = 1;

            while (count < data.Count())
            {
                double x = (double) data[count];
                tempM = m;
                m = tempM + (x - tempM) / (count + 1);
                s = s + (x - m) * (x - tempM);

                count++;
            }

            decimal sd =  (decimal) Math.Sqrt(s / (count - 1));
            return sd;
        }

        public static int GetAnnualisingConstant(IList<DateTime> dates)
        {
            ///<summary>
            ///function for converting the average differnce between dates
            ///in a time series to an annualising constant
            ///</summary>

            int[] dayDiffs = new int[dates.Count - 1];

            for(int i = 0; i < dates.Count-1; i++)
            {
                dayDiffs[i] = (int) (dates[i+1] - dates[i]).TotalDays;
            }

            double avgDifference = dayDiffs.Average();
            double tolUpper = 1 + tol;
            double tolLower = 1 - tol;

            if (avgDifference.IsBetween(dailyLower, dailyUpper)) return dailyConst;
            if (avgDifference.IsBetween(weeklyLower, weeklyUpper)) return weeklyConst;
            if (avgDifference.IsBetween(monthlyLower, monthlyUpper)) return monthlyConst;
            if (avgDifference.IsBetween(yearlyLower, yearlyUpper)) return yearlyConst;

            throw new ArgumentException("Unrecognized time series frequency");

        }

        public static decimal GetAnnualisedVol(SortedList<DateTime, decimal> returns)
        {
            ///<summary>
            ///returns the annualised volatililty of the NATURAL LOG of
            ///a time series of returns
            ///</summary>

            double [] logReturns = new double [returns.Count];

            for(int i = 0; i < returns.Count; i++)
            {
                logReturns[i] = Math.Log( (double) returns.Values[i]);
            }

            decimal vol = GetStandardDeviation(returns.Values);

            decimal annualisingConst = 
                GetAnnualisingConstant(returns.Keys.ToList());

            annualisingConst = (decimal)Math.Sqrt((double)annualisingConst);

            return vol * annualisingConst;


        }

        public static decimal GetDownsideDeviation(
            IList<decimal> returns, decimal threshold)
        {
            ///<summary>
            ///Get the vol of the returns below the 
            ///threshold, with returns above the threshold
            ///capped to the theshold level
            ///</summary>
            IList<decimal> cappedReturns = returns.Cap(threshold);
            decimal downVol = GetStandardDeviation(cappedReturns);

            return downVol;
        }

        public static decimal GetSharpeRatio(SortedList<DateTime, decimal> returns,
            decimal riskFreeRate=0)
        {
            ///<summary>
            ///Returns the sharpe ratio 
            ///Info: https://en.wikipedia.org/wiki/Sharpe_ratio
            ///</summary>

            decimal vol = GetAnnualisedVol(returns);
            decimal[] excessReturns = (decimal[]) returns.Values.Subtract(riskFreeRate);
            decimal avgExcess = excessReturns.Average();
            decimal scaling = GetAnnualisingConstant(returns.Keys);

            decimal sharpeRatio = avgExcess * scaling / vol;

            return sharpeRatio;
        }

        public static decimal GetSortinoRatio(SortedList<DateTime, decimal> returns,
            decimal threshold)
        {
            ///<summary>
            ///Returns the sortino ratio
            ///Info: https://en.wikipedia.org/wiki/Sortino_ratio
            ///</summary>

            decimal downVol = GetDownsideDeviation(returns.Values, threshold);
            decimal[] excessReturns = (decimal[])returns.Values.Subtract(threshold);
            decimal avgExcess = excessReturns.Average();

            decimal annualisingConstant = (decimal)
                Math.Sqrt( (double) GetAnnualisingConstant(returns.Keys));

            decimal sortinoRatio = avgExcess * annualisingConstant / downVol;
            return sortinoRatio;
        }


    }
}
