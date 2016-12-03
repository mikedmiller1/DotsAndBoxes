namespace DotsAndBoxes
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



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theSide">the Side to duplicate</param>
        public Side( Side theSide )
            : this( theSide.Row, theSide.Column, theSide.BoxSide )
        {
            // Assign the owner
            Owner = theSide.Owner;
        }



        /// <summary>
        /// Returns a boolean indicating if the specified side is equal to this side
        /// </summary>
        /// <param name="theOtherSide">The side to compare to</param>
        /// <returns>True if the sides are equal</returns>
        public bool Equals( Side theOtherSide )
        {
            // Initialize the equal flag
            bool IsEqual = false;


            // If the row, column and side are equal
            if( Row == theOtherSide.Row && Column == theOtherSide.Column && BoxSide == theOtherSide.BoxSide )
            {
                IsEqual = true;
            }


            // Return the equal flag
            return IsEqual;
        }
    }
}
