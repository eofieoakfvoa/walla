using System;
using Raylib_cs;

public class items
{
    public bool Stackable;
    public bool Placable;
    public int Stacks;
    public string Name;
    public string Description;
    public string Texture;
    public Color color;
}
public class  Hoe : items
{
    public Hoe()
    {
        Stackable = false;
        Placable = false;
        Name = "Hoe";
        Description = "Is Hoe";
        Texture = "Textures/Hoe.png";
        color = Color.WHITE;

    }
}
public class Seed : items
{
    public Seed()
    {
        Stackable = true;
        Placable = true;
        Name = "Seed";
        Description = "Seed";
        Texture = "Textures/Seed.png";
        color = Color.WHITE;
        
    }
}
public class GrassBlock : items
{
    public GrassBlock()
    {
        Stackable = true;
        Placable = true;
        Name = "GrassBlock";
        Description = "yes";
        Texture = "Textures/Grass.png";
    }
}
