namespace MainForm
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ATSDataGridView = new System.Windows.Forms.DataGridView();
            this.getFacilityButton = new System.Windows.Forms.Button();
            this.getCountriesButton = new System.Windows.Forms.Button();
            this.getRegionButton = new System.Windows.Forms.Button();
            this.fetchInTransitByCountryButton = new System.Windows.Forms.Button();
            this.fetchProductButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.totalToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.getOnhandByLocation = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.fetchCommittedOrdersButton = new System.Windows.Forms.Button();
            this.fetchGetFallOutOrdersButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ATSDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ATSDataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ATSDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ATSDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ATSDataGridView.Location = new System.Drawing.Point(14, 18);
            this.ATSDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ATSDataGridView.Name = "ATSDataGridView";
            this.ATSDataGridView.Size = new System.Drawing.Size(1070, 501);
            this.ATSDataGridView.TabIndex = 0;
            // 
            // getFacilityButton
            // 
            this.getFacilityButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.getFacilityButton.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.getFacilityButton.Location = new System.Drawing.Point(668, 539);
            this.getFacilityButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getFacilityButton.Name = "getFacilityButton";
            this.getFacilityButton.Size = new System.Drawing.Size(110, 35);
            this.getFacilityButton.TabIndex = 1;
            this.getFacilityButton.Text = "Get Facility";
            this.getFacilityButton.UseVisualStyleBackColor = false;
            this.getFacilityButton.Click += new System.EventHandler(this.getFacilityButton_Click);
            // 
            // getCountriesButton
            // 
            this.getCountriesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.getCountriesButton.Location = new System.Drawing.Point(522, 539);
            this.getCountriesButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getCountriesButton.Name = "getCountriesButton";
            this.getCountriesButton.Size = new System.Drawing.Size(110, 35);
            this.getCountriesButton.TabIndex = 5;
            this.getCountriesButton.Text = "Get Countries";
            this.getCountriesButton.UseVisualStyleBackColor = false;
            this.getCountriesButton.Click += new System.EventHandler(this.getCountriesButton_Click);
            // 
            // getRegionButton
            // 
            this.getRegionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.getRegionButton.Location = new System.Drawing.Point(378, 539);
            this.getRegionButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getRegionButton.Name = "getRegionButton";
            this.getRegionButton.Size = new System.Drawing.Size(110, 35);
            this.getRegionButton.TabIndex = 6;
            this.getRegionButton.Text = "Get Region";
            this.getRegionButton.UseVisualStyleBackColor = false;
            this.getRegionButton.Click += new System.EventHandler(this.getRegionButton_Click);
            // 
            // fetchInTransitByCountryButton
            // 
            this.fetchInTransitByCountryButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.fetchInTransitByCountryButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fetchInTransitByCountryButton.Location = new System.Drawing.Point(14, 539);
            this.fetchInTransitByCountryButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fetchInTransitByCountryButton.Name = "fetchInTransitByCountryButton";
            this.fetchInTransitByCountryButton.Size = new System.Drawing.Size(150, 35);
            this.fetchInTransitByCountryButton.TabIndex = 13;
            this.fetchInTransitByCountryButton.Text = "Fetch In-Transit Data";
            this.fetchInTransitByCountryButton.UseVisualStyleBackColor = false;
            this.fetchInTransitByCountryButton.Click += new System.EventHandler(this.fetchInTransitDataButton_Click);
            // 
            // fetchProductButton
            // 
            this.fetchProductButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.fetchProductButton.Location = new System.Drawing.Point(193, 539);
            this.fetchProductButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fetchProductButton.Name = "fetchProductButton";
            this.fetchProductButton.Size = new System.Drawing.Size(150, 35);
            this.fetchProductButton.TabIndex = 14;
            this.fetchProductButton.Text = "Fetch On-Hand Data";
            this.fetchProductButton.UseVisualStyleBackColor = false;
            this.fetchProductButton.Click += new System.EventHandler(this.fetchOnHandDataButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 647);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1099, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // totalToolStripStatusLabel
            // 
            this.totalToolStripStatusLabel.Name = "totalToolStripStatusLabel";
            this.totalToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // getOnhandByLocation
            // 
            this.getOnhandByLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.getOnhandByLocation.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.getOnhandByLocation.Location = new System.Drawing.Point(816, 539);
            this.getOnhandByLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getOnhandByLocation.Name = "getOnhandByLocation";
            this.getOnhandByLocation.Size = new System.Drawing.Size(140, 35);
            this.getOnhandByLocation.TabIndex = 16;
            this.getOnhandByLocation.Text = "On Hand By Location";
            this.getOnhandByLocation.UseVisualStyleBackColor = false;
            this.getOnhandByLocation.Click += new System.EventHandler(this.getOnhandByLocation_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(993, 541);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(91, 30);
            this.testButton.TabIndex = 17;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // fetchCommittedOrdersButton
            // 
            this.fetchCommittedOrdersButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.fetchCommittedOrdersButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fetchCommittedOrdersButton.Location = new System.Drawing.Point(14, 592);
            this.fetchCommittedOrdersButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fetchCommittedOrdersButton.Name = "fetchCommittedOrdersButton";
            this.fetchCommittedOrdersButton.Size = new System.Drawing.Size(165, 35);
            this.fetchCommittedOrdersButton.TabIndex = 18;
            this.fetchCommittedOrdersButton.Text = "Fetch Committed Orders";
            this.fetchCommittedOrdersButton.UseVisualStyleBackColor = false;
            this.fetchCommittedOrdersButton.Click += new System.EventHandler(this.fetchCommittedOrdersButton_Click);
            // 
            // fetchGetFallOutOrdersButton
            // 
            this.fetchGetFallOutOrdersButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.fetchGetFallOutOrdersButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fetchGetFallOutOrdersButton.Location = new System.Drawing.Point(211, 592);
            this.fetchGetFallOutOrdersButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fetchGetFallOutOrdersButton.Name = "fetchGetFallOutOrdersButton";
            this.fetchGetFallOutOrdersButton.Size = new System.Drawing.Size(132, 35);
            this.fetchGetFallOutOrdersButton.TabIndex = 19;
            this.fetchGetFallOutOrdersButton.Text = "Get FallOut Orders";
            this.fetchGetFallOutOrdersButton.UseVisualStyleBackColor = false;
            this.fetchGetFallOutOrdersButton.Click += new System.EventHandler(this.fetchGetFallOutOrdersButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(378, 592);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 35);
            this.button1.TabIndex = 20;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.testButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 669);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fetchGetFallOutOrdersButton);
            this.Controls.Add(this.fetchCommittedOrdersButton);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.getOnhandByLocation);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.fetchProductButton);
            this.Controls.Add(this.fetchInTransitByCountryButton);
            this.Controls.Add(this.getRegionButton);
            this.Controls.Add(this.getCountriesButton);
            this.Controls.Add(this.getFacilityButton);
            this.Controls.Add(this.ATSDataGridView);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATS Data (Ver.20151103)";
            ((System.ComponentModel.ISupportInitialize)(this.ATSDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ATSDataGridView;
        private System.Windows.Forms.Button getFacilityButton;
        private System.Windows.Forms.Button getCountriesButton;
        private System.Windows.Forms.Button getRegionButton;
        private System.Windows.Forms.Button fetchInTransitByCountryButton;
        private System.Windows.Forms.Button fetchProductButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel totalToolStripStatusLabel;
        private System.Windows.Forms.Button getOnhandByLocation;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button fetchCommittedOrdersButton;
        private System.Windows.Forms.Button fetchGetFallOutOrdersButton;
        private System.Windows.Forms.Button button1;
    }
}

