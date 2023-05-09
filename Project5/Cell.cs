using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    public class Cell
    {
        private bool hasMonster;
        private bool leftDoor;
        private bool rightDoor;
        private bool botDoor;
        private bool topDoor;
        private bool leftDoorLock;
        private bool rightDoorLock;
        private bool botDoorLock;
        private bool topDoorLock;
        private bool exitDoor = false;
        private double monsterChance = 0.5;
        private Monster monster;
        private bool hasWeapon;
        private Weapon? roomWep = null;
        private bool wepClaimed = false;
        private bool roomEntered = false;
        private Player player;
        private bool activeRoom = false;
        private bool hasPotion;
        private bool potClaimed;

        public Cell(int lengthNum, int heightNum, int heightTot, int lengthTot, int exitHeight, int wepRoom, Weapon wepType, Player player)
        {
            Random random = new Random();
            monster = new Monster();
            this.player = player;
            this.hasPotion = false;
            this.potClaimed = false;
            topDoorLock = false;
            botDoorLock = false;
            leftDoorLock = false; 
            rightDoorLock = false;

            if (lengthNum == 0 && heightNum == 0)
            {
                this.hasMonster = false;
            }
            else if ((random.NextDouble() < monsterChance))  //generating the monster
            {
                this.hasMonster = true;
            }
            else
            {
                this.hasMonster = false;
            }

            if (heightNum == 0)
            {
                topDoor = false;
                botDoor = true;
                if (lengthNum == 0)    //generating the doors for cells
                {
                    leftDoor = false;
                    rightDoor = true;
                    exitDoor = false;
                    roomEntered = true;
                    activeRoom = true;
                }
                else if (lengthNum < (lengthTot - 1))
                {
                    leftDoor = true;
                    rightDoor = true;
                    exitDoor = false;
                }
                else if (lengthNum == (lengthTot - 1))
                {
                    leftDoor = true;
                    rightDoor = false;
                    if (exitHeight == heightNum)
                    {
                        rightDoor = true;
                        exitDoor = true;
                        if (random.NextDouble() < 0.5)
                        {
                            botDoorLock = true;
                        }
                        else
                        {
                            leftDoorLock = true;
                        }
                    }
                }
            }
            else if (heightNum < (heightTot - 1))
            {
                topDoor = true;
                botDoor = true;
                if (lengthNum == 0)    //generating the doors for cells
                {
                    leftDoor = false;
                    rightDoor = true;
                    exitDoor = false;
                }
                else if (lengthNum < (lengthTot - 1))
                {
                    leftDoor = true;
                    rightDoor = true;
                    exitDoor = false;
                }
                else if (lengthNum == (lengthTot - 1))
                {
                    leftDoor = true;
                    rightDoor = false;
                    if (exitHeight == heightNum)
                    {
                        rightDoor = true;
                        exitDoor = true;
                        if (random.NextDouble() < 0.33)
                        {
                            botDoorLock = true;
                            topDoorLock = true;
                        }
                        else if (random.NextDouble() < 0.66)
                        {
                            topDoorLock = true;
                            leftDoorLock = true;
                        }
                        else
                        {
                            leftDoorLock = true;
                            botDoorLock = true;
                        }
                    }
                }
            }
            else
            {
                topDoor = true;
                botDoor = false;
                if (lengthNum == 0)    //generating the doors for cells
                {
                    leftDoor = false;
                    rightDoor = true;
                    exitDoor = false;
                }
                else if (lengthNum < (lengthTot - 1))
                {
                    leftDoor = true;
                    rightDoor = true;
                    exitDoor = false;
                }
                else if (lengthNum == (lengthTot - 1))
                {
                    leftDoor = true;
                    rightDoor = false;
                    if (exitHeight == heightNum)
                    {
                        rightDoor = true;
                        exitDoor = true;
                        if (random.NextDouble() < 0.5)
                        {
                            topDoorLock = true;
                        }
                        else
                        {
                            leftDoorLock = true;
                        }

                    }
                }
            }

            if ((lengthNum * heightNum) == wepRoom) //generating the weapon info
            {
                this.hasWeapon = true;
                roomWep = wepType;
            }
            else
            {
                roomWep = null;
                if (lengthNum == 0 && heightNum == 0)
                {
                    this.hasPotion = false;
                }
                else if (random.NextDouble() < 0.15)
                {
                    this.hasPotion = true;
                }
                else
                {
                    this.hasWeapon = false;
                }
            }
        }

        public bool GetLeftDoor()
        {
            return leftDoor;
        }

        public bool GetRightDoor()
        {
            return rightDoor;
        }

        public bool GetTopDoor()
        { 
            return topDoor; 
        }

        public bool GetBotDoor()
        { 
            return botDoor; 
        }

        public bool GetExitDoor()
        {
            return exitDoor;
        }

        public bool GetTopDoorLock()  //all of these gets and sets for doors could probably be one method that takes in a value telling it which door it is..
        {
            return topDoorLock;
        }

        public void SetTopDoorLock(bool doorLock)
        {
            if (doorLock)
            {
                this.topDoor = false;
            }
        }

        public void SetBotDoorLock(bool doorLock)
        {
            if (doorLock)
            {
                this.botDoor = false;
            }
        }
        public bool GetBotDoorLock()
        {
            return botDoorLock;
        }

        public bool GetLeftDoorLock()  //all of these gets and sets for doors could probably be one method that takes in a value telling it which door it is..
        {
            return leftDoorLock;
        }

        public void SetLeftDoorLock(bool doorLock)
        {
            if (doorLock)
            {
                this.leftDoor = false;
            }
        }

        public void SetRightDoorLock(bool doorLock)
        {
            if (doorLock)
            {
                this.rightDoor = false;
            }
        }
        public bool GetRightDoorLock()
        {
            return rightDoorLock;
        }
        public bool GetHasMonster()
        {
            if ((monster.GetHealth() <= 0) && hasMonster)
            {
                hasMonster = false;
            }
            return hasMonster;
        }

        public bool GetHasWeapon(int checkType)
        {
            if (checkType == 1)
            {
                if (hasWeapon && !wepClaimed)
                {
                    wepClaimed = true;
                    return hasWeapon;
                }
                return false;
            }
            else
            {
                return hasWeapon;
            }
        }

        public bool GetHasPotion(int checkType)
        {
            if (checkType == 1)
            {
                if (hasPotion && !potClaimed)
                {
                    potClaimed = true;
                    return hasPotion;
                }
                return false;
            }
            else
            {
                return hasPotion;
            }
        }

        public bool GetWepClaimed()
        {
            return wepClaimed;
        }

        public bool GetPotClaimed()
        {
            return potClaimed;
        }
        public Weapon GetWeapon()
        {
            return roomWep;
        }

        public Monster GetMonster()
        {
            return monster;
        }

        public void SetRoomEntered()
        {
            roomEntered = true;
        }

        public bool GetRoomEntered()
        {
            return this.roomEntered;
        }

        public void SetActiveRoom(bool active)
        {
            if (active)
            {
                activeRoom = true;
            }
            else
            {
                activeRoom = false;
            }
        }

        /*public override string ToString()
        {
            string map = "";
            bool doorRight = false;
            bool doorLeft = false;
            bool doorExit = false;
            string doorLeftIcon = "-";
            string doorRightIcon = "-";
            string doorExitIcon = "-";
            string wallTopExitIcon = "-";
            string wallBotExitIcon = "-";
            string wepType = " ";
            string enemyType = " ";
            string playerPos = " ";

            if (roomEntered)
            {
                if (activeRoom)
                {
                    if (player.GetHealth() > 0)  //adjust the map to just display the current room for now.. can't get multiple rooms to display correctly
                    {
                        playerPos = "P";
                    }
                    else
                    {
                        playerPos = "X";
                    }
                }
                else
                {
                    playerPos = " ";
                }

                if (GetRightDoor())
                {
                    doorRight = true;
                    doorRightIcon = "|";
                }

                if (GetLeftDoor())
                {
                    doorLeft = true;
                    doorLeftIcon = "|";
                }

                if (GetExitDoor())
                {
                    doorRight = true;
                    doorRightIcon = " ";
                    wallTopExitIcon = "\\";
                    wallBotExitIcon = "/ ";
                }

                if (GetHasWeapon(0) && !GetWepClaimed())
                {
                    if (GetWeapon().GetName().CompareTo("Stick") == 0)
                    {
                        wepType = "St";
                    }
                    else if (GetWeapon().GetName().CompareTo("Sword") == 0)
                    {
                        wepType = "Sw";
                    }
                    else
                    {
                        wepType = GetWeapon().GetName()[0].ToString();
                    }
                }
                else
                {
                    wepType = " ";
                }

                if (GetHasMonster())
                {
                    enemyType = GetMonster().GetName()[0].ToString();
                }
                else
                {
                    enemyType = " ";
                }

                if (GetHasWeapon(0) && !GetWepClaimed())
                {
                    if (GetWeapon().GetName().CompareTo("Stick") == 0 || GetWeapon().GetName().CompareTo("Sword") == 0)
                    {
                        map += "-------" +
                             $"\n-  {enemyType}  {wallTopExitIcon}" +
                             $"\n{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                             //$"\n{doorLeftIcon}     {doorRightIcon}" +
                             $"\n-  {wepType} {wallBotExitIcon}" +
                             "\n-------\n";
                    }
                    else
                    {
                        map += "-------" +
                             $"\n-  {enemyType}  {wallTopExitIcon}" +
                             $"\n{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                             //$"\n{doorLeftIcon}     {doorRightIcon}" +
                             $"\n-  {wepType}  {wallBotExitIcon}" +
                             "\n-------\n";
                    }
                }
                else
                {
                    map += "-------" +
                         $"\n-  {enemyType}  {wallTopExitIcon}" +
                         $"\n{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                         //$"\n{doorLeftIcon}     {doorRightIcon}" +
                         $"\n-  {wepType}  {wallBotExitIcon}" +
                         "\n-------\n";
                }
            }
            else
            {
                //map = "not yet entered"; //will change this later, this is for testing
                map = "";
            }

            return map;
        } */

        public override string ToString()
        {
            string map = "";
            string doorLeftIcon = "-";
            string doorRightIcon = "-";
            string doorBotIcon = "-";
            string doorTopIcon = "-";
            string wallTopExitIcon = "-";
            string wallBotExitIcon = "-";
            string wepType = " ";
            string enemyType = " ";
            string playerPos = " ";

            if (roomEntered)
            {
                if (activeRoom)
                {
                    if (player.GetHealth() > 0)  //adjust the map to just display the current room for now.. can't get multiple rooms to display correctly
                    {
                        playerPos = "P";
                    }
                    else
                    {
                        playerPos = "X";
                    }
                }
                else
                {
                    playerPos = " ";
                }

                if (GetRightDoor())
                {
                    doorRightIcon = "|";
                }

                if (GetLeftDoor())
                {
                    doorLeftIcon = "|";
                }

                if (GetBotDoor())
                {
                    doorBotIcon = "=";
                }

                if (GetTopDoor())
                {
                    doorTopIcon = "=";
                }

                if (GetExitDoor())
                {
                    /*doorExit = true;
                    doorExitIcon = "[]";*/   // for now since just displaying one cell, i can just override the right door icon for the exit
                    doorRightIcon = " ";
                    wallTopExitIcon = "\\";
                    wallBotExitIcon = "/";
                }

                if (GetHasWeapon(0) && !GetWepClaimed())
                {
                    if (GetWeapon().GetName().CompareTo("Stick") == 0)
                    {
                        wepType = "St";
                    }
                    else if (GetWeapon().GetName().CompareTo("Sword") == 0)
                    {
                        wepType = "Sw";
                    }
                    else
                    {
                        wepType = GetWeapon().GetName()[0].ToString();
                    }
                }
                else if (GetHasPotion(0) && !GetPotClaimed())
                {
                    wepType = "HP";
                }
                else
                {
                    wepType = " ";
                }

                if (GetHasMonster())
                {
                    enemyType = GetMonster().GetName()[0].ToString();
                }
                else
                {
                    enemyType = " ";
                }

                if (GetHasPotion(0) && !GetPotClaimed())
                {
                    if (wepType.ToString().CompareTo("HP") == 0)
                    {
                        map += $"---{doorTopIcon}---" +
                               $"-  {enemyType}  {wallTopExitIcon}" +
                               $"{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                               $"-  {wepType} {wallBotExitIcon}" +
                               $"---{doorBotIcon}---";
                    }
                    else
                    {
                        map += $"---{doorTopIcon}---" +
                             $"-  {enemyType}  {wallTopExitIcon}" +
                             $"{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                             $"-  {wepType}  {wallBotExitIcon}" +
                             $"---{doorBotIcon}---";
                    }
                }
                else if (GetHasWeapon(0) && !GetWepClaimed())
                {
                    if (GetWeapon().GetName().CompareTo("Stick") == 0 || GetWeapon().GetName().CompareTo("Sword") == 0)
                    {
                        map += $"---{doorTopIcon}---" +
                             $"-  {enemyType}  {wallTopExitIcon}" +
                             $"{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                             $"-  {wepType} {wallBotExitIcon}" +
                             $"---{doorBotIcon}---";
                    }
                    else
                    {
                        map += $"---{doorTopIcon}---" +
                             $"-  {enemyType}  {wallTopExitIcon}" +
                             $"{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                             $"-  {wepType}  {wallBotExitIcon}" +
                             $"---{doorBotIcon}---";
                    }
                }
                else
                {
                    map += $"---{doorTopIcon}---" +
                         $"-  {enemyType}  {wallTopExitIcon}" +
                         $"{doorLeftIcon}  {playerPos}  {doorRightIcon}" +
                         $"-  {wepType}  {wallBotExitIcon}" +
                         $"---{doorBotIcon}---";
                }
            }
            else
            {
                map = "                                   ";  //a blank string long enough to count every character that would exist if the map was showing it
            }

            return map;
        }
    }
}
