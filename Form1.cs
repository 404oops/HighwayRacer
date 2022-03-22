using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HWR
{
    public partial class HWR : Form
    {
        Random r = new Random(); int x;
        public HWR()
        {
            InitializeComponent();
            x = label1.Left;
            timer1.Interval = 160;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (timer1.Interval > 20)
                    timer1.Interval -= 20;
                else
                    timer1.Interval = 1;
                if (timer1.Enabled != true)
                    timer1.Start();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Interval += 10;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (x == label1.Left)
                    label1.Left += 50;
                else if (label1.Left - 50 == x)
                    label1.Left -= 50;
                if (!checkBox3.Checked)
                    timer1.Interval++;
                groupBox1.Text = "Brzina je " + Math.Abs(timer1.Interval - 200);
                int v = Math.Abs(timer1.Interval - 200);
                if (v >= 135 && v <= 200)
                    progressBar1.SetState(2);
                else if (v >= 100 && v <= 135)
                    progressBar1.SetState(3);
                else
                    progressBar1.SetState(1);
                progressBar1.Value = Math.Abs(timer1.Interval - 200) / 2;
                if (timer1.Interval >= 200)
                    timer1.Stop();
                toolStripStatusLabel4.Text = "Interval Tajmera: " + timer1.Interval;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 200;
            toolStripStatusLabel3.Text = "Rucna: Stisnuta";
        }
        private void TurnCar(string direction)
        {
            switch (direction) {
                case "left":
                    carlabel.Top = 10;
                    toolStripStatusLabel2.Text = "Strana Puta: Leva";
                    break;
                case "right":
                    carlabel.Top = 96;
                    toolStripStatusLabel2.Text = "Strana Puta: Desna";
                    break;
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            switch (trackBar1.Value)
            {
                case 0:
                    TurnCar("left");
                    break;
                case 1:
                    TurnCar("right");
                    break;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
                checkBox1.Checked = false;
            if (checkBox1.Checked)
            {
                toolStripStatusLabel1.Text = "Auto: Upaljen";
                timer2.Start();
            }  
            else
            {
                toolStripStatusLabel1.Text = "Auto: Ugasen";
                timer2.Stop();
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                toolStripStatusLabel6.Text = "Tempomat: Upaljen";
            else
                toolStripStatusLabel6.Text = "Tempomat: Ugasen";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                toolStripStatusLabel5.Text = "Kljuc: Ubacen";
            else
                toolStripStatusLabel5.Text = "Kljuc: Nema";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Left -= 10;
            label3.Left -= 10;
            label4.Left -= 10;
            if (timer1.Interval <= 190)
            {
                if (label2.Left <= 10)
                {
                    label2.Left = 807;
                    if (r.Next(2) == 1)
                        label2.Top = 10;
                    else
                        label2.Top = 96;
                }
                if (label3.Left <= 10)
                {
                    label3.Left = 807;
                    if (r.Next(2) == 1)
                        label3.Top = 10;
                    else
                        label3.Top = 96;
                }
                if (label4.Left <= 10)
                {
                    label4.Left = 807;
                    if (r.Next(2) == 1)
                        label4.Top = 10;
                    else
                        label4.Top = 96;
                }
            }


            if (carlabel.Bounds.IntersectsWith(label2.Bounds) || 
                carlabel.Bounds.IntersectsWith(label3.Bounds) ||
                carlabel.Bounds.IntersectsWith(label4.Bounds))
                gameOver();
        }
        private void gameOver()
        {
            checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = false;
            timer1.Stop();
            timer2.Stop();
            carlabel.Visible = label2.Visible = label3.Visible = label4.Visible = false;
            label5.Visible = true;
        }
    }
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}
