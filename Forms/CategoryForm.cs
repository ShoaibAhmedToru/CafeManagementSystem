using System;
using System.Data;
using System.Windows.Forms;
using CafeManagementSystem.Data;

namespace CafeManagementSystem.Forms
{
    public partial class CategoryForm : Form
    {
        DataAccess da = new DataAccess();

        public CategoryForm()
        {
            InitializeComponent();

       
            dgvCategories.ReadOnly = true;
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.MultiSelect = false;

            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvCategories.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;

           
            txtCategoryID.ReadOnly = true;

            LoadCategories();
        }


       
      
 

        private void LoadCategories()
        {
            try
            {
                string sql = @"
                    SELECT
                        CategoryID,
                        CategoryName,
                        Description,
                        IsActive
                    FROM Categories
                    ORDER BY CategoryID DESC";

                dgvCategories.DataSource = da.ExecuteQueryTable(sql);
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


    
     
 

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (txtCategoryName.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter category name.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCategoryName.Focus();
                    return;
                }


                string sql =
                    "INSERT INTO Categories " +
                    "(CategoryName, Description, IsActive) VALUES ('" +
                    txtCategoryName.Text.Trim() + "','" +
                    txtDescription.Text.Trim() + "',1)";


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Category added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Category could not be added.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


      
        //update category
     

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (txtCategoryID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please select a category first.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                // Check category name
                if (txtCategoryName.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter category name.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCategoryName.Focus();
                    return;
                }


                string sql =
                    "UPDATE Categories SET " +
                    "CategoryName='" +
                    txtCategoryName.Text.Trim() +
                    "', Description='" +
                    txtDescription.Text.Trim() +
                    "' WHERE CategoryID=" +
                    txtCategoryID.Text;


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Category updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Category could not be updated.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please select a category first.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                DialogResult result =
                    MessageBox.Show(
                        "Are you sure you want to delete this category?",
                        "Delete Category",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );


                if (result == DialogResult.No)
                {
                    return;
                }


                string sql =
                    "DELETE FROM Categories WHERE CategoryID=" +
                    txtCategoryID.Text;


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Category deleted successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Category could not be deleted.\n\n" +
                    "This category may already be used by a menu item.\n\n" +
                    ex.Message,
                    "Delete Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


    
        // search category
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string search =
                    txtSearch.Text.Trim();


                if (search == "")
                {
                    LoadCategories();
                    return;
                }


                string sql = @"
                    SELECT
                        CategoryID,
                        CategoryName,
                        Description,
                        IsActive
                    FROM Categories
                    WHERE CategoryName LIKE '%" +
                    search + @"%'
                    OR Description LIKE '%" +
                    search + @"%'
                    ORDER BY CategoryID DESC";


                dgvCategories.DataSource =
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


       

        private void dgvCategories_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCategoryID.Text =
                    dgvCategories.Rows[e.RowIndex]
                    .Cells["CategoryID"]
                    .Value
                    .ToString();


                txtCategoryName.Text =
                    dgvCategories.Rows[e.RowIndex]
                    .Cells["CategoryName"]
                    .Value
                    .ToString();


                txtDescription.Text =
                    dgvCategories.Rows[e.RowIndex]
                    .Cells["Description"]
                    .Value
                    .ToString();
            }
        }


       

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();

            txtSearch.Clear();

            LoadCategories();
        }


        private void ClearFields()
        {
            txtCategoryID.Clear();
            txtCategoryName.Clear();
            txtDescription.Clear();

            txtCategoryName.Focus();
        }
    }
}