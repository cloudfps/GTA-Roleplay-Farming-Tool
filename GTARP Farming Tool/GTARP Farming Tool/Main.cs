using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GTARP_Farming_Tool
{
    public partial class Main : Form
    {

        [DllImport("user32.dll")]
static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );
        Point lastPoint;

        public Main()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 18, 18));

        }

        private void Main_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            label4.Text = "Bitte wähle eine Aktions Taste aus.";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            label4.Text = "Bitte wähle eine Start Taste aus.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            label4.Text = "Bitte wähle eine Stop Taste aus.";
        }

        private void sendKey(object sender, KeyEventArgs e)
        {
            button2.Text = $"{e.KeyCode}".ToUpper();
            button2.Enabled = false;
            button2.Enabled = true;
            listBox1.Items.Add("Aktions Taste hinzugefügt: " + $"{e.KeyCode}");
            int nItems = (int)(listBox1.Height / listBox1.ItemHeight);
            listBox1.TopIndex = listBox1.Items.Count - nItems;
            label4.Visible = false;
        }

        private void startKey(object sender, KeyEventArgs e)
        {
            button3.Text = $"{e.KeyCode}".ToUpper();
            button3.Enabled = false;
            button3.Enabled = true;
            listBox1.Items.Add("Start Taste hinzugefügt: " + $"{e.KeyCode}");
            int nItems = (int)(listBox1.Height / listBox1.ItemHeight);
            listBox1.TopIndex = listBox1.Items.Count - nItems;
            label4.Visible = false;

        }

        private void stopKey(object sender, KeyEventArgs e)
        {
            button4.Text = $"{e.KeyCode}".ToUpper();
            button4.Enabled = false;
            button4.Enabled = true;
            listBox1.Items.Add("Stop Taste hinzugefügt: " + $"{e.KeyCode}");
            int nItems = (int)(listBox1.Height / listBox1.ItemHeight);
            listBox1.TopIndex = listBox1.Items.Count - nItems;
            label4.Visible = false;

        }

        private void label8_Click(object sender, EventArgs e)
        {
            hilfe hilfeForm = new hilfe();
            hilfeForm.Show();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            char currentKey = (char)e.KeyCode;
            bool modifier = e.Control || e.Alt || e.Shift;
            bool nonNumber = char.IsLetter(currentKey) ||
                             char.IsSymbol(currentKey) ||
                             char.IsWhiteSpace(currentKey) ||
                             char.IsPunctuation(currentKey);

            if (!modifier && nonNumber)
                e.SuppressKeyPress = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                if (checkBox1.Checked == true)
                {
                    int speed;

                    if (int.TryParse(textBox1.Text, out speed))
                    {
                        if (speed > 0)
                        {
                            timer2.Interval = speed;
                        }
                    }
                    if (button2.Text == "None")
                    {
                        label4.Visible = true;
                        label4.Text = "Bitte wähle eine Aktions Taste aus.";
                        checkBox2.Checked = false;
                        return;
                    };
                    label10.ForeColor = Color.Green;
                    label10.Text = "EIN";
                    label10.Visible = true;
                    timer2.Start();
                }else
                {
                    int speed;

                    if (int.TryParse(textBox1.Text, out speed))
                    {
                        if (speed > 0)
                        {
                            timer1.Interval = speed;
                        }
                    }
                    if (button2.Text == "None")
                    {
                        label4.Visible = true;
                        label4.Text = "Bitte wähle eine Aktions Taste aus.";
                        checkBox2.Checked = false;
                        return;
                    };
                    label10.ForeColor = Color.Green;
                    label10.Text = "EIN";
                    label10.Visible = true;
                    timer1.Start();
                }
            }

        }

        private void SendKey()
        {

            Process p = Process.GetProcessesByName("notepad").FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                SendKeys.SendWait(button2.Text);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.SendWait(button2.Text);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            SendKey();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3.Checked = false;
            checkBox2.Checked = false;
            timer1.Stop();
            timer2.Stop();
            label10.ForeColor = Color.Red;
            label10.Text = "AUS";

        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (button2.Text != "None")
            {
                if (button3.Text != "none")
                {
                    Keys key = (Keys)Enum.Parse(typeof(Keys), button3.Text, true);
                    if (e.KeyCode == key)
                    {
                        if (checkBox1.Checked == true)
                    {
                        int speed;

                        if (int.TryParse(textBox1.Text, out speed))
                        {
                            if (speed > 0)
                            {
                                timer1.Interval = speed;
                            }
                        }
                            label10.ForeColor = Color.Green;
                            label10.Text = "EIN";
                            label10.Visible = true;
                            timer2.Start();
                    }else
                    {
                        int speed;

                        if (int.TryParse(textBox1.Text, out speed))
                        {
                            if (speed > 0)
                            {
                                timer1.Interval = speed;
                            }
                        }
                            label10.ForeColor = Color.Green;
                            label10.Text = "EIN";
                            label10.Visible = true;
                            timer1.Start();
                    }

                    }
                }
            }



            if (button2.Text != "None")
            {
                if (button4.Text != "none")
                {
                    Keys key2 = (Keys)Enum.Parse(typeof(Keys), button4.Text, true);
                    if (e.KeyCode == key2)
                    {
                        checkBox3.Checked = false;
                        checkBox2.Checked = false;
                        timer1.Stop();
                        timer2.Stop();
                        label10.ForeColor = Color.Red;
                        label10.Text = "AUS";
                    }
                }
            }
           
        }
    }
}
