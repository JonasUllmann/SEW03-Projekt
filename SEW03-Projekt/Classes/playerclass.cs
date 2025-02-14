public class Playerobject
{

    private int health;
    private int maxhealth;
    public string Name { get; set; }
    public int Power { get; set; }
    public int Angle { get; set; }
    public PointF Playerpos { get; set; }
    public Hitbox Hitbox { get; set; }
    public int Health { get => health; set => health = value; }
    public int Maxhealth { get => maxhealth; set => maxhealth = value; }


    public Playerobject(string name, int health, int maxhealth, PointF playerpos, float scale = 1f)
    {
        Name = name;
        this.health = health;
        this.maxhealth = maxhealth;
        Power = 50;
        Angle = 45;
        Playerpos = playerpos;

        // Hitbox mit Skalierung initialisieren
        float baseWidth = 40f;
        float baseHeight = 70f;
        Hitbox = new Hitbox(
            x: playerpos.X - (baseWidth * scale / 2),
            y: playerpos.Y - (baseHeight * scale / 2),
            width: baseWidth * scale,
            height: baseHeight * scale
        );
    }
    // aktualisiert Hitbox + Playerpos
    public void UpdatePosition(PointF newPosition, float scale)
    {
        Playerpos = newPosition;
        // Hitbox zentriert um die Spielerposition
        Hitbox.UpdatePosition(
            newPosition.X - (Hitbox.Width / 2),
            newPosition.Y - (Hitbox.Height / 2)
        );
    }



    // Hitboxgröße ändern
    public void ResizeHitbox(float width, float height)
    {
        Hitbox.UpdateSize(width, height);
    }
}