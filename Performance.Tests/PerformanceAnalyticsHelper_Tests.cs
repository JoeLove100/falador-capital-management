using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Performance;
using Utils;

namespace Performance.Tests
{
    [TestClass()]

    public class PerformanceAnalyticsHelper_Tests
    {
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
            List<double> numbers = new List<double> { 1, 1.3,
            3.4, 2.3, 1.6, 3.4, 0.8, 0.7, 0.1, -1, 3.5, 2.1,
            2.7};

            //act
            double sd =
                PerformanceAnalyticsHelper.GetStandardDeviation(numbers);

            //assert
            Assert.AreEqual(1.380124, sd, 1e-5);
        }

        [TestMethod()]
        public void GetDownsideDeviation_GetDD_ReturnsDD()
        {
            //arrange
            List<double> numbers = new List<double> { 1, 1.3,
            3.4, 2.3, 1.6, 3.4, 0.8, 0.7, 0.1, -1, 3.5, 2.1,
            2.7};

            //act
            double dd =
                PerformanceAnalyticsHelper.GetDownsideDeviation(numbers, 2);

            //assert
            Assert.AreEqual(0.933081, dd, 1e-5);
        }

        [TestMethod()]
        public void GetAnnualisedVol_MonthlySeries_ReturnsVol()
        {
            //arrange
            DateTime startDate = new DateTime(2017, 1, 31);
            DateTime endDate = new DateTime(2018, 1, 31);
            List<DateTime> dates = 
                (List<DateTime>) DateRange.GetMonthEndsBetween(startDate, endDate);

            List<double> numbers = new List<double> { 1, 1.3,
            3.4, 2.3, 1.6, 3.4, 0.8, 0.7, 0.1, -1, 3.5, 2.1,
            2.7};

            SortedList<DateTime, double> returnSeries = new SortedList<DateTime, double>();

            for(int i = 0; i < dates.Count(); i++)
            {
                returnSeries.Add(dates[i], numbers[i]);
            }

            //act
            double annualVol = PerformanceAnalyticsHelper.GetAnnualisedVol(returnSeries);

            //assert
            Assert.AreEqual(4.780891, annualVol, 1e-5);
        }

        [TestMethod()]
        public void GetAnnualisedVol_DailySeries_ReturnsVol()
        {
            //arrange
            DateTime startDate = new DateTime(2018, 5, 1);
            DateTime endDate = new DateTime(2018, 5, 13);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetDaysBetween(startDate, endDate);

            List<double> numbers = new List<double> { 1, 1.3,
            3.4, 2.3, 1.6, 3.4, 0.8, 0.7, 0.1, -1, 3.5, 2.1,
            2.7};

            SortedList<DateTime, double> returnSeries = new SortedList<DateTime, double>();

            for (int i = 0; i < dates.Count(); i++)
            {
                returnSeries.Add(dates[i], numbers[i]);
            }

            //act
            double annualVol = PerformanceAnalyticsHelper.GetAnnualisedVol(returnSeries);

            //assert
            Assert.AreEqual(21.90880, annualVol, 1e-5);
        }
        
        [TestMethod()]
        public void GetSharpeRatio_MonthlySeries_ReturnSR()
        {
            //arrange
            DateTime startDate = new DateTime(2017, 1, 31);
            DateTime endDate = new DateTime(2018, 1, 31);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetMonthEndsBetween(startDate, endDate);

            List<double> numbers = new List<double> { 1, 1.3,
            3.4, 2.3, 1.6, 3.4, 0.8, 0.7, 0.1, -1, 3.5, 2.1,
            2.7};

            SortedList<DateTime, double> returnSeries = new SortedList<DateTime, double>();

            for (int i = 0; i < dates.Count(); i++)
            {
                returnSeries.Add(dates[i], numbers[i]);
            }

            //act
            double sharpe =
                PerformanceAnalyticsHelper.GetSharpeRatio(returnSeries, 1);

            //assert
            Assert.AreEqual(1.718379, sharpe, 1e-5);
        }

        [TestMethod()]
        public void GetSortinoRatio_MonthlySeries_ReturnSR()
        {
            //arrange
            DateTime startDate = new DateTime(2017, 1, 31);
            DateTime endDate = new DateTime(2018, 1, 31);
            List<DateTime> dates =
                (List<DateTime>)DateRange.GetMonthEndsBetween(startDate, endDate);

            List<double> numbers = new List<double> { 1, 1.3,
            3.4, 2.3, 1.6, 3.4, 0.8, 0.7, 0.1, -1, 3.5, 2.1,
            2.7};

            SortedList<DateTime, double> returnSeries = new SortedList<DateTime, double>();

            for (int i = 0; i < dates.Count(); i++)
            {
                returnSeries.Add(dates[i], numbers[i]);
            }

            //act
            double sortino =
                PerformanceAnalyticsHelper.GetSortinoRatio(returnSeries, 2);

            //assert
            Assert.AreEqual(-1.17088, sortino, 1e-5);
        }




    }
}
