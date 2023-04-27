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
        private bool exitDoor;
        private double monsterChance = 0.5;
        private bool hasWeapon;
        private Weapon? roomWep = null;

        public Cell(int cellNum, int totSize, int wepRoom, Weapon wepType) 
        {
            Random random = new Random();
            if (random.NextDouble() < monsterChance)  //generating the monster
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
            return hasMonster; 
        } 

        public bool GetHasWeapon()
        {
            return hasWeapon;
        }
        public Weapon GetWeapon()
        {
            return roomWep;
        }
    }
}
