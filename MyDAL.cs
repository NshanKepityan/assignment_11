using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace DAL
{
    /// <summary>
    /// data access layer
    /// </summary>
    /// <typeparam name="T">the type of entity</typeparam>
    class MyDAL<T> : IDAL<T>
    {
        /// <summary>
        /// the connection string 
        /// </summary>
        private SqlConnectionStringBuilder connectionString;
        /// <summary>
        /// path to the file with queries
        /// </summary>
        private string filePath;
        /// <summary>
        /// the result list of entities
        /// </summary>
        private List<T> result;
        /// <summary>
        /// constructor wich creates new dal between entity and storage
        /// </summary>
        /// <param name="ConnectionString">the connection string</param>
        /// <param name="path">the path to file</param>
        public MyDAL(SqlConnectionStringBuilder ConnectionString, string path)
        {
            this.connectionString = ConnectionString;
            this.filePath = path;
        }
        /// <summary>
        /// creates exact query by code and parametrs from query file
        /// </summary>
        /// <param name="code">the code name</param>
        /// <param name="parametrs">the parametrs</param>
        /// <returns>query</returns>
        public string GetQuery(string code,List<KeyValuePair<string, object>> parametrs)
        {
            string fs = File.ReadAllText($"{this.filePath}");
            string pattern = $@"^name:{code}(\r\n?|\n)query:(.+)$";
            string query = "";
            Regex regex = new Regex(pattern);
            foreach (Match match in Regex.Matches(fs, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                query = match.Groups[2].ToString();
            }
            string result = "";
            foreach (KeyValuePair<string, object> param in parametrs)
            {
                pattern = $@"(@{param.Key})";
                regex = new Regex(pattern);
                string replacement = $"'{param.Value}'";
                Regex rgx = new Regex(pattern);
                result += rgx.Replace(query, replacement);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">the code name</param>
        /// <param name="parametrs">the parametrs</param>
        /// <returns>list of entity objects</returns>
        public IEnumerable<T> GetData(string code, List<KeyValuePair<string, object>> parametrs)
        {

            using (SqlConnection connection = new SqlConnection(this.connectionString.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = GetQuery(code,parametrs),
                    CommandType = CommandType.Text
                };

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        this.result = new List<T>();
                        while (reader.Read())
                        {
                            var t = (T)typeof(T).GetConstructor(new Type[] { }).Invoke(null);
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                var Props = t.GetType().GetProperties();
                                    Props[i].SetValue(t, reader[i], null);
                            }
                            this.result.Add(t);
                        }
                        return this.result;
                    }
                    else
                    {
                        Console.WriteLine("no rows found");
                        return null;
                    }
                }

            }
        }
    }
}