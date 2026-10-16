using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public class FormEditarHorario : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
        private int horarioId;

        private ComboBox cmbDocente;
        private ComboBox cmbCurso;
        private ComboBox cmbDiaSemana;
        private ComboBox cmbTipoClase;
        private DateTimePicker dtpHoraInicio;
        private DateTimePicker dtpHoraFin;
        private Button btnGuardar;
        private Button btnCancelar;

        private DataTable dtDocentes = new DataTable();
        private DataTable dtCursos = new DataTable();

        public FormEditarHorario(int id = 0, int docenteId = 0, int cursoId = 0, int diaSemana = 0, string horaInicio = "", string horaFin = "", string tipoClase = "TEORÍA")
        {
            this.horarioId = id;
            this.Text = id == 0 ? "Agregar Horario" : "Editar Horario";
            this.Size = new Size(480, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9.5F);

            int lblLeft = 25;
            int ctrlLeft = 150;
            int ctrlWidth = 280;
            int yPos = 25;
            int step = 42;

            // Docente
            this.Controls.Add(new Label() { Text = "Docente:", Left = lblLeft, Top = yPos + 3, Width = 120, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            cmbDocente = new ComboBox() { Left = ctrlLeft, Top = yPos, Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDown };
            this.Controls.Add(cmbDocente);
            yPos += step;

            // Curso
            this.Controls.Add(new Label() { Text = "Curso:", Left = lblLeft, Top = yPos + 3, Width = 120, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            cmbCurso = new ComboBox() { Left = ctrlLeft, Top = yPos, Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDown };
            this.Controls.Add(cmbCurso);
            yPos += step;

            // Día de la semana
            this.Controls.Add(new Label() { Text = "Día:", Left = lblLeft, Top = yPos + 3, Width = 120, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            cmbDiaSemana = new ComboBox() { Left = ctrlLeft, Top = yPos, Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbDiaSemana.Items.AddRange(new object[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" });
            this.Controls.Add(cmbDiaSemana);
            yPos += step;

            // Tipo Clase (Teoría/Práctica)
            this.Controls.Add(new Label() { Text = "Tipo Clase:", Left = lblLeft, Top = yPos + 3, Width = 120, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            cmbTipoClase = new ComboBox() { Left = ctrlLeft, Top = yPos, Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTipoClase.Items.AddRange(new object[] { "TEORÍA", "PRÁCTICA" });
            this.Controls.Add(cmbTipoClase);
            yPos += step;

            // Hora inicio
            this.Controls.Add(new Label() { Text = "Hora Inicio:", Left = lblLeft, Top = yPos + 3, Width = 120, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            dtpHoraInicio = new DateTimePicker() { Left = ctrlLeft, Top = yPos, Width = ctrlWidth, Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm", ShowUpDown = true };
            this.Controls.Add(dtpHoraInicio);
            yPos += step;

            // Hora fin
            this.Controls.Add(new Label() { Text = "Hora Fin:", Left = lblLeft, Top = yPos + 3, Width = 120, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            dtpHoraFin = new DateTimePicker() { Left = ctrlLeft, Top = yPos, Width = ctrlWidth, Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm", ShowUpDown = true };
            this.Controls.Add(dtpHoraFin);
            yPos += step + 15;

            // Botones
            btnGuardar = new Button() { Text = "Guardar", Left = ctrlLeft, Top = yPos, Width = 120, Height = 38, BackColor = Color.LimeGreen, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            btnCancelar = new Button() { Text = "Cancelar", Left = ctrlLeft + 135, Top = yPos, Width = 120, Height = 38, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnCancelar);

            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;

            CargarDocentes();
            CargarCursos();

            // Pre-llenar si es edición
            if (id > 0)
            {
                SeleccionarComboValue(cmbDocente, docenteId);
                SeleccionarComboValue(cmbCurso, cursoId);
                if (diaSemana >= 1 && diaSemana <= 6) cmbDiaSemana.SelectedIndex = diaSemana - 1;
                if (cmbTipoClase != null && !string.IsNullOrEmpty(tipoClase))
                    cmbTipoClase.SelectedItem = tipoClase.ToUpper();
                if (!string.IsNullOrEmpty(horaInicio) && TimeSpan.TryParse(horaInicio, out TimeSpan hi))
                    dtpHoraInicio.Value = DateTime.Today.Add(hi);
                if (!string.IsNullOrEmpty(horaFin) && TimeSpan.TryParse(horaFin, out TimeSpan hf))
                    dtpHoraFin.Value = DateTime.Today.Add(hf);
            }
            else
            {
                // Defaults para nuevo horario
                dtpHoraInicio.Value = DateTime.Today.AddHours(8);
                dtpHoraFin.Value = DateTime.Today.AddHours(10);
                if (cmbDiaSemana.Items.Count > 0) cmbDiaSemana.SelectedIndex = 0;
                if (cmbTipoClase != null) cmbTipoClase.SelectedIndex = 0;
            }

            // Autocompletar docentes
            ConfigurarAutocompletado(cmbDocente, dtDocentes, "Display");
            ConfigurarAutocompletado(cmbCurso, dtCursos, "Display");
        }

        private void CargarDocentes()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Codigo + ' - ' + Nombre + ' ' + Apellido as Display FROM Docentes ORDER BY Nombre ASC", con);
                    da.Fill(dtDocentes);
                    cmbDocente.DataSource = dtDocentes;
                    cmbDocente.DisplayMember = "Display";
                    cmbDocente.ValueMember = "Id";
                    cmbDocente.SelectedIndex = -1;
                }
                catch { }
            }
        }

        private void CargarCursos()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Id, ISNULL(Codigo, '') + ' - ' + Nombre as Display FROM Cursos ORDER BY Nombre ASC", con);
                    da.Fill(dtCursos);
                    cmbCurso.DataSource = dtCursos;
                    cmbCurso.DisplayMember = "Display";
                    cmbCurso.ValueMember = "Id";
                    cmbCurso.SelectedIndex = -1;
                }
                catch { }
            }
        }

        private void ConfigurarAutocompletado(ComboBox cmb, DataTable dt, string displayCol)
        {
            cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void SeleccionarComboValue(ComboBox cmb, int value)
        {
            try
            {
                cmb.SelectedValue = value;
            }
            catch { }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (cmbDocente.SelectedValue == null || cmbDocente.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un docente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbCurso.SelectedValue == null || cmbCurso.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un curso.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbDiaSemana.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un día de la semana.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int docenteId = Convert.ToInt32(cmbDocente.SelectedValue);
            int cursoId = Convert.ToInt32(cmbCurso.SelectedValue);
            int diaSemana = cmbDiaSemana.SelectedIndex + 1; // 1=Lunes ... 6=Sábado
            string tipoClase = cmbTipoClase.SelectedItem?.ToString() ?? "TEORÍA";
            TimeSpan horaInicio = dtpHoraInicio.Value.TimeOfDay;
            TimeSpan horaFin = dtpHoraFin.Value.TimeOfDay;

            if (horaFin <= horaInicio)
            {
                MessageBox.Show("La hora de fin debe ser mayor que la hora de inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Obtener periodo activo
                    int? periodoId = null;
                    using (SqlCommand cmdP = new SqlCommand("SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1", con))
                    {
                        object r = cmdP.ExecuteScalar();
                        if (r != null) periodoId = Convert.ToInt32(r);
                    }

                    if (horarioId == 0)
                    {
                        // Verificar conflicto
                        string queryConflicto = @"SELECT COUNT(*) FROM HorarioClases 
                            WHERE DocenteId = @DocenteId AND DiaSemana = @Dia AND PeriodoId = @PeriodoId
                            AND ((@HoraInicio >= HoraInicio AND @HoraInicio < HoraFin) OR (@HoraFin > HoraInicio AND @HoraFin <= HoraFin) OR (@HoraInicio <= HoraInicio AND @HoraFin >= HoraFin))";
                        using (SqlCommand cmdConf = new SqlCommand(queryConflicto, con))
                        {
                            cmdConf.Parameters.AddWithValue("@DocenteId", docenteId);
                            cmdConf.Parameters.AddWithValue("@Dia", diaSemana);
                            cmdConf.Parameters.AddWithValue("@PeriodoId", periodoId.HasValue ? (object)periodoId.Value : DBNull.Value);
                            cmdConf.Parameters.AddWithValue("@HoraInicio", horaInicio);
                            cmdConf.Parameters.AddWithValue("@HoraFin", horaFin);
                            int conflictos = Convert.ToInt32(cmdConf.ExecuteScalar());
                            if (conflictos > 0)
                            {
                                MessageBox.Show("El docente ya tiene un horario que se superpone en ese día y hora.", "Conflicto de Horario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Insertar
                        string queryInsert = @"INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId, TipoClase) 
                                               VALUES (@DocenteId, @CursoId, @Dia, @HoraInicio, @HoraFin, @PeriodoId, @TipoClase)";
                        using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                        {
                            cmd.Parameters.AddWithValue("@DocenteId", docenteId);
                            cmd.Parameters.AddWithValue("@CursoId", cursoId);
                            cmd.Parameters.AddWithValue("@Dia", diaSemana);
                            cmd.Parameters.AddWithValue("@HoraInicio", horaInicio);
                            cmd.Parameters.AddWithValue("@HoraFin", horaFin);
                            cmd.Parameters.AddWithValue("@PeriodoId", periodoId.HasValue ? (object)periodoId.Value : DBNull.Value);
                            cmd.Parameters.AddWithValue("@TipoClase", tipoClase);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Verificar conflicto (excluyendo el actual)
                        string queryConflicto = @"SELECT COUNT(*) FROM HorarioClases 
                            WHERE DocenteId = @DocenteId AND DiaSemana = @Dia AND PeriodoId = @PeriodoId AND Id <> @Id
                            AND ((@HoraInicio >= HoraInicio AND @HoraInicio < HoraFin) OR (@HoraFin > HoraInicio AND @HoraFin <= HoraFin) OR (@HoraInicio <= HoraInicio AND @HoraFin >= HoraFin))";
                        using (SqlCommand cmdConf = new SqlCommand(queryConflicto, con))
                        {
                            cmdConf.Parameters.AddWithValue("@DocenteId", docenteId);
                            cmdConf.Parameters.AddWithValue("@Dia", diaSemana);
                            cmdConf.Parameters.AddWithValue("@PeriodoId", periodoId.HasValue ? (object)periodoId.Value : DBNull.Value);
                            cmdConf.Parameters.AddWithValue("@HoraInicio", horaInicio);
                            cmdConf.Parameters.AddWithValue("@HoraFin", horaFin);
                            cmdConf.Parameters.AddWithValue("@Id", horarioId);
                            int conflictos = Convert.ToInt32(cmdConf.ExecuteScalar());
                            if (conflictos > 0)
                            {
                                MessageBox.Show("El docente ya tiene un horario que se superpone en ese día y hora.", "Conflicto de Horario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Actualizar
                        string queryUpdate = @"UPDATE HorarioClases SET DocenteId = @DocenteId, CursoId = @CursoId, DiaSemana = @Dia, 
                                               HoraInicio = @HoraInicio, HoraFin = @HoraFin, TipoClase = @TipoClase WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                        {
                            cmd.Parameters.AddWithValue("@DocenteId", docenteId);
                            cmd.Parameters.AddWithValue("@CursoId", cursoId);
                            cmd.Parameters.AddWithValue("@Dia", diaSemana);
                            cmd.Parameters.AddWithValue("@HoraInicio", horaInicio);
                            cmd.Parameters.AddWithValue("@HoraFin", horaFin);
                            cmd.Parameters.AddWithValue("@TipoClase", tipoClase);
                            cmd.Parameters.AddWithValue("@Id", horarioId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormEditarHorario
            // 
            this.ClientSize = new System.Drawing.Size(407, 408);
            this.Name = "FormEditarHorario";
            this.ResumeLayout(false);

        }
    }
}
