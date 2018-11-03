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
    public class Bar_Test
    {
        [TestMethod()]
        public void CheckSame_DifferentClass_ReturnFalse()
        {
            //arrange
            Bar bar = new Bar(1);
            TestIsSameAs dummy = new TestIsSameAs();

            //act 
            bool isSame = bar.IsSameAs(dummy);

            //assert
            Assert.IsFalse(isSame);
        }

        [TestMethod()]
        public void CheckSame_DifferentPrice_ReturnFalse()
        {
            //arrange
            Bar bar = new Bar(0.9);
            Bar barTwo = new Bar(1);

            //act 
            bool isSame = bar.IsSameAs(barTwo);

            //assert
            Assert.IsFalse(isSame);
        }

        [TestMethod()]
        public void CheckSame_AreSame_ReturnTrue()
        {
            //arrange
            Bar bar = new Bar(1);
            Bar barTwo = new Bar(1);

            //act
            bool isSame = bar.IsSameAs(barTwo);

            //assert
            Assert.IsTrue(isSame);
        }

    }
}
