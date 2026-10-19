using CafeManagementSystem.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    internal partial class CustomerDashboard : Form
    {
        DataAccess da = new DataAccess();

        public CustomerDashboard()
        {
            InitializeComponent();

           
        }
        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text ="Welcome, " + Session.FullName;
            LoadAvailableTables();
        }
        private void LoadAvailableTables()
        {
            try
            {
                string query = @"
                SELECT
                TableID,
                TableNumber,
                Status
                FROM CafeTables
                WHERE Status = 'Available'";

                DataTable dt = da.ExecuteQueryTable(query);

                dgvTables.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load available tables.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private int GetCustomerID()
        {
            try
            {
                string sql = @"
                SELECT CustomerID
                FROM Customers
                WHERE UserID = " + Session.UserID;

                object result = da.ExecuteScalar(sql);

                if (result == null || result == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Customer information could not be loaded.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return 0;
            }
        }

        private void btnBrowseMenu_Click(object sender, EventArgs e)
        {
            CustomerMenu form = new CustomerMenu();
            form.ShowDialog();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            int customerID = GetCustomerID();

            if (customerID == 0)
            {
                MessageBox.Show(
                    "Customer information not found."
                );

                return;
            }

            OrderForm form = new OrderForm(customerID);

            form.ShowDialog();
            LoadAvailableTables();
        }

        private void btnMyPayments_Click(object sender, EventArgs e)
        {
            int customerID = GetCustomerID();

            if (customerID == 0)
            {
                MessageBox.Show(
                    "Customer information not found."
                );

                return;
            }

            PaymentForm form =
                new PaymentForm(customerID);

            form.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to logout?",
                    "Logout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result == DialogResult.Yes)
            {
                Session.Clear();

                this.Close();
            }
        }

        
    }
}