using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
 public class Room : IRoom
 {
  public string Name { get; set; }
  public string Description { get; set; }
  public bool Locked { get; set; }
  public List<Item> Items { get; set; }
  public Dictionary<string, Room> Exits { get; set; }
  Dictionary<string, IRoom> IRoom.Exits { get; set; }

  public Room ChangeRoom(string name)
  {
   if (Exits.ContainsKey(name) && this.Locked == false)
   {
    return Exits[name];
   }
   if (Exits.ContainsKey(name) && this.Locked == true)
   {
    System.Console.WriteLine("");
    Console.WriteLine("In need of a key.");
    return this;
   }
   Console.WriteLine("Unless you like running into walls, you cant move further.");
   return this;
  }
  public Item checkforitem(string itemName)
  {
   Item myitem = Items.Find(item => item.Name == itemName);
   if (myitem != null)
   {
    Items.Remove(myitem);
   }
   return myitem;
  }
  public Room(string name, string description, bool locked)
  {
   Name = name;
   Description = description;
   Locked = locked;
   Exits = new Dictionary<string, Room>();
   Items = new List<Item>();
  }
 }
}