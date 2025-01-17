using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using SEW03_Projekt.Classes;


namespace SEW03_Projekt.Drawables
{
    public class Player1Drawable : IDrawable
    {

        private PointF torsostart; 
        private PointF torsoend;

        public Player1Drawable()
        {


            Playerypos = 300;
            playerxpos = 300;

            torsostart = new PointF(playerxpos, Playerypos - 50);
            torsoend = new PointF(playerxpos, Playerypos - 100);
        }

        public void Draw(ICanvas canvas, RectF rect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 3;

            canvas.DrawLine((Playerxpos - 20), (Playerypos), (Playerxpos), Playerypos - 50); 
            canvas.DrawLine((Playerxpos + 20), Playerypos, (Playerxpos), (Playerypos - 50));
            canvas.DrawLine(torsostart, torsoend);
        }
    }


}
