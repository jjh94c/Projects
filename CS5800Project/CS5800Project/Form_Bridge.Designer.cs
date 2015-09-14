namespace CS5800Project
{
    partial class Form_Bridge
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_nodes = new System.Windows.Forms.ListView();
            this.columnNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_startSimulation = new System.Windows.Forms.Button();
            this.label_algorithm = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // listView_nodes
            // 
            this.listView_nodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNode,
            this.columnColor});
            this.listView_nodes.FullRowSelect = true;
            this.listView_nodes.GridLines = true;
            this.listView_nodes.LabelEdit = true;
            this.listView_nodes.Location = new System.Drawing.Point(13, 52);
            this.listView_nodes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView_nodes.Name = "listView_nodes";
            this.listView_nodes.Size = new System.Drawing.Size(240, 460);
            this.listView_nodes.TabIndex = 1;
            this.listView_nodes.UseCompatibleStateImageBehavior = false;
            this.listView_nodes.View = System.Windows.Forms.View.Details;
            this.listView_nodes.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView_nodes_AfterLabelEdit);
            // 
            // columnNode
            // 
            this.columnNode.Text = "Speed";
            this.columnNode.Width = 80;
            // 
            // columnColor
            // 
            this.columnColor.Text = "Color";
            this.columnColor.Width = 90;
            // 
            // button_startSimulation
            // 
            this.button_startSimulation.Location = new System.Drawing.Point(12, 11);
            this.button_startSimulation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_startSimulation.Name = "button_startSimulation";
            this.button_startSimulation.Size = new System.Drawing.Size(241, 37);
            this.button_startSimulation.TabIndex = 2;
            this.button_startSimulation.Text = "Start Simulation";
            this.button_startSimulation.UseVisualStyleBackColor = true;
            this.button_startSimulation.Click += new System.EventHandler(this.button_startSimulation_Click);
            // 
            // label_algorithm
            // 
            this.label_algorithm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_algorithm.Location = new System.Drawing.Point(583, 17);
            this.label_algorithm.Name = "label_algorithm";
            this.label_algorithm.Size = new System.Drawing.Size(421, 31);
            this.label_algorithm.TabIndex = 6;
            this.label_algorithm.Text = "Crossing Algorithm";
            this.label_algorithm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.SkyBlue;
            this.colorDialog1.FullOpen = true;
            // 
            // Form_Bridge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1350, 524);
            this.Controls.Add(this.button_startSimulation);
            this.Controls.Add(this.listView_nodes);
            this.Controls.Add(this.label_algorithm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form_Bridge";
            this.Text = "CS5800 Project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Bridge_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Bridge_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_nodes;
        private System.Windows.Forms.ColumnHeader columnNode;
        private System.Windows.Forms.ColumnHeader columnColor;
        private System.Windows.Forms.Button button_startSimulation;
        private System.Windows.Forms.Label label_algorithm;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}

