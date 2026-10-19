using System;
using System.Data;
using System.Windows.Forms;
using CafeManagementSystem.Data;

namespace CafeManagementSystem.Forms
{
    internal partial class LoginForm : Form
    {
        DataAccess da = new DataAccess();

        public LoginForm()
        {
            InitializeComponent();

           
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (txtUserID.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter User ID.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtUserID.Focus();
                    return;
                }


                if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show(
                        "Please enter Password.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtPassword.Focus();
                    return;
                }



                int userID;

                if (!int.TryParse(txtUserID.Text.Trim(), out userID))
                {
                    MessageBox.Show(
                        "User ID must be a number.",
                        "Invalid User ID",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtUserID.Clear();
                    txtUserID.Focus();

                    return;
                }


                string password = txtPassword.Text.Trim();


               

                string sql = @"
                    SELECT
                        UserID,
                        FullName,
                        Username,
                        Role
                    FROM UserInfo
                    WHERE UserID = " + userID + @"
                    AND Password = '" + password + @"'
                    AND IsActive = 1";


                DataTable dt =
                    da.ExecuteQueryTable(sql);



                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Invalid User ID or Password.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    txtPassword.Clear();
                    txtPassword.Focus();

                    return;
                }


         
                Session.UserID =
                    Convert.ToInt32(
                        dt.Rows[0]["UserID"]
                    );

                Session.FullName =
                    dt.Rows[0]["FullName"].ToString();

                Session.Username =
                    dt.Rows[0]["Username"].ToString();

                Session.Role =
                    dt.Rows[0]["Role"].ToString();


                MessageBox.Show(
                    "Login Successful!\nWelcome " +
                    Session.FullName,
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


               

                if (Session.Role == "Admin")
                {
                    AdminDashboard dashboard =
                        new AdminDashboard();

                    this.Hide();

                    dashboard.ShowDialog();

                    this.Show();
                }


                

                else if (Session.Role == "Customer")
                {
                    CustomerDashboard dashboard =
                        new CustomerDashboard();

                    this.Hide();

                    dashboard.ShowDialog();

                    this.Show();
                }


            

                else
                {
                    MessageBox.Show(
                        "This user role is not supported.",
                        "Role Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    Session.Clear();
                }


               


                txtUserID.Clear();
                txtPassword.Clear();

                txtUserID.Focus();
            }

            catch (Exception ex)
            {
                MessageBox.Show(
                    "Login Error:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Do you want to exit?",
                    "Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}