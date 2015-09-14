using System;
using System.Windows.Forms;

namespace CS5800Project
{
    public partial class SimulationSelector : Form
    {
        public int nodeNumber = 0; //Number of nodes that the algorithm will use
        public byte algorithm = 0; //determines which algorithm will be selected

        //CONSTRUCTOR
        public SimulationSelector()
        {
            InitializeComponent();
        }

        //Function called when the accept button is clicked. It gets the number of nodes and the algorithm selected and then closes the form and sends the data to the Form_Bridge
        private void button_accept_Click(object sender, EventArgs e)
        {
            try
            {
                nodeNumber = Convert.ToUInt16(textBox_nodeNumber.Text); //gets number of nodes
                if (radioButton_multiple.Checked)
                    algorithm = 1; //gets algorithm
                this.Close(); //closes form
            }
            catch (Exception ex)
            {
                //Exception when an invalid argument is provided for the #nodes textfield
                MessageBox.Show("ERROR! - The textbox must be a positive integer!\r\n\r\n" + ex.Message);
            }   
        }

        //Called when the cancel button is selected. It just closes the window
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Called when the enter key is pressed within the #nodes textbox. Just runs button_accept_click and closes the window
        private void textBox_nodeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                button_accept_Click(sender, e);
        }

        //Automatically makes the focus set to the textbox when the window is selected
        private void SimulationSelector_Activated(object sender, EventArgs e)
        {
            textBox_nodeNumber.Focus();
        }
    }
}
