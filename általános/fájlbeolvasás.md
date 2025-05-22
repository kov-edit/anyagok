# <p align="center"> Fájlbeolvasás, fájlkiírás </p>

### **Sima**
``` c#
    StreamReader sr = new StreamReader("..\\..\\..\\filename", Encoding.UTF8);
    while (!sr.EndOfStream)
    {
        Barlang tmp = new Barlang(sr.ReadLine());
        if (tmp.hossz != 0)
        {
            lista.Add(tmp);
        }
    }
```

### **OpenFileDialog
``` c#
    OpenFileDialog ofd = new OpenFileDialog();
    ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
    if (ofd.ShowDialog() == DialogResult.OK)
    {
        try
        {
            StreamReader beolvas = new StreamReader(ofd.FileName);
            while (!beolvas.EndOfStream)
            {
                Barlang tmp = new Barlang(beolvas.ReadLine());
                if (tmp.hossz != 0)
                {
                    lista.Add(tmp);
                }
            }
            filelabel.Text = ofd.FileName;
            beolvas.Close();
        }
        catch
        {
             MessageBox.Show("Nem megfelő fájl");
        }

    }
```

### **ReadAllLines**
``` c#
    string[] sorok = File.ReadAllLines("filename", Encoding.UTF8).ToArray();
    foreach (string s in sorok)
    {
        
    }
```

### **Feltételes fájlolvasás**
``` c#
    StreamReader reader = new StreamReader("filename");

    while (!reader.EndOfStream)
    {
        try
        {
            list.Add(new Adatok(reader.ReadLine()));
        }
        catch (Exception)
        {
            continue;
        }
    }
```

### **Fájlkiírás**
``` c#
    int fajlIndex = int.Parse(index.Text);
    StreamWriter kiiras = new StreamWriter($"Lab{fajlIndex}.txt", false, Encoding.UTF8);

    int sorok = int.Parse(sor.Text);
    int oszlopok = int.Parse(oszlop.Text);

    try
    {
        for (int i = 0; i < oszlopok; i++)
        {
            for (int j = 0; j < sorok; j++)
            {
                if (labirintus[i, j].Checked) kiiras.Write("X");  //fordítva írja ki
                else kiiras.Write(".");
            }
                kiiras.WriteLine();
        }
        MessageBox.Show("Az állomány mentése sikeres");
    }
    catch (Exception)
    {

        MessageBox.Show("Hiba");
    }

    kiiras.Close();
```