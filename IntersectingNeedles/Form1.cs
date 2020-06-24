using System;
using System.Drawing;
using System.Windows.Forms;

namespace IntersectingNeedles
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();
        private int eventCount = 0;
        private int crossCount = 0;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            for (var i = 0; i < 6; i++)
            {
                graphics.DrawLine(new Pen(Color.Black), 0,  i *pictureBox1.Height/6, pictureBox1.Width, i * pictureBox1.Height/6);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            var length = pictureBox1.Height / 6 / 2;
            var x = rnd.Next(pictureBox1.Width - 2 * length) + length;
            var y = rnd.Next( pictureBox1.Height - 2 *length) + length;

            var angle = rnd.Next(0, 180);
            var x1 = (int) (x + length * Math.Sin(angle * Math.PI / 180.0));
            var y1 = (int) (y + length * Math.Cos(angle * Math.PI / 180.0));

            var r = rnd.Next(0, 255);
            var g = rnd.Next(0, 255);
            var b = rnd.Next(0, 255);

            graphics.DrawLine(new Pen(Color.FromArgb(255, r, g, b)), x, y, x1, y1);
            pictureBox1.Invalidate();

            for (int i = 0; i < 6; i++)
            {
                var lineY = i * pictureBox1.Height / 6;
                if (lineY >= y1 && lineY <= y)
                {
                    crossCount++;
                }
                if (lineY <= y1 && lineY >= y)
                {
                    crossCount++;
                }
            }
            eventCount++;

            label3.Text = eventCount.ToString();
            label4.Text = crossCount.ToString();

            try
            {
                double Pi = 2 * length / (pictureBox1.Height * crossCount / 6 / (double)eventCount);
                Text = Pi.ToString();
            }
            catch (Exception exception)
            {
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }
    }
}
