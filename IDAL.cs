using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// Data Acces Layer interface
    /// </summary>
    /// <typeparam name="T">the type of object</typeparam>
    interface IDAL<T>
    {
        /// <summary>
        /// generates the query
        /// </summary>
        /// <param name="code">code name</param>
        /// <param name="parametrs">parametrs</param>
        /// <returns></returns>
        string GetQuery(string code,List<KeyValuePair<string, object>> parametrs);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">code name</param>
        /// <param name="parametrs">parametrs</param>
        /// <returns></returns>
        IEnumerable<T> GetData(string code, List<KeyValuePair<string, object>>parametrs);


    }
}