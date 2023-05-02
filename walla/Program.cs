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
int selectedGridIndex = 1;
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
        if (initialize == true)
        {
            Map.Render();

            initialize = false;
        }
    }
    else if (Scene == "Game")
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            Character.Movement("Verticle", -4);
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            Character.Movement("Verticle", 4);
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            Character.Movement("Horizontal", -4);
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            Character.Movement("Horizontal", 4);
        }

        
        //Här kollar jag ifall personen klickar på av numrena mellan 1-9, detta gör jag igenom att först ha en for loop igenom numrena 49-57
        //49-57 är viktiga nummer där de har samma value som Keyboard.key.one till keyboard.key.nine, detta kan man se genom att hovera över det i en normal situation
        //sen kollar jag ifall knappen är klickad så gör jag om valuet till numret som den representerar
        for (var i = 49; i <= 57; i++)
        {
            if (Raylib.IsKeyPressed((KeyboardKey)i))
            {
                int valueToNumber = i -49; // minus 49 istället för 48 eftersom att invetoryit börjar på 0
                SelectedInventory = valueToNumber;
            }
        }
        
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_C))
        {
            InventoryManager.addToInvetory("Hoe", new Hoe(), 1);
            InventoryManager.addToInvetory("Seed", See1d, 3);
            InventoryManager.addToInvetory("Shovel", new Shovel(), 1);
        }

    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_D) || Vector2.Distance(MousePosition, previousMousePos) > 0 || Raylib.IsKeyReleased(KeyboardKey.KEY_E))
    {
        walking = true;
    }
    else
    {
        walking = false;
    }
    previousMousePos = MousePosition;
    camera.target = new Vector2(Character.Position.x, Character.Position.y);
    float screenLeft = Character.Position.x - ScreenWidth / 2;
    float screenTop = Character.Position.y + ScreenHeight / 2;


    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    Vector2 worldMousePos = Raylib.GetScreenToWorld2D(MousePosition, camera);

    if (Scene == "Start")
    {
        Raylib.DrawText("Press Space To Start", 500, 500, 30, Color.BLACK);
        Raylib.DrawText("WASD to move", 50, 600, 30, Color.BLACK);
        Raylib.DrawText("Press E to use tool", 50, 550, 30, Color.BLACK);
        Raylib.DrawText("1-6 to cycle inventory", 50, 500, 30, Color.BLACK);
        Raylib.DrawText("C debug inventory", 50, 450, 30, Color.BLACK);
        Raylib.DrawText("ESC to exit", 50, 400, 30, Color.BLACK);
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
                if (InventoryManager.InventorySlots[SelectedInventory] == "Empty")
                {
                }
                else
                {
                items ItemInHold =  InventoryManager.itemsInInventory[InventoryManager.InventorySlots[SelectedInventory]];
                Cell Newtile = new Cell();

                if (SelectedInventory == 0)
                {
                    Newtile = selectedGrid.changeTile<farmLand>(selectedGrid);
                }
                if (SelectedInventory == 1)
                {
                    Newtile = selectedGrid.changeTile<Grass>(selectedGrid);
                }
                if (SelectedInventory == 2)
                {
                    Newtile = selectedGrid.changeTile<Grass>(selectedGrid);
                }
                Map.gridcells[selectedGridIndex] = Newtile;
                }

            }
        }
        int index = 0;

        // Rita ut Celler

        foreach (var item in Map.gridcells)
        {
            Color color = Color.WHITE;

            if (walking == true)
            {
                if (item.Position.x < worldMousePos.X && item.Position.x > worldMousePos.X - 32 && item.Position.y < worldMousePos.Y && item.Position.y > worldMousePos.Y - 32)
                {
                    selectedGrid = item;
                    selectedGridIndex = index;
                    gridIsSelected = true;
                }
            }

            //Från test har jag märkt att DrawTexturePro tar mycket av datorn, därför har jag gjort så att bara det som skärmen kan se är vad som renderas, pröva flytta ner drawtexturepro ut ur nesten och se hur dina fps droppar
            if (item.Position.x < screenLeft + ScreenWidth && item.Position.x > screenLeft && item.Position.y < screenTop && item.Position.y > screenTop - ScreenHeight)
            {
                Raylib.DrawTexturePro(textureManager.LoadTexture(item.Texture), new Rectangle(0, 0, Map.CellSize, Map.CellSize), item.Position, new Vector2(0, 0), 0, color);
            }
            index++;
        }
        Raylib.DrawTexturePro(textureManager.LoadTexture(selectedGrid.Texture), new Rectangle(0, 0, Map.CellSize, Map.CellSize), selectedGrid.Position, new Vector2(0, 0), 0, Color.ORANGE);
        Raylib.DrawTexturePro(Character.Sprite, new Rectangle(0, 0, 80, 80), Character.Position, new Vector2(40, 40), 0, Color.WHITE);
        Raylib.DrawFPS(150, -50);


        // Inventory drawing
        int itemIndex = 0;
        foreach (var item in InventoryManager.InventorySlots)
        {
            //Raylib.DrawTexture(textureManager.LoadTexture(itemData.Texture), 0,0, Color.WHITE);
            if (item.Value == "Empty")
            {
                Raylib.DrawRectangle((int)Character.Position.x - 400 + 86 * itemIndex, (int)screenTop - 128, 64, 64, Color.BEIGE);

            }
            else
            {
                items itemData = InventoryManager.itemsInInventory[item.Value];
                Raylib.DrawTexturePro(textureManager.LoadTexture(itemData.Texture), new Rectangle(0, 0, 64, 64), new Rectangle((int)Character.Position.x - 400 + 86 * itemIndex, (int)screenTop - 128, 64, 64), new Vector2(0, 0), 0, itemData.color);
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
