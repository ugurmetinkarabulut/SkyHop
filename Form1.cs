using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SkyHop;

public class Form1 : Form
{
    private const int GroundY = 570;
    private const int PipeSpacing = 290;
    private static readonly Rectangle MenuBounds = new Rectangle(204, 174, 392, 256);
    private static readonly Color MenuColor = Color.FromArgb(24, 48, 65);
    private static readonly Color AccentColor = Color.FromArgb(255, 212, 92);
    private readonly Font menuButtonFont = new Font("Segoe UI", 17, FontStyle.Bold, GraphicsUnit.Pixel);
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
        BackColor = Color.FromArgb(202, 237, 252);
        DoubleBuffered = true;
        KeyPreview = true;

        bird = new Bird(160, 250);
        for (int i = 0; i < 3; i++)
        {
            pipes.Add(new Pipe(390 + i * PipeSpacing, 155 + i * 55, GroundY));
        }

        startButton = new Button();
        startButton.Text = "Oyuna Başla";
        StyleMenuButton(startButton);
        startButton.TabStop = false;
        startButton.Click += StartButton_Click;
        Controls.Add(startButton);

        restartButton = new Button();
        restartButton.Text = "Yeniden Başlat";
        StyleMenuButton(restartButton);
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

    // İki buton da panelde aynı konumu ve renkleri kullanır.
    private void StyleMenuButton(Button button)
    {
        button.SetBounds(MenuBounds.Left + 86, MenuBounds.Top + 148, 220, 46);
        button.Font = menuButtonFont;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderColor = Color.FromArgb(180, 125, 37);
        button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 226, 133);
        button.FlatAppearance.MouseDownBackColor = Color.FromArgb(235, 181, 69);
        button.BackColor = AccentColor;
        button.ForeColor = MenuColor;
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
        // Basılı Space yeniden başlatmada yeni basış sayılmaz; KeyUp beklenir.
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

        DrawSky(graphics);
        for (int i = 0; i < pipes.Count; i++)
        {
            DrawPipe(graphics, pipes[i]);
        }

        DrawGround(graphics);
        DrawBird(graphics);

        if (isPlaying)
        {
            DrawScore(graphics);
        }
        else
        {
            DrawMenu(graphics);
        }
    }

    private void DrawSky(Graphics graphics)
    {
        Rectangle sky = new Rectangle(0, 0, ClientSize.Width, GroundY);
        using LinearGradientBrush skyBrush = new LinearGradientBrush(sky,
            Color.FromArgb(126, 199, 243), Color.FromArgb(229, 249, 251), LinearGradientMode.Vertical);
        using SolidBrush sun = new SolidBrush(Color.FromArgb(255, 231, 149));
        graphics.FillRectangle(skyBrush, sky);
        graphics.FillEllipse(sun, 610, 57, 66, 66);

        DrawCloud(graphics, 61, 124);
        DrawCloud(graphics, 481, 96);
        DrawCloud(graphics, 707, 231);
    }

    private void DrawCloud(Graphics graphics, int x, int y)
    {
        using SolidBrush shade = new SolidBrush(Color.FromArgb(205, 234, 248));
        graphics.FillEllipse(shade, x, y + 21, 115, 29);
        graphics.FillEllipse(Brushes.White, x + 2, y + 16, 111, 27);
        graphics.FillEllipse(Brushes.White, x + 19, y + 5, 35, 35);
        graphics.FillEllipse(Brushes.White, x + 42, y, 44, 42);
        graphics.FillEllipse(Brushes.White, x + 73, y + 12, 27, 29);
    }

    private void DrawGround(Graphics graphics)
    {
        using SolidBrush baseColor = new SolidBrush(Color.FromArgb(39, 78, 66));
        using SolidBrush grass = new SolidBrush(Color.FromArgb(116, 186, 78));
        using SolidBrush grassLight = new SolidBrush(Color.FromArgb(177, 220, 104));
        using SolidBrush groundDetail = new SolidBrush(Color.FromArgb(65, 105, 86));
        graphics.FillRectangle(baseColor, 0, GroundY, ClientSize.Width, ClientSize.Height - GroundY);
        graphics.FillRectangle(grass, 0, GroundY, ClientSize.Width, 17);
        graphics.FillRectangle(grassLight, 0, GroundY, ClientSize.Width, 4);

        for (int x = 16; x < ClientSize.Width - 20; x += 43)
        {
            graphics.FillRectangle(groundDetail, x, GroundY + 34, 20, 3);
            graphics.FillRectangle(groundDetail, x + 14, GroundY + 58, 9, 3);
        }
    }

    // Engel kapakları da çarpışma dikdörtgenlerinin içinde çizilir.
    private void DrawPipe(Graphics graphics, Pipe pipe)
    {
        DrawPipeSection(graphics, pipe.TopBounds, true);
        DrawPipeSection(graphics, pipe.BottomBounds, false);
    }

    private void DrawPipeSection(Graphics graphics, Rectangle bounds, bool isTop)
    {
        using SolidBrush edge = new SolidBrush(Color.FromArgb(26, 83, 57));
        using SolidBrush body = new SolidBrush(Color.FromArgb(83, 172, 90));
        using SolidBrush light = new SolidBrush(Color.FromArgb(153, 215, 106));
        using SolidBrush shade = new SolidBrush(Color.FromArgb(48, 125, 74));
        graphics.FillRectangle(edge, bounds);
        graphics.FillRectangle(body, bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4);
        graphics.FillRectangle(light, bounds.X + 7, bounds.Y + 2, 9, bounds.Height - 4);
        graphics.FillRectangle(shade, bounds.Right - 15, bounds.Y + 2, 13, bounds.Height - 4);

        int capY = isTop ? bounds.Bottom - 18 : bounds.Top;
        graphics.FillRectangle(edge, bounds.X, capY, bounds.Width, 18);
        graphics.FillRectangle(shade, bounds.X + 2, capY + 2, bounds.Width - 4, 14);
        graphics.FillRectangle(light, bounds.X + 6, capY + 3, bounds.Width - 12, 3);
    }

    private void DrawBird(Graphics graphics)
    {
        Rectangle bounds = bird.Bounds;
        int x = bounds.X;
        int y = bounds.Y;
        using SolidBrush feathers = new SolidBrush(AccentColor);
        using SolidBrush orange = new SolidBrush(Color.FromArgb(244, 139, 57));
        using Pen outline = new Pen(Color.FromArgb(108, 68, 38), 1.5f);
        Point[] beak =
        {
            new Point(x + 27, y + 14),
            new Point(x + bounds.Width - 2, y + 18),
            new Point(x + 27, y + 22)
        };
        graphics.FillPolygon(orange, beak);
        graphics.DrawPolygon(outline, beak);
        graphics.FillEllipse(feathers, x + 1, y + 1, 30, bounds.Height - 2);
        graphics.DrawEllipse(outline, x + 1, y + 1, 30, bounds.Height - 2);
        graphics.FillEllipse(orange, x + 4, y + 14, 17, 10);
        graphics.DrawEllipse(outline, x + 4, y + 14, 17, 10);
        graphics.FillEllipse(Brushes.White, x + 19, y + 5, 10, 12);
        graphics.FillEllipse(Brushes.Black, x + 24, y + 8, 4, 6);
    }

    private void DrawScore(Graphics graphics)
    {
        using SolidBrush panel = new SolidBrush(MenuColor);
        using SolidBrush accent = new SolidBrush(AccentColor);
        using Font labelFont = new Font("Segoe UI", 13, FontStyle.Bold, GraphicsUnit.Pixel);
        using Font scoreFont = new Font("Segoe UI", 23, FontStyle.Bold, GraphicsUnit.Pixel);
        using StringFormat center = new StringFormat();
        center.Alignment = StringAlignment.Center;
        center.LineAlignment = StringAlignment.Center;

        graphics.FillRectangle(panel, 16, 16, 132, 65);
        graphics.DrawString("SKOR", labelFont, accent, new Rectangle(16, 20, 132, 20), center);
        graphics.DrawString(score.ToString(), scoreFont, Brushes.White, new Rectangle(16, 40, 132, 34), center);
    }

    // Başlık, açıklama ve Space ipucu, butonun dışında kalan alanlara çizilir.
    private void DrawMenu(Graphics graphics)
    {
        using SolidBrush panel = new SolidBrush(MenuColor);
        using SolidBrush shadow = new SolidBrush(Color.FromArgb(65, MenuColor));
        using SolidBrush accent = new SolidBrush(AccentColor);
        using Font titleFont = new Font("Segoe UI", 46, FontStyle.Bold, GraphicsUnit.Pixel);
        using Font bodyFont = new Font("Segoe UI", 18, FontStyle.Regular, GraphicsUnit.Pixel);
        using Font hintFont = new Font("Segoe UI", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        using StringFormat center = new StringFormat();
        center.Alignment = StringAlignment.Center;
        center.LineAlignment = StringAlignment.Center;

        int x = MenuBounds.Left;
        int y = MenuBounds.Top;
        int width = MenuBounds.Width;
        graphics.FillRectangle(shadow, x + 4, y + 6, width, MenuBounds.Height);
        graphics.FillRectangle(panel, MenuBounds);
        graphics.FillRectangle(accent, x, y, width, 4);

        string title = isGameOver ? "Game Over" : "SkyHop";
        string message = isGameOver ? "Son skor: " + score : "Engellerin arasından geç.";
        graphics.DrawString(title, titleFont, Brushes.White, new Rectangle(x, y + 20, width, 65), center);
        graphics.DrawString(message, bodyFont, Brushes.White, new Rectangle(x, y + 91, width, 32), center);
        if (!isGameOver)
        {
            graphics.DrawString("SPACE ile zıpla", hintFont, accent, new Rectangle(x, y + 207, width, 26), center);
        }
    }

    // Pencere kapatılırken Timer ve butonların yazı tipi serbest bırakılır.
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            gameTimer.Dispose();
        }

        base.Dispose(disposing);
        if (disposing)
        {
            menuButtonFont.Dispose();
        }
    }
}
