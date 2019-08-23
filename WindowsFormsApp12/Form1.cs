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

namespace BaseDeDatos
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private SqlConnection conexion;

        private SqlDataAdapter adaptador;

        private DataSet datos;

        private void Form1_Load(object sender, EventArgs e)
        {
            conexion = new SqlConnection("Data Source=DESKTOP-N6FU7L1\\SQLEXPRESS;Initial Catalog=tp2;Integrated Security=True"); //AQUÍ VA EL STRING DE CONEXIÓN DE SU PC 
            adaptador = new SqlDataAdapter();

            SqlCommand alta = new SqlCommand("insert into usuarios (nombre, clave) values (@nombre, @clave)", conexion); 
            
             adaptador.InsertCommand = alta;
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@clave", SqlDbType.VarChar));

            SqlCommand baja = new SqlCommand("delete from usuarios where nombre=@nombre", conexion);
            adaptador.DeleteCommand = baja;
            adaptador.DeleteCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));

            SqlCommand modificacion = new SqlCommand("update usuarios set nombre=@nombre, clave = @clave where nombre = @nombreant", conexion); 
            
            adaptador.UpdateCommand = modificacion;
            adaptador.UpdateCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new SqlParameter("@clave", SqlDbType.VarChar));
            adaptador.UpdateCommand.Parameters.Add(new SqlParameter("@nombreant",SqlDbType.VarChar));
            SqlCommand consulta = new SqlCommand("select nombre,clave from usuarios", conexion);
            adaptador.SelectCommand = consulta;
            datos = new DataSet();
            conexion.Open();
            adaptador.Fill(datos, "usuarios");
            conexion.Close();
          //  grillaUsuarios.DataSource = datos;
           // grillaUsuarios.DataMember = "usuarios";
        }

        private void ActualizarDatos()
        {
            datos.Clear();
            adaptador.Fill(datos, "usuarios");
        }
        private void btnAlta_Click(object sender, EventArgs e)
        {
            adaptador.InsertCommand.Parameters["@nombre"].Value = txtNombre.Text;
            adaptador.InsertCommand.Parameters["@clave"].Value = txtClave.Text;

            try
            {
                conexion.Open();
                adaptador.InsertCommand.ExecuteNonQuery();
                ActualizarDatos();
            }

            catch (SqlException excepcion)
            {

                MessageBox.Show(excepcion.ToString());
            }

            finally
            {
                conexion.Close();
            }
        }
        private void btnBaja_Click(object sender, EventArgs e)
        {
            adaptador.DeleteCommand.Parameters["@nombre"].Value = txtNombreBaja.Text;

            try
            {
                conexion.Open();

                int cantidad = adaptador.DeleteCommand.ExecuteNonQuery();

                if (cantidad == 0)
                {

                    MessageBox.Show("No existe");
                }
                ActualizarDatos();
            }

            catch (SqlException excepcion)
            {

                MessageBox.Show(excepcion.ToString());
            }

            finally
            {
                conexion.Close();
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            adaptador.UpdateCommand.Parameters["@nombreant"].Value = txtNombreActual.Text;
            adaptador.UpdateCommand.Parameters["@nombre"].Value = txtNuevoNombre.Text;
            adaptador.UpdateCommand.Parameters["@clave"].Value = txtNuevaClave.Text;

            try
            {
                conexion.Open();
                adaptador.UpdateCommand.ExecuteNonQuery();
                ActualizarDatos();
            }

            catch (SqlException excepcion)
            {

                MessageBox.Show(excepcion.ToString());
            }

            finally
            {
                conexion.Close();
            }
        }
    }
}



