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

namespace ProjectSedlakJ
{
    
    public partial class Pracant : Form
    {
        private string username;
        private string connectionString;
        public Pracant(string username)
        {
            InitializeComponent();
            this.username = username;
            label6.Text = this.username;
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True"; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string customer = textBox2.Text;
            int hours;
            if (!int.TryParse(textBox3.Text, out hours))
            {
                MessageBox.Show("Spatnos hodinos!");
                return;
            }
            string workType = textBox4.Text;
            string IDText = textBox1.Text;
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
                command.Parameters.AddWithValue("@Hours", hours);
                command.Parameters.AddWithValue("@WorkType", workType);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@DateAdded", DateTime.Now.Date);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Pridanos probehlos!");
                }
                else
                {
                    MessageBox.Show("Pridanos neprobehlos!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int recordId;
            if (!int.TryParse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(), out recordId))
            {
                MessageBox.Show("Spatnos IDos!");
                return;
            }
            DateTime dateAdded;
            if (!DateTime.TryParse(dataGridView1.Rows[rowIndex].Cells[5].Value.ToString(), out dateAdded))
            {
                MessageBox.Show("Spatnos datos!");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Records WHERE Id = @Id AND Username = @Username AND DateAdded = @DateAdded", connection);
                command.Parameters.AddWithValue("@Id", recordId);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@DateAdded", dateAdded);

                int count = (int)command.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mohos odebranos jenomos to cosos dneskos nadelalos");
                    return;
                }

                command = new SqlCommand("DELETE FROM Records WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", recordId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Odebranos probehlos!");
                    ListRecords();
                }
                else
                {
                    MessageBox.Show("Odebranos neprobehlos!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListRecords();
        }

        private void ListRecords()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Records WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Refresh();
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Loginos Loginos = new Loginos();
            Loginos.FormClosed += (s, args) => this.Close();
            Loginos.Show();
        }
    }
}
