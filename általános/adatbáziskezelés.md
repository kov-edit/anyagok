# <p align="center"> Adatbázis kezelés </p>

### **mappanév -> NuGet -> MysqlConnector**

``` c#
    MySqlConnectionStringBuilder build = new MySqlConnectionStringBuilder {Server = "127.0.0.1", Database = "ingatlan", UserID = "root", Password = "" };  //xamppnál ez a szerver, (ampps-nál a jelszó valszeg mysql)
    MySqlConnection kapcsolat = new MySqlConnection(build.ConnectionString);
    kapcsolat.Open();

    MySqlCommand parancssor = kapcsolat.CreateCommand();  //var-al is működnek
    parancssor.CommandText = "SELECT * FROM `sellers`;";
    MySqlDataReader reader = parancssor.ExecuteReader();
    while (reader.Read())
    {
        //Console.WriteLine(reader.GetString("name"));
        Console.WriteLine($"{reader.GetInt64(0)} {reader.GetString(1)} {reader.GetString(2)}");
    }
```