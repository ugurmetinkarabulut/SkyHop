using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SkyHop;

public class Form1 : Form
{
    private const int GroundY = 570;
    private const int PipeSpacing = 290;
    private readonly Bird bird;
    private readonly List<Pipe> pipes = new List<Pipe>();
    private readonly Random random = new Random();
    private readonly System.Windows.Forms.Timer gameTimer;
    private readonly Button startButton;
    private readonly Button restartButton;
    private int score;
    private bool isPlaying;
    private bool isGameOver;
    private bool spacePressed;

    // Arayüz kodla oluşturulur; Designer dosyasına ihtiyaç yoktur.
    public Form1()
    {
        Text = "SkyHop — YMH219";
        ClientSize = new Size(800, 650);
        AutoScaleMode = AutoScaleMode.None;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.LightSkyBlue;
        DoubleBuffered = true;
        KeyPreview = true;

        bird = new Bird(160, 250);
        for (int i = 0; i < 3; i++)
        {
            pipes.Add(new Pipe(390 + i * PipeSpacing, 155 + i * 55, GroundY));
        }

        startButton = new Button();
        startButton.Text = "Oyuna Başla";
        startButton.SetBounds(300, 355, 200, 45);
        startButton.BackColor = Color.Gold;
        startButton.ForeColor = Color.FromArgb(30, 55, 65);
        startButton.TabStop = false;
        startButton.Click += StartButton_Click;
        Controls.Add(startButton);

        restartButton = new Button();
        restartButton.Text = "Yeniden Başlat";
        restartButton.SetBounds(300, 355, 200, 45);
        restartButton.BackColor = Color.Gold;
        restartButton.ForeColor = Color.FromArgb(30, 55, 65);
        restartButton.TabStop = false;
        restartButton.Visible = false;
        restartButton.Click += RestartButton_Click;
        Controls.Add(restartButton);

        gameTimer = new System.Windows.Forms.Timer();
        gameTimer.Interval = 20;
        gameTimer.Tick += GameTimer_Tick;
        KeyDown += Form1_KeyDown;
        KeyUp += Form1_KeyUp;
        Deactivate += Form1_Deactivate;
    }

    private void StartButton_Click(object? sender, EventArgs e)
    {
        StartGame();
    }

    private void RestartButton_Click(object? sender, EventArgs e)
    {
        StartGame();
    }

    // Hem ilk başlangıçta hem yeniden başlatmada bütün oyun bilgileri sıfırlanır.
    private void StartGame()
    {
        gameTimer.Stop();
        score = 0;
        bird.Reset(160, 250);
        for (int i = 0; i < pipes.Count; i++)
        {
            pipes[i].Reset(390 + i * PipeSpacing, 155 + i * 55);
        }
        spacePressed = false;
        isGameOver = false;
        isPlaying = true;
        startButton.Visible = false;
        restartButton.Visible = false;
        ActiveControl = null;
        Focus();
        gameTimer.Start();
        Invalidate();
    }

    // Space basılı tutulursa otomatik tekrar yerine her basışta bir zıplama yapılır.
    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            if (spacePressed)
            {
                return;
            }

            if (!isPlaying && !isGameOver)
            {
                StartGame();
            }

            spacePressed = true;
            if (isPlaying)
            {
                bird.Jump();
            }
        }
    }

    private void Form1_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            spacePressed = false;
        }
    }

    // Başka pencereye geçilirse Space tuşu basılı kalmış sayılmaz.
    private void Form1_Deactivate(object? sender, EventArgs e)
    {
        spacePressed = false;
    }

    // Oyun döngüsü: hareket, çarpışma, skor ve engellerin yeniden yerleştirilmesi.
    private void GameTimer_Tick(object? sender, EventArgs e)
    {
        bird.Update();
        for (int i = 0; i < pipes.Count; i++)
        {
            pipes[i].Move();
        }

        if (CheckCollision())
        {
            EndGame();
            return;
        }

        for (int i = 0; i < pipes.Count; i++)
        {
            Pipe pipe = pipes[i];
            if (!pipe.Scored && pipe.X + Pipe.Width < bird.X)
            {
                score++;
                pipe.Scored = true;
            }

            if (pipe.X + Pipe.Width < 0)
            {
                int rightmostX = pipes[0].X;
                for (int j = 0; j < pipes.Count; j++)
                {
                    if (pipes[j].X > rightmostX)
                    {
                        rightmostX = pipes[j].X;
                    }
                }

                // Boşluğun altında ve üstünde en az 90 piksel engel kalır.
                int gapTop = random.Next(90, GroundY - Pipe.GapHeight - 90 + 1);
                pipe.Reset(rightmostX + PipeSpacing, gapTop);
            }
        }

        Invalidate();
    }

    // Dikdörtgenlerin kesişmesi, kuşun bir engele çarptığını gösterir.
    private bool CheckCollision()
    {
        Rectangle birdBounds = bird.Bounds;
        if (bird.Y + Bird.Height >= GroundY)
        {
            return true;
        }

        for (int i = 0; i < pipes.Count; i++)
        {
            if (birdBounds.IntersectsWith(pipes[i].TopBounds) ||
                birdBounds.IntersectsWith(pipes[i].BottomBounds))
            {
                return true;
            }
        }

        return false;
    }

    // Timer durduğu için Game Over sonrasında hiçbir nesne hareket etmez.
    private void EndGame()
    {
        gameTimer.Stop();
        isPlaying = false;
        isGameOver = true;
        restartButton.Visible = true;
        Invalidate();
    }

    // Ekran her yenilendiğinde oyun nesneleri mevcut konumlarına çizilir.
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        DrawCloud(graphics, 65, 100);
        DrawCloud(graphics, 490, 70);
        for (int i = 0; i < pipes.Count; i++)
        {
            DrawPipe(graphics, pipes[i]);
        }

        graphics.FillRectangle(Brushes.ForestGreen, 0, GroundY, ClientSize.Width, 12);
        graphics.FillRectangle(Brushes.YellowGreen, 0, GroundY + 12,
            ClientSize.Width, ClientSize.Height - GroundY - 12);
        DrawBird(graphics);

        using Font scoreFont = new Font("Segoe UI", 26, FontStyle.Bold);
        using Font smallFont = new Font("Segoe UI", 12);
        graphics.DrawString("SKOR: " + score, scoreFont, Brushes.MidnightBlue, 20, 15);
        graphics.DrawString("SPACE: Zıpla  •  Engellerin arasından geç!",
            smallFont, Brushes.DarkSlateGray, 20, GroundY + 35);

        if (!isPlaying)
        {
            graphics.FillRectangle(Brushes.MidnightBlue, 190, 205, 420, 215);
            using Font titleFont = new Font("Segoe UI", 32, FontStyle.Bold);
            using StringFormat center = new StringFormat();
            center.Alignment = StringAlignment.Center;
            string title = isGameOver ? "Game Over" : "SkyHop";
            string message = isGameOver ? "Skorun: " + score : "Space ile zıpla, gökyüzüne katıl!";
            graphics.DrawString(title, titleFont, Brushes.Gold,
                new Rectangle(190, 225, 420, 65), center);
            graphics.DrawString(message, smallFont, Brushes.White,
                new Rectangle(190, 305, 420, 40), center);
        }
    }

    private void DrawCloud(Graphics graphics, int x, int y)
    {
        graphics.FillEllipse(Brushes.White, x, y + 15, 110, 30);
        graphics.FillEllipse(Brushes.White, x + 20, y, 45, 45);
        graphics.FillEllipse(Brushes.White, x + 55, y + 5, 40, 40);
    }

    // Engel kapakları da çarpışma dikdörtgenlerinin içinde çizilir.
    private void DrawPipe(Graphics graphics, Pipe pipe)
    {
        graphics.FillRectangle(Brushes.LimeGreen, pipe.TopBounds);
        graphics.FillRectangle(Brushes.LimeGreen, pipe.BottomBounds);
        graphics.DrawRectangle(Pens.DarkGreen, pipe.X, 0, Pipe.Width - 1, pipe.GapTop - 1);
        graphics.DrawRectangle(Pens.DarkGreen, pipe.X, pipe.GapTop + Pipe.GapHeight,
            Pipe.Width - 1, GroundY - pipe.GapTop - Pipe.GapHeight - 1);
        graphics.FillRectangle(Brushes.ForestGreen, pipe.X, pipe.GapTop - 18, Pipe.Width, 18);
        graphics.FillRectangle(Brushes.ForestGreen, pipe.X,
            pipe.GapTop + Pipe.GapHeight, Pipe.Width, 18);
    }

    private void DrawBird(Graphics graphics)
    {
        int x = bird.X;
        int y = (int)bird.Y;
        graphics.FillEllipse(Brushes.Gold, x, y, 32, Bird.Height);
        graphics.FillEllipse(Brushes.Orange, x + 4, y + 14, 17, 10);
        graphics.FillEllipse(Brushes.White, x + 20, y + 5, 10, 11);
        graphics.FillEllipse(Brushes.Black, x + 25, y + 8, 4, 5);
        graphics.FillPolygon(Brushes.DarkOrange, new Point[]
        {
            new Point(x + 29, y + 16),
            new Point(x + Bird.Width, y + 20),
            new Point(x + 29, y + 23)
        });
    }

    // Pencere kapatılırken Timer kaynağı serbest bırakılır.
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            gameTimer.Dispose();
        }

        base.Dispose(disposing);
    }
}
