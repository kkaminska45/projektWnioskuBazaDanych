using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektWnioskuBazaDanych
{
    public partial class Form2 : Form
    {
        private string connectionString = @"Data Source=Wnioski.db;Version=3;";

        public Form2()
        {
            InitializeComponent();
            WczytajDane();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void WczytajDane()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Wnioski";

                    using (var command = new SQLiteCommand(query, connection))
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd przy odczycie danych: " + ex.Message);
                }
            }
        }
    }
}
