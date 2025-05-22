# <p align="center"> Függvények </p>

### **Helyük: _private void Form1_Load(object sender, EventArgs e)_-on kívül <br>Vagy _internal class Program_-on kívül**

### **Lehetséges hivatkozásaik**

#### Static int
``` c#
    static int length(string displaytext)
    {
        return //szám;
    }
```

#### Public void
``` c#
    public void kiiras()
    {
        lbldarab.Visible = true;
        lbldarab.Text = $": {lista.Count} db";
    }

```

#### Private void változóval
``` c#
    private void Display(string btn)  //átküldjük a lenyomott gomb jelét
    {
        string textDisplay = txbDisplay.Text;  //átküldött szöveg
        int maxLength = length(textDisplay);
        if (btn == "+-")
        {
            txbDisplay.Text = (double.Parse(textDisplay) * -1).ToString();
            return;
        }
    }
```

#### Private void
``` c#
    private void kozepso()
    {
        boxes[2, 2].Text = "X";
        boxes[2, 2].Enabled = false;
    }

    //későbbi hivatkozás
    kozepso();
```

#### Lista
``` c#
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
        return lista;
    }
```