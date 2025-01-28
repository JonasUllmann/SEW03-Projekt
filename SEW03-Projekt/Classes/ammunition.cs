using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SEW03_Projekt.Classes
{
    public class Ammunition
    {

        startpage _startpage;

        public string name;
        public int radius;
        public int damage;
        public int weight;

        public Ammunition(string name, int radius, int damage, int weight, startpage startpage)
        {
            this.name = name;
            this.radius = radius;
            this.damage = damage;
            this.weight = weight;
            this._startpage = startpage;
        }   


        //berechnet die x und y position eines projektils an einem gewissen zeitpunkt
        public PointF projectilepath(float t, int power, int angle)
        {
            float x;
            float y;

            x = power * MathF.Cos(angle) * t;
            y = (power * MathF.Sin(angle) * t) - (float)(0.5 * 9.81 * MathF.Pow(t, 2));

            return new PointF(x, y);
        }

    }
}
