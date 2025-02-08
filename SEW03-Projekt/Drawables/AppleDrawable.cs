using Microsoft.Maui.Graphics;
using SEW03_Projekt.Classes;

public class AppleDrawable : IDrawable
{
    private readonly PointF _position;
    private readonly float _size;
    private readonly Color _color;

    Ammunition _ammunition;

    public AppleDrawable(PointF position, float size)
    {
        _ammunition = new Ammunition();
        _position = position;
        _size = size;
        _color = Colors.Red;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = _color;

        // Draw a dot at the specified position with the specified size
        canvas.FillCircle(_position.X, _position.Y, _size);
    }
}