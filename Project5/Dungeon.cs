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
        private Player player;


        public Dungeon(Player player)
        {
            Random random = new Random();
            this.player = player;
            this.roomCount = random.Next(5, 11);
            this.currRoom = 0;
            wepRoom = random.Next(roomCount);
            if (random.NextDouble() < wepChance)
            {
                wepType = new Sword("Sword", 3);
            }
            else if (random.NextDouble() < (wepChance * 2))
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
                cells[i] = new Cell(i, roomCount, wepRoom, wepType, player);
            }

        }

        public int GetCurrRoom()
        {
            return this.currRoom;
        }

        public string GetCellMap(int room)
        {
            return cells[room].ToString();
        }

        public int GoLeft()
        {
            if (cells[this.currRoom].GetLeftDoor())
            {
                cells[this.currRoom].SetActiveRoom(false);
                currRoom--;
                cells[this.currRoom].SetActiveRoom(true);
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
                cells[this.currRoom].SetActiveRoom(false);
                currRoom++;
                cells[this.currRoom].SetActiveRoom(true);
                cells[this.currRoom].SetRoomEntered();
                return 1;
            }
            else { return 0; }
        }

        public int GetRoomCount()
        {
            return this.roomCount;
        }

        public bool GetRightDoor()
        {
            return cells[this.currRoom].GetRightDoor();
        }

        public bool GetLeftDoor()
        {
            return cells[this.currRoom].GetLeftDoor();
        }

        public bool GetExitDoor()
        {
            return cells[this.currRoom].GetExitDoor();
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

        /*
        public override string ToString()  //this is old version that works decently well
        {
            string map = "";
            for (int i = 0;i < roomCount; i++)
            {
                    map += cells[i].ToString(); //this doesn't look done yet?
            }
            return map;
        }
         */

        /*public override string ToString()  //this is newer version that works well but still vertical map
        {
            string map = "";
            for (int i = 0;i < roomCount; i++)
            {
                for (int j = 1; j <= cells[i].ToString().Length; j++)
                {
                    map += cells[i].ToString()[j-1]; //this doesn't look done yet?
                    if (j % 7 == 0)
                    {
                        map += "\n";
                    }
                }
            }
            return map;
        }*/

        public override string ToString()  //this is working with a horizontal map for 2d dungeon
        {
            string map = "";
            int loop = 0;
            int sev = 0;
            int cellLayers = 1;  //this will need to be updated to count how many Rows exist in 2d map
            int cellLayerLoop = 0;
            while (cellLayerLoop < cellLayers)
            {
                while (loop < 5)
                {
                    for (int i = 1; i <= roomCount; i++)
                    {
                        for (int j = 1; j <= 7; j++)
                        {
                            map += cells[i - 1].ToString()[(j + sev) - 1];
                        }
                    }
                    loop++;
                    sev += 7;
                    map += "\n";
                }
                cellLayerLoop++;
            }
            return map;
        }

        public string ToString(int mapType)  //testing to have a tostring for map row and whole map
        {
            string map = "";
            int loop = 0;
            int sev = 0;
            int cellLayers = 1;  //this will need to be updated to count how many Rows exist in 2d map
            int cellLayerLoop = 0;
            while (cellLayerLoop < cellLayers)
            {
                while (loop < 5)
                {
                    for (int i = 1; i <= roomCount; i++)
                    {
                        for (int j = 1; j <= 7; j++)
                        {
                            map += cells[i - 1].ToString()[(j + sev) - 1];
                        }
                    }
                    loop++;
                    sev += 7;
                    map += "\n";
                }
                cellLayerLoop++;
                if (mapType == 0)
                {
                    break;
                }
            }
            return map;
        }

        /*public string ToString(int type)  //this was a test but wasn't working well
        {
            string map = "";
            int listSize = cells.Count();
            //char[] cellMap;
            List<char[]> cellMap = new List<char[]>(listSize); //tried to change this to a list of char[] but this looks way worse
            for (int i = 1; i < 6; i++)
            {
                for (int j = 1; j <= roomCount; j++)
                {
                    int offset = 0;
                    cellMap.Add(cells[j - 1].ToString().ToCharArray()); //this doesn't look done yet, also changed it to do list stuff here for testing
                    Console.WriteLine(cellMap.Count); //changed this from length to count for testing
                    foreach (char c in cellMap[j - 1])
                    { Console.Write(c); }
                    //Console.WriteLine($"{cellMap[0]} cellmap 0");
                    while ((offset + 8) < cellMap.Count) //changed this from length to count for testing
                    {
                        for (int k = 0; k < 7; k++)
                        {
                            map += cellMap[k + offset];
                        }
                        offset = offset + 8;
                    }
                }
            }

            return map;
        }*/
    }
}
