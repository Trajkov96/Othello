using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project
{
    public partial class startingForm : Form
    {
        public startingForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

       /* private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InstructionsForm inst = new InstructionsForm();
            inst.Show();
        }*/

        private void startingForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            InstructionsForm inst = new InstructionsForm();
            inst.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Width = pictureBox1.Width + 10;
            pictureBox1.Height = pictureBox1.Height + 10;
            
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Width = pictureBox1.Width - 10;
            pictureBox1.Height = pictureBox1.Height - 10;
            if (pictureBox1.Width < 197 || pictureBox1.Height < 61)
            {
                pictureBox1.Width = 197;
                pictureBox1.Height = 61;
            }
            
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Width = pictureBox2.Width + 10;
            pictureBox2.Height = pictureBox2.Height + 10;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Width = pictureBox2.Width - 10;
            pictureBox2.Height = pictureBox2.Height - 10;
            if (pictureBox2.Width < 197 || pictureBox2.Height < 61)
            {
                pictureBox2.Width = 197;
                pictureBox2.Height = 61;
            }
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Width = pictureBox3.Width + 10;
            pictureBox3.Height = pictureBox3.Height + 10;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Width = pictureBox3.Width - 10;
            pictureBox3.Height = pictureBox3.Height - 10;

            if (pictureBox3.Width < 197 || pictureBox3.Height < 61)
            {
                pictureBox3.Width = 197;
                pictureBox3.Height = 61;
            }
        }

        
    }
}
