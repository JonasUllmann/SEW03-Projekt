using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEW03_Projekt.Drawables
{
    public class Draweverything : IDrawable
    {
        private Player1Drawable p1;
        private Player2Drawable p2;

        public Draweverything(Player1Drawable p1, Player2Drawable p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }


        public void Draw(ICanvas canvas, RectF rect)
        {
            p1.Draw(canvas, rect);
            p2.Draw(canvas, rect);
        }
    }


}
