using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotsAndBoxes;

namespace DotsAndBoxesTests
{
    [TestClass]
    public class BoxTests
    {
        [TestMethod]
        public void BoxSideTest()
        {
            // Arrange
            Box TopFalse = new Box(0, 0);
            Box TopTrue = new Box(0, 0);

            Box BottomFalse = new Box(0, 0);
            Box BottomTrue = new Box(0, 0);

            Box LeftFalse = new Box(0, 0);
            Box LeftTrue = new Box(0, 0);

            Box RightFalse = new Box(0, 0);
            Box RightTrue = new Box(0, 0);

            // Act
            TopTrue.ClaimSide( BoxSide.Top, Player.Player1 );
            BottomTrue.ClaimSide( BoxSide.Bottom, Player.Player1 );
            LeftTrue.ClaimSide( BoxSide.Left, Player.Player1 );
            RightTrue.ClaimSide( BoxSide.Right, Player.Player1 );

            // Assert
            Assert.IsFalse( TopFalse.Top.Owner == Player.Player1, "Box top false not correct" );
            Assert.IsTrue( TopTrue.Top.Owner == Player.Player1, "Box top true not correct" );

            Assert.IsFalse( BottomFalse.Bottom.Owner == Player.Player1, "Box bottom false not correct" );
            Assert.IsTrue( BottomTrue.Bottom.Owner == Player.Player1, "Box bottom true not correct" );

            Assert.IsFalse( LeftFalse.Left.Owner == Player.Player1, "Box left false not correct" );
            Assert.IsTrue( LeftTrue.Left.Owner == Player.Player1, "Box left true not correct" );

            Assert.IsFalse( RightFalse.Right.Owner == Player.Player1, "Box right false not correct" );
            Assert.IsTrue( RightTrue.Right.Owner == Player.Player1, "Box right true not correct" );
        }



        [TestMethod]
        public void BoxOwnerTest()
        {
            // Arrange
            Box OwnerNone = new Box(0, 0);
            Box OwnerPlayer1 = new Box(0, 0);
            Box OwnerPlayer2 = new Box(0, 0);

            // Act
            OwnerPlayer1.Owner = Player.Player1;
            OwnerPlayer2.Owner = Player.Player2;

            // Assert
            Assert.AreEqual( OwnerNone.Owner, Player.None, "Box owner none not correct" );
            Assert.AreEqual( OwnerPlayer1.Owner, Player.Player1, "Box owner player 1 not correct" );
            Assert.AreEqual( OwnerPlayer2.Owner, Player.Player2, "Box owner player 2 not correct" );
        }



        [TestMethod]
        public void BoxNumSidesTest()
        {
            // Arrange
            Box Sides0 = new Box(0, 0);
            Box Sides1 = new Box(0, 0);
            Box Sides2 = new Box(0, 0);
            Box Sides3 = new Box(0, 0);
            Box Sides4 = new Box(0, 0);

            // Act
            Sides1.ClaimSide( BoxSide.Top, Player.Player1 );

            Sides2.ClaimSide( BoxSide.Top, Player.Player1 );
            Sides2.ClaimSide( BoxSide.Bottom, Player.Player1 );

            Sides3.ClaimSide( BoxSide.Top, Player.Player1 );
            Sides3.ClaimSide( BoxSide.Bottom, Player.Player1 );
            Sides3.ClaimSide( BoxSide.Left, Player.Player1 );

            Sides4.ClaimSide( BoxSide.Top, Player.Player1 );
            Sides4.ClaimSide( BoxSide.Bottom, Player.Player1 );
            Sides4.ClaimSide( BoxSide.Left, Player.Player1 );
            Sides4.ClaimSide( BoxSide.Right, Player.Player1 );

            // Assert
            Assert.AreEqual( Sides0.NumSides(), 0, "Box num sides 0 not correct" );
            Assert.AreEqual( Sides1.NumSides(), 1, "Box num sides 1 not correct" );
            Assert.AreEqual( Sides2.NumSides(), 2, "Box num sides 2 not correct" );
            Assert.AreEqual( Sides3.NumSides(), 3, "Box num sides 3 not correct" );
            Assert.AreEqual( Sides4.NumSides(), 4, "Box num sides 4 not correct" );
        }


    }
}
