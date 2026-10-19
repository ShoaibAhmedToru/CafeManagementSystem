using CafeManagementSystem.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    public partial class PaymentForm : Form
    {
        DataAccess da = new DataAccess();

        private int currentCustomerID = 0;
        private bool customerMode = false;

     
        public PaymentForm()
        {
            InitializeComponent();

            customerMode = false;

            SetupForm();
            LoadPendingOrders();
            LoadPaymentHistory();
        }

       
        public PaymentForm(int customerID)
        {
            InitializeComponent();

            customerMode = true;
            currentCustomerID = customerID;

            SetupForm();
            LoadPendingOrders();
            LoadPaymentHistory();
        }

       
        private void SetupForm()
        {
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.Add("Cash");
            cmbPaymentMethod.Items.Add("Card");
            cmbPaymentMethod.Items.Add("Mobile Banking");

            cmbPaymentMethod.SelectedIndex = 0;
            cmbPaymentMethod.DropDownStyle =
                ComboBoxStyle.DropDownList;

            dgvPendingOrders.ReadOnly = true;
            dgvPendingOrders.AllowUserToAddRows = false;
            dgvPendingOrders.AllowUserToDeleteRows = false;
            dgvPendingOrders.MultiSelect = false;
            dgvPendingOrders.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvPendingOrders.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvPayments.ReadOnly = true;
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.AllowUserToDeleteRows = false;
            dgvPayments.MultiSelect = false;
            dgvPayments.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvPayments.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadPendingOrders()
        {
            try
            {
                string sql;

                if (customerMode == true)
                {
                    sql = @"
                        SELECT
                            OrderID,
                            OrderNumber,
                            OrderType,
                            OrderDate,
                            GrandTotal,
                            PaymentStatus
                        FROM Orders
                        WHERE CustomerID = " + currentCustomerID + @"
                        AND PaymentStatus = 'Pending'
                        ORDER BY OrderDate DESC";
                }
                else
                {
                    sql = @"
                        SELECT
                            o.OrderID,
                            o.OrderNumber,
                            u.FullName AS CustomerName,
                            o.OrderType,
                            o.OrderDate,
                            o.GrandTotal,
                            o.PaymentStatus
                        FROM Orders o

                        LEFT JOIN Customers c
                            ON o.CustomerID = c.CustomerID

                        LEFT JOIN UserInfo u
                            ON c.UserID = u.UserID

                        WHERE o.PaymentStatus = 'Pending'

                        ORDER BY o.OrderDate DESC";
                }

                DataTable dt =
                    da.ExecuteQueryTable(sql);

                dgvPendingOrders.DataSource = null;
                dgvPendingOrders.DataSource = dt;

                if (dgvPendingOrders.Columns["OrderID"] != null)
                {
                    dgvPendingOrders.Columns["OrderID"].Visible = false;
                }

                dgvPendingOrders.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load pending orders.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void LoadPaymentHistory()
        {
            try
            {
                string sql;
                string totalSQL;

                if (customerMode == true)
                {
                    sql = @"
                        SELECT
                            o.OrderNumber,
                            p.Amount,
                            p.PaymentMethod,
                            p.PaymentStatus,
                            p.PaymentDate
                        FROM Payments p

                        INNER JOIN Orders o
                            ON p.OrderID = o.OrderID

                        WHERE o.CustomerID = " +
                        currentCustomerID + @"

                        ORDER BY p.PaymentDate DESC";

                    totalSQL = @"
                        SELECT ISNULL(SUM(p.Amount), 0)
                        FROM Payments p

                        INNER JOIN Orders o
                            ON p.OrderID = o.OrderID

                        WHERE o.CustomerID = " +
                        currentCustomerID + @"

                        AND p.PaymentStatus = 'Paid'

                        AND CAST(p.PaymentDate AS DATE)
                            = CAST(GETDATE() AS DATE)";
                }
                else
                {
                    sql = @"
                        SELECT
                            o.OrderNumber,
                            p.Amount,
                            p.PaymentMethod,
                            p.PaymentStatus,
                            p.PaymentDate
                        FROM Payments p

                        INNER JOIN Orders o
                            ON p.OrderID = o.OrderID

                        ORDER BY p.PaymentDate DESC";

                    totalSQL = @"
                        SELECT ISNULL(SUM(Amount), 0)
                        FROM Payments

                        WHERE PaymentStatus = 'Paid'

                        AND CAST(PaymentDate AS DATE)
                            = CAST(GETDATE() AS DATE)";
                }

                DataTable dt =
                    da.ExecuteQueryTable(sql);

                dgvPayments.DataSource = null;
                dgvPayments.DataSource = dt;

                object result =
                    da.ExecuteScalar(totalSQL);

                decimal total =
                    Convert.ToDecimal(result);

                lblGrandTotal.Text =
                    total.ToString("0.00");

                dgvPayments.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load payment history.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

       
     
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (dgvPendingOrders.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a pending order."
                );

                return;
            }

            if (cmbPaymentMethod.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select payment method."
                );

                return;
            }

            try
            {
                int orderID =
                    Convert.ToInt32(
                        dgvPendingOrders
                        .CurrentRow
                        .Cells["OrderID"]
                        .Value
                    );

                string orderNumber =
                    dgvPendingOrders
                    .CurrentRow
                    .Cells["OrderNumber"]
                    .Value
                    .ToString();

                decimal amount =
                    Convert.ToDecimal(
                        dgvPendingOrders
                        .CurrentRow
                        .Cells["GrandTotal"]
                        .Value
                    );

                string paymentMethod =
                    cmbPaymentMethod.Text;

                DialogResult confirm =
                    MessageBox.Show(
                        "Order: " + orderNumber +
                        "\nAmount: " +
                        amount.ToString("0.00") +
                        "\nMethod: " +
                        paymentMethod +
                        "\n\nConfirm payment?",
                        "Confirm Payment",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                if (confirm == DialogResult.No)
                {
                    return;
                }

                SqlTransaction transaction =
                    da.Sqlcon.BeginTransaction();

                try
                {
                    string paymentSQL = @"
                        INSERT INTO Payments
                        (
                            OrderID,
                            PaymentMethod,
                            Amount,
                            PaymentStatus,
                            PaymentDate
                        )
                        VALUES
                        (
                            @OrderID,
                            @PaymentMethod,
                            @Amount,
                            'Paid',
                            GETDATE()
                        )";

                    SqlCommand paymentCmd =
                        new SqlCommand(
                            paymentSQL,
                            da.Sqlcon,
                            transaction
                        );

                    paymentCmd.Parameters.AddWithValue(
                        "@OrderID",
                        orderID
                    );

                    paymentCmd.Parameters.AddWithValue(
                        "@PaymentMethod",
                        paymentMethod
                    );

                    paymentCmd.Parameters.AddWithValue(
                        "@Amount",
                        amount
                    );

                    paymentCmd.ExecuteNonQuery();

                    string orderSQL = @"
                        UPDATE Orders
                        SET PaymentStatus = 'Paid'
                        WHERE OrderID = @OrderID";

                    SqlCommand orderCmd =
                        new SqlCommand(
                            orderSQL,
                            da.Sqlcon,
                            transaction
                        );

                    orderCmd.Parameters.AddWithValue(
                        "@OrderID",
                        orderID
                    );

                    orderCmd.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show(
                        "Payment successful.\n\n" +
                        "Order Number: " +
                        orderNumber +
                        "\nAmount: " +
                        amount.ToString("0.00") +
                        "\nPayment Method: " +
                        paymentMethod
                    );
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                LoadPendingOrders();
                LoadPaymentHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Payment failed.\n\n" +
                    ex.Message,
                    "Payment Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

    
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPendingOrders();
            LoadPaymentHistory();
        }

     
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}