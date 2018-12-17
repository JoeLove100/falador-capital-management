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

    public class ExtenstionMethods_Test
    {
        [TestMethod()]
        public void Subtract_SubtractZero_NoChange()
        {
            //arrange
            List<decimal> testList = new List<decimal>() { 1, 2, 3, 4 };
            List<decimal> expectedList = new List<decimal>() { 1, 2, 3, 4 };

            decimal[] testArray = new decimal[] { 1.4m, 2.4m, 5.3m, 1.1m };
            decimal[] expectedArray = new decimal[] { 1.4m, 2.4m, 5.3m, 1.1m };

            //act
            List<decimal> subtractedList = (List<decimal>) testList.Subtract(0);
            decimal[] subtractedArray = (decimal[]) testArray.Subtract(0);

            //asssert
            CollectionAssert.AreEqual(subtractedList, expectedList);
            CollectionAssert.AreEqual(subtractedArray, expectedArray);
        }

        [TestMethod()]
        public void Subtract_SubtractPositive_TakeAway()
        {
            //arrange
            List<decimal> testList = new List<decimal>() { 1, 2, 3, 4 };
            List<decimal> expectedList = new List<decimal>() { -1, 0, 1, 2 };

            decimal[] testArray = new decimal[] { 1.4m, 2.4m, 5.3m, 1.1m };
            decimal[] expectedArray = new decimal[] { -0.6m, 0.4m, 3.3m, -0.9m };

            //act
            List<decimal> subtractedList = (List<decimal>) testList.Subtract(2);
            decimal[] subtractedArray = (decimal[]) testArray.Subtract(2);

            //asssert
            Assert.IsTrue(subtractedList.IsAlmostEqual(expectedList));
            Assert.IsTrue(subtractedArray.IsAlmostEqual(expectedArray));
        }

        [TestMethod()]
        public void Subtract_SubtractNegative_AddOn()
        {
            //arrange
            List<decimal> testList = new List<decimal>() { 1, 2, 3, 4 };
            List<decimal> expectedList = new List<decimal>() { 2.5m, 3.5m, 4.5m, 5.5m };

            decimal[] testArray = new decimal[] { 1.4m, 2.4m, 5.3m, 1.1m };
            decimal[] expectedArray = new decimal[] { 2.9m, 3.9m, 6.8m, 2.6m };

            //act
            List<decimal> subtractedList = (List<decimal>) testList.Subtract(-1.5m);
            decimal[] subtractedArray = (decimal[]) testArray.Subtract(-1.5m);

            //asssert
            Assert.IsTrue(subtractedList.IsAlmostEqual(expectedList));
            Assert.IsTrue(subtractedArray.IsAlmostEqual(expectedArray));
        }

        [TestMethod()]
        public void Cap_Positive_CapsValues()
        {
            //arrange
            List<decimal> testList = new List<decimal>() { 1, 2, 3, 4 };
            List<decimal> expectedList = new List<decimal>() { 1, 2, 3, 3 };

            decimal[] testArray = new decimal[] { 1.4m, 2.4m, 5.3m, 1.1m };
            decimal[] expectedArray = new decimal[] { 1.4m, 2.1m, 2.1m, 1.1m };

            //act
            List<decimal> cappedList = (List<decimal>) testList.Cap(3);
            decimal[] cappedArray = (decimal[]) testArray.Cap(2.1m);

            //asssert
            Assert.IsTrue(cappedList.IsAlmostEqual(expectedList));
            Assert.IsTrue(cappedArray.IsAlmostEqual(expectedArray));
        }

        [TestMethod()]
        public void Cap_Negative_CapsValues()
        {
            //arrange
            List<decimal> testList = new List<decimal>() { -1, -2, -3, -4 };
            List<decimal> expectedList = new List<decimal>() { -2, -2, -3, -4 };

            decimal[] testArray = new decimal[] { -1.4m, -2.4m, -5.3m, -1.1m };
            decimal[] expectedArray = new decimal[] {-1.4m, -2.4m, -5.3m, -1.1m };

            //act
            List<decimal> cappedList = (List<decimal>) testList.Cap(-2);
            decimal[] cappedArray = (decimal[]) testArray.Cap(-1);

            //asssert
            Assert.IsTrue(cappedList.IsAlmostEqual(expectedList));
            Assert.IsTrue(cappedArray.IsAlmostEqual(expectedArray));
        }

        [TestMethod()]
        public void PriceToReturn_Prices_YieldsCorrectReturns()
        {
            //arrange
            DateTime startDate = new DateTime(2018, 1, 1);
            DateTime endDate = new DateTime(2018, 1, 4);
            List<DateTime> dates = 
                (List<DateTime>) DateRange.GetDaysBetween(startDate, endDate);

            var testPrices = new SortedList<DateTime, decimal>();
            var expectedReturns = new SortedList<DateTime, decimal>();

            for(int i = 0; i < 4;  i++)
            {
                testPrices.Add(dates[i], (decimal) Math.Pow(1.02, i));
                expectedReturns.Add(dates[i], (decimal)Math.Pow(1.02, i) - 1);
            }

            //act 
            var result = testPrices.PriceToReturns();
            Assert.IsTrue(result.Values.IsAlmostEqual(expectedReturns.Values));
            //assert 


        }
    }
}
