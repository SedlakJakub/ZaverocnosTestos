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
    public partial class Contractos : Form
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Contractos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string customer = textBox2.Text;
            int hours;
            if (!int.TryParse(textBox5.Text, out hours))
            {
                MessageBox.Show("Spatnos hodinos!");
                return;
            }
            string workType = textBox3.Text;
            string IDText = textBox1.Text;
            string username = textBox4.Text;
            int ID;
            try
            {
                ID = Convert.ToInt32(IDText);
            }
            catch (FormatException)
            {
                MessageBox.Show("Spatnos IDos!");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Records (ID, Customer, Hours, WorkType, Username, DateAdded) VALUES (@ID, @Customer, @Hours, @WorkType, @Username, @DateAdded)", connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Customer", customer);              
                command.Parameters.AddWithValue("@WorkType", workType);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Hours", hours);
                command.Parameters.AddWithValue("@DateAdded", DateTime.Now.Date);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Pracos pridanos!");
                }
                else
                {
                    MessageBox.Show("Pracos nepridanos!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Spatnos IDos!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE Records where ID=@ID", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = int.Parse(textBox1.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Uspesnos odebranos!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string customer = textBox2.Text;
            int hours;
            if (!int.TryParse(textBox5.Text, out hours))
            {
                MessageBox.Show("Invalid value for Hours.");
                return;
            }
            string workType = textBox3.Text;
            string IDText = textBox1.Text;
            string username = textBox4.Text;
            int ID;
            try
            {
                ID = Convert.ToInt32(IDText);
            }
            catch (FormatException)
            {
                MessageBox.Show("Spatnos IDos");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Records set Customer=@Customer, Hours=@Hours, WorkType=@WorkType, Username=@Username WHERE ID =@ID", connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Customer", customer);               
                command.Parameters.AddWithValue("@WorkType", workType);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Hours", hours);
                command.Parameters.AddWithValue("@DateAdded", DateTime.Now.Date);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Editacos probehlos!");
                }
                else
                {
                    MessageBox.Show("Editacos neprobehlos!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from Records", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
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
