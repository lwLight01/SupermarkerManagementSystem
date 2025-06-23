using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SupermarketManagmentSystem
{
    public partial class Customer_Dashboard : Form
    {
        private string loggedInUsername;

        public Customer_Dashboard()
        {
            InitializeComponent();
        }
        public Customer_Dashboard(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Customer_Dashboard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supermarkerMangementSystemDataSet11.Orderss' table. You can move, or remove it, as needed.
            this.orderssTableAdapter.Fill(this.supermarkerMangementSystemDataSet11.Orderss);
            // TODO: This line of code loads data into the 'supermarkerMangementSystemDataSet.Manage_Product' table. You can move, or remove it, as needed.
            this.manage_ProductTableAdapter.Fill(this.supermarkerMangementSystemDataSet.Manage_Product);
            guna2DataGridView1.Visible = false;
            guna2DataGridView1.Columns[5].Visible = false;
            panel1.Visible = false;
            guna2DataGridView3.Visible = false;
            guna2DataGridView2.Columns.Add("ProductId", "Product Id");
            guna2DataGridView2.Columns.Add("ProductName", "Product Name");
            guna2DataGridView2.Columns.Add("Price", "Price");
            lblUsername.Text = "Welcome, " + loggedInUsername;
        }
        public class CartItem
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public double Price { get; set; }
            public int Quantity { get; set; }
        }
        List<CartItem> cart = new List<CartItem>();

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int newRowIndex = guna2DataGridView2.Rows.Add();

                guna2DataGridView2.Rows[newRowIndex].Cells[0].Value = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); // Product ID
                guna2DataGridView2.Rows[newRowIndex].Cells[1].Value = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // Product Name
                guna2DataGridView2.Rows[newRowIndex].Cells[2].Value = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(); // Price
               
                CalculateTotalPrice();                                                                                                                  
            }
        }
        private double CalculateTotalPrice()
        {
            double totalPrice = 0;

            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    double price = 0;
                    bool isPriceValid = double.TryParse(row.Cells[2].Value.ToString(), out price);

                    if (isPriceValid)
                    {
                        totalPrice += price;
                    }
                }
            }

            labeltotal.Text = "Total: " + totalPrice.ToString("0.00") + " Tk";

            return totalPrice;
        }



        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Visible=true;
        }
        private void InsertOrderData()
        {
            string connectionString = @"Data Source=TUTUL\SQLEXPRESS;Initial Catalog=SupermarkerMangementSystem;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {
                string uname = loggedInUsername;
                DateTime date = DateTime.Now;
                double total = CalculateTotalPrice();

                StringBuilder productIds = new StringBuilder();
                StringBuilder productNames = new StringBuilder();

                foreach (DataGridViewRow row in guna2DataGridView2.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        string productId = row.Cells[0].Value.ToString();
                        productIds.Append(productId + ", ");
                        productNames.Append(row.Cells[1].Value.ToString() + ", ");
                        string updateQuantityQuery = "UPDATE Manage_Product SET Quantity = Quantity - 1 WHERE [Prodect Id] = @pid";
                        SqlCommand updateCmd = new SqlCommand(updateQuantityQuery, conn, transaction);
                        updateCmd.Parameters.AddWithValue("@pid", productId);
                        updateCmd.ExecuteNonQuery();
                    }
                }

                if (productIds.Length > 0) productIds.Length -= 2;
                if (productNames.Length > 0) productNames.Length -= 2;

                string q = @"INSERT INTO Orderss
            ([User Name], [Product Id], [Product Name], [Price], [Date of product], [Total]) 
            VALUES (@uname, @pid, @pname, @price, @date, @total)";

                SqlCommand cmd = new SqlCommand(q, conn, transaction);
                cmd.Parameters.AddWithValue("@uname", uname);
                cmd.Parameters.AddWithValue("@pid", productIds.ToString());
                cmd.Parameters.AddWithValue("@pname", productNames.ToString());
                cmd.Parameters.AddWithValue("@price", total);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Order Placed Successfully and Stock Updated!");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Placeorbtn_Click(object sender, EventArgs e)
        {
            InsertOrderData();
            panel1.Visible = false;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Placeorbtn_DoubleClick(object sender, EventArgs e)
        {
            guna2DataGridView2.Visible
                = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close
                ();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=TUTUL\SQLEXPRESS;Initial Catalog=SupermarkerMangementSystem;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT [Date Of Product], [Total] 
                             FROM Orderss 
                             WHERE [User Name] = @username
                             ORDER BY [Date Of Product] DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", loggedInUsername);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    guna2DataGridView3.DataSource = dt;
                    guna2DataGridView3.Visible = true;
                    guna2DataGridView3.Columns[0].Visible = false;
                    guna2DataGridView3.Columns[1].Visible = false;
                    guna2DataGridView3.Columns[2].Visible = false;
                    guna2DataGridView3.Columns[5].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading order history: " + ex.Message);
                }
            }
        }

        private void guna2Button2_DoubleClick(object sender, EventArgs e)
        {
            guna2DataGridView3.Visible=false;
        }

        private void guna2Button1_DoubleClick(object sender, EventArgs e)
        {
            guna2DataGridView1.Visible=false;
        }
    }
}
