using System.Drawing;
using System.Windows.Forms;

Form scherm = new Form();
scherm.Text = "Mandelbrot";
scherm.BackColor = Color.Beige;
scherm.ClientSize = new Size(300, 400);

Label tekst_midden_x = new Label();  
Label tekst_midden_y = new Label();
Label tekst_schaal = new Label();
Label tekst_max_aantal = new Label();
TextBox invoer_midden_x = new TextBox();
TextBox invoer_midden_y = new TextBox();
TextBox invoer_schaal = new TextBox();
TextBox invoer_max_aantal = new TextBox();
Button ga_knop = new Button();

scherm.Controls.Add(tekst_midden_x);
tekst_midden_x.Location = new Point(10, 15);
tekst_midden_x.Size = new Size(80, 20);
tekst_midden_x.Text = "midden x:";


scherm.Controls.Add(tekst_midden_y);
tekst_midden_y.Location = new Point(10, 30);
tekst_midden_y.Size = new Size(80,20);
tekst_midden_y.Text = "midden y:";

Application.Run(scherm);



