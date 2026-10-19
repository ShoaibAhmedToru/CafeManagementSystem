using CafeManagementSystem.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    public partial class TableForm : Form
    {
        DataAccess da = new DataAccess();

        public TableForm()
        {
            InitializeComponent();

            txtTableID.ReadOnly = true;

            dgvTables.ReadOnly = true;
            dgvTables.AllowUserToAddRows = false;
            dgvTables.AllowUserToDeleteRows = false;
            dgvTables.MultiSelect = false;

            dgvTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            cmbStatus.Items.Clear();

            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Occupied");
            cmbStatus.Items.Add("Reserved");

            cmbStatus.SelectedIndex = 0;


            LoadTables();
        }


        

        private void LoadTables()
        {
            try
            {
                string sql = "SELECT TableID, TableNumber, Capacity, Status FROM CafeTables ORDER BY TableID DESC";

                dgvTables.DataSource = da.ExecuteQueryTable(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load tables.\n" +
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
                if (txtTableNumber.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter table number.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtTableNumber.Focus();
                    return;
                }


                int capacity;

                if (!int.TryParse(
                    txtCapacity.Text.Trim(),
                    out capacity))
                {
                    MessageBox.Show(
                        "Please enter a valid capacity.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCapacity.Focus();
                    return;
                }


                if (capacity <= 0)
                {
                    MessageBox.Show(
                        "Capacity must be greater than 0.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                if (cmbStatus.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Please select table status.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                // Check duplicate table number
                string checkSQL = "SELECT COUNT(*) FROM CafeTables WHERE TableNumber = '" + txtTableNumber.Text.Trim() + "'";


                int count =
                    Convert.ToInt32(
                        da.ExecuteScalar(checkSQL)
                    );


                if (count > 0)
                {
                    MessageBox.Show(
                        "Table number already exists.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                string sql = "INSERT INTO CafeTables (TableNumber, Capacity, Status) VALUES ('" + txtTableNumber.Text.Trim() + "', " + capacity + ", '" + cmbStatus.Text + "')";


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Table added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadTables();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Table could not be added.\n" +
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
                if (txtTableID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please select a table first.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                if (txtTableNumber.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter table number.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                int capacity;

                if (!int.TryParse(
                    txtCapacity.Text.Trim(),
                    out capacity))
                {
                    MessageBox.Show(
                        "Please enter a valid capacity.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                if (capacity <= 0)
                {
                    MessageBox.Show(
                        "Capacity must be greater than 0.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                string checkSQL =
                    "SELECT COUNT(*) FROM CafeTables " +
                    "WHERE TableNumber = '" +
                    txtTableNumber.Text.Trim() + "' " +

                    "AND TableID <> " +
                    txtTableID.Text;


                int count =
                    Convert.ToInt32(
                        da.ExecuteScalar(checkSQL)
                    );


                if (count > 0)
                {
                    MessageBox.Show(
                        "Table number already exists.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                string sql =
                    "UPDATE CafeTables SET " +

                    "TableNumber='" +
                    txtTableNumber.Text.Trim() +

                    "', Capacity=" +
                    capacity +

                    ", Status='" +
                    cmbStatus.Text +

                    "' WHERE TableID=" +
                    txtTableID.Text;


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Table updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadTables();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Table could not be updated.\n" +
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
                if (txtTableID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please select a table first.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }


                DialogResult result =
                    MessageBox.Show(
                        "Are you sure you want to delete this table?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );


                if (result == DialogResult.No)
                {
                    return;
                }


                string sql =
                    "DELETE FROM CafeTables " +
                    "WHERE TableID=" +
                    txtTableID.Text;


                da.ExecuteDMLQuery(sql);


                MessageBox.Show(
                    "Table deleted successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                LoadTables();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Table could not be deleted.\n\n" +
                    "This table may already be used in an order.\n\n" +
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
                    LoadTables();
                    return;
                }


                string sql = @"
                    SELECT
                        TableID,
                        TableNumber,
                        Capacity,
                        Status
                    FROM CafeTables

                    WHERE
                        TableNumber LIKE '%" +
                        search + @"%'

                    OR
                        Status LIKE '%" +
                        search + @"%'

                    ORDER BY TableID DESC";


                dgvTables.DataSource =
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


    

        private void dgvTables_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }


            DataGridViewRow row =
                dgvTables.Rows[e.RowIndex];


            txtTableID.Text =
                row.Cells["TableID"]
                .Value.ToString();


            txtTableNumber.Text =
                row.Cells["TableNumber"]
                .Value.ToString();


            txtCapacity.Text =
                row.Cells["Capacity"]
                .Value.ToString();


            cmbStatus.Text =
                row.Cells["Status"]
                .Value.ToString();
        }


        

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();

            txtSearch.Clear();

            LoadTables();
        }



        private void ClearFields()
        {
            txtTableID.Clear();
            txtTableNumber.Clear();
            txtCapacity.Clear();

            cmbStatus.SelectedIndex = 0;

            txtTableNumber.Focus();
        }
    }
}