namespace ZKosior.TreeBuilder.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class KnaryTreeTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void OutputsPostOrderStoredLargeNumber1()
        {
            var tree = new KnaryTree(2);
            tree.StoreNumber(1000000);
            Assert.AreEqual("00000010010000101111", tree.OutputPostOrder());
        }

        [TestMethod]
        public void OutputsPostOrderStoredLargeNumber2()
        {
            var tree = new KnaryTree(8);
            tree.StoreNumber(1000000);
            Assert.AreEqual("0011463", tree.OutputPostOrder());
        }

        [TestMethod]
        public void OutputsPostOrderStoredNumber()
        {
            var tree = new KnaryTree(2);
            tree.StoreNumber(7);
            Assert.AreEqual("111", tree.OutputPostOrder());
        }

        [TestMethod]
        public void OutputsPostOrderStoredNumbers1()
        {
            var tree = new KnaryTree(2);
            tree.StoreNumber(1);
            tree.StoreNumber(2);
            Assert.AreEqual("01", tree.OutputPostOrder());
        }

        [TestMethod]
        public void OutputsPostOrderStoredNumbers2()
        {
            var tree = new KnaryTree(4);
            tree.StoreNumber(7);
            tree.StoreNumber(43);
            tree.StoreNumber(2);
            Assert.AreEqual("31322", tree.OutputPostOrder());
        }

        #endregion
    }
}