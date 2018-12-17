using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Backtesting.Performance;
using Utils;

namespace Performance.Tests
{
    [TestClass()]
    public class PerformanceAnalyticsHelper_Tests
    {
        private static readonly decimal tol = 1e-5m;

        [TestMethod()]
        public void GetAnnualisingConstant_Daily_Returns252()
        {
            //arrange 
            DateTime startDate = new DateTime(2018, 1, 1);
            DateTime endDate = new DateTime(2018, 5, 31);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetDaysBetween(startDate, endDate);

            //act
            int constant = PerformanceAnalyticsHelper.GetAnnualisingConstant(dates);

            //assert
            Assert.AreEqual(252, constant);
        }

        [TestMethod()]
        public void GetAnnualisingConstant_Weekly_Returns52()
        {
            //arrange 
            DateTime startDate = new DateTime(2018, 1, 1);
            DateTime endDate = new DateTime(2018, 5, 31);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetDaysBetweenWeekly(startDate, endDate);

            //act
            int constant = PerformanceAnalyticsHelper.GetAnnualisingConstant(dates);

            //assert
            Assert.AreEqual(52, constant);
        }

        [TestMethod()]
        public void GetAnnualisingConstant_Monthly_Returns12()
        {
            //arrange 
            DateTime startDate = new DateTime(2017, 1, 1);
            DateTime endDate = new DateTime(2018, 5, 31);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetMonthEndsBetween(startDate, endDate);

            //act
            int constant = PerformanceAnalyticsHelper.GetAnnualisingConstant(dates);

            //assert
            Assert.AreEqual(12, constant);
        }

        [TestMethod()]
        public void GetAnnualisingConstant_Yearly_Returns1()
        {
            //arrange 
            DateTime startDate = new DateTime(2012, 1, 1);
            DateTime endDate = new DateTime(2018, 5, 31);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetYearEndsBetween(startDate, endDate);

            //act
            int constant = PerformanceAnalyticsHelper.GetAnnualisingConstant(dates);

            //assert
            Assert.AreEqual(1, constant);
        }

        [TestMethod()]
        public void GetStandardDeviation_GetSD_ReturnsSD()
        {
            //arrange
            List<decimal> numbers = new List<decimal> { 1, 1.3m,
            3.4m, 2.3m, 1.6m, 3.4m, 0.8m, 0.7m, 0.1m, -1, 3.5m, 2.1m,
            2.7m};

            //act
            decimal sd =
                PerformanceAnalyticsHelper.GetStandardDeviation(numbers);

            //assert
            Assert.IsTrue(Math.Abs(1.380124m - sd) < tol);
        }

        [TestMethod()]
        public void GetDownsideDeviation_GetDD_ReturnsDD()
        {
            //arrange
            List<decimal> numbers = new List<decimal> { 1, 1.3m,
            3.4m, 2.3m, 1.6m, 3.4m, 0.8m, 0.7m, 0.1m, -1, 3.5m, 2.1m,
            2.7m};

            //act
            decimal dd =
                PerformanceAnalyticsHelper.GetDownsideDeviation(numbers, 2);

            //assert
            Assert.IsTrue(Math.Abs(0.933081m - dd) < tol);
        }

        [TestMethod()]
        public void GetAnnualisedVol_MonthlySeries_ReturnsVol()
        {
            //arrange
            DateTime startDate = new DateTime(2017, 1, 31);
            DateTime endDate = new DateTime(2018, 1, 31);
            List<DateTime> dates = 
                (List<DateTime>) DateRange.GetMonthEndsBetween(startDate, endDate);

            List<decimal> numbers = new List<decimal> { 1, 1.3m,
            3.4m, 2.3m, 1.6m, 3.4m, 0.8m, 0.7m, 0.1m, -1, 3.5m, 2.1m,
            2.7m};

            SortedList<DateTime, decimal> returnSeries = new SortedList<DateTime, decimal>();

            for(int i = 0; i < dates.Count(); i++)
            {
                returnSeries.Add(dates[i], numbers[i]);
            }

            //act
            decimal annualVol = PerformanceAnalyticsHelper.GetAnnualisedVol(returnSeries);

            //assert
            Assert.IsTrue(Math.Abs(4.780891m -  annualVol) <  tol);
        }

        [TestMethod()]
        public void GetAnnualisedVol_DailySeries_ReturnsVol()
        {
            //arrange
            DateTime startDate = new DateTime(2018, 5, 1);
            DateTime endDate = new DateTime(2018, 5, 13);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetDaysBetween(startDate, endDate);

            List<decimal> numbers = new List<decimal> { 1, 1.3m,
            3.4m, 2.3m, 1.6m, 3.4m, 0.8m, 0.7m, 0.1m, -1, 3.5m, 2.1m,
            2.7m};

            SortedList<DateTime, decimal> returnSeries = new SortedList<DateTime, decimal>();

            for (int i = 0; i < dates.Count(); i++)
            {
                returnSeries.Add(dates[i], numbers[i]);
            }

            //act
            decimal annualVol = PerformanceAnalyticsHelper.GetAnnualisedVol(returnSeries);

            //assert
            Assert.IsTrue(Math.Abs(21.90880m - annualVol) < tol);
        }
        
        [TestMethod()]
        public void GetSharpeRatio_MonthlySeries_ReturnSR()
        {
            //arrange
            DateTime startDate = new DateTime(2017, 1, 31);
            DateTime endDate = new DateTime(2018, 1, 31);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetMonthEndsBetween(startDate, endDate);

            List<decimal> numbers = new List<decimal> { 1, 1.3m,
            3.4m, 2.3m, 1.6m, 3.4m, 0.8m, 0.7m, 0.1m, -1, 3.5m, 2.1m,
            2.7m};

            SortedList<DateTime, decimal> returnSeries = new SortedList<DateTime, decimal>();

            for (int i = 0; i < dates.Count(); i++)
            {
                returnSeries.Add(dates[i], numbers[i]);
            }

            //act
            decimal sharpe =
                PerformanceAnalyticsHelper.GetSharpeRatio(returnSeries, 1);

            //assert
            Assert.IsTrue(Math.Abs(1.718379m - sharpe) < tol);
        }

        [TestMethod()]
        public void GetSortinoRatio_MonthlySeries_ReturnSR()
        {
            //arrange
            DateTime startDate = new DateTime(2017, 1, 31);
            DateTime endDate = new DateTime(2018, 1, 31);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetMonthEndsBetween(startDate, endDate);

            List<decimal> numbers = new List<decimal> { 1, 1.3m,
            3.4m, 2.3m, 1.6m, 3.4m, 0.8m, 0.7m, 0.1m, -1, 3.5m, 2.1m,
            2.7m};

            SortedList<DateTime, decimal> returnSeries = new SortedList<DateTime, decimal>();

            for (int i = 0; i < dates.Count(); i++)
            {
                returnSeries.Add(dates[i], numbers[i]);
            }

            //act
            decimal sortino =
                PerformanceAnalyticsHelper.GetSortinoRatio(returnSeries, 2);

            //assert
            Assert.IsTrue(Math.Abs(-1.17088m - sortino) <  tol);
        }




    }
}
