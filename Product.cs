using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// class for creating product entity
    /// </summary>
    class Product
    {
        /// <summary>
        /// the product's id
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// product's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// product's color
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// empty constructor to create new product entity
        /// </summary>
        public Product()
        {
        }
    }
}