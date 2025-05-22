using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace barlang
{
    class Barlang
    {
        public int azon { get; private set; } //nem állíthatjuk át az adatot
        public string nev { get; private set; }
        public string telepules { get; private set; }
        public string vedettseg { get; set; }//átállítható adatok (get set = ha kap adatot beírja)

        private int h = 0;  //csak itt használjuk, csak itt látható
        public int hossz {  //szétszedjük a get set-et mert feltétel kapcsán kaphat más értéket (csak nagyobb lehet a barlang)
            get
            {
                return h;  //muszáj neki a return
            }
            set
            {
                if (h <= value || value == 0)  //ha nagyobb értéket adnék mint eredetileg volt (a hossz valueja), csak akkor csinálja meg
                {
                    h = value;
                }
            }
        }
        private int m = 0;
        public int melyseg { 
            get
            {
                return m;
            }
            set
            {
                if (m < value)
                {
                    m = value;
                }
            }
        }

        public Barlang(string sorok)
        {
            try
            {
                string[] sor = sorok.Split(';');
                azon = int.Parse(sor[0]);
                nev = sor[1];
                hossz = int.Parse(sor[2]);
                melyseg = int.Parse(sor[3]);
                telepules = sor[4];
                vedettseg = sor[5];
            }
            catch (Exception) 
            {
                hossz = 0;
            }

        }

        public override string ToString()
        {
            return $"Azon: {azon}\nNév: {nev}\nHossz: {hossz}\nMélység: {melyseg}\nTelepülés: {telepules}\nVédettség: {vedettseg}";  //ahelyett hogy kiírnánk kiírás közben, pl cw($"{a.szon}") helyett
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /*Barlang a = new Barlang("1;név;500;600;település;védettség");  //teszteset
            Console.WriteLine(a.ToString()); //a fenti overrideot használva automatikusan átalakítja
            a.hossz = 600;
            Console.WriteLine(a.ToString());
            a.hossz = 500;  //nem váltható vissza, kevesebbre nem állítja
            Console.WriteLine(a.ToString());*/

            List<Barlang> lista = new List<Barlang>();

            StreamReader beolvas = new StreamReader("..\\..\\..\\..\\barlangok.txt", Encoding.UTF8); //mappákat lépünk vissza - relatív elérés, két \\ mert az egy speciális karaktert jelez (sima / is működik), ha tudjuk hol van - abszolút elérés, C:\\név\\név\\
            
            /*Barlang a = new Barlang(beolvas.ReadLine());  //az első adat nem jó -> try-catch felül
            Console.WriteLine(a.ToString());*/

            while (!beolvas.EndOfStream)
            {
                Barlang tmp = new Barlang(beolvas.ReadLine());
                if (tmp.hossz != 0) lista.Add(tmp);  //ha nulla akkor csak folyatassa (↑)
            }

            beolvas.Close();

            //2. feladat
            Console.WriteLine($"Barlangok száma: {lista.Count}");

            //3. feladat
            int melysegek = 0;
            int db = 0;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].telepules.StartsWith("Miskolc"))
                {
                    melysegek += lista[i].melyseg;
                    db++;
                }
            }
            double atlag = melysegek / db;
            Console.WriteLine($"A miskolci barlangok átlagos mélysége: {Math.Round(atlag, 3)}");

            //4. feladat
            

            Console.ReadKey();
        }
    }
}
