using System.Collections.Generic;

namespace CastleGrimtol.Models
{
 public interface IPlayer
 {
  string PlayerName { get; set; }
  List<Item> Inventory { get; set; }
 }
}