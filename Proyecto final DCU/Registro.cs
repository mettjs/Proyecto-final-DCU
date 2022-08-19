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
    public partial class Registro : Form
    {
        private SqlConnection conexion = new SqlConnection("Server=DESKTOP-BMGIGR3;Database=TwitchSecurity;Trusted_Connection=True;");
        public Registro()
        {
            InitializeComponent();
            conexion.Open();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string telefono = textBox3.Text + "-" + textBox4.Text + "-" + textBox5.Text;
            string query = "INSERT INTO Usuarios (Nombre, Correo, Telefono, Imagen) VALUES (@NOMBRE, @CORREO, @TELEFONO, @IMAGEN)";
            try 
            {
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@NOMBRE", textBox1.Text);
            comando.Parameters.AddWithValue("@CORREO", textBox2.Text);
            comando.Parameters.AddWithValue("@TELEFONO", telefono);
            comando.Parameters.AddWithValue("@IMAGEN", ConvertirImg());
            comando.ExecuteNonQuery();
            MessageBox.Show("Usuario registrado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Hide();
        }
        private byte[] ConvertirImg()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.GetBuffer();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog b = new OpenFileDialog();
            if (b.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(b.FileName);
            }
        }
    }
}
