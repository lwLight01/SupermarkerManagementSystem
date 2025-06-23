using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SupermarketManagmentSystem
{
    public partial class Admin_dashboard : Form
    {
        BindingSource customerBindingSource = new BindingSource();
        BindingSource leaveRequestBindingSource = new BindingSource();
        private string loginusername;
        public Admin_dashboard(string username)
        {
            InitializeComponent();
            loginusername = username;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Supermarket Management System Admin Dashboard is a powerful tool designed to help administrators efficiently manage products, inventory, sales, and staff records. It provides a user-friendly interface to monitor supermarket activities in real-time and ensures smooth and secure management of all supermarket operations.");
        }

        private void Admin_dashboard_Load(object sender, EventArgs e)
        {
            this.stock_LogTableAdapter.Fill(this.supermarkerMangementSystemDataSet10.Stock_Log);
            this.applicationTableAdapter.Fill(this.supermarkerMangementSystemDataSet9.Application);
            this.leave_requestTableAdapter.Fill(this.supermarkerMangementSystemDataSet8.Leave_request);
            this.damage_reportTableAdapter.Fill(this.supermarkerMangementSystemDataSet7.Damage_report);

            this.customer_LoginTableAdapter.Fill(this.supermarkerMangementSystemDataSet3.Customer_Login);
            customerBindingSource.DataSource = supermarkerMangementSystemDataSet3.Customer_Login;
            guna2DataGridView3.DataSource = customerBindingSource;
            guna2DataGridView3.ReadOnly = false;
            guna2DataGridView3.AllowUserToAddRows = false;

            this.orderssTableAdapter.Fill(this.supermarkerMangementSystemDataSet2.Orderss);
            this.manage_ProductTableAdapter.Fill(this.supermarkerMangementSystemDataSet1.Manage_Product);

            this.leave_requestTableAdapter.Fill(this.supermarkerMangementSystemDataSet8.Leave_request);
            leaveRequestBindingSource.DataSource = supermarkerMangementSystemDataSet8.Leave_request;
            guna2DataGridView5.DataSource = leaveRequestBindingSource;
            guna2DataGridView5.ReadOnly = false;
            guna2DataGridView5.AllowUserToAddRows = false;

            SqlCommand updateCommand = new SqlCommand();
            updateCommand.Connection = customer_LoginTableAdapter.Connection;
            updateCommand.CommandText =
                "UPDATE Customer_Login SET " +
                "[Full Name] = @FullName, " +
                "[Email] = @Email, " +
                "[Password] = @Password, " +
                "[Phone] = @Phone, " +
                "[Date Of Birth] = @DateOfBirth " +
                "WHERE [User Name] = @UserName";
            updateCommand.Parameters.Clear();
            updateCommand.Parameters.Add("@FullName", SqlDbType.VarChar, 50, "Full Name");
            updateCommand.Parameters.Add("@Email", SqlDbType.VarChar, 50, "Email");
            updateCommand.Parameters.Add("@Password", SqlDbType.VarChar, 50, "Password");
            updateCommand.Parameters.Add("@Phone", SqlDbType.VarChar, 15, "Phone");
            updateCommand.Parameters.Add("@DateOfBirth", SqlDbType.DateTime, 0, "Date Of Birth");
            SqlParameter param = updateCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 50, "User Name");
            param.SourceVersion = DataRowVersion.Original;
            customer_LoginTableAdapter.Adapter.UpdateCommand = updateCommand;

            SqlCommand updateLeaveCommand = new SqlCommand();
            updateLeaveCommand.Connection = leave_requestTableAdapter.Connection;
            updateLeaveCommand.CommandText =
                "UPDATE Leave_request SET " +
                "[Leave request] = @LeaveRequest, " +
                "[Accepted/Rejected] = @AcceptedRejected " +
                "WHERE [Staff Id] = @StaffId";
            updateLeaveCommand.Parameters.Clear();
            updateLeaveCommand.Parameters.Add("@LeaveRequest", SqlDbType.VarChar, 100, "Leave request");
            updateLeaveCommand.Parameters.Add("@AcceptedRejected", SqlDbType.VarChar, 50, "Accepted/Rejected");
            SqlParameter leaveParam = updateLeaveCommand.Parameters.Add("@StaffId", SqlDbType.NChar, 50, "Staff Id");
            leaveParam.SourceVersion = DataRowVersion.Original;
            leave_requestTableAdapter.Adapter.UpdateCommand = updateLeaveCommand;

            guna2DataGridView1.Visible = false;
            guna2DataGridView2.Visible = false;
            guna2DataGridView3.Visible = false;
            guna2Button1.Visible = false;
            guna2DataGridView4.Visible = false;
            guna2DataGridView5.Visible = false;
            guna2DataGridView6.Visible = false;
            guna2DataGridView7.Visible = false;
            guna2Button2.Visible = false;

            this.guna2DataGridView5.DataBindingComplete += guna2DataGridView5_DataBindingComplete;
            guna2HtmlLabel1.Text = "Welcome, " + loginusername;
        }
        private void guna2DataGridView5_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (guna2DataGridView5.Columns.Contains("Staff Id"))
            {
                guna2DataGridView5.Columns["Staff Id"].ReadOnly = true;
            }
        }


        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView2.Visible=true;
        }

        private void aboutToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            guna2DataGridView2.Visible=false;
        }

        private void hideOrderHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView2.Visible =false;
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView3.Visible=true;
            guna2Button1.Visible = true;
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView3.Visible=false;
            guna2Button1.Visible=false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                customerBindingSource.EndEdit();

                this.customer_LoginTableAdapter.Update(this.supermarkerMangementSystemDataSet3.Customer_Login);

                this.supermarkerMangementSystemDataSet3.Customer_Login.Clear();
                this.customer_LoginTableAdapter.Fill(this.supermarkerMangementSystemDataSet3.Customer_Login);

                MessageBox.Show("Update Successful and Data Reloaded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void demageReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView4.Visible=true;
        }

        private void leaveReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView5.Visible = true;
            guna2Button2.Visible=true;
        }

        private void stockRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView6.Visible=true;
        }

        private void stockLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView7.Visible=true;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2DataGridView7.Visible=false;
            guna2DataGridView4.Visible=false;
            guna2DataGridView5.Visible=false;
            guna2DataGridView6.Visible=false;
            guna2Button2.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView5.EndEdit(); 
                leaveRequestBindingSource.EndEdit();

                this.leave_requestTableAdapter.Update(this.supermarkerMangementSystemDataSet8.Leave_request);

                this.supermarkerMangementSystemDataSet8.Leave_request.Clear();
                this.leave_requestTableAdapter.Fill(this.supermarkerMangementSystemDataSet8.Leave_request);

                MessageBox.Show("Leave Request Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}

