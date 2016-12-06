using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DotsAndBoxes
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Properties
        /// </summary>
        Graphics drawArea;
        Pen PenBlack = new Pen( Color.Black );
        Pen PenPlayer1 = new Pen( Color.Blue );
        Pen PenPlayer2 = new Pen( Color.Red );
        int panelSizeX = 0;  // width of panel
        int panelSizeY = 0;  // length of panel
        int panelStartX = 0; //top left position of panel
        int panelStartY = 0; //top left position of panel these are starting points
        int ColStart = 50;
        int RowStart = 50;
        int ColIncrement = 0;
        int RowIncrement = 0;
        int DotSize = 20;   // The size of the dot to draw
        List<Dot> DotGrid = new List<Dot>();  // List of drawn dots on the game board
        int mouseDownPointX = 0;
        int mouseDownPointY = 0;
        int mouseUpPointX = 0;
        int mouseUpPointY = 0;
        int RowPosition = 0;
        int ColPosition = 0;
        int drawRows = 0;
        int drawCols = 0;
        int humanScore = 0;
        Board TheBoard;
        Solver Player2 = new Solver( Player.Player2, Skill.Intermediate );
        Player CurrentPlayer = Player.Player1;

        List<PointsData> ComputerPlayerList = new List<PointsData>();
        List<PointsData> HumanPlayerList = new List<PointsData>();

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            panelSizeX = panel2.Size.Width;
            panelSizeY = panel2.Size.Height;
            panelStartX = panel2.Location.X;
            panelStartY = panel2.Location.Y;

            drawArea = panel2.CreateGraphics();
        }



        /// <summary>
        /// Draw button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawButton_Click( object sender, EventArgs e )
        {
            panel2.BackColor = Color.White;
            ComputerPlayerList.Clear();
            HumanPlayerList.Clear();
            panel2.Controls.Clear(); //to remove all controls
            label3.Text = Convert.ToString(0);
            label4.Text = Convert.ToString(0);
            panel2.Refresh();
            // Get the rows and columns
            int rows = Int32.Parse(textBox1.Text)+1; 
            int cols = Int32.Parse(textBox1.Text)+1; 
            ColIncrement = (panelSizeX - panelStartX) / rows;
            RowIncrement = (panelSizeY - panelStartY) / cols;
            
            // Draw the dots
            DrawDots( rows, cols );

            // Create a new board
            TheBoard = new Board( rows-1, cols-1 );
            
        }



        /// <summary>
        /// Draws the dots
        /// </summary>
        /// <param name="Rows">Number of rows of dots</param>
        /// <param name="Cols">Number of columns of dots</param>
        private void DrawDots( int Rows, int Cols )
        {
            // Define the starting position of the dot grid
             ColPosition = ColStart;
             RowPosition = RowStart;
            drawRows = Rows;
            drawCols = Cols;
        }



        /// <summary>
        /// Game area click down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_MouseDown( object sender, MouseEventArgs e )
        {
            mouseDownPointX = e.X;
            mouseDownPointY = e.Y;
        }



        /// <summary>
        /// Game area click release
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_MouseUp( object sender, MouseEventArgs e )
        {
            mouseUpPointX = e.X;
            mouseUpPointY = e.Y;
            HumanMove( mouseDownPointX, mouseDownPointY, mouseUpPointX, mouseUpPointY );
        }



        /// <summary>
        /// Draws a line on the game area
        /// </summary>
        /// <param name="downX"></param>
        /// <param name="downY"></param>
        /// <param name="upX"></param>
        /// <param name="upY"></param>
        private void HumanMove( int downX, int downY, int upX, int upY )
        {
            // Get the start and end coordinates
            downX = (int)Math.Floor( (double)downX );
            downY = (int)Math.Floor( (double)downY );
            upX = (int)Math.Floor( (double)upX );
            upY = (int)Math.Floor( (double)upY );

            // Get the start and end coordinates as drawing points
            Point pointStart = new Point( downX, downY );
            Point pointFinish = new Point( upX, upY );


            // Get the side from the endpoints
            Side TheSide = GetSide( downX, downY, upX, upY );

            // Check if the side is valid and available
            if (TheSide.BoxSide != BoxSide.Invalid && TheBoard.IsSideFree( TheSide ))
            {
                // Initialize the new board
                Board NewBoard = new Board( TheBoard );

                // Add the side to the board
                NewBoard.ClaimSide( TheSide, CurrentPlayer );


                // Get the line endpoints
                Point StartPoint;
                Point EndPoint;
                GetPointsFromSide( TheSide, out StartPoint, out EndPoint);
                HumanPlayerList.Add(new PointsData(StartPoint, EndPoint));
                // Draw the line
                drawArea.DrawLine( PenPlayer1, StartPoint, EndPoint );


                // If the player did not complete a box
                if (NewBoard.GetScore( Player.Player1 ) == TheBoard.GetScore( Player.Player1 ))
                {
                    // Use the new board
                    TheBoard = NewBoard;


                    // Check if the game is over
                    if (TheBoard.GameOver())
                    {
                        GameOver();
                    }


                    // Switch the current player
                    CurrentPlayer = Player.Player2;

                    // Let the computer take a turn
                    ComputerMove();
                }

                
                // Otherwise, the player completed a box
                else
                {   
                    // Use the new board
                    TheBoard = NewBoard;
                    // add a blue marker for human player
                    Label myLabel = new Label();
                    myLabel.Location = getPoint(TheSide.BoxSide, StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y, RowIncrement, ColIncrement);
                    myLabel.BackColor = Color.LightBlue; 
                    myLabel.AutoSize = true;
                    myLabel.Refresh();
                    myLabel.Text = "Player1";
                    panel2.Controls.Add(myLabel);
                    label4.Text = TheBoard.GetScore(Player.Player1).ToString();
                }

                // Check if the game is over
                if( TheBoard.GameOver() )
                {
                    panel2.BackColor = Color.Gray;
                    GameOver();
                }
            }
        }



        /// <summary>
        /// Lets the computer take a turn
        /// </summary>
        private void ComputerMove()
        {
            // Initialize the sides list
            List<Side> TheSides = new List<Side>();

            // Initialize the completed box flag
            bool CompletedBox = false;

            // Initialize the new board
            Board NewBoard;


            // Take a turn
            do
            {
                // Clear the completed box flag
                CompletedBox = false;


                // Take a turn on the current board
                Side theSide;
                NewBoard = Player2.TakeTurn( TheBoard, out theSide );


                // Get the line endpoints
                Point StartPoint;
                Point EndPoint;
                GetPointsFromSide( theSide, out StartPoint, out EndPoint );

                //Store the points
                PointsData point = new PointsData(StartPoint, EndPoint);
                ComputerPlayerList.Add( point );

                // Draw the line
                drawArea.DrawLine( PenPlayer2, StartPoint, EndPoint );


                // If the computer completed a box
                if( NewBoard.GetScore( Player.Player2 ) > TheBoard.GetScore( Player.Player2 ) )
                {
                    // Set the flag to true to take another turn
                    CompletedBox = true;

                    // Make the new board the current board
                    TheBoard = NewBoard;
                    
                    // Update the score
                    label3.Text = TheBoard.GetScore( Player.Player2 ).ToString();

                    // add a red marker for computer player
                    Label myLabel = new Label();
                    myLabel.Location = getPoint(theSide.BoxSide, StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y, RowIncrement, ColIncrement);
                    myLabel.BackColor = Color.Red;
                    myLabel.AutoSize = true;
                    myLabel.Refresh();
                    myLabel.Text = "Player2";
                    panel2.Controls.Add( myLabel );
                   
                }


                // Make the new board the current board
                TheBoard = NewBoard;
            }
            // Continue taking a turn as long as long as a box was completed
            while( CompletedBox && !TheBoard.GameOver() );

            
            // Switch the current player
            CurrentPlayer = Player.Player1;


            // Check if the game is over
            if (TheBoard.GameOver())
            {
                panel2.BackColor = Color.Gray;
                GameOver();
            }
        }



        /// <summary>
        /// Returns the start and end points based on a Side
        /// </summary>
        /// <param name="theSide">The Side to get the start and ends points for</param>
        /// <param name="StartPoint">The starting Point of the line</param>
        /// <param name="EndPoint">The ending Point of the line</param>
        private void GetPointsFromSide( Side theSide, out Point StartPoint, out Point EndPoint )
        {
            // Initialize the start and end variables
            int StartX = 0;
            int StartY = 0;
            int EndX = 0;
            int EndY = 0;


            // Get the start and end points of the line to draw
            switch (theSide.BoxSide)
            {
                case BoxSide.Top:
                    StartX = ColStart + DotSize / 2 + theSide.Column * ColIncrement;
                    StartY = RowStart + DotSize / 2 + theSide.Row * RowIncrement;
                    EndX = ColStart + DotSize / 2 + (theSide.Column + 1) * ColIncrement;
                    EndY = RowStart + DotSize / 2 + theSide.Row * RowIncrement;
                    break;


                case BoxSide.Bottom:
                    StartX = ColStart + DotSize / 2 + theSide.Column * ColIncrement;
                    StartY = RowStart + DotSize / 2 + (theSide.Row + 1) * RowIncrement;
                    EndX = ColStart + DotSize / 2 + (theSide.Column + 1) * ColIncrement;
                    EndY = RowStart + DotSize / 2 + (theSide.Row + 1) * RowIncrement;
                    break;


                case BoxSide.Left:
                    StartX = ColStart + DotSize / 2 + theSide.Column * ColIncrement;
                    StartY = RowStart + DotSize / 2 + theSide.Row * RowIncrement;
                    EndX = ColStart + DotSize / 2 + theSide.Column * ColIncrement;
                    EndY = RowStart + DotSize / 2 + (theSide.Row + 1) * RowIncrement;
                    break;


                case BoxSide.Right:
                    StartX = ColStart + DotSize / 2 + (theSide.Column + 1) * ColIncrement;
                    StartY = RowStart + DotSize / 2 + theSide.Row * RowIncrement;
                    EndX = ColStart + DotSize / 2 + (theSide.Column + 1) * ColIncrement;
                    EndY = RowStart + DotSize / 2 + (theSide.Row + 1) * RowIncrement;
                    break;
            }


            // Get the start and end coordinates as drawing points
            StartPoint = new Point( StartX, StartY );
            EndPoint = new Point( EndX, EndY );
        }



        /// <summary>
        /// Gets the Side from the start and end points of the drawn line
        /// </summary>
        /// <param name="StartX">Line start X coordinate</param>
        /// <param name="StartY">Line start Y coordinate</param>
        /// <param name="EndX">Line end X coordinate</param>
        /// <param name="EndY">Line end Y coordinate</param>
        /// <returns>The Side that was drawn</returns>
        private Side GetSide( int StartX, int StartY, int EndX, int EndY )
        {
            // Get the start and end dots
            Dot StartDot = GetDot( StartX, StartY );
            Dot EndDot = GetDot( EndX, EndY );

            // Get the box between the two dots
            int BoxRow = (int) Math.Floor( ( StartDot.Row + EndDot.Row ) / 2.0 );
            int BoxCol = (int) Math.Floor( ( StartDot.Column + EndDot.Column ) / 2.0 );

            // Get the direction of the line
            int RowSign = Math.Abs( StartDot.Row - EndDot.Row );
            int ColSign = Math.Abs( StartDot.Column - EndDot.Column );


            // Initialize the box side
            Side theSide = null;

            // Top (first row only)
            if( RowSign == 0 && ColSign == 1 && BoxRow == 0 )
            {
                theSide = new Side( 0, BoxCol, BoxSide.Top );
            }

            // Left (first column only)
            else if( RowSign == 1 && ColSign == 0 && BoxCol == 0 )
            {
                theSide = new Side( BoxRow, 0, BoxSide.Left );
            }

            // Bottom
            else if( RowSign == 0 && ColSign == 1 )
            {
                theSide = new Side( BoxRow - 1, BoxCol, BoxSide.Bottom );
            }

            // Right
            else if( RowSign == 1 && ColSign == 0 )
            {
                theSide = new Side( BoxRow, BoxCol - 1, BoxSide.Right );
            }

            // Invalid
            else
            {
                theSide = new Side( 0, 0, BoxSide.Invalid );
            }


            // Return the side
            return theSide;
        }



        /// <summary>
        /// Returns the (row, col) point from the X,Y coordinates
        /// </summary>
        /// <param name="X">X coordinate</param>
        /// <param name="Y">Y coordinate</param>
        /// <returns>Point of (row, col) index</returns>
        private Dot GetDot( int X, int Y )
        {
            // Initialize the output point
            Dot theDot = new Dot();


            // Loop through the dots
            foreach (Dot CurrentDot in DotGrid)
            {
                // Calculate the dot bounds
                int MinX = CurrentDot.Coordinates.X;
                int MaxX = CurrentDot.Coordinates.X + 2 * DotSize;
                int MinY = CurrentDot.Coordinates.Y;
                int MaxY = CurrentDot.Coordinates.Y + 2 * DotSize;

                // If the current point X and Y is within the dot size, consider it to be the dot
                if( X > MinX && X < MaxX && Y > MinY && Y < MaxY )
                {
                    theDot = CurrentDot;
                    break;
                }

                // Otherwise, return an empty dot
                else
                {
                    theDot = new Dot();
                }
                
            }


            // Return the point
            return theDot;
        }



        /// <summary>
        /// Called when the game is over
        /// </summary>
        private void GameOver()
        {
            // Get the scores
            int Player1Score = TheBoard.GetScore( Player.Player1 );
            int Player2Score = TheBoard.GetScore( Player.Player2 );

            // Display the result
            MessageBox.Show( "Game Over!  Human: " + Player1Score.ToString() + ", Computer: " + Player2Score.ToString() );
        }

        
        /// <summary>
        /// Drwa the board and all lines made by human and computer, must draw all lines in this
        /// paint method to maintain board graphics when minimizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            // draw board here, so that it will stay when minimized
            for (int RowNum = 0; RowNum < drawRows; RowNum++)
            {
                // Loop through the columns
                for (int ColNum = 0; ColNum < drawCols; ColNum++)
                {
                    // Draw the dot
                    drawArea.DrawRectangle(PenBlack, ColPosition, RowPosition, DotSize, DotSize);

                    // Store it in the dot grid list
                    DotGrid.Add(new Dot(new Point(ColPosition, RowPosition), RowNum, ColNum));

                    // Increment the column position
                    ColPosition = ColPosition + ColIncrement;
                }


                // Increment the row position and reset the column position
                RowPosition = RowPosition + RowIncrement;
                ColPosition = ColStart;

            }
            // drwaing the lines here keeps them from erasing when minimizing windows
            foreach( PointsData t in ComputerPlayerList)
            {
                e.Graphics.DrawLine(PenPlayer2, t.StartPoint, t.EndPoint);
            }

            foreach (PointsData t in HumanPlayerList)
            {
                e.Graphics.DrawLine(PenPlayer1, t.StartPoint, t.EndPoint);
            }

            
        }// end of method
        
        private void label4_Click(object sender, EventArgs e)
        {
            label4.Text = TheBoard.GetScore(Player.Player1).ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = TheBoard.GetScore(Player.Player2).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// this method gets the point where we should put the label inside the newly formed box for each player
        /// </summary>
        /// <param name="side"></param>
        /// <param name="StartPointX"></param>
        /// <param name="StartPointY"></param>
        /// <param name="EndPointX"></param>
        /// <param name="EndPointY"></param>
        /// <param name="RowIncrement"></param>
        /// <param name="ColIncrement"></param>
        /// <returns></returns>
        private Point getPoint(BoxSide side, int StartPointX, int StartPointY, int EndPointX, int EndPointY, int RowIncrement, int ColIncrement)
        {
            Point p; 
            if (side.Equals(BoxSide.Top))
            {
                p = new Point(((StartPointX + EndPointX) / 2), StartPointY + (RowIncrement / 2));
            }
            else if (side.Equals(BoxSide.Bottom))
            {
                p = new Point(((StartPointX + EndPointX) / 2), StartPointY - (RowIncrement / 2));
            }
            else if (side.Equals(BoxSide.Left))
            {
                p = new Point(StartPointX + (ColIncrement / 2), (StartPointY + EndPointY) / 2);
            }
            else             {
                p = new Point(StartPointX - (ColIncrement / 2), (StartPointY + EndPointY) / 2);
            }

            return p;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    } // end of class

    public partial class PointsData
    {
        public Point StartPoint;
        public Point EndPoint;
        public PointsData(Point start, Point end)
        {
            StartPoint = start;
            EndPoint = end;
        }
    }

}

