using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public class FormEditarAsistencia : Form
    {
        private int idRegistro = 0;
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        private ComboBox cmbDocente;
        private ComboBox cmbCurso;
        private DateTimePicker dtpIngreso;
        private DateTimePicker dtpSalida;
        private CheckBox chkSinSalida;
        private Button btnGuardar;
        private Button btnCancelar;

        public FormEditarAsistencia(int id = 0)
        {
            this.idRegistro = id;
            InitializeComponent();
            ConfigurarUI();
            CargarCombos();
            if (idRegistro > 0)
            {
                this.Text = "Editar Registro de Asistencia";
                CargarDatos();
            }
            else
            {
                this.Text = "Añadir Registro de Asistencia";
            }
        }

        private void InitializeComponent()
        {
            this.cmbDocente = new ComboBox();
            this.cmbCurso = new ComboBox();
            this.dtpIngreso = new DateTimePicker();
            this.dtpSalida = new DateTimePicker();
            this.chkSinSalida = new CheckBox();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();
            
            this.SuspendLayout();

            int left = 30, top = 20, width = 220;

            this.Controls.Add(new Label() { Text = "Docente:", Left = left, Top = top, Width = width });
            cmbDocente.Left = left; cmbDocente.Top = top + 20; cmbDocente.Width = width;
            cmbDocente.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(cmbDocente);

            top += 60;
            this.Controls.Add(new Label() { Text = "Curso:", Left = left, Top = top, Width = width });
            cmbCurso.Left = left; cmbCurso.Top = top + 20; cmbCurso.Width = width;
            cmbCurso.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(cmbCurso);

            top += 60;
            this.Controls.Add(new Label() { Text = "Hora de Ingreso:", Left = left, Top = top, Width = width });
            dtpIngreso.Left = left; dtpIngreso.Top = top + 20; dtpIngreso.Width = width;
            dtpIngreso.Format = DateTimePickerFormat.Custom;
            dtpIngreso.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.Controls.Add(dtpIngreso);

            top += 60;
            this.Controls.Add(new Label() { Text = "Hora de Salida:", Left = left, Top = top, Width = width });
            dtpSalida.Left = left; dtpSalida.Top = top + 20; dtpSalida.Width = width;
            dtpSalida.Format = DateTimePickerFormat.Custom;
            dtpSalida.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.Controls.Add(dtpSalida);

            top += 50;
            chkSinSalida.Text = "Aún no marca salida";
            chkSinSalida.Left = left; chkSinSalida.Top = top; chkSinSalida.Width = width;
            chkSinSalida.CheckedChanged += (s, e) => { dtpSalida.Enabled = !chkSinSalida.Checked; };
            this.Controls.Add(chkSinSalida);

            top += 50;
            btnGuardar.Text = "Guardar";
            btnGuardar.Left = left; btnGuardar.Top = top; btnGuardar.Width = 100;
            btnGuardar.Click += BtnGuardar_Click;
            btnGuardar.BackColor = Color.FromArgb(43, 108, 176);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(btnGuardar);

            btnCancelar.Text = "Cancelar";
            btnCancelar.Left = left + 120; btnCancelar.Top = top; btnCancelar.Width = 100;
            btnCancelar.Click += (s, e) => this.Close();
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(btnCancelar);

            this.ClientSize = new Size(300, top + 60);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.ResumeLayout(false);
        }

        private void ConfigurarUI()
        {
            this.BackColor = Color.White;
        }

        private void CargarCombos()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter daDocentes = new SqlDataAdapter("SELECT Id, Nombre + ' ' + Apellido as Display FROM Docentes", con);
                    DataTable dtDoc = new DataTable(); daDocentes.Fill(dtDoc);
                    cmbDocente.DisplayMember = "Display"; cmbDocente.ValueMember = "Id"; cmbDocente.DataSource = dtDoc;

                    SqlDataAdapter daCursos = new SqlDataAdapter("SELECT Id, Nombre FROM Cursos", con);
                    DataTable dtCur = new DataTable(); daCursos.Fill(dtCur);
                    cmbCurso.DisplayMember = "Nombre"; cmbCurso.ValueMember = "Id"; cmbCurso.DataSource = dtCur;
                }
                catch { }
            }
        }

        private void CargarDatos()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM RegistroAsistencia WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", idRegistro);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cmbDocente.SelectedValue = reader["DocenteId"];
                            cmbCurso.SelectedValue = reader["CursoId"];
                            dtpIngreso.Value = Convert.ToDateTime(reader["FechaHora"]);
                            
                            if (reader["HoraSalida"] != DBNull.Value)
                            {
                                dtpSalida.Value = Convert.ToDateTime(reader["HoraSalida"]);
                                chkSinSalida.Checked = false;
                            }
                            else
                            {
                                chkSinSalida.Checked = true;
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbDocente.SelectedValue == null || cmbCurso.SelectedValue == null)
            {
                MessageBox.Show("Seleccione Docente y Curso.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!chkSinSalida.Checked && dtpSalida.Value <= dtpIngreso.Value)
            {
                MessageBox.Show("La hora de salida debe ser mayor a la hora de ingreso.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "";
                    if (idRegistro == 0)
                    {
                        query = @"INSERT INTO RegistroAsistencia (DocenteId, CursoId, FechaHora, HoraSalida, PeriodoId) 
                                  VALUES (@DocenteId, @CursoId, @FechaHora, @HoraSalida, (SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1))";
                    }
                    else
                    {
                        query = @"UPDATE RegistroAsistencia SET DocenteId=@DocenteId, CursoId=@CursoId, FechaHora=@FechaHora, HoraSalida=@HoraSalida WHERE Id=@Id";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@DocenteId", cmbDocente.SelectedValue);
                    cmd.Parameters.AddWithValue("@CursoId", cmbCurso.SelectedValue);
                    cmd.Parameters.AddWithValue("@FechaHora", dtpIngreso.Value);
                    
                    if (chkSinSalida.Checked)
                        cmd.Parameters.AddWithValue("@HoraSalida", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@HoraSalida", dtpSalida.Value);

                    if (idRegistro > 0) cmd.Parameters.AddWithValue("@Id", idRegistro);

                    cmd.ExecuteNonQuery();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
