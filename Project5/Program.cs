using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;


namespace Project5
{
    public class Program
    {
        public static void Main()
        {
            Player userChar = null;
            Dungeon dungeon = null;
            Monster cellMonster = null;
            bool gameOver = false;
            bool gameContinue = true;
            string? userContinue = "yes";
            string? userContinueChar = "yes";
            string? userDir = "go east";
            bool validContinue = false;
            string playerName;
            bool newGameBlank = true;
            int charWins = 0;
            int highestWins = 0;
            string highScoreName = "";
            string[] scoreValues = new string[2];

            //playerName = GetPlayerName();

            while (gameContinue)
            {
                if (newGameBlank)
                {
                    playerName = GetPlayerName();
                    userChar = new Player(playerName);
                    dungeon = new Dungeon(userChar);
                    charWins = 0;
                    validContinue = false;
                    userContinue = null;
                }
                else
                {
                    // userChar = new Player(playerName);  #we want to use the same character but a new dungeon
                    dungeon = new Dungeon(userChar);
                    validContinue = false;
                    userContinue = null;
                }

                scoreValues = FileRead();
                highestWins = int.Parse(scoreValues[0]);
                highScoreName = scoreValues[1];

                Console.WriteLine($"You are very brave, {userChar.GetName()}.\nWelcome... to the cube.");
                if (charWins == 1)
                {
                    Console.WriteLine($"You have cleared {charWins} dungeon.");
                }
                else if (charWins > 0 && charWins != 1)
                {
                    Console.WriteLine($"You have cleared {charWins} dungeons.");
                }
                if (highestWins == 0)
                {
                    Console.WriteLine($"The high score is {highestWins} dungeons cleared.");
                }
                else if (highestWins == 1)
                {
                    Console.WriteLine($"The high score is {highestWins} dungeon cleared by {highScoreName}.");
                }
                else if (highestWins > 0 && highestWins != 1)
                {
                    Console.WriteLine($"The high score is {highestWins} dungeons cleared in a row by {highScoreName}.");
                }
                Console.Write("\nEnter any key to continue.");
                Console.ReadLine();
                Console.Clear();
                while (!gameOver)  //run a loop of turns until the player reaches exit or reaches zero hp
                {
                    try
                    {
                        //probably add the map to the top of the screen?
                        Console.WriteLine($"The current room is cell {dungeon.GetActiveRoom() + 1}, and you have {userChar.GetHealth()} HP left.\n");  //this might just be for debugging, need to add map later
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
                            Random random = new Random();
                            if (random.NextDouble() < 0.15)
                            {
                                Console.WriteLine($"You found a special HP potion! Your health has increased by 20.\n");
                                userChar.SetHealth(-20);
                            }
                            else
                            {
                                Console.WriteLine($"You found an HP potion! Your health has increased by 10.\n");
                                userChar.SetHealth(-10);
                            }
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
                                "\nPlease enter \"W\", \"A\", \"S\", \"D\" for movement or \"M\" for map.");
                            userDir = Console.ReadLine();
                            int dirCheck;

                            if (userDir.ToLower() == "d")
                            {
                                dirCheck = dungeon.GoRight();
                                if (dirCheck == -1)
                                {
                                    Console.Clear();
                                    Console.WriteLine(dungeon.ToString(1));
                                    Console.WriteLine($"You have beaten the dungeon, {userChar.GetName()}! You win!");
                                    charWins++;
                                    if (charWins > highestWins)
                                    {
                                        highestWins = charWins;
                                        highScoreName = userChar.GetName();
                                        WriteFile(highestWins, highScoreName);
                                    }
                                    Console.ReadLine();  //pausing the console before closing out of the game
                                    gameOver = true;
                                    break;
                                }
                                else if (dirCheck == 0)
                                {
                                    Console.WriteLine($"\nSorry {userChar.GetName()}, but you can't go in that direction.\nEnter any key to continue.");
                                    Console.ReadLine();
                                }
                                else if (dirCheck == 2)
                                {
                                    Console.WriteLine("\nThis door is locked! You must find another way out.");
                                    Console.ReadLine();
                                }
                            }
                            else if (userDir.ToLower() == "a")
                            {
                                dirCheck = dungeon.GoLeft();
                                if (dirCheck == 0)
                                {
                                    Console.WriteLine($"\nSorry {userChar.GetName()}, but you can't go in that direction.\nEnter any key to continue.");
                                    Console.ReadLine();
                                }
                                else if (dirCheck == 2)
                                {
                                    Console.WriteLine("\nThis door is locked! You must find another way out.");
                                    Console.ReadLine();
                                }
                            }
                            else if (userDir.ToLower() == "w")
                            {
                                dirCheck = dungeon.GoUp();
                                if (dirCheck == 0)
                                {
                                    Console.WriteLine($"\nSorry {userChar.GetName()}, but you can't go in that direction.\nEnter any key to continue.");
                                    Console.ReadLine();
                                }
                                else if (dirCheck == 2)
                                {
                                    Console.WriteLine("\nThis door is locked! You must find another way out.");
                                    Console.ReadLine();
                                }
                            }
                            else if (userDir.ToLower() == "s")
                            {
                                dirCheck = dungeon.GoDown();
                                if (dirCheck == 0)
                                {
                                    Console.WriteLine($"\nSorry {userChar.GetName()}, but you can't go in that direction.\nEnter any key to continue.");
                                    Console.ReadLine();
                                }
                                else if (dirCheck == 2)
                                {
                                    Console.WriteLine("\nThis door is locked! You must find another way out.");
                                    Console.ReadLine();
                                }
                            }
                            else if (userDir.ToLower() == "m")
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

                while (!validContinue)
                {
                    Console.Clear();
                    Console.Write("Would you like to play again?" +
                        "\nPlease enter \"Yes\" or \"No\" ");
                    userContinue = Console.ReadLine();

                    if (userContinue.ToLower() == "yes" || userContinue.ToLower() == "y")
                    {
                        Console.Clear();
                        if (userChar.GetHealth() > 0)
                        {
                            Console.Write("Would you like to continue with this character?" +
                                "\nPlease enter \"Yes\" or \"No\" ");
                            userContinueChar = Console.ReadLine();
                            if (userContinueChar.ToLower() == "yes" || userContinueChar.ToLower() == "y")
                            {
                                gameContinue = true;
                                newGameBlank = false;
                                validContinue = true;
                                gameOver = false;
                            }

                            else if (userContinueChar.ToLower() == "no" || userContinueChar.ToLower() == "n")
                            {
                                gameContinue = true;
                                validContinue = true;
                                gameOver = false;
                                newGameBlank = true;
                            }
                            else
                            {
                                validContinue = false;
                            }
                        }
                        else
                        {
                            gameContinue = true;
                            validContinue = true;
                            gameOver = false;
                            newGameBlank = true;
                        }
                    }
                    else if (userContinue.ToLower() == "no" || userContinue.ToLower() == "n")
                    {
                        gameContinue = false;
                        validContinue = true;
                    }
                    else
                    {
                        validContinue = false;
                    }
                }
                Console.Clear();
            }
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

        public static void WriteFile(int highScore, string highScoreName)
        {
            string filePath = @"..\..\..\Text Files\highscore.txt";
            string[] files = new string[2];

            try
            {
                FileStream fcreate = File.Open(@"..\..\..\Text Files\highscore.txt", FileMode.Create);
                using (StreamWriter wtr = new StreamWriter(fcreate))
                {
                    files[0] = highScore.ToString();
                    files[1] = highScoreName;
                    wtr.WriteLine(highScore);
                    wtr.WriteLine(highScoreName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with accessing the file: " + e.Message);
            }
        }
        public static string[] FileRead()
        {
            //string filePath = @"..\..\..\Text Files\delimited.txt";
            string filePath = @"..\..\..\Text Files\highscore.txt";
            //List<string> highScoreValues = null;
            string[] fields = new string[2];

            try
            {
                using (StreamReader rdr = new StreamReader(filePath))
                {
                    int i = 0;
                    //While there is still more text in the file we haven't processed
                    while (rdr.Peek() != -1)
                    {
                        //Read the next line of data from the file
                        string nextLineFromFile = rdr.ReadLine();
                        //Console.WriteLine(nextLineFromFile);
                        fields[i] = nextLineFromFile;
                        i++;
                    }
                    return fields;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with accessing the file: " + e.Message);
                return fields;
            }
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