namespace SEW03_Projekt.Drawables
{
    public class Player1Drawable : IDrawable
    {
        private startpage _startpage;
        private PointF torsostart;
        private PointF torsoend;

        public Player1Drawable(startpage startpage)
        {
            _startpage = startpage;
            torsostart = new Point(_startpage.Player1X, _startpage.Player1Y - 50);
            torsoend = new Point(_startpage.Player1X, _startpage.Player1Y - 100);
        }

        public void Draw(ICanvas canvas, RectF rect)
        {
            if (_startpage == null)
            {
                Console.WriteLine("Fehler: _startpage ist null. Stellen Sie sicher, dass der richtige Konstruktor verwendet wird.");
                return;
            }

            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 3;

            canvas.DrawLine(_startpage.Player1X - 20, _startpage.Player1Y, _startpage.Player1X, _startpage.Player1Y - 50);
            canvas.DrawLine(_startpage.Player1X + 20, _startpage.Player1Y, _startpage.Player1X, _startpage.Player1Y - 50);
            canvas.DrawLine(torsostart, torsoend);
        }
    }
}
