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

    public class IsSameAs_Test
    {
        [TestMethod()]
        public void CheckListsEqual_NullList_ReturnsFalse()
        {
            //arrange
            List<string> listOne = new List<string>();

            //act
            bool isSameFirst = Compare.AreSameAs(null, listOne);
            bool isSameSecond = Compare.AreSameAs(listOne, null);
            bool isSameBoth = Compare.AreSameAs(null, null);

            //assert

            Assert.IsFalse(isSameFirst);
            Assert.IsFalse(isSameSecond);
            Assert.IsFalse(isSameBoth);

            ///<summary>
            /// check that a null list always returns false
            ///</summary>

        }

        [TestMethod()]

        public void CheckListsEqual_DifferentLenghts_ReturnsFalse()
        {
            //arrange
            List<int> listOne = new List<int>() { 1, 2, 4, 5 };
            List<int> listTwo = new List<int>() { 1, 2, 2, 4, 5 };

            //act
            bool isSame = Compare.AreSameAs(listOne, listTwo);

            //assert
            Assert.IsFalse(isSame);

            ///<summary>
            ///check that different lenght lists always return false
            ///</summary>
        }

        [TestMethod()]

        public void CheckListsEqual_DifferentElements_ReturnsFalse()
        {
            //arrange
            List<int> listOne = new List<int> { 1, 2, 3, 4 };
            List<int> listTwo = new List<int> { 1, 2, 3, 5 };

            //act
            bool isSame = Compare.AreSameAs(listOne, listTwo);

            //assert
            Assert.IsFalse(isSame);

            ///<summary>
            ///check that lists with different elements will return false
            ///</summary>
        }

        [TestMethod()]
        public void CheckListsEqual_DifferentOrder_ReturnFalse()
        {
            //arrange
            List<string> list = new List<string>() { "a", "b", "c" };
            List<string> listPermuted = new List<string>() { "a", "c", "b" };

            //act
            bool isSame = Compare.AreSameAs(list, listPermuted);

            //assert
            Assert.IsFalse(isSame);

            ///<summary>
            ///check that lists with the same elements but different orders will
            ///evaluate to false
            ///</summary>
        }

        [TestMethod()]

        public void CheckListsEqual_DifferentArrays_ReturnFalse()
        {

            //arrange
            float[] arr1 = new float[4] { 1, 2, 3, 4 };
            float[] arr2 = new float[4] { 1, 2, 3, 5 };

            //act 
            bool isSame = Compare.AreSameAs(arr1, arr2);

            //assert
            Assert.IsFalse(isSame);

            ///<sumary>
            ///check that comparison evaluates correctly for arrays 
            ///also
            ///</sumary>
        }

        [TestMethod()]
        public void CheckListsEqual_SameList_ReturnTrue()
        {
            //arrange
            List<double> list = new List<double>() { 1, 5, 10, 25, 2.5, 0.255 };

            //act
            bool isSame = Compare.AreSameAs(list, list);

            //arrange
            Assert.IsTrue(isSame);

            ///<summary>
            ///check that method returns true when inputs are in fact the same
            ///</summary>
        }

        [TestMethod()]
        public void CheckDictEqual_NullInput_ReturnsFalse()
        {
            //arrange
            Dictionary<string, string> dictOne = new Dictionary<string, string>();

            //act
            bool isSameFirst = Compare.AreSameAs(null, dictOne);
            bool isSameSecond = Compare.AreSameAs(dictOne, null);
            bool isSameBoth = Compare.AreSameAs(null, null);

            //assert

            Assert.IsFalse(isSameFirst);
            Assert.IsFalse(isSameSecond);
            Assert.IsFalse(isSameBoth);

            ///<summary>
            /// check that a null value always returns false
            ///</summary>
        }

        [TestMethod()]

        public void CheckDictEqual_DifferentLenghts_ReturnsFalse()
        {
            //arrange
            Dictionary<string, float> dictOne = new Dictionary<string, float>()
            { {"a", 2 },{"b", 3 }, {"c", 4 } };
            Dictionary<string, float> dictTwo = new Dictionary<string, float>()
            { {"a", 2 },{"b", 3 } }; 

            //act
            bool isSame = Compare.AreSameAs(dictOne, dictTwo);

            //assert
            Assert.IsFalse(isSame);

            ///<summary>
            ///check that different lenght dictionaries always return false
            ///</summary>
        }

        [TestMethod()]
        public void CheckDictEqual_DifferentKey_ReturnFalse()
        {
            //arrange
            Dictionary<string, float> dictOne = new Dictionary<string, float>()
            { {"a", 2 }};
            Dictionary<string, float> dictTwo = new Dictionary<string, float>()
            { {"b", 2 }};

            //act
            bool isSame = Compare.AreSameAs(dictOne, dictTwo);

            //assert
            Assert.IsFalse(isSame);

            ///<summary>
            ///check that dictionaries with different keys will fail
            ///</summary>
        }

        [TestMethod()]
        public void CheckDictEqual_DifferentValue_ReturnFalse()
        {
            //arrange
            Dictionary<string, float> dictOne = new Dictionary<string, float>()
            { {"a", 0 }};
            Dictionary<string, float> dictTwo = new Dictionary<string, float>()
            { {"a", 1 }};

            //act
            bool isSame = Compare.AreSameAs(dictOne, dictTwo);

            //assert
            Assert.IsFalse(isSame);

            ///<summary>
            ///check that dictionaries with different values for the same key will fail
            ///</summary>
        }

        [TestMethod()]
        public void CheckDictEqual_SameDict_ReturnTrue()
        {
            //arrange
            Dictionary<string, string> dictOne = new Dictionary<string, string>
            { {"a", "test"}, {"b", "test2"}};
            Dictionary<string, string> dictTwo = new Dictionary<string, string>
            { {"b", "test2"}, {"a", "test"}};

            //act
            bool isSame = Compare.AreSameAs(dictOne, dictTwo);

            //assert
            Assert.IsTrue(isSame);

            ///<summary>
            ///check that the the method returns true if the inputs are the same,
            ///noting that dictionaries have no concept of "order"
            ///</summary>
        }


    }
}
