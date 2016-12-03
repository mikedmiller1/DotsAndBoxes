using System.Drawing;

namespace DotsAndBoxes
{
    public class Dot
    {
        /// <summary>
        /// Properties
        /// </summary>
        public Point Coordinates;
        public int Row;
        public int Column;



        /// <summary>
        /// Constructor
        /// </summary>
        public Dot()
        {
        }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thePoint"></param>
        /// <param name="theRow"></param>
        /// <param name="theCol"></param>
        public Dot( Point thePoint, int theRow, int theCol )
        {
            Coordinates = thePoint;
            Row = theRow;
            Column = theCol;
        }
    }
}
