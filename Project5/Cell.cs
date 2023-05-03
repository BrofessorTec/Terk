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
        private bool exitDoor = false;
        private double monsterChance = 0.5;
        private Monster monster;
        private bool hasWeapon;
        private Weapon? roomWep = null;
        private bool wepClaimed = false;
        private bool roomEntered = false;
        private Player player;
        private bool activeRoom = false;

        public Cell(int cellNum, int totSize, int wepRoom, Weapon wepType, Player player) 
        {
            Random random = new Random();
            monster = new Monster();
            this.player = player;

            if ((random.NextDouble() < monsterChance) && cellNum != 0)  //generating the monster
            {
                this.hasMonster = true;
            }
            else
            {
                this.hasMonster = false;
            }

            if (cellNum == 0)    //generating the doors for cells
            {
                leftDoor = false;
                rightDoor = true;
                exitDoor = false;
                roomEntered = true;
                activeRoom = true;
            }
            else if (cellNum < (totSize-1)) 
            {
                leftDoor = true;
                rightDoor = true;
                exitDoor = false;
            }
            else if (cellNum == (totSize-1))
            {
                leftDoor = true;
                rightDoor = true;
                exitDoor = true;
            }

            if (cellNum == wepRoom) //generating the weapon info
            {
                this.hasWeapon = true;
                roomWep = wepType;
            }
            else
            { 
                this.hasWeapon = false;
                roomWep = null;
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

        public bool GetExitDoor() 
        {  
            return exitDoor; 
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

        public bool GetWepClaimed()
        {
            return wepClaimed;
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

        public override string ToString()
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
                    /*doorExit = true;
                    doorExitIcon = "[]";*/   // for now since just displaying one cell, i can just override the right door icon for the exit
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
                map = "not yet entered"; //will change this later, this is for testing
            }

            return map;
        }
    }
}
