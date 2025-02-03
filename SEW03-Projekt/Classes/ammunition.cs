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
        public Point pos;

        private const float g = 9.81f; 

        public Ammunition()
        {
        }

        // Berechnet die x- und y-Position eines Projektils an einem gewissen Zeitpunkt
        public PointF projectilepath(float t, int power, int angle, float windspeed) //windspeed positiv links nach rechts, negativ rechts nach links
        {
            float v0 = power / (1 + (weight / 5f)); // Masse reduziert Reichweite
            float radAngle = MathF.PI * angle / 180f; // Winkel in Radiant

            float vx = v0 * MathF.Cos(radAngle); // Anfangsgeschwindigkeit x-Richtung
            float vy = v0 * MathF.Sin(radAngle); // Anfangsgeschwindigkeit y-Richtung

            float relativeVx = vx - windspeed; // Wind wirkt auf die x-Richtung

            // Wind beeinflusst leichtere Projektile stärker
            float windEffect = windspeed / (weight + 1); // Stärkere Beeinflussung bei geringem Gewicht

            // Berechnung der Position mit Wind in x-Richtung
            float x = (vx * t) + (windEffect * MathF.Pow(t, 2));
            float y = (vy * t) - (0.5f * g * MathF.Pow(t, 2));

            return new PointF(x, y);
        }
    }
}
