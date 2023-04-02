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

namespace ProjectSedlakJ
{
    public partial class Zamestnanos : Form
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Zamestnanos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employee (PersNum, FirstName, LastName, BirthDate, Email, PhoneNumber) VALUES (@PersNum, @FirstName, @LastName, @BirthDate, @Email, @PhoneNumber)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", textBox1.Text);
                    command.Parameters.AddWithValue("@LastName", textBox2.Text);
                    command.Parameters.AddWithValue("@PersNum", textBox3.Text);                  
                    command.Parameters.AddWithValue("@BirthDate", textBox4.Text);
                    command.Parameters.AddWithValue("@Email", textBox5.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", textBox6.Text);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    if (result > 0)
                    {
                        MessageBox.Show("Zamestnanos bylos pridanos!");
                    }
                    else
                    {
                        MessageBox.Show("Necos sos rozbylos.");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Employee WHERE PersNum=@PersNum";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersNum", textBox3.Text);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    if (result > 0)
                    {
                        MessageBox.Show("Zamestnanos bylos odebranos!");
                    }
                    else
                    {
                        MessageBox.Show("Necos sos rozbylos.");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employee SET FirstName=@FirstName, LastName=@LastName, BirthDate=@BirthDate, Email=@Email, PhoneNumber=@PhoneNumber WHERE PersNum=@PersNum";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", textBox1.Text);
                    command.Parameters.AddWithValue("@LastName", textBox2.Text);
                    command.Parameters.AddWithValue("@PersNum", textBox3.Text);                   
                    command.Parameters.AddWithValue("@BirthDate", textBox4.Text);
                    command.Parameters.AddWithValue("@Email", textBox5.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", textBox6.Text);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    if (result > 0)
                    {
                        MessageBox.Show("Zamestnanos bylos zmenenos!");
                    }
                    else
                    {
                        MessageBox.Show("Necos sos rozbylos.");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employee";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
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
