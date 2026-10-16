using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormConfirmarAsistencia : Form
    {
        private int timeLeft = 10;

        public FormConfirmarAsistencia(string mensaje, bool esSalida)
        {
            InitializeComponent();
            lblInfo.Text = mensaje;
            
            if (esSalida)
            {
                this.BackColor = Color.FromArgb(50, 30, 30);
                pnlBorder.BackColor = Color.Tomato;
            }
            else
            {
                this.BackColor = Color.FromArgb(34, 45, 50);
                pnlBorder.BackColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void FormConfirmarAsistencia_Load(object sender, EventArgs e)
        {
            timerCancelacion.Start();
            this.AcceptButton = btnConfirmar;
            btnConfirmar.Select();
        }

        private void timerCancelacion_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            if (timeLeft > 0)
            {
                lblTimer.Text = $"Cancelando en {timeLeft}...";
            }
            else
            {
                timerCancelacion.Stop();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            timerCancelacion.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            timerCancelacion.Stop();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
