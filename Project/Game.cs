using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CastleGrimtol.Project
{
 public class Game : IGame
 {
  public Room CurrentRoom { get; set; }
  public Player CurrentPlayer { get; set; }
  bool playing;
  public void GetUserInput()
  {
   string input = Console.ReadLine();
   string[] command = input.Split();
   string useraction;
   string usercommand;
   if (input.Length == 0)
   {
    return;
   }
   else if (command.Length == 1)
   {
    useraction = command[0];
    useraction = useraction.ToUpper();
    if (useraction == "HELP")
    {
     Help();
     return;
    }
    if (useraction == "QUIT")
    {
     Quit();
     return;
    }
    if (useraction == "LOOK")
    {
     Look();
     return;
    }
    if (useraction == "INVENTORY")
    {
     Inventory();
     return;
    }
    Console.WriteLine("That doesnt work, new command.");
   }
   else if (command.Length == 2)
   {
    useraction = command[0];
    usercommand = command[1];
    useraction = useraction.ToUpper();
    usercommand = usercommand.ToLower();
    if (useraction == "GO")
    {
     Go(usercommand);
     return;
    }
    if (useraction == "TAKE")
    {
     TakeItem(usercommand);
     return;
    }
    if (useraction == "USE")
    {
     UseItem(usercommand);
     return;
    }
    Console.WriteLine("That doesnt work, new command.");
   }
   else
   {
    Console.WriteLine("That doesnt work, new command.");
   }
  }
  public void Go(string direction)
  {
   CurrentRoom = CurrentRoom.ChangeRoom(direction);
  }
  public void Help()
  {
   Console.WriteLine("");
   Console.WriteLine(@"
      -Go <Direction> Moves your character
      -Use <ItemName> Uses an item in current room from stash
      -Take <ItemName> Puts item from that room into your stash
      -Look Gives room description again
      -Inventory Shows players stash
      -Help Shows a list of commands and actions
      -Quit Quits the Game");
   Console.WriteLine("Press ENTER leave menu");
   GetUserInput();
   return;
  }
  public void Inventory()
  {
   if (CurrentPlayer.Inventory.Count == 0)
   {
    Console.WriteLine("Your stash is quite light.");
    return;
   }
   foreach (Item inventoryitem in CurrentPlayer.Inventory)
   {
    if (inventoryitem != null)
    {
     Console.WriteLine(inventoryitem.Name);
     return;
    }
   }
  }
  public void Look()
  {
   Console.WriteLine(CurrentRoom.Description);
   GetUserInput();
  }
  public void Quit()
  {
   Console.WriteLine("Only quiters quit? (Y/N)");
   string newgame = Console.ReadLine();
   string quitGame = newgame.ToUpper();
   if (quitGame == "N")
   {
    return;
   }
   playing = false;
  }
  public void Reset()
  {
   Console.WriteLine("Round 2? (Y/N)");
   string newgame = Console.ReadLine();
   string newGame = newgame.ToUpper();
   if (newGame == "Y")
   {
    StartGame();
   }
   return;
  }
  public void Setup()
  {
   Console.Clear();
   Room dungeon = new Room("Dungeon", @"
   - Welcome to your hell! - 
Thats the first thing you read as you wake up in a dimly lit room, you have no idea where you are and no recollection as to how you got here. All you see is a door to your back (south). All you see is a key dangling from the ceiling. Time to go figure this out.", true);
   Room eastDungeonHall = new Room("East Hall", @"
You are given a tiny hall to pass through, but you hear noises in the distance, you become curious and excited in hopes of help!", false);
   Room dungeonArmory = new Room("Armory", @"
You hear loud clinging down the hall, as you move closer it becomes softer and slower as if you are walking away from it. You begin to open the door and as you do so loud metallic rumbling and chaos occurs. It all suddenly stops with a thud. You enter and see an anvil that tipped over with a freshly forged red hot to the touch sword. Gear and weapons are sporadically tossed all around the room", false);
   Room southDungeonHall = new Room("South Hall", @"
To be quite frank, nothing special happens here...", false);
   Room dungeonMesshall = new Room("Messhall", @"
You could smell this room from a ways away. It reeks of rotten food and butchered animals, yet some of the bodies still drip fresh blood. Something still lives here!", true);
   Room westDungeonDorm = new Room("West Dorm", @"
You have been swarmed and killed by haunted and tormented creatures that protect their old home, nothing you could have done to protect youself.", true);
   Room dungeonShrine = new Room("Shrine", @"
An eerie feeling rolls over your shoulder and up your back sending shivers throughout your body. As you enter the door behind you slams shut but remains openable. You feel you are being watched as you examine the odd writing and symbols posted around the entire room.", false);
   Room dungeonLaboratory = new Room("Dungeon Lab", @"
This room is dusty and smells as if it has been abandoned for ages, wonder what knowledge lye here.", false);
   Room thePit = new Room("The Pit", @"
Massive dark pit, nothing but fowl smells and odd screeches. Lets get out of here.", false);
   Room summoningRoom = new Room("Summoning Room", @"
Door is now unlocked, as you enter the room you see strange symbols. The room starts to shake and flames go out. A bright red flash occures from the center of the room then all goes black.
 - Thank you for unlocking my bind, you wont be of use to me anymore -", false);
   Item key = new Item("key", "Locked door is not so locked now, good luck out there!");
   dungeon.Exits.Add("south", eastDungeonHall);
   eastDungeonHall.Exits.Add("north", dungeon);
   eastDungeonHall.Exits.Add("south", dungeonArmory);
   dungeonArmory.Exits.Add("north", eastDungeonHall);
   dungeonArmory.Exits.Add("west", southDungeonHall);
   southDungeonHall.Exits.Add("east", dungeonArmory);
   southDungeonHall.Exits.Add("west", dungeonMesshall);
   dungeonMesshall.Exits.Add("west", westDungeonDorm);
   dungeonMesshall.Exits.Add("east", southDungeonHall);
   dungeonMesshall.Exits.Add("north", dungeonShrine);
   dungeonShrine.Exits.Add("south", dungeonMesshall);
   dungeonShrine.Exits.Add("north", dungeonLaboratory);
   dungeonLaboratory.Exits.Add("south", dungeonShrine);
   dungeonLaboratory.Exits.Add("east", thePit);
   dungeonLaboratory.Exits.Add("north", summoningRoom);
   dungeon.Items.Add(key);
   CurrentRoom = dungeon;
   Console.Write("Whats your Name?: ");
   string newplayername = Console.ReadLine();
   CurrentPlayer = new Player(newplayername);
   Console.WriteLine($"Good Luck {newplayername}");
  }
  public void StartGame()
  {
   playing = true;
   Setup();
   Console.Write("Type HELP for help, duh, or type the command: ");
   GetUserInput();
   while (playing)
   {
    if (CurrentRoom.Name == "SummoningRoom")
    {
     System.Console.WriteLine(CurrentRoom.Description);
     Thread.Sleep(3000);
     playing = false;
     Reset();
     return;
    }
    if (CurrentRoom.Name == "" || CurrentRoom.Name == "")
    {
     playing = false;
     System.Console.WriteLine(CurrentRoom.Description);
     Thread.Sleep(3000);
     Console.WriteLine("You are dead");
     Reset();
     return;
    }
    Console.WriteLine("");
    Console.WriteLine($"{CurrentRoom.Name}:{CurrentRoom.Description}");
    System.Console.WriteLine("");
    GetUserInput();
   }
  }
  public void TakeItem(string itemName)
  {
   var lootableitem = CurrentRoom.checkforitem(itemName);
   if (lootableitem != null)
   {
    CurrentPlayer.Inventory.Add(lootableitem);
    Console.WriteLine($"You've added {lootableitem.Name} to your stash");
    return;
   }
   Console.WriteLine("fake news");
   return;
  }
  public void UseItem(string itemName)
  {
   var usableitem = CurrentPlayer.checkusableitem(itemName);
   if (CurrentPlayer.Inventory.Contains(usableitem) && CurrentRoom.Name.ToUpper() == "")
   {
    CurrentRoom.Locked = false;
    CurrentPlayer.Inventory.Remove(usableitem);
    Console.WriteLine(usableitem.Description);
    return;
   }
   {
    CurrentRoom.Locked = false;
    CurrentPlayer.Inventory.Remove(usableitem);
    Console.WriteLine(usableitem.Description);
    Thread.Sleep(3000);
    return;
   }
   Console.WriteLine("Hmm, seem to be missing that item.");
   return;
  }
 }
}