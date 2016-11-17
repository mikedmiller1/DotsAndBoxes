﻿namespace DotsAndBoxes
{
    public class Side
    {
        /// <summary>
        /// Properties
        /// </summary>
        public Player Owner = Player.None;
        public readonly int Row;
        public readonly int Column;
        public readonly BoxSide BoxSide;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theRow">The row of the box</param>
        /// <param name="theCol">The column of the box</param>
        /// <param name="theBoxSide">The side of the box</param>
        public Side( int theRow, int theCol, BoxSide theBoxSide )
        {
            Row = theRow;
            Column = theCol;
            BoxSide = theBoxSide;
        }

        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theBox">The box</param>
        /// <param name="theBoxSide">The side of the box</param>
        public Side( Box theBox, BoxSide theBoxSide )
            : this( theBox.Row, theBox.Column, theBoxSide )
        {
        }



    }
}