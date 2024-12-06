/*library import*/
using System;
using System.Drawing;
using System.Reflection.Metadata;
using System.Windows.Forms;

/* Grootte en startposities*/
int[] Venster_Grootte = { 760, 545 }; /* Venster grootte */
int[] StartBM = { 15, 130 };  /* Start positie van bitmap, relatief tot de venster */
int[] BM_Grootte = { 400, 400 };  /* Bitmap grootte */

/*  Voorbeeldplaatjes grootte en posities*/
int[] BM_voorbeeld_grootte = { 150, 150 };
int[] BM_verschil = { 165, 210 };
int[] start_voorbeeld = { 430, 130 };
int[] knop_afstand = { 160, 0 };
int[] knop_grootte = { 60, 25 };

/* Tekst boxen grootte en posities */
int[] StartVensterTekst = { 10, 15 };
int[] StartVensterTekst_Muisklik = { 210, 15 };
int[] StartTekstBox = { 100, 15 };  
int[] TekstBox_Grootte = { 80, 15 };
int[] Tekstbox_extrainfo_grootte = { 180, 15 };
int[] TekstBox_PosVerschil = { 0, 25 };

/* Scherm venster */
Form scherm = new Form();
scherm.Text = "Mandelbrot";
scherm.BackColor = Color.Beige;
scherm.ClientSize = new Size(Venster_Grootte[0], Venster_Grootte[1]);

/* Labels */
Label tekst_midden_x = new Label();
Label tekst_midden_y = new Label();
Label tekst_schaal = new Label();
Label tekst_max_aantal = new Label();
Label tekst_nieuwe_midden = new Label();
Label tekst_nieuwe_schaal = new Label();
Label uitleg_kleur = new Label();
Label uitleg_voorbeeld = new Label();

/* Textboxen */
TextBox invoer_midden_x = new TextBox();
TextBox invoer_midden_y = new TextBox();
TextBox invoer_schaal = new TextBox();
TextBox invoer_max_aantal = new TextBox();

/* Knoppen */
Button ga_knop = new Button();
Button kies_1 = new Button();
Button kies_2 = new Button();
Button kies_3 = new Button();
Button kies_4 = new Button();


TrackBar schuif = new TrackBar();

/* Bitmaps */
Bitmap plaatje = new Bitmap(BM_Grootte[0], BM_Grootte[1]);
Bitmap voorbeeld_1_bm = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_2_bm = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_3_bm = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_4_bm = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);


/* Labels */
Label label_plaatje = new Label();
Label label_voorbeeld_1 = new Label();
Label label_voorbeeld_2 = new Label();
Label label_voorbeeld_3 = new Label();
Label label_voorbeeld_4 = new Label();

/* Voorbeeld waardes */
Voorbeeld_waardes voorbeeld1, voorbeeld2, voorbeeld3, voorbeeld4;

/* Globale toestand variabelen */
double x_midden = 0, y_midden = 0, schaalfactor = 1, muis_x = 0, muis_y = 0;
int Maxmandeliteraties = 100;  /* Maximale keren dat een de mandelbrot itereert */

initVoorbeelden();
opstartVenster();

/* Event handlers voor als de text veranderd in boxen */
invoer_midden_x.TextChanged += boxVeranderd;
invoer_midden_y.TextChanged += boxVeranderd;
invoer_schaal.TextChanged += boxVeranderd;
invoer_max_aantal.TextChanged += boxVeranderd;

/* Even handlers om plaatje opnieuw te tekenen als er op een knop wordt geklikt */
ga_knop.Click += tekenen;
kies_1.Click += teken_1;
kies_2.Click += teken_2;
kies_3.Click += teken_3;
kies_4.Click += teken_4;


label_plaatje.MouseClick += muisKlik;  /* Event handler voor in- en uitzoomen van de muis */

schuif.Scroll += VeranderSchuif;  /* Event handler voor het gebruik van de trackbar */


Application.Run(scherm);

void initVoorbeelden(){

    voorbeeld1.x_midden = 0.0;
    voorbeeld1.y_midden = 0.0;
    voorbeeld1.schaal = 1.0;
    voorbeeld1.maxmandeliteraties = 100;
    voorbeeld1.schuif = 110;
    
    voorbeeld2.x_midden = -0.108625;
    voorbeeld2.y_midden = 0.9014428;
    voorbeeld2.schaal = 3.8147E-6;
    voorbeeld2.maxmandeliteraties = 400;
    voorbeeld2.schuif = 255;

    voorbeeld3.x_midden = 0.006875;
    voorbeeld3.y_midden = 0.80638671875;
    voorbeeld3.schaal = 9.765625E-03;
    voorbeeld3.maxmandeliteraties = 1000;
    voorbeeld3.schuif = 130;

    voorbeeld4.x_midden = -0.16745422105204897;
    voorbeeld4.y_midden = 1.041226005880121;
    voorbeeld4.schaal = 5.82076609134674E-6;
    voorbeeld4.maxmandeliteraties = 2000;
    voorbeeld4.schuif = 50;
}

void opstartVenster()
{
    /* Label voor de bitmap */
    scherm.Controls.Add(label_plaatje);
    label_plaatje.Location = new Point(StartBM[0], StartBM[1]);
    label_plaatje.Size = new Size(BM_Grootte[0], BM_Grootte[1]);
    label_plaatje.BackColor = Color.White;
    label_plaatje.Image = plaatje;

    /* Tekst toevoegen bij het input gedeelte */
    scherm.Controls.Add(tekst_midden_x);
    tekst_midden_x.Location = new Point(StartVensterTekst[0], StartVensterTekst[1]);
    tekst_midden_x.Size = new Size(80, 15);
    tekst_midden_x.Text = "midden x:";

    scherm.Controls.Add(tekst_midden_y);
    tekst_midden_y.Location = new Point(StartVensterTekst[0], StartVensterTekst[1] + TekstBox_PosVerschil[1]);
    tekst_midden_y.Size = new Size(80, 15);
    tekst_midden_y.Text = "midden y:";

    scherm.Controls.Add(tekst_schaal);
    tekst_schaal.Location = new Point(StartVensterTekst[0], StartVensterTekst[1] + TekstBox_PosVerschil[1] * 2);
    tekst_schaal.Size = new Size(80, 15);
    tekst_schaal.Text = "schaal:";

    scherm.Controls.Add(tekst_max_aantal);
    tekst_max_aantal.Location = new Point(StartVensterTekst[0], StartVensterTekst[1] + TekstBox_PosVerschil[1] * 3);
    tekst_max_aantal.Size = new Size(80, 15);
    tekst_max_aantal.Text = "max aantal:";

    /* Tekst boxen voor het input gedeelte */
    scherm.Controls.Add(invoer_midden_x);
    invoer_midden_x.Location = new Point(StartTekstBox[0], StartTekstBox[1]);
    invoer_midden_x.Size = new Size(TekstBox_Grootte[0], TekstBox_Grootte[1]);
    invoer_midden_x.Text = "0";

    scherm.Controls.Add(invoer_midden_y);
    invoer_midden_y.Location = new Point(StartTekstBox[0], StartTekstBox[1] + TekstBox_PosVerschil[1]);
    invoer_midden_y.Size = new Size(TekstBox_Grootte[0], TekstBox_Grootte[1]);
    invoer_midden_y.Text = "0";

    scherm.Controls.Add(invoer_schaal);
    invoer_schaal.Location = new Point(StartTekstBox[0], StartTekstBox[1] + TekstBox_PosVerschil[1] * 2);
    invoer_schaal.Size = new Size(TekstBox_Grootte[0], TekstBox_Grootte[1]);
    invoer_schaal.Text = "1";

    scherm.Controls.Add(invoer_max_aantal);
    invoer_max_aantal.Location = new Point(StartTekstBox[0], StartTekstBox[1] + TekstBox_PosVerschil[1] * 3);
    invoer_max_aantal.Size = new Size(TekstBox_Grootte[0], TekstBox_Grootte[1]);
    invoer_max_aantal.Text = "100";

    /* Knop aanmaken voor input gedeelte */
    scherm.Controls.Add(ga_knop);
    ga_knop.Location = new Point(225, 90);
    ga_knop.Size = new Size(40, 25);
    ga_knop.Text = "Ga!";

    /* Uitleg tekst boven voorbeelden */
    scherm.Controls.Add(uitleg_voorbeeld);
    uitleg_voorbeeld.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] - 30);
    uitleg_voorbeeld.Size = new Size(200, 15);
    uitleg_voorbeeld.Text = "Kies hier uit enkele voorbeelden!";

    /* Voorbeeldbitmap 1 */
    scherm.Controls.Add(label_voorbeeld_1);
    label_voorbeeld_1.Location = new Point(start_voorbeeld[0], start_voorbeeld[1]);
    label_voorbeeld_1.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_1.BackColor = Color.White;
    label_voorbeeld_1.Image = voorbeeld_1_bm;

    /* Voorbeeldbitmap 2 */
    scherm.Controls.Add(label_voorbeeld_2);
    label_voorbeeld_2.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] + BM_verschil[1]);
    label_voorbeeld_2.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_2.BackColor = Color.White;
    label_voorbeeld_2.Image = voorbeeld_2_bm;

    /* Voorbeeldbitmap 3 */
    scherm.Controls.Add(label_voorbeeld_3);
    label_voorbeeld_3.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1]);
    label_voorbeeld_3.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_3.BackColor = Color.White;
    label_voorbeeld_3.Image = voorbeeld_3_bm;

    /* Voorbeeldbitmap 4 */
    scherm.Controls.Add(label_voorbeeld_4);
    label_voorbeeld_4.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1] + BM_verschil[1]);
    label_voorbeeld_4.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_4.BackColor = Color.White;
    label_voorbeeld_4.Image = voorbeeld_4_bm;

    /* Knop voorbeeld 1 */
    scherm.Controls.Add(kies_1);
    kies_1.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] + knop_afstand[0]);
    kies_1.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_1.Text = "Kies 1";

    /* Knop voorbeeld 2 */
    scherm.Controls.Add(kies_2);
    kies_2.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] + BM_verschil[1] + knop_afstand[0]);
    kies_2.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_2.Text = "Kies 2";

    /* Knop voorbeeld 3 */
    scherm.Controls.Add(kies_3);
    kies_3.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1] + knop_afstand[0]);
    kies_3.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_3.Text = "Kies 3";

    /* Knop voorbeeld 4 */
    scherm.Controls.Add(kies_4);
    kies_4.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1] + +BM_verschil[1] + knop_afstand[0]);
    kies_4.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_4.Text = "Kies 4";

    /* Trackbar aanmaken */
    scherm.Controls.Add(schuif);
    schuif.Size = new Size(315, 280);
    schuif.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] - 90);
    schuif.Minimum = 0;
    schuif.Maximum = 255;
    schuif.Orientation = Orientation.Horizontal;

    /* Uitleg tekst boven schuif */
    scherm.Controls.Add(uitleg_kleur);
    uitleg_kleur.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] - 115);
    uitleg_kleur.Size = new Size(200, 15);
    uitleg_kleur.Text = "Verander hiermee de kleur!";

    /* Voorbeelden moeten zichtbaar zijn bij opstarten*/
    voorbeeldenOpstarten();

    // Figuur moet zichtbaar zijn bij opstarten
    tekenfiguur_kleur();
}

void boxVeranderd(object sender, EventArgs ea)
{
    try
    {
        x_midden = double.Parse(invoer_midden_x.Text);
        y_midden = double.Parse(invoer_midden_y.Text);
        schaalfactor = double.Parse(invoer_schaal.Text);
        Maxmandeliteraties = int.Parse(invoer_max_aantal.Text);
    }
    catch
    {
        
    }
}

double[] functieF(double x, double y, double a, double b)  /* Berekent (a,b) door middel van f */
{
    double[] ret = { 0, 0 };
    ret[0] = a * a - b * b + x;
    ret[1] = 2 * a * b + y;
    return ret;  /* Geef nieuwe a,b terug */
}

int mandelGetal(double x, double y, int iteraties)  /* Berekent het mandelgetal */
{
    double[] a_b = [0, 0];
    double afstand = 0;
    int n;

    for (n = 0; afstand < 2 && n < iteraties; n++)
    {
        a_b = functieF(x, y, a_b[0], a_b[1]);  /* Nieuwe a,b */
        afstand = Math.Sqrt(Math.Pow(a_b[0], 2) + Math.Pow(a_b[1], 2));  /* afstand = sqrt(a^2 + b^2) */
    }
    return n;
}

void tekenen(object o, EventArgs e)  /* Event handler voor het tekenen van de figuur bij drukken op de knop */
{
    x_midden = double.Parse(invoer_midden_x.Text);
    y_midden = double.Parse(invoer_midden_y.Text);
    schaalfactor = double.Parse(invoer_schaal.Text);
    Maxmandeliteraties = int.Parse(invoer_max_aantal.Text);
    tekst_nieuwe_midden.Text = ""; 
    tekst_nieuwe_schaal.Text = "";
    tekenfiguur_kleur();
}

void muisKlik(object o, MouseEventArgs ea)  /* Event handler voor in- en uitzoomen door het klikken van de muis */
{
    
    if (ea.Button == MouseButtons.Left)
    {
        schaalfactor = Math.Abs(schaalfactor) - 0.2 * Math.Abs(schaalfactor); 
        muis_x = mandelX(ea.X, schaalfactor);
        x_midden = x_midden + muis_x;
        muis_y = mandelY(ea.Y, schaalfactor);
        y_midden = y_midden + muis_y;
        tekenfiguur_kleur();
    }
    if (ea.Button == MouseButtons.Right)
    {
        schaalfactor = Math.Abs(schaalfactor) + 0.2 * Math.Abs(schaalfactor);
        muis_x = mandelX(ea.X, schaalfactor);
        x_midden = x_midden + muis_x;
        muis_y = mandelY(ea.Y, schaalfactor);
        y_midden = y_midden + muis_y;
        tekenfiguur_kleur();
    }

    double info_midden_x = Math.Round(x_midden, 2);
    double info_midden_y = Math.Round(y_midden, 2);
    double info_schaal = Math.Round(schaalfactor, 6);
    string tekst_midden = $"Het midden is nu ({info_midden_x}, {info_midden_y})";
    string tekst_schaal = $"De schaal is nu {info_schaal}";
    scherm.Controls.Add(tekst_nieuwe_midden);
    tekst_nieuwe_midden.Location = new Point(StartVensterTekst_Muisklik[0], StartVensterTekst_Muisklik[1]);
    tekst_nieuwe_midden.Size = new Size(Tekstbox_extrainfo_grootte[0], Tekstbox_extrainfo_grootte[1]);
    tekst_nieuwe_midden.Text = tekst_midden;
    scherm.Controls.Add(tekst_nieuwe_schaal);
    tekst_nieuwe_schaal.Location = new Point(StartVensterTekst_Muisklik[0], StartVensterTekst_Muisklik[1] + TekstBox_PosVerschil[1]);
    tekst_nieuwe_schaal.Size = new Size(Tekstbox_extrainfo_grootte[0], Tekstbox_extrainfo_grootte[1]);
    tekst_nieuwe_schaal.Text = tekst_schaal;
}

void VeranderSchuif(object o, EventArgs ea)  /* Bij het veranderen van de schuif moet de figuur opnieuw getekend worden */
{
    tekenfiguur_kleur();
}

double mandelX(int x, double schaal_x, int start_transformatie=-2, double start_factor = 1.0/100)  /* Berekent de nieuwe x coördinaat */
{
    double x_new = x * start_factor;

    x_new += start_transformatie;  /* Begin positie x */
    x_new *= schaal_x;

    return x_new;
}

double mandelY(int y, double schaal_y, int start_transformatie=-2, double start_factor = 1.0/100)  /*Berekent de nieuwe y coördinaat */
{
    double y_new = y * start_factor;

    y_new += start_transformatie;  /* Begin positie y */
    y_new = y_new * -1 * schaal_y;

    return y_new;
}

void tekenfiguur_kleur()
{
    int mandel_chunks = Maxmandeliteraties / 3;
    Color kleur;

    for (int x = 0; x < plaatje.Width; x++)
    {
        for (int y = 0; y < plaatje.Height; y++)
        {
            /* Bereken coordinaten in de mandelbrot {-2 <-> 2} */
            double x_mandel = mandelX(x, schaalfactor) + x_midden;
            double y_mandel = mandelY(y, schaalfactor) + y_midden;

            int mandel_n = mandelGetal(x_mandel, y_mandel, Maxmandeliteraties);
                                  
            int rood = Convert.ToInt32((255.0 / Maxmandeliteraties) * mandel_n * (schuif.Value + 1));
            rood %= 256;  /* RGB moet altijd kleiner zijn dan 256 */
                  
            int blauw = Convert.ToInt32((255.0 / (Maxmandeliteraties - mandel_chunks)) * mandel_n * (schuif.Value + 1));
            blauw %= 256;  /* RGB moet altijd kleiner zijn dan 256 */
                       
            int groen = Convert.ToInt32((255.0 / (Maxmandeliteraties - 2*mandel_chunks)) * mandel_n * (schuif.Value + 1));
            groen %= 256;  /* RGB moet altijd kleiner zijn dan 256 */

            kleur = Color.FromArgb(rood, groen, blauw);  /* Update kleur */
            plaatje.SetPixel(x, y, kleur);  /* Zet pixel (x,y) in bitmap */
        }
    }
    label_plaatje.Invalidate();
}

void voorbeeldenOpstarten()  /* De voorbeelden met vooraf ingestelden waarden worden opgestart */
{
    tekenVoorbeelden(voorbeeld_1_bm, label_voorbeeld_1, voorbeeld1.x_midden, voorbeeld1.y_midden, voorbeeld1.schaal, voorbeeld1.maxmandeliteraties, voorbeeld1.schuif);
    tekenVoorbeelden(voorbeeld_2_bm, label_voorbeeld_2, voorbeeld2.x_midden, voorbeeld2.y_midden, voorbeeld2.schaal, voorbeeld2.maxmandeliteraties, voorbeeld2.schuif);
    tekenVoorbeelden(voorbeeld_3_bm, label_voorbeeld_3, voorbeeld3.x_midden, voorbeeld3.y_midden, voorbeeld3.schaal, voorbeeld3.maxmandeliteraties, voorbeeld3.schuif);
    tekenVoorbeelden(voorbeeld_4_bm, label_voorbeeld_4, voorbeeld4.x_midden, voorbeeld4.y_midden, voorbeeld4.schaal, voorbeeld4.maxmandeliteraties, voorbeeld4.schuif);
}

void teken_1(object o, EventArgs e)  /* Event handler voor het tekenen van voorbeeld 1 wanneer op de klop wordt gedrukt */
{
    zetVoorbeeld(voorbeeld1);
}

void teken_2(object o, EventArgs e)  /* Event handler voor het tekenen van voorbeeld 2 wanneer op de klop wordt gedrukt */
{
    zetVoorbeeld(voorbeeld2);
}

void teken_3(object o, EventArgs e)  /* Event handler voor het tekenen van voorbeeld 3 wanneer op de klop wordt gedrukt */
{
    zetVoorbeeld(voorbeeld3);
}

void teken_4(object o, EventArgs e)  /* Event handler voor het tekenen van voorbeeld 4 wanneer op de klop wordt gedrukt */
{
    zetVoorbeeld(voorbeeld4);
}

void zetVoorbeeld(Voorbeeld_waardes voorbeeld){

    x_midden = voorbeeld.x_midden;
    invoer_midden_x.Text =  voorbeeld.x_midden.ToString();
    y_midden = voorbeeld.y_midden;
    invoer_midden_y.Text =  voorbeeld.y_midden.ToString();
    schaalfactor = voorbeeld.schaal;
    invoer_schaal.Text = voorbeeld.schaal.ToString();
    Maxmandeliteraties = voorbeeld.maxmandeliteraties;
    invoer_max_aantal.Text = voorbeeld.maxmandeliteraties.ToString();
    schuif.Value = voorbeeld.schuif;
    tekst_nieuwe_midden.Text = "";
    tekst_nieuwe_schaal.Text = "";

    tekenfiguur_kleur();
}

void tekenVoorbeelden(Bitmap plaatje, Label label, double x_voorbeeld, double y_voorbeeld, double schaal_voorbeeld, int iteraties_voorbeeld, double kleur_voorbeeld)  /* Tekent de voorbeeldplaatjes */
{
    schaal_voorbeeld *= BM_Grootte[0] / BM_voorbeeld_grootte[0];
    int mandel_chunks = iteraties_voorbeeld / 3;
    Color kleur;

    for (int x = 0; x < plaatje.Width; x++)
    {
        for (int y = 0; y < plaatje.Height; y++)
        {
            double mandel_x = mandelX(x, schaal_voorbeeld, -1, 1.0/75) + x_voorbeeld;
            double mandel_y = mandelY(y, schaal_voorbeeld, -1, 1.0/75) + y_voorbeeld;

            int mandel_n = mandelGetal(mandel_x, mandel_y, iteraties_voorbeeld);

            int rood = Convert.ToInt32((255.0 / iteraties_voorbeeld) * mandel_n * (kleur_voorbeeld + 1));
            rood %= 256;

            int blauw = Convert.ToInt32((255.0 / (iteraties_voorbeeld - mandel_chunks)) * mandel_n * (kleur_voorbeeld + 1));
            blauw %= 256;

            int groen = Convert.ToInt32((255.0 / (iteraties_voorbeeld - 2 * mandel_chunks)) * mandel_n * (kleur_voorbeeld + 1));
            groen %= 256;

            kleur = Color.FromArgb(rood, groen, blauw);
            plaatje.SetPixel(x, y, kleur);
        }
    }
    label.Invalidate();
}

struct Voorbeeld_waardes {
    public double x_midden;
    public double y_midden;
    public double schaal;
    public int maxmandeliteraties;
    public int schuif;
};

