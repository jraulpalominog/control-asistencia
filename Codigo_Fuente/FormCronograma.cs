using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormCronograma : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        public FormCronograma()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void FormCronograma_Load(object sender, EventArgs e)
        {
            CargarFechaInicio();
            ActualizarFechaFin();
        }

        private void CargarFechaInicio()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT ValorTxt FROM ConfiguracionSistema WHERE Clave = 'InicioSemestre'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && DateTime.TryParse(result.ToString(), out DateTime fecha))
                        {
                            dtpInicioSemestre.Value = fecha;
                        }
                        else
                        {
                            dtpInicioSemestre.Value = DateTime.Today;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Si falla (por ej. si no existe la columna o la tabla), usamos la fecha de hoy
                    dtpInicioSemestre.Value = DateTime.Today;
                }
            }
        }

        private void dtpInicioSemestre_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaFin();
        }

        private void ActualizarFechaFin()
        {
            DateTime inicio = dtpInicioSemestre.Value.Date;
            int diff = (7 + (inicio.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime lunesSemana1 = inicio.AddDays(-diff);
            
            // 16 weeks * 7 days
            DateTime fin = lunesSemana1.AddDays(16 * 7).AddDays(-1); // Domingo de la semana 16
            
            lblFechaCierre.Text = "Fecha de cierre estimada (Fin Sem. 16): " + fin.ToString("dd/MM/yyyy");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    
                    // Ensure the table and column exist
                    string createTable = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ConfiguracionSistema' AND xtype='U')
                                           CREATE TABLE ConfiguracionSistema (Clave NVARCHAR(100) PRIMARY KEY, Valor INT NULL, Descripcion NVARCHAR(255), ValorTxt NVARCHAR(MAX) NULL);
                                           
                                           IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[ConfiguracionSistema]') AND name = 'ValorTxt')
                                           BEGIN
                                               ALTER TABLE [dbo].[ConfiguracionSistema] ADD [ValorTxt] NVARCHAR(MAX) NULL;
                                           END";
                    using (SqlCommand cmdCreate = new SqlCommand(createTable, con))
                    {
                        cmdCreate.ExecuteNonQuery();
                    }

                    string fechaStr = dtpInicioSemestre.Value.Date.ToString("yyyy-MM-dd");
                    
                    string sql = @"IF EXISTS (SELECT 1 FROM ConfiguracionSistema WHERE Clave = 'InicioSemestre')
                                       UPDATE ConfiguracionSistema SET ValorTxt = @Fecha WHERE Clave = 'InicioSemestre'
                                   ELSE
                                       INSERT INTO ConfiguracionSistema (Clave, ValorTxt, Descripcion) VALUES ('InicioSemestre', @Fecha, 'Día de inicio del semestre para reportes')";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Fecha", fechaStr);
                        cmd.ExecuteNonQuery();
                    }
                    
                    FormAviso.Mostrar("Éxito", "Cronograma del semestre guardado correctamente.", FormAviso.TipoAviso.Exito, 3);
                    this.Close();
                }
                catch (Exception ex)
                {
                    FormAviso.Mostrar("Error", "Ocurrió un error al guardar: " + ex.Message, FormAviso.TipoAviso.Error, 5);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
