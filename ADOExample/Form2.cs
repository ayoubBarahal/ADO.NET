using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADOExample
{
    public partial class Form2 : Form
    {

        private String connectionString = "Server=localhost;Database=GestionCommande;User ID=MyUser;Password=1234 ";


        public Form2()
        {
            InitializeComponent();
            Dataload();
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

        public void AddClient()
        {
            int numCL =int.Parse(textBox1.Text) ;
            String NomCl = textBox2.Text;
            String PrenomCl = textBox3.Text;
            String AdresseCl = textBox4.Text;
            String TeleCl = textBox5.Text;

            String sqli = "insert into Client values(@numCL,@NomCl,@PrenomCl,@AdresseCl,@TeleCl) ";
            MySqlConnection conn = ConnectionDB();
            MySqlCommand command = new MySqlCommand(sqli, conn);

            command.Parameters.AddWithValue("@numCL",numCL);
            command.Parameters.AddWithValue("@NomCl",NomCl);
            command.Parameters.AddWithValue("@PrenomCl",PrenomCl);
            command.Parameters.AddWithValue("@AdresseCl",AdresseCl);
            command.Parameters.AddWithValue("@TeleCl", TeleCl);

            try
            {
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Le Client est ajouté avec succés");
                    Dataload();
                }
                else
                    MessageBox.Show("Failed to ADD Client ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateClient()
        {
            int numCL = int.Parse(textBox1.Text);
            String NomCl = textBox2.Text;
            String PrenomCl = textBox3.Text;
            String AdresseCl = textBox4.Text;
            String TeleCl = textBox5.Text;

            String sqli = "update  Client set  Nomcl=@NomCl , Prenomcl=@PrenomCl , Adressecl=@AdresseCl ,Telcl=@TeleCl where Numcl=@numCL";
            MySqlConnection conn = ConnectionDB();
            MySqlCommand command = new MySqlCommand(sqli, conn);

            command.Parameters.AddWithValue("@numCL", numCL);
            command.Parameters.AddWithValue("@NomCl", NomCl);
            command.Parameters.AddWithValue("@PrenomCl", PrenomCl);
            command.Parameters.AddWithValue("@AdresseCl", AdresseCl);
            command.Parameters.AddWithValue("@TeleCl", TeleCl);

            try
            {
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Client Updated Successfully");
                    Dataload();
                }
                else
                    MessageBox.Show("Failed to Update Client ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void DeleteClient()
        {
            int numCL = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);


            String sqli = "delete from Client where Numcl=@Numcl";
            MySqlConnection conn = ConnectionDB();
            MySqlCommand command = new MySqlCommand(sqli, conn);

            command.Parameters.AddWithValue("@Numcl", numCL);


          
            try
            {
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Client Deleted Successfully");
                    Dataload();
                }
                else
                    MessageBox.Show("Failed to Delete Client ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddClient();
            this.Refresh();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Dataload();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateClient();
            this.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteClient();

        }
    }
}
