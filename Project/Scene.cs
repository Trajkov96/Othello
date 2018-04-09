using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
   public class Scene
    {
       public int n { get; set; }
       public int counter = 0;
      public List<Circle> Circles { get; set; }
      public static int Green = 1;
      public static int Crni = 0;
      public int[,] Board { get; set; }  //matricata vo koja se cuvaat krugovite
      public Rectangle Border { get; set; }
      public Rectangle Border1 { get; set; }  
      
       public Scene(int n)
       {
           Circles = new List<Circle>();
           this.n = n;
           Board = new int[n,n];
           Border = new Rectangle(75, 75, 90 + (n-2) * 40, 90 + (n-2) * 40);
           Border1 = new Rectangle(65, 65, 110 + (n - 2) * 40, 110 + (n - 2) * 40);
       }

       public void setBoard(int i, int y,int value)
       {
           Board[i, y] = value;
       }

       public void Draw(Graphics g)
       {
           Brush b1 = new SolidBrush(Color.SandyBrown);
           Brush b2 = new SolidBrush(Color.SaddleBrown);
           Pen p = new Pen(Color.Black, 2);

           g.FillRectangle(b2, Border1);
           g.FillRectangle(b1, Border);
           g.DrawRectangle(p, Border1);
           g.FillEllipse(b1, (Border1.Width + Border1.X) - (Border1.Width) / 2 - 3, Border1.Y + 3, 6, 6);
           p.Dispose();
           b1.Dispose();
           b2.Dispose();
           foreach (Circle c in Circles) {
               c.Draw(g);
           }
           
           
       }
       //Metod za proverka na kooordinatite igracot koga kliknal na koj krug tocno kliknal

       public bool CheckCoordinates(int xCoordinate, int yCoordinate,bool Turn)
       {
           if (xCoordinate < 80 || xCoordinate > 80 + n  * 40 || yCoordinate < 80 || yCoordinate > 80 + n  * 40)
           {
               //Da ne pravi nisto 
              // string pom = string.Format("{0} x ---- {1} y", xCoordinate, yCoordinate);
               //MessageBox.Show(pom);
               return false;
           }
           else
           {
               foreach (Circle c in Circles)
               {
                   //Da se proveri koj krug kje bide stisnat i da mu se promeni kind;
                   if ((xCoordinate - c.GetCenterX()) * (xCoordinate - c.GetCenterX()) + (yCoordinate - c.GetCenterY()) * (yCoordinate - c.GetCenterY()) <=c.Radius*c.Radius)
                   {
                       
                       //imame pogodok treba da se smeni kind
                       if (c.Kind != -1)
                       {
                           return false;
                       }
                       counter++;
                       if (Turn)
                       {
                           Board[c.Row, c.Column] = Crni;
                           c.Kind = Crni;
                           CheckIfTraped(Crni, c.Row, c.Column);
                           //CheckIfTraped(Green, c.Row, c.Column + 1);
                           
                       }
                       else
                       {
                           Board[c.Row, c.Column] = Green;
                           c.Kind = Green;
                           CheckIfTraped(Green, c.Row, c.Column);
                           //CheckIfTraped(Crni, c.Row, c.Column + 1);
                           
                       }

                       return true;
                   }

               }
               return false;
           }
       }

       public bool isEnd()
       {
           if (counter == n * n)
               return true;
           else
               return false;
       }

       public int[] GetPoints()
       {
           int[] points = new int[2];
           int player1 = 0;
           int player2 = 0;
           for (int i = 0; i < n; i++)
           {
               for (int j = 0; j < n; j++)
               {
                   if (Board[i, j] == 0)
                   {
                       player1++;
                   }
                   else if (Board[i, j] == 1)
                   {
                       player2++;
                   }
               }
           }
           points[0] = player1;
           points[1] = player2;
           return points;
       }

       //metod za updatuvanje na matricata odnosno ako ima nekoe zarobeno treba da se smeni i vo matricata i vo listata

       void updateMatrix(int row, int column,int color)
       {
           Board[row, column] = color;
           foreach (Circle c in Circles)
           {
               if (c.Row == row && c.Column == column)
               {
                   c.Kind = color;
                   break;
               }
           }
       }

       //Metod dali ima zarobeno nekoe za da se oboi vo taa boja

       public void CheckIfTraped(int color,int row,int column)
       {

        

           for (int i = 0; i < n; i++)              //OD TUKA SE PROVERUVA PO REDOVI
           {
               bool flag = false;
               if (Board[row, i] != -1)
               {
                   flag = true;
                   int boja = Board[row, i];
                   for (int j = n-1; j > i; j--)
                   {
                       if (Board[row, j] == boja)
                       {
                           for (int k = i + 1; k < j; k++)
                           {
                               if (Board[row, k] == -1)
                               {
                                   flag = false;
                                   break;
                               }
                           }
                           if (flag)
                           {
                               for (int k = i + 1; k < j; k++)
                               {
                                   updateMatrix(row, k, boja);
                               }
                           }
                       }
                   }
               }
               if (flag)
               {
                   break;
               }
           }

           for (int i = 0; i < n; i++)          //OD TUKA SE PROVERUVAT PO KOLONI
           {
               bool flag = false;
               if (Board[i, column] != -1)
               {
                   flag = true;
                   int boja = Board[i, column];
                   for (int j = n-1; j > i; j--)
                   {
                       if (Board[j, column] == boja)
                       {
                           for (int k = i + 1; k < j; k++)
                           {
                               if (Board[k, column] == -1)
                               {
                                   flag = false;
                                   break;
                               }
                           }
                           if (flag)
                           {
                               for (int k = i + 1; k < j; k++)
                               {
                                   updateMatrix(k, column, boja);
                               }
                           }
                       }
                   }
               }
               if (flag)
               {
                   break;
               }
           }
           
           
           
       }


    }
}
