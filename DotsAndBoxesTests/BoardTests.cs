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
            Board00_Top.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board00_Bottom.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board00_Left.ClaimSide( 0, 0, BoxSide.Left, thePlayer );
            Board00_Right.ClaimSide( 0, 0, BoxSide.Right, thePlayer );

            Board11_Top.ClaimSide( 1, 1, BoxSide.Top, thePlayer );
            Board11_Bottom.ClaimSide( 1, 1, BoxSide.Bottom, thePlayer );
            Board11_Left.ClaimSide( 1, 1, BoxSide.Left, thePlayer );
            Board11_Right.ClaimSide( 1, 1, BoxSide.Right, thePlayer );

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
            Board1Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );

            Board2Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board2Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );

            Board3Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board3Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board3Sides.ClaimSide( 0, 0, BoxSide.Left, thePlayer );

            Board4Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Left, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Right, thePlayer );

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
            Board1Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );

            Board2Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board2Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );

            Board3Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board3Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board3Sides.ClaimSide( 0, 0, BoxSide.Left, thePlayer );

            Board4Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Left, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Right, thePlayer );

            // Assert
            Assert.IsFalse( Board0Sides.GameOver(), "Board game over 0 sides not correct" );
            Assert.IsFalse( Board1Sides.GameOver(), "Board game over 1 sides not correct" );
            Assert.IsFalse( Board2Sides.GameOver(), "Board game over 2 sides not correct" );
            Assert.IsFalse( Board3Sides.GameOver(), "Board game over 3 sides not correct" );
            Assert.IsTrue(  Board4Sides.GameOver(), "Board game over 4 sides not correct" );
        }


        [TestMethod]
        public void GetBoxesWithClaimedSides1By1Test()
        {
            //Arrange
            Player thePlayer = Player.Player1;

            Board Board0Sides = new Board( 1, 1 );
            Board Board1Sides = new Board( 1, 1 );
            Board Board2Sides = new Board( 1, 1 );
            Board Board3Sides = new Board( 1, 1 );
            Board Board4Sides = new Board( 1, 1 );

            // Act
            Board1Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );

            Board2Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board2Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );

            Board3Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board3Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board3Sides.ClaimSide( 0, 0, BoxSide.Left, thePlayer );

            Board4Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Left, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Right, thePlayer );

            // Assert
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 0 ).Count == 1, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 1 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 0 claimed sides not correct" );

            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 0 ).Count == 0, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 1 ).Count == 1, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 1 claimed sides not correct" );

            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 0 ).Count == 0, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 1 ).Count == 0, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 2 ).Count == 1, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 2 claimed sides not correct" );

            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 0 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 1 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 3 ).Count == 1, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 3 claimed sides not correct" );

            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 0 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 1 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 4 ).Count == 1, "Boxes with 3 claimed sides not correct" );
        }


        [TestMethod]
        public void GetBoxesWithClaimedSides2By2Test()
        {
            //Arrange
            Player thePlayer = Player.Player1;

            Board Board0Sides = new Board( 2, 2 );
            Board Board1Sides = new Board( 2, 2 );
            Board Board2Sides = new Board( 2, 2 );
            Board Board3Sides = new Board( 2, 2 );
            Board Board4Sides = new Board( 2, 2 );

            // Act
            Board1Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );

            Board2Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board2Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );

            Board3Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board3Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board3Sides.ClaimSide( 0, 0, BoxSide.Left, thePlayer );

            Board4Sides.ClaimSide( 0, 0, BoxSide.Top, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Bottom, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Left, thePlayer );
            Board4Sides.ClaimSide( 0, 0, BoxSide.Right, thePlayer );

            // Assert
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 0 ).Count == 4, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 1 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 0 claimed sides not correct" );

            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 0 ).Count == 3, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 1 ).Count == 1, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 1 claimed sides not correct" );

            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 0 ).Count == 2, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 1 ).Count == 1, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 2 ).Count == 1, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 2 claimed sides not correct" );

            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 0 ).Count == 2, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 1 ).Count == 1, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 3 ).Count == 1, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 3 claimed sides not correct" );

            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 0 ).Count == 1, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 1 ).Count == 2, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 4 ).Count == 1, "Boxes with 3 claimed sides not correct" );
        }


        [TestMethod]
        public void GetBoxesWithClaimedSides3By3Test()
        {
            //Arrange
            Player thePlayer = Player.Player1;

            Board Board0Sides = new Board( 3, 3 );
            Board Board1Sides = new Board( 3, 3 );
            Board Board2Sides = new Board( 3, 3 );
            Board Board3Sides = new Board( 3, 3 );
            Board Board4Sides = new Board( 3, 3 );

            // Act
            Board1Sides.ClaimSide( 1, 1, BoxSide.Top, thePlayer );

            Board2Sides.ClaimSide( 1, 1, BoxSide.Top, thePlayer );
            Board2Sides.ClaimSide( 1, 1, BoxSide.Bottom, thePlayer );

            Board3Sides.ClaimSide( 1, 1, BoxSide.Top, thePlayer );
            Board3Sides.ClaimSide( 1, 1, BoxSide.Bottom, thePlayer );
            Board3Sides.ClaimSide( 1, 1, BoxSide.Left, thePlayer );

            Board4Sides.ClaimSide( 1, 1, BoxSide.Top, thePlayer );
            Board4Sides.ClaimSide( 1, 1, BoxSide.Bottom, thePlayer );
            Board4Sides.ClaimSide( 1, 1, BoxSide.Left, thePlayer );
            Board4Sides.ClaimSide( 1, 1, BoxSide.Right, thePlayer );

            // Assert
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 0 ).Count == 9, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 1 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 0 claimed sides not correct" );
            Assert.IsTrue( Board0Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 0 claimed sides not correct" );

            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 0 ).Count == 7, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 1 ).Count == 2, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 1 claimed sides not correct" );
            Assert.IsTrue( Board1Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 1 claimed sides not correct" );

            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 0 ).Count == 6, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 1 ).Count == 2, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 2 ).Count == 1, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 2 claimed sides not correct" );
            Assert.IsTrue( Board2Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 2 claimed sides not correct" );

            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 0 ).Count == 5, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 1 ).Count == 3, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 3 ).Count == 1, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board3Sides.GetBoxesWithClaimedSides( 4 ).Count == 0, "Boxes with 3 claimed sides not correct" );

            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 0 ).Count == 4, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 1 ).Count == 4, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 2 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 3 ).Count == 0, "Boxes with 3 claimed sides not correct" );
            Assert.IsTrue( Board4Sides.GetBoxesWithClaimedSides( 4 ).Count == 1, "Boxes with 3 claimed sides not correct" );
        }
    }
}
