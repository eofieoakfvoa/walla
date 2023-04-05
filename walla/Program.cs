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


Vector2 previousMousePos = new Vector2(0, 0);
Cell selectedGrid = new Cell();
int selectedGridIndex = 0;
bool gridIsSelected = false;


TextureManager textureManager = new TextureManager();
Inventory InventoryManager = new Inventory();


int SelectedInventory = 1;


string Scene = "Start";

Seed See1d = new Seed();
bool initialize = true;
while (Raylib.WindowShouldClose() == false)
{
    Vector2 MousePosition = Raylib.GetMousePosition();
    if (Scene == "Start")
    {
        if (initialize == true){
            Map.Render();

            initialize = false;
        }
    }
    else if (Scene == "Game")
    {
    

    if(Raylib.IsKeyDown(KeyboardKey.KEY_W))
    {
        Character.Movement("Verticle", -4);
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
        Character.Movement("Verticle", 4);
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        Character.Movement("Horizontal", -4);
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        Character.Movement("Horizontal", 4);
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_ONE))
    {
        SelectedInventory = 1;
    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_TWO))
    {
        SelectedInventory = 2;
    }
    if(Raylib.IsKeyReleased(KeyboardKey.KEY_C))
    {
        InventoryManager.addToInvetory("Hoe", new Hoe(), 1);
    }
    if(Raylib.IsKeyReleased(KeyboardKey.KEY_V))
    {
        InventoryManager.addToInvetory("Seed", See1d, 3);
        System.Console.WriteLine(See1d.Stacks);
    }

    }
    if(Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_D) || Vector2.Distance(MousePosition, previousMousePos) > 0 || Raylib.IsKeyReleased(KeyboardKey.KEY_E) )
    {
        walking = true;
    }
    else
    {
        walking = false;
    }
    previousMousePos = MousePosition;
    camera.target = new Vector2(Character.hitBox.x, Character.hitBox.y);
    float screenLeft = Character.hitBox.x - ScreenWidth/2;
    float screenTop = Character.hitBox.y + ScreenHeight/2;

    //collisions


    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    Vector2 worldMousePos = Raylib.GetScreenToWorld2D(MousePosition, camera);

    if (Scene == "Start"){
        Raylib.DrawText("Press Space To Start", 500, 500, 30, Color.BLACK);
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE))
        {
            Scene = "Game";
        }
    }
    

    else if (Scene == "Game")
    {
    Raylib.BeginMode2D(camera);
    if (gridIsSelected == true)
    {
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_E))
        {
            Cell Newtile = new Cell();
            if (SelectedInventory == 1){
            Newtile = selectedGrid.changeTile<Rock>(selectedGrid);
            }
            if (SelectedInventory == 2){
                System.Console.WriteLine("hi");
            Newtile = selectedGrid.changeTile<Grass>(selectedGrid);
            }
            Map.gridcells[selectedGridIndex] = Newtile;
            
        }
    }
    int index = 0;
    // Rita ut Celler
    foreach (var item in Map.gridcells)
    {
        Color color = Color.WHITE;
        
        if (walking == true)
        {
            if(item.hitBox.x < worldMousePos.X && item.hitBox.x > worldMousePos.X - 32 && item.hitBox.y < worldMousePos.Y && item.hitBox.y > worldMousePos.Y - 32)
            {
                selectedGrid = item;
                System.Console.WriteLine(index);
                selectedGridIndex = index;
                gridIsSelected = true;
            }
        }

        //Från test har jag märkt att DrawTexturePro tar mycket av datorn, därför har jag gjort så att bara det som skärmen kan se är vad som renderas, pröva flytta ner drawtexturepro ut ur nesten och se hur dina fps droppar
        if(item.hitBox.x < screenLeft + ScreenWidth && item.hitBox.x > screenLeft && item.hitBox.y < screenTop && item.hitBox.y > screenTop - ScreenHeight)
        {
            Raylib.DrawTexturePro(textureManager.LoadTexture(item.Texture), new Rectangle(0,0,Map.CellSize,Map.CellSize), item.hitBox, new Vector2(0,0),0, color);
        }
        index++;
    }
    Raylib.DrawTexturePro(textureManager.LoadTexture(selectedGrid.Texture), new Rectangle(0,0,Map.CellSize,Map.CellSize), selectedGrid.hitBox, new Vector2(0,0),0, Color.ORANGE);
    Raylib.DrawTexturePro(Character.Sprite, new Rectangle(0, 0, 80, 80), Character.hitBox, new Vector2(40, 40),0, Color.WHITE) ;
    Raylib.DrawFPS(150,-50);


    // Inventory drawing
    int itemIndex = 0;
    foreach (var item in InventoryManager.InventorySlots)
    {
        //Raylib.DrawTexture(textureManager.LoadTexture(itemData.Texture), 0,0, Color.WHITE);
        if (item.Value == "Empty")
        {
            Raylib.DrawRectangle((int)Character.hitBox.x-400+86*itemIndex,(int)screenTop-128,64,64, Color.BEIGE);
            
        }
        else
        {
        items itemData = InventoryManager.itemsInInventory[item.Value];
        Raylib.DrawTexturePro(textureManager.LoadTexture(itemData.Texture), new Rectangle(0,0,64,64), new Rectangle((int)Character.hitBox.x-400+86*itemIndex,(int)screenTop-128,64,64), new Vector2(0,0),0,itemData.color); 
        }
        itemIndex++;
        if (itemIndex == InventoryManager.InventorySlots.Count())
        {
            itemIndex = 0;
        }
    }

    }



    Raylib.EndDrawing();
}
