using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Snake : IEnumerable<SnakePart>
    {
        List<SnakePart> snakeParts;
        int size;

        public Snake(Point position, int size)
        {
            snakeParts = new List<SnakePart>();

            this.size = size;

            snakeParts.Add(new SnakePart(position, size));
        }


        public SnakePart this[int index]
        {
            get
            {
                return snakeParts[index];
            }
            set
            {
                snakeParts[index] = value;
            }
        }

        public SnakePart GetHead()
        {
            return snakeParts[0];
        }
        public SnakePart GetTail()
        {
            return snakeParts[snakeParts.Count - 1];
        }

        public void Move()
        {
            for (int i = snakeParts.Count - 1; i > 0; i--)
            {
                snakeParts[i].Position = snakeParts[i - 1].Position;
            }
        }

        public void AddPart()
        {
            SnakePart snakePart = new SnakePart(this.GetTail());
            for (int i = snakeParts.Count - 1; i > 0; i--)
            {
                snakeParts[i].Position = snakeParts[i - 1].Position;
            }
            snakeParts.Add(snakePart);
        }

        public void Draw(Graphics g,MoveDirection moveDirection)
        {
            string filePath = string.Empty;

            switch (moveDirection)
            {
                case MoveDirection.Up:
                    filePath = @"Images\SnakeUp.png";
                    break;
                case MoveDirection.Down:
                    filePath = @"Images\SnakeDown.png";
                    break;
                case MoveDirection.Left:
                    filePath = @"Images\SnakeLeft.png";
                    break;
                case MoveDirection.Right:
                    filePath = @"Images\SnakeRight.png";
                    break;
            }

            foreach (SnakePart snakePart in snakeParts)
            {
                if (snakePart == this.GetHead())
                {
                    snakePart.DrawImage(g,filePath);
                }

                else
                    snakePart.Draw(g, Brushes.DarkGreen);
            }
        }

        public IEnumerator<SnakePart> GetEnumerator()
        {
            return snakeParts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return snakeParts.GetEnumerator();
        }
    }
}
