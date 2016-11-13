using System.Windows;

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
            Solver Player1 = new Solver( Player.Player1 );
            Solver Player2 = new Solver( Player.Player2 );

            // Initialize the player turn flag
            Player Turn = Player.Player1;


            // Loop until the game is finished
            while (!TheBoard.GameOver())
            {
                // Player 1
                if (Turn == Player.Player1)
                {
                    // Take the turn
                    TheBoard = Player1.TakeTurn( TheBoard );

                    // Set the turn flag to player 2
                    Turn = Player.Player2;
                }


                // Player 2
                else
                {
                    // Take the turn
                    TheBoard = Player2.TakeTurn( TheBoard );

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
