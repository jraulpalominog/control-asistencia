using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class FormGestionPeriodos : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        public FormGestionPeriodos()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Nombre, EsActivo as 'Activo' FROM Periodos ORDER BY Id DESC", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                    if(dgv.Columns.Contains("Id")) dgv.Columns["Id"].Visible = false;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) return;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Periodos (Nombre, EsActivo) VALUES (@N, 0)", con);
                    cmd.Parameters.AddWithValue("@N", txtNombre.Text.Trim().ToUpper());
                    cmd.ExecuteNonQuery();
                    txtNombre.Text = "";
                    CargarDatos();
                }
                catch (Exception ex) { MessageBox.Show("Error (probablemente el nombre ya existe):\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null) return;
            int id = Convert.ToInt32(dgv.CurrentRow.Cells["Id"].Value);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    new SqlCommand("UPDATE Periodos SET EsActivo = 0", con).ExecuteNonQuery();
                    new SqlCommand($"UPDATE Periodos SET EsActivo = 1 WHERE Id = {id}", con).ExecuteNonQuery();
                    CargarDatos();
                    MessageBox.Show("Periodo activo actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null) return;
            int id = Convert.ToInt32(dgv.CurrentRow.Cells["Id"].Value);
            string nombre = dgv.CurrentRow.Cells["Nombre"].Value.ToString();
            if (MessageBox.Show($"¿Seguro que deseas eliminar el periodo '{nombre}'?\n(Sus asistencias vinculadas quedarán como Histórico sin periodo)", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        new SqlCommand($"UPDATE RegistroAsistencia SET PeriodoId = NULL WHERE PeriodoId = {id}", con).ExecuteNonQuery();
                        new SqlCommand($"DELETE FROM Periodos WHERE Id = {id}", con).ExecuteNonQuery();
                        CargarDatos();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
    }
}
