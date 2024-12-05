/*library import*/
using System;
using System.Drawing;
using System.Windows.Forms;

/*grootte en startposities*/
int[] Venster_Grootte = { 760, 545 }; /*venster grootte*/
int[] StartBM = { 15, 130 };  /* Start positie van bitmap, relatief tot de venster */
int[] BM_Grootte = { 400, 400 };  /* Bitmap grootte */

//voorbeeldplaatjes
int[] BM_voorbeeld_grootte = { 150, 150 };
int[] BM_verschil = { 165, 210 };
int[] start_voorbeeld = { 430, 130 };
int[] knop_afstand = { 160, 0 };
int[] knop_grootte = { 60, 25 };

/* Tekst boxen */
int[] StartVensterTekst = { 10, 15 };
int[] StartVensterTekst_Muisklik = { 210, 15 };
int[] StartTekstBox = { 100, 15 };  /* Start positie tekst box */
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

//knoppen
Button ga_knop = new Button();
Button kies_1 = new Button();
Button kies_2 = new Button();
Button kies_3 = new Button();
Button kies_4 = new Button();

//trackbar
TrackBar schuif = new TrackBar();

//bitmaps
Bitmap plaatje = new Bitmap(BM_Grootte[0], BM_Grootte[1]);
Bitmap voorbeeld_1 = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_2 = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_3 = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
Bitmap voorbeeld_4 = new Bitmap(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);


//labels
Label label_plaatje = new Label();
Label label_voorbeeld_1 = new Label();
Label label_voorbeeld_2 = new Label();
Label label_voorbeeld_3 = new Label();
Label label_voorbeeld_4 = new Label();


//globale toestand variabelen
double x_midden = 0, y_midden = 0, schaalfactor = 1, muis_x = 0, muis_y = 0;
int Maxmandeliteraties = 100;  /* Maximale keren dat een de mandelbrot itereert */
double oneindig = double.PositiveInfinity;


/*venster opstarten*/
opstartVenster();

/*Event handlers voor als de text veranderd in boxen*/
invoer_midden_x.TextChanged += boxVeranderd;
invoer_midden_y.TextChanged += boxVeranderd;
invoer_schaal.TextChanged += boxVeranderd;
invoer_max_aantal.TextChanged += boxVeranderd;

//als er op de knop geklikt wordt, tekent het plaatje opnieuw
ga_knop.Click += tekenen;

//met de muis in/uitzoomen
label_plaatje.MouseClick += muisKlik;

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
        //voor nu: doe niks. Later UI toevoegen??

    }


}

void opstartVenster()
{

    scherm.Controls.Add(label_plaatje);
    label_plaatje.Location = new Point(StartBM[0], StartBM[1]);
    label_plaatje.Size = new Size(BM_Grootte[0], BM_Grootte[1]);
    label_plaatje.BackColor = Color.White;
    label_plaatje.Image = plaatje;

    /* Invoer tekst aanmaken */
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

    /* Tekst boxen aanmaken */
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

    /* Knop aanmaken */
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

    // voorbeeldbitmap 2
    scherm.Controls.Add(label_voorbeeld_4);
    label_voorbeeld_4.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1] + BM_verschil[1]);
    label_voorbeeld_4.Size = new Size(BM_voorbeeld_grootte[0], BM_voorbeeld_grootte[1]);
    label_voorbeeld_4.BackColor = Color.White;
    label_voorbeeld_4.Image = voorbeeld_4;

    /* Knop aanmaken 1*/
    scherm.Controls.Add(kies_1);
    kies_1.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] + knop_afstand[0]);
    kies_1.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_1.Text = "Kies 1";

    /* Knop aanmaken 2*/
    scherm.Controls.Add(kies_2);
    kies_2.Location = new Point(start_voorbeeld[0], start_voorbeeld[1] + BM_verschil[1] + knop_afstand[0]);
    kies_2.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_2.Text = "Kies 2";

    /* Knop aanmaken 3*/
    scherm.Controls.Add(kies_3);
    kies_3.Location = new Point(start_voorbeeld[0] + BM_verschil[0], start_voorbeeld[1] + knop_afstand[0]);
    kies_3.Size = new Size(knop_grootte[0], knop_grootte[1]);
    kies_3.Text = "Kies 3";

    /* Knop aanmaken 4*/
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

    //figuur moet zichtbaar zijn bij opstarten
    tekenfiguur_kleur();





}

double[] functieF(double x, double y, double a, double b)
{

    double[] ret = { 0, 0 };
    ret[0] = a * a - b * b + x;
    ret[1] = 2 * a * b + y;
    return ret;  /* Geef nieuwe a,b terug */
}

int mandelGetal(double x, double y)
{

    double[] a_b = [0, 0];
    double afstand = 0;
    int n;

    for (n = 0; afstand < 2 && n < Maxmandeliteraties; n++)
    {
        a_b = functieF(x, y, a_b[0], a_b[1]);  /* Nieuwe a,b */

        afstand = Math.Sqrt(Math.Pow(a_b[0], 2) + Math.Pow(a_b[1], 2));  /* afstand = sqrt(a^2 + b^2) */

    }

    return n;
}

double functie_x(int x)
{
    double x_new = (double)x / 100;
    x_new = (x_new - 2) * schaalfactor;
    return x_new;


}

double functie_y(int y)
{
    double y_new = (double)y / 100;
    y_new = y_new - 2;
    y_new = (y_new * -1) * schaalfactor;
    return y_new;
}

void tekenFiguur()
{


    for (int x = 0; x < plaatje.Width; x++)
    {
        for (int y = 0; y < plaatje.Height; y++)
        {

            double x_coordinaat = functie_x(x) + x_midden;
            double y_coordinaat = functie_y(y) + y_midden;


            if (mandelGetal(x_coordinaat, y_coordinaat) % 2 == 0 || mandelGetal(x_coordinaat, y_coordinaat) == oneindig)
            {
                plaatje.SetPixel(x, y, Color.Black);
            }
            else
            {
                plaatje.SetPixel(x, y, Color.White);
            }

        }
    }
}

void tekenen(object o, EventArgs e)
{
    tekst_nieuwe_midden.Text = "";
    tekst_nieuwe_schaal.Text = "";
    tekenFiguur();
    label_plaatje.Invalidate(); //object dat onder handen genomen wordt, wordt opnieuw getekend. 

}

void muisKlik(object o, MouseEventArgs ea)
{


    if (ea.Button == MouseButtons.Left)
    {

        schaalfactor = schaalfactor - 0.1;
        muis_x = functie_x(ea.X);
        x_midden = x_midden + muis_x;
        muis_y = functie_y(ea.Y);
        y_midden = y_midden + muis_y;
        tekenfiguur_kleur();

    }
    if (ea.Button == MouseButtons.Right)
    {
        schaalfactor = schaalfactor + 0.05;
        muis_x = functie_x(ea.X);
        x_midden = x_midden + muis_x;
        muis_y = functie_y(ea.Y);
        y_midden = y_midden + muis_y;
        tekenfiguur_kleur();
    }



    double info_midden_x = Math.Round(x_midden, 2);
    double info_midden_y = Math.Round(y_midden, 2);
    double info_schaal = Math.Round(schaalfactor, 2);
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



    label_plaatje.Invalidate();


}

void VeranderSchuif(object o, EventArgs ea)
{
    label_plaatje.Invalidate();

}

void tekenfiguur_kleur()
{
    int blauw = schuif.Value;
    int rood = 0;
    int groen = 0;
    Color kleur = Color.FromArgb(rood, groen, blauw);


    for (int x = 0; x < plaatje.Width; x++)
    {
        for (int y = 0; y < plaatje.Height; y++)
        {

            double x_coordinaat = functie_x(x) + x_midden;
            double y_coordinaat = functie_y(y) + y_midden;



            if (mandelGetal(x_coordinaat, y_coordinaat) % 2 == 0 || mandelGetal(x_coordinaat, y_coordinaat) == oneindig)
            {
                rood = rood + 50;
                plaatje.SetPixel(x, y, kleur);
            }
            else
            {
                groen = groen + 1;
                plaatje.SetPixel(x, y, kleur);
            }

        }
    }

}

//double functie_x_voorbeelden(int x_voorbeeld)
//{
//    double x_new_v = (double) x_voorbeeld / 75;
//    x_new_v = (x_new_v - 2) * schaalfactor;
//    return x_new_v;

//}
//void voorbeelden_tekenen()
//{
//   // x_midden_v = 0;
//    //y_midden_v = 0;
//    schaalfactor = 1;


//    for (int x_voorbeeld = 0; x < voorbeeld_1.Width; x++)
//    {
//        for (int y_voorbeeld = 0; y < voorbeeld_1.Height; y++)
//        {

//            double x_coordinaat = functie_x_voorbeeld(x) + x_midden;
//            double y_coordinaat = functie_y(y) + y_midden;


//            if (mandelGetal(x_coordinaat, y_coordinaat) % 2 == 0 || mandelGetal(x_coordinaat, y_coordinaat) == oneindig)
//            {
//                voorbeeld_1.SetPixel(x, y, Color.Black);
//            }
//            else
//            {
//                voorbeeld_1.SetPixel(x, y, Color.White);
//            }

//        }
//    }
//}
