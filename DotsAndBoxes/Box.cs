namespace DotsAndBoxes
{
    public class Box
    {
        /// <summary>
        /// Properties
        /// </summary>
        public Player Owner = Player.None;

        public readonly int Row;
        public readonly int Column;

        public Side Top;
        public Side Bottom;
        public Side Left;
        public Side Right;
        


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theRow">The row index</param>
        /// <param name="theColumn">The column index</param>
        public Box( int theRow, int theColumn )
        {
            Row    = theRow;
            Column = theColumn;
            Top    = new Side( theRow, theColumn, BoxSide.Top );
            Bottom = new Side( theRow, theColumn, BoxSide.Bottom );
            Left   = new Side( theRow, theColumn, BoxSide.Left );
            Right  = new Side( theRow, theColumn, BoxSide.Right );
        }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theRow">The row index</param>
        /// <param name="theColumn">The column index</param>
        /// <param name="theSide">The side to set</param>
        public Box( int theRow, int theColumn, Side theSide )
            : this( theRow, theColumn )  // Calls the base constructor
        {
            ClaimSide( theSide, Player.None );
        }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theBox">The Box to duplicate</param>
        public Box( Box theBox )
        {
            // Assign the rows and columns
            Row    = theBox.Row;
            Column = theBox.Column;

            // Duplicate the sides
            Top    = new Side( theBox.Top );
            Bottom = new Side( theBox.Bottom );
            Left   = new Side( theBox.Left );
            Right  = new Side( theBox.Right );

            // Assign the owner
            Owner  = theBox.Owner;
        }



        /// <summary>
        /// Adds a side to the box
        /// </summary>
        /// <param name="theSide"></param>
        /// <param name="thePlayer"></param>
        public void ClaimSide( BoxSide theSide, Player thePlayer )
        {
            // Add the side to the box
            switch( theSide )
            {
                case BoxSide.Top:
                    Top.Owner = thePlayer;
                    break;

                case BoxSide.Bottom:
                    Bottom.Owner = thePlayer;
                    break;

                case BoxSide.Left:
                    Left.Owner = thePlayer;
                    break;

                case BoxSide.Right:
                    Right.Owner = thePlayer;
                    break;
            }

            // Check if this completes the box
            if( thePlayer != Player.None && NumSides() == 4 )
            {
                Owner = thePlayer;
            }
        }



        /// <summary>
        /// Adds a side to the box
        /// </summary>
        /// <param name="theSide">The side to add</param>
        public void ClaimSide( Side theSide, Player thePlayer )
        {
            ClaimSide( theSide.BoxSide, thePlayer );
        }



        /// <summary>
        /// The number of sides of the box
        /// </summary>
        /// <returns>The number of sides</returns>
        public int NumSides()
        {
            // Initialize the number of sides
            int theSides = 0;


            // Top
            if( Top.Owner != Player.None ) { theSides++; }

            // Bottom
            if( Bottom.Owner != Player.None) { theSides++; }

            // Left
            if( Left.Owner != Player.None) { theSides++; }

            // Right
            if( Right.Owner != Player.None) { theSides++; }


            // Return the number of sides
            return theSides;
        }



    }
}
