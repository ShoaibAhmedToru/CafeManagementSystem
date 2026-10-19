using CafeManagementSystem.Data;
using System;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    public partial class ReportForm : Form
    {
        DataAccess da = new DataAccess();

        public ReportForm()
        {
            InitializeComponent();

            SetupForm();
        }

        private void SetupForm()
        {
            dgvReport.ReadOnly = true;
            dgvReport.AllowUserToAddRows = false;
            dgvReport.AllowUserToDeleteRows = false;
            dgvReport.MultiSelect = false;
            dgvReport.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvReport.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

    
        private void btnTodaySales_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"
                    SELECT
                        OrderNumber,
                        OrderDate,
                        Subtotal,
                        DeliveryFee,
                        GrandTotal,
                        PaymentStatus,
                        OrderStatus
                    FROM Orders
                    WHERE CAST(OrderDate AS DATE)
                        = CAST(GETDATE() AS DATE)
                    ORDER BY OrderDate DESC";

                dgvReport.DataSource =
                    da.ExecuteQueryTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load today's sales.\n\n" +
                    ex.Message
                );
            }
        }

      
        private void btnCustomerOrders_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"
                    SELECT
                        o.OrderNumber,
                        c.CustomerID,
                        m.ItemName,
                        od.Quantity,
                        od.UnitPrice,
                        od.TotalPrice,
                        o.PaymentStatus,
                        o.OrderStatus,
                        o.OrderDate
                    FROM Orders o

                    INNER JOIN Customers c
                        ON o.CustomerID = c.CustomerID

                    INNER JOIN OrderDetails od
                        ON o.OrderID = od.OrderID

                    INNER JOIN MenuItems m
                        ON od.MenuItemID = m.MenuItemID

                    WHERE CAST(o.OrderDate AS DATE)
                        = CAST(GETDATE() AS DATE)

                    ORDER BY o.OrderDate DESC";

                dgvReport.DataSource =
                    da.ExecuteQueryTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load customer orders.\n\n" +
                    ex.Message
                );
            }
        }

       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}