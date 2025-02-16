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
        public Microsoft.Maui.Graphics.Color color;

        private const float g = 9.81f;
        
        public Point pos;

        public Ammunition()
        {
        }

        // Berechnet die x- und y-Position eines Projektils an einem gewissen Zeitpunkt
        public PointF projectilepath(float t, int power, int angle, float windspeed, bool shootingLeft, float startX, float startY, float weight)
        {
            //power = (int)(power*2);

            // Keine Skalierung von power, es sei denn, sie ist beabsichtigt
            float v0 = power / (1 + (weight / 15f));
            float radAngle = MathF.PI * angle / 180f;

            float direction = shootingLeft ? -1 : 1;
            float vx = direction * v0 * MathF.Cos(radAngle);
            float vy = v0 * MathF.Sin(radAngle);

            // Wind als Beschleunigung (z. B. windspeed = m/s²)
            float windEffect = windspeed / (weight + 1);

            // Korrekte Anwendung von Wind (0.5 * a * t²)
            float x = startX + (vx * t) + (0.5f * windEffect * t * t);

            // Annahme: Y-Achse zeigt nach unten
            float y = startY - (vy * t) + (0.5f * 9.81f * t * t);

            return new PointF(x, y);
        }

    }
}
