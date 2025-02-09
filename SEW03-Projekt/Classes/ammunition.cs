using System;
using System.Drawing;
using Point = Microsoft.Maui.Graphics.Point;
using PointF = Microsoft.Maui.Graphics.PointF;


namespace SEW03_Projekt.Classes
{
    public class Ammunition
    {
        

        public string name;
        public int radius;
        public int damage;
        public int weight;
        public string source;

        private const float g = 9.81f;
        
        public Point pos;

        public Ammunition()
        {
        }

        // Berechnet die x- und y-Position eines Projektils an einem gewissen Zeitpunkt
        public PointF projectilepath(float t, int power, int angle, float windspeed, bool shootingLeft, float startX, float startY)
        {
            float v0 = power / (1 + (weight / 5f));
            float radAngle = MathF.PI * angle / 180f;

            float direction = shootingLeft ? -1 : 1; // -1 für links, 1 für rechts

            float vx = direction * v0 * MathF.Cos(radAngle);
            float vy = v0 * MathF.Sin(radAngle);

            float windEffect = windspeed / (weight + 1);

            // Berechnung der Flugbahn mit Startposition
            float x = startX + (vx * t) + (windEffect * MathF.Pow(t, 2));
            float y = startY + (vy * t) - (0.5f * 9.81f * MathF.Pow(t, 2));

            return new PointF(x, y);
        }

    }
}
