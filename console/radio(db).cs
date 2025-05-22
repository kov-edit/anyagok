using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySqlConnector;

namespace Radioadok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new MySqlConnectionStringBuilder { Server = "127.0.0.1", UserID = "root", Password = ""};
            var kapcsolat = new MySqlConnection(server.ConnectionString);
            var sqlParancs = kapcsolat.CreateCommand();


            #region kiosztas
            string[] kiosztasadatok = File.ReadAllLines("kiosztas.txt", Encoding.UTF8);

            sqlParancs.CommandText = "create database if not exists radioadok character set utf8 collate utf8_hungarian_ci;" +
                "use radioadok;" +
                "drop table if exists kiosztas;";
            sqlParancs.CommandText += $"create table kiosztas (" 
                + "azon int auto_increment primary key," 
                + $"{kiosztasadatok[0].Split('\t')[0].Trim()} float," 
                + $"{kiosztasadatok[0].Split('\t')[1].Trim()} float," 
                + $"{kiosztasadatok[0].Split('\t')[2].Trim()} varchar(250)," 
                + $"{kiosztasadatok[0].Split('\t')[3].Trim()} varchar(250)," 
                + $"{kiosztasadatok[0].Split('\t')[4].Trim()} varchar(250))";

            kapcsolat.Open();
            var reader = sqlParancs.ExecuteReader();
            reader.Read();  
            reader.Close();

            sqlParancs.CommandText = $"insert into kiosztas (" +
                $"{kiosztasadatok[0].Split('\t')[0].Trim()}," +
                $"{kiosztasadatok[0].Split('\t')[1].Trim()}," +
                $"{kiosztasadatok[0].Split('\t')[2].Trim()}," +
                $"{kiosztasadatok[0].Split('\t')[3].Trim()}," +
                $"{kiosztasadatok[0].Split('\t')[4].Trim()}) values ";

            for (int i = 1; i < kiosztasadatok.Length; i++)
            {
                sqlParancs.CommandText += $"(" +
                $"{kiosztasadatok[i].Split('\t')[0].Trim()}," +
                $"{kiosztasadatok[i].Split('\t')[1].Trim()}," +
                $"'{kiosztasadatok[i].Split('\t')[2].Trim()}'," +
                $"'{kiosztasadatok[i].Split('\t')[3].Trim()}',";
                if (kiosztasadatok[i].Split('\t')[4].Trim() != "") sqlParancs.CommandText += $"'{kiosztasadatok[i].Split('\t')[4].Trim()}')";
                else sqlParancs.CommandText += $"null)";
                if (i != kiosztasadatok.Length - 1) sqlParancs.CommandText += $",\n";
            }

            reader = sqlParancs.ExecuteReader();
            reader.Read();
            reader.Close();

            //Console.WriteLine(sqlParancs.CommandText);

            #endregion


            #region telepules
            sqlParancs.CommandText = "";

            string[] telepulesadatok = File.ReadAllLines("telepules.txt", Encoding.UTF8);

            sqlParancs.CommandText = "drop table if exists telepules;";
            sqlParancs.CommandText += $"create table telepules ("
                + $"{telepulesadatok[0].Split('\t')[0].Trim()} varchar(255) primary key,"
                + $"{telepulesadatok[0].Split('\t')[1].Trim()} varchar(255))";

            reader = sqlParancs.ExecuteReader();
            reader.Read();
            reader.Close();

            sqlParancs.CommandText = $"insert into telepules (" +
                $"{telepulesadatok[0].Split('\t')[0].Trim()}," +
                $"{telepulesadatok[0].Split('\t')[1].Trim()}) values ";

            for (int i = 0; i < telepulesadatok.Length; i++)
            {
                sqlParancs.CommandText += $"(" +
                $"'{telepulesadatok[i].Split('\t')[0].Trim()}'," +
                $"'{telepulesadatok[i].Split('\t')[1].Trim()}')";
                if (i != telepulesadatok.Length - 1) sqlParancs.CommandText += $",\n";
            }

            reader = sqlParancs.ExecuteReader();
            reader.Read();
            reader.Close();

            //Console.WriteLine(sqlParancs.CommandText);

            #endregion


            #region regio
            sqlParancs.CommandText = "";

            string[] regioadatok = File.ReadAllLines("regio.txt", Encoding.UTF8);

            sqlParancs.CommandText = "drop table if exists regio;";
            sqlParancs.CommandText += $"create table regio ("
                + $"{regioadatok[0].Split('\t')[0].Trim()} varchar(255),"
                + $"{regioadatok[0].Split('\t')[1].Trim()} varchar(255) primary key)";

            reader = sqlParancs.ExecuteReader();
            reader.Read();
            reader.Close();

            sqlParancs.CommandText = $"insert into regio (" +
                $"{regioadatok[0].Split('\t')[0].Trim()}," +
                $"{regioadatok[0].Split('\t')[1].Trim()}) values ";

            for (int i = 0; i < regioadatok.Length; i++)
            {
                sqlParancs.CommandText += $"(" +
                $"'{regioadatok[i].Split('\t')[0].Trim()}'," +
                $"'{regioadatok[i].Split('\t')[1].Trim()}')";
                if (i != regioadatok.Length - 1) sqlParancs.CommandText += $",\n";
            }

            reader = sqlParancs.ExecuteReader();
            reader.Read();
            reader.Close();

            //Console.WriteLine(sqlParancs.CommandText);


            #endregion
            sqlParancs.CommandText = "";
            sqlParancs.CommandText = "alter table kiosztas add constraint foreign key (adohely) references telepules(nev);";
            sqlParancs.CommandText += "alter table telepules add constraint foreign key (megye) references regio(megye);";

            reader = sqlParancs.ExecuteReader();
            reader.Read();
            reader.Close();

            kapcsolat.Close();

            Console.WriteLine("kész");
            Console.ReadKey();
        }
    }
}
