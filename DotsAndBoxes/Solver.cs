using System;
using System.Collections.Generic;
using System.Diagnostics;


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
        public Solver( Player thePlayer, Skill theSkill )
        {
            // Check that the player ID is not None
            Debug.Assert( thePlayer != Player.None, "Player ID cannot be None." );

            // Set the player ID
            PlayerID = thePlayer;

            // Set the skill level
            SkillLevel = theSkill;

            // Set the search depth based on the skill level
            SearchDepth = Convert.ToInt32( SkillLevel ) + 2;

        } // Solver



        /// <summary>
        /// Takes a turn on the provided board, and returns the resulting board.
        /// If the player completes a box, he will take another turn.
        /// </summary>
        /// <param name="theBoard">The board to play the turn</param>
        /// <returns>The board after playing this turn</returns>
        public Board TakeTurn( Board theBoard )
        {
            // Initialize the completed box flag
            bool CompletedBox = false;

            // Initialize the new board
            Board NewBoard;


            // Take a turn
            do
            {
                // Clear the completed box flag
                CompletedBox = false;


                // Run Minimax on the current board
                NewBoard = MiniMax( theBoard, SearchDepth );


                // If the player completed a box
                if( NewBoard.GetScore( PlayerID ) > theBoard.GetScore( PlayerID ) )
                {
                    // Set the flag to true to take another turn
                    CompletedBox = true;

                    // Make the new board the current board
                    theBoard = NewBoard;
                }
            }
            // Continue taking a turn as long as long as a box was completed
            while( CompletedBox && !theBoard.GameOver() );


            // Return the board
            return NewBoard;

        } // TakeTurn




        private Board MiniMax( Board theBoard, int theDepth )
        {
            // If the depth is less than 0, we have reached the depth limit
            if( theDepth < 0 )
            {
                // Return the board
                return theBoard;
            }


            // Create a copy of the board
            Board NewBoard = new Board( theBoard );



            // ***** FOR DEVELOPMENT ONLY *****
            // ***** PICK A RANDOM FREE LINE *****
            // Get the free sides
            List<Side> FreeSides = NewBoard.GetFreeSides();

            // Pick a random side number
            int SideNum = R.Next( 0, FreeSides.Count - 1 );

            // Get the random side
            Side RandomSide = FreeSides[ SideNum ];

            // Get the first free side
            Side FirstSide = FreeSides[ 0 ];

            // Add it to the board
            NewBoard.ClaimSide( RandomSide, PlayerID );



            // Return the new board
            return NewBoard;
        }



        


    } // Solver class
}
