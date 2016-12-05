using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DotsAndBoxes
{
    class Solver
    {
        /// <summary>
        /// Properties
        /// </summary>
        public readonly Player PlayerID;
        public readonly Skill SkillLevel;
        public static Random R = new Random();

        private readonly int SearchDepth;



        /// <summary>
        /// Constructor
        /// Creates a new solver with the specified player ID
        /// </summary>
        /// <param name="thePlayer">The player ID</param>
        public Solver(Player thePlayer, Skill theSkill)
        {
            // Check that the player ID is not None
            Debug.Assert(thePlayer != Player.None, "Player ID cannot be None.");

            // Set the player ID
            PlayerID = thePlayer;

            // Set the skill level
            SkillLevel = theSkill;

            // Set the search depth based on the skill level
            SearchDepth = Convert.ToInt32(SkillLevel) + 2;

        } // Solver



        /// <summary>
        /// Takes a turn on the provided board, and returns the resulting board.
        /// If the player completes a box, he will take another turn.
        /// </summary>
        /// <param name="theBoard">The board to play the turn</param>
        /// <returns>The board after playing this turn</returns>
        public Board TakeTurn(Board theBoard, out Side theSide)
        {
            // Run Minimax on the current board
            Board NewBoard = MiniMax(theBoard, SearchDepth, out theSide);

            // Return the board
            return NewBoard;

        } // TakeTurn




        private Board MiniMax(Board theBoard, int theDepth, out Side theSide)
        {
            // If the depth is less than 0, we have reached the depth limit
            if (theDepth < 0)
            {
                // Return the board
                // ***** FOR DEVELOPMENT ONLY *****
                // ***** WILL GET THE SIDE RECURSIVELY *****
                theSide = new Side(0, 0, BoxSide.Invalid);
                return theBoard;
            }


            // Create a copy of the board
            Board NewBoard = new Board(theBoard);




            // ***** FOR DEVELOPMENT ONLY *****
            // ***** PICK A RANDOM FREE LINE *****
            // Get the free sides
            List<Side> FreeSides = NewBoard.GetFreeSides();

            // get the number of free sides still avialable, below a certain amount we start minimax
            int numFreeSides = FreeSides.Count();


            // Pick a random side number
            int SideNum = R.Next(0, FreeSides.Count - 1);

            // Get the random side
            Side RandomSide = FreeSides[SideNum];

            // Get the first free side
            //Side FirstSide = FreeSides[0];

            // Use the side
            theSide = RandomSide;

            // group all boxes with their sides together
            // FreeSides.GroupBy(t =>new { t.Column, t.Row });

            // even in a open board, if a particular box has only 1 side in the free list, it means
            // that two sides will be assigned, thus we need to start minimax

            // FreeSides=FreeSides;
            // List<Side> potentialBoxes = new List<Side>();
            
            for (int i = 0; i < NewBoard.NumRows; i++)
            {
                for (int j = 0; j < NewBoard.NumCols; j++)
                {   // if any of these boxes have 1 sides still in free list, go get the next side based on heuristic
                    if (FreeSides.Where(t => t.Column.Equals(i) && t.Row.Equals(j)).Count() ==1)
                    {
                        theSide = getNextSide(NewBoard, FreeSides,theDepth);
                        break;
                    }

                }
            }

            //theSide = minSide;
            //NewBoard.ClaimSide(theSide, PlayerID);
            
               

                // Add it to the board
                NewBoard.ClaimSide(theSide, PlayerID);
                  

            // Return the new board
            return NewBoard;

        } // MiniMax

        /// <summary>
        /// This is supposed to be the min function. the computer picks the side that will give the
        /// human player the least amount of boxes.
        /// </summary>
        /// <param name="NewBoard"></param>
        /// <param name="FreeSides"></param>
        /// <returns></returns>
       
        public Side getNextSide(Board NewBoard, List<Side> FreeSides,int depth)
        {
            List<Side> potentialBoxes = new List<Side>();

            for (int i = 0; i < NewBoard.NumRows; i++)
            {
                for (int j = 0; j < NewBoard.NumCols; j++)
                {
                    if (FreeSides.Where(t => t.Column.Equals(i) && t.Row.Equals(j)).Count() == 1)
                    {
                        // Adds those boxes that have only 1 side left to fill, these are potential boxes
                        potentialBoxes.AddRange(FreeSides.Where(t => t.Column.Equals(i) && t.Row.Equals(j)));

                        break;
                    }
                }
            }

            // now we have a list of all the sides of the boxes available, and the list of all the boxes that only
            // have 1 side left given the sides that can form a box right now, we need to check the adjacent boxes 
            // and see if boxes can be made there so if a side corresponding to box 0 0 was present,then we need to 
            // check whether box 0 1 or 1 0 can be affected if a line was put there (i.e. how many boxes can human 
            // form if computer puts line here--> minimize that) pick the side with the least number of possible boxes

            // corner cases

            //I want the adjancet boxes of every box that only has one side left

            List<UtilitiesWithSides> sidesWithUtilities = new List<UtilitiesWithSides>();

            for (int i = 0; i < potentialBoxes.Count; i++)
            {
                int utility = 0;

                if (potentialBoxes[i].BoxSide == BoxSide.Top)
                {
                          
                    if (FreeSides.Where(t => t.Row == potentialBoxes[i].Row + 1 && t.Column == potentialBoxes[i].Column).Count() - 1 == 1)
                    {
                        utility++;
                    }

                    if (FreeSides.Where(t => t.Row == potentialBoxes[i].Row - 1 && t.Column == potentialBoxes[i].Column).Count() - 1 == 1)
                    {
                        utility++;
                    }

                    sidesWithUtilities.Add(new  UtilitiesWithSides(utility, potentialBoxes[i]));
                    
                    // putin check to see if count -1 = 0, then that has precedence, so subtract it by 1 more

                    //check count - 1 not just count, this gives direct measure if top influences adjancent box, 
                    // if count-1 is 1, then the human player can form box there, and thats a problem
                }
                else if (potentialBoxes[i].BoxSide == BoxSide.Bottom)
                {
                  //  for (int j = 0; j < depth; j++) { }
                    if (FreeSides.Where(t => t.Row == potentialBoxes[i].Row - 1 && t.Column == potentialBoxes[i].Column).Count() - 1 == 1)
                    {
                        utility++;
                    }
                    if (FreeSides.Where(t => t.Row == potentialBoxes[i].Row + 1 && t.Column == potentialBoxes[i].Column).Count() - 1 == 1)
                    {
                        utility++;
                    }

                    sidesWithUtilities.Add(new UtilitiesWithSides(utility, potentialBoxes[i]));
                    
                    //check count - 1 not just count
                }
                else if (potentialBoxes[i].BoxSide == BoxSide.Left)
                {
                    //for (int j = 0; j < depth; j++) { }
                    if (FreeSides.Where(t => t.Row == potentialBoxes[i].Row && t.Column == potentialBoxes[i].Column + 1).Count() - 1 == 1)
                    {
                        utility++;
                    }
                    if (FreeSides.Where(t => t.Row == potentialBoxes[i].Row && t.Column == potentialBoxes[i].Column - 1).Count() - 1 == 1)
                    {
                        utility++;
                    }

                    sidesWithUtilities.Add(new UtilitiesWithSides(utility, potentialBoxes[i]));
                        //check count - 1 not just count
                    
                }
                else
                {
                   // for (int j = 0; j < depth; j++) { }
                    if (FreeSides.Where(t => t.Row == potentialBoxes[i].Row && t.Column == potentialBoxes[i].Column + 1).Count() - 1 == 1)
                    {
                        utility++;
                    }
                    if (FreeSides.Where(t => t.Row == potentialBoxes[i].Row && t.Column == potentialBoxes[i].Column - 1).Count() - 1 == 1)
                    {
                        utility++;
                    }

                    sidesWithUtilities.Add(new UtilitiesWithSides(utility, potentialBoxes[i]));
                    

                }

            } // end of for loop



            //FreeSides.Where(t => t.Row == potentialBoxes[0].Row+1 && t.Column == potentialBoxes[0].Column);
            //FreeSides.Where(t => t.Row == potentialBoxes[0].Row-1 && t.Column == potentialBoxes[0].Column);
            //FreeSides.Where(t => t.Row == potentialBoxes[0].Row  && t.Column == potentialBoxes[0].Column+1);
            //FreeSides.Where(t => t.Row == potentialBoxes[0].Row  && t.Column == potentialBoxes[0].Column-1);
            // nowe we have every side that can make a box with its associated utility, pick the minimum one  and return it


            // pick the side with the minimum of all the utilities for the human to create the least amount of boxes with
            sidesWithUtilities.OrderBy(t => t.utility);

            return sidesWithUtilities[0].side;
        } // end of get side

    } // Solver class

    public class UtilitiesWithSides
    {
        public int utility;
        public Side side;
        public UtilitiesWithSides(int Utility, Side Side)
        {
            utility = Utility;
            side = Side;
        }
    }
}
