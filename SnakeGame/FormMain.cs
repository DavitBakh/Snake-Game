namespace SnakeGame
{
    public partial class FormMain : Form
    {

        int speed = 100;
        int size = 25;


        int widht = 800;
        int height = 500;

        int deltaX;
        int deltaY;

        Snake snake;
         Point fruit;
        
        int score = 0;
        MoveDirection moveDirection = MoveDirection.Right;

        bool inhibitor = false;

        public FormMain()
        {
            InitializeComponent();
            this.Width = widht + 100; //For correction
            this.Height = height + 45;//For correction

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Interval = speed;
            Icon icon = new Icon(@"Images\Snake Icon.ico", 100, 100);

            this.Icon = icon;


            SnakeLaunchPreparation();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (inhibitor)
                inhibitor = false;

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
                    DialogResult dialogResult = MessageBox.Show("You lose", "Ooooops", MessageBoxButtons.RetryCancel);
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
            snake.Draw(e.Graphics, moveDirection);
            DrawFruit(e.Graphics);
        }


        private bool IsTouchBorders()
        {
            SnakePart head = snake.GetHead();

            if ((head.Position.X + deltaX) < 0 ||
                (head.Position.X + deltaX) * size >= widht ||
                (head.Position.Y + deltaY) < 0 ||
                (head.Position.Y + deltaY) * size >= height)
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
            if (inhibitor)
                return;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (moveDirection != MoveDirection.Down)
                    {
                        moveDirection = MoveDirection.Up;
                        inhibitor = true;
                    }
                    break;
                case Keys.Down:
                    if (moveDirection != MoveDirection.Up)
                    {
                        moveDirection = MoveDirection.Down;
                        inhibitor = true;

                    }
                    break;
                case Keys.Left:
                    if (moveDirection != MoveDirection.Right)
                    {
                        moveDirection = MoveDirection.Left;
                        inhibitor = true;

                    }
                    break;
                case Keys.Right:
                    if (moveDirection != MoveDirection.Left)
                    {
                        moveDirection = MoveDirection.Right;
                        inhibitor = true;

                    }
                    break;
            }

            switch (moveDirection)
            {
                case MoveDirection.Up:
                    deltaX = 0;
                    deltaY = -1;
                    break;
                case MoveDirection.Down:
                    deltaX = 0;
                    deltaY = 1;
                    break;
                case MoveDirection.Left:
                    deltaX = -1;
                    deltaY = 0;
                    break;
                case MoveDirection.Right:
                    deltaX = 1;
                    deltaY = 0;
                    break;
            }
        }

        private void SnakeLaunchPreparation()
        {
            this.snake = new Snake(new Point(20, 12), size);
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
            Pen pen = new Pen(Color.Black) { Width = 2 };

            g.DrawLine(pen, 1, 0, 1, height);
            g.DrawLine(pen, widht, 0, widht, height);
            g.DrawLine(pen, 0, 0, widht, 0);
            g.DrawLine(pen, 0, height, widht, height);

            g.FillRectangle(Brushes.LightSkyBlue, 0, 0, widht, height);
        }


        private void DrawFruit(Graphics g)
        {
            
            g.DrawImage(Image.FromFile(@"Images\apple.png"), fruit.X * size, fruit.Y * size, size, size);
        }

        private void GenerateFruit()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, widht / size);
            int y = rnd.Next(0, height / size);

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