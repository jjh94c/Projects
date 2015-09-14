using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace CS5800Project
{
    public partial class Form_Bridge : Form
    {
        private List<Node> allNodes; //the list of all nodes that are used in the simulation
        private PointPath board; //the board that contains the list of points that the nodes follow
        private Thread updatethread; //thread that continuously calls the paint function (to update all node movements)
        private SimulationSelector selector; //a windows form for selecting which algorithm to run and how many nodes should be used in the simulation
        private int totalNodes; //the total number of nodes used in the simulation

        public Form_Bridge() //Constructor
        {
            InitializeComponent();
            label_algorithm.Text = ""; //text displaying algorithm thats being used
            allNodes = new List<Node>();
            selector = new SimulationSelector(); 
            board = new PointPath(); //creates set of points
            this.DoubleBuffered = true; //avoids flickering of nodes when updating their positions
            this.Paint += new PaintEventHandler(Form_Bridge_Paint); //calls this function when form needs to be repainted
            updatethread = new Thread(new ThreadStart(UpdateThread)); //creates the thread that will update the paint function
            updatethread.Start(); //starts the update thread
        }
        #region Starting Simulation
        //This function is called when the user clicks on the "Start Simulation" Button. It displays the information needed to start a simulation
        private void button_startSimulation_Click(object sender, EventArgs e)
        {
            if (!selector.IsHandleCreated) //checks to see if a window is already opened
            {
                //Creates the window and shows it to the user
                selector = new SimulationSelector();
                selector.FormClosing += HandleSelectorClosing; //the function that will be called when the window is closed
                selector.Show();
            }
            selector.Focus(); //sets the user's focus to the simulation window 
        }

        //This function is called when the Simulation Selector is closed, either by clicking the 'x', pressing accept, decline or enter in the textfield.
        //This function will create the node objects and start their thread functionality with respect to the algorithm selected.
        private void HandleSelectorClosing(object sender, EventArgs e)
        {
            SimulationSelector selector = (SimulationSelector)sender;
            
            //Check if a positive number of nodes is selected 
            //Note that if cancel or 'x' is selected in the SimulationSelector then the nodeNumber will be 0.
            if (selector.nodeNumber != 0)
            {
                //Deallocates all nodes from the previous simulation
                if (totalNodes != 0)
                {
                    foreach (Node node in allNodes)
                        lock (node)
                            node.thread.Abort();
                    allNodes.Clear();
                    listView_nodes.Items.Clear();
                }
                
                totalNodes = selector.nodeNumber; //gets the total number of nodes that will be in the simulation
                Random newRandomNumber = new Random();
                KnownColor[] colorNames = (KnownColor[])Enum.GetValues(typeof(KnownColor)); //Creates an array of all known colors (by color name like red,blue...etc)
                KnownColor randomColorName; //the NAME of the color that a node will be
                Color randomColor; //the color of the node
                int randomPosition; //the starting position of the node

                //Create all nodes for simulation, giving them a random color and starting position
                for (int i=0; i< totalNodes; ++i)
                {
                    randomColorName = colorNames[newRandomNumber.Next(colorNames.Length)]; //selects a random color name that the node will be
                    randomColor = Color.FromKnownColor(randomColorName); //selects the actual color from its name
                    randomPosition = newRandomNumber.Next(board.pointList.Count); //selects a random position on the path for the node's starting location

                    //Make Sure that the randomPosition is not in the critical section
                    while ((randomPosition >= board.EnterFromLeft-100 && randomPosition <= board.LeaveFromLeft + 10) || (randomPosition >= board.EnterFromRight-100 && randomPosition <= board.LeaveFromRight+10))
                        randomPosition = newRandomNumber.Next(board.pointList.Count); //create a new random position

                    Node newNode = new Node(randomColor, totalNodes, i, randomPosition); //creates a new node to put into the simulation
                    allNodes.Add(newNode); //adds node to node list
                    ListViewItem item = new ListViewItem(newNode.m_speed.ToString()); //adds new listview item with 1st column being its speed
                    item.SubItems.Add(randomColor.Name); //2nd column is it's color
                    listView_nodes.Items.Add(item); //adds new item to list
                    Thread.Sleep(18); //allows enough time for the random number generator to change to a different color
                }

                //Start Each Node's Thread depending on the algorithm selected
                if (selector.algorithm == 0) //if Ricart & Agrawalas algorithm is selected from the algorithm selector window
                {
                    label_algorithm.Text = "Ricart & Agrawalas";
                    for (int i=0; i<totalNodes; ++i)
                    {
                        lock (allNodes[i]) //locks the current resource to this block of code
                        {
                            int passVal = i; //need to create local variable because thread may not receive the same 'i' value when allNodes[i] is unlocked
                            allNodes[i].thread = new Thread(() => ricatAgrawalsThread(passVal)); //assign thread to run, passing it the id of the node
                            allNodes[i].thread.Start(); //start the thread
                        }
                    }
                }
                else //if the multiple nodes in the same direction algorithm is selected
                {
                    label_algorithm.Text = "Multiple in Same Direction";
                    for (int i=0; i<totalNodes; ++i)
                    {
                        lock (allNodes[i])
                        {
                            int passVal = i;
                            allNodes[i].thread = new Thread(() => multipleBridgeThread(passVal)); //assigns thread to run
                            allNodes[i].thread.Start();
                        }
                    }
                }
            }
        }
        #endregion
        #region Ricat and Agrawals Algorithm

        //This is the thread that each node will run to implement Ricat & Agrawals Algorithm
        private void ricatAgrawalsThread(int id)
        {
            //Assign node that is running this thread
            Node node;
            lock (allNodes[id])
                node = allNodes[id];
            int curLocation, newLocation;

            //Thread Loop to update node's position
            while (true) 
            {
                //Get Updated values for current and next locations
                lock (node)
                {
                    Thread.Sleep(500 / node.m_speed); //delay for updating node's location
                    curLocation = node.m_location;
                    newLocation = curLocation + 1 + node.m_speed / 8; //new location to move to -- This is just a random function I created to change the speeds
                }

                //Check if node is trying to gain access to the bridge (left or right side)
                if ((curLocation < board.EnterFromLeft && newLocation >= board.EnterFromLeft) || (curLocation < board.EnterFromRight && newLocation >= board.EnterFromRight))
                {
                    //First get node's timestamp
                    long timestamp;
                    lock (node)
                    {
                        node.m_location = (curLocation < 590) ? 590 : 1640;
                        node.createTimeStamp(); //creates timestamp
                        timestamp = node.m_timestamp; //assigns local variable to node's timestamp
                    }

                    //___________________SEND REQUEST TO ALL OTHER NODES IN THE SIMULATION_________________________//
                    int replies = 0;
                    for (int i = 0; i < totalNodes; ++i)
                    {
                        if (i != id)
                        {
                            lock (allNodes[i])
                            {
                                //Checks if each node is either moving across the bridge or is waiting to get on the bridge and has a shorter timestamp than the current node's timestamp
                                if ((allNodes[i].m_location > board.EnterFromLeft && allNodes[i].m_location < board.LeaveFromLeft) ||
                                    (allNodes[i].m_location > board.EnterFromRight && allNodes[i].m_location < board.LeaveFromRight) ||
                                    ((allNodes[i].m_location == board.EnterFromLeft || allNodes[i].m_location == board.EnterFromRight) && allNodes[i].m_timestamp < timestamp))
                                    allNodes[i].m_requestDeffered[id] = true;
                                else
                                    replies++;
                            }
                        }
                    }

                    //Wait until Node has recieved a reply from every other node in the simulation
                    while (true)
                    {
                        lock (node) //node.replyNum will be updated once the CS node leaves the CS
                            if (node.replyNum == totalNodes - 1 - replies)
                                break;
                        Thread.Sleep(200);
                    }
                    lock (node)
                        node.replyNum = 0;
                    //The node will now move onto the Bridge
                }
                //If a node is leaving the Bridge from either side
                else if ((curLocation < board.LeaveFromLeft && newLocation >= board.LeaveFromLeft) || (curLocation < board.LeaveFromRight && newLocation >= board.LeaveFromRight)) //Leaving CS
                {
                    //__________________LEAVING THE CRITICAL SECTION_____________________//
                    //Send Replies back to all nodes still waiting that send this node a request

                    //Get Request nodes to send replies to
                    lock (node)
                    {
                        for (int i = 0; i < totalNodes; ++i)
                            if (node.m_requestDeffered[i])
                            {
                                allNodes[i].replyNum++; //send a reply to all node's waiting for a reply
                                node.m_requestDeffered[i] = false;
                            }
                    }
                }

                //Actually Update Location of the Node
                if (newLocation >= board.pointList.Count)
                    lock (node)
                        node.m_location = 0;
                else
                    lock (node)
                        node.m_location = newLocation;
            }
        }

        #endregion
        #region multiple nodes moving across bridge at a time Algorithm

        //The thread that each node will run to implement the Multiple Nodes traveling in the same direction along the bridge Algorithm
        private void multipleBridgeThread(int id)
        {
            //Assign node to current threaded node
            Node node;
            lock (allNodes[id])
                node = allNodes[id];
            int curLocation, newLocation;

            //Loop to update node's position
            while (true)
            {
                //assign local variables to node's current location and it's next position 
                lock (node)
                {
                    Thread.Sleep(500 / node.m_speed); //make thread sleep
                    curLocation = node.m_location;
                    newLocation = curLocation + 1 + node.m_speed / 8;
                }

                //Check if node is trying to gain access to the bridge from the left side
                if (curLocation < board.EnterFromLeft && newLocation >= board.EnterFromLeft)
                {
                    //Move Node to wait right before the bridge
                    lock (node)
                        node.m_location = board.EnterFromLeft;

                    //Tell all nodes that a node wants to enter the Bridge from the Left Side
                    for (int i=0; i<totalNodes; ++i)
                        lock (allNodes[i])
                            allNodes[i].leftCS++;

                    //Loop until the direction changes to allow the node to pass
                    while (true)
                    {
                        lock (node)
                        {
                            if (node.dir == Node.direction.right) //if nodes are still moving to the right
                            {
                                if (node.rightCS == 0) //then wait until there are no more nodes moving to the right
                                {
                                    for (int i = 0; i < totalNodes; ++i)
                                            allNodes[i].dir = Node.direction.left; //and change the direction for all nodes so that nodes can move to the left along the bridge
                                    break;
                                }
                            }
                            else 
                                break;
                        }
                        Thread.Sleep(200);
                    }
                }
                //Check if the node is trying to gain access to the bridge from the right side
                else if (curLocation < board.EnterFromRight && newLocation >= board.EnterFromRight)
                {
                    //Move Node to wait right before the entrance of the bridge
                    lock (node)
                        node.m_location = board.EnterFromRight;

                    //Tell all Nodes that this node wants to enter the Bridge from the Right Side
                    for (int i = 0; i < totalNodes; ++i)
                        lock (allNodes[i])
                            allNodes[i].rightCS++;

                    //Loop until the direction changes to "Right" to allow the node to pass
                    while (true)
                    {
                        lock (node)
                        {
                            if (node.dir == Node.direction.left) //if nodes are still moving to the left
                            {
                                if (node.leftCS == 0) //then wait until there are no more nodes moving to the left
                                {
                                    for (int i = 0; i < totalNodes; ++i)
                                            allNodes[i].dir = Node.direction.right; //and change the direction of moving nodes to the right now to allow those nodes to travel along the bridge
                                    break;
                                }
                            }
                            else
                                break;                                  
                        }
                        Thread.Sleep(200);
                    }
                }
                //If a Node is leaving the Bridge from the Left Side
                else if (curLocation < board.LeaveFromLeft && newLocation >= board.LeaveFromLeft) //exit CS from left
                {
                    for (int i=0; i<totalNodes; ++i)
                        lock (allNodes[i])
                            allNodes[i].leftCS--; //let all nodes know that there is now 1 less node moving along the bridge from the left
                }
                //If a Node is leaving the Bridge from the Right Side
                else if (curLocation < board.LeaveFromRight && newLocation >= board.LeaveFromRight) //exit CS from right
                {
                    for (int i = 0; i < totalNodes; ++i)
                        lock (allNodes[i])
                            allNodes[i].rightCS--; //let all nodes know that there is now 1 less node moving along the bridge from the right
                }

                //Actually Update Location of the Node
                if (newLocation >= board.pointList.Count)
                    lock (node)
                        node.m_location = 0;
                else
                    lock (node)
                        node.m_location = newLocation;
            }
        }
        #endregion
        #region Changing The Speed of a Node

        //Changing the Speed of a Node within the ListView
        private void listView_nodes_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            Node node;
            try
            {
                node = allNodes[listView_nodes.SelectedIndices[0]];
            }
            catch { return; }

            try //make sure that the change was an integer value between 1-100
            {
                int val = Convert.ToInt16(e.Label);
                if (val > 0 && val < 101)
                    node.m_speed = val;
                else
                    throw new Exception();
            }
            catch
            {
                e.CancelEdit = true; //cancels the edit and the speed will go back to its default value
                MessageBox.Show("ERROR - The speed must be an integer between the values of 1-100!");
            }
        }
        #endregion
        #region Paint and Update UI
        
        //REPAINTING ALL NODES TO REPRESENT MOVEMENT ALONG THE BRIDGE
        private void Form_Bridge_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawLines(Pens.Red,board.linePoints); //draws lines of the board

            //Draw all Nodes and their color and current position
            foreach (Node node in allNodes)
                e.Graphics.DrawImage(node.m_image, board.pointList[node.m_location].X - (node.m_image.Width / 2), board.pointList[node.m_location].Y - (node.m_image.Height / 2));
        }
        
        //UPDATES THE DISPLAY
        private void UpdateThread()
        {
            while (true)
            {
                Thread.Sleep(20);
                Invalidate(); //calls the paint function of the window form
            }
        }
        
        //Function called when the window is closed.
        //It turns off all running threads before closing the program down
        private void Form_Bridge_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Node node in allNodes)
                node.thread.Abort();
            allNodes.Clear();
            updatethread.Abort();
        }
        #endregion
    }
}