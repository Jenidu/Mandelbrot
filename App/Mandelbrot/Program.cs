using System;
using System.Drawing;
using System.Windows.Forms;

Form scherm = new Form();
scherm.Text = "Mandelbrot";
scherm.BackColor = Color.Beige;
scherm.ClientSize = new Size(300, 400);

//tekstkaders en knop maken

Label tekst_midden_x = new Label();  
Label tekst_midden_y = new Label();
Label tekst_schaal = new Label();
Label tekst_max_aantal = new Label();
TextBox invoer_midden_x = new TextBox();
TextBox invoer_midden_y = new TextBox();
TextBox invoer_schaal = new TextBox();
TextBox invoer_max_aantal = new TextBox();
Button ga_knop = new Button();
Bitmap plaatje = new Bitmap(280, 280);
Label label_plaatje = new Label();
scherm.Controls.Add(label_plaatje);
label_plaatje.Location = new Point(15, 130);
label_plaatje.Size = new Size(270, 260);
label_plaatje.BackColor = Color.White;
label_plaatje.Image = plaatje;
Graphics gr_plaatje = Graphics.FromImage(plaatje);




Opstart_venster();

Teken_figuur();

Application.Run(scherm);


void Opstart_venster(){
    scherm.Controls.Add(tekst_midden_x);
    tekst_midden_x.Location = new Point(10, 15);
    tekst_midden_x.Size = new Size(80, 15);
    tekst_midden_x.Text = "midden x:";

    scherm.Controls.Add(invoer_midden_x);
    invoer_midden_x.Location = new Point(100, 15);
    invoer_midden_x.Size = new Size(80, 15);

    scherm.Controls.Add(tekst_midden_y);
    tekst_midden_y.Location = new Point(10, 40);
    tekst_midden_y.Size = new Size(80,15);
    tekst_midden_y.Text = "midden y:";

    scherm.Controls.Add(invoer_midden_y);
    invoer_midden_y.Location = new Point(100, 40);
    invoer_midden_y.Size = new Size(80, 15);

    scherm.Controls.Add(tekst_schaal);
    tekst_schaal.Location = new Point(10, 65);
    tekst_schaal.Size = new Size(80, 15);
    tekst_schaal.Text = "schaal";

    scherm.Controls.Add(invoer_schaal);
    invoer_schaal.Location = new Point(100, 65);
    invoer_schaal.Size = new Size(80, 15);

    scherm.Controls.Add(tekst_max_aantal);
    tekst_max_aantal.Location = new Point(10, 90);
    tekst_max_aantal.Size = new Size(80, 25);
    tekst_max_aantal.Text = "max aantal:";

    scherm.Controls.Add(invoer_max_aantal);
    invoer_max_aantal.Location = new Point(100, 90);
    invoer_max_aantal.Size = new Size(80, 15);

    scherm.Controls.Add(ga_knop);
    ga_knop.Location = new Point(225, 90);
    ga_knop.Size = new Size(40, 25);
    ga_knop.Text = "Ga!";
}

double[] functie_f(double x, double y, double a, double b){

    double[] ret = {0, 0};
    ret[0] = a * a - b * b + x;
    ret[1] = 2 * a * b + y;
    return ret;
}


bool mandelgetal(double x, double y)
{
    double[] nieuwe_a_b = [x, y];
    double afstand = 0;
    int n;

    for (n = 0; afstand < 2; n++)
    { 
        nieuwe_a_b = functie_f(x, y, nieuwe_a_b[0], nieuwe_a_b[1]);
        afstand = Math.Sqrt(Math.Pow(nieuwe_a_b[0], 2) + Math.Pow(nieuwe_a_b[1], 2));
    }
   
    return (n % 2 == 0);
}



void Teken_figuur ()
{
    // for (int x = 0; x < plaatje.Width ; x++) {
    //   for (int y = 0; y < plaatje.Height; y++)
    // {
    //   plaatje.SetPixel(x,y, Color.White);
    //}
    //}

    gr_plaatje.FillEllipse(Brushes.AliceBlue, 100, 100, 100, 100);
   
    
}
