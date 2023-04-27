

using System.Threading;
/**
* ------------------------------------------------------------------------
* File Name: Project5
* Project Name: Zork Game
* ------------------------------------------------------------------------
* Author's Name and Email: Tyler Campbell, tcampbell5@etsu.edu
* Course-Section: CSCI-1260-002
* Creation Date: 4/12/23
* ------------------------------------------------------------------------
* */

namespace Project5
{
    public class Program
    {
        public static void Main()
        {
        }
    }

    public void PlayerCombat(Player player, Monster monster, int Damage)
    {
        if (Damage > 0)
        {
            Console.WriteLine(!"{player.GetName()} hit {monster.GetName()} for {Damage} damage!");
        }
        else
        {
            Console.WriteLine(!"{player.GetName()} missed {monster.GetName()}!");
        }
    }
}