using System;
using System.Drawing;
using Point = Microsoft.Maui.Graphics.Point;
using PointF = Microsoft.Maui.Graphics.PointF;


namespace SEW03_Projekt.Classes
{
    public class Ammunition
    {
        

        public string name; //Name des Projektils
        public int damage; // wie viel schaden das Projektil macht
        public int weight; // Gewicht. beinflusst flugbahn, wenig gewicht stark von wind beinflusst, mehr gewicht weniger
        public Microsoft.Maui.Graphics.Color color; // Farbe des Projektils

        private const float g = 9.81f; // Erdanziehungskraft
        
        public Point pos; // Position die zurückgegeben wird

        public Ammunition()
        {
        }

        // Berechnet Koordinaten zum zeitpunkt t
        public PointF projectilepath(float t, int power, int angle, float windspeed, bool shootingLeft, float startX, float startY, float weight)
        {
            //power = (int)(power*2);

            float v0 = power / (1 + (weight / 15f)); // Geschwindigkeit am anfang
            float radAngle = MathF.PI * angle / 180f; // Abschusswinkel in Radianten

            float direction = shootingLeft ? -1 : 1; // wird nach links oder nach rechts geschossen
            float vx = direction * v0 * MathF.Cos(radAngle); // geschwindigkeit in x richtung
            float vy = v0 * MathF.Sin(radAngle); // geschwindigkeit in y richtung


            float windEffect = windspeed / (weight + 1); // Einfluss vom Wind


            float x = startX + (vx * t) + (0.5f * windEffect * t * t); // x(t)

            // Annahme: Y-Achse zeigt nach unten
            float y = startY - (vy * t) + (0.5f * 9.81f * t * t); // y(t)

            return new PointF(x, y);
        }

    }
}
