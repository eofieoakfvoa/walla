using Raylib_cs;
public class Character
{
    public Texture2D Sprite = Raylib.LoadTexture("Character.png");
    public Rectangle hitBox = new Rectangle(0,0, 40, 80);
}
