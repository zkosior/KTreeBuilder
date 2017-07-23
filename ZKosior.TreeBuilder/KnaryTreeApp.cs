// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnaryTreeApp.cs" company="ZKosior">
//   Copyright (C) Zbigniew Kosior. All rights reserved.
// </copyright>
// <summary>
//   The knary tree app.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZKosior.TreeBuilder
{
    using System;
    using System.IO;

    /// <summary>
    /// The k-nary tree app.
    /// </summary>
    public class KnaryTreeApp
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create tree.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <returns>
        /// The <see cref="KnaryTree"/>.
        /// </returns>
        public virtual KnaryTree CreateTree(int order)
        {
            return new KnaryTree(order);
        }

        /// <summary>
        /// Parses data from input, stores them in tree and outputs the tree.
        /// </summary>
        /// <param name="inputStream">
        /// The input stream.
        /// </param>
        /// <param name="outputStream">
        /// The output stream.
        /// </param>
        public virtual void ParseData(Stream inputStream, Stream outputStream)
        {
            var reader = new StreamReader(inputStream);
            var writer = new StreamWriter(outputStream);
            int lineNumber = 0;
            string nextNumber = string.Empty;
            int order = 0;

            if (reader.EndOfStream)
            {
                return; // return on empty input file
            }

            while (!reader.EndOfStream)
            {
                nextNumber = reader.ReadLine();
                lineNumber++;
                if (string.IsNullOrWhiteSpace(nextNumber))
                {
                    continue; // ignore whitespace at the beginning
                }

                if (!int.TryParse(nextNumber, out order))
                {
                    new ApplicationException(string.Format("Please verify line number: {0}", lineNumber));
                }

                this.VerifyOrderValue(order, lineNumber);
                break;
            }

            this.VerifyOrderValue(order, lineNumber); // in case we exited loop on EndOfStream

            KnaryTree tree = this.CreateTree(order);
            while (!reader.EndOfStream && ((nextNumber = reader.ReadLine()) != "0"))
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(nextNumber))
                {
                    continue; // ignore whitespace as the middle line
                }

                try
                {
                    uint number = uint.Parse(nextNumber);
                    tree.StoreNumber(number);
                }
                catch (FormatException e)
                {
                    throw new ApplicationException(string.Format("Please verify line number: {0}", lineNumber), e);
                }
                catch (OverflowException e)
                {
                    throw new ApplicationException(
                        string.Format("Please verify line number: {0}. Numbers can not be negative.", lineNumber), e);
                }
            }

            this.VerifyInputClosedCorrectly(nextNumber);

            writer.Write(tree.OutputPostOrder());
            writer.Flush();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The verify input closed correctly.
        /// </summary>
        /// <param name="nextNumber">
        /// The next number.
        /// </param>
        /// <exception cref="ApplicationException">
        /// When input file not ended with correct sign.
        /// </exception>
        private void VerifyInputClosedCorrectly(string nextNumber)
        {
            if (nextNumber != "0")
            {
                throw new ApplicationException("Input file has to be ended with '0'");
            }
        }

        /// <summary>
        /// The verify order value.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <param name="lineNumber">
        /// The line number.
        /// </param>
        /// <exception cref="ApplicationException">
        /// When wrong order specified.
        /// </exception>
        private void VerifyOrderValue(int order, int lineNumber)
        {
            if (order < 2 || order > 9)
            {
                throw new ApplicationException(
                    string.Format(
                        "Input has to start with K-nary tree order number. Order must be higher than 1 or lower than 10. Please verify line number: {0}", 
                        lineNumber));
            }
        }

        #endregion
    }
}