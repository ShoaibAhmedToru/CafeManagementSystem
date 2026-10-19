using CafeManagementSystem.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    public partial class CustomerForm : Form
    {
        DataAccess da = new DataAccess();

        public CustomerForm()
        {
            InitializeComponent();

            txtCustomerID.ReadOnly = true;

            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.MultiSelect = false;

            dgvCustomers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCustomers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            LoadCustomers();
        }


     
       

        private void LoadCustomers()
        {
            try
            {
                string sql = @"
                        SELECT
                        c.CustomerID,
                        u.UserID,
                        u.FullName,
                        u.Username,
                        u.Password,
                        u.Email,
                        u.Phone,
                        c.Address
                       FROM Customers AS c
                       INNER JOIN UserInfo AS u
                        ON c.UserID = u.UserID
                      ORDER BY c.CustomerID DESC";

                dgvCustomers.DataSource =
                    da.ExecuteQueryTable(sql);


                if (dgvCustomers.Columns["UserID"] != null)
                {
                    dgvCustomers.Columns["UserID"].Visible = false;
                }


              
                if (dgvCustomers.Columns["Password"] != null)
                {
                    dgvCustomers.Columns["Password"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load customers.\n" +
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
                if (txtFullName.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter full name.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtFullName.Focus();
                    return;
                }


                if (txtUsername.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter username.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtUsername.Focus();
                    return;
                }


                if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter password.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtPassword.Focus();
                    return;
                }


                string checkSQL = @"
                    SELECT COUNT(*)
                    FROM UserInfo
                    WHERE Username = '" +
                    txtUsername.Text.Trim() + "'";


                int count =
                    Convert.ToInt32(
                        da.ExecuteScalar(checkSQL)
                    );


                if (count > 0)
                {
                    MessageBox.Show(
                        "Username already exists.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtUsername.Focus();
                    return;
                }


                string sql = @"
                    INSERT INTO UserInfo
                    (
                        FullName,
                        Username,
                        Password,
                        Email,
                        Phone,
                        Role,
                        IsActive
                    )
                    VALUES
                    (
                        '" + txtFullName.Text.Trim() + @"',
                        '" + txtUsername.Text.Trim() + @"',
                        '" + txtPassword.Text.Trim() + @"',
                        '" + txtEmail.Text.Trim() + @"',
                        '" + txtPhone.Text.Trim() + @"',
                        'Customer',
                        1
                    );

                    SELECT SCOPE_IDENTITY();
                ";


                int userID =
                    Convert.ToInt32(
                        da.ExecuteScalar(sql)
                    );


                string customerSQL = @"
                    INSERT INTO Customers
                    (
                        UserID,
                        Address,
                        CreatedAt
                    )
                    VALUES
                    (
                        " + userID + @",
                        '" + txtAddress.Text.Trim() + @"',
                        GETDATE()
                    )";


                da.ExecuteDMLQuery(customerSQL);


                MessageBox.Show(
                    "Customer registered successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadCustomers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Customer could not be added.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


      

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please select a customer first.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                if (txtFullName.Text.Trim() == "" ||
                    txtUsername.Text.Trim() == "" ||
                    txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Full Name, Username and Password are required.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                string userSQL = @"
                    SELECT UserID
                    FROM Customers
                    WHERE CustomerID = " +
                    txtCustomerID.Text;


                object userResult =
                    da.ExecuteScalar(userSQL);


                if (userResult == null ||
                    userResult == DBNull.Value)
                {
                    MessageBox.Show(
                        "Customer user information not found."
                    );

                    return;
                }


                int userID =
                    Convert.ToInt32(userResult);


                // Check whether another user already has this username
                string checkSQL = @"
                    SELECT COUNT(*)
                    FROM UserInfo
                    WHERE Username = '" +
                    txtUsername.Text.Trim() + @"'
                    AND UserID <> " +
                    userID;


                int count =
                    Convert.ToInt32(
                        da.ExecuteScalar(checkSQL)
                    );


                if (count > 0)
                {
                    MessageBox.Show(
                        "Username already exists.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                string sql = @"
                    UPDATE UserInfo
                    SET
                        FullName = '" + txtFullName.Text.Trim() + @"',
                        Username = '" + txtUsername.Text.Trim() + @"',
                        Password = '" + txtPassword.Text.Trim() + @"',
                        Email = '" + txtEmail.Text.Trim() + @"',
                        Phone = '" + txtPhone.Text.Trim() + @"'
                    WHERE UserID = " + userID;


                da.ExecuteDMLQuery(sql);


                string customerSQL = @"
                    UPDATE Customers
                    SET
                        Address = '" + txtAddress.Text.Trim() + @"'
                    WHERE CustomerID = " +
                    txtCustomerID.Text;


                da.ExecuteDMLQuery(customerSQL);


                MessageBox.Show(
                    "Customer updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadCustomers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Customer could not be updated.\n" +
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
                if (txtCustomerID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please select a customer first.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                DialogResult result =
                    MessageBox.Show(
                        "Are you sure you want to delete this customer?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );


                if (result == DialogResult.No)
                {
                    return;
                }


                string userSQL = @"
                    SELECT UserID
                    FROM Customers
                    WHERE CustomerID = " +
                    txtCustomerID.Text;


                object userResult =
                    da.ExecuteScalar(userSQL);


                if (userResult == null ||
                    userResult == DBNull.Value)
                {
                    MessageBox.Show(
                        "Customer information not found."
                    );

                    return;
                }


                int userID =
                    Convert.ToInt32(userResult);


                string customerSQL = @"
                    DELETE FROM Customers
                    WHERE CustomerID = " +
                    txtCustomerID.Text;


                da.ExecuteDMLQuery(customerSQL);


                string sql = @"
                    DELETE FROM UserInfo
                    WHERE UserID = " + userID;


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Customer deleted successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadCustomers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Customer could not be deleted.\n\n" +
                    "This customer may already have orders.\n\n" +
                    ex.Message,
                    "Delete Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }




        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string search =
                    txtSearch.Text.Trim();


                if (search == "")
                {
                    LoadCustomers();
                    return;
                }


                string sql = @"
                        SELECT
                        c.CustomerID,
                        u.UserID,
                        u.FullName,
                        u.Username,
                        u.Password,
                        u.Email,
                        u.Phone,
                        c.Address
                         FROM Customers AS c

                        INNER JOIN UserInfo AS u
                        ON c.UserID = u.UserID

                        WHERE
                        u.FullName LIKE '%" +
                        search + @"%'

                    OR
                        u.Username LIKE '%" +
                        search + @"%'

                    OR
                        u.Email LIKE '%" +
                        search + @"%'

                    OR
                        u.Phone LIKE '%" +
                        search + @"%'

                    OR
                        c.Address LIKE '%" +
                        search + @"%'

                    ORDER BY c.CustomerID DESC";


                dgvCustomers.DataSource =
                    da.ExecuteQueryTable(sql);


                if (dgvCustomers.Columns["UserID"] != null)
                {
                    dgvCustomers.Columns["UserID"].Visible = false;
                }


                if (dgvCustomers.Columns["Password"] != null)
                {
                    dgvCustomers.Columns["Password"].Visible = false;
                }
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


     
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();

            txtSearch.Clear();

            LoadCustomers();
        }


   

        private void ClearFields()
        {
            txtCustomerID.Clear();
            txtFullName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();

            txtFullName.Focus();
        }


       

        private void dgvCustomers_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }


            DataGridViewRow row =
                dgvCustomers.Rows[e.RowIndex];


            txtCustomerID.Text =
                row.Cells["CustomerID"]
                .Value.ToString();


            txtFullName.Text =
                row.Cells["FullName"]
                .Value.ToString();


            txtUsername.Text =
                row.Cells["Username"]
                .Value.ToString();


            txtPassword.Text =
                row.Cells["Password"]
                .Value.ToString();


            txtEmail.Text =
                row.Cells["Email"]
                .Value.ToString();


            txtPhone.Text =
                row.Cells["Phone"]
                .Value.ToString();


            txtAddress.Text =
                row.Cells["Address"]
                .Value.ToString();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {

        }
    }
}