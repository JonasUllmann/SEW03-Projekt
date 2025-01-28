namespace SEW03_Projekt.Drawables
{
    public class Player1Drawable : IDrawable
    {
        private startpage _startpage;
        private PointF torsostart;
        private PointF torsoend;

        public float Player1X; 
        public float Player1Y; 


        public Player1Drawable(startpage startpage)
        {
            _startpage = startpage;

            Player1X = (float)_startpage.Player1.Playerpos.X; 
            Player1Y = (float)_startpage.Player1.Playerpos.Y;

            torsostart = new Point(Player1X, Player1Y - 50);
            torsoend = new Point(Player1X, Player1Y - 100);
        }

        public void Draw(ICanvas canvas, RectF rect)
        {
            if (_startpage == null)
            {
                //verhindert Programmabsturz falls startpage noch nicht initialisiert ist, war nur für Fehlersuche wichtig
                return;
            }

            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 3;

            canvas.DrawLine(Player1X - 20, Player1Y, Player1X, Player1Y - 50);
            canvas.DrawLine(Player1X + 20, Player1Y, Player1X, Player1Y - 50);
            canvas.DrawLine(torsostart, torsoend);
        }
    }
}
