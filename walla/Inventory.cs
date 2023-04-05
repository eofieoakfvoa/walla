using System;
using Raylib_cs;
public class Inventory
{
    public Dictionary<string, items> itemsInInventory = new Dictionary<string, items>();
    public Dictionary<int, string> InventorySlots = new Dictionary<int, string>();
    int inventoryLength = 6;
    public List<Rectangle> InventoryItemBox = new();
    public Inventory()
    {
        for (var i = 0; i < inventoryLength; i++)
        {
        
        InventorySlots.Add(i, "Empty");

        }
    }
    public void addToInvetory(string item, items ItemData, int Amount){
        if (itemsInInventory.ContainsKey(item))
        {
            if (ItemData.Stackable == true)
            {
                ItemData.Stacks += Amount;
                System.Console.WriteLine(ItemData.Stacks);
            }
        }
        else
        {

            int UsableSlot = findFirstEmptySlot();  
            InventorySlots.Remove(UsableSlot);
            InventorySlots.Add(UsableSlot, ItemData.Name);
            itemsInInventory.Add(ItemData.Name, ItemData);

            foreach (var kvp in InventorySlots) {
            Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            foreach (var kvp in itemsInInventory) {
            Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
        }
    }
    public void RemoveFromInventory(int slot)
    {
        InventorySlots[slot] = "Empty";
    }
    public int findFirstEmptySlot()
    {
        for (var i = 0; i < inventoryLength; i++)
        {
            if (InventorySlots[i] == "Empty")
            {
                return i;
            }
            continue;
        }
        return -1;
    }
    
}
