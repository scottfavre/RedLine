namespace RedLine.View
{
    partial class ManageCrutchListsDialog
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
            this.lbCrutchLists = new System.Windows.Forms.ListBox();
            this.tbNewTextBox = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbCrutchLists
            // 
            this.lbCrutchLists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCrutchLists.FormattingEnabled = true;
            this.lbCrutchLists.Location = new System.Drawing.Point(12, 12);
            this.lbCrutchLists.Name = "lbCrutchLists";
            this.lbCrutchLists.Size = new System.Drawing.Size(260, 277);
            this.lbCrutchLists.TabIndex = 0;
            this.lbCrutchLists.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbCrutchLists_KeyDown);
            this.lbCrutchLists.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbCrutchLists_DoubleClick);
            // 
            // tbNewTextBox
            // 
            this.tbNewTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbNewTextBox.Location = new System.Drawing.Point(12, 303);
            this.tbNewTextBox.Name = "tbNewTextBox";
            this.tbNewTextBox.Size = new System.Drawing.Size(179, 20);
            this.tbNewTextBox.TabIndex = 1;
            this.tbNewTextBox.TextChanged += new System.EventHandler(this.tbNewTextBox_TextChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelect.Location = new System.Drawing.Point(197, 301);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // ManageCrutchListsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 336);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbNewTextBox);
            this.Controls.Add(this.lbCrutchLists);
            this.Name = "ManageCrutchListsDialog";
            this.Text = "ManageCrutchListsDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbCrutchLists;
        private System.Windows.Forms.TextBox tbNewTextBox;
        private System.Windows.Forms.Button btnSelect;

    }
}