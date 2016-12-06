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

        } // Solver



        /// <summary>
        /// Takes a turn on the provided board, and returns the resulting board.
        /// If the player completes a box, he will take another turn.
        /// </summary>
        /// <param name="theBoard">The board to play the turn</param>
        /// <returns>The board after playing this turn</returns>
        public Board TakeTurn(Board theBoard, out Side theSide)
        {
            // Create a new board
            Board NewBoard = new Board(theBoard);

            // Get the depth from the skill level
            int theDepth = (int)SkillLevel;

            // Start recursion using the max utility value
            Turn theTurn = MaxValue(theBoard, theDepth);

            // Get the chosen side
            theSide = theTurn.TheSide;

            // Claim the chosen side
            NewBoard.ClaimSide(theSide, PlayerID);

            // Return the board
            return NewBoard;

        } // TakeTurn



        /// <summary>
        /// Returns the board with the minimum utility value
        /// </summary>
        /// <param name="TheBoard"></param>
        /// <param name="theDepth"></param>
        /// <returns></returns>
        public Turn MinValue(Board TheBoard, int theDepth)
        {
            // Get the free sides of the current board
            List<Side> FreeSides = TheBoard.GetFreeSides();

            // Initialize the best turn
            Turn bestTurn = new Turn();

            // Assign the utility value of the board
            TheBoard.Utility = UtilityFunction(TheBoard);

            // Loop through the free sides of the board
            foreach (Side freeSide in FreeSides)
            {
                // Create a new board
                Board NewBoard = new Board(TheBoard);

                // Claim the current side
                NewBoard.ClaimSide(freeSide, Player.Player1);

                // Initialize the max turn
                Turn maxTurn = null;


                // Check if we have reached the depth limit
                if (theDepth == 0 || NewBoard.GameOver())
                {
                    // Calculate the utility function of the new board
                    NewBoard.Utility = UtilityFunction(NewBoard);

                    // Create a new turn with the new board and the side
                    maxTurn = new Turn(NewBoard, freeSide);
                }

                // Otherwise, continue recursion
                else
                {
                    // Get the max turn of the new board
                    maxTurn = MaxValue(NewBoard, theDepth - 1);
                }


                // If the max turn is less than the best turn, store it
                if (bestTurn.TheBoard == null || maxTurn.TheBoard.Utility < bestTurn.TheBoard.Utility)
                {
                    bestTurn.TheBoard = NewBoard;
                    bestTurn.TheSide = freeSide;
                }

            }


            // Return the best turn
            return bestTurn;
        }



        /// <summary>
        /// Returns the board with the maximum utility value
        /// </summary>
        /// <param name="NewBoard"></param>
        /// <param name="theDepth"></param>
        /// <returns></returns>
        public Turn MaxValue(Board TheBoard, int theDepth)
        {
            // Get the free sides of the current board
            List<Side> FreeSides = TheBoard.GetFreeSides();

            // Initialize the best turn
            Turn bestTurn = new Turn();

            // Assign the utility value of the board
            TheBoard.Utility = UtilityFunction(TheBoard);

            // Loop through the free sides of the board
            foreach (Side freeSide in FreeSides)
            {
                // Create a new board
                Board NewBoard = new Board(TheBoard);

                // Claim the current side
                NewBoard.ClaimSide(freeSide, PlayerID);

                // Initialize the max turn
                Turn minTurn = null;


                // Check if we have reached the depth limit
                if (theDepth == 0 || NewBoard.GameOver() )
                {
                    // Calculate the utility function of the new board
                    NewBoard.Utility = UtilityFunction(NewBoard);

                    // Create a new turn with the new board and the side
                    minTurn = new Turn(NewBoard, freeSide);
                }

                // Otherwise, continue recursion
                else
                {
                    // Get the min turn of the new board
                    minTurn = MinValue(NewBoard, theDepth - 1);
                }


                // If the min turn is greater than the best turn, store it
                if (bestTurn.TheBoard == null || minTurn.TheBoard.Utility > bestTurn.TheBoard.Utility)
                {
                    bestTurn.TheBoard = NewBoard;
                    bestTurn.TheSide = freeSide;
                }

            }


            // Return the best turn
            return bestTurn;
        }



        /// <summary>
        /// Returns the utility function of the provided board
        /// </summary>
        /// <param name="NewBoard"></param>
        /// <returns></returns>
        public int UtilityFunction (Board NewBoard)
        {
            // Initialize the utility value to 0
            int utility = 0;


            // Define the weights
            int WeightScore = 20;
            int WeightThree = 15;
            int WeightTwo   = 1;

            // Get the free sides for each box side count
            List<Side> FreeSidesBoxesWith2Claimed = NewBoard.GetFreeSidesFromBoxesWithSides( 2 );
            List<Side> FreeSidesBoxesWith3Claimed = NewBoard.GetFreeSidesFromBoxesWithSides( 3 );

            // Calculate the utility value
            utility += (NewBoard.GetScore( PlayerID ) * WeightScore) - (NewBoard.GetScore( Player.Player1 ) * WeightScore);
            utility += FreeSidesBoxesWith2Claimed.Count() * WeightTwo;
            utility -= FreeSidesBoxesWith3Claimed.Count() * WeightThree;


            // Return the utility value
            return utility;
        }

    } // Solver class


}
