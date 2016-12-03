using System.Collections.Generic;
using System.Windows.Forms;

namespace DotsAndBoxes
{
    class GameTest
    {
        public static void Test()
        {
            // Define the board size
            int Rows = 3;
            int Cols = Rows;

            // Create a new board
            Board TheBoard = new Board( Rows, Cols );

            // Create a solver for each player
            Solver Player1 = new Solver( Player.Player1, Skill.Beginner );
            Solver Player2 = new Solver( Player.Player2, Skill.Expert );

            // Initialize the player turn flag
            Player Turn = Player.Player1;


            // Loop until the game is finished
            while (!TheBoard.GameOver())
            {
                // Player 1
                if (Turn == Player.Player1)
                {
                    // Take the turn
                    List<Side> TheSides = new List<Side>();
                    TheBoard = Player1.TakeTurn( TheBoard, out TheSides );

                    // Set the turn flag to player 2
                    Turn = Player.Player2;
                }


                // Player 2
                else
                {
                    // Take the turn
                    List<Side> TheSides = new List<Side>();
                    TheBoard = Player2.TakeTurn( TheBoard, out TheSides );

                    // Set the turn flag to player 1
                    Turn = Player.Player1;
                }
            }


            // Get the scores
            int Player1Score = TheBoard.GetScore( Player.Player1 );
            int Player2Score = TheBoard.GetScore( Player.Player2 );

            // Display the result
            MessageBox.Show( "Game Over!  Player 1: " + Player1Score.ToString() + ", Player 2: " + Player2Score.ToString() );
        }
    }
}
