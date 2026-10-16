using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormConfigLimites : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        public FormConfigLimites()
        {
            InitializeComponent();
            CargarValores();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void CargarValores()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Clave, Valor FROM ConfiguracionSistema WHERE Clave IN ('MinutosAntesClase','MinutosToleranciaTardanza','MinutosParaFalta')";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            string clave = r["Clave"].ToString();
                            int valor = Convert.ToInt32(r["Valor"]);
                            switch (clave)
                            {
                                case "MinutosAntesClase": nudAntesClase.Value = valor; break;
                                case "MinutosToleranciaTardanza": nudToleranciaTardanza.Value = valor; break;
                                case "MinutosParaFalta": nudFalta.Value = valor; break;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Si la tabla no existe aún, se usan los valores default
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Crear tabla si no existe
                    string crearTabla = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ConfiguracionSistema' AND xtype='U')
                        CREATE TABLE ConfiguracionSistema (Clave NVARCHAR(100) PRIMARY KEY, Valor INT NOT NULL, Descripcion NVARCHAR(255))";
                    using (SqlCommand cmd = new SqlCommand(crearTabla, con))
                        cmd.ExecuteNonQuery();

                    // Upsert de cada valor
                    UpsertConfig(con, "MinutosAntesClase", (int)nudAntesClase.Value,
                        "Minutos antes del inicio de clase en que se permite registrar asistencia");
                    UpsertConfig(con, "MinutosToleranciaTardanza", (int)nudToleranciaTardanza.Value,
                        "Minutos después del inicio antes de considerar tardanza");
                    UpsertConfig(con, "MinutosParaFalta", (int)nudFalta.Value,
                        "Minutos después del inicio para registrar como falta");
                }

                FormAviso.Mostrar("Configuración guardada",
                    $"Anticipación: {nudAntesClase.Value} min  |  Tardanza: {nudToleranciaTardanza.Value} min  |  Falta: {nudFalta.Value} min",
                    FormAviso.TipoAviso.Exito, 5);
                this.Close();
            }
            catch (Exception ex)
            {
                FormAviso.Mostrar("Error al guardar", ex.Message, FormAviso.TipoAviso.Error, 7);
            }
        }

        private void UpsertConfig(SqlConnection con, string clave, int valor, string descripcion)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM ConfiguracionSistema WHERE Clave = @Clave)
                               UPDATE ConfiguracionSistema SET Valor = @Valor WHERE Clave = @Clave
                           ELSE
                               INSERT INTO ConfiguracionSistema (Clave, Valor, Descripcion) VALUES (@Clave, @Valor, @Desc)";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@Clave", clave);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@Desc", descripcion);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
