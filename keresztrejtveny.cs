using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keresztrejtveny
{
    internal class KeresztrejtvenyRacs
    {
        private List<string> adatsorok;
        private char[,] racs;
        private int[,] sorszamok;


        public int oszlopokDb { get; set; }
        public int sorokDb { get; set; }



        private void BeolvasAdatsorok(string forras)
        {
            adatsorok = new List<string>(File.ReadAllLines(forras));
        }

        private void FeltoltRacs()
        {
            racs = new char[sorokDb, oszlopokDb];
            for (int i = 0; i < sorokDb; i++)
            {
                for (int j = 0; j < oszlopokDb; j++)
                {
                    racs[i, j] = adatsorok[i][j];
                }
            }
        }

        private void feltoltSzam()
        {
            sorszamok = new int[sorokDb, oszlopokDb];
            for (int i = 0; i < sorokDb; i++)
            {
                for (int j = 0; j < oszlopokDb; j++)
                {
                    sorszamok[i, j] = adatsorok[i][j];
                }
            }
        }

        public KeresztrejtvenyRacs(string forras)
        {
            BeolvasAdatsorok(forras);
            sorokDb = adatsorok.Count;
            oszlopokDb = adatsorok[0].Length;
            this.racs = new char[sorokDb, oszlopokDb];  //lehet kell +2
            FeltoltRacs();
            this.sorszamok = new int[sorokDb, oszlopokDb];
            feltoltSzam();
        }


        public void kinezet ()
        {
            for (int i = 0; i < sorokDb; ++i)
            {
                Console.Write("\t");
                for (int j = 0; j < oszlopokDb; ++j)
                {
                    if (racs[i, j] == '-')
                    {
                        Console.Write("[]");
                    }
                    else Console.Write("##");
                }
                Console.WriteLine();
            }
        }

        public int leghosszabbFuggoleges ()
        {
            int maxHossz = 0;
            for (int i = 0; i < sorokDb; ++i)
            {
                int hossz = 0;
                for (int j = 0; j < oszlopokDb; ++j)
                {
                    if (racs[j, i] == '-') hossz++;
                    else hossz = 0;
                    maxHossz = Math.Max(maxHossz, hossz);
                }
            }
            return maxHossz;
        }

        public void vizszintesStatisztika ()
        {
            for (int i = 0; i < sorokDb; ++i)
            {
                for (int j = 0; j < 1; j++)
                {
                    for (int k = 0; k < oszlopokDb; k++)
                    {
                        if (racs[i, j] == '#')
                        {

                        }

                        if (racs[i, j] == '-' && racs[i + 1, j] == '-')
                        {

                        }
                    }
                    
                }
            }
        }

        public void szamokkal()
        {
            int szam = 0;
            for (int i = 0; i < sorokDb-1; i++)
            {
                for (int j = 0; j < oszlopokDb-1; j++)
                {
                    if (sorszamok[i, j] == '-' && sorszamok[i, j+1] != '#'){
                        szam++;
                        Console.Write($"{szam}");
                        continue;
                    }
                    if (sorszamok[i, j] == '-' && sorszamok[i+1, j] != '#'){
                        szam++;
                        Console.Write($"{szam}");
                        continue;
                    }
                    if (sorszamok[i, j] == '-')
                    {
                        Console.Write("[]");
                        continue;
                    }
                    if (sorszamok[i, j] == '#')
                    {
                        Console.Write("##");
                        continue;
                    }

                }
                Console.WriteLine();
            }

        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var adat = new KeresztrejtvenyRacs("kr1.txt");

            Console.WriteLine($"5. feladat: A keresztrejtvény mérete\n\tSorok száma: {adat.sorokDb}\n\tOszlopok száma: {adat.oszlopokDb}");

            Console.WriteLine("6. feladat: A beolvasott keresztrejtvény");
            adat.kinezet();

            Console.WriteLine($"7. feladat: A leghosszabb függ.: {adat.leghosszabbFuggoleges()} karakter");

            Console.WriteLine("8. feladat: Vízszintes szavak statisztikája");

            Console.WriteLine($"9. feladat: A keresztrejtvény számokkal");
            adat.szamokkal();

            Console.ReadKey();
        }
    }
}
