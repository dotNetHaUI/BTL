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

namespace Modul5A
{
    public partial class RegisterForm : Form
    {

        SqlConnection con;
        string connection = "Data Source=HAINGUYEN;Initial Catalog=module5a;Integrated Security=True";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            loadDataFromDatabase();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to cancel?", "Alert", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Close();
            }
        }

        public void loadDataFromDatabase()
        {
            loadRoles();
            loadOffice();
        }

        public void loadRoles()
        {

            try
            {
                con = new SqlConnection(connection);
                string query = "SELECT Title FROM Roles";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);

                DataTable dtb = new DataTable();
                sda.Fill(dtb);

                foreach (DataRow dr in dtb.Rows)
                {
                    cbb_Roles.Items.Add(dr["Title"].ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public void loadOffice()
        {
            try
            {
                con = new SqlConnection(connection);
                string query = "SELECT Title from Offices";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);

                DataTable dtb = new DataTable();
                sda.Fill(dtb);

                foreach (DataRow dr in dtb.Rows)
                {
                    cbb_Office.Items.Add(dr["Title"].ToString());
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (validatePass())
            {
                try
                {
                    con = new SqlConnection(connection);
                    string query = "INSERT INTO Users VALUES (@roleID, @officeID, @email, @password, @firstName, @lastName, @dateOfBirth, @active)";
                    SqlCommand scd = new SqlCommand(query, con);

                    scd.Parameters.AddWithValue("@roleID", cbb_Roles.Text);
                    scd.Parameters.AddWithValue("@officeID", cbb_Office.Text);
                    scd.Parameters.AddWithValue("@email", txb_Email.Text);
                    scd.Parameters.AddWithValue("@password", txb_Password);
                    scd.Parameters.AddWithValue("@password", txb_Password.Text);
                    scd.Parameters.AddWithValue("@firstName", txb_FirstName.Text);
                    scd.Parameters.AddWithValue("@lastName", txb_LastName.Text);
                    scd.Parameters.Add("@dateOfBirth", SqlDbType.Date).Value = dtp_DOB.Text;
                    scd.Parameters.AddWithValue("@active", 0);
                    MessageBox.Show("Register successfully");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

            }
            else
            {
                MessageBox.Show("Password is incorrect. Try again");
            }

        }

        public bool validatePass()
        {
            if (txb_Password.Text == txb_ComfirmPassword.Text)
            {
                return true;
            }
            else
                return false;
        }


    }
}
