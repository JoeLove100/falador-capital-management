using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Tests
{
    [TestClass()]
    public class MarketData_Test
    {
        [TestMethod()]
        public void MarketData_NextDate_YieldCorrectDates()
        {
            List<DateTime> dateRangeOne = new List<DateTime>()
            {
                new DateTime(2018, 1, 1),
                new DateTime(2018, 1, 3)
            };
            List<DateTime> dateRangeTwo = new List<DateTime>()
            {
                new DateTime(2018, 1, 2)
            };

            List<Bar> barsOne = new List<Bar>() { new Bar(1), new Bar(3) };
            List<Bar> barsTwo = new List<Bar>() { new Bar(2) };

            AssetDataSeries dataSeriesOne = new AssetDataSeries(dateRangeOne, barsOne,
                "one");
            MarketData marketData = new MarketData(new AssetDataSeriesCollection()
            { dataSeriesOne});
            AssetDataSeries seriesTwo = new AssetDataSeries(dateRangeTwo, barsTwo,
                "two");

            //act

            IEnumerator<DateTime> dateGenerator = marketData.GetNextDate();
            dateGenerator.MoveNext();
            dateGenerator.MoveNext();
            DateTime date = dateGenerator.Current;

            //assert 
            Assert.AreEqual(date, new DateTime(2018, 1, 3));
        }
    }
}
