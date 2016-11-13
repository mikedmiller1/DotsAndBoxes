using System;
using System.Diagnostics;


namespace DotsAndBoxes
{
    class Solver
    {
        /// <summary>
        /// Properties
        /// </summary>
        public readonly Player PlayerID;
        public static Random R = new Random();



        /// <summary>
        /// Constructor
        /// Creates a new solver with the specified player ID
        /// </summary>
        /// <param name="thePlayer">The player ID</param>
        public Solver( Player thePlayer )
        {
            // Check that the player ID is not None
            Debug.Assert( thePlayer != Player.None, "Player ID cannot be None." );

            // Set the player ID
            PlayerID = thePlayer;
        }



        /// <summary>
        /// Takes a turn on the provided board, and returns the resulting board.
        /// If the player completes a box, he will take another turn.
        /// </summary>
        /// <param name="theBoard">The board to play the turn</param>
        /// <returns>The board after playing this turn</returns>
        public Board TakeTurn( Board theBoard )
        {


            // Return the board
            return theBoard;
        }





    }
}
