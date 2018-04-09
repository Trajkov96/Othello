using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Project
{
   public class Circle
    {
       public int Radius { get; set; }
       public int X {get;set;}
       public int Y {get;set;}
       public int Kind { get; set; }
       public int Row { get; set; }
       public int Column { get; set; }
       public bool Flag { get; set; }
       public Circle(int r, int x, int y,int k,bool f) {
           Radius = r;
           X = x;
           Y = y;
           Kind = k;
           Flag = f;
       }

       public int GetCenterX()
       {
           return X ;
       }

       public int GetCenterY()
       {
          return Y;
           
       }

       public void Draw(Graphics g, Color c) {
           Pen p = new Pen(c,2);
           g.DrawEllipse(p, X-Radius, Y-Radius, Radius * 2, Radius * 2);
           p.Dispose();
       }

       public void Draw(Graphics g) {

           if (Flag)
           {
               Brush b = new SolidBrush(Color.ForestGreen);
               g.FillRectangle(b, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
               b.Dispose();
           }
           else
           {
               Brush b = new SolidBrush(Color.LightGreen);
               g.FillRectangle(b, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
               b.Dispose();

           }

           if (Kind == 0)
           {
               Brush b = new SolidBrush(Color.Black);
               g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
               b.Dispose();
           }
           else if (Kind == 1)
           {
               Brush b = new SolidBrush(Color.White);
               g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
               b.Dispose();
           }
          
           
           
       }

       

    }
}
