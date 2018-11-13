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

        public static double GetStandardDeviation(IList<double> data)
        {
            ///<summary>
            ///Compute the standard deviation of a set of 
            ///numbers using Welfords method
            ///</summary>

            double m = data[0];
            double tempM;
            double s = 0;
            int count = 1;

            while (count < data.Count())
            {
                double x = data[count];
                tempM = m;
                m = tempM + (x - tempM) / (count + 1);
                s = s + (x - m) * (x - tempM);

                count++;
            }

            double sd =  Math.Sqrt(s / (count - 1));
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

        public static double GetAnnualisedVol(SortedList<DateTime, double> returns)
        {
            ///<summary>
            ///returns the annualised volatililty of the NATURAL LOG of
            ///a time series of returns
            ///</summary>

            double[] logReturns = new double[returns.Count];

            for(int i = 0; i < returns.Count; i++)
            {
                logReturns[i] = Math.Log(returns.Values[i]);
            }

            double vol = GetStandardDeviation(returns.Values);

            double annualisingConst = 
                GetAnnualisingConstant(returns.Keys.ToList());

            return vol * Math.Sqrt(annualisingConst);


        }

        public static double GetDownsideDeviation(
            IList<double> returns, double threshold)
        {
            ///<summary>
            ///Get the vol of the returns below the 
            ///threshold, with returns above the threshold
            ///capped to the theshold level
            ///</summary>
            IList<double> cappedReturns = returns.Cap(threshold);
            double downVol = GetStandardDeviation(cappedReturns);

            return downVol;
        }

        public static double GetSharpeRatio(SortedList<DateTime, double> returns,
            double riskFreeRate=0)
        {
            ///<summary>
            ///Returns the sharpe ratio 
            ///Info: https://en.wikipedia.org/wiki/Sharpe_ratio
            ///</summary>

            double vol = GetAnnualisedVol(returns);
            double[] excessReturns = (double[]) returns.Values.Subtract(riskFreeRate);
            double avgExcess = excessReturns.Average();
            double scaling = GetAnnualisingConstant(returns.Keys);

            double sharpeRatio = avgExcess * scaling / vol;

            return sharpeRatio;
        }

        public static double GetSortinoRatio(SortedList<DateTime, double> returns,
            double threshold)
        {
            ///<summary>
            ///Returns the sortino ratio
            ///Info: https://en.wikipedia.org/wiki/Sortino_ratio
            ///</summary>

            double downVol = GetDownsideDeviation(returns.Values, threshold);
            double[] excessReturns = (double[])returns.Values.Subtract(threshold);
            double avgExcess = excessReturns.Average();

            double annualisingConstant = 
                Math.Sqrt(GetAnnualisingConstant(returns.Keys));

            double sortinoRatio = avgExcess * annualisingConstant / downVol;
            return sortinoRatio;
        }


    }
}
