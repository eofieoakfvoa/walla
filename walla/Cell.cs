using Raylib_cs;

public class Cell
{
    public bool Walkable;
    public bool Farmable;
    public bool playerStanding;
    public bool interactable;
    public string Texture = "Textures/black.png";

    public Rectangle Position;

    public T changeTile<T>(Cell old) where T : Cell, new()
    {
        T newTile = new T();
        newTile.Position = old.Position;
        return newTile;
    }
}
public class Grass : Cell
{
    public Grass()
    {
        Walkable = true;
        Farmable = true;
        interactable = true;
        Texture = "Textures/Grass.png";
    }
}
public class farmLand : Cell
{
    public farmLand()
    {
      Walkable = true;
      Farmable = false;
      interactable = true;
      Texture = "Textures/farmLand.png";
    }

}
public class water : Cell
{

}
