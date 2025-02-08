using Microsoft.Maui.Graphics;

public class MelonDrawable : IDrawable
{
    private readonly PointF _position;
    private readonly float _size;
    private readonly Color _color;

    public MelonDrawable(PointF position, float size)
    {
        _position = position;
        _size = size;
        _color = Colors.DarkGreen;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = _color;

        // Draw a dot at the specified position with the specified size
        canvas.FillCircle(_position.X, _position.Y, _size);
    }
}
