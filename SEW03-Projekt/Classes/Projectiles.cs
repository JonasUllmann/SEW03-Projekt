﻿using System;
using System.Collections.Generic;
using System;
using System.Drawing;
using Point = Microsoft.Maui.Graphics.Point;
using PointF = Microsoft.Maui.Graphics.PointF;




namespace SEW03_Projekt.Classes
{
    public class Apple : Ammunition
    {
        public Apple( ) : base()
        {
            name = "Apple";
            radius = 5;
            damage = 15;
            weight = 2; 
        }
    }

    public class Melon : Ammunition
    {
        public Melon( ) : base()
        {
            name = "Melon";
            radius = 15;
            damage = 30;
            weight = 7; 
        }
    }

    public class Wrench : Ammunition
    {
        public Wrench( ) : base()
        {
            name = "Wrench";
            radius = 7;
            damage = 35;
            weight = 10; 
        }
    }

    public class Dung : Ammunition
    {
        public Dung( ) : base()
        {
            name = "Dung";
            radius = 15;
            damage = 10;
            weight = 1; 
        }
    }
}



