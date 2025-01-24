using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using SEW03_Projekt;


namespace SEW03_Projekt.Drawables
{
    public class Player1Drawable : IDrawable
    {

        private PointF torsostart; 
        private PointF torsoend;
        MainPage mainpage;


        public Player1Drawable()
        {

        }

        public Player1Drawable(MainPage mainpage)
        {




            torsostart = new Point(mainpage.Player1X, mainpage.Player1Y - 50);
            torsoend = new Point(mainpage.Player1X, mainpage.Player1Y - 100);
        }

        public void Draw(ICanvas canvas, RectF rect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 3;

            canvas.DrawLine((mainpage.Player1X - 20), (mainpage.Player1Y), (mainpage.Player1X), mainpage.Player1Y - 50);
            canvas.DrawLine((mainpage.Player1X + 20), mainpage.Player1Y, (mainpage.Player1X), (mainpage.Player1Y - 50));
            canvas.DrawLine(torsostart, torsoend);
        }
    }


}
