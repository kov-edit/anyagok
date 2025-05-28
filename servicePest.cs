using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ServicePest_megoldott
{
    internal class Adatok
    {
        public string szerelo;
        public List<string> gepek = new List<string>();
        public bool[] munka = new bool[7];
        public int minosites;
        public Adatok(string sorok)
        {
            string[] sor = sorok.Split(',');
            int utolso = sor.Length-1;
            szerelo = sor[0];
            minosites = int.Parse(sor[utolso]);
            for (int i = 1; i < sor.Length - 8; i++)
            {
                gepek.Add(sor[i]);
            }
            
            int f = 6;
            for (int i = sor.Length - 2; i >= sor.Length - 9; i--)
            {
                if (sor[i] == "X") munka[f] = true;
                f--;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Adatok> lista = new List<Adatok>();
            try
            {
                string[] sorok = File.ReadAllLines("Pest.csv", Encoding.UTF8);
                
                foreach (string s in sorok)
                {
                    lista.Add(new Adatok(s));
                }
                Console.WriteLine("1.feladat:\n\tA Pest.csv nevű fájl beolvasása sikeres");
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)  //csak ha nem találja a fájlt
                {
                    Console.WriteLine("1.feladat:\n\tA Pest.csv nevű fájl beolvasása sikertelen");

                }
                else if (e is FormatException)
                {
                    Console.WriteLine("1. feladat:\n\tHibás adat");
                }
                else
                {
                    Console.WriteLine($"1.feladat:\n\tEgyéb hiba ({e.Message})");
                }
                Console.ReadKey();
                Environment.Exit(0);  //kilép a programból
            }



            //2. feladat

            int hossz = lista.Count;
            int legtobbIndex = 0;
            for (int i = 0; i < hossz; i++)
            {
                if (lista[i].gepek.Count > lista[legtobbIndex].gepek.Count)
                {
                    legtobbIndex = i;
                }
            }
            Console.Write($"2. feladat:\n\tA legtöbb (7 db) különböző típusú berendezéshez értő szerelők azonosítója:");

            
            int szombat = 0, vasarnap = 0;  //3. feladat
            int osszeg = 0;  //4. feladat

            foreach (var item in lista)
            {
                if (item.gepek.Count == lista[legtobbIndex].gepek.Count)  //2. feladat
                {
                    Console.Write($"{item.szerelo}, ");
                }

                if (item.munka[5])  //3. feladat
                {
                    szombat++;
                }
                if (item.munka[6])
                {
                    vasarnap++;
                }

                osszeg += item.minosites;  //4. feladat

            }
            Console.WriteLine("\b\b ");  //visszatörli az utolsó karaktereket


            //3. feladat

            Console.WriteLine($"3. feladat:\n\tSzombatonként {szombat} szerelő, vasárnaponként {vasarnap} szerelő áll rendelkezésre");



            //4. feladat

            double atlag = (double)osszeg / (double)hossz;

            int atlagFeletti = 0;
            foreach (var item in lista)
            {
                if (item.minosites > atlag)
                {
                    atlagFeletti++;
                }
            }

            Console.WriteLine($"4. feladat:\n\tA szerelők átlagosan {atlag:#.#} pontot kaptak. A szerelők {(double)atlagFeletti / (double)hossz * 100:#}%-a az átlagnál magasabb pontszámot kapott.");  //atlag:#.0 - kerekít



            //5. feladat

            Console.Write("5. feladat:\n\tMindhárom gázzal működő berendezéshez értő szerelők: ");

            foreach (var item in lista)
            {
                int gaz = 0;
                foreach (var item2 in item.gepek)
                {
                    if (item2 == "C" || item2 == "K" || item2 == "G")
                    {
                        gaz++;
                    }
                }
                if (gaz == 3)
                {
                    Console.Write($"{item.szerelo}, ");
                }
            }
            Console.Write("\b\b ");



            //6. feladat

            Console.Write("6. feladat:\n\tAdja meg a keresendő háztartási eszköz nevének rövidítését: ");
            string gep = Console.ReadLine();

            if (gep != "C" && gep != "KG" && gep != "K" && gep != "H" && gep != "P" && gep != "G" && gep != "MG" && gep != "MGT")
            {
                Console.WriteLine("Helytelen rövidítést adott meg");
            }
            else
            {
                string[] napok = {"hétfő", "kedd", "szerda", "csütörtök", "péntek", "szombat", "vasárnap"};
                foreach (var item in lista)
                {
                    bool erthozza = false;
                    foreach (var item2 in item.gepek)
                    {
                        if (item2 == gep)
                        {
                            erthozza = true;
                            break;
                        }
                    }
                    if (erthozza)
                    {
                        for (int i = 0; i < item.munka.Length; i++)
                        {
                            if (item.munka[i]) napok[i] = "";
                        }
                    }
                }
                Console.Write("A keresett típushoz a következő napokon nem áll rendelkezésre szerelő: ");
                foreach (var item in napok)
                {
                    Console.Write(item + " ");
                }
            }



            Console.ReadKey();
        }
    }
}
