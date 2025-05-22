using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ingatlan
{
    internal class Seller
    {
        public int id;
        public string name;
        public string phone;
        public Seller (int id, string name, string phone)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
        }
    }

    internal class Category
    {
        public int id;
        public string name;
        public Category (int id, string name)
        {
            this.id = id;
            this.name = name;
        }
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

        public Ad(string sor)
        {
            string[] s = sor.Split(';');
            this.area = int.Parse(s[4]);
            this.category = new Category(int.Parse(s[12]), s[13]);
            this.creatAt = DateTime.Parse(s[8]);
            this.description = s[5];
            this.floors = int.Parse(s[3]);
            if (s[6] == "0")
            {
                this.freeOfCharge = false;
            }
            else this.freeOfCharge = true;
            this.id = int.Parse(s[0]);
            this.imageUrl = s[7];
            this.latLong = s[2];
            this.rooms = int.Parse(s[1]);
            this.seller = new Seller(int.Parse(s[9]), s[10], s[11]);

        }

        public static List<Ad> LoadFromCsv(string realestates)
        {
            List <Ad> lista = new List<Ad>();
            StreamReader beolvas = new StreamReader("realestates.csv", Encoding.UTF8);
            beolvas.ReadLine();
            while (!beolvas.EndOfStream)
            {
                Ad adat = new Ad(beolvas.ReadLine());
                lista.Add(adat);
            }

            //string[] sorok = File.ReadAllLines("realestate.csv", Encoding.UTF8).ToArray();


            /*foreach (string s in sorok)
            {

                
            }*/


            return lista;
        }
        public static double DistanceTo(string f, string g)
        {
            string[] a = g.Split(',');
            double x1 = double.Parse(a[0].Replace(".", ","));
            double y1 = double.Parse(a[1].Replace(".", ","));

            string[] b = f.Split(',');
            double x2 = double.Parse(b[0].Replace(".", ","));
            double y2 = double.Parse(b[1].Replace(".", ","));

            double tavolsag = Math.Pow(Math.Abs((x1 - x2) * (y1 - y2)) ,2);

            return tavolsag;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            List<Ad> lista =  Ad.LoadFromCsv("realestates.csv");

            //6. feladat
            double ossz = 0;
            double db = 0;

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].floors == 0)
                {
                    ossz += lista[i].area;
                    db++;
                }
            }

            double átlag = ossz / db;

            Console.WriteLine($"6. feladat: Földszinti ingatlanok átlagos alapterülete: {Math.Round(átlag, 2)} m2");

            //8. feladat
            double legkisebb = 0;
            double legkisebbertek = 10000;
            for (int i = 0; i < lista.Count;i++)
            {
                
                if (Ad.DistanceTo(lista[i].latLong, "47.4164220114023,19.066342425796986") < legkisebbertek && lista[i].freeOfCharge)
                {
                    legkisebbertek = Ad.DistanceTo(lista[i].latLong, "47.4164220114023,19.066342425796986");
                    legkisebb = i;
                }
            }

            for (int i = 0; i < lista.Count;i++)
            {
                if (i == legkisebb)
                {
                    Console.Write($"8. feladat: Mesevár óvodához légvonalban legközelebbi tehermentes ingatlan adatai:\n\tEladó neve: {lista[i].seller.name}\n\tEladó telefonja: {lista[i].seller.phone}\n\tAlapterület: {lista[i].area}\n\tSzobák száma: {lista[i].rooms}");
                }
            }
            

            Console.ReadKey();
        }
    }
}
