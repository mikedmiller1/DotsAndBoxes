﻿using System;
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
        List<Point> DrawnPoints = new List<Point>();

        Board TheBoard;
        Solver Player1 = new Solver( Player.Player1, Skill.Beginner );
        Solver Player2 = new Solver( Player.Player2, Skill.Expert );
        Player CurrentPlayer = Player.Player1;



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
            int rows = Int32.Parse(textBox1.Text) + 1; // get rows from user
            int cols = Int32.Parse(textBox2.Text) + 1;
            ColIncrement = (panelSizeX - panelStartX) / rows;
            RowIncrement = (panelSizeY - panelStartY) / cols;

            panel2.Refresh();
            drawIt( rows, cols );


            // Create a new board
            TheBoard = new Board( rows - 1, cols - 1 );
        }



        /// <summary>
        /// Draws the dots
        /// </summary>
        /// <param name="Rows"></param>
        /// <param name="Cols"></param>
        private void drawIt( int Rows, int Cols )
        {
            // Define the starting position of the dot grid
            int ColPosition = ColStart;
            int RowPosition = RowStart;

            // Loop through the rows
            for (int RowNum = 0; RowNum < Rows; RowNum++)
            {
                // Loop through the columns
                for (int ColNum = 0; ColNum < Cols; ColNum++)
                {
                    // Draw the dot
                    drawArea.DrawRectangle( PenBlack, ColPosition, RowPosition, DotSize, DotSize );

                    // Store it in the dot grid list
                    DotGrid.Add( new Dot( new Point( ColPosition, RowPosition ), RowNum, ColNum ) );

                    // Increment the column position
                    ColPosition = ColPosition + ColIncrement;
                }

                // Increment the row position and reset the column position
                RowPosition = RowPosition + RowIncrement;
                ColPosition = ColStart;
            }
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
            drawLine( mouseDownPointX, mouseDownPointY, mouseUpPointX, mouseUpPointY );
        }



        /// <summary>
        /// Draws a line on the game area
        /// </summary>
        /// <param name="downX"></param>
        /// <param name="downY"></param>
        /// <param name="upX"></param>
        /// <param name="upY"></param>
        private void drawLine( int downX, int downY, int upX, int upY )
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
                // Save the points
                DrawnPoints.Add( pointStart );
                DrawnPoints.Add( pointFinish );

                // Add the side to the board
                TheBoard.ClaimSide( TheSide, CurrentPlayer );


                // Draw the line and switch the current player
                if (CurrentPlayer == Player.Player1)
                {
                    drawArea.DrawLine( PenPlayer1, pointStart, pointFinish );
                    CurrentPlayer = Player.Player2;
                }
                else
                {
                    // Take the turn
                    //TheBoard = Player2.TakeTurn( TheBoard );

                    drawArea.DrawLine( PenPlayer2, pointStart, pointFinish );
                    CurrentPlayer = Player.Player1;
                }
            }
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



    }
}
