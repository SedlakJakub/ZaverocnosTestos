using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Policy;

namespace ProjectSedlakJ
{
    public partial class PracantosEditos : Form
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection conn;
        public PracantosEditos()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }
        private void ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private DataTable ExecuteQuery(SqlCommand command)
        {
            DataTable dataTable = new DataTable();

            try
            {
                conn.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Necos sos nepovedlos!");
                return;
            }
            string hashedPassword;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(textBox3.Text));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                hashedPassword = builder.ToString();
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (ID, Name, Password, Role) VALUES (@ID, @Name, @Password, @Role)", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", checkBox1.Checked ? "Admin" : "User");
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Pracantos pridanos!");
                    }
                    else
                    {
                        MessageBox.Show("Pracantos nepridanos!");
                    }
                }
            }
           

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("None or invalid data have been provided");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE ID=@ID", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    ExecuteNonQuery(cmd);
                }
            }
            MessageBox.Show("Povedlosos!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Necos sos rozbylos!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Users SET Name=@Name, Password=@Password, Role=@Role WHERE ID=@ID", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    string password = textBox3.Text;
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashedBytes = SHA256.Create().ComputeHash(passwordBytes);
                    string hashedPassword = "";
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        builder.Append(hashedBytes[i].ToString("x2"));
                    }
                    hashedPassword = builder.ToString();

                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", checkBox1.Checked ? "Admin" : "User");
                    ExecuteNonQuery(cmd);
                }
            }
            MessageBox.Show("Povedlosos!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn);
            dataGridView1.DataSource = ExecuteQuery(cmd);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Adminos Adminos = new Adminos();
            Adminos.FormClosed += (s, args) => this.Close();
            Adminos.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Loginos Loginos = new Loginos();
            Loginos.FormClosed += (s, args) => this.Close();
            Loginos.Show();
        }
    }
}
