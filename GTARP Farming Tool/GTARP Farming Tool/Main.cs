using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GTARP_Farming_Tool
{
    public partial class Main : Form
    {
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

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {

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
    }
}
