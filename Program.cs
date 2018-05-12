using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path to query file");
            string path = Console.ReadLine();
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder
            {
                DataSource = "DESKTOP-F6GS4ME",
                InitialCatalog = "AdventureWorks2",
                IntegratedSecurity = true
            };

            List<KeyValuePair<string, object>> parametrs = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("param1", "SP")
            };
            var personDal = new MyDAL<Person>(cs, path);
            var personEntities = personDal.GetData("persons", parametrs);
            foreach (var elem in personEntities)
            {
                Console.WriteLine("----------------");
                var Props = elem.GetType().GetProperties();
                foreach (var property in Props)
                {
                    Console.WriteLine("{0} - {1}", property.Name, property.GetValue(elem));

                }
            }

            Console.WriteLine("--------------------------------------------------");

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("amount", "500")
            };
            var prodDal = new MyDAL<Product>(cs, path);
            var prodEntities = prodDal.GetData("products", parameters);
            foreach (var elem in prodEntities)
            {
                Console.WriteLine("----------------");
                var Props = elem.GetType().GetProperties();
                foreach (var property in Props)
                {
                    Console.WriteLine("{0} - {1}", property.Name, property.GetValue(elem));
                }
            }
        }
    }
}
    

