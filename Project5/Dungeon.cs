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
        private double wepChance = 0.5;
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
                wepType = new Sword("Sword", 8);
            }
            else
            {
                wepType = new Stick("Stick", 6);
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
                return 1;
            }
            else { return 0; }
        }
    }
}
