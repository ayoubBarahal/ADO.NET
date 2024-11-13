using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace ADOExample
{
    public partial class Form1 : Form
    {


        private String connectionString = "Server=localhost;Database=GestionCommande;User ID=MyUser;Password=1234 ";


        public Form1()
        {
            InitializeComponent();
            Dataload();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private MySqlConnection ConnectionDB()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {

                connection.Open();
                //MessageBox.Show("Connected Successefully");
                return connection;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Failed to Connect ");
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectionDB();
        }
        public void Dataload()
        {
            String sql = "SELECT * FROM client";
            MySqlConnection connection = ConnectionDB(); 
            if (connection != null) 
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    dataAdapter.Fill(dataTable); 
                    dataGridView1.DataSource = dataTable; 
                    Console.WriteLine("Data loaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message); 
                }
                finally
                {
                    connection.Close(); 
                }
            }
            else
            {
                MessageBox.Show("Failed to connect to the database.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Dataload();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();

        }
    }
}
