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
        private Weapon? wepType;
        private int wepRoom;
        private Player player;


        public Dungeon(Player player)
        {
            Random random = new Random();
            this.player = player;
            this.lengthTot = random.Next(6, 11);
            this.heightTot = random.Next(4, 6);   //set this back to 4,6 after. testing 4 that way there is always 2 middle layers for generation
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
            else if (random.NextDouble() < (wepChance * 2.0))
            {
                wepType = new Knife("Knife", 2);
            }
            else if (random.NextDouble() < (wepChance * 3.0))
            {
                wepType = new Sword("Sword", 3);
            }
            else if (random.NextDouble() < (wepChance * 4.0))
            {
                wepType = new Laser("Laser", 4);
            }
            else if (random.NextDouble() <= (wepChance * 5.0))
            {
                wepType = new Gun("Gun", 5);
            }
            else
            {
                wepType = new Knife("Knife", 2);
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

            // add new logic for locking cells below this at the dungeon level sending the command to lock to the cell

            //also need to keep in mind that the locked doors are supposed to be hidden unlock the player tries to pass through it. 
            //this means there should probably be one method to lock the door initially, and one to make the lock actually close out the door when player enters it
            int cornerUnlock = random.Next(1, 4);
            //Console.WriteLine(cornerUnlock);  //printing this for testing
            //int cornerCheck = 0;
            cellCount = 0;
            cellOffset = 0;

            while (cellCount < lengthTot * heightTot)
            {
                for (int j = 0; j < heightTot; j++)
                {
                    for (int i = 0; i < lengthTot; i++)
                    {
                        //Console.WriteLine(cells[i+cellOffset].GetTotalDoorUnlock());  //for testing, need to remove later
                        if (j == 0)
                        {
                            /*if (i + cellOffset == 1)  //this looks to work for the first room to the right of the start
                            {
                                if (random.NextDouble() < 0.5)
                                {
                                    if (random.NextDouble() < 0.5)
                                    {
                                        cells[i + cellOffset].HideRightDoorLock();
                                        cells[i + cellOffset + 1].HideLeftDoorLock();
                                    }
                                }
                                else
                                {
                                    if (random.NextDouble() < 0.5)
                                    {
                                        cells[i + cellOffset].HideBotDoorLock();
                                        cells[i + cellOffset + cellOffset].HideTopDoorLock();
                                    }
                                }
                            }*/

                            //Console.WriteLine($"i is {i} and length is {lengthTot}"); // this is for testing

                            /*if (i + cellOffset == 1)  //this looks to work for the first room to the right of the start
                            {
                                if (random.NextDouble() < 0.5)
                                {
                                    if (random.NextDouble() < 0.5)
                                    {
                                        cells[i + cellOffset].HideRightDoorLock();
                                        cells[i + cellOffset + 1].HideLeftDoorLock();
                                    }
                                    else
                                    {
                                        cells[i + cellOffset].HideBotDoorLock();
                                        cells[i + cellOffset + cellOffset].HideTopDoorLock();
                                    }
                                }
                            }
                            else */
                            if (i + 1 == (lengthTot))
                            {
                                if (cornerUnlock != 1)
                                {
                                    if (random.NextDouble() < 0.6)
                                    {
                                        if (random.NextDouble() < 0.5)
                                        {
                                            cells[i + cellOffset].HideLeftDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                            cells[i + cellOffset - 1].HideRightDoorLock();
                                        }
                                        else
                                        {
                                            cells[i + cellOffset].HideBotDoorLock();  //lock the door to the bottom and top
                                            cells[i + cellOffset + lengthTot].HideTopDoorLock();
                                        }
                                    }
                                }
                            }
                            else if (i + 1 == (lengthTot - 1)) //for floors beside this first one this should probably be i > 0
                            {
                                if (cells[i].GetTotalDoorUnlock() == 3) //for floors beside this first one this should probably be == 4 besides bottom floor
                                {
                                    double chance = random.NextDouble();
                                    double doorChance = random.NextDouble();
                                    //Console.WriteLine($"chance of locking is {chance} and less than 0.75 locks bot door and {lengthTot} is lengthtot");
                                    if (chance < 0.75)
                                    {
                                        cells[i + cellOffset].HideBotDoorLock();  //lock the door to the bottom and top
                                        cells[i + cellOffset + lengthTot].HideTopDoorLock();
                                    }
                                }
                            }
                            else if (i + 1 < lengthTot && i > 0) //for floors beside this first one this should probably be i >= 0
                            {
                                if (cells[i].GetTotalDoorUnlock() == 3) //for floors beside this first one this should probably be == 4 besides bottom floor
                                {
                                    double chance = random.NextDouble();
                                    double doorChance = random.NextDouble();
                                    //Console.WriteLine($"chance of locking is {chance} and less than 0.75 locks a door and {lengthTot} is lengthtot");
                                    //Console.WriteLine($"doorchance is {doorChance} and less than 0.5 locks the right door otherwise the bottom door is locked");
                                    if (chance < 0.75)
                                    {
                                        if (doorChance < 0.5)
                                        {
                                            cells[i + cellOffset].HideRightDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                            cells[i + cellOffset + 1].HideLeftDoorLock();
                                        }
                                        else
                                        {
                                            cells[i + cellOffset].HideBotDoorLock();  //lock the door to the bottom and top
                                            cells[i + cellOffset + lengthTot].HideTopDoorLock();
                                        }
                                    }
                                }
                            }
                        }
                        //add for the second column here? need to incoporate j into this somehow
                        else if (j + 1 < heightTot)
                        {
                            //Console.WriteLine($"i is {i} and length is {lengthTot}"); // this is for testing
                            //Console.WriteLine($"j is {j} and height is {heightTot}"); // this is for testing
                            /*if (j + 1 == (heightTot))  //this should probably just be for the bottom floor, i changed the height minimum to be 3 now
                            {
                                if (cornerUnlock != 2)
                                {
                                    if (random.NextDouble() < 0.6)
                                    {
                                        if (random.NextDouble() < 0.5)
                                        {
                                            cells[i + cellOffset].HideRightDoorLock();  //lock the door to the right and from cell to the right lock the door to the left
                                            cells[i + cellOffset + 1].HideLeftDoorLock();
                                        }
                                        else
                                        {
                                            cells[i + cellOffset].HideTopDoorLock();  //lock the door to the top and bottom
                                            cells[i + cellOffset - cellOffset].HideBotDoorLock();
                                        }
                                    }
                                }
                                else if (cornerUnlock != 3)
                                {
                                    if (random.NextDouble() < 0.6)
                                    {
                                        if (random.NextDouble() < 0.5)
                                        {
                                            cells[i + cellOffset].HideLeftDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                            cells[i + cellOffset - 1].HideRightDoorLock();
                                        }
                                        else
                                        {
                                            cells[i + cellOffset].HideTopDoorLock();  //lock the door to the top and bottom
                                            cells[i + cellOffset - cellOffset].HideBotDoorLock();
                                        }
                                    }
                                }*/

                            if (cells[i + cellOffset].GetTotalDoorUnlock() == 4) //for floors beside this first one this should probably be == 4 besides bottom floor
                            {
                                double chance = random.NextDouble();
                                double doorChance = random.NextDouble();
                                //Console.WriteLine($"chance of locking is {chance} and less than 0.75 locks 1 door and {lengthTot} is lengthtot. higher locks both");
                                //Console.WriteLine($"doorchance is {doorChance} and less than 0.5 locks the right door otherwise the bottom door is locked");
                                if (chance < 0.75)  //75% chance to lock 1 door and 25% chance to lock both doors
                                {
                                    if (doorChance < 0.5)
                                    {
                                        cells[i + cellOffset].HideRightDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                        cells[i + cellOffset + 1].HideLeftDoorLock();
                                    }
                                    else
                                    {
                                        cells[i + cellOffset].HideBotDoorLock();  //lock the door to the bottom and top
                                        cells[i + cellOffset + lengthTot].HideTopDoorLock(); //testing if changing this to length total fixes
                                    }
                                }
                                else
                                {
                                    cells[i + cellOffset].HideRightDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                    cells[i + cellOffset + 1].HideLeftDoorLock();
                                    cells[i + cellOffset].HideBotDoorLock();  //lock the door to the bottom and top
                                    cells[i + cellOffset + lengthTot].HideTopDoorLock();
                                }
                            }
                            else if (cells[i + cellOffset].GetTotalDoorUnlock() == 3) //for floors beside this first one this should probably be == 4 besides bottom floor
                            {
                                double chance = random.NextDouble();
                                double doorChance = random.NextDouble();
                                //Console.WriteLine($"chance of locking is {chance} and less than 0.5 locks a door and {lengthTot} is lengthtot");
                                //Console.WriteLine($"doorchance is {doorChance} and less than 0.5 locks the right door otherwise the bottom door is locked");
                                if (cells[i + cellOffset - 1].GetTotalDoorUnlock() != 2)
                                {
                                    if (chance < 0.5)
                                    {
                                        if (doorChance < 0.5)
                                        {
                                            cells[i + cellOffset].HideRightDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                            cells[i + cellOffset + 1].HideLeftDoorLock();
                                        }
                                        else
                                        {
                                            cells[i + cellOffset].HideBotDoorLock();  //lock the door to the bottom and top
                                            cells[i + cellOffset + lengthTot].HideTopDoorLock();
                                        }
                                    }
                                }
                                else
                                {
                                    if (i + 1 == lengthTot)
                                    {
                                        if (chance < 0.5)
                                        {
                                            cells[i + cellOffset].HideBotDoorLock();
                                            cells[i + cellOffset + lengthTot].HideTopDoorLock();
                                        }
                                    }
                                    else if (chance < 0.5)
                                    {
                                        cells[i + cellOffset].HideRightDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                        cells[i + cellOffset + 1].HideLeftDoorLock();
                                    }
                                }
                            }
                            /*if (cells[i + cellOffset + lengthTot].GetRightDoorLock() && cells[i + cellOffset - lengthTot].GetRightDoorLock() && cells[i + cellOffset].GetRightDoorLock())
                            {
                                cells[i + cellOffset].UnlockRightDoorLock();
                                cells[i + cellOffset + 1].UnlockLeftDoorLock();
                            }

                            if (cells[i + cellOffset + 1].GetBotDoorLock() && cells[i + cellOffset - 1].GetBotDoorLock() && cells[i + cellOffset].GetBotDoorLock())
                            {
                                cells[i + cellOffset].UnlockBotDoorLock();
                                cells[i + cellOffset + lengthTot].UnlockTopDoorLock();
                            }*/
                        }

                        else if (j + 1 == heightTot)
                        {
                            //add the final row locking logic here
                            if (i == 0)
                            {
                                //Console.WriteLine("test1");
                                if (cornerUnlock != 2)
                                {
                                    //Console.WriteLine("test2");
                                    //Console.WriteLine($"i+1 is {i + 1} and lengthtot is {lengthTot}");
                                    if (random.NextDouble() < 0.6)
                                    {
                                        if (random.NextDouble() < 0.5)
                                        {
                                            cells[i + cellOffset].HideRightDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                            cells[i + cellOffset + 1].HideLeftDoorLock();
                                        }
                                        else
                                        {
                                            cells[i + cellOffset].HideTopDoorLock();  //lock the door to the bottom and top
                                            cells[i + cellOffset - lengthTot].HideBotDoorLock();
                                        }
                                    }
                                }
                            }
                            else if (i + 1 == lengthTot)
                            {
                                //Console.WriteLine("test3");
                                //Console.WriteLine($"i+1 is {i + 1} and lengthtot is {lengthTot}");
                                if (cornerUnlock != 3)
                                {
                                    //Console.WriteLine("test4");
                                    if (random.NextDouble() < 0.6)
                                    {
                                        if (random.NextDouble() < 0.5)
                                        {
                                            cells[i + cellOffset].HideLeftDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                            cells[i + cellOffset - 1].HideRightDoorLock();
                                        }
                                        else
                                        {
                                            cells[i + cellOffset].HideTopDoorLock();  //lock the door to the bottom and top
                                            cells[i + cellOffset - lengthTot].HideBotDoorLock();
                                        }
                                    }
                                }
                            }
                            else if (cells[i + cellOffset].GetTotalDoorUnlock() == 3)
                            {
                                //Console.WriteLine("test5");
                                double chance = random.NextDouble();
                                double doorChance = random.NextDouble();
                                //Console.WriteLine($"chance of locking is {chance} and less than 0.5 locks a door and {lengthTot} is lengthtot");
                                //Console.WriteLine($"doorchance is {doorChance} and less than 0.5 locks the right door");
                                if (cells[i + cellOffset - 1].GetTotalDoorUnlock() != 2)
                                {
                                    //Console.WriteLine("test6");
                                    if (chance < 0.5)
                                    {
                                        cells[i + cellOffset].HideRightDoorLock();  //lock the door to the left and from cell to the left lock the door to the right
                                        cells[i + cellOffset + 1].HideLeftDoorLock();
                                    }
                                }
                            }
                        }
                        if (cells[i + cellOffset].GetTotalDoorUnlock() == 0)
                        {
                            //Console.WriteLine("test30");
                            cells[i + cellOffset].UnlockLeftDoorLock();
                            cells[i + cellOffset - 1].UnlockRightDoorLock();
                        }

                        if (exitHeight == j && cells[i + cellOffset].GetTotalDoorUnlock() == 1)
                        {
                                //Console.WriteLine("test20");
                                cells[i + cellOffset].UnlockLeftDoorLock();
                                cells[i + cellOffset - 1].UnlockRightDoorLock();

                        }

                        //final pass of dungeon to try and make sure it is winnable below here
                        //testing some logic here if door to the right is locked and room top right has bottom door locked, unlock door to the right
                        if (j + 1 < heightTot && j != 0 && i + 1 < lengthTot)
                        {
                            if (cells[i + cellOffset].GetRightDoorLock() && cells[i + cellOffset + 1 - lengthTot].GetBotDoorLock())
                            {
                                cells[i + cellOffset].UnlockRightDoorLock();
                                cells[i + 1 + cellOffset].UnlockLeftDoorLock();
                            }

                            if (cells[i + cellOffset].GetTotalDoorUnlock() == 4)  //if the above changed made it so all 4 doors are unlocked now, lock the bottom one so it's not too simple
                            {
                                cells[i + cellOffset].HideBotDoorLock();
                                cells[i + cellOffset + lengthTot].HideTopDoorLock();
                            }
                        }

                        if (j == 1 && i == 1 && cells[i + cellOffset].GetTotalDoorUnlock() == 2)  //there was a common box trap at the very beginning, this will just force the door open
                        {
                            cells[i + cellOffset].UnlockRightDoorLock();
                            cells[i + 1 + cellOffset].UnlockLeftDoorLock();
                        }

                        if (j + 1 == heightTot && i + 1 < lengthTot)  //if bottom floor, going the other route where you need to go up instead of going straight across the bottom
                        {
                            if (cells[i + cellOffset].GetRightDoorLock() && cells[i + cellOffset + 1 - lengthTot].GetBotDoorLock())
                            {
                                cells[i + cellOffset + 1 - lengthTot].UnlockBotDoorLock();
                                cells[i + 1 + cellOffset].UnlockTopDoorLock();
                            }
                            if (cells[i + cellOffset].GetRightDoorLock() && cells[i + cellOffset - lengthTot].GetTopDoorLock())  //unless the room above it has a locked door at top already
                            {
                                cells[i + cellOffset].UnlockRightDoorLock();
                                cells[i + 1 + cellOffset].UnlockLeftDoorLock();
                            }
                        }

                        if(i + 1 == lengthTot && j == exitHeight)  
                        {
                            if(exitHeight == 0 && cells[i].GetBotDoorLock() && cells[i+ lengthTot].GetTotalDoorUnlock() == 1)
                            {
                                cells[i].UnlockBotDoorLock();
                                cells[i + lengthTot].UnlockTopDoorLock();
                            }
                            else if (exitHeight == heightTot && cells[i+cellOffset].GetTopDoorLock() && cells[i + cellOffset - lengthTot].GetTotalDoorUnlock() == 1)
                            {
                                cells[i + cellOffset].UnlockTopDoorLock();
                                cells[i + cellOffset - lengthTot].UnlockBotDoorLock();
                            }
                            else if (cells[i+cellOffset].GetBotDoorLock() && cells[i +cellOffset + lengthTot].GetTotalDoorUnlock() == 1)
                            {
                                cells[i + cellOffset].UnlockBotDoorLock();
                                cells[i + cellOffset + lengthTot].UnlockTopDoorLock();
                            }
                            else if (cells[i+ cellOffset].GetTopDoorLock() && cells[i + cellOffset - lengthTot].GetTotalDoorUnlock() == 1)
                            {
                                cells[i + cellOffset].UnlockTopDoorLock();
                                cells[i + cellOffset - lengthTot].UnlockBotDoorLock();
                            }

                            if (cells[i + cellOffset].GetTotalDoorUnlock() == 2 && cells[i + cellOffset].GetLeftDoorLock())   //these are supposed to make sure the exit room always has at least 3 doors
                            {
                                cells[i + cellOffset].UnlockLeftDoorLock();
                                cells[i + cellOffset - 1].UnlockRightDoorLock();
                            }
                            else if (cells[i + cellOffset].GetTotalDoorUnlock() == 2 && cells[i + cellOffset].GetBotDoorLock())
                            {
                                cells[i + cellOffset].UnlockBotDoorLock();
                                cells[i + cellOffset + lengthTot].UnlockTopDoorLock();
                            }
                            else if (cells[i + cellOffset].GetTotalDoorUnlock() == 2 && cells[i + cellOffset].GetTopDoorLock())
                            {
                                cells[i + cellOffset].UnlockTopDoorLock();
                                cells[i + cellOffset-lengthTot].UnlockBotDoorLock();
                            }

                        }

                        cellCount++;
                    }
                    cellOffset += lengthTot;
                }
            }

            //add logic to check if there is a vertical and horizontal path available here?
            //this works, but can still lose if the closed off walls are perpendicular
            cellCount = lengthTot;
            cellOffset = lengthTot;

            while (cellCount < lengthTot * heightTot)  //does this work now??
            {
                for (int j = 1; j < heightTot; j++)
                {
                    for (int i = 0; i < lengthTot; i++)
                    {
                        if (j + 1 < heightTot)
                        {
                            if (i + 1 < lengthTot && i > 0)
                            {
                                if (cells[i + cellOffset + lengthTot].GetRightDoorLock() && cells[i + cellOffset - lengthTot].GetRightDoorLock() && cells[i + cellOffset].GetRightDoorLock())
                                {
                                    cells[i + cellOffset].UnlockRightDoorLock();
                                    cells[i + cellOffset + 1].UnlockLeftDoorLock();
                                }

                                if (cells[i + cellOffset + 1].GetBotDoorLock() && cells[i + cellOffset - 1].GetBotDoorLock() && cells[i + cellOffset].GetBotDoorLock())
                                {
                                    cells[i + cellOffset].UnlockBotDoorLock();
                                    cells[i + cellOffset + lengthTot].UnlockTopDoorLock();
                                }

                                int unlockTestCount = cells[i + cellOffset].GetTotalDoorUnlock();
                                //if (j + 1 == heightTot && unlockTestCount == 1)
                                if (unlockTestCount <= 1)
                                {
                                    Console.WriteLine("test420");   //this code doesnt seem to be reading at all, havent seen this test print
                                    cells[i + cellOffset].UnlockLeftDoorLock();
                                    cells[i + cellOffset - 1].UnlockRightDoorLock();
                                }
                            }
                        }
                        cellCount++;
                    }
                    cellOffset += lengthTot;
                }
            }

            //add logic here to move away from the final door and make sure that those doors have 3 doors available, that should help some?



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
            if (mapType != -1)
            {
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
                                map += cells[i - 1 + cellOffset].ToString()[(j + sev) - 1];
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
            else  //need to try to code this so that the whole map is shown
            {
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
                                map += cells[i - 1 + cellOffset].CheatMap()[(j + sev) - 1];
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
