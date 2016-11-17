using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotsAndBoxes
{
    public partial class MainWindow : Form
    {
        Graphics drawArea;
        Pen myPen = new Pen(Color.Black);
        int panelSizeX = 0;  // width of panel
        int panelSizeY = 0;  // length of panel
        int panelStartX = 0; //top left position of panel
        int panelStartY = 0; //top left position of panel these are starting points
        int incrementsX = 0;
        int incrementsY = 0;
        int mouseDownPointX = 0;
        int mouseDownPointY = 0;
        int mouseUpPointX = 0;
        int mouseUpPointY = 0;
        List<Point> points = new List<Point>();



        public MainWindow()
        {
            InitializeComponent();
            panelSizeX = panel2.Size.Width;
            panelSizeY = panel2.Size.Height;
            panelStartX = panel2.Location.X;
            panelStartY = panel2.Location.Y;

            drawArea = panel2.CreateGraphics();
        }



        private void MainWindow_Load( object sender, EventArgs e )
        {
        }



        private void panel2_Paint( object sender, PaintEventArgs e )
        {
        }



        private void DrawButton_Click( object sender, EventArgs e )
        {
            int rows = Int32.Parse(textBox1.Text); // get rows from user
            int cols = Int32.Parse(textBox2.Text);
            incrementsX = (panelSizeX - panelStartX) / rows;
            incrementsY = (panelSizeY - panelStartY) / cols;

            panel2.Refresh();
            drawIt( rows, cols );
        }



        private void drawIt( int rows, int cols )
        {
            int k = 0;
            int l = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    drawArea.DrawRectangle( myPen, k, l, 5, 5 );
                    k = k + incrementsX;

                }
                l = l + incrementsY;
                k = 0;
            }
        }



        private void panel2_MouseDown( object sender, MouseEventArgs e )
        {
            mouseDownPointX = e.X;
            mouseDownPointY = e.Y;
        }



        private void panel2_MouseMove( object sender, MouseEventArgs e )
        {
        }



        private void panel2_MouseUp( object sender, MouseEventArgs e )
        {
            mouseUpPointX = e.X;
            mouseUpPointY = e.Y;
            drawLine( mouseDownPointX, mouseDownPointY, mouseUpPointX, mouseUpPointY );
        }



        private void drawLine( int downX, int downY, int upX, int upY )
        {
            downX = (int)Math.Floor( (double)downX );
            downY = (int)Math.Floor( (double)downY );
            upX = (int)Math.Floor( (double)upX );
            upY = (int)Math.Floor( (double)upY );

            Point pointStart = new Point(downX, downY);
            Point pointFinish = new Point(upX, upY);

            points.Add( pointStart );  //save points
            points.Add( pointFinish ); //save points
            drawArea.DrawLine( myPen, pointStart, pointFinish );
        }

    }
}
