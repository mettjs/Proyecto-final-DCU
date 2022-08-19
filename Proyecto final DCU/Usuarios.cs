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

namespace Proyecto_final_DCU
{
    public partial class Usuarios : Form
    {
        private SqlConnection conexion = new SqlConnection("Server=DESKTOP-BMGIGR3;Database=TwitchSecurity;Trusted_Connection=True;");
        public Usuarios()
        {
            InitializeComponent();
            conexion.Open();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Registro u = new Registro();
            u.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "DELETE FROM Usuarios WHERE ID = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", Convert.ToInt32(textBox1.Text));
                comando.ExecuteNonQuery();
                MessageBox.Show("Usuario eliminado correctamente");
                textBox1.Text = "";
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == string.Empty)
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuarios", conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
            }
            else
            {
                try
                {
                    SqlCommand comando = new SqlCommand("SP_MOSTRAR", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@BUSCAR", Convert.ToInt32(textBox1.Text));
                    SqlDataAdapter adaptador = new SqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;
                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }   
        }
    }
}
