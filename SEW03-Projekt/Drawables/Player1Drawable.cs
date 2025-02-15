namespace SEW03_Projekt.Drawables
{
    public class Player1Drawable : IDrawable
    {
        private Playerobject _player;

        public Player1Drawable(Playerobject player)
        {
            _player = player;
        }

        public void Draw(ICanvas canvas, RectF rect)
        {
            if (_player?.Hitbox == null) return;

            // Strichmännchen MIT Skalierung zeichnen
            float scale = _player.Hitbox.Width / 40f; // 40 = baseWidth
            DrawStickFigure(canvas, _player.Playerpos, scale);

            // Hitbox zeichnen
            canvas.StrokeColor = Colors.Blue.WithAlpha(0.5f);
            canvas.DrawRectangle(_player.Hitbox.X, _player.Hitbox.Y, _player.Hitbox.Width, _player.Hitbox.Height);
        }

        private void DrawStickFigure(ICanvas canvas, PointF position, float scale)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 1.5f * scale;

            // Alle Werte skalieren
            float headRadius = 10 * scale;
            float bodyLength = 30 * scale;

            // Kopf
            canvas.DrawCircle(position.X, position.Y - 50 * scale, headRadius);

            // Körper
            canvas.DrawLine(
                position.X,
                position.Y - 40 * scale,
                position.X,
                position.Y - 10 * scale
            );

            // Arme
            canvas.DrawLine(
                position.X - 20 * scale,
                position.Y - 30 * scale,
                position.X + 20 * scale,
                position.Y - 30 * scale
            );

            // Beine
            canvas.DrawLine(
                position.X,
                position.Y - 10 * scale,
                position.X - 20 * scale,
                position.Y + 20 * scale
            );
            canvas.DrawLine(
                position.X,
                position.Y - 10 * scale,
                position.X + 20 * scale,
                position.Y + 20 * scale
            );
        }
    }
}