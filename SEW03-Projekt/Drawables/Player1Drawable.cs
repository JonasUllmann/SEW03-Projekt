﻿namespace SEW03_Projekt.Drawables
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
            if (_player == null)
            {
                return;
            }

            // Debug-Ausgabe
            Console.WriteLine($"Player1 Position: X={_player.Playerpos.X}, Y={_player.Playerpos.Y}");

            // skalierungsfaktor
            float scale = Math.Min(rect.Width, rect.Height) / 500f;

            // Strichmännchen zeichnen
            DrawStickFigure(canvas, (float)_player.Playerpos.X, (float)_player.Playerpos.Y, scale);

            _player.ResizeHitbox(scale * _player.Hitbox.Width, scale * _player.Hitbox.Height);


            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(
                _player.Hitbox.X,
                _player.Hitbox.Y,
                _player.Hitbox.Width,
                _player.Hitbox.Height
            );
        }

        private void DrawStickFigure(ICanvas canvas, float x, float y, float scale)
        {
            // Linienstärke und Farbe
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 3 * scale;

            // Kopf (Kreis)
            float headRadius = 10 * scale;
            canvas.DrawCircle(x, y - 50 * scale, headRadius);

            // Körper (Linie)
            canvas.DrawLine(x, y - 40 * scale, x, y - 10 * scale);

            // Arme (Linien)
            canvas.DrawLine(x - 20 * scale, y - 30 * scale, x + 20 * scale, y - 30 * scale);

            // Beine (Linien)
            canvas.DrawLine(x, y - 10 * scale, x - 20 * scale, y + 20 * scale); // Linkes Bein
            canvas.DrawLine(x, y - 10 * scale, x + 20 * scale, y + 20 * scale); // Rechtes Bein
        }
    }
}