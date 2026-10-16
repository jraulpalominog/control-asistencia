using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class FormRegistroCurso : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        private int docenteIdActual;
        private string docenteNombreActual;
        private string docenteCodigoActual;

        private bool esSalida = false;
        private int? registroIdActualSalida = null;
        private int? idCursoSalida = null;
        private string nombreCursoSalida = "";

        private List<string> listaMaestraCursos = new List<string>();

        private ComboBox buscadorCurso;
        private Label etiquetaBienvenida;
        private Timer timerInactividad;
        private Label lblInactividad;
        private Label lblTipoRegistro;
        private int segundosInactividad = 20;

        private bool modoHorarioAutomatico = false;
        private DataTable cursosHoyDT;
        private List<DataRow> cursosPendientesSalidaList;
        private List<DataRow> cursosSinIngresoList;
        private ListBox lstCursosHoy;
        private int cursoIdSeleccionadoAuto = 0;
        private string cursoNombreSeleccionadoAuto = "";

        public FormRegistroCurso(int id, string nombre, string codigo)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            
            docenteIdActual = id;
            docenteNombreActual = nombre;
            docenteCodigoActual = codigo;

            DeterminarTipoRegistro();

            this.Load += FormRegistroCurso_Load_Custom;

            EnlazarControlesSeguros();
            CargarAsignaturasDesdeBD();

            if (buscadorCurso != null)
            {
                ConfigurarBuscadorCursos();
            }

            this.Load += FormRegistroCurso_Load;
        }

        public FormRegistroCurso(int id, string nombre, string codigo, DataTable cursosHoy, List<DataRow> cursosPendientesSalida, List<DataRow> cursosSinIngreso)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            docenteIdActual = id;
            docenteNombreActual = nombre;
            docenteCodigoActual = codigo;

            modoHorarioAutomatico = true;
            cursosHoyDT = cursosHoy;
            cursosPendientesSalidaList = cursosPendientesSalida;
            cursosSinIngresoList = cursosSinIngreso;

            EnlazarControlesSeguros();

            if (buscadorCurso != null) buscadorCurso.Visible = false;
            Control[] labels = this.Controls.Find("label1", true);
            foreach (Control lbl in labels) { if (lbl is Label && lbl.Text.ToLower().Contains("curso")) lbl.Visible = false; }

            this.Load += FormRegistroCurso_Load_Custom_Auto;
            this.Load += FormRegistroCurso_Load;
        }

        private Panel pnlAuto;

        private void FormRegistroCurso_Load_Custom_Auto(object sender, EventArgs e)
        {
            pnlAuto = new Panel();
            pnlAuto.Size = new Size(900, 500);
            pnlAuto.BackColor = Color.FromArgb(34, 45, 50);
            pnlAuto.Paint += (sPaint, evPaint) => {
                evPaint.Graphics.DrawRectangle(new Pen(Color.MediumSeaGreen, 5), 0, 0, pnlAuto.Width - 1, pnlAuto.Height - 1);
            };

            lblTipoRegistro = new Label();
            lblTipoRegistro.AutoSize = false;
            lblTipoRegistro.Width = 860;
            lblTipoRegistro.Height = 250;
            lblTipoRegistro.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblTipoRegistro.TextAlign = ContentAlignment.MiddleCenter;
            lblTipoRegistro.ForeColor = Color.White;

            // Lógica de disponibilidad
            bool EsDisponible(DataRow r)
            {
                if (TimeSpan.TryParse(r["HoraInicio"].ToString(), out TimeSpan inicio) && TimeSpan.TryParse(r["HoraFin"].ToString(), out TimeSpan fin))
                {
                    TimeSpan ahora = DateTime.Now.TimeOfDay;
                    if (ahora >= inicio.Subtract(TimeSpan.FromMinutes(30)) && ahora <= fin) return true;
                }
                return false;
            }

            if (cursosSinIngresoList.Count == 1 && cursosPendientesSalidaList.Count == 0)
            {
                DataRow row = cursosSinIngresoList[0];
                cursoIdSeleccionadoAuto = Convert.ToInt32(row["CursoId"]);
                cursoNombreSeleccionadoAuto = row["CursoNombre"].ToString();
                string hora = row["HoraInicio"].ToString() + " - " + row["HoraFin"].ToString();

                if (EsDisponible(row))
                {
                    lblTipoRegistro.Text = $"INGRESO AUTOMÁTICO\n\nDocente: {docenteNombreActual}\nCurso: {cursoNombreSeleccionadoAuto}\nHorario: {hora}\n\n[ DISPONIBLE - Presione ENTER para confirmar ]";
                }
                else
                {
                    cursoIdSeleccionadoAuto = 0; // Bloquea el enter
                    lblTipoRegistro.Text = $"CURSO FUERA DE HORA\n\nDocente: {docenteNombreActual}\nCurso: {cursoNombreSeleccionadoAuto}\nHorario: {hora}\n\n[ BLOQUEADO - Fuera del horario permitido ]";
                    lblTipoRegistro.ForeColor = Color.LightCoral;
                }
                
                lblTipoRegistro.Location = new Point(20, 125);
                pnlAuto.Controls.Add(lblTipoRegistro);
            }
            else
            {
                string titulo = "CURSOS DE HOY";
                if (cursosPendientesSalidaList.Count > 0)
                    titulo = "[!] CURSOS DE HOY (tiene salidas pendientes)";

                lblTipoRegistro.Text = titulo;
                lblTipoRegistro.Height = 80;
                lblTipoRegistro.Location = new Point(20, 30);
                pnlAuto.Controls.Add(lblTipoRegistro);

                lstCursosHoy = new ListBox();
                lstCursosHoy.Font = new Font("Segoe UI", 14, FontStyle.Regular);
                lstCursosHoy.Size = new Size(650, 200);
                lstCursosHoy.BackColor = Color.FromArgb(245, 245, 250);
                lstCursosHoy.BorderStyle = BorderStyle.FixedSingle;

                foreach (DataRow row in cursosPendientesSalidaList)
                {
                    string cursoNombre = row["CursoNombre"].ToString();
                    lstCursosHoy.Items.Add($"[X] SALIDA PENDIENTE: {cursoNombre}");
                }

                int primerDisponibleIdx = -1;
                foreach (DataRow row in cursosSinIngresoList)
                {
                    string cursoNombre = row["CursoNombre"].ToString();
                    string hora = row["HoraInicio"].ToString() + " - " + row["HoraFin"].ToString();
                    if (EsDisponible(row))
                    {
                        lstCursosHoy.Items.Add($"[>] {cursoNombre}  ({hora})  [DISPONIBLE]");
                        if (primerDisponibleIdx == -1 && cursosPendientesSalidaList.Count == 0) primerDisponibleIdx = lstCursosHoy.Items.Count - 1;
                    }
                    else
                    {
                        lstCursosHoy.Items.Add($"[ ] {cursoNombre}  ({hora})  [FUERA DE HORA]");
                    }
                }

                if (lstCursosHoy.Items.Count > 0)
                {
                    if (cursosPendientesSalidaList.Count > 0) lstCursosHoy.SelectedIndex = 0;
                    else if (primerDisponibleIdx != -1) lstCursosHoy.SelectedIndex = primerDisponibleIdx;
                    else lstCursosHoy.SelectedIndex = 0;
                }

                int lastValidIndex = lstCursosHoy.SelectedIndex;

                lstCursosHoy.SelectedIndexChanged += (s, ev) =>
                {
                    ReiniciarTimer();
                    int idx = lstCursosHoy.SelectedIndex;
                    if (idx < 0) return;

                    string texto = lstCursosHoy.Items[idx].ToString();
                    if (texto.Contains("[FUERA DE HORA]"))
                    {
                        if (lastValidIndex >= 0 && lastValidIndex < lstCursosHoy.Items.Count && !lstCursosHoy.Items[lastValidIndex].ToString().Contains("[FUERA DE HORA]"))
                            lstCursosHoy.SelectedIndex = lastValidIndex;
                        else
                            lstCursosHoy.SelectedIndex = -1;
                        cursoIdSeleccionadoAuto = 0;
                        return;
                    }
                    
                    lastValidIndex = idx;

                    if (idx < cursosPendientesSalidaList.Count)
                    {
                        esSalida = true;
                        DataRow rowSal = cursosPendientesSalidaList[idx];
                        registroIdActualSalida = Convert.ToInt32(rowSal["Id"]);
                        idCursoSalida = Convert.ToInt32(rowSal["CursoId"]);
                        nombreCursoSalida = rowSal["CursoNombre"].ToString();
                        cursoIdSeleccionadoAuto = idCursoSalida.Value;
                        cursoNombreSeleccionadoAuto = nombreCursoSalida;
                    }
                    else
                    {
                        esSalida = false;
                        int cursoIdx = idx - cursosPendientesSalidaList.Count;
                        if (cursoIdx < cursosSinIngresoList.Count)
                        {
                            DataRow rowIng = cursosSinIngresoList[cursoIdx];
                            cursoIdSeleccionadoAuto = Convert.ToInt32(rowIng["CursoId"]);
                            cursoNombreSeleccionadoAuto = rowIng["CursoNombre"].ToString();
                        }
                    }
                };

                if (lstCursosHoy.SelectedIndex >= 0)
                {
                    int idx = lstCursosHoy.SelectedIndex;
                    if (idx < cursosPendientesSalidaList.Count)
                    {
                        esSalida = true;
                        DataRow rowSal = cursosPendientesSalidaList[idx];
                        registroIdActualSalida = Convert.ToInt32(rowSal["Id"]);
                        idCursoSalida = Convert.ToInt32(rowSal["CursoId"]);
                        nombreCursoSalida = rowSal["CursoNombre"].ToString();
                        cursoIdSeleccionadoAuto = idCursoSalida.Value;
                        cursoNombreSeleccionadoAuto = nombreCursoSalida;
                    }
                    else
                    {
                        esSalida = false;
                        int cursoIdx = idx - cursosPendientesSalidaList.Count;
                        if (cursoIdx < cursosSinIngresoList.Count)
                        {
                            DataRow rowIng = cursosSinIngresoList[cursoIdx];
                            cursoIdSeleccionadoAuto = Convert.ToInt32(rowIng["CursoId"]);
                            cursoNombreSeleccionadoAuto = rowIng["CursoNombre"].ToString();
                        }
                    }
                }

                lstCursosHoy.Location = new Point(150, 130);
                pnlAuto.Controls.Add(lstCursosHoy);

                Label lblInstrucciones = new Label();
                lblInstrucciones.Text = "Seleccione un curso y presione ENTER para confirmar";
                lblInstrucciones.Font = new Font("Segoe UI", 12, FontStyle.Italic);
                lblInstrucciones.ForeColor = Color.LightGray;
                lblInstrucciones.AutoSize = true;
                lblInstrucciones.Location = new Point(150, 350);
                pnlAuto.Controls.Add(lblInstrucciones);
            }
            Button btnVerRecord = new Button();
            btnVerRecord.Text = "Ver mi Récord (16 Semanas)";
            btnVerRecord.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnVerRecord.Size = new Size(260, 50);
            btnVerRecord.Location = new Point(pnlAuto.Width - 280, pnlAuto.Height - 70);
            btnVerRecord.BackColor = Color.SteelBlue;
            btnVerRecord.ForeColor = Color.White;
            btnVerRecord.FlatStyle = FlatStyle.Flat;
            btnVerRecord.Cursor = Cursors.Hand;
            btnVerRecord.Click += (sC, evC) => {
                timerInactividad.Stop();
                FormReporteDocente frmRep = new FormReporteDocente(docenteIdActual, docenteNombreActual, docenteCodigoActual);
                frmRep.ShowDialog();
                ReiniciarTimer();
            };
            pnlAuto.Controls.Add(btnVerRecord);

            this.Controls.Add(pnlAuto);
            pnlAuto.BringToFront();

            CentrarControlesAuto();
            this.WindowState = FormWindowState.Maximized;
            IniciarTimerInactividad();
        }

        private void CentrarControlesAuto()
        {
            this.Resize += (s, e) =>
            {
                if (pnlAuto != null)
                {
                    pnlAuto.Left = (this.ClientSize.Width - pnlAuto.Width) / 2;
                    pnlAuto.Top = (this.ClientSize.Height - pnlAuto.Height) / 2;
                }
            };
        }

        private void DeterminarTipoRegistro()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = @"SELECT TOP 1 r.Id, r.CursoId, c.Nombre as CursoNombre 
                                     FROM RegistroAsistencia r 
                                     INNER JOIN Cursos c ON r.CursoId = c.Id 
                                     WHERE r.DocenteId = @DocenteId AND CAST(r.FechaHora AS DATE) = CAST(GETDATE() AS DATE) AND r.HoraSalida IS NULL 
                                     ORDER BY r.FechaHora DESC";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@DocenteId", docenteIdActual);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                esSalida = true;
                                registroIdActualSalida = Convert.ToInt32(reader["Id"]);
                                idCursoSalida = Convert.ToInt32(reader["CursoId"]);
                                nombreCursoSalida = reader["CursoNombre"].ToString();
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void FormRegistroCurso_Load_Custom(object sender, EventArgs e)
        {
            if (esSalida)
            {
                lblTitulo.Text = $"SALIDA - Curso: {nombreCursoSalida}";
                lblTitulo.BackColor = Color.Tomato;
            }
            else
            {
                lblTitulo.Text = "INGRESO";
                lblTitulo.BackColor = Color.ForestGreen;
            }

            CentrarControles();
            this.WindowState = FormWindowState.Maximized;
            IniciarTimerInactividad();
        }

        private void IniciarTimerInactividad()
        {
            lblInactividad = new Label();
            lblInactividad.Text = "Tiempo restante: 20 segundos";
            lblInactividad.Font = new Font("Arial", 12, FontStyle.Bold);
            lblInactividad.ForeColor = Color.Red;
            lblInactividad.AutoSize = true;
            lblInactividad.BackColor = Color.Transparent;
            lblInactividad.Location = new Point(this.ClientSize.Width - 300, 20);
            lblInactividad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(lblInactividad);
            lblInactividad.BringToFront();

            timerInactividad = new Timer();
            timerInactividad.Interval = 1000;
            timerInactividad.Tick += (s, ev) => {
                segundosInactividad--;
                if (segundosInactividad > 0) {
                    lblInactividad.Text = $"Tiempo restante: {segundosInactividad} segundos";
                } else {
                    timerInactividad.Stop();
                    this.Close();
                }
            };
            timerInactividad.Start();
        }

        private void ReiniciarTimer()
        {
            segundosInactividad = 20;
            if (lblInactividad != null && !lblInactividad.IsDisposed)
                lblInactividad.Text = $"Tiempo restante: {segundosInactividad} segundos";
            if (timerInactividad != null && !timerInactividad.Enabled)
                timerInactividad.Start();
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
                if (c.Location.Y < 80 && c != lblTipoRegistro) continue;
                if (c == lblInactividad) continue;
                panelCentro.Controls.Add(c);
            }

            this.Controls.Add(panelCentro);

            this.Resize += (s, e) => {
                panelCentro.Left = (this.ClientSize.Width - panelCentro.Width) / 2;
                panelCentro.Top = (this.ClientSize.Height - panelCentro.Height) / 2;

                if (lblTipoRegistro != null)
                {
                    lblTipoRegistro.Left = (panelCentro.Width - lblTipoRegistro.Width) / 2;
                    lblTipoRegistro.Top = 10;
                }
            };
        }

        private void EnlazarControlesSeguros()
        {
            Control[] lbl1 = this.Controls.Find("lblNombre", true);
            Control[] lbl2 = this.Controls.Find("label1", true);
            Control[] lbl3 = this.Controls.Find("label2", true);

            if (lbl1.Length > 0) etiquetaBienvenida = lbl1[0] as Label;
            else if (lbl2.Length > 0) etiquetaBienvenida = lbl2[0] as Label;
            else if (lbl3.Length > 0) etiquetaBienvenida = lbl3[0] as Label;

            Control[] cmb1 = this.Controls.Find("cmbCurso", true);
            Control[] cmb2 = this.Controls.Find("comboBox1", true);

            if (cmb1.Length > 0) buscadorCurso = cmb1[0] as ComboBox;
            else if (cmb2.Length > 0) buscadorCurso = cmb2[0] as ComboBox;

            Control[] btnR1 = this.Controls.Find("btnRegistrar", true);
            Control[] btnR2 = this.Controls.Find("btnConfirmar", true);
            Control[] btnR3 = this.Controls.Find("button1", true);

            Control btnRegistrar = btnR1.Length > 0 ? btnR1[0] : (btnR2.Length > 0 ? btnR2[0] : (btnR3.Length > 0 ? btnR3[0] : null));
            if (btnRegistrar != null)
            {
                btnRegistrar.Click -= EjecutarRegistroAsistencia;
                btnRegistrar.Click += EjecutarRegistroAsistencia;
            }

            Control[] btnC1 = this.Controls.Find("btnCancelar", true);
            Control[] btnC2 = this.Controls.Find("button2", true);
            Control[] btnC3 = this.Controls.Find("cancelarysalir", true);
            Control btnCancelar = btnC1.Length > 0 ? btnC1[0] : (btnC2.Length > 0 ? btnC2[0] : (btnC3.Length > 0 ? btnC3[0] : null));
            if (btnCancelar != null)
            {
                btnCancelar.Click -= CerrarVentana;
                btnCancelar.Click += CerrarVentana;
            }
        }

        private void FormRegistroCurso_Load(object sender, EventArgs e)
        {
            if (etiquetaBienvenida != null)
            {
                etiquetaBienvenida.Text = "Bienvenido: " + docenteNombreActual;
            }

            if (modoHorarioAutomatico)
            {
                OcultarControlesOriginales();

                if (lstCursosHoy != null)
                {
                    Timer timerFoco = new Timer();
                    timerFoco.Interval = 150;
                    timerFoco.Tick += (s, ev) =>
                    {
                        timerFoco.Stop();
                        lstCursosHoy.Select();
                        lstCursosHoy.Focus();
                    };
                    timerFoco.Start();
                }
                return;
            }

            if (esSalida)
            {
                if (buscadorCurso != null)
                {
                    buscadorCurso.Visible = false;
                    
                    Control[] labels = this.Controls.Find("label1", true);
                    foreach (Control lbl in labels)
                    {
                        if (lbl is Label && lbl.Text.ToLower().Contains("curso"))
                        {
                            lbl.Visible = false;
                        }
                    }
                }

                foreach (Control c in this.Controls)
                {
                    if (c is Panel pnl)
                    {
                        foreach (Control child in pnl.Controls)
                        {
                            string n = child.Name.ToLower();
                            if (n.Contains("btn") || n.Contains("button") || n.Contains("cancel"))
                            {
                                child.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        string n = c.Name.ToLower();
                        if (n.Contains("btn") || n.Contains("button") || n.Contains("cancel"))
                        {
                            c.Visible = false;
                        }
                    }
                }
            }
            else
            {
                Timer timerFoco = new Timer();
                timerFoco.Interval = 150;
                timerFoco.Tick += (s, ev) =>
                {
                    timerFoco.Stop();
                    if (buscadorCurso != null)
                    {
                        buscadorCurso.Select();
                        buscadorCurso.Focus();
                    }
                };
                timerFoco.Start();
            }
        }

        private void OcultarControlesOriginales()
        {
            if (buscadorCurso != null) buscadorCurso.Visible = false;
            if (guna2GroupBox1 != null) guna2GroupBox1.Visible = false;
            if (lblTitulo != null) lblTitulo.Visible = false;

            foreach (Control c in this.Controls)
            {
                string n = c.Name.ToLower();
                if (n.Contains("btn") || n.Contains("button") || n.Contains("cancel") || n.Contains("cmb") || n.Contains("combo") || c == guna2GroupBox1 || c == lblTitulo)
                {
                    if (c != lstCursosHoy && c != lblTipoRegistro && c != lblInactividad && c != etiquetaBienvenida && c != pnlAuto)
                        c.Visible = false;
                }
                if (c is Panel pnl)
                {
                    foreach (Control child in pnl.Controls)
                    {
                        string nc = child.Name.ToLower();
                        if (nc.Contains("btn") || nc.Contains("button") || nc.Contains("cancel") || nc.Contains("cmb") || nc.Contains("combo"))
                        {
                            child.Visible = false;
                        }
                    }
                }
            }
        }

        private string RemoverTildes(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return "";
            string consignos = "\u00e1\u00e0\u00e4\u00e9\u00e8\u00eb\u00ed\u00ec\u00ef\u00f3\u00f2\u00f6\u00fa\u00f9\u00fc\u00f1\u00c1\u00c0\u00c4\u00c9\u00c8\u00cb\u00cd\u00cc\u00cf\u00d3\u00d2\u00d6\u00da\u00d9\u00dc\u00d1";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUN";
            for (int v = 0; v < consignos.Length; v++)
            {
                if (texto.Contains(consignos[v]))
                    texto = texto.Replace(consignos[v].ToString(), sinsignos[v].ToString());
            }
            return texto;
        }

        private void CargarAsignaturasDesdeBD()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Nombre FROM Cursos ORDER BY Nombre ASC", con);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) listaMaestraCursos.Add(dr["Nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error DB: " + ex.Message);
                }
            }
        }

        private void ConfigurarBuscadorCursos()
        {
            buscadorCurso.DropDownStyle = ComboBoxStyle.DropDown;
            buscadorCurso.AutoCompleteMode = AutoCompleteMode.None;
            buscadorCurso.MaxDropDownItems = 2;

            buscadorCurso.TextUpdate -= BuscadorCurso_TextUpdate;
            buscadorCurso.TextUpdate += BuscadorCurso_TextUpdate;
            
            buscadorCurso.KeyDown -= BuscadorCurso_KeyDown;
            buscadorCurso.KeyDown += BuscadorCurso_KeyDown;
        }

        private bool backspacePressed = false;
        private bool ignorarTextUpdate = false;

        private void BuscadorCurso_KeyDown(object sender, KeyEventArgs e)
        {
            ReiniciarTimer();
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                backspacePressed = true;
            else
                backspacePressed = false;
        }

        private void BuscadorCurso_TextUpdate(object sender, EventArgs e)
        {
            ReiniciarTimer();
            if (ignorarTextUpdate) return;

            string textoOriginal = buscadorCurso.Text.ToUpper();
            int posicionCursor = buscadorCurso.SelectionStart;

            buscadorCurso.TextUpdate -= BuscadorCurso_TextUpdate;
            buscadorCurso.Items.Clear();

            if (!string.IsNullOrWhiteSpace(textoOriginal))
            {
                string textoLimpio = RemoverTildes(textoOriginal);

                var coincidencias = listaMaestraCursos
                    .Where(c => RemoverTildes(c).ToUpper().Contains(textoLimpio))
                    .Take(2)
                    .ToArray();

                if (coincidencias.Length > 0)
                {
                    buscadorCurso.Items.AddRange(coincidencias);
                    buscadorCurso.DroppedDown = true;

                    string primerItem = coincidencias[0];
                    if (!backspacePressed && primerItem.StartsWith(textoOriginal, StringComparison.OrdinalIgnoreCase))
                    {
                        ignorarTextUpdate = true;
                        
                        buscadorCurso.SelectedIndex = 0; 
                        
                        buscadorCurso.SelectionStart = textoOriginal.Length;
                        buscadorCurso.SelectionLength = primerItem.Length - textoOriginal.Length;
                        
                        ignorarTextUpdate = false;
                        buscadorCurso.TextUpdate += BuscadorCurso_TextUpdate;
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }
            }

            buscadorCurso.Text = textoOriginal;
            buscadorCurso.SelectionStart = posicionCursor;

            buscadorCurso.TextUpdate += BuscadorCurso_TextUpdate;
            Cursor.Current = Cursors.Default;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (modoHorarioAutomatico)
                {
                    if (cursoIdSeleccionadoAuto > 0)
                    {
                        EjecutarRegistroAutomatico();
                    }
                    return true;
                }

                if (buscadorCurso != null && buscadorCurso.DroppedDown)
                {
                    string textoActual = buscadorCurso.Text;
                    buscadorCurso.DroppedDown = false;
                    buscadorCurso.Text = textoActual;
                    buscadorCurso.SelectionStart = textoActual.Length;
                    buscadorCurso.SelectionLength = 0;
                    return true;
                }

                EjecutarRegistroAsistencia(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (modoHorarioAutomatico)
                EjecutarRegistroAutomatico();
            else
                EjecutarRegistroAsistencia(sender, e);
        }

        private void EjecutarRegistroAutomatico()
        {
            if (cursoIdSeleccionadoAuto <= 0) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    if (timerInactividad != null) timerInactividad.Stop();

                    string tipoReg = esSalida ? "SALIDA" : "INGRESO";
                    Color colorFondo = esSalida ? Color.FromArgb(50, 30, 30) : Color.FromArgb(34, 45, 50);

                    Form confirmForm = new Form();
                    confirmForm.Size = new Size(900, 500);
                    confirmForm.StartPosition = FormStartPosition.CenterParent;
                    confirmForm.FormBorderStyle = FormBorderStyle.None;
                    confirmForm.BackColor = colorFondo;
                    confirmForm.KeyPreview = true;
                    confirmForm.Paint += (senderPaint, evPaint) => {
                        var borderColor = esSalida ? Color.Tomato : Color.FromArgb(52, 152, 219);
                        evPaint.Graphics.DrawRectangle(new Pen(borderColor, 5), 0, 0, confirmForm.Width - 1, confirmForm.Height - 1);
                    };

                    Label lblInfo = new Label();
                    lblInfo.Text = $"CONFIRMAR {tipoReg}\n\nDocente: {docenteNombreActual}\nCodigo: {docenteCodigoActual}\nCurso: {cursoNombreSeleccionadoAuto}\n\n[ Presione ENTER para confirmar ]";
                    lblInfo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                    lblInfo.ForeColor = Color.White;
                    lblInfo.TextAlign = ContentAlignment.MiddleCenter;
                    lblInfo.Dock = DockStyle.Fill;
                    confirmForm.Controls.Add(lblInfo);

                    Label lblTimer = new Label();
                    lblTimer.Text = "Cancelando en 10...";
                    lblTimer.Dock = DockStyle.Bottom;
                    lblTimer.Height = 50;
                    lblTimer.TextAlign = ContentAlignment.MiddleCenter;
                    lblTimer.ForeColor = Color.FromArgb(231, 76, 60);
                    lblTimer.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    confirmForm.Controls.Add(lblTimer);

                    int timeLeft = 10;
                    Timer t = new Timer() { Interval = 1000 };
                    t.Tick += (sTimer, evTimer) => {
                        timeLeft--;
                        if (timeLeft > 0) { lblTimer.Text = $"Cancelando en {timeLeft}..."; }
                        else { t.Stop(); confirmForm.DialogResult = DialogResult.Cancel; confirmForm.Close(); }
                    };
                    confirmForm.KeyDown += (sKey, evKey) => {
                        if (evKey.KeyCode == Keys.Enter) { t.Stop(); confirmForm.DialogResult = DialogResult.OK; confirmForm.Close(); }
                    };
                    confirmForm.Load += (sLoad, evLoad) => { t.Start(); };

                    if (confirmForm.ShowDialog() != DialogResult.OK)
                    {
                        ReiniciarTimer();
                        return;
                    }

                    int? periodoIdActual = null;
                    using (SqlCommand cmdPeriodo = new SqlCommand("SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1", con))
                    {
                        object pResult = cmdPeriodo.ExecuteScalar();
                        if (pResult != null) periodoIdActual = Convert.ToInt32(pResult);
                    }

                    string mensajeExito = "";

                    if (esSalida && registroIdActualSalida.HasValue)
                    {
                        string queryUpdate = "UPDATE RegistroAsistencia SET HoraSalida = @HoraSalida WHERE Id = @Id";
                        using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con))
                        {
                            cmdUpdate.Parameters.AddWithValue("@HoraSalida", DateTime.Now);
                            cmdUpdate.Parameters.AddWithValue("@Id", registroIdActualSalida.Value);
                            cmdUpdate.ExecuteNonQuery();
                        }
                        mensajeExito = $"SALIDA REGISTRADA!\n\nCurso: {cursoNombreSeleccionadoAuto}\nHora: {DateTime.Now.ToString("HH:mm:ss")}";
                    }
                    else
                    {
                        string queryInsertar = @"INSERT INTO RegistroAsistencia (DocenteId, CursoId, FechaHora, PeriodoId) VALUES (@DocenteId, @CursoId, @FechaHora, @PeriodoId)";
                        using (SqlCommand cmdInsertar = new SqlCommand(queryInsertar, con))
                        {
                            cmdInsertar.Parameters.AddWithValue("@DocenteId", docenteIdActual);
                            cmdInsertar.Parameters.AddWithValue("@CursoId", cursoIdSeleccionadoAuto);
                            cmdInsertar.Parameters.AddWithValue("@FechaHora", DateTime.Now);
                            cmdInsertar.Parameters.AddWithValue("@PeriodoId", periodoIdActual.HasValue ? (object)periodoIdActual.Value : DBNull.Value);
                            cmdInsertar.ExecuteNonQuery();
                        }
                        mensajeExito = $"ASISTENCIA REGISTRADA!\n\nCurso: {cursoNombreSeleccionadoAuto}\nHora: {DateTime.Now.ToString("HH:mm:ss")}";
                    }

                    MostrarMensajeExito(mensajeExito);
                }
                catch (Exception ex)
                {
                    AutoClosingMessageBox.Show("Error al registrar:\n" + ex.Message, "Error Critico", 2000, MessageBoxIcon.Error);
                }
            }
        }

        private void MostrarMensajeExito(string mensajeExito)
        {
            Form msgForm = new Form();
            msgForm.Size = new Size(900, 500);
            msgForm.StartPosition = FormStartPosition.CenterParent;
            msgForm.FormBorderStyle = FormBorderStyle.None;
            msgForm.BackColor = Color.FromArgb(46, 204, 113);
            msgForm.Paint += (senderPaint, evPaint) => {
                evPaint.Graphics.DrawRectangle(new Pen(Color.White, 4), 2, 2, msgForm.Width - 5, msgForm.Height - 5);
            };

            Label lblMsg = new Label();
            lblMsg.Text = mensajeExito;
            lblMsg.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblMsg.ForeColor = Color.White;
            lblMsg.TextAlign = ContentAlignment.MiddleCenter;
            lblMsg.Dock = DockStyle.Fill;
            msgForm.Controls.Add(lblMsg);

            Timer timerCierre = new Timer();
            timerCierre.Interval = 2000;
            timerCierre.Tick += (s, args) => {
                timerCierre.Stop();
                msgForm.Close();
                this.Close();
            };

            timerCierre.Start();
            msgForm.ShowDialog();
        }

        private void EjecutarRegistroAsistencia(object sender, EventArgs e)
        {
            string cursoInput = buscadorCurso != null ? buscadorCurso.Text.Trim() : "";

            if (!esSalida && string.IsNullOrWhiteSpace(cursoInput))
            {
                AutoClosingMessageBox.Show("Por favor, seleccione o escriba su curso.", "Campo requerido", 2000, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    int idCursoActual = 0;
                    if (esSalida)
                    {
                        idCursoActual = idCursoSalida.Value;
                        cursoInput = nombreCursoSalida;
                    }
                    else
                    {
                        string queryCheck = "SELECT Id FROM Cursos WHERE Nombre = @NombreCurso";
                        using (SqlCommand cmdCheck = new SqlCommand(queryCheck, con))
                        {
                            cmdCheck.Parameters.AddWithValue("@NombreCurso", cursoInput);
                            object result = cmdCheck.ExecuteScalar();
                            if (result == null)
                            {
                                AutoClosingMessageBox.Show("El curso ingresado no existe en la base de datos.", "Error de validacion", 2000, MessageBoxIcon.Error);
                                return;
                            }
                            idCursoActual = Convert.ToInt32(result);
                        }
                    }

                    if (timerInactividad != null) timerInactividad.Stop();

                    if (!esSalida)
                    {
                        Form confirmForm = new Form();
                        confirmForm.Size = new Size(900, 500);
                        confirmForm.StartPosition = FormStartPosition.CenterParent;
                        confirmForm.FormBorderStyle = FormBorderStyle.None;
                        confirmForm.BackColor = Color.FromArgb(34, 45, 50);
                        confirmForm.KeyPreview = true;
                        confirmForm.Paint += (senderPaint, evPaint) => {
                            evPaint.Graphics.DrawRectangle(new Pen(Color.FromArgb(52, 152, 219), 5), 0, 0, confirmForm.Width - 1, confirmForm.Height - 1);
                        };

                        Label lblInfo = new Label();
                        string tituloConf = "CONFIRMAR INGRESO";
                        lblInfo.Text = $"{tituloConf}\n\nDocente: {docenteNombreActual}\nCodigo: {docenteCodigoActual}\nCurso: {cursoInput}\n\n[ Presione ENTER para confirmar ]";
                        lblInfo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                        lblInfo.ForeColor = Color.White;
                        lblInfo.TextAlign = ContentAlignment.MiddleCenter;
                        lblInfo.Dock = DockStyle.Fill;
                        confirmForm.Controls.Add(lblInfo);

                        Label lblTimer = new Label();
                        lblTimer.Text = "Cancelando en 10...";
                        lblTimer.Dock = DockStyle.Bottom;
                        lblTimer.Height = 50;
                        lblTimer.TextAlign = ContentAlignment.MiddleCenter;
                        lblTimer.ForeColor = Color.FromArgb(231, 76, 60);
                        lblTimer.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                        confirmForm.Controls.Add(lblTimer);

                        int timeLeft = 10;
                        Timer t = new Timer() { Interval = 1000 };
                        t.Tick += (sTimer, evTimer) => {
                            timeLeft--;
                            if (timeLeft > 0) {
                                lblTimer.Text = $"Cancelando en {timeLeft}...";
                            } else {
                                t.Stop();
                                confirmForm.DialogResult = DialogResult.Cancel;
                                confirmForm.Close();
                            }
                        };

                        confirmForm.KeyDown += (sKey, evKey) => {
                            if (evKey.KeyCode == Keys.Enter) {
                                t.Stop();
                                confirmForm.DialogResult = DialogResult.OK;
                                confirmForm.Close();
                            }
                        };

                        confirmForm.Load += (sLoad, evLoad) => {
                            t.Start();
                        };

                        if (confirmForm.ShowDialog() != DialogResult.OK)
                        {
                            if (buscadorCurso != null) {
                                buscadorCurso.Text = "";
                                buscadorCurso.Focus();
                            }
                            ReiniciarTimer();
                            return;
                        }
                    }

                    int? periodoIdActual = null;
                    using (SqlCommand cmdPeriodo = new SqlCommand("SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1", con))
                    {
                        object pResult = cmdPeriodo.ExecuteScalar();
                        if (pResult != null) periodoIdActual = Convert.ToInt32(pResult);
                    }

                    int? registroIdActual = null;
                    string queryCheckEntrada = "SELECT TOP 1 Id FROM RegistroAsistencia WHERE DocenteId = @DocenteId AND CursoId = @CursoId AND CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE) AND HoraSalida IS NULL ORDER BY FechaHora DESC";
                    using (SqlCommand cmdCheckEntrada = new SqlCommand(queryCheckEntrada, con))
                    {
                        cmdCheckEntrada.Parameters.AddWithValue("@DocenteId", docenteIdActual);
                        cmdCheckEntrada.Parameters.AddWithValue("@CursoId", idCursoActual);
                        object result = cmdCheckEntrada.ExecuteScalar();
                        if (result != null)
                        {
                            registroIdActual = Convert.ToInt32(result);
                        }
                    }

                    string mensajeExito = "";

                    if (registroIdActual.HasValue)
                    {
                        string queryUpdate = "UPDATE RegistroAsistencia SET HoraSalida = @HoraSalida WHERE Id = @Id";
                        using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con))
                        {
                            cmdUpdate.Parameters.AddWithValue("@HoraSalida", DateTime.Now);
                            cmdUpdate.Parameters.AddWithValue("@Id", registroIdActual.Value);
                            cmdUpdate.ExecuteNonQuery();
                        }
                        mensajeExito = $"SALIDA REGISTRADA!\n\nCurso: {cursoInput}\nHora: {DateTime.Now.ToString("HH:mm:ss")}";
                    }
                    else
                    {
                        string queryInsertar = @"INSERT INTO RegistroAsistencia (DocenteId, CursoId, FechaHora, PeriodoId) VALUES (@DocenteId, @CursoId, @FechaHora, @PeriodoId)";
                        using (SqlCommand cmdInsertar = new SqlCommand(queryInsertar, con))
                        {
                            cmdInsertar.Parameters.AddWithValue("@DocenteId", docenteIdActual);
                            cmdInsertar.Parameters.AddWithValue("@CursoId", idCursoActual);
                            cmdInsertar.Parameters.AddWithValue("@FechaHora", DateTime.Now);
                            cmdInsertar.Parameters.AddWithValue("@PeriodoId", periodoIdActual.HasValue ? (object)periodoIdActual.Value : DBNull.Value);
                            cmdInsertar.ExecuteNonQuery();
                        }
                        mensajeExito = $"ASISTENCIA REGISTRADA!\n\nCurso: {cursoInput}\nHora: {DateTime.Now.ToString("HH:mm:ss")}";
                    }

                    MostrarMensajeExito(mensajeExito);
                }
                catch (Exception ex)
                {
                    AutoClosingMessageBox.Show("Error de conexion al registrar:\n" + ex.Message, "Error Critico", 2000, MessageBoxIcon.Error);
                }
            }
        }

        private void CerrarVentana(object sender, EventArgs e)
        {
            this.Close();
        }

        // Eventos vacíos requeridos por el diseñador
        private void cmbCurso_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtCurso_TextChanged(object sender, EventArgs e) { }
        private void FormRegistroCurso_Load_1(object sender, EventArgs e) { }
        private void FormRegistroCurso_VisibleChanged(object sender, EventArgs e) { }
        private void lblNombre_Click(object sender, EventArgs e) { }
    }
}
