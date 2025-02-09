using Microsoft.Maui.Graphics;
using SEW03_Projekt;
using SEW03_Projekt.Classes;

public class AppleDrawable : IDrawable
{
    private PointF _position;
    private float _size;
    private Color _color;
    private int _power;
    private int _angle;
    private float _windspeed;
    private bool _shootingLeft;

    private Ammunition _ammunition;

    private float t;

    public AppleDrawable(PointF position, float size, int power, int angle, float windspeed, bool shootingLeft)
    {
        _ammunition = new Ammunition();
        _position = position;
        _size = size;
        _power = power;
        _angle = angle;
        _windspeed = windspeed;
        _shootingLeft = shootingLeft;
        _color = Colors.Red;
        t = 0;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {

        _position = _ammunition.projectilepath(t, _power, _angle, _windspeed, _shootingLeft, _position.X, _position.Y);
        

        canvas.FillColor = _color;

        // Draw a dot at the specified position with the specified size
        canvas.FillCircle(_position.X, _position.Y, _size);

        t++;
    }
}