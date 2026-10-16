using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class FormRegistroSalida : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
        private int docenteIdActual;
        private string docenteNombreActual;
        private string docenteCodigoActual;
        private string cursoNombre;
        private int registroId;
        
        private Timer timerInactividad;
        private int segundosInactividad = 20;

        public FormRegistroSalida(int docenteId, string nombre, string codigo, string curso, int registroActualId)
        {
            InitializeComponent();
            docenteIdActual = docenteId;
            docenteNombreActual = nombre;
            docenteCodigoActual = codigo;
            cursoNombre = curso;
            registroId = registroActualId;

            this.Load += FormRegistroSalida_Load;
            this.KeyDown += FormRegistroSalida_KeyDown;
        }

        private void FormRegistroSalida_Load(object sender, EventArgs e)
        {
            lblNombre.Text = "Bienvenido: " + docenteNombreActual;
            lblCurso.Text = "Curso: " + cursoNombre;

            IniciarTimerInactividad();
            this.ActiveControl = null;
        }

        private void IniciarTimerInactividad()
        {
            lblInactividad.Text = "Tiempo restante: 20 segundos";
            
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

        private void FormRegistroSalida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RegistrarSalida();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                RegistrarSalida();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void RegistrarSalida()
        {
            if (timerInactividad != null) timerInactividad.Stop();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string queryUpdate = "UPDATE RegistroAsistencia SET HoraSalida = @HoraSalida WHERE Id = @Id";
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con))
                    {
                        cmdUpdate.Parameters.AddWithValue("@HoraSalida", DateTime.Now);
                        cmdUpdate.Parameters.AddWithValue("@Id", registroId);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    Form msgForm = new Form();
                    msgForm.Size = new Size(900, 500);
                    msgForm.StartPosition = FormStartPosition.CenterParent;
                    msgForm.FormBorderStyle = FormBorderStyle.None;
                    msgForm.BackColor = Color.FromArgb(46, 204, 113);
                    msgForm.Paint += (senderPaint, evPaint) => {
                        evPaint.Graphics.DrawRectangle(new Pen(Color.White, 4), 2, 2, msgForm.Width - 5, msgForm.Height - 5);
                    };

                    Label lblMsg = new Label();
                    lblMsg.Text = $"✔ ¡SALIDA REGISTRADA!\n\nCurso: {cursoNombre}\nHora: {DateTime.Now.ToString("HH:mm:ss")}";
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
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar salida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            RegistrarSalida();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (timerInactividad != null) timerInactividad.Stop();
            this.Close();
        }
    }
}
