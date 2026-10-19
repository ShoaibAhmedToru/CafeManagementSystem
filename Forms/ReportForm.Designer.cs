namespace CafeManagementSystem.Forms
{
    partial class ReportForm
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
            this.btnTodaySales = new System.Windows.Forms.Button();
            this.btnCustomerOrders = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTodaySales
            // 
            this.btnTodaySales.Location = new System.Drawing.Point(173, 47);
            this.btnTodaySales.Name = "btnTodaySales";
            this.btnTodaySales.Size = new System.Drawing.Size(152, 55);
            this.btnTodaySales.TabIndex = 0;
            this.btnTodaySales.Text = "Today Sales";
            this.btnTodaySales.UseVisualStyleBackColor = true;
            this.btnTodaySales.Click += new System.EventHandler(this.btnTodaySales_Click);
            // 
            // btnCustomerOrders
            // 
            this.btnCustomerOrders.Location = new System.Drawing.Point(423, 47);
            this.btnCustomerOrders.Name = "btnCustomerOrders";
            this.btnCustomerOrders.Size = new System.Drawing.Size(203, 55);
            this.btnCustomerOrders.TabIndex = 3;
            this.btnCustomerOrders.Text = "Today Customer Orders";
            this.btnCustomerOrders.UseVisualStyleBackColor = true;
            this.btnCustomerOrders.Click += new System.EventHandler(this.btnCustomerOrders_Click);
            // 
            // dgvReport
            // 
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(-5, 142);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.RowHeadersWidth = 62;
            this.dgvReport.RowTemplate.Height = 28;
            this.dgvReport.Size = new System.Drawing.Size(784, 228);
            this.dgvReport.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(343, 383);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 43);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.btnCustomerOrders);
            this.Controls.Add(this.btnTodaySales);
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTodaySales;
        private System.Windows.Forms.Button btnCustomerOrders;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Button btnClose;
    }
}