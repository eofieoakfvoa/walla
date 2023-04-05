using Raylib_cs;

public class Cell
{
    public int Index;
    public bool Walkable;
    public bool Farmable;
    public bool playerStanding;
    public bool crop;
    public bool interactable;
    public string Texture = "Textures/black.png";
    public Rectangle hitBox;

    public T changeTile<T>(Cell old) where T : Cell, new()
    {
        T newTile = new T();
        newTile.hitBox = old.hitBox;
        System.Console.WriteLine(newTile);
        return newTile;
    }
}
public class Grass : Cell
{
    public Grass()
    {
        Index = 1;
        Walkable = true;
        Farmable = true;
        crop = false;
        interactable = true;
        Texture = "Textures/Grass.png";
    }
}
public class Rock : Cell
{
    public Rock()
    {
      Index = 2;
      Walkable = true;
      Farmable = false;
      crop = true;
      interactable = true;
      Texture = "Textures/rock.png";
    }

}
public class water : Cell
{

}
