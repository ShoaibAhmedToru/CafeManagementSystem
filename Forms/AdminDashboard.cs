using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            lblWelcome.Text = "Welcome, " + Session.FullName;

        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            CategoryForm form = new CategoryForm();
            form.ShowDialog();

        }

       

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuForm form = new MenuForm();
            form.ShowDialog();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            CustomerForm form = new CustomerForm();
            form.ShowDialog();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            TableForm form = new TableForm();
            form.ShowDialog();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrderForm form = new OrderForm();
            form.ShowDialog();
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            PaymentForm form = new PaymentForm();
            form.ShowDialog();
        }

        

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportForm form = new ReportForm();
            form.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            this.Close();
        }
    }
}
