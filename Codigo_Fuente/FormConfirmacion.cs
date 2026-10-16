using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormConfirmacion : Form
    {
        public FormConfirmacion(string mensaje, string titulo)
        {
            InitializeComponent();
            lblTitulo.Text = titulo;
            lblMensaje.Text = mensaje;
        }

        public static DialogResult Mostrar(string mensaje, string titulo = "Confirmar")
        {
            using (var form = new FormConfirmacion(mensaje, titulo))
            {
                return form.ShowDialog();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void FormConfirmacion_Load(object sender, EventArgs e)
        {
            // Focus cancel by default to prevent accidental enters
            btnCancelar.Select();
        }
    }
}
