using System;
using System.Collections.Generic;
using System;
using System.Drawing;
using SEW03_Projekt.Classes;
using Point = Microsoft.Maui.Graphics.Point;
using PointF = Microsoft.Maui.Graphics.PointF;




namespace SEW03_Projekt.Classes
{
    public class Apple : Ammunition
    {
        public Apple() : base()
        {
            name = "Apple";
            damage = 15;
            weight = 2;
            color = Colors.Red;
        }
    }
    public class Melon : Ammunition
    {
        public Melon() : base()
        {
            name = "Melon";
            damage = 30;
            weight = 4;
            color = Colors.Green;
        }
    }

    public class Wrench : Ammunition
    {
        public Wrench() : base()
        {
            name = "Wrench";
            damage = 35;
            weight = 5;
            color = Colors.Gray;
        }
    }

    public class Dung : Ammunition
    {
        public Dung() : base()
        {
            name = "Dung";
            damage = 10;
            weight = 1;
            color = Colors.SaddleBrown;
        }
    }
    
}



