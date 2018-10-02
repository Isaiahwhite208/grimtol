using System.Collections.Generic;

namespace CastleGrimtol.Project
{
 public interface IGame
 {
  Room CurrentRoom { get; set; }
  Player CurrentPlayer { get; set; }
  void Setup();
  void Reset();
  void StartGame();
  void GetUserInput();
  #region Console Commands
  void Quit();
  void Help();
  void Go(string direction);
  void TakeItem(string itemName);
  void UseItem(string itemName);
  void Inventory();
  void Look();
  #endregion
 }
}
