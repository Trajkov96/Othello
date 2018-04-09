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
    public partial class Form1 : Form
    {
        public Scene scene { get; set; }
        public int n { get; set; }
        Font font;
        public int PointsPl1 { get; set; }
        public int PointsPl2 { get; set; }
        public int currentTime1=60;
        public int currentTime2=60;

        public bool Turn { get; set; }  //Civ red e dali player 1-turn e true (crni) ili plaYer 2 turn e false(zeleni)
        public Form1()
        {
            
            InitializeComponent();
            n = 5;
            scene = new Scene(n);
            this.DoubleBuffered = true;
            Turn = true;
            font = new Font("Ariel", 20);
            
            progressBar1.ForeColor = Color.Black;
            progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            progressBar2.ForeColor = Color.White;
            progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
        }

        public void initBoard()
        {
            Turn = true;
            timer1.Start();
            pictureBox1.Image = Project.Properties.Resources.GO;
            pictureBox2.Image = Project.Properties.Resources.STOP;
            
            currentTime1 = currentTime2 = 60;
            progressBar1.Value = 60;
            progressBar2.Value = 60;
            PointsPl1 = 0;
            PointsPl2 = 0;

            //Moze tuka da se stave primer Nekoe si rows pridruzeno so nekoi columns za sekoj krug da se mapira u matrica
            int row = 0;
            int column = 0;
            int y = 100;
            

            //i,y se koordinatite na sekoj krug(x,y)
             bool FLAG=true;
            for (int i = 100; i <= 100+(n-1)*40 && y <= 100+(n-1)*40; i = i + 40)
            {
                Circle c = new Circle(20, i, y, -1,FLAG);
                c.Row = row;
                c.Column = column;
                scene.setBoard(row, column, -1);  //Mapiranje vo matricata
                //Board[row,column] = -1;
                scene.Circles.Add(c);
                column++;
                if (i == 100+(n-1)*40)  //SE PREMESTUVAME FOF NOF RED
                {
                    y = y + 40;
                    i = 60;
                    row++;
                    column = 0;
                    if (n % 2 == 0)         //ZA parniot board da ima razlicni ovakvo kockicki
                    {
                        FLAG = !FLAG;
                    }
                }

                FLAG=!FLAG;
            }

            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            startingForm sf = new startingForm();
            DialogResult result = sf.ShowDialog();
            if (result == DialogResult.OK)
            {
                initBoard();
                timer1.Start();
            }
            else
                this.Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.Clear(Color.White);
            scene.Draw(e.Graphics);
            Brush b1=new SolidBrush(Color.Black);
            Brush b2=new SolidBrush(Color.White);
            e.Graphics.DrawString(string.Format("Points: {0}", PointsPl1), font, b1, 540, 130);
            e.Graphics.DrawString(string.Format("Points: {0}", PointsPl2), font, b2, 780, 130);
            e.Graphics.DrawString(string.Format("Time: {0}", currentTime1), font, b1, 540, 180);
            e.Graphics.DrawString(string.Format("Time: {0}", currentTime2), font, b2, 780, 180);
            b1.Dispose();
            b2.Dispose();
        }

      

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (scene.CheckCoordinates(e.X, e.Y, Turn))
            {
                Invalidate();   
                Turn = !Turn;
                if (Turn)
                {
                    pictureBox1.Image = Project.Properties.Resources.GO;
                    pictureBox2.Image = Project.Properties.Resources.STOP;
                }
                else
                {
                    pictureBox1.Image = Project.Properties.Resources.STOP;
                    pictureBox2.Image = Project.Properties.Resources.GO;
                }
                
                UpdatePoints();
                
                //Treba da se proverat site sosediii ako ima nekoj sto e vo druga boja da go oboi i nego
            }   
            
            if (scene.isEnd())
            {
                winnerBox();
            }
        }

        private void winnerBox()
        {
            
            string winner = PointsPl1 > PointsPl2 ? "Player1" : "Player2";
            MessageBox.Show(string.Format("The winner is {0}",winner));
            timer1.Stop();
            
        }

       

        private void UpdatePoints()
        {
            int[] points = new int[2];
            points = scene.GetPoints();
            
            PointsPl1 = points[0];
            PointsPl2 = points[1];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Turn)
            {
                progressBar1.Value = currentTime1;
                currentTime1--;
            }
            else
            {
                progressBar2.Value = currentTime2;
                currentTime2--;
            }
            if (currentTime1 == 0)
            {
                timer1.Stop();
                MessageBox.Show("The winner is Player2");
            }
            else
                if(currentTime2==0)
                {
                    timer1.Stop();
                    MessageBox.Show("The winner is Player1");
                }
            Invalidate(true);
        }

        

        
        private void board7x7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            n = 7;
            board5x5ToolStripMenuItem.Checked = false;
            board6x6ToolStripMenuItem.Checked = false;
            board7x7ToolStripMenuItem.Checked = true;
            scene = new Scene(n);
            initBoard();
            Invalidate(true);

        }

        private void board6x6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            n = 6;
            board5x5ToolStripMenuItem.Checked = false;
            board6x6ToolStripMenuItem.Checked = true;
            board7x7ToolStripMenuItem.Checked = false;
            scene = new Scene(n);
            initBoard();
            Invalidate(true);

        }

        private void board5x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            n = 5;
            board5x5ToolStripMenuItem.Checked = true;
            board6x6ToolStripMenuItem.Checked = false;
            board7x7ToolStripMenuItem.Checked = false;
            scene = new Scene(n);
            initBoard();
            Invalidate(true);

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scene = new Scene(n);
            initBoard();
            Invalidate(true);

        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstructionsForm inst = new InstructionsForm();
            inst.Show();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        
    }
}
