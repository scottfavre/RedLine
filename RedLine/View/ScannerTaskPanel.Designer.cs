namespace RedLine.View
{
    partial class ScannerTaskPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clbScanners = new System.Windows.Forms.CheckedListBox();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lbCrutches = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemoveCrutch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbScanners
            // 
            this.clbScanners.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbScanners.CheckOnClick = true;
            this.clbScanners.FormattingEnabled = true;
            this.clbScanners.Location = new System.Drawing.Point(3, 3);
            this.clbScanners.Name = "clbScanners";
            this.clbScanners.Size = new System.Drawing.Size(225, 94);
            this.clbScanners.TabIndex = 0;
            // 
            // btnPrev
            // 
            this.btnPrev.Enabled = false;
            this.btnPrev.Location = new System.Drawing.Point(3, 103);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 1;
            this.btnPrev.Text = "<< Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(84, 103);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next >>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lbCrutches
            // 
            this.lbCrutches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCrutches.FormattingEnabled = true;
            this.lbCrutches.Location = new System.Drawing.Point(3, 160);
            this.lbCrutches.Name = "lbCrutches";
            this.lbCrutches.Size = new System.Drawing.Size(225, 108);
            this.lbCrutches.TabIndex = 3;
            this.lbCrutches.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.lbCrutches_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Crutches:";
            // 
            // btnRemoveCrutch
            // 
            this.btnRemoveCrutch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveCrutch.Location = new System.Drawing.Point(3, 271);
            this.btnRemoveCrutch.Name = "btnRemoveCrutch";
            this.btnRemoveCrutch.Size = new System.Drawing.Size(106, 23);
            this.btnRemoveCrutch.TabIndex = 5;
            this.btnRemoveCrutch.Text = "Remove Crutch";
            this.btnRemoveCrutch.UseVisualStyleBackColor = true;
            this.btnRemoveCrutch.Click += new System.EventHandler(this.btnRemoveCrutch_Click);
            // 
            // ScannerTaskPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemoveCrutch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCrutches);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.clbScanners);
            this.Name = "ScannerTaskPanel";
            this.Size = new System.Drawing.Size(231, 297);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbScanners;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ListBox lbCrutches;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemoveCrutch;
    }
}
