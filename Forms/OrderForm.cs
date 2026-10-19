using CafeManagementSystem.Data;
using CafeManagementSystem.Forms.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CafeManagementSystem.Forms
{
    public partial class OrderForm : Form
    {
        DataAccess da = new DataAccess();

        List<CartItem> cart =
            new List<CartItem>();

        public OrderForm()
        {
            InitializeComponent();

            LoadCustomers();
            LoadTables();
            LoadOrderTypes();
            LoadMenu();

            txtQuantity.Text = "1";
            txtDeliveryFee.Text = "0";

            lblSubtotal.Text = "0.00";
            lblGrandTotal.Text = "0.00";
        }


        //para constructor for Customer
        public OrderForm(int customerID)
        {
            InitializeComponent();

            LoadCustomers();
            LoadTables();
            LoadOrderTypes();
            LoadMenu();

            cmbCustomer.SelectedValue =
                customerID;

            cmbCustomer.Enabled = false;

            txtQuantity.Text = "1";
            txtDeliveryFee.Text = "0";

            lblSubtotal.Text = "0.00";
            lblGrandTotal.Text = "0.00";
        }


      
        private void LoadCustomers()
        {
            try
            {
                string sql = @"
                    SELECT
                        c.CustomerID,
                        u.FullName
                    FROM Customers c

                    INNER JOIN UserInfo u
                        ON c.UserID = u.UserID";

                cmbCustomer.DataSource =
                    da.ExecuteQueryTable(sql);

                cmbCustomer.DisplayMember =
                    "FullName";

                cmbCustomer.ValueMember =
                    "CustomerID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Customer Load Error:\n" +
                    ex.Message
                );
            }
        }


     
        private void LoadTables()
        {
            try
            {
                string sql = @"
                    SELECT
                        TableID,
                        TableNumber
                    FROM CafeTables
                    WHERE Status = 'Available'";

                cmbTable.DataSource =
                    da.ExecuteQueryTable(sql);

                cmbTable.DisplayMember =
                    "TableNumber";

                cmbTable.ValueMember =
                    "TableID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Table Load Error:\n" +
                    ex.Message
                );
            }
        }


        
        private void LoadOrderTypes()
        {
            cmbOrderType.Items.Clear();

            cmbOrderType.Items.Add("Dine-In");
            cmbOrderType.Items.Add("Takeaway");
            cmbOrderType.Items.Add("Delivery");

            cmbOrderType.SelectedIndex = 0;
        }


     
        private void LoadMenu()
        {
            try
            {
                string sql = @"
                    SELECT
                        MenuItemID,
                        ItemName,
                        Price
                    FROM MenuItems
                    WHERE IsAvailable = 1";

                dgvMenu.DataSource =
                    da.ExecuteQueryTable(sql);

                dgvMenu.ReadOnly = true;
                dgvMenu.AllowUserToAddRows = false;

                dgvMenu.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvMenu.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Menu Load Error:\n" +
                    ex.Message
                );
            }
        }


     
        private void btnAddToCart_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (dgvMenu.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Please select a menu item."
                    );

                    return;
                }


                int quantity;

                if (!int.TryParse(
                    txtQuantity.Text.Trim(),
                    out quantity))
                {
                    MessageBox.Show(
                        "Please enter a valid quantity."
                    );

                    return;
                }


                if (quantity <= 0)
                {
                    MessageBox.Show(
                        "Quantity must be greater than 0."
                    );

                    return;
                }


                int menuItemID =
                    Convert.ToInt32(
                        dgvMenu.CurrentRow
                        .Cells["MenuItemID"]
                        .Value
                    );


                string itemName =
                    dgvMenu.CurrentRow
                    .Cells["ItemName"]
                    .Value
                    .ToString();


                decimal price =
                    Convert.ToDecimal(
                        dgvMenu.CurrentRow
                        .Cells["Price"]
                        .Value
                    );


                CartItem item =
                    new CartItem();

                item.MenuItemID =
                    menuItemID;

                item.ItemName =
                    itemName;

                item.UnitPrice =
                    price;

                item.Quantity =
                    quantity;


                cart.Add(item);


                RefreshCart();

                CalculateTotal();


                txtQuantity.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Add To Cart Error:\n" +
                    ex.Message
                );
            }
        }


        // ==================================
        // REFRESH CART
        // ==================================
        private void RefreshCart()
        {
            dgvCart.DataSource = null;

            dgvCart.DataSource =
                cart;

            dgvCart.ReadOnly = true;
            dgvCart.AllowUserToAddRows = false;

            dgvCart.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCart.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }


      
        private void CalculateTotal()
        {
            decimal subtotal =
                cart.Sum(
                    x => x.TotalPrice
                );


            decimal delivery = 0;


            decimal.TryParse(
                txtDeliveryFee.Text.Trim(),
                out delivery
            );


            if (delivery < 0)
            {
                delivery = 0;
            }


            decimal grandTotal =
                subtotal + delivery;


            lblSubtotal.Text =
                subtotal.ToString("0.00");


            lblGrandTotal.Text =
                grandTotal.ToString("0.00");
        }


        
        private void txtDeliveryFee_TextChanged(
            object sender,
            EventArgs e)
        {
            CalculateTotal();
        }


        private void btnRemove_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (dgvCart.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Please select a cart item."
                    );

                    return;
                }


                int index =
                    dgvCart.CurrentRow.Index;


                if (index >= 0 &&
                    index < cart.Count)
                {
                    cart.RemoveAt(index);
                }


                RefreshCart();

                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Remove Error:\n" +
                    ex.Message
                );
            }
        }


 
        private void btnPlaceOrder_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (cart.Count == 0)
                {
                    MessageBox.Show(
                        "Cart is empty."
                    );

                    return;
                }


                if (cmbCustomer.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Please select a customer."
                    );

                    return;
                }


                if (cmbOrderType.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Please select order type."
                    );

                    return;
                }


                // Dine-In requires table
                if (cmbOrderType.Text == "Dine-In" &&
                    cmbTable.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Please select a table."
                    );

                    return;
                }


                decimal subtotal =
                    cart.Sum(
                        x => x.TotalPrice
                    );


                decimal delivery = 0;


                if (!decimal.TryParse(
                    txtDeliveryFee.Text.Trim(),
                    out delivery))
                {
                    MessageBox.Show(
                        "Please enter a valid delivery fee."
                    );

                    return;
                }


                if (delivery < 0)
                {
                    MessageBox.Show(
                        "Delivery fee cannot be negative."
                    );

                    return;
                }


                decimal grandTotal =
                    subtotal + delivery;


                string orderNumber =
                    "ORD" +
                    DateTime.Now.ToString(
                        "yyyyMMddHHmmssfff"
                    );


                // =========================
                // INSERT INTO ORDERS
                // =========================

                string sql = @"
                    INSERT INTO Orders
                    (
                        OrderNumber,
                        CustomerID,
                        TableID,
                        OrderType,
                        OrderDate,
                        Subtotal,
                        Discount,
                        Tax,
                        DeliveryFee,
                        GrandTotal,
                        PaymentStatus,
                        OrderStatus
                    )
                    VALUES
                    (
                        @OrderNumber,
                        @CustomerID,
                        @TableID,
                        @OrderType,
                        GETDATE(),
                        @Subtotal,
                        0,
                        0,
                        @DeliveryFee,
                        @GrandTotal,
                        'Pending',
                        'Pending'
                    );

                    SELECT SCOPE_IDENTITY();
                ";


                SqlCommand cmd =
                    new SqlCommand(
                        sql,
                        da.Sqlcon
                    );


                cmd.Parameters.AddWithValue(
                    "@OrderNumber",
                    orderNumber
                );


                cmd.Parameters.AddWithValue(
                    "@CustomerID",
                    Convert.ToInt32(
                        cmbCustomer.SelectedValue
                    )
                );


                // Table only for Dine-In
                if (cmbOrderType.Text == "Dine-In")
                {
                    cmd.Parameters.AddWithValue(
                        "@TableID",
                        Convert.ToInt32(
                            cmbTable.SelectedValue
                        )
                    );
                }
                else
                {
                    cmd.Parameters.AddWithValue(
                        "@TableID",
                        DBNull.Value
                    );
                }


                cmd.Parameters.AddWithValue(
                    "@OrderType",
                    cmbOrderType.Text
                );


                cmd.Parameters.AddWithValue(
                    "@Subtotal",
                    subtotal
                );


                cmd.Parameters.AddWithValue(
                    "@DeliveryFee",
                    delivery
                );


                cmd.Parameters.AddWithValue(
                    "@GrandTotal",
                    grandTotal
                );


                object result =
                    cmd.ExecuteScalar();


                if (result == null ||
                    result == DBNull.Value)
                {
                    MessageBox.Show(
                        "Order ID could not be generated."
                    );

                    return;
                }


                int orderID =
                    Convert.ToInt32(result);



                foreach (CartItem item in cart)
                {
                    string detailSQL = @"
                        INSERT INTO OrderDetails
                        (
                            OrderID,
                            MenuItemID,
                            Quantity,
                            UnitPrice
                        )
                        VALUES
                        (
                            @OrderID,
                            @MenuItemID,
                            @Quantity,
                            @UnitPrice
                        )";


                    SqlCommand detailCmd =
                        new SqlCommand(
                            detailSQL,
                            da.Sqlcon
                        );


                    detailCmd.Parameters.AddWithValue(
                        "@OrderID",
                        orderID
                    );


                    detailCmd.Parameters.AddWithValue(
                        "@MenuItemID",
                        item.MenuItemID
                    );


                    detailCmd.Parameters.AddWithValue(
                        "@Quantity",
                        item.Quantity
                    );


                    detailCmd.Parameters.AddWithValue(
                        "@UnitPrice",
                        item.UnitPrice
                    );


                    detailCmd.ExecuteNonQuery();
                }



                if (cmbOrderType.Text == "Dine-In")
                {
                    string tableSQL =
                        "UPDATE CafeTables " +
                        "SET Status = 'Occupied' " +
                        "WHERE TableID = " +
                        Convert.ToInt32(
                            cmbTable.SelectedValue
                        );


                    da.ExecuteDMLQuery(
                        tableSQL
                    );
                }


                MessageBox.Show(
                    "Order placed successfully.\n\n" +
                    "Order Number: " +
                    orderNumber +
                    "\nGrand Total: " +
                    grandTotal.ToString("0.00"),
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                ClearFields();

                LoadTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Order Error:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        
        private void btnClear_Click(
            object sender,
            EventArgs e)
        {
            ClearFields();
        }


        private void ClearFields()
        {
            cart.Clear();

            dgvCart.DataSource = null;

            txtQuantity.Text = "1";

            txtDeliveryFee.Text = "0";

            lblSubtotal.Text = "0.00";

            lblGrandTotal.Text = "0.00";


            if (cmbOrderType.Items.Count > 0)
            {
                cmbOrderType.SelectedIndex = 0;
            }
        }

        private void lblSubtotal_Click(object sender, EventArgs e)
        {

        }
    }
}