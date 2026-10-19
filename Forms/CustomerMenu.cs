using CafeManagementSystem.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    public partial class CustomerMenu : Form
    {
        DataAccess da = new DataAccess();

        public CustomerMenu()
        {
            InitializeComponent();

            LoadCategories();
            LoadMenu();
        }

        private void LoadCategories()
        {
            string sql = @"
                SELECT CategoryID, CategoryName
                FROM Categories
                WHERE IsActive = 1
                ORDER BY CategoryName";

            cmbCategory.DataSource =
                da.ExecuteQueryTable(sql);

            cmbCategory.DisplayMember =
                "CategoryName";

            cmbCategory.ValueMember =
                "CategoryID";

            cmbCategory.SelectedIndex = -1;
        }

        private void LoadMenu()
        {
            string sql = @"
                SELECT
                    m.MenuItemID,
                    m.ItemName,
                    c.CategoryName,
                    m.Description,
                    m.Price
                FROM MenuItems m
                INNER JOIN Categories c
                    ON m.CategoryID = c.CategoryID
                WHERE m.IsAvailable = 1
                ORDER BY m.ItemName";

            dgvMenu.DataSource =
                da.ExecuteQueryTable(sql);
        }

        private void cmbCategory_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbCategory.SelectedIndex == -1)
            {
                return;
            }

            if (cmbCategory.SelectedValue == null)
            {
                return;
            }

            int categoryID;

            if (!int.TryParse(
                cmbCategory.SelectedValue.ToString(),
                out categoryID))
            {
                return;
            }

            string sql = @"
                SELECT
                    m.MenuItemID,
                    m.ItemName,
                    c.CategoryName,
                    m.Description,
                    m.Price
                FROM MenuItems m
                INNER JOIN Categories c
                    ON m.CategoryID = c.CategoryID
                WHERE m.IsAvailable = 1
                AND m.CategoryID = " + categoryID + @"
                ORDER BY m.ItemName";

            dgvMenu.DataSource =
                da.ExecuteQueryTable(sql);
        }

        private void btnRefresh_Click(
            object sender,
            EventArgs e)
        {
            txtSearch.Clear();
            cmbCategory.SelectedIndex = 0;

            LoadMenu();
        }

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText =
                    txtSearch.Text.Trim();

                string sql = @"
                 SELECT
                m.MenuItemID,
                c.CategoryName,
                m.ItemName,
                m.Description,
                m.Price
                FROM MenuItems m
                INNER JOIN Categories c
                ON m.CategoryID = c.CategoryID
                WHERE m.IsAvailable = 1
                AND m.ItemName LIKE '%" +
                    searchText + @"%'";

                DataTable dt =
                    da.ExecuteQueryTable(sql);

                dgvMenu.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Search Error:\n" + ex.Message
                );
            }
        }
    }
}