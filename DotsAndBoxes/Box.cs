namespace DotsAndBoxes
{
    public class Box
    {
        // Box borders
        private bool _top = false;
        public bool Top
        {
            get { return _top; }
            set { _top = value; }
        }

        private bool _bottom = false;
        public bool Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        private bool _left = false;
        public bool Left
        {
            get { return _left; }
            set { _left = value ; }
        }

        private bool _right = false;
        public bool Right
        {
            get { return _right; }
            set { _right = value; }
        }



        // Box owner
        private Player _owner = Player.None;
        public Player Owner
        {
            get { return _owner; }
            set { _owner = value; }
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
            if( Top ) { theSides++; }

            // Bottom
            if( Bottom ) { theSides++; }

            // Left
            if( Left ) { theSides++; }

            // Right
            if( Right ) { theSides++; }


            // Return the number of sides
            return theSides;
        }



    }
}
