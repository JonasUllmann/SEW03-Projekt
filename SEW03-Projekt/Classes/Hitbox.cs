public class Hitbox
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }

    public Hitbox(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    // Methode zum Aktualisieren der Position
    public void UpdatePosition(float x, float y)
    {
        X = x;
        Y = y;
    }

    // Methode zum Aktualisieren der Größe
    public void UpdateSize(float width, float height)
    {
        Width = width;
        Height = height;
    }

    // Methode zum Überprüfen, ob ein Punkt in der Hitbox liegt
    public bool Contains(PointF point)
    {
        return point.X >= X && point.X <= X + Width &&
               point.Y >= Y && point.Y <= Y + Height;
    }

    public void CenterAround(PointF position)
    {
        X = position.X - (Width / 2);
        Y = position.Y - (Height / 2);
    }
}