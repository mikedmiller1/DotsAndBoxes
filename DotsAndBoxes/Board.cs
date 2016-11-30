using System;
using System.Collections.Generic;

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
            _board = new Box[ NumRows, NumCols ];

            // Loop through the rows
            for( int RowNum = 0; RowNum < NumRows; RowNum++ )
            {
                // Loop through the columns
                for( int ColNum = 0; ColNum < NumCols; ColNum++ )
                {
                    // Create a new box
                    _board[ RowNum, ColNum ] = new Box( RowNum, ColNum );
                }
            }
        }



        /// <summary>
        /// Constructor
        /// Creates a copy of the input board
        /// </summary>
        /// <param name="theBoard">The board to copy</param>
        public Board( Board theBoard )
            : this( theBoard.NumRows, theBoard.NumCols )
        {
            // Get all the sides of the board to copy
            List<Side> AllSides = theBoard.GetAllSides();

            // Loop through the sides
            foreach( Side CurrentSide in AllSides )
            {
                // Add the side to the board
                ClaimSide( CurrentSide, CurrentSide.Owner );
            }
        }



        /// <summary>
        /// Adds a side to the specified position
        /// Adding a side to a box also adds the corresponding side to the adjacent box
        /// </summary>
        /// <param name="RowNum">The row of the box</param>
        /// <param name="ColNum">The column of the box</param>
        /// <param name="theSide">The side of the box</param>
        public void ClaimSide( int RowNum, int ColNum, BoxSide theSide, Player thePlayer )
        {
            // Add the side to the input box
            _board[ RowNum, ColNum ].ClaimSide( theSide, thePlayer );


            // Add the bottom side to the box above
            if( RowNum > 0 && theSide == BoxSide.Top )
            { _board[ RowNum - 1, ColNum ].ClaimSide( BoxSide.Bottom, thePlayer ); }

            // Add the top side to the box below
            if( RowNum < NumRows - 1 && theSide == BoxSide.Bottom )
            { _board[ RowNum + 1, ColNum ].ClaimSide( BoxSide.Top, thePlayer ); }

            // Add the right side to the box on the left
            if( ColNum > 0 && theSide == BoxSide.Left )
            { _board[ RowNum, ColNum - 1 ].ClaimSide( BoxSide.Right, thePlayer ); }

            // Add the left side to the box on the right
            if( ColNum < NumCols - 1 && theSide == BoxSide.Right )
            { _board[ RowNum, ColNum + 1 ].ClaimSide( BoxSide.Left, thePlayer ); }

        }



        /// <summary>
        /// Adds a side to the specified position
        /// Adding a side to a box also adds the corresponding side to the adjacent box
        /// </summary>
        /// <param name="theSide">The Side to claim</param>
        /// <param name="thePlayer"></param>
        public void ClaimSide( Side theSide, Player thePlayer )
        {
            ClaimSide( theSide.Row, theSide.Column, theSide.BoxSide, thePlayer );
        }



        /// <summary>
        /// Returns a list of Sides representing the available sides on the board
        /// </summary>
        /// <returns></returns>
        public List<Side> GetFreeSides()
        {
            // Initialize the free side list
            List<Side> theFreeSides = new List<Side>();


            // Loop through the rows
            for (int RowNum = 0; RowNum < NumRows; RowNum++)
            {
                // Loop through the columns
                for (int ColNum = 0; ColNum < NumCols; ColNum++)
                {
                    // If the top side is free
                    if( _board[ RowNum, ColNum ].Top.Owner == Player.None )
                    { theFreeSides.Add( new Side( RowNum, ColNum, BoxSide.Top ) ); }

                    // If the bottom side is free
                    if( _board[ RowNum, ColNum ].Bottom.Owner == Player.None)
                    { theFreeSides.Add( new Side( RowNum, ColNum, BoxSide.Bottom ) ); }

                    // If the left side is free
                    if( _board[ RowNum, ColNum ].Left.Owner == Player.None)
                    { theFreeSides.Add( new Side( RowNum, ColNum, BoxSide.Left ) ); }

                    // If the right side is free
                    if( _board[ RowNum, ColNum ].Right.Owner == Player.None)
                    { theFreeSides.Add( new Side( RowNum, ColNum, BoxSide.Right ) ); }
                }
            }

            
            // Return the free side list
            return theFreeSides;
        }



        /// <summary>
        /// Returns a list of Sides of all the sides on the board
        /// </summary>
        /// <returns></returns>
        public List<Side> GetAllSides()
        {
            // Initialize the side list
            List<Side> theSides = new List<Side>();


            // Loop through the rows
            for (int RowNum = 0; RowNum < NumRows; RowNum++)
            {
                // Loop through the columns
                for (int ColNum = 0; ColNum < NumCols; ColNum++)
                {
                    // Add the sides
                    theSides.Add( _board[ RowNum, ColNum ].Top );
                    theSides.Add( _board[ RowNum, ColNum ].Bottom );
                    theSides.Add( _board[ RowNum, ColNum ].Left );
                    theSides.Add( _board[ RowNum, ColNum ].Right );
                }
            }


            // Return the side list
            return theSides;
        }



        /// <summary>
        /// Returns a boolean indicating if the specified side is free
        /// </summary>
        /// <param name="theSide">The Side to check</param>
        /// <returns>True if the side is free</returns>
        public bool IsSideFree( Side theSide )
        {
            // Initialize the free flag
            bool IsFree = false;


            // Get the list of free sides
            List<Side> FreeSides = GetFreeSides();

            // Loop through the free sides
            foreach (Side CurrentSide in FreeSides)
            {
                // If the current side matches the input side
                if (theSide.Equals( CurrentSide ))
                {
                    // The side is not free
                    IsFree = true;
                }
            }


            // Return the free flag
            return IsFree;
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
