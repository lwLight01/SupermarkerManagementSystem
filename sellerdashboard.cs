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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SupermarketManagmentSystem
{
    public partial class sellerdashboard : Form
    {
        private string loggedInSeller;
        private string app;
        public sellerdashboard(string Seller_Id)
        {
            InitializeComponent();
            loggedInSeller = Seller_Id;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sellerdashboard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supermarkerMangementSystemDataSet5.Orderss' table. You can move, or remove it, as needed.
            this.orderssTableAdapter.Fill(this.supermarkerMangementSystemDataSet5.Orderss);
            // TODO: This line of code loads data into the 'supermarkerMangementSystemDataSet4.Manage_Product' table. You can move, or remove it, as needed.
            this.manage_ProductTableAdapter.Fill(this.supermarkerMangementSystemDataSet4.Manage_Product);
            guna2DataGridView1.Visible = false;
            guna2Panel1.Visible = false;
            guna2DataGridView2.Visible = false;
            guna2DataGridView3.Visible = false;
            lblSeller.Text = "Welcome, " + loggedInSeller;
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Visible = true;
            guna2DataGridView1.Columns[5].Visible = false;
        }

        private void viewProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Visible
                =false;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2Panel1.Visible = true;
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2Panel1.Visible=false;
        }
        private void insert()
        {
            string connectionString = @"Data Source=TUTUL\SQLEXPRESS;Initial Catalog=SupermarkerMangementSystem;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string q = @"INSERT INTO Application 
([Seller Id], [Application])
VALUES (@sellerId, @application)";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@sellerId", loggedInSeller);
            cmd.Parameters.AddWithValue("@application", richTextBox1.Text);
            cmd.ExecuteNonQuery();
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            app = richTextBox1.Text;
            insert();
            MessageBox.Show("Succesfully Submit an applicatio");
            richTextBox1.Text = "";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView2.Visible = true;
        }

        private void hideOrderHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView2.Visible=false;
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView3.Visible = true;
            guna2DataGridView3.Columns[1].Visible = false;
            guna2DataGridView3.Columns[2].Visible = false;
            guna2DataGridView3.Columns[5].Visible = false;
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView3.Visible = false;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void lblSeller_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
