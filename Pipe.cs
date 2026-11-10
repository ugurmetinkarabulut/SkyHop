using System.Drawing;

namespace SkyHop;

public class Pipe
{
    public const int Width = 70;
    public const int GapHeight = 180;
    private const int MoveSpeed = 4;

    public int X { get; private set; }
    public int GapTop { get; private set; }
    public bool Scored { get; set; }
    private readonly int groundY;

    public Rectangle TopBounds
    {
        get { return new Rectangle(X, 0, Width, GapTop); }
    }

    public Rectangle BottomBounds
    {
        get
        {
            int bottomY = GapTop + GapHeight;
            return new Rectangle(X, bottomY, Width, groundY - bottomY);
        }
    }

    // Bir Pipe nesnesi, üst ve alt engeli birlikte temsil eder.
    public Pipe(int x, int gapTop, int groundY)
    {
        this.groundY = groundY;
        Reset(x, gapTop);
    }

    // Her Timer adımında engel çiftini sola taşır.
    public void Move()
    {
        X -= MoveSpeed;
    }

    // Engeli yeniden konumlandırır ve tekrar puan alınabilmesini sağlar.
    public void Reset(int x, int gapTop)
    {
        X = x;
        GapTop = gapTop;
        Scored = false;
    }
}
