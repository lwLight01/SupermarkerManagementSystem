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

namespace SupermarketManagmentSystem
{
    public partial class Staffdashboad : Form
    {
        private string loggedInStaff;
        public Staffdashboad(string username)
        {
            InitializeComponent();
            loggedInStaff = username;
        }

        private void Staffdashboad_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supermarkerMangementSystemDataSet6.Manage_Product' table. You can move, or remove it, as needed.
            this.manage_ProductTableAdapter.Fill(this.supermarkerMangementSystemDataSet6.Manage_Product);
            guna2DataGridView1.Visible = false;
            richTextBox1.Visible = false;
            guna2Button1.Visible = false;
            lblStaff.Text = "Welcome, " + loggedInStaff;
            richTextBox2.Visible=false;
            guna2Button2.Visible=false;
            richTextBox3.Visible=false;
            guna2Button3.Visible=false;
            guna2DataGridView2.Visible=false;
        }

        private void viewProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Visible = true;
            guna2DataGridView1.Columns[5].Visible = false;
        }

        private void hideProductViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Visible = false;
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            guna2Button1.Visible = true;
        }

        private void closeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible=false;
            guna2Button1.Visible=false;
        }
        private void insert()
        {
            string connectionString = @"Data Source=TUTUL\SQLEXPRESS;Initial Catalog=SupermarkerMangementSystem;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string q = @"INSERT INTO Stock_Log
([Staff Id], [Log])
VALUES (@staffId, @logText)";

            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@staffId", loggedInStaff);
            cmd.Parameters.AddWithValue("@logText", richTextBox1.Text);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        private void insertleave()
        {
            string connectionString = @"Data Source=TUTUL\SQLEXPRESS;Initial Catalog=SupermarkerMangementSystem;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string q = @"INSERT INTO Leave_request
([Staff Id], [Leave request],[Accepted/Rejected])
VALUES (@staffId, @requText,@acc)";

            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@staffId", loggedInStaff);
            cmd.Parameters.AddWithValue("@requText", richTextBox2.Text);
            cmd.Parameters.AddWithValue("@acc","");
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        private void insertD()
        {
            string connectionString = @"Data Source=TUTUL\SQLEXPRESS;Initial Catalog=SupermarkerMangementSystem;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string q = @"INSERT INTO Damage_report
([Staff Id], [Damage report])
VALUES (@staffId, @reportText)";

            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@staffId", loggedInStaff);
            cmd.Parameters.AddWithValue("@reportText", richTextBox3.Text);

            cmd.ExecuteNonQuery();
            conn.Close();
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            insert();
            MessageBox.Show("Submit Successful");
            richTextBox1.Text = "Submit the item in warehouse";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Seller Dashboard in the Supermarket Management System is a dedicated workspace designed for sellers to efficiently manage their product listings and track sales performance.\\n\\nKey Features:\\n- Stock Check.\\n- Application to admin for stock.\\n- Monitor personal sales history.\\n- Manage customer product requests.");
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Visible = true;
            guna2Button2.Visible=true;
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Visible = false;
            guna2Button2.Visible = false;
            guna2DataGridView2.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            insertleave();
            MessageBox.Show("Submit Successful");
            richTextBox2.Text = "";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox3.Visible = true;
            guna2Button3.Visible=true;
        }

        private void hideOrderHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox3.Visible = false;
            guna2Button3.Visible=false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            insertD();
            MessageBox.Show("Succesful");
            richTextBox3.Text = "";
        }

        private void lblStaff_Click(object sender, EventArgs e)
        {

        }

        private void seeRequestFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=TUTUL\SQLEXPRESS;Initial Catalog=SupermarkerMangementSystem;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT [Staff Id], [Leave request], [Accepted/Rejected]
                             FROM [Leave_request]
                             WHERE [Staff Id] = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", loggedInStaff);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    guna2DataGridView2.DataSource = dt;
                    guna2DataGridView2.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading order history: " + ex.Message);
                }
            }
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
