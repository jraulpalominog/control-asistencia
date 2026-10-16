using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
        private Label lblFecha;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += Form1_Load_Custom;
            txtCodigo.CharacterCasing = CharacterCasing.Upper;

            // Inicializar lblFecha
            this.lblFecha = new Label();
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.AutoSize = false;
            this.lblFecha.Width = 600;
            this.lblFecha.Height = 40;
            this.lblFecha.TextAlign = ContentAlignment.MiddleCenter;
            this.lblFecha.BackColor = Color.Transparent;
            this.lblFecha.Font = new Font("Arial", 22F, FontStyle.Bold);
            this.lblFecha.ForeColor = Color.White;
            this.lblFecha.Location = new Point(lblReloj.Location.X + (lblReloj.Width - this.lblFecha.Width) / 2, lblReloj.Location.Y - 50);
            this.Controls.Add(this.lblFecha);

            timerReloj.Tick -= timerReloj_Tick;
            timerReloj.Tick += timerReloj_Tick;
            timerReloj.Start();

            if (this.Controls.Find("btnCambiarClave", true).Length > 0)
            {
                Button btnClave = (Button)this.Controls.Find("btnCambiarClave", true)[0];
                btnClave.Click -= btnCambiarClave_Click;
                btnClave.Click += btnCambiarClave_Click;
            }

            ConfigurarAutocompletadoCursos();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is FormRegistroCurso || c is FormRegistroAutomatico)
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }

                btnRegistrar.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Load_Custom(object sender, EventArgs e)
        {
            CentrarControles();
            this.WindowState = FormWindowState.Maximized;
            ResetCodigo();
            this.ActiveControl = txtCodigo;
        }

        private void CentrarControles()
        {
            Panel panelCentro = new Panel();
            panelCentro.Size = this.ClientSize;
            panelCentro.Location = new Point(0, 0);
            panelCentro.BackColor = Color.Transparent;

            Control[] controles = new Control[this.Controls.Count];
            this.Controls.CopyTo(controles, 0);
            foreach (Control c in controles)
            {
                if (c.Location.Y < 80) continue;
                if (c.Name == "btnVolverHome")
                {
                    c.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                    continue;
                }
                panelCentro.Controls.Add(c);
            }

            this.Controls.Add(panelCentro);

            Control[] homeBtns = this.Controls.Find("btnVolverHome", true);
            if (homeBtns.Length > 0)
            {
                ToolTip tt = new ToolTip() { InitialDelay = 0 };
                tt.SetToolTip(homeBtns[0], "Acceder al Panel de Administrador");
            }

            this.Resize += (s, e) => {
                panelCentro.Left = (this.ClientSize.Width - panelCentro.Width) / 2;
                panelCentro.Top = (this.ClientSize.Height - panelCentro.Height) / 2;

                Control[] lbl2 = this.Controls.Find("label2", true);
                Control[] lbl3 = this.Controls.Find("label3", true);
                if (lbl2.Length > 0) lbl2[0].Left = (this.ClientSize.Width - lbl2[0].Width) / 2;
                if (lbl3.Length > 0) lbl3[0].Left = (this.ClientSize.Width - lbl3[0].Width) / 2;
            };
        }

        private void ConfigurarAutocompletadoCursos()
        {
            AutoCompleteStringCollection coleccionCursos = new AutoCompleteStringCollection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Nombre FROM Cursos ORDER BY Nombre ASC", con);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            coleccionCursos.Add(dr["Nombre"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error pasivo al cargar autocompletado: " + ex.Message);
                }
            }

            Control[] controlesCurso = this.Controls.Find("txtCurso", true);
            if (controlesCurso.Length > 0 && controlesCurso[0] is TextBox txtC)
            {
                txtC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtC.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtC.AutoCompleteCustomSource = coleccionCursos;
            }
        }

        private void timerReloj_Tick(object sender, EventArgs e)
        {
            lblReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            if (this.lblFecha != null)
            {
                string fechaLarga = DateTime.Now.ToString("dddd, d 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));
                this.lblFecha.Text = char.ToUpper(fechaLarga[0]) + fechaLarga.Substring(1);
            }
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

            Button btnGuardar = new Button() { Text = "Guardar", Left = 130, Top = 150, Width = 80, BackColor = Color.LightGreen, FlatStyle = FlatStyle.Flat };
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

                DialogResult confirmacion = MessageBox.Show("¿Estás seguro que deseas cambiar la contraseña de acceso al sistema?", "Confirmación de Seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Control[] controlesCodigo = this.Controls.Find("txtCodigo", true);
            if (controlesCodigo.Length == 0) return;
            TextBox txtC = (TextBox)controlesCodigo[0];

            string codigo = txtC.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo) || codigo == "DOC")
            {
                AutoClosingMessageBox.Show("Por favor, ingrese un Código válido.", "Advertencia", 2000, MessageBoxIcon.Warning);
                ResetCodigo();
                txtCodigo.Focus();
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string queryBuscar = "SELECT Id, Nombre + ' ' + Apellido as NombreCompleto FROM Docentes WHERE Codigo = @Codigo";
                    SqlCommand cmdBuscar = new SqlCommand(queryBuscar, con);
                    cmdBuscar.Parameters.AddWithValue("@Codigo", codigo);

                    using (SqlDataReader reader = cmdBuscar.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["Id"]);
                            string nombre = reader["NombreCompleto"].ToString();
                            reader.Close();

                            int diaSemanaHoy = ObtenerDiaSemanaSQL();

                            DataTable cursosHoy = new DataTable();
                            string queryHorarios = @"SELECT h.CursoId, c.Nombre as CursoNombre, 
                                                        CONVERT(VARCHAR(5), h.HoraInicio, 108) as HoraInicio, 
                                                        CONVERT(VARCHAR(5), h.HoraFin, 108) as HoraFin
                                                     FROM HorarioClases h
                                                     INNER JOIN Cursos c ON h.CursoId = c.Id
                                                     WHERE h.DocenteId = @DocenteId AND h.DiaSemana = @Dia
                                                       AND (h.PeriodoId = (SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1)
                                                            OR (h.PeriodoId IS NULL AND NOT EXISTS (SELECT 1 FROM Periodos WHERE EsActivo = 1)))
                                                     ORDER BY h.HoraInicio";
                            using (SqlCommand cmdH = new SqlCommand(queryHorarios, con))
                            {
                                cmdH.Parameters.AddWithValue("@DocenteId", id);
                                cmdH.Parameters.AddWithValue("@Dia", diaSemanaHoy);
                                SqlDataAdapter da = new SqlDataAdapter(cmdH);
                                da.Fill(cursosHoy);
                            }

                            // bool tablaHorariosExiste = true;
                            if (cursosHoy.Rows.Count == 0)
                            {
                                try
                                {
                                    using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM HorarioClases WHERE DocenteId = @DocId", con))
                                    {
                                        cmdCheck.Parameters.AddWithValue("@DocId", id);
                                        int totalHorarios = Convert.ToInt32(cmdCheck.ExecuteScalar());
                                    }
                                }
                                catch
                                {
                                }
                            }



                            DataTable registrosHoy = new DataTable();
                            string queryRegistros = @"SELECT r.Id, r.CursoId, c.Nombre as CursoNombre, r.FechaHora, r.HoraSalida
                                                      FROM RegistroAsistencia r
                                                      INNER JOIN Cursos c ON r.CursoId = c.Id
                                                      WHERE r.DocenteId = @DocenteId AND CAST(r.FechaHora AS DATE) = CAST(GETDATE() AS DATE)
                                                      ORDER BY r.FechaHora DESC";
                            using (SqlCommand cmdR = new SqlCommand(queryRegistros, con))
                            {
                                cmdR.Parameters.AddWithValue("@DocenteId", id);
                                SqlDataAdapter daR = new SqlDataAdapter(cmdR);
                                daR.Fill(registrosHoy);
                            }

                            var cursosPendientesSalida = new System.Collections.Generic.List<DataRow>();
                            foreach (DataRow rowR in registrosHoy.Rows)
                            {
                                if (rowR["HoraSalida"] == DBNull.Value)
                                {
                                    cursosPendientesSalida.Add(rowR);
                                }
                            }

                            if (cursosHoy.Rows.Count == 0 && cursosPendientesSalida.Count == 0)
                            {
                                FormAviso.Mostrar(
                                    "Sin cursos programados",
                                    $"Hola {nombre}, no tienes asignaturas programadas para el día de hoy.",
                                    FormAviso.TipoAviso.Informacion, 6);
                                ResetCodigo();
                                txtCodigo.Focus();
                                return;
                            }

                            var cursosSinIngreso = new System.Collections.Generic.List<DataRow>();
                            foreach (DataRow rowH in cursosHoy.Rows)
                            {
                                int cursoIdH = Convert.ToInt32(rowH["CursoId"]);
                                bool tieneIngreso = false;
                                foreach (DataRow rowR in registrosHoy.Rows)
                                {
                                    if (Convert.ToInt32(rowR["CursoId"]) == cursoIdH)
                                    {
                                        tieneIngreso = true;
                                        break;
                                    }
                                }
                                if (!tieneIngreso) cursosSinIngreso.Add(rowH);
                            }

                            if (cursosPendientesSalida.Count > 0 && cursosSinIngreso.Count == 0)
                            {
                                Form frmCurso = new FormRegistroAutomatico(id, nombre, codigo, cursosHoy, cursosPendientesSalida, cursosSinIngreso);
                                frmCurso.TopLevel = false;
                                frmCurso.FormBorderStyle = FormBorderStyle.None;
                                frmCurso.Dock = DockStyle.Fill;
                                frmCurso.FormClosed += (s, ev) => { ResetCodigo(); txtCodigo.Focus(); };
                                this.Controls.Add(frmCurso);
                                frmCurso.BringToFront();
                                frmCurso.Show();
                            }
                            else
                            {
                                Form frmCurso = new FormRegistroAutomatico(id, nombre, codigo, cursosHoy, cursosPendientesSalida, cursosSinIngreso);
                                frmCurso.TopLevel = false;
                                frmCurso.FormBorderStyle = FormBorderStyle.None;
                                frmCurso.Dock = DockStyle.Fill;
                                frmCurso.FormClosed += (s, ev) => { ResetCodigo(); txtCodigo.Focus(); };
                                this.Controls.Add(frmCurso);
                                frmCurso.BringToFront();
                                frmCurso.Show();
                            }
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Código no encontrado. El docente no está registrado en la facultad.", "Error", 2000, MessageBoxIcon.Error);
                            ResetCodigo();
                            txtCodigo.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de base de datos: " + ex.Message, "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int ObtenerDiaSemanaSQL()
        {
            DayOfWeek dow = DateTime.Now.DayOfWeek;
            switch (dow)
            {
                case DayOfWeek.Monday: return 1;
                case DayOfWeek.Tuesday: return 2;
                case DayOfWeek.Wednesday: return 3;
                case DayOfWeek.Thursday: return 4;
                case DayOfWeek.Friday: return 5;
                case DayOfWeek.Saturday: return 6;
                case DayOfWeek.Sunday: return 7;
                default: return 0;
            }
        }

        private void btnAbrirAdmin_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Acceso Restringido",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Ingrese la contraseña de administrador:", Width = 250 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240, PasswordChar = '*' };
            Button confirmation = new Button() { Text = "Entrar", Left = 160, Width = 100, Top = 80, DialogResult = DialogResult.OK };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                if (textBox.Text == ObtenerContraseñaAdmin())
                {
                    FormAdmin ventanaAdmin = new FormAdmin();
                    this.Hide();
                    ventanaAdmin.FormClosed += (sArgs, args) => { this.Show(); };
                    ventanaAdmin.Show();
                }
                else
                {
                    AutoClosingMessageBox.Show("Contraseña incorrecta. Acceso denegado.", "Error", 2000, MessageBoxIcon.Error);
                }
            }
        }

        private bool adminAutorizado = false;

        private bool SolicitarClaveAdmin()
        {
            Form prompt = new Form()
            {
                Width = 350, Height = 200, FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Seguridad del Sistema", StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false, MinimizeBox = false
            };

            Label lblInfo = new Label() { Left = 20, Top = 20, Text = "Ingrese contraseña de Administrador para salir:", Width = 300 };
            TextBox txtClave = new TextBox() { Left = 20, Top = 45, Width = 290, UseSystemPasswordChar = true };
            Button btnConfirmar = new Button() { Text = "Confirmar", Left = 20, Top = 80, Width = 140, BackColor = Color.LightCoral };
            Button btnCancelar = new Button() { Text = "Cancelar", Left = 170, Top = 80, Width = 140 };
            Label lblCountdown = new Label() { Left = 20, Top = 120, Width = 300, ForeColor = Color.Gray, Text = "Cerrando ventana en 20 segundos..." };

            int timeLeft = 20;
            Timer countdownTimer = new Timer() { Interval = 1000 };
            countdownTimer.Tick += (s, ev) => 
            {
                timeLeft--;
                if (timeLeft > 0)
                {
                    lblCountdown.Text = $"Cerrando ventana en {timeLeft} segundos...";
                }
                else
                {
                    countdownTimer.Stop();
                    prompt.DialogResult = DialogResult.Cancel;
                    prompt.Close();
                }
            };

            btnConfirmar.Click += (s, ev) => { countdownTimer.Stop(); prompt.DialogResult = DialogResult.OK; prompt.Close(); };
            btnCancelar.Click += (s, ev) => { countdownTimer.Stop(); prompt.DialogResult = DialogResult.Cancel; prompt.Close(); };
            
            prompt.Controls.Add(lblInfo); prompt.Controls.Add(txtClave); prompt.Controls.Add(btnConfirmar); prompt.Controls.Add(btnCancelar); prompt.Controls.Add(lblCountdown);
            prompt.AcceptButton = btnConfirmar; prompt.CancelButton = btnCancelar;
            prompt.Load += (s, ev) => countdownTimer.Start();

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string ruta = Path.Combine(Application.StartupPath, "admin_config.txt");
                string claveVerdadera = File.Exists(ruta) ? File.ReadAllText(ruta).Trim() : "Admin123";

                if (txtClave.Text == claveVerdadera) return true;

                Form errorForm = new Form() { Size = new Size(400, 100), FormBorderStyle = FormBorderStyle.None, StartPosition = FormStartPosition.CenterParent, BackColor = Color.FromArgb(220, 53, 69) };
                Label lblError = new Label() { Text = "Contraseña incorrecta. Acción cancelada.", ForeColor = Color.White, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Arial", 12, FontStyle.Bold) };
                errorForm.Controls.Add(lblError);
                Timer t = new Timer() { Interval = 2000 };
                t.Tick += (s, ev) => { t.Stop(); errorForm.Close(); };
                t.Start();
                errorForm.ShowDialog();
            }
            return false;
        }

        private void btnVolverHome_Click(object sender, EventArgs e)
        {
            if (SolicitarClaveAdmin())
            {
                adminAutorizado = true;
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Form frmReg = null;
            foreach (Control c in this.Controls)
            {
                if (c is Form && (c.Name == "FormRegistroCurso" || c.Name == "FormRegistroSalida" || c.Name == "FormRegistroAutomatico"))
                {
                    frmReg = (Form)c;
                    break;
                }
            }

            if (frmReg != null)
            {
                e.Cancel = true;
                frmReg.Close();
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing && !adminAutorizado)
            {
                if (SolicitarClaveAdmin())
                {
                    adminAutorizado = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }

        private void ResetCodigo()
        {
            txtCodigo.Text = "DOC";
            txtCodigo.SelectionStart = txtCodigo.Text.Length;
        }

        // Eventos vacíos requeridos por el diseñador
        private void label1_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void timer1_Tick(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void guna2ImageButton2_Click(object sender, EventArgs e) { }
    }

    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout, MessageBoxIcon icon)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed, null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer) MessageBox.Show(text, caption, MessageBoxButtons.OK, icon);
        }
        public static void Show(string text, string caption, int timeout, MessageBoxIcon icon = MessageBoxIcon.Warning) { new AutoClosingMessageBox(text, caption, timeout, icon); }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption);
            if (mbWnd != IntPtr.Zero) SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}
