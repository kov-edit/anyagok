using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace adatbazisKezeles
{
    internal class Program
    {
        static void Main(string[] args)
        {
			//mysqlconnector letölteni
            //kötelező adatbázisnál - mysqlconnector letöltés
            MySqlConnectionStringBuilder build = new MySqlConnectionStringBuilder {Server = "127.0.0.1", Database = "ingatlan", UserID = "root", Password = "" };  //xamppnál ez a szerver, (ampps-nál a jelszó valszeg mysql)
            MySqlConnection kapcsolat = new MySqlConnection(build.ConnectionString);
            kapcsolat.Open();

            MySqlCommand parancssor = kapcsolat.CreateCommand();  //var-al is működnek

            /*parancssor.CommandText = "SELECT * FROM `sellers`;";
            MySqlDataReader reader = parancssor.ExecuteReader();
            while (reader.Read())
            {
                //Console.WriteLine(reader.GetString("name"));
                Console.WriteLine($"{reader.GetInt64(0)} {reader.GetString(1)} {reader.GetString(2)}");
            }*/

            parancssor.CommandText = "SELECT name FROM sellers WHERE id = (SELECT sellerId FROM realestates WHERE area = (SELECT max(area) FROM `realestates`));";
            MySqlDataReader reader = parancssor.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }



            int nm = 0;
            parancssor.CommandText = "SELECT max(area) FROM `realestates`;";
            reader = parancssor.ExecuteReader();
            while (reader.Read())
            {
                nm = reader.GetInt32(0);
            }

            parancssor.CommandText = $"SELECT sellerId FROM realestates WHERE area = {nm}";
            reader = parancssor.ExecuteReader();
            int sellerid = 0;
            while (reader.Read())
            {
                 sellerid = reader.GetInt32(0);
            }

            parancssor.CommandText = $"SELECT name FROM sellers WHERE id = {sellerid}";
            reader = parancssor.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }

            kapcsolat.Close();

            Console.ReadKey();
        }
    }
}
