using Microsoft.Maui.Graphics;

public class WrenchDrawable : IDrawable
{
    private readonly PointF _position;
    private readonly float _size;
    private readonly Color _color;

    public WrenchDrawable(PointF position, float size)
    {
        _position = position;
        _size = size;
        _color = Colors.Gray;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = _color;

        // Draw a dot at the specified position with the specified size
        canvas.FillCircle(_position.X, _position.Y, _size);
    }
}
