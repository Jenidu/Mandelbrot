/*library import*/
using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Windows.Forms;

/*grootte en startposities*/
int[] Venster_Grootte = { 760, 545 }; /*venster grootte*/
int[] StartBM = { 15, 130 };  /* Start positie van bitmap, relatief tot de venster */
int[] BM_Grootte = { 400, 400 };  /* Bitmap grootte */

/*voorbeeldplaatjes grootte en posities*/
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

/*scherm venster*/
Form scherm = new Form();
scherm.Text = "Mandelbrot";
scherm.BackColor = Color.Beige;
scherm.ClientSize = new Size(Venster_Grootte[0], Venster_Grootte[1]);

/*labels*/
Label tekst_midden_x = new Label();
Label tekst_midden_y = new Label();
Label tekst_schaal = new Label();
Label tekst_max_aantal = new Label();
Label tekst_nieuwe_midden = new Label();
Label tekst_nieuwe_schaal = new Label();
Label uitleg_kleur = new Label();
Label uitleg_voorbeeld = new Label();

/*textboxen*/
TextBox invoer_midden_x = new TextBox();
TextBox invoer_midden_y = new TextBox();
TextBox invoer_schaal = new TextBox();
TextBox invoer_max_aantal = new TextBox();

/*knoppen*/
Button ga_knop = new Button();
Button kies_1 = new Button();
Button kies_2 = new Button();
Button kies_3 = new Button();
Button kies_4 = new Button();


TrackBar schuif = new TrackBar();

/*bitmaps*/
Bitmap plaatje = new Bitmap(BM_Grootte[0], BM_Grootte[1]);
Bitmap voorbeeld_1 = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_2 = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_3 = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_4 = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);


/*labels*/
Label label_plaatje = new Label();
Label label_voorbeeld_1 = new Label();
Label label_voorbeeld_2 = new Label();
Label label_voorbeeld_3 = new Label();
Label label_voorbeeld_4 = new Label();


/*globale toestand variabelen*/
double x_midden = 0, y_midden = 0, schaalfactor = 1, muis_x = 0, muis_y = 0;
int Maxmandeliteraties = 100;  /* Maximale keren dat een de mandelbrot itereert */

/*venster opstarten*/
opstartVenster();

/*Event handlers voor als de text veranderd in boxen*/
invoer_midden_x.TextChanged += boxVeranderd;
invoer_midden_y.TextChanged += boxVeranderd;
invoer_schaal.TextChanged += boxVeranderd;
invoer_max_aantal.TextChanged += boxVeranderd;

/*Even handlers om plaatje opnieuw te tekenen als er op een knop wordt geklikt*/
ga_knop.Click += tekenen;
kies_1.Click += teken_1;
kies_2.Click += teken_2;
kies_3.Click += teken_3;
kies_4.Click += teken_4;

/*Event handler voor in- en uitzoomen van de muis*/
label_plaatje.MouseClick += muisKlik;

/*Even handler voor het gebruik van de trackbar*/
schuif.Scroll += VeranderSchuif;


Application.Run(scherm);


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

void opstartVenster()
{
    /*Label voor de bitmap*/
    scherm.Controls.Add(label_plaatje);
    label_plaatje.Location = new Point(StartBM[0], StartBM[1]);
    label_plaatje.Size = new Size(BM_Grootte[0], BM_Grootte[1]);
    label_plaatje.BackColor = Color.White;
    label_plaatje.Image = plaatje;

    /* tekst toevoegen bij het input gedeelte */
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

    /* Knop aanmaken voor input gedeelte*/
    scherm.Controls.Add(ga_knop);
    ga_knop.Location = new Point(225, 90);
    ga_knop.Size = new Size(40, 25);
    ga_knop.Text = "Ga!";

    /* Uitleg tekst boven voorbeelden */
    scherm.Controls.Add(uitleg_voorbeeld);
    uitleg_voorbeeld.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] - 30);
    uitleg_voorbeeld.Size = new Size(200, 15);
    uitleg_voorbeeld.Text = "Kies hier uit enkele voorbeelden!";

    // voorbeeldbitmap 1
    scherm.Controls.Add(label_voorbeeld_1);
    label_voorbeeld_1.Location = new Point(start_voorbeeld[0], start_voorbeeld[1]);
    label_voorbeeld_1.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_1.BackColor = Color.White;
    label_voorbeeld_1.Image = voorbeeld_1;

    // voorbeeldbitmap 2
    scherm.Controls.Add(label_voorbeeld_2);
    label_voorbeeld_2.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] + BM_verschil[1]);
    label_voorbeeld_2.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_2.BackColor = Color.White;
    label_voorbeeld_2.Image = voorbeeld_2;

    // voorbeeldbitmap 3
    scherm.Controls.Add(label_voorbeeld_3);
    label_voorbeeld_3.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1]);
    label_voorbeeld_3.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_3.BackColor = Color.White;
    label_voorbeeld_3.Image = voorbeeld_3;

    // voorbeeldbitmap 4
    scherm.Controls.Add(label_voorbeeld_4);
    label_voorbeeld_4.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1] + BM_verschil[1]);
    label_voorbeeld_4.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_4.BackColor = Color.White;
    label_voorbeeld_4.Image = voorbeeld_4;

    /* Knop voorbeeld 1*/
    scherm.Controls.Add(kies_1);
    kies_1.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] + knop_afstand[0]);
    kies_1.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_1.Text = "Kies 1";

    /* Knop voorbeeld 2*/
    scherm.Controls.Add(kies_2);
    kies_2.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] + BM_verschil[1] + knop_afstand[0]);
    kies_2.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_2.Text = "Kies 2";

    /* Knop voorbeeld 3*/
    scherm.Controls.Add(kies_3);
    kies_3.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1] + knop_afstand[0]);
    kies_3.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_3.Text = "Kies 3";

    /* Knop voorbeeld 4*/
    scherm.Controls.Add(kies_4);
    kies_4.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1] + +BM_verschil[1] + knop_afstand[0]);
    kies_4.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_4.Text = "Kies 4";

    //trackbar aanmaken
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
    voorbeelden_opstarten();

    //figuur moet zichtbaar zijn bij opstarten
    tekenfiguur_kleur();
}

double[] functieF(double x, double y, double a, double b)/*berekent (a,b) door middel van f*/
{
    double[] ret = { 0, 0 };
    ret[0] = a * a - b * b + x;
    ret[1] = 2 * a * b + y;
    return ret;  /* Geef nieuwe a,b terug */
}

int mandelGetal(double x, double y, int iteraties) /*berekent het mandelgetal*/
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

double functie_x(int x) /*berekent de nieuwe x_coördinaat*/
{
    double x_new = (double)x / 100;
    x_new = (x_new - 2) * schaalfactor;
    return x_new;
}

double functie_y(int y)/*berekent de nieuwe y coördinaat*/
{
    double y_new = (double)y / 100;
    y_new = y_new - 2;
    y_new = (y_new * -1) * schaalfactor;
    return y_new;
}

void tekenen(object o, EventArgs e)/*Event handler voor het tekenen van de figuur bij drukken op de knop*/
{
    x_midden = Double.Parse(invoer_midden_x.Text);
    y_midden = double.Parse(invoer_midden_y.Text);
    schaalfactor = double.Parse(invoer_schaal.Text);
    Maxmandeliteraties = int.Parse(invoer_max_aantal.Text);
    tekst_nieuwe_midden.Text = ""; 
    tekst_nieuwe_schaal.Text = "";
    tekenfiguur_kleur();   

}

void muisKlik(object o, MouseEventArgs ea)/*Event handler voor in- en uitzoomen door het klikken van de muis*/
{
    
    if (ea.Button == MouseButtons.Left)
    {
        schaalfactor = Math.Abs(schaalfactor) - 0.2 * Math.Abs(schaalfactor); 
        muis_x = functie_x(ea.X);
        x_midden = x_midden + muis_x;
        muis_y = functie_y(ea.Y);
        y_midden = y_midden + muis_y;
        tekenfiguur_kleur();
    }
    if (ea.Button == MouseButtons.Right)
    {
        schaalfactor = Math.Abs(schaalfactor) + 0.2 * Math.Abs(schaalfactor);
        muis_x = functie_x(ea.X);
        x_midden = x_midden + muis_x;
        muis_y = functie_y(ea.Y);
        y_midden = y_midden + muis_y;
        tekenfiguur_kleur();
    }

    double info_midden_x = Math.Round(x_midden, 2);
    double info_midden_y = Math.Round(y_midden, 2);
    double info_schaal = Math.Round(schaalfactor, 6);
    string tekst_midden = ($"het midden is nu ({info_midden_x},{info_midden_y})!");
    string tekst_schaal = ($"De schaal is nu {info_schaal}!");
    scherm.Controls.Add(tekst_nieuwe_midden);
    tekst_nieuwe_midden.Location = new Point(StartVensterTekst_Muisklik[0], StartVensterTekst_Muisklik[1]);
    tekst_nieuwe_midden.Size = new Size(Tekstbox_extrainfo_grootte[0], Tekstbox_extrainfo_grootte[1]);
    tekst_nieuwe_midden.Text = tekst_midden;
    scherm.Controls.Add(tekst_nieuwe_schaal);
    tekst_nieuwe_schaal.Location = new Point(StartVensterTekst_Muisklik[0], StartVensterTekst_Muisklik[1] + TekstBox_PosVerschil[1]);
    tekst_nieuwe_schaal.Size = new Size(Tekstbox_extrainfo_grootte[0], Tekstbox_extrainfo_grootte[1]);
    tekst_nieuwe_schaal.Text = tekst_schaal;
}

void VeranderSchuif(object o, EventArgs ea)/*Bij het veranderen van de schuif moet de figuur opnieuw getekend worden*/
{
    tekenfiguur_kleur();
}

void tekenfiguur_kleur()
{
    int blauw = 0;
    int rood = 0;
    int groen = 0;
    Color kleur = Color.FromArgb(rood, groen, blauw);
    int mandel_chunks = Maxmandeliteraties / 3;


    for (int x = 0; x < plaatje.Width; x++)
    {
        for (int y = 0; y < plaatje.Height; y++)
        {

            double x_coordinaat = functie_x(x) + x_midden;
            double y_coordinaat = functie_y(y) + y_midden;

            int mandel = mandelGetal(x_coordinaat, y_coordinaat, Maxmandeliteraties);
                                  
            rood = Convert.ToInt32((255.0 / Maxmandeliteraties) * mandel * (schuif.Value + 1));
            rood %= 256;            
                  
            blauw = Convert.ToInt32((255.0 / (Maxmandeliteraties - mandel_chunks)) * mandel * (schuif.Value + 1));
            blauw %= 256;
                       
            groen = Convert.ToInt32((255.0 / (Maxmandeliteraties - 2*mandel_chunks)) * mandel * (schuif.Value + 1));
            groen %= 256;
            
            kleur = Color.FromArgb(rood, groen, blauw);
            plaatje.SetPixel(x, y, kleur);
        }
    }
    label_plaatje.Invalidate();
}

void voorbeelden_opstarten()/*De voorbeelden met vooraf ingestelden waarden worden opgestart*/
{
    teken_voorbeelden(voorbeeld_1, label_voorbeeld_1, 0, 0, 1, 100, 110);
    teken_voorbeelden(voorbeeld_2, label_voorbeeld_2, -0.108625, 0.9014428, 3.8147E-6, 400, 255);
    teken_voorbeelden(voorbeeld_3, label_voorbeeld_3, 0.006875, 0.80638671875, 9.765625E-03, 1000, 130);
    teken_voorbeelden(voorbeeld_4, label_voorbeeld_4, -0.16745422105204897, 1.041226005880121, 5.82076609134674E-6, 2000, 50);
}

void teken_1(object o, EventArgs e)/*Event handler voor het tekenen van voorbeeld 1 wanneer op de klop wordt gedrukt*/
{
    x_midden = 0;
    invoer_midden_x.Text = "0";
    y_midden = 0;
    invoer_midden_y.Text = "0";
    schaalfactor = 1;
    invoer_schaal.Text = "1";
    Maxmandeliteraties = 100;
    invoer_max_aantal.Text = "100";
    schuif.Value = 110;
    tekst_nieuwe_midden.Text = "";
    tekst_nieuwe_schaal.Text = "";

    tekenfiguur_kleur();
}

void teken_2(object o, EventArgs e)/*Event handler voor het tekenen van voorbeeld 2*/
{
    x_midden = -0.108625;
    invoer_midden_x.Text = "-0.108625";
    y_midden = 0.9014428;
    invoer_midden_y.Text = "0.9014428";
    schaalfactor = 3.8147E-6;
    invoer_schaal.Text = "3.8147E-6";
    Maxmandeliteraties = 400;
    invoer_max_aantal.Text = "400";
    schuif.Value = 255;
    tekst_nieuwe_midden.Text = "";
    tekst_nieuwe_schaal.Text = "";

    tekenfiguur_kleur();
}

void teken_3(object o, EventArgs e)/*Het tekenen van voorbeeld 3*/
{
    x_midden = 0.006875;
    invoer_midden_x.Text = "0.006875";
    y_midden = 0.80638671875;
    invoer_midden_y.Text = "0.80638671875";
    schaalfactor = 9.765625E-03;
    invoer_schaal.Text = "9.765625E-03";
    Maxmandeliteraties = 1000;
    invoer_max_aantal.Text = "1000";
    tekst_nieuwe_midden.Text = "";
    tekst_nieuwe_schaal.Text = "";
    schuif.Value = 130;

    tekenfiguur_kleur();
}

void teken_4(object o, EventArgs e)/*Het tekenen van voorbeeld 4*/
{
    x_midden = -0.16745422105204897;
    invoer_midden_x.Text = "-0.16745422105204897";
    y_midden = 1.041226005880121;
    invoer_midden_y.Text = "1.041226005880121";
    schaalfactor = 5.82076609134674E-6;
    invoer_schaal.Text = "5.82076609134674E-11";
    Maxmandeliteraties = 2000;
    invoer_max_aantal.Text = "2000";
    schuif.Value = 50;
    tekst_nieuwe_midden.Text = "";
    tekst_nieuwe_schaal.Text = "";

    tekenfiguur_kleur();
}

double functie_x_test(int x, double c)/*Berekent de nieuwe x coördinaat in de voorbeelden*/
{
    double x_new_t = (double)x / 75;
    x_new_t = x_new_t -1;
    x_new_t = x_new_t * c;
    return x_new_t;
}

double functie_y_test(int y, double c)/*Berekent de nieuwe y coördinaat in de voorbeelden*/
{
    double y_new_t = (double)y / 75;
    y_new_t = y_new_t -1;
    y_new_t = y_new_t * -1;
    y_new_t = y_new_t * c;
    return y_new_t;
}

void teken_voorbeelden(Bitmap kies_bitmap, Label kies_label, double x_voorbeeld, double y_voorbeeld, double schaal_voorbeeld, int iteraties_voorbeeld, double kleur_voorbeeld)/*Tekent de voorbeeldplaatjes*/
{
    schaal_voorbeeld = schaal_voorbeeld * (400 / 150);
    int blauw = 0;
    int rood = 0;
    int groen = 0;
    Color kleur = Color.FromArgb(rood, groen, blauw);
    int mandel_chunks = iteraties_voorbeeld / 3;

    for (int x = 0; x < kies_bitmap.Width; x++)
    {
        for (int y = 0; y < kies_bitmap.Height; y++)
        {
            
            double x_coordinaat_t = functie_x_test(x, schaal_voorbeeld) + x_voorbeeld;
            double y_coordinaat_t = functie_y_test(y, schaal_voorbeeld) + y_voorbeeld;

            int mandel = mandelGetal(x_coordinaat_t, y_coordinaat_t, iteraties_voorbeeld);

            rood = Convert.ToInt32((255.0 / iteraties_voorbeeld) * mandel * (kleur_voorbeeld + 1));
            rood %= 256;

            blauw = Convert.ToInt32((255.0 / (iteraties_voorbeeld - mandel_chunks)) * mandel * (kleur_voorbeeld + 1));
            blauw %= 256;

            groen = Convert.ToInt32((255.0 / (iteraties_voorbeeld - 2 * mandel_chunks)) * mandel * (kleur_voorbeeld + 1));
            groen %= 256;

            kleur = Color.FromArgb(rood, groen, blauw);
            kies_bitmap.SetPixel(x, y, kleur);
        }
    }
    kies_label.Invalidate();
}