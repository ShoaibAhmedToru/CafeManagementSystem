namespace CafeManagementSystem.Forms
{
    partial class PaymentForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvPendingOrders = new System.Windows.Forms.DataGridView();
            this.lblPendingTitle = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.btnPay = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.lblTotalAmountTitle = new System.Windows.Forms.Label();
            this.lblGrandTotal = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(211, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(443, 46);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "PAYMENT SUMMARY";
            // 
            // dgvPendingOrders
            // 
            this.dgvPendingOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendingOrders.Location = new System.Drawing.Point(93, 118);
            this.dgvPendingOrders.Name = "dgvPendingOrders";
            this.dgvPendingOrders.RowHeadersWidth = 62;
            this.dgvPendingOrders.RowTemplate.Height = 28;
            this.dgvPendingOrders.Size = new System.Drawing.Size(697, 135);
            this.dgvPendingOrders.TabIndex = 9;
            // 
            // lblPendingTitle
            // 
            this.lblPendingTitle.AutoSize = true;
            this.lblPendingTitle.Location = new System.Drawing.Point(89, 83);
            this.lblPendingTitle.Name = "lblPendingTitle";
            this.lblPendingTitle.Size = new System.Drawing.Size(119, 20);
            this.lblPendingTitle.TabIndex = 10;
            this.lblPendingTitle.Text = "Pending Orders";
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(161, 277);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(129, 20);
            this.lblPaymentMethod.TabIndex = 11;
            this.lblPaymentMethod.Text = "Payment Method";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(318, 277);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(121, 28);
            this.cmbPaymentMethod.TabIndex = 12;
            // 
            // btnPay
            // 
            this.btnPay.Location = new System.Drawing.Point(374, 333);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(80, 31);
            this.btnPay.TabIndex = 13;
            this.btnPay.Text = "Pay Now";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(477, 333);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(86, 31);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Location = new System.Drawing.Point(89, 381);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(124, 20);
            this.lblHistoryTitle.TabIndex = 15;
            this.lblHistoryTitle.Text = "Payment History";
            // 
            // dgvPayments
            // 
            this.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayments.Location = new System.Drawing.Point(93, 422);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.RowHeadersWidth = 62;
            this.dgvPayments.RowTemplate.Height = 28;
            this.dgvPayments.Size = new System.Drawing.Size(697, 150);
            this.dgvPayments.TabIndex = 16;
            // 
            // lblTotalAmountTitle
            // 
            this.lblTotalAmountTitle.AutoSize = true;
            this.lblTotalAmountTitle.Location = new System.Drawing.Point(89, 600);
            this.lblTotalAmountTitle.Name = "lblTotalAmountTitle";
            this.lblTotalAmountTitle.Size = new System.Drawing.Size(141, 20);
            this.lblTotalAmountTitle.TabIndex = 17;
            this.lblTotalAmountTitle.Text = "Today\'s Paid Total:";
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.Location = new System.Drawing.Point(252, 600);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(80, 31);
            this.lblGrandTotal.TabIndex = 18;
            this.lblGrandTotal.Text = "0.00";
            this.lblGrandTotal.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(403, 650);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 31);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(855, 765);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.lblTotalAmountTitle);
            this.Controls.Add(this.dgvPayments);
            this.Controls.Add(this.lblHistoryTitle);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.lblPendingTitle);
            this.Controls.Add(this.dgvPendingOrders);
            this.Controls.Add(this.lblTitle);
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Summary";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvPendingOrders;
        private System.Windows.Forms.Label lblPendingTitle;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.DataGridView dgvPayments;
        private System.Windows.Forms.Label lblTotalAmountTitle;
        private System.Windows.Forms.Button lblGrandTotal;
        private System.Windows.Forms.Button btnClose;
    }
}