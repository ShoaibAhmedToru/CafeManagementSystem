using CafeManagementSystem.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    public partial class MenuForm : Form
    {
        DataAccess da = new DataAccess();

        public MenuForm()
        {
            InitializeComponent();

            
            txtMenuItemID.ReadOnly = true;

        
            dgvMenu.ReadOnly = true;
            dgvMenu.AllowUserToAddRows = false;
            dgvMenu.AllowUserToDeleteRows = false;
            dgvMenu.MultiSelect = false;

            dgvMenu.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvMenu.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            LoadCategories();
            LoadMenu();
        }


      

        private void LoadCategories()
        {
            try
            {
                string sql = @"
                    SELECT
                        CategoryID,
                        CategoryName
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
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load categories.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void LoadMenu()
        {
            try
            {
                string sql = @"
                    SELECT
                        m.MenuItemID,
                        m.ItemName,
                        c.CategoryName,
                        m.Description,
                        m.Price,
                        m.IsAvailable
                    FROM MenuItems m

                    INNER JOIN Categories c
                        ON m.CategoryID =
                           c.CategoryID

                    ORDER BY m.MenuItemID DESC";

                dgvMenu.DataSource =
                    da.ExecuteQueryTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load menu items.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


    

        private void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // Category validation
                if (cmbCategory.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Please select a category.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    cmbCategory.Focus();
                    return;
                }


                // Item name validation
                if (txtItemName.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter item name.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtItemName.Focus();
                    return;
                }


                // Price validation
                decimal price;

                if (!decimal.TryParse(
                    txtPrice.Text.Trim(),
                    out price))
                {
                    MessageBox.Show(
                        "Please enter a valid price.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtPrice.Focus();
                    return;
                }


                if (price <= 0)
                {
                    MessageBox.Show(
                        "Price must be greater than 0.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtPrice.Focus();
                    return;
                }


                string sql =
                    "INSERT INTO MenuItems " +
                    "(CategoryID, ItemName, Description, " +
                    "Price, IsAvailable, CreatedAt) " +

                    "VALUES (" +
                    cmbCategory.SelectedValue + ",'" +
                    txtItemName.Text.Trim() + "','" +
                    txtDescription.Text.Trim() + "'," +
                    price + ",1,GETDATE())";


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Menu item added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadMenu();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Menu item could not be added.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


    

        private void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (txtMenuItemID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please select a menu item first.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                if (cmbCategory.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Please select a category.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                if (txtItemName.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter item name.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                decimal price;

                if (!decimal.TryParse(
                    txtPrice.Text.Trim(),
                    out price))
                {
                    MessageBox.Show(
                        "Please enter a valid price.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                if (price <= 0)
                {
                    MessageBox.Show(
                        "Price must be greater than 0.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                string sql =
                    "UPDATE MenuItems SET " +

                    "CategoryID=" +
                    cmbCategory.SelectedValue +

                    ", ItemName='" +
                    txtItemName.Text.Trim() +

                    "', Description='" +
                    txtDescription.Text.Trim() +

                    "', Price=" +
                    price +

                    ", IsAvailable=1 " +

                    "WHERE MenuItemID=" +
                    txtMenuItemID.Text;


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Menu item updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadMenu();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Menu item could not be updated.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (txtMenuItemID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please select a menu item first.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                DialogResult result =
                    MessageBox.Show(
                        "Are you sure you want to delete this menu item?",
                        "Delete Menu Item",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );


                if (result == DialogResult.No)
                {
                    return;
                }


                string sql =
                    "DELETE FROM MenuItems " +
                    "WHERE MenuItemID=" +
                    txtMenuItemID.Text;


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Menu item deleted successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadMenu();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Menu item could not be deleted.\n\n" +
                    "This item may already be used in an order.\n\n" +
                    ex.Message,
                    "Delete Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


 

        private void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string search =
                    txtSearch.Text.Trim();


                if (search == "")
                {
                    LoadMenu();
                    return;
                }


                string sql = @"
                    SELECT
                        m.MenuItemID,
                        m.ItemName,
                        c.CategoryName,
                        m.Description,
                        m.Price,
                        m.IsAvailable

                    FROM MenuItems m

                    INNER JOIN Categories c
                        ON m.CategoryID =
                           c.CategoryID

                    WHERE
                        m.ItemName LIKE '%" +
                        search + @"%'

                    OR
                        c.CategoryName LIKE '%" +
                        search + @"%'

                    OR
                        m.Description LIKE '%" +
                        search + @"%'

                    ORDER BY m.MenuItemID DESC";


                dgvMenu.DataSource =
                    da.ExecuteQueryTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Search Error:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void dgvMenu_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMenuItemID.Text =
                    dgvMenu.Rows[e.RowIndex]
                    .Cells["MenuItemID"]
                    .Value
                    .ToString();


                txtItemName.Text =
                    dgvMenu.Rows[e.RowIndex]
                    .Cells["ItemName"]
                    .Value
                    .ToString();


                txtDescription.Text =
                    dgvMenu.Rows[e.RowIndex]
                    .Cells["Description"]
                    .Value
                    .ToString();


                txtPrice.Text =
                    dgvMenu.Rows[e.RowIndex]
                    .Cells["Price"]
                    .Value
                    .ToString();


                // Category ComboBox select
                cmbCategory.Text =
                    dgvMenu.Rows[e.RowIndex]
                    .Cells["CategoryName"]
                    .Value
                    .ToString();
            }
        }




        private void btnClear_Click(
            object sender,
            EventArgs e)
        {
            ClearFields();

            txtSearch.Clear();

            LoadMenu();
        }



        private void ClearFields()
        {
            txtMenuItemID.Clear();
            txtItemName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();

            cmbCategory.SelectedIndex = -1;

            txtItemName.Focus();
        }
    }
}