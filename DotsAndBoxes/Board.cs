namespace DotsAndBoxes
{
    public class Board
    {
        // Properties
        public readonly int NumRows;
        public readonly int NumCols;

        protected Box[,] _board;


        /// <summary>
        /// Constructor
        /// Creates a board of the specified size and initializes the boxes
        /// </summary>
        /// <param name="theRows">The number of rows</param>
        /// <param name="theCols">The number of columns</param>
        public Board( int theRows, int theCols )
        {
            // Set the rows and columns
            NumRows = theRows;
            NumCols = theCols;

            // Initialize the board
            _board = new Box[ NumCols, NumRows ];

            // Loop through the rows
            for( int RowNum = 0; RowNum < NumRows; RowNum++ )
            {
                // Loop through the columns
                for( int ColNum = 0; ColNum < NumCols; ColNum++ )
                {
                    // Create the box
                    _board[ ColNum, RowNum ] = new Box();
                }
            }
        }



        /// <summary>
        /// Constructor
        /// Creates a copy of the input board
        /// </summary>
        /// <param name="theBoard">The board to copy</param>
        public Board( Board theBoard )
        {
            this.NumRows = theBoard.NumRows;
            this.NumCols = theBoard.NumCols;
            this._board = theBoard._board;
        }



        /// <summary>
        /// Adds a side to the specified position
        /// Even rows are horizontal sides, odd rows are vertical sides
        /// Even columns are vertical sides, odd columns are horizontal sides
        /// </summary>
        /// <param name="RowNum">The row number of the side</param>
        /// <param name="ColNum">The column number of the side</param>
        /// <param name="Side">The side of the box</param>
        public void AddSide( int RowNum, int ColNum, BoxSide Side, Player thePlayer )
        {
            // Add the sides to the input box
            switch( Side )
            {
                case BoxSide.Top:
                    _board[ RowNum, ColNum ].Top = true;
                    break;

                case BoxSide.Bottom:
                    _board[ RowNum, ColNum ].Bottom = true;
                    break;

                case BoxSide.Left:
                    _board[ RowNum, ColNum ].Left = true;
                    break;

                case BoxSide.Right:
                    _board[ RowNum, ColNum ].Right = true;
                    break;
            }


            // Check if this completes the box
            if( _board[ RowNum, ColNum ].NumSides() == 4 )
            {
                _board[ RowNum, ColNum ].Owner = thePlayer;
            }


            // Add the bottom side to the box above
            if( RowNum > 0 && Side == BoxSide.Top )
            {
                _board[ RowNum - 1, ColNum ].Bottom = true;

                // Check if this completes the box
                if (_board[ RowNum - 1, ColNum ].NumSides() == 4)
                {
                    _board[ RowNum - 1, ColNum ].Owner = thePlayer;
                }
            }


            // Add the top side to the box below
            if( RowNum < NumRows - 1 && Side == BoxSide.Bottom )
            {
                _board[ RowNum + 1, ColNum ].Top = true;

                // Check if this completes the box
                if (_board[ RowNum + 1, ColNum ].NumSides() == 4)
                {
                    _board[ RowNum + 1, ColNum ].Owner = thePlayer;
                }
            }


            // Add the right side to the box on the left
            if( ColNum > 0 && Side == BoxSide.Left )
            {
                _board[ RowNum, ColNum - 1 ].Right = true;

                // Check if this completes the box
                if (_board[ RowNum, ColNum - 1 ].NumSides() == 4)
                {
                    _board[ RowNum, ColNum - 1 ].Owner = thePlayer;
                }
            }


            // Add the left side to the box on the right
            if( ColNum < NumCols - 1 && Side == BoxSide.Right )
            {
                _board[ RowNum, ColNum + 1 ].Left = true;

                // Check if this completes the box
                if (_board[ RowNum, ColNum + 1 ].NumSides() == 4)
                {
                    _board[ RowNum, ColNum + 1 ].Owner = thePlayer;
                }
            }

        }



        /// <summary>
        /// Returns the box at the specified index
        /// </summary>
        /// <param name="RowNum"></param>
        /// <param name="ColNum"></param>
        /// <returns></returns>
        public Box GetBox( int RowNum, int ColNum )
        {
            return _board[ RowNum, ColNum ];
        }



        /// <summary>
        /// Returns the number of boxes owned by the specified player
        /// </summary>
        /// <param name="thePlayer">The player's score to get</param>
        /// <returns>The number of boxes owned by the player</returns>
        public int GetScore( Player thePlayer )
        {
            // Initialize the score
            int theScore = 0;


            // Loop through the rows
            for (int RowNum = 0; RowNum < NumRows; RowNum++)
            {
                // Loop through the columns
                for (int ColNum = 0; ColNum < NumCols; ColNum++)
                {
                    // If the current box belongs to the player
                    if (_board[ ColNum, RowNum ].Owner == thePlayer )
                    {
                        // Increment the score
                        theScore++;
                    }
                }
            }


            // Return the score
            return theScore;
        }



        /// <summary>
        /// Returns a boolean indicating if the game is over
        /// </summary>
        /// <returns>Flag is true when all boxes have 4 sides</returns>
        public bool GameOver()
        {
            // Loop through the rows
            for (int RowNum = 0; RowNum < NumRows; RowNum++)
            {
                // Loop through the columns
                for (int ColNum = 0; ColNum < NumCols; ColNum++)
                {
                    // If the current box has less than 4 sides
                    if( _board[ ColNum, RowNum ].NumSides() < 4 )
                    {
                        // The game is not over
                        return false;
                    }
                }
            }


            // If we get here, all of the boxes have 4 sides and the game is over
            return true;

        } //GameOver


    } // Board class
}
