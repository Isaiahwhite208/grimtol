using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
 public class Player : IPlayer
 {
  public string PlayerName { get; set; }
  public List<Item> Inventory { get; set; }

  public Item checkusableitem(string itemName)
  {
   Item inventoryitem = Inventory.Find(item => item.Name == itemName);
   if (inventoryitem != null)
   {
    return inventoryitem;
   }
   return inventoryitem;
  }

  public Player(string playername)
  {
   PlayerName = playername;
   Inventory = new List<Item>();
  }
 }
}