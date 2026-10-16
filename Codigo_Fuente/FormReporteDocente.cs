using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class FormReporteDocente : Form
    {
        private int docenteId;
        private string docenteNombre;
        private string docenteCodigo;
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        private int _puntuales, _tardanzas, _faltas, _programadas;
        private List<InasistenciaInfo> _inasistencias = new List<InasistenciaInfo>();

        private class InasistenciaInfo
        {
            public DateTime Fecha;
            public string CursoNombre;
            public int CursoId;
            public int DiaSemana;
            public string Justificacion;
        }

        public FormReporteDocente(int id, string nombre, string codigo)
        {
            docenteId = id;
            docenteNombre = nombre;
            docenteCodigo = codigo;

            InitializeComponent();
            ConfigurarUI();
        }

        private void ConfigurarUI()
        {
            this.Text = "Récord de Asistencia - 16 Semanas";
            this.Size = new Size(800, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(34, 45, 50);

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "REPORTE DE ASISTENCIA";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.AutoSize = false;
            lblTitulo.Width = 750;
            lblTitulo.Height = 45;
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            // Docente
            Label lblDocente = new Label();
            lblDocente.Text = $"Docente: {docenteNombre} ({docenteCodigo})";
            lblDocente.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lblDocente.ForeColor = Color.LightGray;
            lblDocente.AutoSize = false;
            lblDocente.Width = 750;
            lblDocente.Height = 30;
            lblDocente.Location = new Point(20, 60);
            lblDocente.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblDocente);

            // Panel de estadísticas numéricas
            Panel pnlStats = new Panel();
            pnlStats.Size = new Size(730, 140);
            pnlStats.Location = new Point(25, 100);
            pnlStats.BackColor = Color.FromArgb(50, 60, 70);
            this.Controls.Add(pnlStats);

            // Panel del gráfico de barras
            Panel pnlGrafico = new Panel();
            pnlGrafico.Size = new Size(730, 220);
            pnlGrafico.Location = new Point(25, 255);
            pnlGrafico.BackColor = Color.FromArgb(50, 60, 70);
            pnlGrafico.Paint += PnlGrafico_Paint;
            this.Controls.Add(pnlGrafico);

            // Botón Justificar Inasistencia
            Button btnJustificar = new Button();
            btnJustificar.Text = "📝 Justificar Inasistencia";
            btnJustificar.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnJustificar.Size = new Size(300, 45);
            btnJustificar.Location = new Point(25, 490);
            btnJustificar.BackColor = Color.FromArgb(52, 152, 219);
            btnJustificar.ForeColor = Color.White;
            btnJustificar.FlatStyle = FlatStyle.Flat;
            btnJustificar.Cursor = Cursors.Hand;
            btnJustificar.Click += BtnJustificar_Click;
            this.Controls.Add(btnJustificar);

            // Botón Cerrar
            Button btnCerrar = new Button();
            btnCerrar.Text = "CERRAR";
            btnCerrar.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCerrar.Size = new Size(180, 45);
            btnCerrar.Location = new Point(545, 490);
            btnCerrar.BackColor = Color.IndianRed;
            btnCerrar.ForeColor = Color.White;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);

            // Timer auto cierre
            Label lblTimer = new Label();
            lblTimer.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTimer.ForeColor = Color.FromArgb(231, 76, 60);
            lblTimer.Location = new Point(25, 545);
            lblTimer.AutoSize = true;
            lblTimer.Text = "Cerrando en 30 segundos...";
            this.Controls.Add(lblTimer);

            int tiempoRestante = 30;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, ev) =>
            {
                tiempoRestante--;
                if (tiempoRestante > 0)
                    lblTimer.Text = $"Cerrando en {tiempoRestante} segundos...";
                else
                {
                    timer.Stop();
                    this.Close();
                }
            };
            timer.Start();

            CargarYCalcularEstadisticas(pnlStats);
        }

        private void PnlGrafico_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = (Panel)sender;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int total = _puntuales + _tardanzas + _faltas;
            if (total == 0)
            {
                g.DrawString("No hay datos para graficar", new Font("Segoe UI", 14), Brushes.Gray, 200, 90);
                return;
            }

            // Título del gráfico
            g.DrawString("Distribución de Asistencia", new Font("Segoe UI", 14, FontStyle.Bold), Brushes.White, 220, 10);

            int barX = 180;
            int barMaxWidth = 480;
            int barHeight = 40;
            int startY = 50;
            int spacing = 55;

            // Datos de las barras
            var barras = new[]
            {
                new { Label = "Asistencias", Valor = _puntuales, Color = Color.FromArgb(46, 204, 113) },
                new { Label = "Tardanzas", Valor = _tardanzas, Color = Color.FromArgb(243, 156, 18) },
                new { Label = "Inasistencias", Valor = _faltas, Color = Color.FromArgb(231, 76, 60) }
            };

            for (int i = 0; i < barras.Length; i++)
            {
                int y = startY + (i * spacing);
                float porcentaje = (float)barras[i].Valor / total;
                int barWidth = Math.Max((int)(barMaxWidth * porcentaje), (barras[i].Valor > 0 ? 5 : 0));

                // Etiqueta
                g.DrawString(barras[i].Label, new Font("Segoe UI", 11, FontStyle.Bold),
                    new SolidBrush(barras[i].Color), 10, y + 8);

                // Barra de fondo
                using (var bgBrush = new SolidBrush(Color.FromArgb(70, 80, 90)))
                    g.FillRectangle(bgBrush, barX, y, barMaxWidth, barHeight);

                // Barra de valor
                if (barWidth > 0)
                {
                    using (var brush = new LinearGradientBrush(
                        new Rectangle(barX, y, barWidth, barHeight),
                        barras[i].Color, ControlPaint.Dark(barras[i].Color, 0.2f),
                        LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, barX, y, barWidth, barHeight);
                    }
                }

                // Texto dentro/al lado de la barra
                string textoValor = $"{barras[i].Valor}  ({(porcentaje * 100):F0}%)";
                float textoX = barX + barWidth + 8;
                if (textoX + 80 > barX + barMaxWidth)
                    textoX = barX + 8; // Dentro de la barra si es muy ancha

                g.DrawString(textoValor, new Font("Segoe UI", 10, FontStyle.Bold),
                    Brushes.White, textoX, y + 10);
            }
        }

        private class HorarioInfo
        {
            public int CursoId;
            public string CursoNombre;
            public int DiaSemana;
            public TimeSpan HoraInicio;
        }

        private class RegistroInfo
        {
            public int CursoId;
            public DateTime FechaHora;
            public string Justificacion;
        }

        private void CargarYCalcularEstadisticas(Panel pnl)
        {
            List<HorarioInfo> horarios = new List<HorarioInfo>();
            List<RegistroInfo> registros = new List<RegistroInfo>();

            DateTime hoy = DateTime.Now.Date;
            DateTime hace16Semanas = hoy.AddDays(-16 * 7);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string qHorario = @"SELECT h.CursoId, c.Nombre as CursoNombre, h.DiaSemana, h.HoraInicio 
                        FROM HorarioClases h 
                        INNER JOIN Cursos c ON h.CursoId = c.Id
                        WHERE h.DocenteId = @DocId 
                        AND (h.PeriodoId = (SELECT TOP 1 Id FROM Periodos WHERE EsActivo=1) OR h.PeriodoId IS NULL)";
                    using (SqlCommand cmd = new SqlCommand(qHorario, con))
                    {
                        cmd.Parameters.AddWithValue("@DocId", docenteId);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                horarios.Add(new HorarioInfo {
                                    CursoId = Convert.ToInt32(dr["CursoId"]),
                                    CursoNombre = dr["CursoNombre"].ToString(),
                                    DiaSemana = Convert.ToInt32(dr["DiaSemana"]),
                                    HoraInicio = (TimeSpan)dr["HoraInicio"]
                                });
                            }
                        }
                    }

                    string qRegistros = @"SELECT CursoId, FechaHora, Justificacion FROM RegistroAsistencia 
                        WHERE DocenteId = @DocId AND FechaHora >= @Inicio AND FechaHora <= @Fin";
                    using (SqlCommand cmd = new SqlCommand(qRegistros, con))
                    {
                        cmd.Parameters.AddWithValue("@DocId", docenteId);
                        cmd.Parameters.AddWithValue("@Inicio", hace16Semanas);
                        cmd.Parameters.AddWithValue("@Fin", hoy.AddDays(1).AddTicks(-1));
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                registros.Add(new RegistroInfo {
                                    CursoId = Convert.ToInt32(dr["CursoId"]),
                                    FechaHora = Convert.ToDateTime(dr["FechaHora"]),
                                    Justificacion = dr["Justificacion"] == DBNull.Value ? null : dr["Justificacion"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos: " + ex.Message);
                    return;
                }
            }

            int programadas = 0;
            int puntuales = 0;
            int tardanzas = 0;
            int faltas = 0;
            _inasistencias.Clear();

            for (DateTime d = hace16Semanas; d <= hoy; d = d.AddDays(1))
            {
                int diaSQL = ObtenerDiaSemanaSQL(d.DayOfWeek);
                var clasesDia = horarios.Where(h => h.DiaSemana == diaSQL).ToList();

                foreach (var c in clasesDia)
                {
                    // Si es hoy, solo contamos como falta si ya pasó la hora de la clase + 30 minutos
                    if (d.Date == hoy)
                    {
                        TimeSpan ahora = DateTime.Now.TimeOfDay;
                        if (ahora < c.HoraInicio.Add(TimeSpan.FromMinutes(30)))
                            continue;
                    }

                    programadas++;
                    var reg = registros.FirstOrDefault(r => r.CursoId == c.CursoId && r.FechaHora.Date == d.Date);
                    
                    if (reg == null)
                    {
                        faltas++;
                        _inasistencias.Add(new InasistenciaInfo
                        {
                            Fecha = d,
                            CursoNombre = c.CursoNombre,
                            CursoId = c.CursoId,
                            DiaSemana = c.DiaSemana,
                            Justificacion = null
                        });
                    }
                    else
                    {
                        // Verificar si tiene justificación de inasistencia guardada como registro especial
                        TimeSpan diff = reg.FechaHora.TimeOfDay - c.HoraInicio;
                        if (diff.TotalMinutes > 15) tardanzas++;
                        else puntuales++;
                    }
                }
            }

            // Cargar justificaciones ya existentes
            CargarJustificacionesExistentes();

            _puntuales = puntuales;
            _tardanzas = tardanzas;
            _faltas = faltas;
            _programadas = programadas;

            Label lblProg = CrearLabel($"Clases Programadas (Últ. 16 sem):   {programadas}", 20, 15, Color.White);
            Label lblPunt = CrearLabel($"Asistencias Puntuales:   {puntuales}", 20, 50, Color.FromArgb(46, 204, 113));
            Label lblTard = CrearLabel($"Tardanzas (>15 min):   {tardanzas}", 20, 85, Color.FromArgb(243, 156, 18));
            Label lblFalt = CrearLabel($"Inasistencias:   {faltas}", 420, 85, Color.FromArgb(231, 76, 60));

            pnl.Controls.Add(lblProg);
            pnl.Controls.Add(lblPunt);
            pnl.Controls.Add(lblTard);
            pnl.Controls.Add(lblFalt);
        }

        private void CargarJustificacionesExistentes()
        {
            // Cargar justificaciones de la base de datos (registros con Justificacion no nula)
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string q = @"SELECT CursoId, CAST(FechaHora AS DATE) as Fecha, Justificacion 
                        FROM RegistroAsistencia 
                        WHERE DocenteId = @DocId AND Justificacion IS NOT NULL";
                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        cmd.Parameters.AddWithValue("@DocId", docenteId);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int cursoId = Convert.ToInt32(dr["CursoId"]);
                                DateTime fecha = Convert.ToDateTime(dr["Fecha"]);
                                string just = dr["Justificacion"].ToString();
                                
                                var inasistencia = _inasistencias.FirstOrDefault(
                                    i => i.CursoId == cursoId && i.Fecha.Date == fecha.Date);
                                if (inasistencia != null)
                                    inasistencia.Justificacion = just;
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void BtnJustificar_Click(object sender, EventArgs e)
        {
            if (_inasistencias.Count == 0)
            {
                MessageBox.Show("No tienes inasistencias registradas para justificar.", "Sin inasistencias",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (Form frmJustificar = new Form())
            {
                frmJustificar.Text = "Justificar Inasistencia";
                frmJustificar.Size = new Size(650, 500);
                frmJustificar.StartPosition = FormStartPosition.CenterParent;
                frmJustificar.FormBorderStyle = FormBorderStyle.FixedDialog;
                frmJustificar.MaximizeBox = false;
                frmJustificar.MinimizeBox = false;
                frmJustificar.BackColor = Color.FromArgb(34, 45, 50);

                Label lblTitulo = new Label()
                {
                    Text = "Selecciona la inasistencia a justificar:",
                    Font = new Font("Segoe UI", 13, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(20, 15),
                    AutoSize = true
                };
                frmJustificar.Controls.Add(lblTitulo);

                ListBox lstInasistencias = new ListBox()
                {
                    Location = new Point(20, 50),
                    Size = new Size(590, 180),
                    Font = new Font("Segoe UI", 11),
                    BackColor = Color.FromArgb(50, 60, 70),
                    ForeColor = Color.White
                };

                foreach (var ina in _inasistencias.OrderByDescending(i => i.Fecha))
                {
                    string estado = string.IsNullOrEmpty(ina.Justificacion) ? "❌ Sin justificar" : $"✅ {ina.Justificacion}";
                    lstInasistencias.Items.Add($"{ina.Fecha:dd/MM/yyyy} - {ina.CursoNombre} [{estado}]");
                }
                frmJustificar.Controls.Add(lstInasistencias);

                Label lblMotivo = new Label()
                {
                    Text = "Motivo de justificación:",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(20, 245),
                    AutoSize = true
                };
                frmJustificar.Controls.Add(lblMotivo);

                TextBox txtMotivo = new TextBox()
                {
                    Location = new Point(20, 275),
                    Size = new Size(590, 90),
                    Multiline = true,
                    Font = new Font("Segoe UI", 11),
                    Text = "Ej: Descanso médico, cita en el hospital, emergencia familiar...",
                    ForeColor = Color.Gray
                };
                txtMotivo.GotFocus += (s3, e3) => { if (txtMotivo.ForeColor == Color.Gray) { txtMotivo.Text = ""; txtMotivo.ForeColor = Color.Black; } };
                txtMotivo.LostFocus += (s3, e3) => { if (string.IsNullOrWhiteSpace(txtMotivo.Text)) { txtMotivo.Text = "Ej: Descanso médico, cita en el hospital, emergencia familiar..."; txtMotivo.ForeColor = Color.Gray; } };
                frmJustificar.Controls.Add(txtMotivo);

                Button btnGuardar = new Button()
                {
                    Text = "Guardar Justificación",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(250, 45),
                    Location = new Point(20, 385),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };

                Button btnCerrarJ = new Button()
                {
                    Text = "Cerrar",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(150, 45),
                    Location = new Point(460, 385),
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    DialogResult = DialogResult.Cancel
                };

                var inasistenciasOrdenadas = _inasistencias.OrderByDescending(i => i.Fecha).ToList();

                btnGuardar.Click += (s2, e2) =>
                {
                    if (lstInasistencias.SelectedIndex < 0)
                    {
                        MessageBox.Show("Selecciona una inasistencia de la lista.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtMotivo.Text) || txtMotivo.ForeColor == Color.Gray)
                    {
                        MessageBox.Show("Escribe un motivo de justificación.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var seleccionada = inasistenciasOrdenadas[lstInasistencias.SelectedIndex];
                    GuardarJustificacion(seleccionada, txtMotivo.Text.Trim());
                    
                    seleccionada.Justificacion = txtMotivo.Text.Trim();
                    lstInasistencias.Items[lstInasistencias.SelectedIndex] = 
                        $"{seleccionada.Fecha:dd/MM/yyyy} - {seleccionada.CursoNombre} [✅ {seleccionada.Justificacion}]";
                    
                    MessageBox.Show("Justificación guardada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMotivo.Clear();
                };

                frmJustificar.Controls.Add(btnGuardar);
                frmJustificar.Controls.Add(btnCerrarJ);
                frmJustificar.ShowDialog();
            }
        }

        private void GuardarJustificacion(InasistenciaInfo inasistencia, string motivo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // Verificar si ya existe un registro para esa fecha y curso
                    string qCheck = @"SELECT Id FROM RegistroAsistencia 
                        WHERE DocenteId = @DocId AND CursoId = @CursoId AND CAST(FechaHora AS DATE) = @Fecha";
                    using (SqlCommand cmdCheck = new SqlCommand(qCheck, con))
                    {
                        cmdCheck.Parameters.AddWithValue("@DocId", docenteId);
                        cmdCheck.Parameters.AddWithValue("@CursoId", inasistencia.CursoId);
                        cmdCheck.Parameters.AddWithValue("@Fecha", inasistencia.Fecha.Date);
                        object existeId = cmdCheck.ExecuteScalar();

                        if (existeId != null)
                        {
                            // Actualizar justificación existente
                            string qUpdate = "UPDATE RegistroAsistencia SET Justificacion = @Motivo WHERE Id = @Id";
                            using (SqlCommand cmdUpdate = new SqlCommand(qUpdate, con))
                            {
                                cmdUpdate.Parameters.AddWithValue("@Motivo", motivo);
                                cmdUpdate.Parameters.AddWithValue("@Id", Convert.ToInt32(existeId));
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Crear un registro especial de justificación (sin FechaHora de asistencia real)
                            int? periodoId = null;
                            using (SqlCommand cmdP = new SqlCommand("SELECT TOP 1 Id FROM Periodos WHERE EsActivo = 1", con))
                            {
                                object p = cmdP.ExecuteScalar();
                                if (p != null) periodoId = Convert.ToInt32(p);
                            }

                            string qInsert = @"INSERT INTO RegistroAsistencia (DocenteId, CursoId, FechaHora, PeriodoId, Justificacion) 
                                VALUES (@DocId, @CursoId, @FechaHora, @PeriodoId, @Motivo)";
                            using (SqlCommand cmdInsert = new SqlCommand(qInsert, con))
                            {
                                cmdInsert.Parameters.AddWithValue("@DocId", docenteId);
                                cmdInsert.Parameters.AddWithValue("@CursoId", inasistencia.CursoId);
                                // Usar la fecha de la inasistencia con hora 00:00 para distinguirla de asistencias reales
                                cmdInsert.Parameters.AddWithValue("@FechaHora", inasistencia.Fecha.Date);
                                cmdInsert.Parameters.AddWithValue("@PeriodoId", periodoId.HasValue ? (object)periodoId.Value : DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@Motivo", motivo);
                                cmdInsert.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar justificación: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Label CrearLabel(string texto, int x, int y, Color color)
        {
            Label l = new Label();
            l.Text = texto;
            l.Location = new Point(x, y);
            l.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            l.ForeColor = color;
            l.AutoSize = true;
            return l;
        }

        private int ObtenerDiaSemanaSQL(DayOfWeek dow)
        {
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
    }
}
