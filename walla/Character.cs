using Raylib_cs;
public class Character
{
    public Texture2D Sprite = Raylib.LoadTexture("Textures/Character.png");
    public static Rectangle Position = new Rectangle(0,0, 40, 80);

    public static void Movement(string Direction, int Speed){
    if (Direction == "Horizontal"){
        Position.x += Speed;
    }
    if (Direction == "Verticle"){
        Position.y += Speed;
    }
    }
}
