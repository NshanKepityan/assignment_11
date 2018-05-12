//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// class for creating person entity
    /// </summary>
    class Person
    {
        /// <summary>
        /// person's name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// person's surename
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// empty constructor for creating new person entity
        /// </summary>
        public Person()
        {
        }
    }
}