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
            List<double> testList = new List<double>() { 1, 2, 3, 4 };
            List<double> expectedList = new List<double>() { 1, 2, 3, 4 };

            double[] testArray = new double[] { 1.4, 2.4, 5.3, 1.1 };
            double[] expectedArray = new double[] { 1.4, 2.4, 5.3, 1.1 };

            //act
            List<double> subtractedList = (List<double>) testList.Subtract(0);
            double[] subtractedArray = (double[]) testArray.Subtract(0);

            //asssert
            CollectionAssert.AreEqual(subtractedList, expectedList);
            CollectionAssert.AreEqual(subtractedArray, expectedArray);
        }

        [TestMethod()]
        public void Subtract_SubtractPositive_TakeAway()
        {
            //arrange
            List<double> testList = new List<double>() { 1, 2, 3, 4 };
            List<double> expectedList = new List<double>() { -1, 0, 1, 2 };

            double[] testArray = new double[] { 1.4, 2.4, 5.3, 1.1 };
            double[] expectedArray = new double[] { -0.6, 0.4, 3.3, -0.9 };

            //act
            List<double> subtractedList = (List<double>) testList.Subtract(2);
            double[] subtractedArray = (double[]) testArray.Subtract(2);

            //asssert
            Assert.IsTrue(subtractedList.IsAlmostEqual(expectedList));
            Assert.IsTrue(subtractedArray.IsAlmostEqual(expectedArray));
        }

        [TestMethod()]
        public void Subtract_SubtractNegative_AddOn()
        {
            //arrange
            List<double> testList = new List<double>() { 1, 2, 3, 4 };
            List<double> expectedList = new List<double>() { 2.5, 3.5, 4.5, 5.5 };

            double[] testArray = new double[] { 1.4, 2.4, 5.3, 1.1 };
            double[] expectedArray = new double[] { 2.9, 3.9, 6.8, 2.6 };

            //act
            List<double> subtractedList = (List<double>) testList.Subtract(-1.5);
            double[] subtractedArray = (double[]) testArray.Subtract(-1.5);

            //asssert
            Assert.IsTrue(subtractedList.IsAlmostEqual(expectedList));
            Assert.IsTrue(subtractedArray.IsAlmostEqual(expectedArray));
        }

        [TestMethod()]
        public void Cap_Positive_CapsValues()
        {
            //arrange
            List<double> testList = new List<double>() { 1, 2, 3, 4 };
            List<double> expectedList = new List<double>() { 1, 2, 3, 3 };

            double[] testArray = new double[] { 1.4, 2.4, 5.3, 1.1 };
            double[] expectedArray = new double[] { 1.4, 2.1, 2.1, 1.1 };

            //act
            List<double> cappedList = (List<double>) testList.Cap(3);
            double[] cappedArray = (double[]) testArray.Cap(2.1);

            //asssert
            Assert.IsTrue(cappedList.IsAlmostEqual(expectedList));
            Assert.IsTrue(cappedArray.IsAlmostEqual(expectedArray));
        }

        [TestMethod()]
        public void Cap_Negative_CapsValues()
        {
            //arrange
            List<double> testList = new List<double>() { -1, -2, -3, -4 };
            List<double> expectedList = new List<double>() { -2, -2, -3, -4 };

            double[] testArray = new double[] { -1.4, -2.4, -5.3, -1.1 };
            double[] expectedArray = new double[] {-1.4, -2.4, -5.3, -1.1 };

            //act
            List<double> cappedList = (List<double>) testList.Cap(-2);
            double[] cappedArray = (double[]) testArray.Cap(-1);

            //asssert
            Assert.IsTrue(cappedList.IsAlmostEqual(expectedList));
            Assert.IsTrue(cappedArray.IsAlmostEqual(expectedArray));
        }
    }
}
