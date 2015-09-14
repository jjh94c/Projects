using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CS5800Project
{
    //The Path that is created for the nodes to travel along
    class PointPath
    {
        public List<PointF> pointList; //list of points that the nodes travel on
        public Point[] linePoints = new Point[9]; //number of points needed to create line segments of the entire board
        public int EnterFromLeft=590,EnterFromRight=1640,LeaveFromLeft=810,LeaveFromRight=1860; //contants of entering and exiting the CS
        
        //CONSTRUCTOR: Creates the entire path that the nodes will travel on
        public PointPath()
        {
            //Creates the Line segments that will be drawn to the window
            pointList = new List<PointF>();
            linePoints[0] = new Point(250, 50);
            linePoints[1] = new Point(250, 400);
            linePoints[2] = new Point(490, 225);
            linePoints[3] = new Point(710, 225);
            linePoints[4] = new Point(950, 400);
            linePoints[5] = new Point(950, 50);
            linePoints[6] = new Point(710, 225);
            linePoints[7] = new Point(490, 225);
            linePoints[8] = new Point(250, 50);
           
            //Creates the actual points that each node will travel on
            //From top left to bottom left
            for (int y = 50; y < 400; y++)
                pointList.Add(new Point(250, y));
            //From bottom left to left middle
            for (double x = 250, y = 400; x < 490; x++, y-=175/240.0)
                pointList.Add(new PointF((float)x, (float)y));
            //From left middle to right middle (the bridge)
            for (int x = 490; x < 710; x++)
                pointList.Add(new Point(x, 225));
            //From right middle to bottom right
            for (double x = 710, y = 225; x < 950; x++, y += 175 / 240.0)
                pointList.Add(new PointF((float)x, (float)y));
            //From bottom right to top right
            for (int y = 400; y > 50; y--)
                pointList.Add(new Point(950, y));
            //From top right to right middle
            for (double x = 950, y = 50; x > 710; x--, y += 175 / 240.0)
                pointList.Add(new PointF((float)x, (float)y));
            //From right middle to left middle (going left on bridge)
            for (int x = 710; x > 490; x--)
                pointList.Add(new Point(x, 225));
            for (double x = 490, y = 225; x > 250; x--, y -= 175 / 240.0)
                pointList.Add(new PointF((float)x, (float)y));
        }
    }
}

