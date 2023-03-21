using Raylib_cs;
    public class Cell
    {
        
        public bool Walkable;
        public bool Farmable;
        public bool playerStanding;
        public bool crop;
        public bool interactable;
        public Texture2D Texture;
        public Rectangle hitBox;

    }
    public class Grass : Cell
    {

    }
    public class Treebase : Cell
    {

    }
    public class water : Cell
    {
        
    }
