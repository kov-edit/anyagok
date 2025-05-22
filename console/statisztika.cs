using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace statisztika
{
    class adatok
    {
        public bool playerid; //vagy byte
        public string[] types = new string[3];
        public int[] scores = new int[3];
        public adatok(string sor)
        {
            string[] strings = sor.Split(';');

            try
            {
                this.playerid = strings[0] == "1"; //true vagy false
                for (int i = 0; i < 3; i++)
                {
                    if (strings[i + 1][0] == 'D' || strings[ i + 1][0] == 'T') //az elem első karaktere
                    {
                        this.types[i] = strings[i + 1][0].ToString();
                        this.scores[i] = int.Parse(strings[i + 1].Substring(1)); //az adott karaktertől az elem végéig, ha nincs hogy meddig
                    } 
                    else
                    {
                        this.types[i] = "";
                        this.scores[i] = int.Parse(strings[i + 1]);
                    }
                }
            }
            catch (Exception)
            {
                this.types[0] = "error";
            }
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {

            #region változok
            adatok[] tömb = new adatok[300];
            int tömbindex = 0;

            List<adatok> lista = new List<adatok>();
            #endregion

            #region 1. feladat
            //Console.WriteLine("1. feladat:");

            string filename = "dobasok.txt";
            StreamReader beolvas = new StreamReader(filename);

            while (!beolvas.EndOfStream)
            {
                adatok tmp = new adatok(beolvas.ReadLine());
                /*int a = 0; // leteszteljük, hogy műdödik e - breakpoint, alul az outputba látjuk*/
                if (tmp.types[0] != "error")
                {
                    lista.Add(tmp);
                    tömb[tömbindex++] = tmp;
                }
            }

            beolvas.Close();
            #endregion

            #region 2. feladat
            Console.WriteLine("2. feladat:");
            Console.WriteLine($"Körök száma: {tömbindex}");
            //Console.WriteLine($"Körök száma: {lista.Count}");

            #endregion

            #region 3. feladat
            Console.WriteLine("3. feladat:");
            int bullseye = 0;
            foreach (var item in lista)
            {
                if (item.scores[2] == 25 && item.types[2] == "D") bullseye++;
            }
            Console.WriteLine($"3. dobásra Bullseye: {bullseye}");

            bullseye = 0;
            foreach (var item in tömb)
            {
                if (item == null) continue; //következőre megy automata (tömb 300 elem miatt kell)
                if (item.scores[2] == 25 && item.types[2] == "D") bullseye++;
            }
            Console.WriteLine($"3. dobásra Bullseye: {bullseye}");
            #endregion

            #region 4. feladat
            Console.WriteLine("4. feladat:");
            Console.Write("Adja meg a szektor értékét! Szektor = ");
            string szektor = Console.ReadLine();

            int szektorÖssz1 = 0;
            int szektorÖssz2 = 0;

            int _180P1 = 0;
            int _180P2 = 0;

            foreach(var item in lista)
            {
                if (item == null) continue; //ha tömb akkor kell
                string segito = "";
                for(int i = 0; i < 3; i++)
                {
                    //4. feladat
                    if (item.playerid && szektor == item.types[i] + item.scores[i].ToString())
                    {
                        szektorÖssz1++;
                    }
                    else if (!item.playerid && szektor == item.types[i] + item.scores[i].ToString())
                    {
                        szektorÖssz2++;
                    }

                    //5. feladat
                    segito += item.types[i] + item.scores[i].ToString();
                }
                if (item.playerid && segito == "T20T20T20")
                {
                    _180P1++;
                }
                else if (!item.playerid && segito == "T20T20T20")
                {
                    _180P2++;
                }
            }

            Console.WriteLine($"Az 1. játékos a(z) {szektor} szektoros dobásainak száma: {szektorÖssz1}");
            Console.WriteLine($"A 2. játékos a(z) {szektor} szektoros dobásainak száma: {szektorÖssz2}");
            #endregion

            #region 5. feladat
            Console.WriteLine("5. feladat:");
            Console.WriteLine($"Az 1. játékos {_180P1} db 180-ast dobott");
            Console.WriteLine($"A 2. játékos {_180P2} db 180-ast dobott");
            #endregion


            Console.ReadKey();
        }
    }
}
