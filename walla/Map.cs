using System;
using Raylib_cs;

public class Map
{
    //Cellsize = xPx x yPx, Mapsize = antal cells, Mapsize * cellsize = pixel width pixel height
    public static List<Cell> gridcells = new();
    public static int MapSize = 100;
    public static int CellSize = 32;
    
    public static void Render()
    {
        for (var x = 0; x < MapSize; x++)
        {
            for (var y = 0; y < MapSize; y++)
            {
            Cell cellAdd = tiles(x,y);
            gridcells.Add(cellAdd);
            cellAdd.Position = new Rectangle(x*CellSize, y*CellSize, CellSize, CellSize);
            }
        }
    } 
    public static Cell tiles(int x, int y)
    {
        if (x >= 0 || y >= 0)
        {
            return new Grass();
        }
        return new Cell();
    }
}
