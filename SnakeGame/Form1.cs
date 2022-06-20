namespace SnakeGame
{
    public partial class Form1 : Form
    {
        int widht = 800;
        int height = 500;
        int sizeOfSides = 20;
        int deltaX;
        int deltaY;
        int speed = 100;
        Point fruit;
        Snake snake;
        int score = 0;

        public Form1()
        {
            InitializeComponent();
            this.Width = widht + 100; //For correction
            this.Height = height + 45;//For correction
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Interval = speed;

            SnakeLaunchPreparation();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (!IsTouchBorders())
            {
                if (IsEatFruit())
                {
                    snake.AddPart();
                    GenerateFruit();
                    score += 10;
                    this.lblScorePoints.Text = score.ToString();
                }
                else
                    snake.Move();

                snake.GetHead().Position = new Point(snake.GetHead().Position.X + deltaX, snake.GetHead().Position.Y + deltaY);

                if (IsEatItself())
                {
                    timer.Stop();
                  DialogResult dialogResult = MessageBox.Show("You lose","Ooooops",MessageBoxButtons.RetryCancel);
                    DialogResultHandler(dialogResult);
                }
                Invalidate();
            }
            else
            {
                timer.Stop();
                DialogResult dialogResult = MessageBox.Show("You lose", "Ooooops", MessageBoxButtons.RetryCancel);
                DialogResultHandler(dialogResult);
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
            snake.Draw(e.Graphics);
            DrawFruit(e.Graphics);
        }


        private bool IsTouchBorders()
        {
            SnakePart head = snake.GetHead();

            if ((head.Position.X + deltaX) < 0 ||
                (head.Position.X + deltaX) * sizeOfSides >= widht ||
                (head.Position.Y + deltaY) < 0 ||
                (head.Position.Y + deltaY) * sizeOfSides >= height)
            {
                return true;
            }

            return false;
        }

        private void DialogResultHandler(DialogResult dialogResult)
        {
            if (dialogResult == DialogResult.Retry)
            {
                SnakeLaunchPreparation();
            }
            else
            {
                Application.Exit();
            }
        }

        private bool IsEatFruit()
        {
            if (snake.GetHead().Position.X + deltaX == fruit.X && snake.GetHead().Position.Y + deltaY == fruit.Y)
            {
                return true;
            }
            return false;
        }

        private bool IsEatItself()
        {
            SnakePart head = snake.GetHead();
            for (int i = 1; i < snake.Count(); i++)
            {
                if (head.Position == snake[i].Position)
                {
                    return true;
                }

            }
            return false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (deltaY != 1)
                    {
                        deltaY = -1;
                        deltaX = 0;
                    }
                    break;
                case Keys.Down:
                    if (deltaY != -1)
                    {
                        deltaY = 1;
                        deltaX = 0;
                    }
                    break;
                case Keys.Left:
                    if (deltaX != 1)
                    {
                        deltaX = -1;
                        deltaY = 0;
                    }
                    break;
                case Keys.Right:
                    if (deltaX != -1)
                    {
                        deltaX = 1;
                        deltaY = 0;
                    }
                    break;
            }
        }
        
        private void SnakeLaunchPreparation()
        {
            this.snake = new Snake(new Point(20, 12), sizeOfSides);
            for (int i = 0; i < 2; i++)
            {
                snake.AddPart();
            }
            GenerateFruit();

            deltaX = 1;
            deltaY = 0;

            score = 0;
            this.lblScorePoints.Text = score.ToString();

            timer.Start();
        }
            

        private void DrawGrid(Graphics g)
        {
            for (int i = 0; i <= widht; i += 20)
            {
                g.DrawLine(Pens.Black, i, 0, i, height);
            }
            for (int i = 0; i <= height; i += 20)
            {
                g.DrawLine(Pens.Black, 0, i, widht, i);
            }
        }


        private void DrawFruit(Graphics g)
        {
            g.FillRectangle(Brushes.Red, new Rectangle(new Point(fruit.X * sizeOfSides, fruit.Y * sizeOfSides), new Size(sizeOfSides, sizeOfSides)));
        }

        private void GenerateFruit()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, widht / sizeOfSides);
            int y = rnd.Next(0, height / sizeOfSides);

            Point position = new Point(x, y);

            foreach (SnakePart part in snake)
            {
                if (position == part.Position)
                { //Haskanum em vor inchqan ocy mecanuma aynqanel mecanuma havanakanutyuny
                  //vor aystex anverj rekursia, aveli chisht stackOverFlow exeption klini bayc es pahin urish ban mtqis chekav
                    GenerateFruit();
                    return;
                }
            }
            fruit = position;
        }
    }
}