using System.Drawing;

namespace SkyHop;

public class Bird
{
    public const int Width = 38;
    public const int Height = 30;
    private const float Gravity = 0.45f;
    private const float JumpSpeed = -7.5f;

    public int X { get; private set; }
    public float Y { get; private set; }
    public float VerticalSpeed { get; private set; }

    public Rectangle Bounds
    {
        get { return new Rectangle(X, (int)Y, Width, Height); }
    }

    // Constructor, kuş oluşturulurken başlangıç konumunu belirler.
    public Bird(int x, float y)
    {
        Reset(x, y);
    }

    // Negatif hız, ekran koordinatlarında yukarı hareket anlamına gelir.
    public void Jump()
    {
        VerticalSpeed = JumpSpeed;
    }

    // Her Timer adımında hız ve ardından konum güncellenir.
    public void Update()
    {
        VerticalSpeed += Gravity;
        Y += VerticalSpeed;

        // Kuş ekranın üstünden çıkamaz; üst sınıra değince düşmeye başlar.
        if (Y < 0)
        {
            Y = 0;
            VerticalSpeed = 0;
        }
    }

    // Yeni oyunda önceki konumu ve düşme hızını temizler.
    public void Reset(int x, float y)
    {
        X = x;
        Y = y;
        VerticalSpeed = 0;
    }
}
