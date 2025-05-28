using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace szalloda_megoldott
{
    class Adatok
    {
        public int sorszam;
        public byte szobaSzam;
        public int erkezes;
        public int tavozas;
        public byte vendegek;
        public bool reggeli;
        public string id;
        public int szobaAr;
        public int ejszakak;

        public Adatok(string sorok, int[,] honapok)
        {
            string[] sor = sorok.Split(' ');
            this.sorszam = int.Parse(sor[0]);
            this.szobaSzam = byte.Parse(sor[1]);
            this.erkezes = int.Parse(sor[2]);
            this.tavozas = int.Parse(sor[3]);
            this.vendegek = byte.Parse(sor[4]);
            this.reggeli = sor[5] == "1";  //ha egyenlő 1 akkor true, memóriára és futási sebességre optimális
            this.id = sor[6];

            this.ejszakak = this.tavozas - this.erkezes;
        }
    }
    internal class Program
    {
        static int[,] honapok = new int[12, 3];
        static List<Adatok> foglalasok = new List<Adatok>();
        static List<string> screenText = new List<string>();  //classon belül, de mainen kívül a classba lévő minden függvény tudja használni
        
        static int szobaAr(int i)
        {
            int ii = 0;
            int szobaAr = 0;
            while (ii < honapok.GetLength(0) && !(foglalasok[i].erkezes > honapok[ii, 1] && foglalasok[i].erkezes <= honapok[ii, 1] + honapok[ii, 0]))
            {
                ii++;
            }

            if (ii < honapok.GetLength(0))
            {
                szobaAr = honapok[ii, 2];
                if (foglalasok[i].reggeli)
                {
                    szobaAr += 1100 * foglalasok[i].vendegek;
                }
                if (foglalasok[i].vendegek == 3)
                {
                    szobaAr += 2000;
                }
            }
            return szobaAr;

            /* vagy
            for (int j = 0; j < honapok.GetLength(0); j++)  //getlength 0 = magasság, 1 = szélesség
            {

                if (foglalasok[i].erkezes > honapok[j, 0] && foglalasok[i].erkezes <= honapok[j, 0] + honapok[j, 1])
                {
                    return honapok[j, 2];
                }
            }*/
        }

        static void Main(string[] args)
        {

            CultureInfo ci = CultureInfo.InstalledUICulture;  //más neylveken is megnyitható legyen

            try
            {
                string[] textS = File.ReadAllLines(ci.ThreeLetterWindowsLanguageName + ".lng");
                screenText.AddRange(textS);  //AddRange = tömböt listába rak
            }
            catch (Exception)
            {
                string[] textS = File.ReadAllLines("eng.lng");
                screenText.AddRange(textS);
            }


            #region 1. feladat

            string[] honapsorok = File.ReadAllLines("honapok.txt");  //3. feladat

            for (int i = 0; i < honapsorok.Length; i = i + 4)  //+4 mert egy adathoz 4 sor van
            {
                for (int j = 1; j < 4; j++)
                {
                    honapok[i / 4, j - 1] = int.Parse(honapsorok[i + j]);
                }
            }

            string[] sorok = File.ReadAllLines("pitypang.txt").Skip(1).ToArray(); //nem kell lezárni, automatikusan zárja, skip = az első sort kihagyja

            foreach (var item in sorok)
            {
                foglalasok.Add(new Adatok(item, honapok));
            }

            int hossz = foglalasok.Count;

            Console.WriteLine(screenText[0] + hossz);

            #endregion


            #region 2. feladat

            int maxindex = 0;
            for (int i = 1; i < hossz; i++)
            {
                if (foglalasok[i].ejszakak > foglalasok[maxindex].ejszakak)
                {
                    maxindex = i;
                }
            }

            Console.Write(screenText[1]);
            Console.WriteLine($"{foglalasok[maxindex].id} ({foglalasok[maxindex].erkezes}) - {foglalasok[maxindex].ejszakak}");

            #endregion


            #region 3. feladat

            for (int i = 0; i < hossz; i++)
            {
                foglalasok[i].szobaAr = szobaAr(i);
            }

            StreamWriter kiir = new StreamWriter("bevetel.txt", false, Encoding.UTF8);

            int ossz = 0;
            foreach (var item in foglalasok)
            {
                kiir.WriteLine($"{item.sorszam}:{item.ejszakak*item.szobaAr}");
                ossz += item.ejszakak * item.szobaAr;
            }

            kiir.Close();

            Console.WriteLine($"{screenText[2]} {ossz:### ### ### ###} {screenText[3]}");  //tagolja a számot
            #endregion


            #region 4. feladat

            int evNap = honapok[honapok.GetLength(0)-1, 1] + honapok[honapok.GetLength(0) - 1, 0];
            int[,] napok = new int[evNap,28];
            foreach (var item in foglalasok)
            {
                for (int i = item.erkezes; i < item.tavozas; i++)
                {
                    napok[i,0] += item.vendegek;
                    napok[i, item.szobaSzam] += item.vendegek;
                }
            }

            Console.WriteLine(screenText[4]);

            for (int i = 0; i < honapok.GetLength(0); i++)
            {
                int honapHossz = 0;
                for (int j = honapok[i, 1]; j < honapok[i, 1] + honapok[i, 0]; j++)
                {
                    honapHossz += napok[j,0];
                }
                Console.WriteLine($"\t{i+1}: {honapHossz} {screenText[5]}");
            }

            #endregion


            #region 5. feladat




            #endregion

            Console.ReadKey();
        }
    }
}
