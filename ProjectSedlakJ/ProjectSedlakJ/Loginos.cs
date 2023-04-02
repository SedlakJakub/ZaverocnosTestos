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

namespace ProjectSedlakJ
{
    public partial class Loginos : Form
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Loginos()
        {
            InitializeComponent();
        }    
        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string username = textBox1.Text;
                    string password = textBox2.Text;
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Zadejtos spravnos heslos nebos jmenos!");
                        return;
                    }
                    string hashedPassword = HashPassword(password);
                    using (SqlCommand command = new SqlCommand("SELECT role FROM Users WHERE Name = @Name AND Password = @Password", connection))
                    {
                        command.Parameters.AddWithValue("@Name", username);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        string role = (string)command.ExecuteScalar();
                        if (string.IsNullOrEmpty(role))
                        {
                            MessageBox.Show("Spatnos jmenos nebos heslos");
                            return;
                        }
                        if (role.Equals("Admin"))
                        {
                            this.Hide();
                            Adminos Adminos = new Adminos();
                            Adminos.FormClosed += (s, args) => this.Close();
                            Adminos.Show();
                        }
                        else if (role.Equals("User"))
                        {
                            this.Hide();
                            Pracant Pracant = new Pracant(username);
                            Pracant.FormClosed += (s, args) => this.Close();
                            Pracant.Owner = this;
                            Pracant.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

