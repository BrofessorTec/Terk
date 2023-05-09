using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    public class Dungeon
    {
        private int lengthTot;
        private int heightTot;
        private int activeRoom;
        private int currLength;
        private int currHeight;
        private int cellCount;
        private int exitHeight;
        private Cell[] cells;
        private double wepChance = 0.2;
        private Weapon wepType;
        private int wepRoom;
        private Player player;


        public Dungeon(Player player)
        {
            Random random = new Random();
            this.player = player;
            this.lengthTot = random.Next(6, 11);
            this.heightTot = random.Next(2, 6);
            this.exitHeight = random.Next(0, (heightTot));
            this.activeRoom = 0;
            this.currLength = 0;
            this.currHeight = 0;
            this.cellCount = 0;
            int cellOffset = 0;
            wepRoom = random.Next(2, (lengthTot * heightTot));
            if (random.NextDouble() < wepChance)
            {
                wepType = new Stick("Stick", 1);
            }
            else if (random.NextDouble() < (wepChance * 2))
            {
                wepType = new Knife("Knife", 2);
            }
            else if (random.NextDouble() < (wepChance * 3))
            {
                wepType = new Sword("Sword", 3);
            }
            else if (random.NextDouble() < (wepChance * 4))
            {
                wepType = new Laser("Laser", 4);
            }
            else if (random.NextDouble() < (wepChance * 5))
            {
                wepType = new Gun("Gun", 5);
            }

            this.cells = new Cell[lengthTot * heightTot];
            while (cellCount < lengthTot * heightTot)
            {
                for (int j = 0; j < heightTot; j++)
                {
                    for (int i = 0; i < lengthTot; i++)
                    {
                        cells[i+cellOffset] = new Cell(i, j, heightTot, lengthTot, exitHeight, wepRoom, wepType, player);
                        cellCount++;
                    }
                    cellOffset += lengthTot;
                }
            }

        }

        public int GetActiveRoom()
        {
            return this.activeRoom;
        }

        public int GetCurrHeight()
        {
            return this.currHeight;
        }

        public int GetCurrLength()
        {
            return this.currLength;
        }

        public string GetCellMap(int room)
        {
            return cells[room].ToString();
        }

        public int GoLeft()
        {
            if (cells[this.activeRoom].GetLeftDoorLock())
            {
                cells[this.activeRoom].SetLeftDoorLock(cells[this.activeRoom].GetLeftDoorLock());
                return 2;
            }
            else if (currLength > 0)
            {
                if (cells[this.activeRoom - 1].GetRightDoorLock())
                {
                    cells[this.activeRoom].SetRightDoorLock(cells[this.activeRoom - 1].GetRightDoorLock());
                    return 2;
                }
                else if (cells[this.activeRoom].GetLeftDoor())
                {
                    cells[this.activeRoom].SetActiveRoom(false);
                    activeRoom--;
                    currLength--;
                    cells[this.activeRoom].SetActiveRoom(true);
                    cells[this.activeRoom].SetRoomEntered();
                    return 1;
                }
                else { return 0; }
            }  //testing this
            else if (cells[this.activeRoom].GetLeftDoor())
            {
                cells[this.activeRoom].SetActiveRoom(false);
                activeRoom--;
                currLength--;
                cells[this.activeRoom].SetActiveRoom(true);
                cells[this.activeRoom].SetRoomEntered();
                return 1;
            }
            else { return 0; }
        }

        public int GoRight()
        {
            if (cells[this.activeRoom].GetExitDoor())
            {
                //Console.WriteLine("Congrats! You win!"); //can move this win code to the driver probably for it to handle
                return -1;
            }
            else if (cells[this.activeRoom].GetRightDoorLock())
            {
                cells[this.activeRoom].SetRightDoorLock(cells[this.activeRoom].GetRightDoorLock());
                return 2;
            }
            else if (currLength < lengthTot)
            {
                if (cells[this.activeRoom + 1].GetLeftDoorLock())
                {
                    cells[this.activeRoom].SetRightDoorLock(cells[this.activeRoom + 1].GetLeftDoorLock());
                    return 2;
                }
                else if (cells[this.activeRoom].GetRightDoor())
                {
                    cells[this.activeRoom].SetActiveRoom(false);
                    activeRoom++;
                    currLength++;
                    cells[this.activeRoom].SetActiveRoom(true);
                    cells[this.activeRoom].SetRoomEntered();
                    return 1;
                }
                else { return 0; }
            }  //testing this
            else if (cells[this.activeRoom].GetRightDoor())
            {
                cells[this.activeRoom].SetActiveRoom(false);
                activeRoom++;
                currLength++;
                cells[this.activeRoom].SetActiveRoom(true);
                cells[this.activeRoom].SetRoomEntered();
                return 1;
            }
            else { return 0; }
        }

        public int GoUp()
        {
            if (cells[this.activeRoom].GetTopDoorLock())
            {
                cells[this.activeRoom].SetTopDoorLock(cells[this.activeRoom].GetTopDoorLock());
                return 2;
            }
            else if (currHeight > 0)
            {
                if (cells[this.activeRoom - lengthTot].GetBotDoorLock())
                {
                    cells[this.activeRoom].SetTopDoorLock(cells[this.activeRoom - lengthTot].GetBotDoorLock());
                    return 2;
                }
                else if (cells[this.activeRoom].GetTopDoor())
                {
                    cells[this.activeRoom].SetActiveRoom(false);
                    //currHeight = currHeight + lengthTot;
                    currHeight--;
                    activeRoom = activeRoom - lengthTot;
                    cells[this.activeRoom].SetActiveRoom(true);
                    cells[this.activeRoom].SetRoomEntered();
                    return 1;
                }
                else { return 0; }
            }
            else if (cells[this.activeRoom].GetTopDoor())
            {
                cells[this.activeRoom].SetActiveRoom(false);
                //currHeight = currHeight - lengthTot;  //this wont work yet since it is using the array with the .activeRoom.... need to maybe 
                currHeight--;
                //activeRoom = ((activeRoom + 1) * (currHeight + 1));
                activeRoom = activeRoom - lengthTot;
                cells[this.activeRoom].SetActiveRoom(true);
                cells[this.activeRoom].SetRoomEntered();
                return 1;
            }
            else 
            { 
                return 0; 
            }
        }

        public int GoDown()
        {
            if (cells[this.activeRoom].GetBotDoorLock())
            {
                cells[this.activeRoom].SetBotDoorLock(cells[this.activeRoom].GetBotDoorLock());
                return 2;
            }
            else if (currHeight < heightTot)  
            {
                if (cells[this.activeRoom + lengthTot].GetTopDoorLock())
                {
                    cells[this.activeRoom].SetBotDoorLock(cells[this.activeRoom + lengthTot].GetTopDoorLock());  // for better performance could just make this Set bool be "true" since this if was true
                    return 2;
                }
                else if (cells[this.activeRoom].GetBotDoor())
                {
                    cells[this.activeRoom].SetActiveRoom(false);
                    //currHeight = currHeight + lengthTot;
                    currHeight++;
                    activeRoom = activeRoom + lengthTot;
                    cells[this.activeRoom].SetActiveRoom(true);
                    cells[this.activeRoom].SetRoomEntered();
                    return 1;
                }
                else { return 0; }
            }
            else if (cells[this.activeRoom].GetBotDoor())
            {
                cells[this.activeRoom].SetActiveRoom(false);
                //currHeight = currHeight + lengthTot;
                currHeight++;
                activeRoom = activeRoom + lengthTot;
                cells[this.activeRoom].SetActiveRoom(true);
                cells[this.activeRoom].SetRoomEntered();
                return 1;
            }
            else { return 0; }
        }

        public int GetLenghtCount()
        {
            return this.lengthTot;
        }

        public int GetHeightCount()
        {
            return this.heightTot;
        }

        public bool GetRightDoor()
        {
            return cells[this.activeRoom].GetRightDoor();
        }

        public bool GetLeftDoor()
        {
            return cells[this.activeRoom].GetLeftDoor();
        }

        public bool GetTopDoor()
        {
            return cells[this.activeRoom].GetTopDoor();
        }

        public bool GetTopDoorLock()
        {
            return cells[this.activeRoom].GetTopDoorLock();
        }
        public bool GetBotDoor()
        {
            return cells[this.activeRoom].GetBotDoor();
        }

        public bool GetBotDoorLock()
        {
            return cells[this.activeRoom].GetBotDoorLock();
        }

        public bool GetExitDoor()
        {
            return cells[this.activeRoom].GetExitDoor();
        }

        public bool RoomHasWep(int checkType)
        {
            return cells[this.activeRoom].GetHasWeapon(checkType);
        }

        public bool RoomHasPot(int checkType)
        {
            return cells[this.activeRoom].GetHasPotion(checkType);
        }

        public bool RoomHasMonster()
        {
            return cells[this.activeRoom].GetHasMonster();
        }

        public bool RoomWepClaimed()
        {
            return cells[this.activeRoom].GetWepClaimed();
        }

        public bool RoomPotClaimed()
        {
            return cells[this.activeRoom].GetPotClaimed();
        }
        public Weapon GetRoomWeapon()
        {
            return cells[this.activeRoom].GetWeapon();
        }

        public Monster GetRoomMonster()
        {
            return cells[this.activeRoom].GetMonster();
        }

        /*
        public override string ToString()  //this is old version that works decently well
        {
            string map = "";
            for (int i = 0;i < lengthTot; i++)
            {
                    map += cells[i].ToString(); //this doesn't look done yet?
            }
            return map;
        }
         */

        /*public override string ToString()  //this is newer version that works well but still vertical map
        {
            string map = "";
            for (int i = 0;i < lengthTot; i++)
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
            int cellLayers = heightTot;  //this will need to be updated to count how many Rows exist in 2d map
            int cellLayerLoop = 0;
            int cellOffset = 0;

            cellOffset = currHeight * lengthTot;
            while (cellLayerLoop < cellLayers)
            {
                while (loop < 5)
                {
                    for (int i = 1; i <= lengthTot; i++)
                    {
                        for (int j = 1; j <= 7; j++)
                        {
                            map += cells[(i - 1)+cellOffset].ToString()[(j + sev) - 1];
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
            int cellLayers = heightTot;  //this will need to be updated to count how many Rows exist in 2d map
            int cellLayerLoop = 0;
            int cellOffset = 0;
            while (cellLayerLoop < cellLayers)
            {
                loop = 0;
                sev = 0;
                    while (loop < 5)
                    {
                        for (int i = 1; i <= lengthTot; i++)
                        {
                            for (int j = 1; j <= 7; j++)
                            {
                                map += cells[i - 1+cellOffset].ToString()[(j + sev) - 1];
                            }
                        }
                        loop++;
                        sev += 7;
                        map += "\n";
                    }
                    cellLayerLoop++;
                    cellOffset += lengthTot;
            }
            return map;
        }

        /*public string ToString(int mapType)  //testing to have a tostring for map row and whole map, working version
        {
            string map = "";
            int loop = 0;
            int sev = 0;
            int cellLayers = heightTot;  //this will need to be updated to count how many Rows exist in 2d map
            int cellLayerLoop = 0;
            int cellOffset = 0;
            if (mapType == 0)
            {
                cellOffset += currHeight * lengthTot;
                while (loop < 5)
                {
                    for (int i = 1; i <= lengthTot; i++)
                    {
                        for (int j = 1; j <= 7; j++)
                        {
                            map += cells[(i - 1)+cellOffset].ToString()[(j + sev) - 1];
                        }
                    }
                    loop++;
                    sev += 7;
                    map += "\n";
                }
            }
            else
            {
                while (cellLayerLoop < cellLayers)
                {
                    while (loop < 5)
                    {
                        for (int i = 1; i <= lengthTot; i++)
                        {
                            for (int j = 1; j <= 7; j++)
                            {
                                map += cells[(i - 1)].ToString()[(j + sev) - 1];
                            }
                        }
                        loop++;
                        sev += 7;
                        map += "\n";
                    }
                    cellLayerLoop++;
                }
            }
            return map;
        }*/

        /*public string ToString(int type)  //this was a test but wasn't working well
        {
            string map = "";
            int listSize = cells.Count();
            //char[] cellMap;
            List<char[]> cellMap = new List<char[]>(listSize); //tried to change this to a list of char[] but this looks way worse
            for (int i = 1; i < 6; i++)
            {
                for (int j = 1; j <= lengthTot; j++)
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
