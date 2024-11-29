using System;
using System.Drawing;
using System.Windows.Forms;

int[] Venster_Grootte = {300, 400};
int[] StartBM = {15, 130};  /* Start positie van bitmap, relatief tot de venster */
int[] BM_Grootte = {270, 260};  /* Bitmap grootte */

/* Tekst boxen */
int[] StartVensterTekst = {10, 15};
int[] StartTekstBox = {100, 15};  /* Start positie tekst box */
int[] TekstBox_Grootte = {80, 15};
int[] TekstBox_PosVerschil = {0, 25};

const int MaxMandelIteraties = 10000;  /* Maximale keren dat een de mandelbrot itereert */


Form scherm = new Form();
scherm.Text = "Mandelbrot";
scherm.BackColor = Color.Beige;
scherm.ClientSize = new Size(Venster_Grootte[0], Venster_Grootte[1]);

Label tekst_midden_x = new Label();  
Label tekst_midden_y = new Label();
Label tekst_schaal = new Label();
Label tekst_max_aantal = new Label();
TextBox invoer_midden_x = new TextBox();
TextBox invoer_midden_y = new TextBox();
TextBox invoer_schaal = new TextBox();
TextBox invoer_max_aantal = new TextBox();
Button ga_knop = new Button();
Bitmap plaatje = new Bitmap(BM_Grootte[0], BM_Grootte[1]);
Label label_plaatje = new Label();
scherm.Controls.Add(label_plaatje);
label_plaatje.Location = new Point(StartBM[0], StartBM[1]);
label_plaatje.Size = new Size(BM_Grootte[0], BM_Grootte[1]);
label_plaatje.BackColor = Color.White;
label_plaatje.Image = plaatje;
Graphics gr_plaatje = Graphics.FromImage(plaatje);


opstartVenster();

tekenFiguur();

Application.Run(scherm);

void opstartVenster(){

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

    scherm.Controls.Add(invoer_midden_y);
    invoer_midden_y.Location = new Point(StartTekstBox[0], StartTekstBox[1] + TekstBox_PosVerschil[1]);
    invoer_midden_y.Size = new Size(TekstBox_Grootte[0], TekstBox_Grootte[1]);

    scherm.Controls.Add(invoer_schaal);
    invoer_schaal.Location = new Point(StartTekstBox[0], StartTekstBox[1] + TekstBox_PosVerschil[1] * 2);
    invoer_schaal.Size = new Size(TekstBox_Grootte[0], TekstBox_Grootte[1]);

    scherm.Controls.Add(invoer_max_aantal);
    invoer_max_aantal.Location = new Point(StartTekstBox[0], StartTekstBox[1] + TekstBox_PosVerschil[1] * 3);
    invoer_max_aantal.Size = new Size(TekstBox_Grootte[0], TekstBox_Grootte[1]);

    /* Knop aanmaken */
    scherm.Controls.Add(ga_knop);
    ga_knop.Location = new Point(225, 90);
    ga_knop.Size = new Size(40, 25);
    ga_knop.Text = "Ga!";
}

double[] functieF(double x, double y, double a, double b){

    double[] ret = {0, 0};
    ret[0] = a * a - b * b + x;
    ret[1] = 2 * a * b + y;
    return ret;  /* Geef nieuwe a,b terug */
}

int mandelGetal(double a, double b){

    double[] a_b = [a, b];
    double afstand = 10;
    int n;

    for (n = 0; afstand >= 2 && n < MaxMandelIteraties; n++)
    { 
        a_b = functieF(0, 0, a_b[0], a_b[1]);  /* Nieuwe a,b */
        afstand = Math.Sqrt(Math.Pow(a_b[0], 2) + Math.Pow(a_b[1], 2));  /* afstand = sqrt(a^2 + b^2) */
    }

    return n;
}

void tekenFiguur(){

    for (int x = 0; x < plaatje.Width; x++)
    {
        for (int y = 0; y < plaatje.Height; y++)
        {
            if ((mandelGetal(x, y) % 2) == 0)  /* Mandelgetal is even */
            {
                gr_plaatje.FillRectangle(Brushes.Black, x, y, 1, 1);  /* Maak pixel x,y zwart */
            }
            else  /* Mandelgetal is oneven */
            {
                gr_plaatje.FillRectangle(Brushes.White, x, y, 1, 1);  /* Maak pixel x,y wit */
            }
        }
    }
}
