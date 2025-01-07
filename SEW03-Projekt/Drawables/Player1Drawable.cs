using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEW03_Projekt.Drawables
{
    public class Player1Drawable : IDrawable
    {

        private int playerxpos;
        private int playerypos;

        new private PointF torsostart; 
        new private PointF torsoend;


        public int Playerxpos { get => playerxpos; set => playerxpos = value; }
        public int Playerypos { get => playerypos; set => playerypos = value; }

        public Player1Drawable()
        {
            playerxpos = 300;
            playerypos = 300;

            torsostart = new PointF(playerxpos, playerypos - 50);
            torsoend = new PointF(playerxpos, playerypos - 100);
        }

        public Player1Drawable(int playerxpos, int playerypos)
        {
            this.playerxpos = playerxpos;
            this.playerypos = playerypos;

            torsostart = new PointF(playerxpos, playerypos - 50);
            torsoend = new PointF(playerxpos, playerypos - 100);
        }



        public void Draw(ICanvas canvas, RectF rect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 3;

            canvas.DrawLine((Playerxpos), (Playerypos + 50), (Playerxpos - 20), Playerypos); 
            //canvas.DrawLine((Playerxpos - 20), Playerypos, (Playerxpos), (Playerypos + 50));
            canvas.DrawLine(torsostart, torsoend);
        }
    }


}
