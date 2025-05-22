using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace RealEstate
{

    public partial class Form1 : Form
    {
        static List<Seller> sellers = new List<Seller>();
        static List<Seller> activeSellers = new List<Seller>();
        static MySqlConnection kapcsolat;

        public Form1()
        {
            InitializeComponent();

            MySqlConnectionStringBuilder build = new MySqlConnectionStringBuilder { Server = "127.0.0.1", Database = "ingatlan", UserID = "root", Password = "" };  //xamppnál ez a szerver, (ampps-nál a jelszó valszeg mysql)
            kapcsolat = new MySqlConnection(build.ConnectionString);
            kapcsolat.Open();
            activeSellers = activeRead();
            listBoxSellers.Items.Clear();
            foreach (var item in  activeSellers)
            {
                listBoxSellers.Items.Add(item.name);
            }
        }

        static List<Seller> activeRead()
        {
            List<Seller> listtmp = new List<Seller>();
            var command = kapcsolat.CreateCommand();
            command.CommandText = "select * from sellers where id in (select sellerId from realestates) order by name";  //akiknek van eladó ingatlanuk azokat írja ki
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Seller tmp = new Seller();
                tmp.id = reader.GetInt32("id");
                tmp.name = reader.GetString("name");
                tmp.phone = reader.GetString("phone");
                listtmp.Add(tmp);
            }
            reader.Close();
            return listtmp;
        }

        static List<Seller> fullRead()
        {
            List<Seller> listtmp = new List<Seller>();
            var command = kapcsolat.CreateCommand();
            command.CommandText = "select * from sellers order by name";  //akiknek van eladó ingatlanuk azokat írja ki
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Seller tmp = new Seller();
                tmp.id = reader.GetInt32("id");
                tmp.name = reader.GetString("name");
                tmp.phone = reader.GetString("phone");
                listtmp.Add(tmp);
            }
            reader.Close();
            return listtmp;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            btnHirdetesek.Enabled = false;
        }

        private void btnHirdetesek_Click(object sender, EventArgs e)
        {
            /*var command = kapcsolat.CreateCommand();
            command.CommandText = $"select count(id) from realestates where sellerid = {activeSellers[listBoxSellers.SelectedIndex].id}";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lblCount.Text = $"Hirdetések száma: {reader.GetInt32(0)}";
            }
            reader.Close();*/


            /*var command = kapcsolat.CreateCommand();
            command.CommandText = $"select latlong from realestates where sellerid = {activeSellers[listBoxSellers.SelectedIndex].id}";
            var reader = command.ExecuteReader();
            listBoxCoordinates.Items.Clear();
            while (reader.Read())
            {
                listBoxCoordinates.Items.Add(reader.GetString(0));
            }
            reader.Close();*/

            var command = kapcsolat.CreateCommand();
            command.CommandText = $"select * from realestates where sellerid = {activeSellers[listBoxSellers.SelectedIndex].id}";
            var reader = command.ExecuteReader();
            listBoxCoordinates.Items.Clear();
            while (reader.Read())
            {
                /*string tmp = "";
                tmp += $"Hirdetés száma: {reader.GetInt32("id")}";
                tmp += $"\tSzobák száma: {reader.GetInt32("rooms")}";
                tmp += $"\tTerület: {reader.GetInt32("area")}";
                tmp += $"\tKoordináta: {reader.GetString("latlong")}";*/
                listBoxCoordinates.Items.Add($"Hirdetés száma: {reader.GetInt32("id")}");
                listBoxCoordinates.Items.Add($"Szobák száma: {reader.GetInt32("rooms")}");
                listBoxCoordinates.Items.Add($"Terület: {reader.GetInt32("area")}");
                listBoxCoordinates.Items.Add($"Koordináta: {reader.GetString("latlong")}");
                listBoxCoordinates.Items.Add(" ");

            }
            reader.Close();

            lblCount.Text = $"Hirdetések száma: {(listBoxCoordinates.Items.Count/5).ToString()}";

        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            btnHirdetesek.Enabled = false;
            listBoxCoordinates.Items.Clear();


            if (btnSellers.BackColor == Color.Crimson)
            {
                btnSellers.BackColor = Color.LightGreen;
                btnSellers.Text = "Aktív ügynökök";
                activeSellers = fullRead();
                listBoxSellers.Items.Clear();
                foreach (var item in activeSellers)
                {
                    listBoxSellers.Items.Add(item.name);
                }
                lblSellers.Text = "Összes ügynök:";
            }
            else
            {
                btnSellers.BackColor = Color.Crimson;
                btnSellers.Text = "Összes ügynök";
                activeSellers = activeRead();
                listBoxSellers.Items.Clear();
                foreach (var item in activeSellers)
                {
                    listBoxSellers.Items.Add(item.name);
                }
                lblSellers.Text = "Aktív ügynökök:";
            }
        }

        private void listBoxSellers_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnHirdetesek.Enabled = true;
            lblCount.Text = "Hirdetések száma: -";
            listBoxCoordinates.Items.Clear();
            lblSellerName.Text = $"Eladó neve: {activeSellers[listBoxSellers.SelectedIndex].name}";
            lblSellerPhone.Text = $"Eladó telefonszáma: {activeSellers[listBoxSellers.SelectedIndex].phone}";
        }

        private void Form1_Close(object sender, EventArgs e)
        {
            kapcsolat.Close();
        }
    }

    internal class Seller
    {
        public int id;
        public string name;
        public string phone;
       
    }

    internal class Category
    {
        public int id;
        public string name;
        
    }

    internal class Ad
    {
        public int area;
        public Category category;
        public DateTime creatAt;
        public string description;
        public int floors;
        public bool freeOfCharge;
        public int id;
        public string imageUrl;
        public string latLong;
        public int rooms;
        public Seller seller;

    }

}
