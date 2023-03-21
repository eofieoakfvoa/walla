using System.Numerics;
using Raylib_cs;

int ScreenWidth = 1280;
int ScreenHeight = 720;
bool walking;
Raylib.InitWindow(ScreenWidth, ScreenHeight, "game");
Raylib.SetTargetFPS(60);

Character Character = new Character();
Camera2D camera;
camera.zoom = (float)1.25;
camera.rotation = 0;
camera.offset = new Vector2(ScreenWidth / 2, ScreenHeight / 2);
List<Cell> gridcells = new();
Texture2D Grass = Raylib.LoadTexture("Textures/Grass.png");
//Cellsize = xPx x yPx, Mapsize = antal cells, Mapsize * cellsize = pixel width pixel height
int CellSize = 32;
int MapSize = 1000;

for (var x = 0; x < MapSize; x++)
    {
        for (var y = 0; y < MapSize; y++)
        {
        gridcells.Add(new Cell(){
            hitBox = new Rectangle(x*CellSize, y*CellSize, CellSize, CellSize)});
        }
    } 


while (Raylib.WindowShouldClose() == false)
{
    float screenLeft = Character.hitBox.x - ScreenWidth/2;
    float screenTop = Character.hitBox.y + ScreenHeight/2;
    camera.target = new Vector2(Character.hitBox.x, Character.hitBox.y);
    Vector2 MousePosition = Raylib.GetMousePosition();
    

    if(Raylib.IsKeyDown(KeyboardKey.KEY_W)){
        Character.hitBox.y -=4;
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_S)){
        Character.hitBox.y +=4;
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_A)){
        Character.hitBox.x -=4;
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_D)){
        Character.hitBox.x +=4;
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_D)){
        walking = true;
    }
    else{
        walking = false;
    }
    
    //collisions

    
    System.Console.WriteLine(walking);


    Raylib.BeginDrawing();
    Raylib.BeginMode2D(camera);
    Raylib.ClearBackground(Color.WHITE);
    Vector2 worldMousePos = Raylib.GetScreenToWorld2D(MousePosition, camera);

    foreach (var item in gridcells)
    {
        Color color = Color.WHITE;
        if(item.hitBox.x < worldMousePos.X && item.hitBox.x > worldMousePos.X - 32 && item.hitBox.y < worldMousePos.Y && item.hitBox.y > worldMousePos.Y - 32)
        {
            color = Color.ORANGE;
        }


        //Från test har jag märkt att DrawTexturePro tar mycket av datorn, därför har jag gjort så att bara det som skärmen kan se är vad som renderas, pröva flytta ner drawtexturepro ut ur nesten och se hur dina fps droppar
        if(item.hitBox.x < screenLeft + ScreenWidth && item.hitBox.x > screenLeft && item.hitBox.y < screenTop && item.hitBox.y > screenTop - ScreenHeight)
        {
        Raylib.DrawTexturePro(Grass, new Rectangle(0,0,CellSize,CellSize), item.hitBox, new Vector2(0,0),0, color);
        }


    }
    Raylib.DrawTexturePro(Character.Sprite, new Rectangle(0, 0, 80, 80), Character.hitBox, new Vector2(40, 40),0, Color.WHITE) ;

    Raylib.DrawFPS(150,-50);


    Raylib.EndDrawing();
}
