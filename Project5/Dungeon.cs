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
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    public class Dungeon
    {
        private int roomCount;
        private int currRoom;
        private Cell[] cells;
        private double wepChance = 0.2;
        private Weapon wepType;
        private int wepRoom;


        public Dungeon() 
        {
            Random random = new Random();
            this.roomCount = random.Next(5,11);
            this.currRoom = 0;
            wepRoom = random.Next(roomCount);
            if (random.NextDouble() < wepChance)
            {
                wepType = new Sword("Sword", 3);
            }
            else if (random.NextDouble() < (wepChance*2))
            {
                wepType = new Stick("Stick", 1);
            }
            else if (random.NextDouble() < (wepChance * 3))
            {
                wepType = new Knife("Knife", 2);
            }
            else if (random.NextDouble() < (wepChance * 4))
            {
                wepType = new Laser("Laser", 4);
            }
            else if (random.NextDouble() < (wepChance * 5))
            {
                wepType = new Gun("Gun", 5);
            }

            this.cells = new Cell[roomCount];
            for (int i = 0; i < roomCount; i++)
            {
                cells[i] = new Cell(i, roomCount, wepRoom, wepType);
            }

        }

        public int GetCurrRoom()
        { 
            return this.currRoom;
        }

        public int GoLeft()
        {
            if (cells[this.currRoom].GetLeftDoor())
            {
                currRoom--;
                return 1;
            }
            else { return 0; }
        }

        public int GoRight()
        {
            if (cells[this.currRoom].GetExitDoor())
            {
                //Console.WriteLine("Congrats! You win!"); //can move this win code to the driver probably for it to handle
                return -1;
            }
            else if (cells[this.currRoom].GetRightDoor())
            {
                currRoom++;
                return 1;
            }
            else { return 0; }
        }

        public int GetRoomCount()
        {
            return this.roomCount;
        }
        public bool RoomHasWep(int checkType)
        {
            return cells[this.currRoom].GetHasWeapon(checkType);
        }

        public bool RoomHasMonster()
        {
            return cells[this.currRoom].GetHasMonster();
        }

        public bool RoomWepClaimed()
        {
            return cells[this.currRoom].GetWepClaimed();
        }
        public Weapon GetRoomWeapon()
        {
            return cells[this.currRoom].GetWeapon();
        }

        public Monster GetRoomMonster()
        {
            return cells[this.currRoom].GetMonster();
        }
    }
}
