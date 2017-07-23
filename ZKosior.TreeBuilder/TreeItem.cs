// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeItem.cs" company="ZKosior">
//   Copyright (C) Zbigniew Kosior. All rights reserved.
// </copyright>
// <summary>
//   The tree item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZKosior.TreeBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// The tree item.
    /// </summary>
    internal class TreeItem
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeItem"/> class.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        public TreeItem(int order)
        {
            // If building KnaryTree for generic usage TreeItem would most probably be public and debug assertions should be changed to proper exceptions
            Debug.Assert(order >= 2 && order <= 9, "Wrong order level.");
            this.Children = new List<TreeItem>(order);
            for (int i = 0; i < order; i++)
            {
                // I'm loosing memory on empty Lists, but not on the elements itself.
                // The alternative would be to store value inside tree item gaining in memory but loosing on insert and access time
                this.Children.Add(null);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        public List<TreeItem> Children { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The output post order.
        /// </summary>
        /// <param name="collectingParameter">
        /// The collecting parameter.
        /// </param>
        public void OutputPostOrder(StringBuilder collectingParameter)
        {
            // This method could be divided into two. 
            // One would be a part of PostOrderEnumerator which would enumerate items 
            // and the other would be part of KnaryTree client and building result..
            for (int i = 0; i < this.Children.Capacity; i++)
            {
                if (this.Children[i] != null)
                {
                    this.Children[i].OutputPostOrder(collectingParameter);
                    collectingParameter.Append(i);
                }
            }
        }

        #endregion
    }
}