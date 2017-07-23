// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertBase.cs" company="ZKosior">
//   Copyright (C) Zbigniew Kosior. All rights reserved.
// </copyright>
// <summary>
//   Converts base data type to another base data type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZKosior.TreeBuilder
{
    using System;

    /// <summary>
    ///     Converts base data type to another base data type.
    /// </summary>
    public class ConvertBase
    {
        #region Public Methods and Operators

        /// <summary>
        /// Converts number from base10 to byte array containing digits in different base.
        /// </summary>
        /// <param name="toBase">
        /// Base to convert to.
        /// </param>
        /// <param name="number">
        /// Number for base conversion.
        /// </param>
        /// <returns>
        /// Byte[] containing digits with converted number.
        /// </returns>
        /// <remarks>
        /// Base to convert to has to be higher than 1 an lower than 10.
        /// </remarks>
        public static byte[] FromBase10(uint toBase, uint number)
        {
            if (toBase < 2 || toBase > 9)
            {
                throw new ArgumentException("Base needs to be higher than 1 and lower than 10.");
            }

            // 32 is the max cast buffer size for base 2 and uint.MaxValue
            int i = 32;
            var buffer = new byte[i];

            do
            {
                buffer[--i] = (byte)(number % toBase);
                number = number / toBase;
            }
            while (number > 0);

            var result = new byte[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return result;
        }

        #endregion
    }
}