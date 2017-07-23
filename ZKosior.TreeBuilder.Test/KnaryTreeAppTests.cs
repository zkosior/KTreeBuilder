namespace ZKosior.TreeBuilder.Test
{
    using System;
    using System.IO;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rhino.Mocks;

    [TestClass]
    public class KnaryTreeAppTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void CanHndleMultipleLines()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(4);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("4\n7\n43\n2\n0\n"));
            var result = new byte[5];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(4)).Return(knaryTree);
                knaryTree.StoreNumber(7);
                knaryTree.StoreNumber(43);
                knaryTree.StoreNumber(2);
                Expect.Call(knaryTree.OutputPostOrder()).Return("31322");
            }

            using (mocks.Playback())
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }

            Assert.AreEqual("31322", Encoding.ASCII.GetString(result));
        }

        [TestMethod]
        public void HandlesAndRethrowsExceptionWhenNegativeNumber()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("2\n-1\n0"));
            var result = new byte[3];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(1);
                LastCall.Throw(new OverflowException("exception"));
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            try
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ApplicationException));
                Assert.AreEqual("Please verify line number: 2. Numbers can not be negative.", e.Message);
            }
        }

        [TestMethod]
        public void IgnoresEmptyLinesAtInTheBeginning()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("2\n2\n\n0\n"));
            var result = new byte[2];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(2);
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            using (mocks.Playback())
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }

            Assert.AreEqual("01", Encoding.ASCII.GetString(result));
        }

        [TestMethod]
        public void IgnoresEmptyLinesAtInTheMiddle()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("2\n2\n\n0\n"));
            var result = new byte[2];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(2);
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            using (mocks.Playback())
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }

            Assert.AreEqual("01", Encoding.ASCII.GetString(result));
        }

        [TestMethod]
        public void IgnoresEmptyLinesAtTheEnd()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("2\n2\n0\n\n"));
            var result = new byte[2];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(2);
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            using (mocks.Playback())
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }

            Assert.AreEqual("01", Encoding.ASCII.GetString(result));
        }

        [TestMethod]
        public void ReadsDataAndWritesResult()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("2\n2\n0\n"));
            var result = new byte[2];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(2);
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            using (mocks.Playback())
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }

            Assert.AreEqual("01", Encoding.ASCII.GetString(result));
        }

        [TestMethod]
        public void WhenInputDoesntEndWithZero_ThrowsExceptionAndGivesLineNumberOfTheInvalidLine()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("2\n\n"));
            var result = new byte[3];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(1);
                LastCall.Throw(new FormatException("exception"));
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            try
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ApplicationException));
                Assert.AreEqual("Input file has to be ended with '0'", e.Message);
            }
        }

        [TestMethod]
        public void WhenInputDoesntStartWithCorrectOrderLevel_ThrowsExceptionAndGivesLineNumberOfTheInvalidLine()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("1\n\n"));
            var result = new byte[3];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(1);
                LastCall.Throw(new FormatException("exception"));
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            try
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ApplicationException));
                Assert.AreEqual(
                    "Input has to start with K-nary tree order number. Order must be higher than 1 or lower than 10. Please verify line number: 1", 
                    e.Message);
            }
        }

        [TestMethod]
        public void WhenInvalidCharacters_ThrowsExceptionGivesNumberOfFirstInvalidLine()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("2\na"));
            var result = new byte[3];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(1);
                LastCall.Throw(new FormatException("exception"));
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            try
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ApplicationException));
                Assert.AreEqual("Please verify line number: 2", e.Message);
            }
        }

        [TestMethod]
        public void WhenThrowingExceptionAfterEmptyLine_GivesLineNumberOfTheFirstInvalidLine()
        {
            var mocks = new MockRepository();
            var knaryTreeApp = mocks.PartialMock<KnaryTreeApp>();
            var knaryTree = mocks.PartialMock<KnaryTree>(2);
            var readerStream = new MemoryStream(Encoding.ASCII.GetBytes("20\n\na"));
            var result = new byte[3];
            var writerStream = new MemoryStream(result);
            using (mocks.Record())
            {
                Expect.Call(knaryTreeApp.CreateTree(2)).Return(knaryTree);
                knaryTree.StoreNumber(1);
                LastCall.Throw(new FormatException("exception"));
                Expect.Call(knaryTree.OutputPostOrder()).Return("01");
            }

            try
            {
                knaryTreeApp.ParseData(readerStream, writerStream);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ApplicationException));
                Assert.AreEqual(
                    "Input has to start with K-nary tree order number. Order must be higher than 1 or lower than 10. Please verify line number: 1", 
                    e.Message);
            }
        }

        #endregion
    }
}