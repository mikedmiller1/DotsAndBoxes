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
            Box TopFalse = new Box();
            Box TopTrue = new Box();

            Box BottomFalse = new Box();
            Box BottomTrue = new Box();

            Box LeftFalse = new Box();
            Box LeftTrue = new Box();

            Box RightFalse = new Box();
            Box RightTrue = new Box();

            // Act
            TopTrue.Top = true;
            BottomTrue.Bottom = true;
            LeftTrue.Left = true;
            RightTrue.Right = true;

            // Assert
            Assert.IsFalse( TopFalse.Top, "Box top false not correct" );
            Assert.IsTrue( TopTrue.Top, "Box top true not correct" );

            Assert.IsFalse( BottomFalse.Bottom, "Box bottom false not correct" );
            Assert.IsTrue( BottomTrue.Bottom, "Box bottom true not correct" );

            Assert.IsFalse( LeftFalse.Left, "Box left false not correct" );
            Assert.IsTrue( LeftTrue.Left, "Box left true not correct" );

            Assert.IsFalse( RightFalse.Right, "Box right false not correct" );
            Assert.IsTrue( RightTrue.Right, "Box right true not correct" );
        }



        [TestMethod]
        public void BoxOwnerTest()
        {
            // Arrange
            Box OwnerNone = new Box();
            Box OwnerPlayer1 = new Box();
            Box OwnerPlayer2 = new Box();

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
            Box Sides0 = new Box();
            Box Sides1 = new Box();
            Box Sides2 = new Box();
            Box Sides3 = new Box();
            Box Sides4 = new Box();

            // Act
            Sides1.Top = true;

            Sides2.Top = true;
            Sides2.Bottom = true;

            Sides3.Top = true;
            Sides3.Bottom = true;
            Sides3.Left = true;

            Sides4.Top = true;
            Sides4.Bottom = true;
            Sides4.Left = true;
            Sides4.Right = true;

            // Assert
            Assert.AreEqual( Sides0.NumSides(), 0, "Box num sides 0 not correct" );
            Assert.AreEqual( Sides1.NumSides(), 1, "Box num sides 1 not correct" );
            Assert.AreEqual( Sides2.NumSides(), 2, "Box num sides 2 not correct" );
            Assert.AreEqual( Sides3.NumSides(), 3, "Box num sides 3 not correct" );
            Assert.AreEqual( Sides4.NumSides(), 4, "Box num sides 4 not correct" );
        }


    }
}
