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
    public class AssetDataSeries_Test
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetSubset_FirstDateTooEarly_RaiseException()
        {
            //Arrange
            DateTime dateStart = new DateTime(2017, 10, 31);
            DateTime dateEnd = new DateTime(2018, 3, 31);

            DateRange rangeLow = new DateRange(dateStart, dateEnd);

            DateTime[] dates = new DateTime[6];
            double[] values = new double[] { 1, 2, 3, 4, 5, 6 };
            Bar[] bars = new Bar[6];

            for (int i = 1; i < 7; i++)
            {
                dates[i - 1] = new DateTime(2018, i, 15);
                bars[i - 1] = new Bar(values[i-1]);
            }

            AssetDataSeries series = new AssetDataSeries(dates, bars, "test");

            //act
            series.GetSubset(rangeLow);

            ///<summary>
            /// check exception raised if the at least one date is before the start of 
            /// the asset price series
            ///</summary>

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetSubset_DateTooLate_RaiseException()
        {
            //Arrange
            DateTime dateStart = new DateTime(2018, 3, 31);
            DateTime dateEnd = new DateTime(2018, 7, 31);

            DateRange rangeHigh = new DateRange(dateStart, dateEnd);

            DateTime[] dates = new DateTime[6];
            double[] values = new double[] { 1, 2, 3, 4, 5, 6 };
            Bar[] bars = new Bar[6];

            for (int i = 1; i < 7; i++)
            {
                dates[i - 1] = new DateTime(2018, i, 15);
                bars[i - 1] = new Bar(values[i-1]);
            }

            AssetDataSeries series = new AssetDataSeries(dates, bars, "test");

            //act
            series.GetSubset(rangeHigh);

            ///<summary>
            /// check exception raised if the at least one date is after the end of 
            /// the asset price series
            ///</summary>

        }

        [TestMethod()]
        public void GetSubset_CorrectRange_ReturnSubset()
        {
            //Arrange
            DateTime dateStart = new DateTime(2018, 2, 1);
            DateTime dateEnd = new DateTime(2018, 5, 31);

            DateRange range = new DateRange(dateStart, dateEnd);

            DateTime[] dates = new DateTime[6];
            double[] values = new double[] { 1, 2, 3, 4, 5, 6 };
            Bar[] bars = new Bar[6];

            for (int i = 1; i < 7; i++)
            {
                dates[i - 1] = new DateTime(2018, i, 15);
                bars[i - 1] = new Bar(values[i-1]);
            }

            AssetDataSeries series = new AssetDataSeries(dates, bars, "test");

            AssetDataSeries expectedSubset = new AssetDataSeries("test");

            for (int i = 1; i < 5; i++)
            {
                expectedSubset.Add(dates[i], bars[i]);
            }

            //act
            AssetDataSeries result = series.GetSubset(range);

            //assert
            Assert.IsTrue(result.IsSameAs(expectedSubset));

            ///<summary>
            /// check that the correct subset can returned for valid date ranges
            ///</summary>

        }

        [TestMethod()]
        public void CheckSame_NullValue_ReturnFalse()
        {
            //arrange
            AssetDataSeries series = new AssetDataSeries("test");

            //act 
            bool isSame = series.IsSameAs(null);

            //assert
            Assert.IsFalse(isSame);
        }

        [TestMethod()]
        public void CheckSame_DifferentType_ReturnFalse()
        {
            //arrange
            AssetDataSeries series = new AssetDataSeries("test");

            //act
            bool isSame = series.IsSameAs(new TestIsSameAs());

            //assert 
            Assert.IsFalse(isSame);

        }

        [TestMethod()]
        public void CheckSame_DifferentName_ReturnFalse()
        {
            //arrange 
            AssetDataSeries series = new AssetDataSeries("test_1");
            AssetDataSeries series2 = new AssetDataSeries("test_2");

            //act
            bool isSame = series.IsSameAs(series2);

            //assert
            Assert.IsFalse(isSame);
        }

        [TestMethod()]
        public void CheckSame_DifferentSeries_ReturnFalse()
        {
            //arrange
            AssetDataSeries series1 = new AssetDataSeries("test_1");
            series1.Add(new DateTime(2018, 1, 1), new Bar(10));
            series1.Add(new DateTime(2018, 1, 2), new Bar(11));

            AssetDataSeries series2 = new AssetDataSeries("test_2");
            series2.Add(new DateTime(2018, 1, 1), new Bar(10));
            series2.Add(new DateTime(2018, 1, 20), new Bar(11));

            //act
            bool isSame = series1.IsSameAs(series2);

            //assert
            Assert.IsFalse(isSame);

        }

        [TestMethod()]
        public void CheckSame_SameObject_ReturnTrue()
        {
            //arrange
            AssetDataSeries series1 = new AssetDataSeries("test")
            {
                { new DateTime(2018, 1, 1), new Bar(10) }
            };

            AssetDataSeries series2 = new AssetDataSeries("test")
            {
                { new DateTime(2018, 1, 1), new Bar(10) }
            };

            //act
            bool isSame = series1.IsSameAs(series2);

            //assert
            Assert.IsTrue(isSame);
        }

        [TestMethod()]
        public void GetEntriesInOrder_GetEntries_ReturnsOrderedByDate()
        {
            //arrange
            AssetDataSeries series1 = new AssetDataSeries("test")
            {
                {new DateTime(2018, 1, 1), new Bar(10) },
                {new DateTime(2017, 2, 1), new Bar(11) },
                {new DateTime(2017, 5, 1), new Bar(12) }
            };

            List<double> expectedOrderedPrices = new List<double> { 11, 12, 10 };

            //act
            List<double> orderedEntries = series1.GetPricesInOrder();

            //assert
            CollectionAssert.AreEqual(orderedEntries, expectedOrderedPrices);
        }
    }
}
