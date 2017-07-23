// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ZKosior">
//   Copyright (C) Zbigniew Kosior. All rights reserved.
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZKosior.TreeBuilder
{
    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        #region Methods

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            new ApplicationRunner(new KnaryTreeApp()).Run(args); // IoC could be used, but this is a small appliaction
        }

        #endregion
    }
}