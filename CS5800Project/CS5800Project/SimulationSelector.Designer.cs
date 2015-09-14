namespace CS5800Project
{
    partial class SimulationSelector
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
            this.label_algorithm = new System.Windows.Forms.Label();
            this.radioButton_single = new System.Windows.Forms.RadioButton();
            this.radioButton_multiple = new System.Windows.Forms.RadioButton();
            this.label_nodeNumber = new System.Windows.Forms.Label();
            this.textBox_nodeNumber = new System.Windows.Forms.TextBox();
            this.button_accept = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_algorithm
            // 
            this.label_algorithm.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.label_algorithm.Location = new System.Drawing.Point(12, 9);
            this.label_algorithm.Name = "label_algorithm";
            this.label_algorithm.Size = new System.Drawing.Size(217, 52);
            this.label_algorithm.TabIndex = 0;
            this.label_algorithm.Text = "Algorithm";
            this.label_algorithm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButton_single
            // 
            this.radioButton_single.AutoSize = true;
            this.radioButton_single.Location = new System.Drawing.Point(12, 64);
            this.radioButton_single.Name = "radioButton_single";
            this.radioButton_single.Size = new System.Drawing.Size(68, 21);
            this.radioButton_single.TabIndex = 1;
            this.radioButton_single.TabStop = true;
            this.radioButton_single.Text = "Single";
            this.radioButton_single.UseVisualStyleBackColor = true;
            // 
            // radioButton_multiple
            // 
            this.radioButton_multiple.AutoSize = true;
            this.radioButton_multiple.Location = new System.Drawing.Point(152, 64);
            this.radioButton_multiple.Name = "radioButton_multiple";
            this.radioButton_multiple.Size = new System.Drawing.Size(77, 21);
            this.radioButton_multiple.TabIndex = 2;
            this.radioButton_multiple.TabStop = true;
            this.radioButton_multiple.Text = "Multiple";
            this.radioButton_multiple.UseVisualStyleBackColor = true;
            // 
            // label_nodeNumber
            // 
            this.label_nodeNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_nodeNumber.Location = new System.Drawing.Point(12, 102);
            this.label_nodeNumber.Name = "label_nodeNumber";
            this.label_nodeNumber.Size = new System.Drawing.Size(217, 125);
            this.label_nodeNumber.TabIndex = 3;
            this.label_nodeNumber.Text = "Number of Nodes";
            this.label_nodeNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_nodeNumber
            // 
            this.textBox_nodeNumber.Location = new System.Drawing.Point(12, 230);
            this.textBox_nodeNumber.Name = "textBox_nodeNumber";
            this.textBox_nodeNumber.Size = new System.Drawing.Size(216, 22);
            this.textBox_nodeNumber.TabIndex = 4;
            this.textBox_nodeNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_nodeNumber_KeyDown);
            // 
            // button_accept
            // 
            this.button_accept.Location = new System.Drawing.Point(12, 276);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(100, 45);
            this.button_accept.TabIndex = 5;
            this.button_accept.Text = "Accept";
            this.button_accept.UseVisualStyleBackColor = true;
            this.button_accept.Click += new System.EventHandler(this.button_accept_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(129, 276);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(100, 45);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // SimulationSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 333);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.textBox_nodeNumber);
            this.Controls.Add(this.label_nodeNumber);
            this.Controls.Add(this.radioButton_multiple);
            this.Controls.Add(this.radioButton_single);
            this.Controls.Add(this.label_algorithm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SimulationSelector";
            this.Text = "Select the Simulation";
            this.Activated += new System.EventHandler(this.SimulationSelector_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_algorithm;
        private System.Windows.Forms.RadioButton radioButton_single;
        private System.Windows.Forms.RadioButton radioButton_multiple;
        private System.Windows.Forms.Label label_nodeNumber;
        private System.Windows.Forms.TextBox textBox_nodeNumber;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.Button button_cancel;
    }
}