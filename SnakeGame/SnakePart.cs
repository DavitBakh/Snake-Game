using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class SnakePart
    {
        private int size;

        public Point Position { get; set; }


        public SnakePart(Point position, int size)
        { 
            this.Position = position;
            this.size = size;
        }
        public SnakePart(SnakePart snakePart)
        {
            this.Position = snakePart.Position;
            this.size = snakePart.size;
        }
        

        

        public void Draw(Graphics g, Brush brush)
        {
            g.FillRectangle(brush, new Rectangle(new Point(Position.X * size,Position.Y * size), new Size(size, size)));
           
        }
        
        public void DrawImage(Graphics g,string filePath)
        {
            g.DrawImage(Image.FromFile(filePath),this.Position.X*size,this.Position.Y* size, size,size);
        }

        public static bool operator ==(SnakePart a, SnakePart b)
        {
            return a.Position == b.Position && a.size == b.size;
        }
        public static bool operator !=(SnakePart a, SnakePart b)
        {
            return !(a.Position == b.Position && a.size == b.size);
        }

        public static bool operator ==(Point p, SnakePart s)
        {
            return p == s.Position ;
        }
        public static bool operator !=(Point p, SnakePart s)
        {
            return p == s.Position;
        }
    }
}
