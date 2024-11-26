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

Opstart_venster();

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
