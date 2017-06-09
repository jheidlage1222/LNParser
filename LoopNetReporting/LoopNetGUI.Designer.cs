namespace LoopNetReporting
{
    partial class LoopNetGUI
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
            this.btn_Submit = new System.Windows.Forms.Button();
            this.tBox_Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tBox_Password = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.tBox_Output = new System.Windows.Forms.TextBox();
            this.dgvSubMktData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubMktData)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Submit
            // 
            this.btn_Submit.Location = new System.Drawing.Point(12, 362);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(75, 23);
            this.btn_Submit.TabIndex = 1;
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // tBox_Username
            // 
            this.tBox_Username.Location = new System.Drawing.Point(12, 61);
            this.tBox_Username.Name = "tBox_Username";
            this.tBox_Username.Size = new System.Drawing.Size(132, 20);
            this.tBox_Username.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // tBox_Password
            // 
            this.tBox_Password.Location = new System.Drawing.Point(160, 61);
            this.tBox_Password.Name = "tBox_Password";
            this.tBox_Password.Size = new System.Drawing.Size(132, 20);
            this.tBox_Password.TabIndex = 4;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(93, 362);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // tBox_Output
            // 
            this.tBox_Output.Location = new System.Drawing.Point(12, 87);
            this.tBox_Output.Multiline = true;
            this.tBox_Output.Name = "tBox_Output";
            this.tBox_Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBox_Output.Size = new System.Drawing.Size(463, 69);
            this.tBox_Output.TabIndex = 0;
            // 
            // dgvSubMktData
            // 
            this.dgvSubMktData.AllowUserToOrderColumns = true;
            this.dgvSubMktData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubMktData.Location = new System.Drawing.Point(12, 181);
            this.dgvSubMktData.Name = "dgvSubMktData";
            this.dgvSubMktData.Size = new System.Drawing.Size(463, 150);
            this.dgvSubMktData.TabIndex = 7;
            // 
            // LoopNetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 397);
            this.Controls.Add(this.dgvSubMktData);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tBox_Password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBox_Username);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.tBox_Output);
            this.Name = "LoopNetGUI";
            this.Text = "LoopNet Report Generator";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubMktData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.TextBox tBox_Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBox_Password;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox tBox_Output;
        private System.Windows.Forms.DataGridView dgvSubMktData;
    }
}

