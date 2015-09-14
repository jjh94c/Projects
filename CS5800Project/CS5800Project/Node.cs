using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace CS5800Project
{
    class Node
    {
        public int m_speed; //speed of node
        public Color m_color; //color of node
        public int m_id; //it's id
        public int m_location; //current location of node
        public Bitmap m_image = null; //the bitmap to draw
        public Thread thread; //the algorithm thread it should run
        public int m_size; //the total number of nodes in the system
        public int replyNum; //the total number of replies this node has received

        //Ricat & Agrawals algorithm Variables
        public long m_timestamp; //timestamp of node when it wants to enter the CS
        public bool[] m_requestDeffered; //if a node sends a request to this node and this node cannot reply, it stores the request in this array and sends the reply once it leaves the CS
       
        //Multiple passing algorithm Variables
        public enum direction { left, right }; //the direction a node can travel across the bridge
        public int leftCS; //number of nodes that want to travel across the bridge from the left
        public int rightCS; //the number of nodes that want to travel across the bridge from the right
        public direction dir;  //the current direction that nodes can travel across the bridge
             
        //CONSTRUCTOR: Initializes the color, the total number of nodes in the system, it's id, and its randomized starting position
        public Node(Color c,int totalNodes,int id, int startingPosition)
        {
            //Initializes variables
            replyNum = 0;
            m_speed = 25;
            m_color = c;
            m_location = startingPosition;
            m_size = totalNodes;
            m_id = id;

            m_timestamp = 0;
            m_requestDeffered = new bool[m_size];
            for (int j = 0; j < m_size; j++)
                m_requestDeffered[j] = false;

            leftCS = 0;
            rightCS = 0;
            dir = direction.left;
            
            //Create the Node Image to display to the screen
            m_image = new Bitmap(40, 40);
            using (Graphics g = Graphics.FromImage(m_image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //Draws the faded border around the dot
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(127, m_color)))
                    g.FillEllipse(sb, new Rectangle(0, 0, m_image.Width, m_image.Height));

                //Draws the actual small dot centered with the faded border
                g.FillEllipse(Brushes.Black, new Rectangle(m_image.Width / 2 - 2, m_image.Height / 2 - 2, 4, 4));
            }
        }

        //Function to create an accurate timestamp (accurate to 100 nanoseconds?)
        public void createTimeStamp()
        {
            m_timestamp = Stopwatch.GetTimestamp();
        } 
    }
}
