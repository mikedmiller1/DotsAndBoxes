namespace DotsAndBoxes
{
    public class Turn
    {
        public Board TheBoard;
        public Side TheSide;



        public Turn()
        { }
        


        public Turn( Board theBoard, Side theSide )
        {
            TheBoard = theBoard;
            TheSide = theSide;
        }
    }
}
