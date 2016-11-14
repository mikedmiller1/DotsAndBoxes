using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotsAndBoxes;

namespace DotsAndBoxesTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void BoardSideTest()
        {
            // Arrange
            Player thePlayer = Player.Player1;

            Board Board00_Top      = new Board( 2, 2 );
            Board Board00_Bottom   = new Board( 2, 2 );
            Board Board00_Left     = new Board( 2, 2 );
            Board Board00_Right    = new Board( 2, 2 );

            Board Board11_Top      = new Board( 2, 2 );
            Board Board11_Bottom   = new Board( 2, 2 );
            Board Board11_Left     = new Board( 2, 2 );
            Board Board11_Right    = new Board( 2, 2 );


            // Act
            Board00_Top.AddSide( 0, 0, BoxSide.Top, thePlayer );
            Board00_Bottom.AddSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board00_Left.AddSide( 0, 0, BoxSide.Left, thePlayer );
            Board00_Right.AddSide( 0, 0, BoxSide.Right, thePlayer );

            Board11_Top.AddSide( 1, 1, BoxSide.Top, thePlayer );
            Board11_Bottom.AddSide( 1, 1, BoxSide.Bottom, thePlayer );
            Board11_Left.AddSide( 1, 1, BoxSide.Left, thePlayer );
            Board11_Right.AddSide( 1, 1, BoxSide.Right, thePlayer );

            Box Board00_Top_Box00 = Board00_Top.GetBox( 0, 0 );

            Box Board11_Top_Box00 = Board11_Top.GetBox( 0, 0 );
            Box Board11_Top_Box10 = Board11_Top.GetBox( 1, 0 );
            Box Board11_Top_Box01 = Board11_Top.GetBox( 0, 1 );
            Box Board11_Top_Box11 = Board11_Top.GetBox( 1, 1 );

            // Assert

        }



        [TestMethod]
        public void BoardBoxOwnerTest()
        {
            //Arrange
            Player thePlayer = Player.Player1;

            Board Board0Sides = new Board( 1, 1 );
            Board Board1Sides = new Board( 1, 1 );
            Board Board2Sides = new Board( 1, 1 );
            Board Board3Sides = new Board( 1, 1 );
            Board Board4Sides = new Board( 1, 1 );

            // Act
            Board1Sides.AddSide( 0, 0, BoxSide.Top, thePlayer );

            Board2Sides.AddSide( 0, 0, BoxSide.Top, thePlayer );
            Board2Sides.AddSide( 0, 0, BoxSide.Bottom, thePlayer );

            Board3Sides.AddSide( 0, 0, BoxSide.Top, thePlayer );
            Board3Sides.AddSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board3Sides.AddSide( 0, 0, BoxSide.Left, thePlayer );

            Board4Sides.AddSide( 0, 0, BoxSide.Top, thePlayer );
            Board4Sides.AddSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board4Sides.AddSide( 0, 0, BoxSide.Left, thePlayer );
            Board4Sides.AddSide( 0, 0, BoxSide.Right, thePlayer );

            // Assert

        }



        [TestMethod]
        public void BoardGameOverTest()
        {
            //Arrange
            Player thePlayer = Player.Player1;

            Board Board0Sides = new Board( 1, 1 );
            Board Board1Sides = new Board( 1, 1 );
            Board Board2Sides = new Board( 1, 1 );
            Board Board3Sides = new Board( 1, 1 );
            Board Board4Sides = new Board( 1, 1 );

            // Act
            Board1Sides.AddSide( 0, 0, BoxSide.Top, thePlayer );

            Board2Sides.AddSide( 0, 0, BoxSide.Top, thePlayer );
            Board2Sides.AddSide( 0, 0, BoxSide.Bottom, thePlayer );

            Board3Sides.AddSide( 0, 0, BoxSide.Top, thePlayer );
            Board3Sides.AddSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board3Sides.AddSide( 0, 0, BoxSide.Left, thePlayer );

            Board4Sides.AddSide( 0, 0, BoxSide.Top, thePlayer );
            Board4Sides.AddSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board4Sides.AddSide( 0, 0, BoxSide.Left, thePlayer );
            Board4Sides.AddSide( 0, 0, BoxSide.Right, thePlayer );

            // Assert
            Assert.IsFalse( Board0Sides.GameOver(), "Board game over 0 sides not correct" );
            Assert.IsFalse( Board1Sides.GameOver(), "Board game over 1 sides not correct" );
            Assert.IsFalse( Board2Sides.GameOver(), "Board game over 2 sides not correct" );
            Assert.IsFalse( Board3Sides.GameOver(), "Board game over 3 sides not correct" );
            Assert.IsTrue(  Board4Sides.GameOver(), "Board game over 4 sides not correct" );
        }




    }
}
