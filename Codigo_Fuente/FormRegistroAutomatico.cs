using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormRegistroAutomatico : Form
    {
        private int docenteIdActual;
        private string docenteNombreActual;
        private string docenteCodigoActual;
        
        private DataTable cursosHoyList;
        private List<DataRow> cursosPendientesSalidaList;
        private List<DataRow> cursosSinIngresoList;

        public bool esSalida = false;
        public int cursoIdSeleccionadoAuto = 0;
        public string cursoNombreSeleccionadoAuto = "";
        public int? registroIdActualSalida = null;

        private int segundosInactividad = 20;
        private bool _inicializando = false;

        public FormRegistroAutomatico(int id, string nombre, string codigo, DataTable cursosHoy, List<DataRow> cursosPendientesSalida, List<DataRow> cursosSinIngreso)
        {
            InitializeComponent();
            
            docenteIdActual = id;
            docenteNombreActual = nombre;
            docenteCodigoActual = codigo;
            
            cursosHoyList = cursosHoy;
            cursosPendientesSalidaList = cursosPendientesSalida;
            cursosSinIngresoList = cursosSinIngreso;
            
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void FormRegistroAutomatico_Load(object sender, EventArgs e)
        {
            lblNombreDocente.Text = "Docente: " + docenteNombreActual;
            ConfigurarControles();
            CentrarControlesEnPanel();
            IniciarTimerInactividad();
            this.AcceptButton = btnRegistrar;
            if (btnRegistrar.Enabled) btnRegistrar.Select();
            else btnCancelar.Select();
        }

        private void CentrarControlesEnPanel()
        {
            // Centrar los controles horizontalmente dentro del panel
            int panelW = pnlCenter.ClientSize.Width;
            
            lstCursosHoy.Width = Math.Min(900, panelW - 200);
            lstCursosHoy.Left = (panelW - lstCursosHoy.Width) / 2;
            
            lblInstrucciones.Left = lstCursosHoy.Left;
            
            int botonesAncho = btnRegistrar.Width + 30 + btnCancelar.Width;
            int inicioX = (panelW - botonesAncho) / 2;
            btnRegistrar.Left = inicioX;
            btnCancelar.Left = inicioX + btnRegistrar.Width + 30;
            
            btnVerRecord.Left = (panelW - btnVerRecord.Width) / 2;
            lblInactividad.Left = (panelW - lblInactividad.Width) / 2;
        }

        private void FormRegistroAutomatico_Resize(object sender, EventArgs e)
        {
            CentrarControlesEnPanel();
        }

        private bool EsDisponible(DataRow r)
        {
            if (TimeSpan.TryParse(r["HoraInicio"].ToString(), out TimeSpan inicio) && TimeSpan.TryParse(r["HoraFin"].ToString(), out TimeSpan fin))
            {
                TimeSpan ahora = DateTime.Now.TimeOfDay;
                if (ahora >= inicio.Subtract(TimeSpan.FromMinutes(30)) && ahora <= fin) return true;
            }
            return false;
        }

        private void ConfigurarControles()
        {
            lstCursosHoy.Items.Clear();
            
            // 1. Salidas pendientes primero
            foreach (DataRow row in cursosPendientesSalidaList)
            {
                lstCursosHoy.Items.Add($"[X] SALIDA PENDIENTE: {row["CursoNombre"]}");
            }

            // 2. Cursos con ingreso pendiente: disponibles primero, luego fuera de hora
            int primerDisponibleIdx = -1;
            foreach (DataRow row in cursosSinIngresoList)
            {
                string cursoNombre = row["CursoNombre"].ToString();
                string hora = row["HoraInicio"].ToString() + " - " + row["HoraFin"].ToString();
                if (EsDisponible(row))
                {
                    lstCursosHoy.Items.Add($"[>] {cursoNombre}  ({hora})  [DISPONIBLE]");
                    if (primerDisponibleIdx == -1 && cursosPendientesSalidaList.Count == 0)
                        primerDisponibleIdx = lstCursosHoy.Items.Count - 1;
                }
                else
                {
                    lstCursosHoy.Items.Add($"[ ] {cursoNombre}  ({hora})  [FUERA DE HORA]");
                }
            }

            // 3. Cursos ya completados (solo informativos)
            foreach (DataRow rowH in cursosHoyList.Rows)
            {
                int cId = Convert.ToInt32(rowH["CursoId"]);
                bool yaEnLista = false;
                foreach (var r in cursosPendientesSalidaList) if (Convert.ToInt32(r["CursoId"]) == cId) yaEnLista = true;
                foreach (var r in cursosSinIngresoList) if (Convert.ToInt32(r["CursoId"]) == cId) yaEnLista = true;
                
                if (!yaEnLista)
                {
                    string cNombre = rowH["CursoNombre"].ToString();
                    string cHora = rowH["HoraInicio"].ToString() + " - " + rowH["HoraFin"].ToString();
                    lstCursosHoy.Items.Add($"[V] {cNombre}  ({cHora})  [COMPLETADO]");
                }
            }

            // 4. Pre-seleccionar el mejor candidato y cargar datos
            if (lstCursosHoy.Items.Count > 0)
            {
                int idxASeleccionar;
                if (cursosPendientesSalidaList.Count > 0)
                    idxASeleccionar = 0;
                else if (primerDisponibleIdx != -1)
                    idxASeleccionar = primerDisponibleIdx;
                else
                    idxASeleccionar = 0;

                // Cargar datos directamente (sin depender de SelectedIndexChanged)
                string itemTexto = lstCursosHoy.Items[idxASeleccionar].ToString();
                if (itemTexto.Contains("[FUERA DE HORA]") || itemTexto.Contains("[COMPLETADO]"))
                {
                    cursoIdSeleccionadoAuto = 0;
                    btnRegistrar.Enabled = false;
                }
                else if (idxASeleccionar < cursosPendientesSalidaList.Count)
                {
                    esSalida = true;
                    DataRow rowSal = cursosPendientesSalidaList[idxASeleccionar];
                    registroIdActualSalida = Convert.ToInt32(rowSal["Id"]);
                    cursoIdSeleccionadoAuto = Convert.ToInt32(rowSal["CursoId"]);
                    cursoNombreSeleccionadoAuto = rowSal["CursoNombre"].ToString();
                    btnRegistrar.Enabled = true;
                }
                else
                {
                    esSalida = false;
                    int cursoIdx = idxASeleccionar - cursosPendientesSalidaList.Count;
                    if (cursoIdx < cursosSinIngresoList.Count)
                    {
                        DataRow rowIng = cursosSinIngresoList[cursoIdx];
                        cursoIdSeleccionadoAuto = Convert.ToInt32(rowIng["CursoId"]);
                        cursoNombreSeleccionadoAuto = rowIng["CursoNombre"].ToString();
                        btnRegistrar.Enabled = true;
                    }
                }

                // Poner SelectedIndex sin que el evento SelectedIndexChanged pise los datos ya cargados
                _inicializando = true;
                lstCursosHoy.SelectedIndex = idxASeleccionar;
                _inicializando = false;

                // Aviso si todos los cursos ya están completados
                if (itemTexto.Contains("[COMPLETADO]") && cursosSinIngresoList.Count == 0 && cursosPendientesSalidaList.Count == 0)
                {
                    Timer t = new Timer(); t.Interval = 400; t.Tick += (s2, e2) => { t.Stop();
                        FormAviso.Mostrar("Asistencia ya registrada",
                            $"Ya registraste tu asistencia para todos los cursos de hoy, {docenteNombreActual}.",
                            FormAviso.TipoAviso.Informacion, 5);
                    }; t.Start();
                }
            }
        }

        private int lastValidIndex = -1;

        private void lstCursosHoy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (btnRegistrar.Enabled)
                    btnRegistrar.PerformClick();
            }
        }

        private void lstCursosHoy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReiniciarTimer();
            if (_inicializando) return;  // Evitar que pise los datos cargados en ConfigurarControles
            int idx = lstCursosHoy.SelectedIndex;
            if (idx < 0) return;

            string texto = lstCursosHoy.Items[idx].ToString();
            if (texto.Contains("[FUERA DE HORA]") || texto.Contains("[COMPLETADO]"))
            {
                cursoIdSeleccionadoAuto = 0;
                btnRegistrar.Enabled = false;
                if (texto.Contains("[COMPLETADO]"))
                    FormAviso.Mostrar("Ya registrado", "Este curso ya fue registrado hoy. No puedes marcarlo dos veces.", FormAviso.TipoAviso.Advertencia, 5);
                else
                    FormAviso.Mostrar("Fuera de horario", "Este curso está fuera del rango de hora permitido (±30 min del horario).", FormAviso.TipoAviso.Error, 5);
                return;
            }
            
            lastValidIndex = idx;
            btnRegistrar.Enabled = true;

            if (idx < cursosPendientesSalidaList.Count)
            {
                esSalida = true;
                DataRow rowSal = cursosPendientesSalidaList[idx];
                registroIdActualSalida = Convert.ToInt32(rowSal["Id"]);
                cursoIdSeleccionadoAuto = Convert.ToInt32(rowSal["CursoId"]);
                cursoNombreSeleccionadoAuto = rowSal["CursoNombre"].ToString();
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

        private void IniciarTimerInactividad()
        {
            lblInactividad.Text = "Tiempo restante: 20 segundos";
            timerInactividad.Start();
        }

        private void ReiniciarTimer()
        {
            segundosInactividad = 20;
            lblInactividad.Text = $"Tiempo restante: {segundosInactividad} segundos";
            if (!timerInactividad.Enabled) timerInactividad.Start();
        }

        private void timerInactividad_Tick(object sender, EventArgs e)
        {
            segundosInactividad--;
            if (segundosInactividad > 0)
            {
                lblInactividad.Text = $"Tiempo restante: {segundosInactividad} segundos";
            }
            else
            {
                timerInactividad.Stop();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void FormRegistroAutomatico_Paint(object sender, PaintEventArgs e)
        {
            // Sin borde visible ya que ahora ocupa toda la pantalla
        }

        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cursoIdSeleccionadoAuto <= 0) return;
            timerInactividad.Stop();

            // Registro directo sin pantalla de confirmación intermedia
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    int? periodoIdActual = null;
                    using (System.Data.SqlClient.SqlCommand cmdPeriodo = new System.Data.SqlClient.SqlCommand("SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1", con))
                    {
                        object pResult = cmdPeriodo.ExecuteScalar();
                        if (pResult != null) periodoIdActual = Convert.ToInt32(pResult);
                    }

                    string mensajeExito = "";

                    if (esSalida && registroIdActualSalida.HasValue)
                    {
                        // Verificar si es salida anticipada
                        string motivoSalida = null;
                        bool salidaAnticipada = false;
                        
                        try
                        {
                            string qHoraFin = @"SELECT h.HoraFin FROM HorarioClases h 
                                INNER JOIN RegistroAsistencia r ON r.DocenteId = h.DocenteId AND r.CursoId = h.CursoId
                                WHERE r.Id = @RegId AND h.DiaSemana = @Dia
                                AND (h.PeriodoId = (SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1) OR h.PeriodoId IS NULL)";
                            using (var cmdHF = new System.Data.SqlClient.SqlCommand(qHoraFin, con))
                            {
                                cmdHF.Parameters.AddWithValue("@RegId", registroIdActualSalida.Value);
                                int diaHoy = ObtenerDiaSemanaSQL();
                                cmdHF.Parameters.AddWithValue("@Dia", diaHoy);
                                object horaFinObj = cmdHF.ExecuteScalar();
                                if (horaFinObj != null && horaFinObj != DBNull.Value)
                                {
                                    TimeSpan horaFin = (TimeSpan)horaFinObj;
                                    TimeSpan ahora = DateTime.Now.TimeOfDay;
                                    if (ahora < horaFin.Subtract(TimeSpan.FromMinutes(10)))
                                    {
                                        salidaAnticipada = true;
                                        motivoSalida = PedirMotivoSalidaAnticipada(horaFin);
                                    }
                                }
                            }
                        }
                        catch { /* Si falla la verificación, continuar sin marcar anticipada */ }

                        string queryUpdate = "UPDATE RegistroAsistencia SET HoraSalida = @HoraSalida, SalidaAnticipada = @SalidaAnticipada, MotivoSalidaAnticipada = @Motivo WHERE Id = @Id";
                        using (System.Data.SqlClient.SqlCommand cmdUpdate = new System.Data.SqlClient.SqlCommand(queryUpdate, con))
                        {
                            cmdUpdate.Parameters.AddWithValue("@HoraSalida", DateTime.Now);
                            cmdUpdate.Parameters.AddWithValue("@SalidaAnticipada", salidaAnticipada ? 1 : 0);
                            cmdUpdate.Parameters.AddWithValue("@Motivo", (object)motivoSalida ?? DBNull.Value);
                            cmdUpdate.Parameters.AddWithValue("@Id", registroIdActualSalida.Value);
                            cmdUpdate.ExecuteNonQuery();
                        }
                        mensajeExito = $"SALIDA REGISTRADA!\n\nCurso: {cursoNombreSeleccionadoAuto}\nHora: {DateTime.Now.ToString("HH:mm:ss")}";
                        if (salidaAnticipada) mensajeExito += "\n(Salida anticipada registrada)";
                    }
                    else
                    {
                        string queryInsertar = @"INSERT INTO RegistroAsistencia (DocenteId, CursoId, FechaHora, PeriodoId) VALUES (@DocenteId, @CursoId, @FechaHora, @PeriodoId)";
                        using (System.Data.SqlClient.SqlCommand cmdInsertar = new System.Data.SqlClient.SqlCommand(queryInsertar, con))
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
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    FormAviso.Mostrar("Error al registrar", ex.Message, FormAviso.TipoAviso.Error, 7);
                }
            }
        }

        private string PedirMotivoSalidaAnticipada(TimeSpan horaFinProgramada)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 550;
                prompt.Height = 300;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.Text = "Salida Anticipada";
                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.MaximizeBox = false;
                prompt.MinimizeBox = false;
                prompt.BackColor = Color.FromArgb(34, 45, 50);

                Label lblAviso = new Label()
                {
                    Left = 20, Top = 20, Width = 490, Height = 60,
                    Text = $"Estás saliendo antes de la hora programada ({horaFinProgramada.ToString(@"hh\:mm")}).\n¿Cuál es el motivo? (opcional)",
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12)
                };

                TextBox txtMotivo = new TextBox()
                {
                    Left = 20, Top = 90, Width = 490, Height = 80,
                    Multiline = true,
                    Font = new Font("Segoe UI", 11),
                    Text = "Ej: Examen terminó antes, toma de local, descanso médico...",
                    ForeColor = Color.Gray
                };
                txtMotivo.GotFocus += (s3, e3) => { if (txtMotivo.ForeColor == Color.Gray) { txtMotivo.Text = ""; txtMotivo.ForeColor = Color.Black; } };
                txtMotivo.LostFocus += (s3, e3) => { if (string.IsNullOrWhiteSpace(txtMotivo.Text)) { txtMotivo.Text = "Ej: Examen terminó antes, toma de local, descanso médico..."; txtMotivo.ForeColor = Color.Gray; } };

                Button btnGuardar = new Button()
                {
                    Text = "Guardar", Left = 140, Width = 120, Top = 200,
                    Height = 40, BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    DialogResult = DialogResult.OK
                };
                Button btnOmitir = new Button()
                {
                    Text = "Omitir", Left = 280, Width = 120, Top = 200,
                    Height = 40, BackColor = Color.Gray,
                    ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    DialogResult = DialogResult.Cancel
                };

                prompt.Controls.AddRange(new Control[] { lblAviso, txtMotivo, btnGuardar, btnOmitir });
                prompt.AcceptButton = btnGuardar;

                if (prompt.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txtMotivo.Text) && txtMotivo.ForeColor != Color.Gray)
                {
                    return txtMotivo.Text.Trim();
                }
                return null;
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

        private void MostrarMensajeExito(string mensajeExito)
        {
            bool esSalidaLocal = mensajeExito.Contains("SALIDA");
            string titulo = esSalidaLocal ? "Salida registrada" : "Asistencia registrada";
            FormAviso.Mostrar(titulo, $"{docenteNombreActual}\nCurso: {cursoNombreSeleccionadoAuto}\nHora: {DateTime.Now:HH:mm:ss}",
                FormAviso.TipoAviso.Exito, 5);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            timerInactividad.Stop();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnVerRecord_Click(object sender, EventArgs e)
        {
            timerInactividad.Stop();
            FormReporteDocente frmRep = new FormReporteDocente(docenteIdActual, docenteNombreActual, docenteCodigoActual);
            frmRep.ShowDialog();
            ReiniciarTimer();
        }
    }
}
