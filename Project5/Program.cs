using System.ComponentModel.DataAnnotations;
using System.Threading;


namespace Project5
{
    public class Program
    {
        public static void Main()
        {
            Player userChar;
            Dungeon dungeon;
            Monster cellMonster;
            bool gameOver = false;
            string? userDir = "go east";

            userChar = new Player(GetPlayerName());
            dungeon = new Dungeon(userChar);

            Console.WriteLine($"You are very brave, {userChar.GetName()}.\nWelcome... to the cube.");
            Console.Write("\nEnter any key to continue.");
            Console.ReadLine();
            Console.Clear();

            while (!gameOver)  //run a loop of turns until the player reaches exit or reaches zero hp
            {
                try
                {
                    //probably add the map to the top of the screen?
                    Console.WriteLine($"The current room is cell {dungeon.GetActiveRoom() + 1}, and you have {userChar.GetHealth()} HP left.\n");  //this will just be for debugging, need to add map later
                    //Console.WriteLine(DisplayMap(userChar, dungeon));  //testing map here
                    //Console.WriteLine(dungeon.GetCellMap(dungeon.GetActiveRoom())); //this also looks like it works for the single cell map
                    Console.WriteLine(dungeon.ToString()); //testing new map here

                    //will probably tweak it so that the default map shows your current row, and the View Map display alls rows and columns

                    if (dungeon.RoomHasWep(1))
                    {
                        Console.WriteLine($"You found a {dungeon.GetRoomWeapon().GetName()}! Your attack power has increased by {dungeon.GetRoomWeapon().GetDamage()}.\n");
                        userChar.SetDamage(dungeon.GetRoomWeapon().GetDamage());
                    }
                    else if (dungeon.RoomHasPot(1))
                    {
                        Console.WriteLine($"You found an HP potion! Your health has increased by 10.\n");
                        userChar.SetHealth(-10);
                    }

                    if (dungeon.RoomHasMonster())
                    {
                        cellMonster = dungeon.GetRoomMonster();
                        Console.WriteLine($"There is a {cellMonster.GetName()} here!\n");
                        while (cellMonster.GetHealth() > 0 && userChar.GetHealth() > 0)
                        {
                            if (userChar.Attack() != 0)
                            {
                                Console.WriteLine($"{userChar.GetName()} hits the {cellMonster.GetName()} with an attack for {userChar.GetDamage()} damage!");
                                cellMonster.SetHealth(userChar.GetDamage());
                            }
                            else
                            {
                                Console.WriteLine($"{userChar.GetName()} misses with an attack!");
                            }

                            if (cellMonster.GetHealth() > 0)
                            {
                                if (cellMonster.Attack() != 0)
                                {
                                    Console.WriteLine($"{cellMonster.GetName()} hits {userChar.GetName()} with an attack for {cellMonster.GetDamage()} damage!");
                                    userChar.SetHealth(cellMonster.GetDamage());
                                }
                                else
                                {
                                    Console.WriteLine($"{cellMonster.GetName()} misses {userChar.GetName()} with an attack!");
                                }
                            }

                            if (userChar.GetHealth() <= 0)
                            {
                                Console.WriteLine("\nEnter any key to continue.");
                                Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"The current room is cell {dungeon.GetActiveRoom() + 1}, and you have 0 HP left.\n");  //this will just be for debugging, need to add map later
                                //Console.WriteLine(DisplayMap(userChar, dungeon));  //testing map here
                                //Console.WriteLine(dungeon.GetCellMap(dungeon.GetActiveRoom())); //this also looks like it works for the single cell map
                                Console.WriteLine(dungeon.ToString(1)); //testing new map here
                                Console.WriteLine($"{userChar.GetName()} is dead. The game is over!");
                                gameOver = true;
                            }
                            if (cellMonster.GetHealth() > 0)
                            {
                                Console.WriteLine("\nEnter any key to continue.");
                                Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine($"The current room is cell {dungeon.GetActiveRoom() + 1}, and you have {userChar.GetHealth()} HP left.\n");  //this will just be for debugging, need to add map later
                                //Console.WriteLine(DisplayMap(userChar, dungeon));  //testing map here
                                //Console.WriteLine(dungeon.GetCellMap(dungeon.GetActiveRoom())); //this also looks like it works for the single cell map
                                Console.WriteLine(dungeon.ToString()); //testing new map here

                            }
                        }
                        if (userChar.GetHealth() > 0)
                        {
                            Console.WriteLine($"\n{userChar.GetName()} has defeated the {cellMonster.GetName()}!");
                            Console.WriteLine("\nEnter any key to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine($"The current room is cell {dungeon.GetActiveRoom() + 1}, and you have {userChar.GetHealth()} HP left.\n");  //this will just be for debugging, need to add map later
                            //Console.WriteLine(DisplayMap(userChar, dungeon));  //testing map here
                            //Console.WriteLine(dungeon.GetCellMap(dungeon.GetActiveRoom())); //this also looks like it works for the single cell map
                            Console.WriteLine(dungeon.ToString()); //testing new map here
                        }
                    }

                    if (userChar.GetHealth() > 0)
                    {
                        Console.WriteLine("What would you like to do?" +
                            "\nPlease enter \"Go East\" or \"Go West\" or \"Go North\" or \"Go South\" or \"View Map\"");
                        userDir = Console.ReadLine();
                        int dirCheck;

                        if (userDir.ToLower() == "go east")
                        {
                            dirCheck = dungeon.GoRight();
                            if (dirCheck == -1)
                            {
                                Console.Clear();
                                Console.WriteLine(dungeon.ToString(1));
                                Console.WriteLine($"You have beaten the dungeon, {userChar.GetName()}! You win!");
                                gameOver = true;
                                break;
                            }
                            else if (dirCheck == 0)
                            {
                                Console.WriteLine($"\nSorry {userChar.GetName()}, but you can't go in that direction.\nEnter any key to continue.");
                                Console.ReadLine();
                            }
                        }
                        else if (userDir.ToLower() == "go west")
                        {
                            dirCheck = dungeon.GoLeft();
                            if (dirCheck == 0)
                            {
                                Console.WriteLine($"\nSorry {userChar.GetName()}, but you can't go in that direction.\nEnter any key to continue.");
                                Console.ReadLine();
                            }
                        }
                        else if (userDir.ToLower() == "go north")
                        {
                            dirCheck = dungeon.GoUp();
                            if (dirCheck == 0)
                            {
                                Console.WriteLine($"\nSorry {userChar.GetName()}, but you can't go in that direction.\nEnter any key to continue.");
                                Console.ReadLine();
                            }
                        }
                        else if (userDir.ToLower() == "go south")
                        {
                            dirCheck = dungeon.GoDown();
                            if (dirCheck == 0)
                            {
                                Console.WriteLine($"\nSorry {userChar.GetName()}, but you can't go in that direction.\nEnter any key to continue.");
                                Console.ReadLine();
                            }
                        }
                        else if (userDir.ToLower() == "view map")
                        {
                            Console.Clear();
                            Console.WriteLine(dungeon.ToString(1)); //testing new map here
                            //Console.WriteLine(dungeon.ToString(1)); //testing new map here
                            Console.WriteLine("Enter any key to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nI do not know what you mean, {userChar.GetName()}.\nEnter any key to continue.");
                    //Console.WriteLine($"{ex.Message}"); this is for troubleshooting
                    Console.ReadLine();
                    Console.Clear();
                }
                Console.Clear();
            }
            Console.ReadLine();  //pausing the console before closing out of the game
        }

        public static string GetPlayerName()
        {
            string? userName = "Dale";
            bool userNameValid = false;
            while (!userNameValid)
            {
                try
                {
                    Console.Write("Hello and welcome to Terk! \nWhat is your name, adventurer?\nName: ");
                    userName = Console.ReadLine();
                    if (userName == "")
                    {
                        throw new Exception();
                    }
                    userNameValid = true;
                    Console.Clear();
                }
                catch
                {
                    Console.Write("\nThat is not a valid entry.\nEnter any key to continue.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            return userName;
        }

        /*public static string DisplayMap(Player player, Dungeon dungeon)
        {
            string map = "";
            int cellSize = dungeon.GetLenghtCount();
            //for (int i = 0; i < cellSize; i++)
            //{
            string enemyType;
            string wepType;
            string playerPos;
            bool doorLeft;
            string doorLeftIcon = "-";
            string doorRightIcon = "-";
            string doorExitIcon = "-";
            string wallTopExitIcon = "-";
            string wallBotExitIcon = "-";
            bool doorRight;
            bool doorExit;

            if (player.GetHealth() > 0)  //adjust the map to just display the current room for now.. can't get multiple rooms to display correctly
            {
                playerPos = "P";
            }
            else
            {
                playerPos = "X";
            }

            if (dungeon.RoomHasMonster())
            {
                enemyType = dungeon.GetRoomMonster().GetName()[0].ToString();
            }
            else
            {
                enemyType = " ";
            }

            if (dungeon.RoomHasWep(0) && !dungeon.RoomWepClaimed())
            {
                if (dungeon.GetRoomWeapon().GetName().CompareTo("Stick") == 0)
                {
                    wepType = "St";
                }
                else if (dungeon.GetRoomWeapon().GetName().CompareTo("Sword") == 0)
                {
                    wepType = "Sw";
                }
                else
                {
                    wepType = dungeon.GetRoomWeapon().GetName()[0].ToString();
                }
            }
            else
            {
                wepType = " ";
            }


            if (dungeon.GetRightDoor()) 
            {
                doorRight = true;
                doorRightIcon = "|";
            }

            if (dungeon.GetLeftDoor())
            {
                doorLeft = true;
                doorLeftIcon = "|";
            }

            if (dungeon.GetExitDoor())
            {
                doorRight = true;
                doorRightIcon = " ";
                wallTopExitIcon = "\\";
                wallBotExitIcon = "/ ";
            }

            if (dungeon.RoomHasWep(0) && !dungeon.RoomWepClaimed())
            {
                if (dungeon.GetRoomWeapon().GetName().CompareTo("Stick") == 0 || dungeon.GetRoomWeapon().GetName().CompareTo("Sword") == 0)
                {
                    map += "-------" +
                         $"\n-  {enemyType}  {wallTopExitIcon}" +
                         $"\n{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                         $"\n-  {wepType} {wallBotExitIcon}" +
                         "\n-------\n";
                }
                else
                {
                    map += "-------" +
                         $"\n-  {enemyType}  {wallTopExitIcon}" +
                         $"\n{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                         $"\n-  {wepType}  {wallBotExitIcon}" +
                         "\n-------\n";
                }
            }
            else
            {
                map += "-------" +
                     $"\n-  {enemyType}  {wallTopExitIcon}" +
                     $"\n{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                     $"\n-  {wepType}  {wallBotExitIcon}" +
                     "\n-------\n";
            }
            //}
            return map;
        }/*


        /*public void PlayerCombat(Player player, Monster monster, int Damage)  //this might be used for combat later? i think this would only be needed if there was multiple enemies at once and you need to select one
        {
            if (Damage > 0)
            {
                Console.WriteLine(!"{player.GetName()} hit {monster.GetName()} for {Damage} damage!");
            }
            else
            {
                Console.WriteLine(!"{player.GetName()} missed {monster.GetName()}!");
            }
        }*/
    }
}