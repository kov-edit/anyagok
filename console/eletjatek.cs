using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EletJatek
{
    internal class EletjatekSzimulator
    {
        
        private int OszlopokSzama = 10;
        private int SorokSzama = 10;
        private int[,] Matrix;
        
        private EletjatekSzimulator()
        {
            Random rnd = new Random();
            for (int i = 0; i < OszlopokSzama + 2; i++)
            {
                for (int j = 0; j < SorokSzama + 2; j++)
                {
                    if ((i > 0 || i < OszlopokSzama + 1) && (j > 0 || j < SorokSzama + 1))
                    {
                        this.Matrix[i, j] = rnd.Next(0, 2);
                    }
                }
            }


        }

        public void EletJatekSzimulator()
        {
            
        }

        private void KovetkezoAllapot()
        {
            int szomszedja = 0;
            int[,] matrixtmp = new int[OszlopokSzama + 2, SorokSzama + 2];
            for (int i = 1; i < OszlopokSzama + 1; i++)
            {
                for (int j = 1; j < SorokSzama + 1; j++)
                {
                    if (Matrix[i - 1, j - 1] == 1) szomszedja++;  //bal felső átlós
                    if (Matrix[i, j - 1] == 1) szomszedja++;  //felette
                    if (Matrix[i + 1, j - 1] == 1) szomszedja++;  //jobb felső átló
                    if (Matrix[i + 1, j] == 1) szomszedja++;  //jobb mellette
                    if (Matrix[i + 1, j + 1] == 1) szomszedja++;  //jobb alsó átló
                    if (Matrix[i, j + 1] == 1) szomszedja++;  //alatta
                    if (Matrix[i - 1, j + 1] == 1) szomszedja++;  //bal alsó átló
                    if (Matrix[i - 1, j] == 1) szomszedja++;  //bal mellette

                    if (Matrix[i, j] == 0)
                    {
                        if (szomszedja == 3) matrixtmp[i, j] = 1;
                    }

                    if (Matrix[i, j] == 1)
                    {
                        if (szomszedja < 2 || szomszedja > 3) matrixtmp[i, j] = 0;
                    }
                }
            }

            for (int i = 0; i < OszlopokSzama + 2; i++)
            {
                for (int j = 0; j < SorokSzama + 2; j++)
                {
                    matrixtmp[i, j] = Matrix[i, j];
                }
            }
        }

        private void Megjelenit()
        {
            for (int i = 0; i < OszlopokSzama + 2; i++)
            {
                for (int j = 0; j < SorokSzama + 2; j++)
                {
                    if (i == 0 || j == 0 || i == OszlopokSzama + 2 || j == SorokSzama + 2) Matrix[i, j] = 'X';
                    if (Matrix[i, j] == 0)
                    {
                        Matrix[i, j] = ' ';
                    }
                    if (Matrix[i, j] == 1)
                    {
                        Matrix[i, j] = 'S';
                    }
                    Console.Write(Matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void Run()
        {
            Megjelenit();
            KovetkezoAllapot();
            Thread.Sleep(500);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {



            Console.ReadKey();
        }
    }
}
