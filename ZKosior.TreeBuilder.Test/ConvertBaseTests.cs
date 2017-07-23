namespace ZKosior.TreeBuilder.Test
{
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConvertBaseTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void ConvertsFromBase10ToLowerBase()
        {
            Assert.AreEqual("11110100001001000000", this.GetStringNumber(ConvertBase.FromBase10(2, 1000000)));
        }

        #endregion

        #region Methods

        private string GetStringNumber(byte[] digits)
        {
            var sb = new StringBuilder();
            foreach (byte digit in digits)
            {
                sb.Append(digit.ToString());
            }

            return sb.ToString();
        }

        #endregion
    }
}