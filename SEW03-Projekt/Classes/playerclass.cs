public class Playerobject
{

    private int health; //momentane leben
    private int maxhealth; //maximale leben
    private bool isshootingleft; 
    public string Name { get; set; }
    public int Power { get; set; }
    public int Angle { get; set; }
    public PointF Playerpos { get; set; } //position des Spielers von der Hüfte aus
    public Hitbox Hitbox { get; set; } //hitbox des Spielers
    public int Health { get => health; set => health = value; }
    public int Maxhealth { get => maxhealth; set => maxhealth = value; }
    public bool Isshootingleft { get => isshootingleft; set => isshootingleft = value; }

    private readonly float baseWidth = 40f; //basisbreite der HItbox
    private readonly float baseHeight = 70f; //basishöhe der HItbox


    public Playerobject(int health, int maxhealth, PointF playerpos, bool isshootingleft)
    {
        this.health = health;
        this.maxhealth = maxhealth;
        this.isshootingleft = isshootingleft;
        this.Playerpos = playerpos;

        Power = 50;
        Angle = 45;

        Hitbox = new Hitbox(0, 0, baseWidth, baseHeight);
        UpdatePosition(playerpos, 1f); // Initial mit scale=1

    }
    // aktualisiert Hitbox + Playerpos
    public void UpdatePosition(PointF newPosition, float scale)
    {
        Playerpos = newPosition;

        // Hitbox-Größe neu berechnen
        Hitbox.UpdateSize(baseWidth * scale, baseHeight * scale);

        // Hitbox zentrieren
        Hitbox.CenterAround(newPosition);
    }



    // Hitboxgröße ändern
    public void ResizeHitbox(float width, float height)
    {
        Hitbox.UpdateSize(width, height);
    }
}