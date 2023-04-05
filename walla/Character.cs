using Raylib_cs;
public class Character
{
    public Texture2D Sprite = Raylib.LoadTexture("Character.png");
    public static Rectangle hitBox = new Rectangle(0,0, 40, 80);

    public static void Movement(String Direction, int Speed){
    if (Direction == "Horizontal"){
        hitBox.x += Speed;
    }
    if (Direction == "Verticle"){
        hitBox.y += Speed;
    }
    }
}
