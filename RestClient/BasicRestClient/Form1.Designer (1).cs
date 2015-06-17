namespace BasicRestClient
{
    partial class BasicRestClient
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnAcct = new System.Windows.Forms.Button();
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.lblFName = new System.Windows.Forms.Label();
            this.txtBxFName = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.signInToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1027, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // signInToolStripMenuItem
            // 
            this.signInToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.signInToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.signInToolStripMenuItem.Name = "signInToolStripMenuItem";
            this.signInToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.signInToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.signInToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.signInToolStripMenuItem.Text = "Sign in";
            this.signInToolStripMenuItem.Click += new System.EventHandler(this.signInToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.testToolStripMenuItem.Text = "test";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtBxFName);
            this.splitContainer1.Panel2.Controls.Add(this.lblFName);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewProducts);
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1027, 607);
            this.splitContainer1.SplitterDistance = 175;
            this.splitContainer1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btnProducts);
            this.flowLayoutPanel1.Controls.Add(this.btnOrders);
            this.flowLayoutPanel1.Controls.Add(this.btnAcct);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(171, 607);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btnProducts
            // 
            this.btnProducts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProducts.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnProducts.Enabled = false;
            this.btnProducts.Location = new System.Drawing.Point(3, 3);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(165, 42);
            this.btnProducts.TabIndex = 0;
            this.btnProducts.Text = "View Products";
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOrders.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOrders.Enabled = false;
            this.btnOrders.Location = new System.Drawing.Point(3, 51);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(165, 42);
            this.btnOrders.TabIndex = 1;
            this.btnOrders.Text = "Orders";
            this.btnOrders.UseVisualStyleBackColor = true;
            // 
            // btnAcct
            // 
            this.btnAcct.Enabled = false;
            this.btnAcct.Location = new System.Drawing.Point(3, 99);
            this.btnAcct.Name = "btnAcct";
            this.btnAcct.Size = new System.Drawing.Size(165, 42);
            this.btnAcct.TabIndex = 2;
            this.btnAcct.Text = "Account";
            this.btnAcct.UseVisualStyleBackColor = true;
            this.btnAcct.Click += new System.EventHandler(this.btnAcct_Click);
            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewProducts.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(848, 607);
            this.dataGridViewProducts.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(40, 21);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(796, 563);
            this.listBox1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Location = new System.Drawing.Point(102, 127);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(57, 13);
            this.lblFName.TabIndex = 2;
            this.lblFName.Text = "First Name";
            // 
            // txtBxFName
            // 
            this.txtBxFName.Location = new System.Drawing.Point(169, 120);
            this.txtBxFName.Name = "txtBxFName";
            this.txtBxFName.Size = new System.Drawing.Size(205, 20);
            this.txtBxFName.TabIndex = 3;
            // 
            // BasicRestClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1027, 631);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BasicRestClient";
            this.Text = "BasicRestClient";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnAcct;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityInStockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productVendorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.TextBox txtBxFName;
        private System.Windows.Forms.Label lblFName;

      
    }
}

