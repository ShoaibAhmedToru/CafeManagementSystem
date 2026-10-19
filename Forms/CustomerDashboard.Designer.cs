namespace CafeManagementSystem.Forms
{
    partial class CustomerDashboard
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
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.btnMyPayments = new System.Windows.Forms.Button();
            this.btnBrowseMenu = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pnlAvailableTables = new System.Windows.Forms.Panel();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlAvailableTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(311, 357);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(86, 49);
            this.btnLogout.TabIndex = 14;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.Location = new System.Drawing.Point(65, 248);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(115, 49);
            this.btnPlaceOrder.TabIndex = 12;
            this.btnPlaceOrder.Text = "Place Order";
            this.btnPlaceOrder.UseVisualStyleBackColor = true;
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click);
            // 
            // btnMyPayments
            // 
            this.btnMyPayments.Location = new System.Drawing.Point(65, 177);
            this.btnMyPayments.Name = "btnMyPayments";
            this.btnMyPayments.Size = new System.Drawing.Size(115, 49);
            this.btnMyPayments.TabIndex = 11;
            this.btnMyPayments.Text = "My Payment";
            this.btnMyPayments.UseVisualStyleBackColor = true;
            this.btnMyPayments.Click += new System.EventHandler(this.btnMyPayments_Click);
            // 
            // btnBrowseMenu
            // 
            this.btnBrowseMenu.Location = new System.Drawing.Point(65, 101);
            this.btnBrowseMenu.Name = "btnBrowseMenu";
            this.btnBrowseMenu.Size = new System.Drawing.Size(115, 49);
            this.btnBrowseMenu.TabIndex = 10;
            this.btnBrowseMenu.Text = "Browse Menu";
            this.btnBrowseMenu.UseVisualStyleBackColor = true;
            this.btnBrowseMenu.Click += new System.EventHandler(this.btnBrowseMenu_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(322, 34);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(75, 20);
            this.lblWelcome.TabIndex = 15;
            this.lblWelcome.Text = "Welcome";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlAvailableTables
            // 
            this.pnlAvailableTables.Controls.Add(this.dgvTables);
            this.pnlAvailableTables.Location = new System.Drawing.Point(427, 126);
            this.pnlAvailableTables.Name = "pnlAvailableTables";
            this.pnlAvailableTables.Size = new System.Drawing.Size(350, 220);
            this.pnlAvailableTables.TabIndex = 16;
            // 
            // dgvTables
            // 
            this.dgvTables.AllowUserToAddRows = false;
            this.dgvTables.AllowUserToDeleteRows = false;
            this.dgvTables.AllowUserToResizeRows = false;
            this.dgvTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTables.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgvTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTables.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.Location = new System.Drawing.Point(14, 41);
            this.dgvTables.MultiSelect = false;
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.ReadOnly = true;
            this.dgvTables.RowHeadersVisible = false;
            this.dgvTables.RowHeadersWidth = 62;
            this.dgvTables.RowTemplate.Height = 28;
            this.dgvTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTables.Size = new System.Drawing.Size(320, 150);
            this.dgvTables.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Available Tables";
            // 
            // CustomerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(839, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlAvailableTables);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnPlaceOrder);
            this.Controls.Add(this.btnMyPayments);
            this.Controls.Add(this.btnBrowseMenu);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CustomerDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerDashboard";
            this.Load += new System.EventHandler(this.CustomerDashboard_Load);
            this.pnlAvailableTables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.Button btnMyPayments;
        private System.Windows.Forms.Button btnBrowseMenu;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel pnlAvailableTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTables;
    }
}