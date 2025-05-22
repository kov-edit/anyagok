# <p align="center"> Class </p>

### **Sima**
``` c#
    class Arak
    {
        public string szezon;
        public string kezdet;
        public string vege;
        public int ar;
        public Arak(string sorok)
        {
            string[] sor = sorok.Split(';');
            this.szezon = sor[0];
            this.kezdet = sor[1];
            this.vege = sor[2];
            this.ar = int.Parse(sor[3]);
        }
    }
```

### **GetSet**
``` c#
    class Barlang
    {
        public int azon { get; private set; } //nem állíthatjuk át az adatot
        public string nev { get; private set; }
        public string telepules { get; private set; }
        public string vedettseg { get; set; } //átállítható adatok (get set = ha kap adatot beírja)

        /* a classba is lehet feltételt adni
        private int h = 0;
        public int hossz
        {
            get
            {
                return h;
            }
            set
            {
                if (h <= value || value == 0)
                {
                     h = value;
                }
            }
        }*/
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
    }
```

### **Internal class**
``` c#
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
```

### **Összetett class**
``` c#
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
```