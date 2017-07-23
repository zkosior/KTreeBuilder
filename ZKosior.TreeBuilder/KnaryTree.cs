// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnaryTree.cs" company="ZKosior">
//   Copyright (C) Zbigniew Kosior. All rights reserved.
// </copyright>
// <summary>
//   The knary tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZKosior.TreeBuilder
{
    using System;
    using System.Text;

    /// <summary>
    /// The k-nary tree.
    /// </summary>
    public class KnaryTree
    {
        // For such a small application I decided that to create tree for representing numbers.
        // To represent other objects, or making tree generic 
        // I would choose to move methods responsible for storing number and outputing content to another class.
        // Additionaly I would probably make K-naryTree inherit from Enumerable interfaces and make one that walks it post orderly.
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KnaryTree"/> class.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <exception cref="ArgumentException">
        /// When wrong order level set.
        /// </exception>
        public KnaryTree(int order)
        {
            if (order < 2 || order > 9)
            {
                throw new ArgumentException("Order needs to be higher than 1 and lower than 10.");
            }

            this.Order = order;
            this.Root = new TreeItem(order);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        private int Order { get; set; }

        /// <summary>
        /// Gets or sets the root.
        /// </summary>
        private TreeItem Root { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The output post order.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public virtual string OutputPostOrder()
        {
            // This small method could be moved to KnaryTree client and make use of PostOrderEnumerator, 
            // but for such a small program this would be an overkill. It would also force storing item values inside TreeItem.
            var result = new StringBuilder();
            this.Root.OutputPostOrder(result);
            return result.ToString();
        }

        /// <summary>
        /// The store number.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        public virtual void StoreNumber(uint number)
        {
            TreeItem node = this.Root;
            TreeItem child;
            foreach (byte digit in ConvertBase.FromBase10((uint)this.Order, number))
            {
                child = node.Children[digit];
                if (child == null)
                {
                    child = new TreeItem(this.Order);
                    node.Children[digit] = child;
                }

                node = child;
            }
        }

        #endregion
    }
}