using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace jatszma5
{
    internal class Játék
    {
        public string allas;
        public string adogatoJatekos;
        public string fogadoJatekos;

        public Játék(string allas, string adogatoJatekos, string fogadoJatekos)
        {
            this.allas = allas;
            this.adogatoJatekos = adogatoJatekos;
            this.fogadoJatekos = fogadoJatekos;
        }
    
    public void Hozzáad(string eredmeny)
    {
        this.allas += eredmeny;
    }

    public void NyertLabdamenetekSzáma(string nyert)
    {
        
    }

    public void JátékVége(bool vege)
    {
        int nyertAdogató = 0;
        int nyertFogadó = 0;
        int különbség;

        if (NyertLabdamenetekSzáma('A')) nyertAdogató++;
        if (NyertLabdamenetekSzáma('F')) nyertFogadó++;

        különbség = Math.Abs(nyertAdogató - nyertFogadó);

        if ((nyertAdogató >= 4 || nyertFogadó >= 4) && különbség >= 2) return vege = true;
        else return false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            //1-2. feladat
            StreamReader beolvasas = new StreamReader("labdamenetek5.txt");

            string labdamenetek = "";

            while (!beolvasas.EndOfStream)
            {
               labdamenetek += beolvasas.ReadLine();
            }

            beolvasas.Close();


            //3. feladat
            int hossz = labdamenetek.Length;
            Console.WriteLine($"3. feladat: Labdamenetek száma: {hossz}");


            //4. feladat
            int adogatoNyer = 0;
            for (int i = 0; i < hossz; i++)
            {
                if (labdamenetek[i] == 'A')
                {
                    adogatoNyer++;
                }
            }

            Console.WriteLine($"4. feladat: Az adogató játékos {(double)adogatoNyer / (double)hossz * 100}%-ban nyerte meg a labdameneteket");


            //5. feladat
            int legtobbA = 0;
            int legtobb = 1;
            for (int i = 1; i < hossz; i++)
            {
                if (labdamenetek[i] != labdamenetek[i-1]) legtobb = 0;

                legtobb++;

                if (legtobb > legtobbA) legtobbA = legtobb;
                
            }

            Console.WriteLine($"5. feladat: Leghosszabb sorozat: {legtobbA}");




            Console.ReadKey();
        }
    }
}
