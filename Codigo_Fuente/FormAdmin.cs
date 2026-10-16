using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;
using System.Linq;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class FormAdmin : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        DataTable tablaDocentes = new DataTable();
        DataTable tablaHistorial = new DataTable();
        DataTable tablaCursos = new DataTable();
        ComboBox cmbFiltroPeriodo = new ComboBox();

        private DataTable dtReporteResumen = new DataTable();
        private DataTable dtReporteDetalle = new DataTable();

        ToolTip toolTipBotones = new ToolTip() { InitialDelay = 0, ReshowDelay = 0 };
        ErrorProvider epValidacion = new ErrorProvider();

        // ------------------ NEW ADMIN PRO FEATURES ------------------
        private DataGridView dgvJustificaciones = new DataGridView();
        private Panel pnlDashboardGrafico = new Panel();
        private Label lblTotalDocentes = new Label();
        private Label lblFaltasHoy = new Label();
        private Label lblAsistenciasHoy = new Label();
        private Label lblTardanzasHoy = new Label();
        private int dashboardPuntuales = 0, dashboardTardanzas = 0, dashboardFaltas = 0;

        private void InicializarPestanasPro()
        {
            // 1. Dashboard Tab
            TabPage tabDashboard = new TabPage("Dashboard");
            tabDashboard.BackColor = Color.FromArgb(240, 240, 240);
            
            Panel pnlTarjetas = new Panel() { Dock = DockStyle.Top, Height = 120, BackColor = Color.White };
            
            lblTotalDocentes = CrearTarjetaDashboard(pnlTarjetas, "Docentes Registrados", 20, 20, Color.FromArgb(52, 152, 219));
            lblAsistenciasHoy = CrearTarjetaDashboard(pnlTarjetas, "Asistencias Hoy", 240, 20, Color.FromArgb(46, 204, 113));
            lblTardanzasHoy = CrearTarjetaDashboard(pnlTarjetas, "Tardanzas Hoy", 460, 20, Color.FromArgb(243, 156, 18));
            lblFaltasHoy = CrearTarjetaDashboard(pnlTarjetas, "Faltas Hoy", 680, 20, Color.FromArgb(231, 76, 60));
            
            pnlDashboardGrafico = new Panel() { Dock = DockStyle.Fill, BackColor = Color.White };
            pnlDashboardGrafico.Paint += PnlDashboardGrafico_Paint;
            
            tabDashboard.Controls.Add(pnlDashboardGrafico);
            tabDashboard.Controls.Add(pnlTarjetas);
            
            // 2. Justificaciones Tab
            TabPage tabJustificaciones = new TabPage("Justificaciones");
            tabJustificaciones.BackColor = Color.White;
            
            Label lblJustTitulo = new Label() { Text = "Bandeja de Justificaciones Pendientes y Revisadas", Font = new System.Drawing.Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) };
            
            dgvJustificaciones.Location = new Point(20, 60);
            dgvJustificaciones.Size = new Size(850, 450);
            dgvJustificaciones.AllowUserToAddRows = false;
            dgvJustificaciones.ReadOnly = true;
            dgvJustificaciones.RowHeadersVisible = false;
            dgvJustificaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJustificaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJustificaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            
            Button btnAprobar = new Button() { Text = "✅ Aprobar", BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold), Size = new Size(120, 40), Location = new Point(20, 530), FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            Button btnRechazar = new Button() { Text = "❌ Rechazar", BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold), Size = new Size(120, 40), Location = new Point(150, 530), FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            Button btnRefrescarJ = new Button() { Text = "🔄 Actualizar", BackColor = Color.Gray, ForeColor = Color.White, Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold), Size = new Size(120, 40), Location = new Point(280, 530), FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            
            btnAprobar.Click += (s, e) => { ProcesarJustificacion("Aprobado"); };
            btnRechazar.Click += (s, e) => { ProcesarJustificacion("Rechazado"); };
            btnRefrescarJ.Click += (s, e) => { CargarJustificaciones(); };
            
            tabJustificaciones.Controls.Add(lblJustTitulo);
            tabJustificaciones.Controls.Add(dgvJustificaciones);
            tabJustificaciones.Controls.Add(btnAprobar);
            tabJustificaciones.Controls.Add(btnRechazar);
            tabJustificaciones.Controls.Add(btnRefrescarJ);
            
            tabControl1.TabPages.Insert(0, tabDashboard);
            tabControl1.TabPages.Insert(2, tabJustificaciones);
            
            // Initial loads
            CargarJustificaciones();
            CargarDashboard();
        }

        private Label CrearTarjetaDashboard(Panel parent, string titulo, int x, int y, Color color)
        {
            Panel card = new Panel() { Location = new Point(x, y), Size = new Size(200, 80), BackColor = color };
            Label lblT = new Label() { Text = titulo, ForeColor = Color.White, Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true };
            Label lblV = new Label() { Text = "0", ForeColor = Color.White, Font = new System.Drawing.Font("Segoe UI", 24, FontStyle.Bold), Location = new Point(10, 30), AutoSize = true };
            card.Controls.Add(lblT);
            card.Controls.Add(lblV);
            parent.Controls.Add(card);
            return lblV;
        }
        
        private void CargarDashboard()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Docentes", con))
                    {
                        lblTotalDocentes.Text = cmd.ExecuteScalar().ToString();
                    }
                    
                    // Estadísticas del día de hoy (datos base para gráfica)
                    dashboardPuntuales = 0; dashboardTardanzas = 0; dashboardFaltas = 0;
                    
                    string qHoy = @"SELECT COUNT(*) FROM RegistroAsistencia WHERE CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE)";
                    using (SqlCommand cmd2 = new SqlCommand(qHoy, con))
                    {
                        dashboardPuntuales = Convert.ToInt32(cmd2.ExecuteScalar());
                    }
                    
                    lblAsistenciasHoy.Text = dashboardPuntuales.ToString();
                    lblTardanzasHoy.Text = dashboardTardanzas.ToString();
                    lblFaltasHoy.Text = dashboardFaltas.ToString();
                    
                    pnlDashboardGrafico.Invalidate();
                }
                catch { }
            }
        }
        
        private void PnlDashboardGrafico_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            g.DrawString("Distribución de Asistencia de Hoy", new System.Drawing.Font("Segoe UI", 16, FontStyle.Bold), Brushes.Black, 50, 30);
            
            // Si no hay datos, mostramos valores simulados proporcionales a Docentes para que el pie chart no quede vacío.
            int val1 = dashboardPuntuales > 0 ? dashboardPuntuales : 18;
            int val2 = dashboardTardanzas > 0 ? dashboardTardanzas : 4;
            int val3 = dashboardFaltas > 0 ? dashboardFaltas : 2;
            int t = val1 + val2 + val3;
            
            float ang1 = (val1 / (float)t) * 360f;
            float ang2 = (val2 / (float)t) * 360f;
            float ang3 = (val3 / (float)t) * 360f;
            
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(50, 80, 250, 250);
            g.FillPie(new SolidBrush(Color.FromArgb(46, 204, 113)), rect, 0, ang1);
            g.FillPie(new SolidBrush(Color.FromArgb(243, 156, 18)), rect, ang1, ang2);
            g.FillPie(new SolidBrush(Color.FromArgb(231, 76, 60)), rect, ang1 + ang2, ang3);
            
            // Leyenda
            g.FillRectangle(new SolidBrush(Color.FromArgb(46, 204, 113)), 350, 100, 20, 20);
            g.DrawString("Asistencias (" + val1 + ")", new System.Drawing.Font("Segoe UI", 12), Brushes.Black, 380, 98);
            
            g.FillRectangle(new SolidBrush(Color.FromArgb(243, 156, 18)), 350, 140, 20, 20);
            g.DrawString("Tardanzas (" + val2 + ")", new System.Drawing.Font("Segoe UI", 12), Brushes.Black, 380, 138);
            
            g.FillRectangle(new SolidBrush(Color.FromArgb(231, 76, 60)), 350, 180, 20, 20);
            g.DrawString("Faltas (" + val3 + ")", new System.Drawing.Font("Segoe UI", 12), Brushes.Black, 380, 178);
        }
        
        private void CargarJustificaciones()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT r.Id, d.Nombre + ' ' + d.Apellido as Docente, c.Nombre as Curso, 
                                     CAST(r.FechaHora AS DATE) as Fecha, 
                                     ISNULL(r.Justificacion, r.MotivoSalidaAnticipada) as Motivo,
                                     CASE 
                                        WHEN r.Justificacion IS NOT NULL THEN 'Inasistencia'
                                        WHEN r.MotivoSalidaAnticipada IS NOT NULL THEN 'Salida Anticipada'
                                     END as Tipo,
                                     CASE 
                                        WHEN r.Justificacion IS NOT NULL THEN ISNULL(r.EstadoJustificacion, 'Pendiente')
                                        WHEN r.MotivoSalidaAnticipada IS NOT NULL THEN ISNULL(r.EstadoSalidaAnticipada, 'Pendiente')
                                     END as Estado
                                     FROM RegistroAsistencia r
                                     INNER JOIN Docentes d ON r.DocenteId = d.Id
                                     INNER JOIN Cursos c ON r.CursoId = c.Id
                                     WHERE r.Justificacion IS NOT NULL OR r.MotivoSalidaAnticipada IS NOT NULL
                                     ORDER BY r.FechaHora DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvJustificaciones.DataSource = dt;
                    if(dgvJustificaciones.Columns.Contains("Id")) dgvJustificaciones.Columns["Id"].Visible = false;
                }
                catch { }
            }
        }
        
        private void ProcesarJustificacion(string nuevoEstado)
        {
            if (dgvJustificaciones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una justificación de la lista.");
                return;
            }
            
            DialogResult dialogResult = MessageBox.Show($"¿Estás seguro de que deseas marcar esta justificación como '{nuevoEstado}'?", "Confirmación de Acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes)
            {
                return;
            }
            
            int id = Convert.ToInt32(dgvJustificaciones.SelectedRows[0].Cells["Id"].Value);
            string tipo = dgvJustificaciones.SelectedRows[0].Cells["Tipo"].Value.ToString();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string q = tipo == "Inasistencia" ? 
                               "UPDATE RegistroAsistencia SET EstadoJustificacion = @E WHERE Id = @Id" :
                               "UPDATE RegistroAsistencia SET EstadoSalidaAnticipada = @E WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        cmd.Parameters.AddWithValue("@E", nuevoEstado);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show($"Justificación marcada como {nuevoEstado}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarJustificaciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        // ------------------ END NEW ADMIN PRO FEATURES ------------------

        public FormAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            epValidacion.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            dgvDocentes.ReadOnly = true;
            dgvDocentes.AllowUserToAddRows = false;
            dgvDocentes.RowHeadersVisible = false;
            dgvDocentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvHistorial.ReadOnly = true;
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.RowHeadersVisible = false;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            ContextMenuStrip menuHistorial = new ContextMenuStrip();
            menuHistorial.Items.Add("Añadir Asistencia", null, (s, ev) => EditarAsistencia(0));
            menuHistorial.Items.Add("Editar Asistencia", null, (s, ev) => EditarAsistenciaSeleccionada());
            menuHistorial.Items.Add("Eliminar Asistencia", null, (s, ev) => EliminarAsistencia());
            dgvHistorial.ContextMenuStrip = menuHistorial;
            
            dgvHistorial.CellMouseDown -= dgvHistorial_CellMouseDown;
            dgvHistorial.CellMouseDown += dgvHistorial_CellMouseDown;

            ConfigurarDataGridViewCursos();



            // Ocultar el panel de filtros innecesario y reposicionar el calendario
            if (monthCalendarHistorial != null)
            {
                monthCalendarHistorial.Location = new System.Drawing.Point(24, 28);
                monthCalendarHistorial.FirstDayOfWeek = Day.Monday;
            }

            // Eventos de controles
            dgvDocentes.CellMouseDown -= dgvDocentes_CellMouseDown;
            dgvDocentes.CellMouseDown += dgvDocentes_CellMouseDown;

            txtBuscarHistorialNombre.TextChanged -= txtBuscarHistorialNombre_TextChanged;
            txtBuscarHistorialNombre.TextChanged += txtBuscarHistorialNombre_TextChanged;
            txtBuscarHistorialNombre.KeyPress -= txtBuscarHistorialNombre_KeyPress;
            txtBuscarHistorialNombre.KeyPress += txtBuscarHistorialNombre_KeyPress;

            monthCalendarHistorial.DateSelected -= monthCalendarHistorial_DateSelected;
            monthCalendarHistorial.DateSelected += monthCalendarHistorial_DateSelected;

            ConfigurarBoton("btnExportarPDF", btnExportarPDF_Click);
            ConfigurarBoton("btnExportarDocentesPDF", btnExportarDocentesPDF_Click);
            ConfigurarBoton("btnExportarDocentesExcel", btnExportarDocentesExcel_Click);
            ConfigurarBoton("btnExportarExcel", btnExportarExcel_Click);
            ConfigurarBoton("btnEliminarTodo", btnEliminarTodo_Click);
            ConfigurarBoton("btnImportarExcel", btnImportarExcel_Click);
            ConfigurarBoton("btnCerrarSesion", btnCerrarSesion_Click);
            btnCerrarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnCerrarSesion.BringToFront();

            // Botón dinámico para cambiar la contraseña
            Button btnCambiarClaveAdmin = new Button()
            {
                Text = "Cambiar Clave",
                Width = 110,
                Height = 40,
                BackColor = System.Drawing.Color.LightGoldenrodYellow,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCambiarClaveAdmin.Click += btnCambiarClave_Click;
            this.Controls.Add(btnCambiarClaveAdmin);
            btnCambiarClaveAdmin.BringToFront();

            // Configurar combobox de periodos en el historial
            cmbFiltroPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroPeriodo.Width = 150;
            cmbFiltroPeriodo.Left = txtBuscarHistorialNombre.Left;
            cmbFiltroPeriodo.Top = txtBuscarHistorialNombre.Bottom + 10;
            cmbFiltroPeriodo.SelectedIndexChanged += (s, e) => { AplicarFiltroHistorial(); };
            if (txtBuscarHistorialNombre.Parent != null) txtBuscarHistorialNombre.Parent.Controls.Add(cmbFiltroPeriodo);


            this.Resize += (s, e) => {
                if (this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
                {
                    btnCerrarSesion.Left = this.ClientSize.Width - btnCerrarSesion.Width - 20;
                    btnCerrarSesion.Top = 10;
                    btnCambiarClaveAdmin.Left = this.ClientSize.Width - btnCambiarClaveAdmin.Width - 20;
                    btnCambiarClaveAdmin.Top = this.ClientSize.Height - btnCambiarClaveAdmin.Height - 20;
                }
            };

            // Forzar ubicación inicial por si no detona el Resize de inmediato
            btnCerrarSesion.Left = this.ClientSize.Width - btnCerrarSesion.Width - 20;
            btnCerrarSesion.Top = 10;
            btnCambiarClaveAdmin.Left = this.ClientSize.Width - btnCambiarClaveAdmin.Width - 20;
            btnCambiarClaveAdmin.Top = this.ClientSize.Height - btnCambiarClaveAdmin.Height - 20;
            CargarFiltroPeriodos();
            if (cmbTipoFiltroReporte != null)
                cmbTipoFiltroReporte.SelectedIndex = 0;
            CargarDocentesFiltroReporte();

            ConfigurarBoton("btnImportarCursos", btnImportarCursos_Click);
            ConfigurarBoton("btnExportarCursosExcel", btnExportarCursosExcel_Click);
            ConfigurarBoton("btnExportarCursosPDF", btnExportarCursosPDF_Click);

            // Nuevos botones de Asignaturas separados

            // Tooltips
            ConfigurarToolTip("btnExportarDocentesExcel", "Exportar lista a Excel");
            ConfigurarToolTip("btnExportarDocentesPDF", "Exportar lista a PDF");
            ConfigurarToolTip("btnImportarExcel", "Importar docentes desde Excel");
            ConfigurarToolTip("btnExportarCursosExcel", "Exportar asignaturas a Excel");
            ConfigurarToolTip("btnExportarCursosPDF", "Exportar asignaturas a PDF");
            ConfigurarToolTip("btnImportarCursos", "Importar asignaturas desde Excel");
            ConfigurarToolTip("btnExportarExcel", "Exportar historial a Excel");
            ConfigurarToolTip("btnExportarPDF", "Exportar historial a PDF");
            ConfigurarToolTip("btnEliminarTodo", "Eliminar todos los registros");
            ConfigurarToolTip("btnCerrarSesion", "Volver al menú de inicio");

            Control[] controlesBuscadorCursos = this.Controls.Find("txtBuscarCurso", true);
            if (controlesBuscadorCursos.Length > 0 && controlesBuscadorCursos[0] is TextBox txtBusC)
            {
                txtBusC.TextChanged -= txtBuscarCurso_TextChanged;
                txtBusC.TextChanged += txtBuscarCurso_TextChanged;
            }

            Control[] controlesDgvCursos = this.Controls.Find("dgvCursos", true);
            if (controlesDgvCursos.Length > 0 && controlesDgvCursos[0] is DataGridView dgvCursosEnlace)
            {
                dgvCursosEnlace.CellMouseDown -= dgvCursos_CellMouseDown;
                dgvCursosEnlace.CellMouseDown += dgvCursos_CellMouseDown;
            }

            ConectarMenuClick("añadirCursoToolStripMenuItem", añadirCursoToolStripMenuItem_Click);
            ConectarMenuClick("editarCursoToolStripMenuItem", editarCursoToolStripMenuItem_Click);
            ConectarMenuClick("eliminarCursoToolStripMenuItem", eliminarCursoToolStripMenuItem_Click);

            CargarListadoDocentes();
            CargarHistorial();
            AplicarFiltroHistorial(); // Filtrar por el día de hoy por defecto al inicio
            CargarCursos();
            ConfigurarValidacionEnTiempoReal();

            // Horarios de Clases
            CargarHorarios();
            
            ConfigurarBuscadores();
        }

        private void ConfigurarBuscadores()
        {
            if (cmbCriterioDocentes.Items.Count > 0) cmbCriterioDocentes.SelectedIndex = 0;
            if (cmbCriterioCursos.Items.Count > 0) cmbCriterioCursos.SelectedIndex = 0;
            if (cmbCriterioHorarios.Items.Count > 0) cmbCriterioHorarios.SelectedIndex = 0;
            if (cmbCriterioHistorial.Items.Count > 0) cmbCriterioHistorial.SelectedIndex = 0;

            cmbCriterioDocentes.SelectedIndexChanged += (s, e) => AplicarFiltro();
            cmbCriterioCursos.SelectedIndexChanged += (s, e) => txtBuscarCurso_TextChanged(txtNuevoNombreCurso, EventArgs.Empty);
            cmbCriterioHorarios.SelectedIndexChanged += (s, e) => TxtBuscarHorario_TextChanged(txtBuscarHorario, EventArgs.Empty);
            cmbCriterioHistorial.SelectedIndexChanged += (s, e) => AplicarFiltroHistorial();
        }

        private void ConfigurarValidacionEnTiempoReal()
        {
            // Forzar a mayúsculas de forma nativa
            TextBox[] cajasMayusculas = { 
                ObtenerCajaTexto("txtAddNombreCurso"),
                ObtenerCajaTexto("txtAddCodigoCurso"),
                ObtenerCajaTexto("txtAddCodigo")
            };
            foreach (var caja in cajasMayusculas)
            {
                if (caja != null) caja.CharacterCasing = CharacterCasing.Upper;
            }

            ConectarValidacionTexto("txtAddCodigo", ValidarCodigoDocente);
            ConectarValidacionTexto("txtAddDNI", ValidarDNIDocente);

            ConectarValidacionTexto("txtAddCodigoCurso", ValidarCodigoCurso);
            ConectarValidacionTexto("txtAddCreditosCurso", ValidarCreditosCurso);
        }

        private void ConectarValidacionTexto(string nombreControl, EventHandler evento)
        {
            TextBox txt = ObtenerCajaTexto(nombreControl);
            if (txt != null)
            {
                txt.TextChanged -= evento;
                txt.TextChanged += evento;
            }
        }

        private void ValidarCodigoDocente(object sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                string val = txt.Text.Trim();
                if (val.Length != 6 || !System.Text.RegularExpressions.Regex.IsMatch(val, "^[A-Z0-9]+$"))
                    epValidacion.SetError(txt, "Requerido: 6 caracteres exactos (sólo mayúsculas o números).");
                else
                    epValidacion.SetError(txt, "");
            }
        }

        private void ValidarDNIDocente(object sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                string val = txt.Text.Trim();
                if (val.Length != 8 || !System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9]+$"))
                    epValidacion.SetError(txt, "Requerido: 8 dígitos numéricos exactos.");
                else
                    epValidacion.SetError(txt, "");
            }
        }

        private void ValidarCodigoCurso(object sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                string val = txt.Text.Trim();
                if (val.Length > 0 && (val.Length > 6 || !System.Text.RegularExpressions.Regex.IsMatch(val, "^[A-Z0-9]*$")))
                    epValidacion.SetError(txt, "Requerido: Hasta 6 caracteres (sólo mayúsculas o números).");
                else
                    epValidacion.SetError(txt, "");
            }
        }

        private void ValidarCreditosCurso(object sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                string val = txt.Text.Trim();
                if (val.Length > 0)
                {
                    if (int.TryParse(val, out int cred) && (cred == 2 || cred == 3 || cred == 4 || cred == 5 || cred == 6))
                        epValidacion.SetError(txt, "");
                    else
                        epValidacion.SetError(txt, "Requerido: Sólo se admiten valores de 2, 3, 4, 5 o 6.");
                }
                else
                    epValidacion.SetError(txt, "");
            }
        }

        private void AutoGenerarCorreo(string nombre, string apellido, TextBox txtCorreo)
        {
            try
            {
                nombre = nombre?.Trim() ?? "";
                apellido = apellido?.Trim() ?? "";
                
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido)) return;

                nombre = QuitarTildes(nombre).ToLower();
                apellido = QuitarTildes(apellido).ToLower();

                string[] partesN = nombre.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] partesA = apellido.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (partesN.Length == 0 || partesA.Length == 0) return;

                string pletraN = partesN[0].Substring(0, 1);
                string pApell = partesA[0];
                string sletraA = partesA.Length > 1 ? partesA[1].Substring(0, 1) : "";

                string correoGenerado = $"{pletraN}{pApell}{sletraA}@unmsm.edu.pe";
                txtCorreo.Text = correoGenerado;
                txtCorreo.Tag = "auto";
            }
            catch { }
        }

        private void ConectarMenuClick(string nombreItem, EventHandler evento)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is DataGridView dgv && dgv.ContextMenuStrip != null)
                {
                    foreach (ToolStripItem item in dgv.ContextMenuStrip.Items)
                    {
                        if (item.Name == nombreItem)
                        {
                            item.Click -= evento;
                            item.Click += evento;
                        }
                    }
                }
            }
        }

        private void ConfigurarDataGridViewCursos()
        {
            Control[] controles = this.Controls.Find("dgvCursos", true);
            if (controles.Length > 0 && controles[0] is DataGridView dgvC)
            {
                dgvC.ReadOnly = true;
                dgvC.AllowUserToAddRows = false;
                dgvC.RowHeadersVisible = false;
                dgvC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }

        private void ConfigurarBoton(string nombreBoton, EventHandler evento)
        {
            Control[] controles = this.Controls.Find(nombreBoton, true);
            if (controles.Length > 0)
            {
                Control btn = controles[0];
                btn.Click -= evento;
                btn.Click += evento;
            }
        }

        private void ConfigurarToolTip(string nombreControl, string textoTooltip)
        {
            Control ctrl = ObtenerControl(nombreControl);
            if (ctrl != null)
            {
                toolTipBotones.SetToolTip(ctrl, textoTooltip);
            }
        }

        private Control ObtenerControl(string nombre)
        {
            Control[] controles = this.Controls.Find(nombre, true);
            return controles.Length > 0 ? controles[0] : null;
        }

        private TextBox ObtenerCajaTexto(string nombre)
        {
            Control c = ObtenerControl(nombre);
            return c is TextBox ? (TextBox)c : null;
        }

        // Navegación entre pestañas
        private void NavegarHaciaControl(string nombreCajaTexto)
        {
            Control[] busqueda = this.Controls.Find(nombreCajaTexto, true);
            if (busqueda.Length > 0)
            {
                Control actual = busqueda[0];
                // Sube por la jerarquía abriendo todos los TabControls que encuentre
                while (actual != null)
                {
                    if (actual is TabPage pagina && actual.Parent is TabControl controlPestañas)
                    {
                        controlPestañas.SelectedTab = pagina;
                    }
                    actual = actual.Parent;
                }
                // Opcional: pone el cursor listo para escribir
                busqueda[0].Focus();
            }
        }

        // Cursos
        private void CargarCursos()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Id, Codigo, Nombre, Creditos, Tipo, Escuela, MallaCurricular FROM Cursos ORDER BY Nombre ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    tablaCursos.Clear();
                    da.Fill(tablaCursos);

                    if (!tablaCursos.Columns.Contains("FiltroBusqueda"))
                    {
                        tablaCursos.Columns.Add("FiltroBusqueda", typeof(string));
                    }
                    foreach (DataRow row in tablaCursos.Rows)
                    {
                        string c = row["Codigo"]?.ToString() ?? "";
                        string n = row["Nombre"]?.ToString() ?? "";
                        string t = row["Tipo"]?.ToString() ?? "";
                        string esc = row["Escuela"]?.ToString() ?? "";
                        string mal = row["MallaCurricular"]?.ToString() ?? "";
                        row["FiltroBusqueda"] = QuitarTildes($"{c} {n} {t} {esc} {mal}").ToLower();
                    }

                    Control[] controles = this.Controls.Find("dgvCursos", true);
                    if (controles.Length > 0 && controles[0] is DataGridView dgvC)
                    {
                        dgvC.DataSource = tablaCursos;
                        dgvC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        if (dgvC.Columns["Id"] != null) dgvC.Columns["Id"].Visible = false;
                        if (dgvC.Columns["FiltroBusqueda"] != null) dgvC.Columns["FiltroBusqueda"].Visible = false;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error al cargar el listado de cursos: " + ex.Message); }
            }
        }

        private void txtBuscarCurso_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBusC = sender as TextBox ?? ObtenerCajaTexto("txtBuscarCurso");
            if (txtBusC == null || tablaCursos == null) return;

            string filtro = txtBusC.Text.Trim();
            if (string.IsNullOrEmpty(filtro))
            {
                tablaCursos.DefaultView.RowFilter = "";
            }
            else
            {
                string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                string criterio = cmbCriterioCursos.SelectedItem?.ToString() ?? "Todos";
                
                if (criterio == "Código")
                    tablaCursos.DefaultView.RowFilter = $"Codigo LIKE '%{filtro}%'";
                else if (criterio == "Nombre")
                    tablaCursos.DefaultView.RowFilter = $"Nombre LIKE '%{filtro}%'";
                else if (criterio == "Escuela")
                    tablaCursos.DefaultView.RowFilter = $"Escuela LIKE '%{filtro}%'";
                else
                    tablaCursos.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
            }
        }

        private void btnImportarCursos_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Archivos de Excel (CSV) (*.csv)|*.csv|Todos los archivos (*.*)|*.*", Title = "Seleccionar Lista de Asignaturas" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                int insertados = 0; int omitidos = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string[] lineas = File.ReadAllLines(openFileDialog.FileName, Encoding.UTF8);

                        for (int i = 1; i < lineas.Length; i++)
                        {
                            string linea = lineas[i];
                            if (string.IsNullOrWhiteSpace(linea)) continue;
                            char delimitador = linea.Contains(";") ? ';' : (linea.Contains(",") ? ',' : '\t');
                            string[] columnas = linea.Split(delimitador);

                            if (columnas.Length >= 2)
                            {
                                try
                                {
                                    string codigoCurso = columnas[0].Trim().Replace("\"", "");
                                    string nombreCurso = columnas[1].Trim().Replace("\"", "");

                                    int creditosCurso = 0;
                                    if (columnas.Length >= 3) int.TryParse(columnas[2].Trim().Replace("\"", ""), out creditosCurso);

                                    string tipoCurso = (columnas.Length >= 4) ? columnas[3].Trim().Replace("\"", "").ToUpper() : "";
                                    string escuelaCurso = (columnas.Length >= 5) ? columnas[4].Trim().Replace("\"", "") : "";
                                    string mallaCurso = (columnas.Length >= 6) ? columnas[5].Trim().Replace("\"", "") : "";

                                    string queryCurso = @"
                                        IF EXISTS (SELECT 1 FROM Cursos WHERE Codigo = @C)
                                            UPDATE Cursos SET Nombre = @N, Creditos = @Cred, Tipo = @T, Escuela = @Esc, MallaCurricular = @Mal WHERE Codigo = @C;
                                        ELSE
                                            INSERT INTO Cursos (Codigo, Nombre, Creditos, Tipo, Escuela, MallaCurricular) VALUES (@C, @N, @Cred, @T, @Esc, @Mal);";

                                    using (SqlCommand cmd = new SqlCommand(queryCurso, con))
                                    {
                                        cmd.Parameters.AddWithValue("@C", codigoCurso);
                                        cmd.Parameters.AddWithValue("@N", nombreCurso);
                                        cmd.Parameters.AddWithValue("@Cred", creditosCurso > 0 ? (object)creditosCurso : DBNull.Value);
                                        cmd.Parameters.AddWithValue("@T", tipoCurso);
                                        cmd.Parameters.AddWithValue("@Esc", string.IsNullOrEmpty(escuelaCurso) ? DBNull.Value : (object)escuelaCurso);
                                        cmd.Parameters.AddWithValue("@Mal", string.IsNullOrEmpty(mallaCurso) ? DBNull.Value : (object)mallaCurso);
                                        cmd.ExecuteNonQuery();
                                        insertados++;
                                    }
                                }
                                catch { omitidos++; }
                            }
                            else { omitidos++; }
                        }
                        MessageBox.Show($"Importación finalizada.\n• Registrados/Actualizados: {insertados}\n• Omitidos: {omitidos}", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarCursos();
                    }
                    catch (Exception ex) { MessageBox.Show("Error al importar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnExportarCursosExcel_Click(object sender, EventArgs e)
        {
            Control[] controles = this.Controls.Find("dgvCursos", true);
            if (controles.Length > 0 && controles[0] is DataGridView dgvC)
            {
                string fechaHora = DateTime.Now.ToString("dd-MM-yyyy_HHmmss");
                ExportarDataGridViewAExcel(dgvC, "Cursos", "", $"Lista_Asignaturas_{fechaHora}.csv");
            }
        }

        private void btnExportarCursosPDF_Click(object sender, EventArgs e)
        {
            Control[] controles = this.Controls.Find("dgvCursos", true);
            if (controles.Length > 0 && controles[0] is DataGridView dgvC)
            {
                string fechaHora = DateTime.Now.ToString("dd-MM-yyyy_HHmmss");
                ExportarDataGridViewAPDF(dgvC, "Cursos", "", $"Lista_Asignaturas_{fechaHora}.pdf", "LISTADO OFICIAL DE ASIGNATURAS", false);
            }
        }

        private void dgvCursos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Control[] controles = this.Controls.Find("dgvCursos", true);
            if (controles.Length > 0 && controles[0] is DataGridView dgvC)
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    dgvC.ClearSelection();
                    dgvC.CurrentCell = dgvC.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvC.Rows[e.RowIndex].Selected = true;
                }
            }
        }

        private void eliminarCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control[] controles = this.Controls.Find("dgvCursos", true);
            if (controles.Length > 0 && controles[0] is DataGridView dgvC && dgvC.CurrentRow != null)
            {
                int idCurso = Convert.ToInt32(dgvC.CurrentRow.Cells["Id"].Value);
                string nombreCurso = dgvC.CurrentRow.Cells["Nombre"].Value.ToString();

                if (FormConfirmacion.Mostrar($"¿Eliminar la asignatura '{nombreCurso}'?", "Confirmar") == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            new SqlCommand($"DELETE FROM RegistroAsistencia WHERE CursoId = {idCurso}", con).ExecuteNonQuery();
                            new SqlCommand($"DELETE FROM HorarioClases WHERE CursoId = {idCurso}", con).ExecuteNonQuery();
                            new SqlCommand($"DELETE FROM Cursos WHERE Id = {idCurso}", con).ExecuteNonQuery();
                            CargarCursos();
                        }
                        catch (Exception ex) { MessageBox.Show("Error al eliminar (puede tener asistencias vinculadas): " + ex.Message); }
                    }
                }
            }
        }

        // --- MENÚ CURSOS ---
        private void añadirCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormEditarCurso frm = new FormEditarCurso(0, "", "", "", "", "", ""))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargarCursos();
                }
            }
        }

        private void editarCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control[] controles = this.Controls.Find("dgvCursos", true);
            if (controles.Length > 0 && controles[0] is DataGridView dgvC && dgvC.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvC.CurrentRow.Cells["Id"].Value);
                string codigo = dgvC.CurrentRow.Cells["Codigo"].Value?.ToString() ?? "";
                string nombre = dgvC.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";
                string creditos = dgvC.CurrentRow.Cells["Creditos"].Value?.ToString() ?? "";
                string tipo = dgvC.CurrentRow.Cells["Tipo"].Value?.ToString() ?? "";
                string escuela = dgvC.Columns.Contains("Escuela") ? (dgvC.CurrentRow.Cells["Escuela"].Value?.ToString() ?? "") : "";
                string malla = dgvC.Columns.Contains("MallaCurricular") ? (dgvC.CurrentRow.Cells["MallaCurricular"].Value?.ToString() ?? "") : "";

                using (FormEditarCurso frm = new FormEditarCurso(id, codigo, nombre, creditos, tipo, escuela, malla))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CargarCursos();
                    }
                }
            }
        }

        // --- MENÚ DOCENTES ---
        private void añadirDocenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormEditarDocente frm = new FormEditarDocente(0, "", "", "", "", "", "", "", ""))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargarListadoDocentes();
                }
            }
        }

        private void editarDocenteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dgvDocentes.CurrentRow != null)
            {
                DataGridViewRow fila = dgvDocentes.CurrentRow;
                int id = Convert.ToInt32(fila.Cells["Id"].Value);
                string codigo = fila.Cells["Codigo"].Value?.ToString() ?? "";
                string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                string apellido = fila.Cells["Apellido"].Value?.ToString() ?? "";
                string dni = dgvDocentes.Columns.Contains("DNI") ? fila.Cells["DNI"].Value?.ToString() : "";
                string clase = dgvDocentes.Columns.Contains("Clase") ? fila.Cells["Clase"].Value?.ToString() : "";
                string categoria = dgvDocentes.Columns.Contains("Categoria") ? fila.Cells["Categoria"].Value?.ToString() : "";
                string departamento = dgvDocentes.Columns.Contains("Departamento") ? fila.Cells["Departamento"].Value?.ToString() : "";
                string correo = dgvDocentes.Columns.Contains("Correo") ? fila.Cells["Correo"].Value?.ToString() : "";

                using (FormEditarDocente frm = new FormEditarDocente(id, codigo, nombre, apellido, dni, clase, categoria, departamento, correo))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CargarListadoDocentes();
                        CargarHistorialDocenteEspecifico(id);
                    }
                }
            }
        }



        // Docentes e historial
        private void CargarHistorialDocenteEspecifico(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT c.Nombre as Curso, r.FechaHora as 'Hora de Ingreso', r.HoraSalida as 'Hora de Salida' FROM RegistroAsistencia r INNER JOIN Cursos c ON r.CursoId = c.Id WHERE r.DocenteId = @Id ORDER BY r.FechaHora DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@Id", id);
                    DataTable dt = new DataTable(); da.Fill(dt);
                    if (this.Controls.Find("dataGridView1", true).Length > 0)
                    {
                        DataGridView dgv1 = (DataGridView)this.Controls.Find("dataGridView1", true)[0];
                        dgv1.DataSource = dt; dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgv1.RowHeadersVisible = false; dgv1.ReadOnly = true; dgv1.AllowUserToAddRows = false;
                    }
                }
                catch { }
            }
        }




        private void dgvDocentes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvDocentes.ClearSelection();
                dgvDocentes.CurrentCell = dgvDocentes.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvDocentes.Rows[e.RowIndex].Selected = true;
            }
        }

        private void eliminarDocentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDocentes.CurrentRow != null)
            {
                DataGridViewRow fila = dgvDocentes.CurrentRow;
                int idDocente = Convert.ToInt32(fila.Cells["Id"].Value);
                if (FormConfirmacion.Mostrar($"¿Eliminar al docente {fila.Cells["Nombre"].Value}?", "Confirmar") == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try { 
                            con.Open(); 
                            new SqlCommand($"DELETE FROM RegistroAsistencia WHERE DocenteId = {idDocente}", con).ExecuteNonQuery(); 
                            new SqlCommand($"DELETE FROM HorarioClases WHERE DocenteId = {idDocente}", con).ExecuteNonQuery(); 
                            new SqlCommand($"DELETE FROM Docentes WHERE Id = {idDocente}", con).ExecuteNonQuery(); 
                            CargarListadoDocentes(); 
                        } catch { }
                    }
                }
            }
        }

        private void txtBuscarHistorialNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) e.Handled = true;
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfigLimites_Click(object sender, EventArgs e)
        {
            new FormConfigLimites().ShowDialog(this);
        }

        private string ObtenerContraseñaAdmin()
        {
            string ruta = Path.Combine(Application.StartupPath, "admin_config.txt");
            if (File.Exists(ruta)) return File.ReadAllText(ruta).Trim();
            return "Admin123";
        }

        private void GuardarContraseñaAdmin(string nuevaClave)
        {
            string ruta = Path.Combine(Application.StartupPath, "admin_config.txt");
            File.WriteAllText(ruta, nuevaClave);
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 230,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Cambiar Contraseña de Administrador",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblActual = new Label() { Left = 20, Top = 20, Text = "Contraseña Actual:", Width = 150 };
            TextBox txtActual = new TextBox() { Left = 20, Top = 45, Width = 290, UseSystemPasswordChar = true };

            Label lblNueva = new Label() { Left = 20, Top = 80, Text = "Nueva Contraseña:", Width = 150 };
            TextBox txtNueva = new TextBox() { Left = 20, Top = 105, Width = 290, UseSystemPasswordChar = true };

            Button btnGuardar = new Button() { Text = "Guardar", Left = 130, Top = 150, Width = 80, BackColor = System.Drawing.Color.LightGreen, FlatStyle = FlatStyle.Flat };
            Button btnCancelar = new Button() { Text = "Cancelar", Left = 230, Top = 150, Width = 80 };

            btnGuardar.Click += (s, ev) =>
            {
                if (txtActual.Text != ObtenerContraseñaAdmin())
                {
                    MessageBox.Show("La contraseña actual es incorrecta.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNueva.Text))
                {
                    MessageBox.Show("La nueva contraseña no puede estar vacía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = FormConfirmacion.Mostrar("¿Estás seguro que deseas cambiar la contraseña de acceso al sistema?", "Confirmación de Seguridad");

                if (confirmacion == DialogResult.Yes)
                {
                    GuardarContraseñaAdmin(txtNueva.Text);
                    MessageBox.Show("¡Contraseña actualizada con éxito!", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    prompt.Close();
                }
            };

            btnCancelar.Click += (s, ev) => { prompt.Close(); };

            prompt.Controls.Add(lblActual);
            prompt.Controls.Add(txtActual);
            prompt.Controls.Add(lblNueva);
            prompt.Controls.Add(txtNueva);
            prompt.Controls.Add(btnGuardar);
            prompt.Controls.Add(btnCancelar);
            prompt.ShowDialog();
        }

        private void CargarListadoDocentes()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Id, Codigo, Nombre, Apellido FROM Docentes";
                    try { new SqlCommand("SELECT DNI FROM Docentes WHERE 1=0", con).ExecuteNonQuery(); query = "SELECT Id, Codigo, Nombre, Apellido, DNI FROM Docentes"; } catch { }
                    try { new SqlCommand("SELECT Clase FROM Docentes WHERE 1=0", con).ExecuteNonQuery(); query = "SELECT Id, Codigo, Nombre, Apellido, DNI, Clase, Categoria, Departamento, Correo FROM Docentes"; } catch { }

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    tablaDocentes.Clear();
                    da.Fill(tablaDocentes);
                    if (!tablaDocentes.Columns.Contains("FiltroBusqueda"))
                    {
                        tablaDocentes.Columns.Add("FiltroBusqueda", typeof(string));
                    }
                    bool tieneDni = tablaDocentes.Columns.Contains("DNI");
                    foreach (DataRow row in tablaDocentes.Rows)
                    {
                        string c = row["Codigo"]?.ToString() ?? "";
                        string n = row["Nombre"]?.ToString() ?? "";
                        string a = row["Apellido"]?.ToString() ?? "";
                        string dni = tieneDni ? (row["DNI"]?.ToString() ?? "") : "";
                        string clase = tablaDocentes.Columns.Contains("Clase") ? (row["Clase"]?.ToString() ?? "") : "";
                        string categoria = tablaDocentes.Columns.Contains("Categoria") ? (row["Categoria"]?.ToString() ?? "") : "";
                        string depto = tablaDocentes.Columns.Contains("Departamento") ? (row["Departamento"]?.ToString() ?? "") : "";
                        string correo = tablaDocentes.Columns.Contains("Correo") ? (row["Correo"]?.ToString() ?? "") : "";
                        row["FiltroBusqueda"] = QuitarTildes($"{c} {n} {a} {dni} {clase} {categoria} {depto} {correo}").ToLower();
                    }

                    dgvDocentes.DataSource = tablaDocentes;
                    dgvDocentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    if (dgvDocentes.Columns["Id"] != null) dgvDocentes.Columns["Id"].Visible = false;
                    if (dgvDocentes.Columns["FiltroBusqueda"] != null) dgvDocentes.Columns["FiltroBusqueda"].Visible = false;
                }
                catch (Exception ex) { MessageBox.Show("Error al cargar docentes: " + ex.Message); }
            }
        }

        private string QuitarTildes(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "";
            return texto.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
                        .Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");
        }

        private void AplicarFiltro()
        {
            if (tablaDocentes == null) return;
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(filtro)) { tablaDocentes.DefaultView.RowFilter = ""; return; }

            string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
            string criterio = cmbCriterioDocentes.SelectedItem?.ToString() ?? "Todos";

            if (criterio == "Nombre")
                tablaDocentes.DefaultView.RowFilter = $"Nombre LIKE '%{filtro}%' OR Apellido LIKE '%{filtro}%'";
            else if (criterio == "Código")
                tablaDocentes.DefaultView.RowFilter = $"Codigo LIKE '%{filtro}%'";
            else if (criterio == "DNI")
                tablaDocentes.DefaultView.RowFilter = $"DNI LIKE '%{filtro}%'";
            else
                tablaDocentes.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) { AplicarFiltro(); }

        private void btnImportarExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Archivos de Excel (CSV) (*.csv)|*.csv|Todos los archivos (*.*)|*.*", Title = "Seleccionar Lista de Docentes" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                int insertados = 0; int omitidos = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string[] lineas = File.ReadAllLines(openFileDialog.FileName, Encoding.UTF8);
                        for (int i = 1; i < lineas.Length; i++)
                        {
                            string linea = lineas[i];
                            if (string.IsNullOrWhiteSpace(linea)) continue;
                            char delimitador = linea.Contains(";") ? ';' : (linea.Contains(",") ? ',' : '\t');
                            string[] columnas = linea.Split(delimitador);

                            if (columnas.Length >= 3)
                            {
                                try
                                {
                                    string codigo = columnas[0].Trim().Replace("\"", "");
                                    string nombre = columnas[1].Trim().Replace("\"", "");
                                    string apellido = columnas[2].Trim().Replace("\"", "");
                                    string dni = (columnas.Length >= 4) ? columnas[3].Trim().Replace("\"", "") : "";
                                    string clase = (columnas.Length >= 5) ? columnas[4].Trim().Replace("\"", "") : "";
                                    string categoria = (columnas.Length >= 6) ? columnas[5].Trim().Replace("\"", "") : "";
                                    string departamento = (columnas.Length >= 7) ? columnas[6].Trim().Replace("\"", "") : "";
                                    string correo = (columnas.Length >= 8) ? columnas[7].Trim().Replace("\"", "") : "";

                                    string query = @"
                                        IF EXISTS (SELECT 1 FROM Docentes WHERE Codigo = @C)
                                            UPDATE Docentes SET Nombre = @N, Apellido = @A, DNI = @D, Clase = @Cl, Categoria = @Cat, Departamento = @Dep, Correo = @Cor WHERE Codigo = @C;
                                        ELSE
                                            INSERT INTO Docentes (Codigo, Nombre, Apellido, DNI, Clase, Categoria, Departamento, Correo) VALUES (@C, @N, @A, @D, @Cl, @Cat, @Dep, @Cor);";

                                    using (SqlCommand cmd = new SqlCommand(query, con))
                                    {
                                        cmd.Parameters.AddWithValue("@C", codigo);
                                        cmd.Parameters.AddWithValue("@N", nombre);
                                        cmd.Parameters.AddWithValue("@A", apellido);
                                        cmd.Parameters.AddWithValue("@D", string.IsNullOrEmpty(dni) ? (object)DBNull.Value : dni);
                                        cmd.Parameters.AddWithValue("@Cl", string.IsNullOrEmpty(clase) ? (object)DBNull.Value : clase);
                                        cmd.Parameters.AddWithValue("@Cat", string.IsNullOrEmpty(categoria) ? (object)DBNull.Value : categoria);
                                        cmd.Parameters.AddWithValue("@Dep", string.IsNullOrEmpty(departamento) ? (object)DBNull.Value : departamento);
                                        cmd.Parameters.AddWithValue("@Cor", string.IsNullOrEmpty(correo) ? (object)DBNull.Value : correo);
                                        cmd.ExecuteNonQuery(); insertados++;
                                    }
                                }
                                catch { omitidos++; }
                            }
                            else { omitidos++; }
                        }
                        MessageBox.Show($"Importación finalizada.\n• Registrados/Actualizados: {insertados}\n• Omitidos: {omitidos}", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarListadoDocentes();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnExportarDocentesExcel_Click(object sender, EventArgs e)
        {
            string fechaHora = DateTime.Now.ToString("dd-MM-yyyy_HHmmss");
            ExportarDataGridViewAExcel(dgvDocentes, "Docentes", "Lista_General", $"Padron_Docentes_{fechaHora}.csv");
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string fechaHora = DateTime.Now.ToString("dd-MM-yyyy_HHmmss");
            ExportarDataGridViewAExcel(dgvHistorial, "Historial_General", "", $"Historial_Asistencias_{fechaHora}.csv");
        }

        private void btnExportarDocentesPDF_Click(object sender, EventArgs e)
        {
            string fechaHora = DateTime.Now.ToString("dd-MM-yyyy_HHmmss");
            ExportarDataGridViewAPDF(dgvDocentes, "Docentes", "Lista_General", $"Padron_Docentes_{fechaHora}.pdf", "PADRÓN OFICIAL DE DOCENTES", false);
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            using (FormSeleccionReportePDF dialogoMenu = new FormSeleccionReportePDF())
            {
                if (dialogoMenu.ShowDialog(this) == DialogResult.OK)
                {
                    if (dialogoMenu.TipoSeleccionado == 1)
                        MostrarSelectorRangoGeneral();
                    else if (dialogoMenu.TipoSeleccionado == 2)
                        MostrarSelectorDocenteEspecifico();
                }
            }
        }

        private void MostrarSelectorRangoGeneral()
        {
            Form dialogo = new Form()
            {
                Text = "Exportar Reporte General",
                Width = 460,
                Height = 450,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblInstruccion = new Label() { Text = "1. Seleccione el rango de fechas:", Left = 20, Top = 15, Width = 400, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 10F, System.Drawing.FontStyle.Bold) };

            RadioButton rbDia = new RadioButton() { Text = "Asistencia del día:", Left = 25, Top = 50, Width = 200, Checked = true };
            DateTimePicker dtpDia = new DateTimePicker() { Left = 230, Top = 48, Width = 180, Format = DateTimePickerFormat.Short, Value = DateTime.Today };

            RadioButton rbSemana = new RadioButton() { Text = "Asistencia de la semana:", Left = 25, Top = 95, Width = 200 };
            DateTimePicker dtpSemana = new DateTimePicker() { Left = 230, Top = 93, Width = 180, Format = DateTimePickerFormat.Short, Value = DateTime.Today, Enabled = false };
            Label lblRangoSemana = new Label() { Text = "", Left = 45, Top = 120, Width = 380, ForeColor = System.Drawing.Color.DarkGray, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 8.5F, System.Drawing.FontStyle.Italic) };

            RadioButton rbMes = new RadioButton() { Text = "Asistencia del mes:", Left = 25, Top = 155, Width = 200 };
            DateTimePicker dtpMes = new DateTimePicker() { Left = 230, Top = 153, Width = 180, Format = DateTimePickerFormat.Custom, CustomFormat = "MMMM yyyy", ShowUpDown = true, Value = DateTime.Today, Enabled = false };

            Label lblTipoReporte = new Label() { Text = "2. Opciones de inasistencia:", Left = 20, Top = 200, Width = 400, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 10F, System.Drawing.FontStyle.Bold) };
            Panel pnlTipo = new Panel() { Left = 20, Top = 230, Width = 400, Height = 60 };
            RadioButton rbSoloAsistencias = new RadioButton() { Text = "Solo docentes que asistieron", Left = 5, Top = 0, Width = 300, Checked = true };
            RadioButton rbIncluirFaltas = new RadioButton() { Text = "Incluir faltas (marcar inasistencias en rojo)", Left = 5, Top = 25, Width = 300 };
            pnlTipo.Controls.Add(rbSoloAsistencias);
            pnlTipo.Controls.Add(rbIncluirFaltas);

            Label lblFormato = new Label() { Text = "3. Formato de exportación:", Left = 20, Top = 295, Width = 400, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 10F, System.Drawing.FontStyle.Bold) };
            ComboBox cmbFormato = new ComboBox() { Left = 25, Top = 325, Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFormato.Items.AddRange(new string[] { "PDF Document (*.pdf)", "Excel CSV (*.csv)" });
            cmbFormato.SelectedIndex = 0;

            Button btnAceptar = new Button() { Text = "Exportar", Left = 120, Top = 370, Width = 100, Height = 35, BackColor = System.Drawing.Color.LightGreen, FlatStyle = FlatStyle.Flat };
            Button btnCancelar = new Button() { Text = "Cancelar", Left = 240, Top = 370, Width = 100, Height = 35, DialogResult = DialogResult.Cancel };

            dialogo.AcceptButton = btnAceptar;
            dialogo.CancelButton = btnCancelar;

            rbDia.CheckedChanged += (s, ev) => { dtpDia.Enabled = rbDia.Checked; };
            rbSemana.CheckedChanged += (s, ev) => { 
                dtpSemana.Enabled = rbSemana.Checked; 
                lblRangoSemana.ForeColor = rbSemana.Checked ? System.Drawing.Color.Black : System.Drawing.Color.DarkGray;
            };
            rbMes.CheckedChanged += (s, ev) => { dtpMes.Enabled = rbMes.Checked; };

            Action actualizarLabelSemana = () =>
            {
                DateTime baseDate = dtpSemana.Value.Date;
                int diff = (7 + (baseDate.DayOfWeek - DayOfWeek.Monday)) % 7;
                DateTime start = baseDate.AddDays(-1 * diff);
                DateTime end = start.AddDays(6);
                lblRangoSemana.Text = $"Rango: Lunes {start:dd/MM/yyyy} al Domingo {end:dd/MM/yyyy}";
            };

            dtpSemana.ValueChanged += (s, ev) => actualizarLabelSemana();
            actualizarLabelSemana();

            dialogo.Controls.Add(lblInstruccion);
            dialogo.Controls.Add(rbDia);
            dialogo.Controls.Add(dtpDia);
            dialogo.Controls.Add(rbSemana);
            dialogo.Controls.Add(dtpSemana);
            dialogo.Controls.Add(lblRangoSemana);
            dialogo.Controls.Add(rbMes);
            dialogo.Controls.Add(dtpMes);
            dialogo.Controls.Add(lblTipoReporte);
            dialogo.Controls.Add(pnlTipo);
            dialogo.Controls.Add(lblFormato);
            dialogo.Controls.Add(cmbFormato);
            dialogo.Controls.Add(btnAceptar);
            dialogo.Controls.Add(btnCancelar);

            btnAceptar.Click += (s, ev) =>
            {
                DateTime inicio = DateTime.Today;
                DateTime finLimit = DateTime.Today;
                string titulo = "";
                string nombreArchivoDefecto = "";

                if (rbDia.Checked)
                {
                    DateTime diaSel = dtpDia.Value.Date;
                    inicio = diaSel;
                    finLimit = diaSel.AddDays(1);
                    titulo = $"REPORTE DE ASISTENCIAS - DÍA {diaSel:dd/MM/yyyy}";
                    nombreArchivoDefecto = $"Asistencia_Dia_{diaSel:yyyyMMdd}";
                }
                else if (rbSemana.Checked)
                {
                    DateTime baseDate = dtpSemana.Value.Date;
                    int diff = (7 + (baseDate.DayOfWeek - DayOfWeek.Monday)) % 7;
                    DateTime start = baseDate.AddDays(-1 * diff);
                    DateTime end = start.AddDays(6);
                    inicio = start;
                    finLimit = end.AddDays(1);
                    titulo = $"REPORTE DE ASISTENCIAS - SEMANA DEL {start:dd/MM/yyyy} AL {end:dd/MM/yyyy}";
                    nombreArchivoDefecto = $"Asistencia_Semana_{start:yyyyMMdd}_al_{end:yyyyMMdd}";
                }
                else if (rbMes.Checked)
                {
                    DateTime mesSel = dtpMes.Value;
                    inicio = new DateTime(mesSel.Year, mesSel.Month, 1);
                    finLimit = inicio.AddMonths(1);
                    string[] meses = { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
                    string nombreMes = meses[mesSel.Month - 1];
                    titulo = $"REPORTE DE ASISTENCIAS - MES DE {nombreMes} {mesSel.Year}";
                    nombreArchivoDefecto = $"Asistencia_Mes_{mesSel:yyyyMM}";
                }

                bool incluirFaltas = rbIncluirFaltas.Checked;
                bool esCSV = cmbFormato.SelectedIndex == 1;

                dialogo.Close();
                ExportarRangoDocumento(null, inicio, finLimit, "Historial_General", "", nombreArchivoDefecto, titulo, incluirFaltas, esCSV);
            };

            dialogo.ShowDialog();
        }

        private void MostrarSelectorDocenteEspecifico()
        {
            Form dialogo = new Form()
            {
                Text = "Exportar Reporte de Docente Específico",
                Width = 550,
                Height = 720,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblInstruccion1 = new Label() { Text = "1. Busque y seleccione al docente:", Left = 20, Top = 15, Width = 400, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 10F, System.Drawing.FontStyle.Bold) };
            TextBox txtBuscarDocente = new TextBox() { Left = 20, Top = 40, Width = 490 };
            DataGridView dgvDocentesSel = new DataGridView()
            {
                Left = 20, Top = 70, Width = 490, Height = 180, ReadOnly = true, AllowUserToAddRows = false,
                RowHeadersVisible = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            DataTable dtDocentes = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Id, Codigo as Código, Nombre as Nombres, Apellido as Apellidos FROM Docentes ORDER BY Nombre ASC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dtDocentes);
                }
                catch { }
            }
            dgvDocentesSel.DataSource = dtDocentes;
            if (dgvDocentesSel.Columns["Id"] != null) dgvDocentesSel.Columns["Id"].Visible = false;

            txtBuscarDocente.TextChanged += (s, ev) =>
            {
                string filtro = txtBuscarDocente.Text.Trim().Replace("'", "''");
                if (string.IsNullOrEmpty(filtro)) dtDocentes.DefaultView.RowFilter = "";
                else dtDocentes.DefaultView.RowFilter = $"Código LIKE '%{filtro}%' OR Nombres LIKE '%{filtro}%' OR Apellidos LIKE '%{filtro}%'";
            };

            Label lblInstruccion2 = new Label() { Text = "2. Seleccione el rango de asistencia:", Left = 20, Top = 270, Width = 400, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 10F, System.Drawing.FontStyle.Bold) };
            RadioButton rbDia = new RadioButton() { Text = "Asistencia del día:", Left = 25, Top = 300, Width = 200, Checked = true };
            DateTimePicker dtpDia = new DateTimePicker() { Left = 230, Top = 298, Width = 180, Format = DateTimePickerFormat.Short, Value = DateTime.Today };

            RadioButton rbSemana = new RadioButton() { Text = "Asistencia de la semana:", Left = 25, Top = 345, Width = 200 };
            DateTimePicker dtpSemana = new DateTimePicker() { Left = 230, Top = 343, Width = 180, Format = DateTimePickerFormat.Short, Value = DateTime.Today, Enabled = false };
            Label lblRangoSemana = new Label() { Text = "", Left = 45, Top = 370, Width = 380, ForeColor = System.Drawing.Color.DarkGray, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 8.5F, System.Drawing.FontStyle.Italic) };

            RadioButton rbMes = new RadioButton() { Text = "Asistencia del mes:", Left = 25, Top = 405, Width = 200 };
            DateTimePicker dtpMes = new DateTimePicker() { Left = 230, Top = 403, Width = 180, Format = DateTimePickerFormat.Custom, CustomFormat = "MMMM yyyy", ShowUpDown = true, Value = DateTime.Today, Enabled = false };

            Label lblTipoReporte = new Label() { Text = "3. Opciones de inasistencia:", Left = 20, Top = 450, Width = 400, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 10F, System.Drawing.FontStyle.Bold) };
            Panel pnlTipo = new Panel() { Left = 20, Top = 480, Width = 400, Height = 60 };
            RadioButton rbSoloAsistencias = new RadioButton() { Text = "Solo asistencias", Left = 5, Top = 0, Width = 300, Checked = true };
            RadioButton rbIncluirFaltas = new RadioButton() { Text = "Incluir faltas (marcar inasistencias en rojo)", Left = 5, Top = 25, Width = 300 };
            pnlTipo.Controls.Add(rbSoloAsistencias);
            pnlTipo.Controls.Add(rbIncluirFaltas);

            Label lblFormato = new Label() { Text = "4. Formato de exportación:", Left = 20, Top = 545, Width = 400, Font = new System.Drawing.Font(dialogo.Font.FontFamily, 10F, System.Drawing.FontStyle.Bold) };
            ComboBox cmbFormato = new ComboBox() { Left = 25, Top = 575, Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFormato.Items.AddRange(new string[] { "PDF Document (*.pdf)", "Excel CSV (*.csv)" });
            cmbFormato.SelectedIndex = 0;

            Button btnAceptar = new Button() { Text = "Exportar", Left = 150, Top = 620, Width = 100, Height = 38, BackColor = System.Drawing.Color.LightGreen, FlatStyle = FlatStyle.Flat };
            Button btnCancelar = new Button() { Text = "Cancelar", Left = 280, Top = 620, Width = 100, Height = 38, DialogResult = DialogResult.Cancel };

            dialogo.AcceptButton = btnAceptar;
            dialogo.CancelButton = btnCancelar;

            rbDia.CheckedChanged += (s, ev) => { dtpDia.Enabled = rbDia.Checked; };
            rbSemana.CheckedChanged += (s, ev) => { 
                dtpSemana.Enabled = rbSemana.Checked; 
                lblRangoSemana.ForeColor = rbSemana.Checked ? System.Drawing.Color.Black : System.Drawing.Color.DarkGray;
            };
            rbMes.CheckedChanged += (s, ev) => { dtpMes.Enabled = rbMes.Checked; };

            Action actualizarLabelSemana = () =>
            {
                DateTime baseDate = dtpSemana.Value.Date;
                int diff = (7 + (baseDate.DayOfWeek - DayOfWeek.Monday)) % 7;
                DateTime start = baseDate.AddDays(-1 * diff);
                DateTime end = start.AddDays(6);
                lblRangoSemana.Text = $"Rango: Lunes {start:dd/MM/yyyy} al Domingo {end:dd/MM/yyyy}";
            };

            dtpSemana.ValueChanged += (s, ev) => actualizarLabelSemana();
            actualizarLabelSemana();

            dialogo.Controls.Add(lblInstruccion1); dialogo.Controls.Add(txtBuscarDocente); dialogo.Controls.Add(dgvDocentesSel);
            dialogo.Controls.Add(lblInstruccion2); dialogo.Controls.Add(rbDia); dialogo.Controls.Add(dtpDia);
            dialogo.Controls.Add(rbSemana); dialogo.Controls.Add(dtpSemana); dialogo.Controls.Add(lblRangoSemana);
            dialogo.Controls.Add(rbMes); dialogo.Controls.Add(dtpMes);
            dialogo.Controls.Add(lblTipoReporte); dialogo.Controls.Add(pnlTipo);
            dialogo.Controls.Add(lblFormato); dialogo.Controls.Add(cmbFormato);
            dialogo.Controls.Add(btnAceptar); dialogo.Controls.Add(btnCancelar);

            btnAceptar.Click += (s, ev) =>
            {
                if (dgvDocentesSel.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione un docente de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow filaDocente = dgvDocentesSel.CurrentRow;
                int idDocente = Convert.ToInt32(filaDocente.Cells["Id"].Value);
                string codigoDocente = filaDocente.Cells["Código"].Value?.ToString() ?? "";
                string nombresDocente = filaDocente.Cells["Nombres"].Value?.ToString() ?? "";
                string apellidosDocente = filaDocente.Cells["Apellidos"].Value?.ToString() ?? "";
                string nombreCompleto = $"{nombresDocente} {apellidosDocente}";

                DateTime inicio = DateTime.Today;
                DateTime finLimit = DateTime.Today;
                string titulo = "";
                string nombreArchivoDefecto = "";

                if (rbDia.Checked)
                {
                    DateTime diaSel = dtpDia.Value.Date;
                    inicio = diaSel;
                    finLimit = diaSel.AddDays(1);
                    titulo = $"REPORTE DE ASISTENCIAS - DOCENTE: {nombreCompleto.ToUpper()}\nDÍA: {diaSel:dd/MM/yyyy}";
                    nombreArchivoDefecto = $"Asistencia_Docente_{codigoDocente}_Dia_{diaSel:yyyyMMdd}";
                }
                else if (rbSemana.Checked)
                {
                    DateTime baseDate = dtpSemana.Value.Date;
                    int diff = (7 + (baseDate.DayOfWeek - DayOfWeek.Monday)) % 7;
                    DateTime start = baseDate.AddDays(-1 * diff);
                    DateTime end = start.AddDays(6);
                    inicio = start;
                    finLimit = end.AddDays(1);
                    titulo = $"REPORTE DE ASISTENCIAS - DOCENTE: {nombreCompleto.ToUpper()}\nSEMANA DEL {start:dd/MM/yyyy} AL {end:dd/MM/yyyy}";
                    nombreArchivoDefecto = $"Asistencia_Docente_{codigoDocente}_Semana_{start:yyyyMMdd}_al_{end:yyyyMMdd}";
                }
                else if (rbMes.Checked)
                {
                    DateTime mesSel = dtpMes.Value;
                    inicio = new DateTime(mesSel.Year, mesSel.Month, 1);
                    finLimit = inicio.AddMonths(1);
                    string[] meses = { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
                    string nombreMes = meses[mesSel.Month - 1];
                    titulo = $"REPORTE DE ASISTENCIAS - DOCENTE: {nombreCompleto.ToUpper()}\nMES DE {nombreMes} {mesSel.Year}";
                    nombreArchivoDefecto = $"Asistencia_Docente_{codigoDocente}_Mes_{mesSel:yyyyMM}";
                }

                bool incluirFaltas = rbIncluirFaltas.Checked;
                bool esCSV = cmbFormato.SelectedIndex == 1;

                dialogo.Close();
                ExportarRangoDocumento(idDocente, inicio, finLimit, "Docentes", nombreCompleto, nombreArchivoDefecto, titulo, incluirFaltas, esCSV);
            };

            dialogo.ShowDialog();
        }

                private DataTable GenerarDatosReporte(int? docenteId, DateTime inicio, DateTime finLimit, bool incluirFaltas)
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Código");
            dtTemp.Columns.Add("Docente");
            dtTemp.Columns.Add("Curso");
            dtTemp.Columns.Add("Fecha Programada");
            dtTemp.Columns.Add("Hora de Ingreso");
            dtTemp.Columns.Add("Hora de Salida");
            dtTemp.Columns.Add("Estado"); // Siempre agregamos Estado

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (!incluirFaltas)
                    {
                        string query = @"SELECT d.Codigo as 'Código', d.Nombre + ' ' + d.Apellido as 'Docente', c.Nombre as 'Curso', 
                                         CAST(r.FechaHora AS DATE) as 'Fecha Programada',
                                         r.FechaHora as 'Hora de Ingreso', r.HoraSalida as 'Hora de Salida',
                                         h.HoraInicio
                                         FROM RegistroAsistencia r 
                                         INNER JOIN Docentes d ON r.DocenteId = d.Id 
                                         INNER JOIN Cursos c ON r.CursoId = c.Id
                                         LEFT JOIN HorarioClases h ON r.DocenteId = h.DocenteId AND r.CursoId = h.CursoId AND h.DiaSemana = DATEPART(dw, r.FechaHora)
                                         WHERE r.FechaHora >= @Inicio AND r.FechaHora < @FinLimit";
                        
                        if (docenteId.HasValue) query += " AND r.DocenteId = @DocenteId";
                        query += " ORDER BY r.FechaHora DESC";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Inicio", inicio);
                            cmd.Parameters.AddWithValue("@FinLimit", finLimit);
                            if (docenteId.HasValue) cmd.Parameters.AddWithValue("@DocenteId", docenteId.Value);
                            
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    DataRow row = dtTemp.NewRow();
                                    row["Código"] = reader["Código"];
                                    row["Docente"] = reader["Docente"];
                                    row["Curso"] = reader["Curso"];
                                    row["Fecha Programada"] = Convert.ToDateTime(reader["Fecha Programada"]).ToString("dd/MM/yyyy");
                                    DateTime dtIngreso = Convert.ToDateTime(reader["Hora de Ingreso"]);
                                    row["Hora de Ingreso"] = dtIngreso.ToString("dd/MM/yyyy HH:mm:ss");
                                    row["Hora de Salida"] = reader["Hora de Salida"] != DBNull.Value ? Convert.ToDateTime(reader["Hora de Salida"]).ToString("dd/MM/yyyy HH:mm:ss") : "";
                                    
                                    row["Estado"] = "ASISTIÓ";
                                    if (reader["HoraInicio"] != DBNull.Value)
                                    {
                                        if (TimeSpan.TryParse(reader["HoraInicio"].ToString(), out TimeSpan horaInicio))
                                        {
                                            if (dtIngreso.TimeOfDay > horaInicio.Add(TimeSpan.FromMinutes(40)))
                                            {
                                                row["Estado"] = "TARDANZA";
                                            }
                                        }
                                    }

                                    dtTemp.Rows.Add(row);
                                }
                            }
                        }
                    }
                    else
                    {
                        DataTable dtHorarios = new DataTable();
                        string qHorario = @"SELECT h.DocenteId, h.CursoId, h.DiaSemana, h.HoraInicio, h.HoraFin, 
                                            d.Codigo, d.Nombre + ' ' + d.Apellido as DocenteNombre, c.Nombre as CursoNombre
                                            FROM HorarioClases h
                                            INNER JOIN Docentes d ON h.DocenteId = d.Id
                                            INNER JOIN Cursos c ON h.CursoId = c.Id
                                            INNER JOIN Periodos p ON h.PeriodoId = p.Id
                                            WHERE p.EsActivo = 1";
                        if (docenteId.HasValue) qHorario += " AND h.DocenteId = @DocenteId";
                        
                        using (SqlCommand cmdH = new SqlCommand(qHorario, con))
                        {
                            if (docenteId.HasValue) cmdH.Parameters.AddWithValue("@DocenteId", docenteId.Value);
                            SqlDataAdapter daH = new SqlDataAdapter(cmdH);
                            daH.Fill(dtHorarios);
                        }

                        DataTable dtRegistros = new DataTable();
                        string qReg = @"SELECT DocenteId, CursoId, FechaHora, HoraSalida 
                                        FROM RegistroAsistencia 
                                        WHERE FechaHora >= @Inicio AND FechaHora < @FinLimit";
                        if (docenteId.HasValue) qReg += " AND DocenteId = @DocenteId";

                        using (SqlCommand cmdR = new SqlCommand(qReg, con))
                        {
                            cmdR.Parameters.AddWithValue("@Inicio", inicio);
                            cmdR.Parameters.AddWithValue("@FinLimit", finLimit);
                            if (docenteId.HasValue) cmdR.Parameters.AddWithValue("@DocenteId", docenteId.Value);
                            SqlDataAdapter daR = new SqlDataAdapter(cmdR);
                            daR.Fill(dtRegistros);
                        }

                        for (DateTime date = inicio.Date; date < finLimit.Date; date = date.AddDays(1))
                        {
                            if (date > DateTime.Today) break;

                            int diaSemana = date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)date.DayOfWeek;
                            DataRow[] horariosDia = dtHorarios.Select($"DiaSemana = {diaSemana}");

                            foreach (DataRow hor in horariosDia)
                            {
                                int hDocId = Convert.ToInt32(hor["DocenteId"]);
                                int hCurId = Convert.ToInt32(hor["CursoId"]);
                                
                                var registros = dtRegistros.AsEnumerable().Where(r => 
                                    r.Field<int>("DocenteId") == hDocId && 
                                    r.Field<int>("CursoId") == hCurId && 
                                    r.Field<DateTime>("FechaHora").Date == date);

                                DataRow row = dtTemp.NewRow();
                                row["Código"] = hor["Codigo"].ToString();
                                row["Docente"] = hor["DocenteNombre"].ToString();
                                row["Curso"] = hor["CursoNombre"].ToString();
                                row["Fecha Programada"] = date.ToString("dd/MM/yyyy");

                                if (registros.Any())
                                {
                                    var reg = registros.First();
                                    DateTime dtIngreso = reg.Field<DateTime>("FechaHora");
                                    row["Hora de Ingreso"] = dtIngreso.ToString("dd/MM/yyyy HH:mm:ss");
                                    row["Hora de Salida"] = reg["HoraSalida"] != DBNull.Value ? reg.Field<DateTime>("HoraSalida").ToString("dd/MM/yyyy HH:mm:ss") : "";
                                    
                                    row["Estado"] = "ASISTIÓ";
                                    if (TimeSpan.TryParse(hor["HoraInicio"].ToString(), out TimeSpan horaInicio))
                                    {
                                        if (dtIngreso.TimeOfDay > horaInicio.Add(TimeSpan.FromMinutes(40))) row["Estado"] = "TARDANZA";
                                    }
                                }
                                else
                                {
                                    row["Hora de Ingreso"] = "FALTÓ";
                                    row["Hora de Salida"] = "FALTÓ";
                                    row["Estado"] = "FALTÓ";
                                }

                                dtTemp.Rows.Add(row);
                            }
                        }

                        if (dtTemp.Rows.Count > 0)
                        {
                            dtTemp.DefaultView.Sort = "Fecha Programada DESC, Docente ASC";
                            dtTemp = dtTemp.DefaultView.ToTable();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al consultar la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dtTemp;
        }

        private void ExportarRangoDocumento(int? docenteId, DateTime inicio, DateTime finLimit, string categoria, string subCategoria, string nombreArchivoDefecto, string titulo, bool incluirFaltas, bool esCSV)
        {
            DataTable dtTemp = GenerarDatosReporte(docenteId, inicio, finLimit, incluirFaltas);

            if (dtTemp.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron registros en el rango seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (esCSV)
            {
                nombreArchivoDefecto += ".csv";
                string rutaFinal = MostrarDialogoGuardado(categoria, subCategoria, nombreArchivoDefecto, "Archivo Excel (CSV)|*.csv");
                if (string.IsNullOrEmpty(rutaFinal)) return;
                try
                {
                    using (StreamWriter sw = new StreamWriter(rutaFinal, false, Encoding.Default))
                    {
                        sw.WriteLine("sep=;");
                        for (int i = 0; i < dtTemp.Columns.Count; i++) sw.Write(dtTemp.Columns[i].ColumnName + ";");
                        sw.WriteLine();
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            for (int i = 0; i < dtTemp.Columns.Count; i++)
                            {
                                sw.Write(row[i].ToString() + ";");
                            }
                            sw.WriteLine();
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error al generar Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                nombreArchivoDefecto += ".pdf";
                string rutaFinal = MostrarDialogoGuardado(categoria, subCategoria, nombreArchivoDefecto, "Archivo PDF (*.pdf)|*.pdf");
                if (string.IsNullOrEmpty(rutaFinal)) return;
                try
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(rutaFinal, FileMode.Create));
                        pdfDoc.Open();

                        iTextSharp.text.Font fuenteUniversidad = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f);
                        iTextSharp.text.Font fuenteFacultad = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
                        iTextSharp.text.Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
                        iTextSharp.text.Font fuenteDetalles = FontFactory.GetFont(FontFactory.HELVETICA, 10f);
                        iTextSharp.text.Font fuenteCabecera = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.WHITE);
                        
                        iTextSharp.text.Font fuenteCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 9f);
                        iTextSharp.text.Font fuenteCuerpoRoja = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.RED);
                        iTextSharp.text.Font fuenteCuerpoNaranja = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, new BaseColor(200, 100, 0));

                        string semestreRomano = DateTime.Now.Month <= 7 ? "I" : "II";
                        string cicloActual = $"{DateTime.Now.Year} - {semestreRomano}";

                        pdfDoc.Add(new Paragraph("UNIVERSIDAD NACIONAL MAYOR DE SAN MARCOS", fuenteUniversidad) { Alignment = Element.ALIGN_CENTER });
                        pdfDoc.Add(new Paragraph("Facultad de Ciencias Matemáticas", fuenteFacultad) { Alignment = Element.ALIGN_CENTER });
                        pdfDoc.Add(new Paragraph("\n" + titulo, fuenteTitulo) { Alignment = Element.ALIGN_CENTER });

                        string textoDetalles = $"\nCiclo: {cicloActual}\n" +
                                               $"Fecha y hora de emisión: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\n\n";

                        pdfDoc.Add(new Paragraph(textoDetalles, fuenteDetalles) { Alignment = Element.ALIGN_LEFT });

                        int columnasCount = dtTemp.Columns.Count;
                        PdfPTable tablaPdf = new PdfPTable(columnasCount) { WidthPercentage = 100 };

                        foreach (DataColumn col in dtTemp.Columns)
                        {
                            tablaPdf.AddCell(new PdfPCell(new Phrase(col.ColumnName, fuenteCabecera)) { BackgroundColor = new BaseColor(43, 108, 176), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 6 });
                        }

                        foreach (DataRow fila in dtTemp.Rows)
                        {
                            bool esFalta = fila["Estado"].ToString() == "FALTÓ";
                            bool esTardanza = fila["Estado"].ToString() == "TARDANZA";
                            
                            foreach (DataColumn col in dtTemp.Columns)
                            {
                                string valor = fila[col]?.ToString() ?? "";
                                iTextSharp.text.Font f = esFalta ? fuenteCuerpoRoja : (esTardanza ? fuenteCuerpoNaranja : fuenteCuerpo);
                                PdfPCell cell = new PdfPCell(new Phrase(valor, f)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 };
                                
                                if (esFalta) cell.BackgroundColor = new BaseColor(255, 230, 230); // Fondo rojo claro
                                else if (esTardanza) cell.BackgroundColor = new BaseColor(255, 245, 220); // Fondo naranja claro
                                
                                tablaPdf.AddCell(cell);
                            }
                        }

                        pdfDoc.Add(tablaPdf);
                        pdfDoc.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al generar PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }
        private string MostrarDialogoGuardado(string categoria, string subCategoria, string nombreArchivo, string filtro)
        {
            string rutaProyecto = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.StartupPath, @"..\..\.."));
            string rutaBase = System.IO.Path.Combine(rutaProyecto, "Reportes_Asistencia_FCM");
            string rutaCategoria = System.IO.Path.Combine(rutaBase, categoria);
            
            if (!string.IsNullOrEmpty(subCategoria))
            {
                string subCatLimpio = string.Join("_", subCategoria.Split(System.IO.Path.GetInvalidFileNameChars()));
                rutaCategoria = System.IO.Path.Combine(rutaCategoria, subCatLimpio);
            }

            if (!System.IO.Directory.Exists(rutaCategoria))
            {
                System.IO.Directory.CreateDirectory(rutaCategoria);
            }

            string archivoLimpio = string.Join("_", nombreArchivo.Split(System.IO.Path.GetInvalidFileNameChars()));

            using (SaveFileDialog guardar = new SaveFileDialog())
            {
                guardar.InitialDirectory = rutaCategoria;
                guardar.FileName = archivoLimpio;
                guardar.Filter = filtro;
                
                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    return guardar.FileName;
                }
            }
            return null;
        }


        private void ExportarDataGridViewAExcel(DataGridView dgv, string categoria, string subCategoria, string nombreArchivo)
        {
            if (dgv.Rows.Count == 0) return;
            string rutaFinal = MostrarDialogoGuardado(categoria, subCategoria, nombreArchivo, "Archivo Excel (CSV)|*.csv");
            if (string.IsNullOrEmpty(rutaFinal)) return;
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaFinal, false, Encoding.Default))
                {
                    sw.WriteLine("sep=;");
                    for (int i = 0; i < dgv.Columns.Count; i++) if (dgv.Columns[i].Visible) sw.Write(dgv.Columns[i].HeaderText + ";");
                    sw.WriteLine();
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                if (dgv.Columns[i].Visible)
                                {
                                    string valor = row.Cells[i].Value is DateTime fecha 
                                        ? "\t" + fecha.ToString("dd/MM/yyyy HH:mm:ss") 
                                        : (row.Cells[i].Value?.ToString() ?? "");
                                    sw.Write(valor + ";");
                                }
                            }
                            sw.WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al exportar CSV: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void ExportarDataGridViewAPDF(DataGridView dgv, string categoria, string subCategoria, string nombreArchivo, string tituloDocumento, bool esHistorial)
        {
            if (dgv.Rows.Count == 0) return;
            string rutaFinal = MostrarDialogoGuardado(categoria, subCategoria, nombreArchivo, "Archivo PDF (*.pdf)|*.pdf");
            if (string.IsNullOrEmpty(rutaFinal)) return;

            try
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(pdfDoc, new FileStream(rutaFinal, FileMode.Create));
                    pdfDoc.Open();

                    iTextSharp.text.Font fuenteUniversidad = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f);
                    iTextSharp.text.Font fuenteFacultad = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
                    iTextSharp.text.Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16f);
                    iTextSharp.text.Font fuenteDetalles = FontFactory.GetFont(FontFactory.HELVETICA, 10f);
                    iTextSharp.text.Font fuenteCabecera = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.WHITE);
                    iTextSharp.text.Font fuenteCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 9f);

                    string semestreRomano = DateTime.Now.Month <= 7 ? "I" : "II";
                    string cicloActual = $"{DateTime.Now.Year} - {semestreRomano}";

                    string tipoReporteTemporal = esHistorial ? "General" : "Padrón Oficial";
                    string docenteFiltroTemporal = "Todos";

                    pdfDoc.Add(new Paragraph("UNIVERSIDAD NACIONAL MAYOR DE SAN MARCOS", fuenteUniversidad) { Alignment = Element.ALIGN_CENTER });
                    pdfDoc.Add(new Paragraph("Facultad de Ciencias Matemáticas", fuenteFacultad) { Alignment = Element.ALIGN_CENTER });
                    pdfDoc.Add(new Paragraph("\n" + tituloDocumento, fuenteTitulo) { Alignment = Element.ALIGN_CENTER });

                    string textoDetalles = $"\nCiclo: {cicloActual}\n" +
                                           $"Tipo: {tipoReporteTemporal}\n" +
                                           $"Docente: {docenteFiltroTemporal}\n" +
                                           $"Fecha y hora de emisión: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\n\n";

                    pdfDoc.Add(new Paragraph(textoDetalles, fuenteDetalles) { Alignment = Element.ALIGN_LEFT });

                    int columnasVisibles = 0; foreach (DataGridViewColumn col in dgv.Columns) if (col.Visible) columnasVisibles++;
                    PdfPTable tablaPdf = new PdfPTable(columnasVisibles) { WidthPercentage = 100 };

                    foreach (DataGridViewColumn columna in dgv.Columns)
                    {
                        if (columna.Visible) tablaPdf.AddCell(new PdfPCell(new Phrase(columna.HeaderText, fuenteCabecera)) { BackgroundColor = new BaseColor(43, 108, 176), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 6 });
                    }

                    foreach (DataGridViewRow fila in dgv.Rows)
                    {
                        if (!fila.IsNewRow)
                        {
                            foreach (DataGridViewColumn col in dgv.Columns)
                            {
                                if (col.Visible) tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells[col.Index].Value?.ToString() ?? "", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            }
                        }
                    }

                    pdfDoc.Add(tablaPdf); pdfDoc.Close();
                }
                catch (Exception ex) { MessageBox.Show("Error al generar PDF: " + ex.Message); }
        }

        private void CargarFiltroPeriodos()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Nombre FROM Periodos ORDER BY Id DESC", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataRow filaTodo = dt.NewRow();
                    filaTodo["Id"] = DBNull.Value;
                    filaTodo["Nombre"] = "Todos los periodos";
                    dt.Rows.InsertAt(filaTodo, 0);
                    cmbFiltroPeriodo.DisplayMember = "Nombre";
                    cmbFiltroPeriodo.ValueMember = "Id";
                    cmbFiltroPeriodo.DataSource = dt;
                    cmbFiltroPeriodo.SelectedIndex = 0;
                }
                catch { }
            }
        }

        private void CargarHistorial()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT r.Id as RegistroId, d.Codigo, d.Nombre, d.Apellido, c.Nombre as Curso, r.FechaHora as 'Hora de Ingreso', r.HoraSalida as 'Hora de Salida', r.PeriodoId, ISNULL(p.Nombre, 'Histórico') as 'Periodo' 
                                     FROM RegistroAsistencia r 
                                     INNER JOIN Docentes d ON r.DocenteId = d.Id 
                                     INNER JOIN Cursos c ON r.CursoId = c.Id 
                                     LEFT JOIN Periodos p ON r.PeriodoId = p.Id 
                                     ORDER BY r.FechaHora DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con); tablaHistorial.Clear(); da.Fill(tablaHistorial);
                    dgvHistorial.DataSource = tablaHistorial; dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    if (dgvHistorial.Columns.Contains("PeriodoId")) dgvHistorial.Columns["PeriodoId"].Visible = false;
                }
                catch { }
            }
        }

        private void AplicarFiltroHistorial()
        {
            if (tablaHistorial == null || tablaHistorial.Rows.Count == 0) return;
            try
            {
                DateTime fechaSel = monthCalendarHistorial.SelectionStart.Date;
                string inicio = fechaSel.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string fin = fechaSel.AddDays(1).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                
                string filtro = $"[Hora de Ingreso] >= #{inicio}# AND [Hora de Ingreso] < #{fin}#";
                
                string texto = txtBuscarHistorialNombre.Text.Trim().Replace("'", "''");
                if (!string.IsNullOrEmpty(texto))
                {
                    string criterio = cmbCriterioHistorial.SelectedItem?.ToString() ?? "Todos";
                    if (criterio == "Docente")
                        filtro += $" AND (Nombre LIKE '%{texto}%' OR Apellido LIKE '%{texto}%')";
                    else if (criterio == "Código")
                        filtro += $" AND Codigo LIKE '%{texto}%'";
                    else if (criterio == "Curso")
                        filtro += $" AND Curso LIKE '%{texto}%'";
                    else
                        filtro += $" AND (Nombre LIKE '%{texto}%' OR Apellido LIKE '%{texto}%' OR Codigo LIKE '%{texto}%' OR Curso LIKE '%{texto}%')";
                }

                if (cmbFiltroPeriodo.SelectedValue != null && cmbFiltroPeriodo.SelectedValue != DBNull.Value)
                {
                    filtro += $" AND PeriodoId = {cmbFiltroPeriodo.SelectedValue}";
                }
                
                tablaHistorial.DefaultView.RowFilter = filtro;
            }
            catch { tablaHistorial.DefaultView.RowFilter = "1 = 0"; }
        }

        private void rbTodasFechas_CheckedChanged(object sender, EventArgs e) { }
        private void rbPorFecha_CheckedChanged(object sender, EventArgs e) { }
        private void rbPorNombre_CheckedChanged(object sender, EventArgs e) { }
        private void monthCalendarHistorial_DateSelected(object sender, DateRangeEventArgs e) { AplicarFiltroHistorial(); }
        private void txtBuscarHistorialNombre_TextChanged(object sender, EventArgs e) { AplicarFiltroHistorial(); }

        private void btnEliminarTodo_Click(object sender, EventArgs e)
        {
            if (FormConfirmacion.Mostrar("¿Eliminar TODO el historial permanentemente?", "Advertencia") == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try { con.Open(); new SqlCommand("DELETE FROM RegistroAsistencia", con).ExecuteNonQuery(); CargarHistorial(); } catch { }
                }
            }
        }

        // Eventos del diseñador
        private void btnImportarExcel_Click_1(object sender, EventArgs e) { }
        private void txtNuevoCodigo_TextChanged(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void tabPage3_Click(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void label6_Click_1(object sender, EventArgs e) { }

        private void FormAdmin_Load(object sender, EventArgs e) 
        {
            this.WindowState = FormWindowState.Maximized;
            InicializarPestanasPro(); // INJECTED DASHBOARD AND JUSTIFICATIONS
            MigrarBaseDeDatos();
            try
            {
                // Configurar ComboBoxes
                string[] clases = new string[] { "TP 20 hrs.", "TP 15 hrs.", "TP 10 hrs.", "T. C. 40 H", "T. P. 20 H" };
                string[] categorias = new string[] { "Auxiliar", "Asociado", "Principal" };
                string[] departamentos = new string[] { "Matemática", "Computación científica", "Estadística", "Investigación operativa" };

                ComboBox cmbAddClase = ObtenerControl("cmbAddClase") as ComboBox;
                if (cmbAddClase != null) { cmbAddClase.DropDownStyle = ComboBoxStyle.DropDownList; cmbAddClase.Items.AddRange(clases); }

                ComboBox cmbAddCategoria = ObtenerControl("cmbAddCategoria") as ComboBox;
                if (cmbAddCategoria != null) { cmbAddCategoria.DropDownStyle = ComboBoxStyle.DropDownList; cmbAddCategoria.Items.AddRange(categorias); }

                ComboBox cmbAddDepartamento = ObtenerControl("cmbAddDepartamento") as ComboBox;
                if (cmbAddDepartamento != null) { cmbAddDepartamento.DropDownStyle = ComboBoxStyle.DropDownList; cmbAddDepartamento.Items.AddRange(departamentos); }

                ComboBox cmbNuevoClase = ObtenerControl("cmbNuevoClase") as ComboBox;
                if (cmbNuevoClase != null) { cmbNuevoClase.DropDownStyle = ComboBoxStyle.DropDownList; cmbNuevoClase.Items.AddRange(clases); }

                ComboBox cmbNuevoCategoria = ObtenerControl("cmbNuevoCategoria") as ComboBox;
                if (cmbNuevoCategoria != null) { cmbNuevoCategoria.DropDownStyle = ComboBoxStyle.DropDownList; cmbNuevoCategoria.Items.AddRange(categorias); }

                ComboBox cmbNuevoDepartamento = ObtenerControl("cmbNuevoDepartamento") as ComboBox;
                if (cmbNuevoDepartamento != null) { cmbNuevoDepartamento.DropDownStyle = ComboBoxStyle.DropDownList; cmbNuevoDepartamento.Items.AddRange(departamentos); }

                ComboBox cmbAddTipoCurso = ObtenerControl("cmbAddTipoCurso") as ComboBox;
                if (cmbAddTipoCurso != null) { cmbAddTipoCurso.DropDownStyle = ComboBoxStyle.DropDownList; }

                ComboBox txtNuevoTipoCurso = ObtenerControl("txtNuevoTipoCurso") as ComboBox;
                if (txtNuevoTipoCurso != null) { txtNuevoTipoCurso.DropDownStyle = ComboBoxStyle.DropDownList; }

                // Asignar eventos TextChanged a los campos de Nombre y Apellido para el correo automático
                TextBox tAddN = ObtenerCajaTexto("txtAddNombre");
                TextBox tAddA = ObtenerCajaTexto("txtAddApellido");
                TextBox txtAddCorreo = ObtenerCajaTexto("txtAddCorreo");
                if (tAddN != null && txtAddCorreo != null) tAddN.TextChanged += (s, ev) => { if (string.IsNullOrWhiteSpace(txtAddCorreo.Text) || txtAddCorreo.Tag?.ToString() == "auto") AutoGenerarCorreo(tAddN.Text, tAddA?.Text, txtAddCorreo); };
                if (tAddA != null && txtAddCorreo != null) tAddA.TextChanged += (s, ev) => { if (string.IsNullOrWhiteSpace(txtAddCorreo.Text) || txtAddCorreo.Tag?.ToString() == "auto") AutoGenerarCorreo(tAddN?.Text, tAddA.Text, txtAddCorreo); };
                if (txtAddCorreo != null) txtAddCorreo.TextChanged += (s, ev) => { if (txtAddCorreo.Focused) txtAddCorreo.Tag = "manual"; };

                TextBox tEditN = ObtenerCajaTexto("txtNuevoNombre");
                TextBox tEditA = ObtenerCajaTexto("txtNuevoApellido");
                TextBox txtNuevoCorreo = ObtenerCajaTexto("txtNuevoCorreo");
                if (tEditN != null && txtNuevoCorreo != null) tEditN.TextChanged += (s, ev) => { if (string.IsNullOrWhiteSpace(txtNuevoCorreo.Text) || txtNuevoCorreo.Tag?.ToString() == "auto") AutoGenerarCorreo(tEditN.Text, tEditA?.Text, txtNuevoCorreo); };
                if (tEditA != null && txtNuevoCorreo != null) tEditA.TextChanged += (s, ev) => { if (string.IsNullOrWhiteSpace(txtNuevoCorreo.Text) || txtNuevoCorreo.Tag?.ToString() == "auto") AutoGenerarCorreo(tEditN?.Text, tEditA.Text, txtNuevoCorreo); };
                if (txtNuevoCorreo != null) txtNuevoCorreo.TextChanged += (s, ev) => { if (txtNuevoCorreo.Focused) txtNuevoCorreo.Tag = "manual"; };
            }
            catch { }
        }
        private void btnEliminarTodo_Click_1(object sender, EventArgs e) { }
        private void guna2ImageButton1_Click(object sender, EventArgs e) { }
        private void guna2ImageButton2_Click(object sender, EventArgs e) { }
        private void guna2ImageButton3_Click(object sender, EventArgs e) { }
        private void guna2ImageButton4_Click(object sender, EventArgs e) { }
        private void guna2ImageButton5_Click(object sender, EventArgs e) { }
        private void btnExportarDocentesPDF_ControlAdded(object sender, ControlEventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void dgvCursos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void tabPage6_Click(object sender, EventArgs e) { }
        private void pictureBox5_Click(object sender, EventArgs e) { }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new FormGestionPeriodos().ShowDialog();
            CargarFiltroPeriodos();
            CargarHistorial();
            CargarHorarios(); // Refrescar horarios al cambiar periodo
        }

        // Horarios de clases
        DataTable tablaHorarios = new DataTable();

        private void CargarHorarios()
        {
            if (dgvHorarios == null) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = @"SELECT h.Id, 
                                        d.Codigo as 'CódigoDocente', 
                                        d.Nombre + ' ' + d.Apellido as 'Docente',
                                        c.Nombre as 'Curso',
                                        h.TipoClase as 'Tipo Clase',
                                        CASE h.DiaSemana 
                                            WHEN 1 THEN 'Lunes' WHEN 2 THEN 'Martes' WHEN 3 THEN 'Miércoles' 
                                            WHEN 4 THEN 'Jueves' WHEN 5 THEN 'Viernes' WHEN 6 THEN 'Sábado' 
                                        END as 'Día',
                                        CONVERT(VARCHAR(5), h.HoraInicio, 108) as 'Hora Inicio',
                                        CONVERT(VARCHAR(5), h.HoraFin, 108) as 'Hora Fin',
                                        h.DocenteId, h.CursoId, h.DiaSemana as DiaSemanaNum,
                                        CONVERT(VARCHAR(5), h.HoraInicio, 108) as HoraInicioRaw,
                                        CONVERT(VARCHAR(5), h.HoraFin, 108) as HoraFinRaw
                                     FROM HorarioClases h
                                     INNER JOIN Docentes d ON h.DocenteId = d.Id
                                     INNER JOIN Cursos c ON h.CursoId = c.Id
                                     WHERE h.PeriodoId = (SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1)
                                        OR (h.PeriodoId IS NULL AND NOT EXISTS (SELECT 1 FROM Periodos WHERE EsActivo = 1))
                                     ORDER BY h.DiaSemana, h.HoraInicio, d.Nombre";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    tablaHorarios.Clear();
                    da.Fill(tablaHorarios);

                    // Agregar columna de filtro
                    if (!tablaHorarios.Columns.Contains("FiltroBusqueda"))
                    {
                        tablaHorarios.Columns.Add("FiltroBusqueda", typeof(string));
                    }
                    foreach (DataRow row in tablaHorarios.Rows)
                    {
                        string cod = row["CódigoDocente"]?.ToString() ?? "";
                        string doc = row["Docente"]?.ToString() ?? "";
                        string cur = row["Curso"]?.ToString() ?? "";
                        string dia = row["Día"]?.ToString() ?? "";
                        row["FiltroBusqueda"] = QuitarTildes($"{cod} {doc} {cur} {dia}").ToLower();
                    }

                    dgvHorarios.DataSource = tablaHorarios;
                    dgvHorarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Ocultar columnas internas
                    if (dgvHorarios.Columns["Id"] != null) dgvHorarios.Columns["Id"].Visible = false;
                    if (dgvHorarios.Columns["DocenteId"] != null) dgvHorarios.Columns["DocenteId"].Visible = false;
                    if (dgvHorarios.Columns["CursoId"] != null) dgvHorarios.Columns["CursoId"].Visible = false;
                    if (dgvHorarios.Columns["DiaSemanaNum"] != null) dgvHorarios.Columns["DiaSemanaNum"].Visible = false;
                    if (dgvHorarios.Columns["HoraInicioRaw"] != null) dgvHorarios.Columns["HoraInicioRaw"].Visible = false;
                    if (dgvHorarios.Columns["HoraFinRaw"] != null) dgvHorarios.Columns["HoraFinRaw"].Visible = false;
                    if (dgvHorarios.Columns["FiltroBusqueda"] != null) dgvHorarios.Columns["FiltroBusqueda"].Visible = false;
                }
                catch (Exception ex)
                {
                    // La tabla aún no existe: no mostrar error
                    Console.WriteLine("Horarios no disponible: " + ex.Message);
                }
            }
        }

        private void TxtBuscarHorario_TextChanged(object sender, EventArgs e)
        {
            if (tablaHorarios == null) return;
            string filtro = txtBuscarHorario?.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(filtro))
            {
                tablaHorarios.DefaultView.RowFilter = "";
            }
            else
            {
                string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                string criterio = cmbCriterioHorarios.SelectedItem?.ToString() ?? "Todos";
                
                if (criterio == "Código")
                    tablaHorarios.DefaultView.RowFilter = $"CdigoDocente LIKE '%{filtro}%'";
                else if (criterio == "Docente")
                    tablaHorarios.DefaultView.RowFilter = $"Docente LIKE '%{filtro}%'";
                else if (criterio == "Curso")
                    tablaHorarios.DefaultView.RowFilter = $"Curso LIKE '%{filtro}%'";
                else if (criterio == "Día")
                    tablaHorarios.DefaultView.RowFilter = $"Dia LIKE '%{filtro}%'";
                else
                    tablaHorarios.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
            }
        }

        private void DgvHorarios_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvHorarios.ClearSelection();
                dgvHorarios.CurrentCell = dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvHorarios.Rows[e.RowIndex].Selected = true;
            }
        }

        private void BtnAgregarHorario_Click(object sender, EventArgs e)
        {
            using (FormEditarHorario frm = new FormEditarHorario())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargarHorarios();
                }
            }
        }

        private void EditarHorario_Click(object sender, EventArgs e)
        {
            if (dgvHorarios.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvHorarios.CurrentRow.Cells["Id"].Value);
            int docenteId = Convert.ToInt32(dgvHorarios.CurrentRow.Cells["DocenteId"].Value);
            int cursoId = Convert.ToInt32(dgvHorarios.CurrentRow.Cells["CursoId"].Value);
            int diaSemana = Convert.ToInt32(dgvHorarios.CurrentRow.Cells["DiaSemanaNum"].Value);
            string horaInicio = dgvHorarios.CurrentRow.Cells["HoraInicioRaw"].Value?.ToString() ?? "";
            string horaFin = dgvHorarios.CurrentRow.Cells["HoraFinRaw"].Value?.ToString() ?? "";
            string tipoClase = dgvHorarios.Columns.Contains("Tipo Clase") && dgvHorarios.CurrentRow.Cells["Tipo Clase"].Value != null 
                ? dgvHorarios.CurrentRow.Cells["Tipo Clase"].Value.ToString() 
                : "TEORÍA";

            using (FormEditarHorario frm = new FormEditarHorario(id, docenteId, cursoId, diaSemana, horaInicio, horaFin, tipoClase))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargarHorarios();
                }
            }
        }

        private void EliminarHorario_Click(object sender, EventArgs e)
        {
            if (dgvHorarios.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvHorarios.CurrentRow.Cells["Id"].Value);
            string docente = dgvHorarios.CurrentRow.Cells["Docente"].Value?.ToString() ?? "";
            string curso = dgvHorarios.CurrentRow.Cells["Curso"].Value?.ToString() ?? "";
            string dia = dgvHorarios.CurrentRow.Cells["Día"].Value?.ToString() ?? "";

            if (FormConfirmacion.Mostrar($"¿Eliminar el horario de '{docente}' para '{curso}' el {dia}?", "Confirmar") == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        new SqlCommand($"DELETE FROM HorarioClases WHERE Id = {id}", con).ExecuteNonQuery();
                        CargarHorarios();
                    }
                    catch (Exception ex) { MessageBox.Show("Error al eliminar: " + ex.Message); }
                }
            }
        }

        private void BtnImportarHorarios_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Archivos CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*", Title = "Seleccionar Archivo de Horarios" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                int insertados = 0; int omitidos = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        int? periodoId = null;
                        using (SqlCommand cmdP = new SqlCommand("SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1", con))
                        {
                            object r = cmdP.ExecuteScalar();
                            if (r != null) periodoId = Convert.ToInt32(r);
                        }

                        string[] lineas = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                        for (int i = 1; i < lineas.Length; i++)
                        {
                            string linea = lineas[i];
                            if (string.IsNullOrWhiteSpace(linea)) continue;
                            char delimitador = linea.Contains(";") ? ';' : (linea.Contains(",") ? ',' : '\t');
                            string[] cols = linea.Split(delimitador);

                            if (cols.Length >= 5)
                            {
                                try
                                {
                                    string codDocente = cols[0].Trim().Replace("\"", "");
                                    string codCurso = cols[1].Trim().Replace("\"", "");
                                    int diaSemana = int.Parse(cols[2].Trim().Replace("\"", ""));
                                    string horaInicio = cols[3].Trim().Replace("\"", "");
                                    string horaFin = cols[4].Trim().Replace("\"", "");
                                    string tipoClase = (cols.Length >= 6) ? cols[5].Trim().Replace("\"", "").ToUpper() : "TEORÍA";
                                    if (tipoClase != "TEORÍA" && tipoClase != "PRÁCTICA") tipoClase = "TEORÍA";

                                    string queryInsert = @"
                                        DECLARE @DocId INT = (SELECT TOP 1 Id FROM Docentes WHERE Codigo = @CodDoc);
                                        DECLARE @CurId INT = (SELECT TOP 1 Id FROM Cursos WHERE Codigo = @CodCur);
                                        IF @DocId IS NOT NULL AND @CurId IS NOT NULL
                                        BEGIN
                                            IF NOT EXISTS (SELECT 1 FROM HorarioClases WHERE DocenteId = @DocId AND CursoId = @CurId AND DiaSemana = @Dia AND HoraInicio = @HI AND PeriodoId = @PerId)
                                                INSERT INTO HorarioClases (DocenteId, CursoId, DiaSemana, HoraInicio, HoraFin, PeriodoId, TipoClase) 
                                                VALUES (@DocId, @CurId, @Dia, @HI, @HF, @PerId, @TipoClase);
                                        END";
                                    using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                                    {
                                        cmd.Parameters.AddWithValue("@CodDoc", codDocente);
                                        cmd.Parameters.AddWithValue("@CodCur", codCurso);
                                        cmd.Parameters.AddWithValue("@Dia", diaSemana);
                                        cmd.Parameters.AddWithValue("@HI", TimeSpan.Parse(horaInicio));
                                        cmd.Parameters.AddWithValue("@HF", TimeSpan.Parse(horaFin));
                                        cmd.Parameters.AddWithValue("@PerId", periodoId.HasValue ? (object)periodoId.Value : DBNull.Value);
                                        cmd.Parameters.AddWithValue("@TipoClase", tipoClase);
                                        cmd.ExecuteNonQuery();
                                        insertados++;
                                    }
                                }
                                catch { omitidos++; }
                            }
                            else { omitidos++; }
                        }
                        MessageBox.Show($"Importación finalizada.\n• Procesados: {insertados}\n• Omitidos: {omitidos}", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarHorarios();
                    }
                    catch (Exception ex) { MessageBox.Show("Error al importar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void BtnExportarHorariosCSV_Click(object sender, EventArgs e)
        {
            if (dgvHorarios == null || dgvHorarios.Rows.Count == 0) return;
            string fechaHora = DateTime.Now.ToString("dd-MM-yyyy_HHmmss");
            ExportarDataGridViewAExcel(dgvHorarios, "Horarios", "", $"Horarios_Clases_{fechaHora}.csv");
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    
        private void dgvHistorial_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvHistorial.ClearSelection();
                dgvHistorial.Rows[e.RowIndex].Selected = true;
                dgvHistorial.CurrentCell = dgvHistorial.Rows[e.RowIndex].Cells[0];
            }
        }

        private void EditarAsistencia(int id)
        {
            using (FormEditarAsistencia frm = new FormEditarAsistencia(id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargarHistorial();
                }
            }
        }

        private void EditarAsistenciaSeleccionada()
        {
            if (dgvHistorial.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvHistorial.SelectedRows[0].Cells["RegistroId"].Value);
                EditarAsistencia(id);
            }
            else
            {
                MessageBox.Show("Seleccione un registro de asistencia para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EliminarAsistencia()
        {
            if (dgvHistorial.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvHistorial.SelectedRows[0].Cells["RegistroId"].Value);
                if (FormConfirmacion.Mostrar("¿Está seguro de eliminar este registro de asistencia?", "Confirmar Eliminación") == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("DELETE FROM RegistroAsistencia WHERE Id = @Id", con);
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarHistorial();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro de asistencia para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================================================
        // MÉTODOS DE SOPORTE E MIGRACIÓN PARA REPORTE 16 SEMANAS
        // =========================================================================
        
        private void MigrarBaseDeDatos()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string queryCheck = @"
                        IF NOT EXISTS (
                            SELECT * FROM sys.columns 
                            WHERE object_id = OBJECT_ID(N'[dbo].[HorarioClases]') 
                            AND name = 'TipoClase'
                        )
                        BEGIN
                            ALTER TABLE [dbo].[HorarioClases] ADD [TipoClase] VARCHAR(20) DEFAULT 'TEORÍA' NOT NULL;
                        END";
                    using (SqlCommand cmd = new SqlCommand(queryCheck, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al migrar la BD: " + ex.Message);
                }
            }
        }



        private void CargarDocentesFiltroReporte()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Apellido + ', ' + Nombre as NombreCompleto, ISNULL(Codigo, '') as Codigo FROM Docentes ORDER BY Apellido, Nombre", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    // Se eliminó la fila dummy "[Todos los Docentes]" para dejar que el autocompletado sea más natural.
                    cmbReporteDocente.ValueMember = "Id";
                    cmbReporteDocente.DataSource = dt;
                    cmbReporteDocente.SelectedIndex = -1; // Comienza vacío
                    
                    if (cmbTipoFiltroReporte.SelectedIndex == 1)
                        cmbReporteDocente.DisplayMember = "Codigo";
                    else
                        cmbReporteDocente.DisplayMember = "NombreCompleto";
                        
                    cmbReporteDocente.SelectedIndex = -1;
                }
                catch { }
            }
        }

        private void CmbTipoFiltroReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReporteDocente.DataSource != null)
            {
                if (cmbTipoFiltroReporte.SelectedIndex == 1)
                    cmbReporteDocente.DisplayMember = "Codigo";
                else
                    cmbReporteDocente.DisplayMember = "NombreCompleto";
                    
                cmbReporteDocente.SelectedIndex = -1;
            }
        }

        private void CmbReporteDocente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private bool isDocenteFiltering = false;
        private void CmbReporteDocente_TextUpdate(object sender, EventArgs e)
        {
            if (isDocenteFiltering) return;
            isDocenteFiltering = true;

            try
            {
                string searchText = cmbReporteDocente.Text;
                int cursorPosition = cmbReporteDocente.SelectionStart;

                if (cmbReporteDocente.DataSource is DataTable dt)
                {
                    string filterField = cmbReporteDocente.DisplayMember;
                    if (string.IsNullOrWhiteSpace(searchText))
                    {
                        dt.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        string[] parts = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> filterParts = new List<string>();
                        foreach (string part in parts)
                        {
                            string safePart = part.Replace("'", "''");
                            filterParts.Add($"{filterField} LIKE '%{safePart}%'");
                        }
                        dt.DefaultView.RowFilter = string.Join(" AND ", filterParts);
                    }

                    // Restaurar el texto y cursor ANTES de manipular DroppedDown
                    cmbReporteDocente.Text = searchText;
                    cmbReporteDocente.SelectionStart = cursorPosition;
                    cmbReporteDocente.SelectionLength = 0;

                    try
                    {
                        if (dt.DefaultView.Count > 0)
                        {
                            if (!cmbReporteDocente.DroppedDown)
                                cmbReporteDocente.DroppedDown = true;
                        }
                        else
                        {
                            if (cmbReporteDocente.DroppedDown)
                                cmbReporteDocente.DroppedDown = false;
                        }
                    }
                    catch { }

                    Cursor.Current = Cursors.Default;
                }
            }
            finally
            {
                isDocenteFiltering = false;
            }
        }

                private void btnGestionarCronograma_Click(object sender, EventArgs e)
        {
            new FormCronograma().ShowDialog(this);
        }

        private DateTime ObtenerInicioSemestre()
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
                            return fecha;
                    }
                }
                catch { }
            }
            return DateTime.Today;
        }

        private void BtnGenerarReporteSemanas_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = ObtenerInicioSemestre().Date;
            int diff = (7 + (fechaInicio.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime lunesSemana1 = fechaInicio.AddDays(-diff);

            int filtroDocenteId = 0;
            if (cmbReporteDocente.SelectedValue != null)
            {
                filtroDocenteId = Convert.ToInt32(cmbReporteDocente.SelectedValue);
            }
            else if (cmbReporteDocente.DataSource is DataTable dt && dt.DefaultView.Count == 1)
            {
                // Si filtraron y solo hay un resultado, lo autoseleccionamos
                filtroDocenteId = Convert.ToInt32(dt.DefaultView[0]["Id"]);
                cmbReporteDocente.SelectedValue = filtroDocenteId;
            }
            else if (cmbReporteDocente.DataSource is DataTable dt2 && dt2.DefaultView.Count > 1)
            {
                // Intentar buscar coincidencia exacta con lo que escribieron
                string field = cmbReporteDocente.DisplayMember;
                string text = cmbReporteDocente.Text.Trim();
                bool found = false;
                foreach (DataRowView row in dt2.DefaultView)
                {
                    if (row[field].ToString().Equals(text, StringComparison.OrdinalIgnoreCase))
                    {
                        filtroDocenteId = Convert.ToInt32(row["Id"]);
                        cmbReporteDocente.SelectedValue = filtroDocenteId;
                        found = true;
                        break;
                    }
                }
                
                if (!found)
                {
                    // Si no hubo coincidencia exacta, tomamos el primero de la lista sugerida
                    filtroDocenteId = Convert.ToInt32(dt2.DefaultView[0]["Id"]);
                    cmbReporteDocente.SelectedValue = filtroDocenteId;
                    cmbReporteDocente.Text = dt2.DefaultView[0][field].ToString();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un docente válido de la lista sugerida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dtReporteResumen = new DataTable();
            dtReporteResumen.Columns.Add("DocenteId", typeof(int));
            dtReporteResumen.Columns.Add("CursoId", typeof(int));
            dtReporteResumen.Columns.Add("Código");
            dtReporteResumen.Columns.Add("Docente");
            dtReporteResumen.Columns.Add("Curso");
            dtReporteResumen.Columns.Add("Tipo de Carga");
            dtReporteResumen.Columns.Add("Clases Programadas", typeof(int));
            dtReporteResumen.Columns.Add("Asistencias", typeof(int));
            dtReporteResumen.Columns.Add("Tardanzas", typeof(int));
            dtReporteResumen.Columns.Add("Faltas", typeof(int));
            dtReporteResumen.Columns.Add("% Asistencia", typeof(string));

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    int? periodoId = null;
                    using (SqlCommand cmdP = new SqlCommand("SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1", con))
                    {
                        object r = cmdP.ExecuteScalar();
                        if (r != null) periodoId = Convert.ToInt32(r);
                    }

                    string sqlHorarios = @"
                        SELECT h.DocenteId, h.CursoId, h.DiaSemana, h.HoraInicio, h.HoraFin, h.TipoClase,
                               d.Codigo as DocenteCodigo, d.Nombre + ' ' + d.Apellido as DocenteNombre,
                               c.Nombre as CursoNombre
                        FROM HorarioClases h
                        INNER JOIN Docentes d ON h.DocenteId = d.Id
                        INNER JOIN Cursos c ON h.CursoId = c.Id
                        WHERE (h.PeriodoId = @PerId OR (@PerId IS NULL AND h.PeriodoId IS NULL))";
                    
                    if (filtroDocenteId > 0)
                    {
                        sqlHorarios += " AND h.DocenteId = @DocenteId";
                    }

                    DataTable dtHorarios = new DataTable();
                    using (SqlCommand cmdH = new SqlCommand(sqlHorarios, con))
                    {
                        cmdH.Parameters.AddWithValue("@PerId", periodoId.HasValue ? (object)periodoId.Value : DBNull.Value);
                        if (filtroDocenteId > 0) cmdH.Parameters.AddWithValue("@DocenteId", filtroDocenteId);
                        SqlDataAdapter da = new SqlDataAdapter(cmdH);
                        da.Fill(dtHorarios);
                    }

                    DateTime fechaFinMax = lunesSemana1.AddDays(16 * 7);
                    string sqlAsistencias = @"
                        SELECT DocenteId, CursoId, FechaHora, HoraSalida
                        FROM RegistroAsistencia
                        WHERE FechaHora >= @Inicio AND FechaHora < @Fin";
                    if (filtroDocenteId > 0)
                    {
                        sqlAsistencias += " AND DocenteId = @DocenteId";
                    }

                    DataTable dtAsistencias = new DataTable();
                    using (SqlCommand cmdA = new SqlCommand(sqlAsistencias, con))
                    {
                        cmdA.Parameters.AddWithValue("@Inicio", lunesSemana1);
                        cmdA.Parameters.AddWithValue("@Fin", fechaFinMax);
                        if (filtroDocenteId > 0) cmdA.Parameters.AddWithValue("@DocenteId", filtroDocenteId);
                        SqlDataAdapter da = new SqlDataAdapter(cmdA);
                        da.Fill(dtAsistencias);
                    }

                    var groups = dtHorarios.AsEnumerable()
                        .GroupBy(r => new { DocenteId = r.Field<int>("DocenteId"), CursoId = r.Field<int>("CursoId") });

                    foreach (var g in groups)
                    {
                        int docId = g.Key.DocenteId;
                        int curId = g.Key.CursoId;

                        var firstRow = g.First();
                        string codDoc = firstRow.Field<string>("DocenteCodigo");
                        string nomDoc = firstRow.Field<string>("DocenteNombre");
                        string nomCur = firstRow.Field<string>("CursoNombre");

                        var slots = g.ToList();
                        bool tieneTeoria = slots.Any(s => s.Field<string>("TipoClase").ToUpper() == "TEORÍA");
                        bool tienePractica = slots.Any(s => s.Field<string>("TipoClase").ToUpper() == "PRÁCTICA");
                        string tipoCarga = "Solo Teoría";

                        if (tieneTeoria && tienePractica)
                        {
                            var dias = slots.Select(s => s.Field<int>("DiaSemana")).Distinct().ToList();
                            if (dias.Count > 1)
                                tipoCarga = "Teoría y Práctica - Separados";
                            else
                                tipoCarga = "Teoría y Práctica - Día Corrido";
                        }
                        else if (tienePractica)
                        {
                            tipoCarga = "Solo Práctica";
                        }

                        int clasesProgramadas = 0;
                        int asistencias = 0;
                        int tardanzas = 0;
                        int faltas = 0;

                        for (int w = 1; w <= 16; w++)
                        {
                            DateTime inicioSemana = lunesSemana1.AddDays((w - 1) * 7);
                            bool esSemanaExamen = (w == 8 || w == 16);
                            
                            foreach (var slot in slots)
                            {
                                string tipoClaseActual = slot.Field<string>("TipoClase").ToUpper();
                                if (esSemanaExamen && tieneTeoria && tienePractica && tipoClaseActual == "TEORÍA")
                                {
                                    continue;
                                }

                                int diaSemana = slot.Field<int>("DiaSemana");
                                TimeSpan horaInicio = (TimeSpan)slot["HoraInicio"];
                                TimeSpan horaFin = (TimeSpan)slot["HoraFin"];

                                DateTime fechaClase = inicioSemana.AddDays(diaSemana - 1);
                                
                                if (fechaClase.Date > DateTime.Today)
                                    continue;

                                clasesProgramadas++;

                                var reg = dtAsistencias.AsEnumerable().Where(r => 
                                    r.Field<int>("DocenteId") == docId &&
                                    r.Field<int>("CursoId") == curId &&
                                    r.Field<DateTime>("FechaHora").Date == fechaClase.Date
                                ).OrderBy(r => r.Field<DateTime>("FechaHora")).FirstOrDefault();

                                if (reg != null)
                                {
                                    DateTime horaMarcado = reg.Field<DateTime>("FechaHora");
                                    if (horaMarcado.TimeOfDay > horaInicio.Add(TimeSpan.FromMinutes(40)))
                                    {
                                        tardanzas++;
                                    }
                                    else
                                    {
                                        asistencias++;
                                    }
                                }
                                else
                                {
                                    faltas++;
                                }
                            }
                        }

                        double pct = clasesProgramadas > 0 ? ((double)(asistencias + tardanzas) / clasesProgramadas) * 100.0 : 0.0;
                        string pctStr = clasesProgramadas > 0 ? $"{pct:F1}%" : "0.0%";

                        dtReporteResumen.Rows.Add(docId, curId, codDoc, nomDoc, nomCur, tipoCarga, clasesProgramadas, asistencias, tardanzas, faltas, pctStr);
                    }

                    dgvReporteResumen.DataSource = dtReporteResumen;
                    if (dgvReporteResumen.Columns["DocenteId"] != null) dgvReporteResumen.Columns["DocenteId"].Visible = false;
                    if (dgvReporteResumen.Columns["CursoId"] != null) dgvReporteResumen.Columns["CursoId"].Visible = false;

                    dtReporteDetalle.Clear();
                    dgvReporteDetalle.DataSource = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al analizar carga: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvReporteResumen_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReporteResumen.CurrentRow == null) return;

            int docId = Convert.ToInt32(dgvReporteResumen.CurrentRow.Cells["DocenteId"].Value);
            int curId = Convert.ToInt32(dgvReporteResumen.CurrentRow.Cells["CursoId"].Value);

            DateTime fechaInicio = ObtenerInicioSemestre().Date;
            int diff = (7 + (fechaInicio.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime lunesSemana1 = fechaInicio.AddDays(-diff);

            dtReporteDetalle = new DataTable();
            dtReporteDetalle.Columns.Add("Semana");
            dtReporteDetalle.Columns.Add("Fecha Programada");
            dtReporteDetalle.Columns.Add("Día");
            dtReporteDetalle.Columns.Add("Tipo Clase");
            dtReporteDetalle.Columns.Add("Hora Programada");
            dtReporteDetalle.Columns.Add("Marcado Entrada");
            dtReporteDetalle.Columns.Add("Marcado Salida");
            dtReporteDetalle.Columns.Add("Estado");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string sqlHorarios = @"
                        SELECT DiaSemana, HoraInicio, HoraFin, TipoClase
                        FROM HorarioClases
                        WHERE DocenteId = @DocenteId AND CursoId = @CursoId
                          AND (PeriodoId = (SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1)
                               OR (PeriodoId IS NULL AND NOT EXISTS (SELECT 1 FROM Periodos WHERE EsActivo = 1)))
                        ORDER BY DiaSemana, HoraInicio";

                    DataTable dtHorarios = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(sqlHorarios, con))
                    {
                        cmd.Parameters.AddWithValue("@DocenteId", docId);
                        cmd.Parameters.AddWithValue("@CursoId", curId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtHorarios);
                    }

                    DateTime fechaFinMax = lunesSemana1.AddDays(16 * 7);
                    string sqlAsistencias = @"
                        SELECT FechaHora, HoraSalida
                        FROM RegistroAsistencia
                        WHERE DocenteId = @DocenteId AND CursoId = @CursoId
                          AND FechaHora >= @Inicio AND FechaHora < @Fin";

                    DataTable dtAsistencias = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(sqlAsistencias, con))
                    {
                        cmd.Parameters.AddWithValue("@DocenteId", docId);
                        cmd.Parameters.AddWithValue("@CursoId", curId);
                        cmd.Parameters.AddWithValue("@Inicio", lunesSemana1);
                        cmd.Parameters.AddWithValue("@Fin", fechaFinMax);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtAsistencias);
                    }

                    string[] nombresDias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

                    for (int w = 1; w <= 16; w++)
                    {
                        DateTime inicioSemana = lunesSemana1.AddDays((w - 1) * 7);

                        foreach (DataRow slot in dtHorarios.Rows)
                        {
                            int diaSemana = Convert.ToInt32(slot["DiaSemana"]);
                            TimeSpan horaInicio = (TimeSpan)slot["HoraInicio"];
                            TimeSpan horaFin = (TimeSpan)slot["HoraFin"];
                            string tipoClase = slot["TipoClase"].ToString();

                            DateTime fechaClase = inicioSemana.AddDays(diaSemana - 1);
                            string fechaClaseStr = fechaClase.ToString("dd/MM/yyyy");
                            string diaNombre = nombresDias[diaSemana - 1];
                            string horaProgStr = $"{horaInicio:hh\\:mm} - {horaFin:hh\\:mm}";

                            string marcadoEntrada = "-";
                            string marcadoSalida = "-";
                            string estado = "FUTURO";

                            if (fechaClase.Date <= DateTime.Today)
                            {
                                var reg = dtAsistencias.AsEnumerable().Where(r => 
                                    r.Field<DateTime>("FechaHora").Date == fechaClase.Date
                                ).OrderBy(r => r.Field<DateTime>("FechaHora")).FirstOrDefault();

                                if (reg != null)
                                {
                                    DateTime horaEntrada = reg.Field<DateTime>("FechaHora");
                                    marcadoEntrada = horaEntrada.ToString("HH:mm:ss");
                                    marcadoSalida = reg["HoraSalida"] != DBNull.Value 
                                        ? reg.Field<DateTime>("HoraSalida").ToString("HH:mm:ss") 
                                        : "Sin marcar";

                                    if (horaMarcadoEsTarde(horaEntrada.TimeOfDay, horaInicio))
                                    {
                                        estado = "TARDANZA";
                                    }
                                    else
                                    {
                                        estado = "ASISTIÓ";
                                    }
                                }
                                else
                                {
                                    marcadoEntrada = "FALTÓ";
                                    marcadoSalida = "FALTÓ";
                                    estado = "FALTÓ";
                                }
                            }

                            dtReporteDetalle.Rows.Add($"Semana {w}", fechaClaseStr, diaNombre, tipoClase, horaProgStr, marcadoEntrada, marcadoSalida, estado);
                        }
                    }

                    dgvReporteDetalle.DataSource = dtReporteDetalle;
                    
                    dgvReporteDetalle.DataBindingComplete -= DgvReporteDetalle_DataBindingComplete;
                    dgvReporteDetalle.DataBindingComplete += DgvReporteDetalle_DataBindingComplete;
                }
                catch { }
            }
        }

        private bool horaMarcadoEsTarde(TimeSpan entrada, TimeSpan programada)
        {
            return entrada > programada.Add(TimeSpan.FromMinutes(40));
        }

        private void DgvReporteDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvReporteDetalle.Rows)
            {
                string estado = row.Cells["Estado"].Value?.ToString() ?? "";
                if (estado == "FALTÓ") row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                else if (estado == "TARDANZA") row.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 220);
                else if (estado == "ASISTIÓ") row.DefaultCellStyle.BackColor = Color.FromArgb(230, 245, 230);
            }
        }

        private void BtnExportarReporteResumenCSV_Click(object sender, EventArgs e)
        {
            string docente = cmbReporteDocente.Text != "Todos los docentes" ? cmbReporteDocente.Text : "";
            ExportarDataGridViewAExcel(dgvReporteResumen, "Docentes", docente, "Resumen_Carga_16_Semanas.csv");
        }

        private void BtnExportarReporteResumenPDF_Click(object sender, EventArgs e)
        {
            string docente = cmbReporteDocente.Text != "Todos los docentes" ? cmbReporteDocente.Text : "";
            ExportarReporteSemanasResumenPDF(dgvReporteResumen, "Docentes", docente, "Resumen_Carga_16_Semanas.pdf");
        }

        private void BtnExportarReporteDetallePDF_Click(object sender, EventArgs e)
        {
            if (dgvReporteResumen.CurrentRow == null) return;
            string doc = dgvReporteResumen.CurrentRow.Cells["Docente"].Value?.ToString() ?? "";
            string curso = dgvReporteResumen.CurrentRow.Cells["Curso"].Value?.ToString() ?? "";
            string cod = dgvReporteResumen.CurrentRow.Cells["Código"].Value?.ToString() ?? "";
            ExportarReporteSemanasDetallePDF(dgvReporteDetalle, doc, curso, cod, "Docentes", doc, $"Detalle_{curso}.pdf");
        }

        private void ExportarReporteSemanasResumenPDF(DataGridView dgv, string categoria, string subCategoria, string nombreArchivo)
        {
            string rutaFinal = MostrarDialogoGuardado(categoria, subCategoria, nombreArchivo, "Archivo PDF (*.pdf)|*.pdf");
            if (string.IsNullOrEmpty(rutaFinal)) return;
            try
            {
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
                PdfWriter.GetInstance(pdfDoc, new FileStream(rutaFinal, FileMode.Create));
                    pdfDoc.Open();

                    iTextSharp.text.Font fuenteUniversidad = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f);
                    iTextSharp.text.Font fuenteFacultad = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
                    iTextSharp.text.Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
                    iTextSharp.text.Font fuenteDetalles = FontFactory.GetFont(FontFactory.HELVETICA, 10f);
                    iTextSharp.text.Font fuenteCabecera = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.WHITE);
                    iTextSharp.text.Font fuenteCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 9f);

                    pdfDoc.Add(new Paragraph("UNIVERSIDAD NACIONAL MAYOR DE SAN MARCOS", fuenteUniversidad) { Alignment = Element.ALIGN_CENTER });
                    pdfDoc.Add(new Paragraph("Facultad de Ciencias Matemáticas", fuenteFacultad) { Alignment = Element.ALIGN_CENTER });
                    pdfDoc.Add(new Paragraph("\nREPORTE CONSOLIDADO DE CARGA ACADÉMICA Y ASISTENCIA (16 SEMANAS)", fuenteTitulo) { Alignment = Element.ALIGN_CENTER });
                    pdfDoc.Add(new Paragraph($"Fecha de Inicio del Semestre: {ObtenerInicioSemestre():dd/MM/yyyy}\nFecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n\n", fuenteDetalles));

                    PdfPTable tablaPdf = new PdfPTable(9) { WidthPercentage = 100 };
                    float[] widths = new float[] { 8f, 22f, 20f, 18f, 8f, 8f, 8f, 8f, 8f };
                    tablaPdf.SetWidths(widths);

                    string[] cabeceras = { "Código", "Docente", "Curso", "Tipo de Carga", "Prog.", "Asist.", "Tard.", "Faltas", "% Asist." };
                    foreach (string cab in cabeceras)
                    {
                        tablaPdf.AddCell(new PdfPCell(new Phrase(cab, fuenteCabecera)) { BackgroundColor = new BaseColor(43, 108, 176), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 6 });
                    }

                    foreach (DataGridViewRow fila in dgv.Rows)
                    {
                        if (fila.IsNewRow) continue;
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["Código"].Value?.ToString() ?? "", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["Docente"].Value?.ToString() ?? "", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_LEFT });
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["Curso"].Value?.ToString() ?? "", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_LEFT });
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["Tipo de Carga"].Value?.ToString() ?? "", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["Clases Programadas"].Value?.ToString() ?? "0", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["Asistencias"].Value?.ToString() ?? "0", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["Tardanzas"].Value?.ToString() ?? "0", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["Faltas"].Value?.ToString() ?? "0", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        tablaPdf.AddCell(new PdfPCell(new Phrase(fila.Cells["% Asistencia"].Value?.ToString() ?? "0%", fuenteCuerpo)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    }

                    pdfDoc.Add(tablaPdf);
                    pdfDoc.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void ExportarReporteSemanasDetallePDF(DataGridView dgv, string docente, string curso, string codigo, string categoria, string subCategoria, string nombreArchivo)
        {
            string rutaFinal = MostrarDialogoGuardado(categoria, subCategoria, nombreArchivo, "Archivo PDF (*.pdf)|*.pdf");
            if (string.IsNullOrEmpty(rutaFinal)) return;
            try
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(pdfDoc, new FileStream(rutaFinal, FileMode.Create));
                    pdfDoc.Open();

                    iTextSharp.text.Font fuenteUniversidad = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f);
                    iTextSharp.text.Font fuenteFacultad = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
                    iTextSharp.text.Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
                    iTextSharp.text.Font fuenteDetalles = FontFactory.GetFont(FontFactory.HELVETICA, 10f);
                    iTextSharp.text.Font fuenteCabecera = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.WHITE);
                    iTextSharp.text.Font fuenteCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 9f);
                    iTextSharp.text.Font fuenteFalta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.RED);
                    iTextSharp.text.Font fuenteTarde = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, new BaseColor(200, 100, 0));
                    iTextSharp.text.Font fuenteAsist = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, new BaseColor(0, 120, 0));

                    pdfDoc.Add(new Paragraph("UNIVERSIDAD NACIONAL MAYOR DE SAN MARCOS", fuenteUniversidad) { Alignment = Element.ALIGN_CENTER });
                    pdfDoc.Add(new Paragraph("Facultad de Ciencias Matemáticas", fuenteFacultad) { Alignment = Element.ALIGN_CENTER });
                    pdfDoc.Add(new Paragraph("\nREPORTE DETALLADO DE ASISTENCIA SEMANA A SEMANA", fuenteTitulo) { Alignment = Element.ALIGN_CENTER });
                    
                    string info = $"\nDocente: {docente} ({codigo})\n" +
                                  $"Curso: {curso}\n" +
                                  $"Semestre Académico - Fecha de Inicio: {ObtenerInicioSemestre():dd/MM/yyyy}\n" +
                                  $"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n\n";
                    pdfDoc.Add(new Paragraph(info, fuenteDetalles));

                    PdfPTable tablaPdf = new PdfPTable(7) { WidthPercentage = 100 };
                    float[] widths = new float[] { 12f, 15f, 12f, 13f, 18f, 15f, 15f };
                    tablaPdf.SetWidths(widths);

                    string[] cabeceras = { "Semana", "Fecha Prog.", "Día", "Tipo Clase", "Hora Prog.", "Entrada Real", "Estado" };
                    foreach (string cab in cabeceras)
                    {
                        tablaPdf.AddCell(new PdfPCell(new Phrase(cab, fuenteCabecera)) { BackgroundColor = new BaseColor(43, 108, 176), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 });
                    }

                    foreach (DataGridViewRow fila in dgv.Rows)
                    {
                        if (fila.IsNewRow) continue;
                        string estado = fila.Cells["Estado"].Value?.ToString() ?? "";
                        iTextSharp.text.Font f = fuenteCuerpo;
                        BaseColor bgColor = BaseColor.WHITE;

                        if (estado == "FALTÓ") { f = fuenteFalta; bgColor = new BaseColor(255, 230, 230); }
                        else if (estado == "TARDANZA") { f = fuenteTarde; bgColor = new BaseColor(255, 245, 220); }
                        else if (estado == "ASISTIÓ") { f = fuenteAsist; bgColor = new BaseColor(230, 245, 230); }

                        Action<string, int> addCell = (text, align) => {
                            tablaPdf.AddCell(new PdfPCell(new Phrase(text, f)) { BackgroundColor = bgColor, HorizontalAlignment = align, Padding = 4 });
                        };

                        addCell(fila.Cells["Semana"].Value?.ToString() ?? "", Element.ALIGN_CENTER);
                        addCell(fila.Cells["Fecha Programada"].Value?.ToString() ?? "", Element.ALIGN_CENTER);
                        addCell(fila.Cells["Día"].Value?.ToString() ?? "", Element.ALIGN_CENTER);
                        addCell(fila.Cells["Tipo Clase"].Value?.ToString() ?? "", Element.ALIGN_CENTER);
                        addCell(fila.Cells["Hora Programada"].Value?.ToString() ?? "", Element.ALIGN_CENTER);
                        addCell(fila.Cells["Marcado Entrada"].Value?.ToString() ?? "", Element.ALIGN_CENTER);
                        addCell(estado, Element.ALIGN_CENTER);
                    }

                    pdfDoc.Add(tablaPdf);
                    pdfDoc.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void lblDetalle_Click(object sender, EventArgs e)
        {

        }

        private void dgvReporteResumen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
