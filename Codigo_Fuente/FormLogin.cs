using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += FormLogin_Load_Custom;
        }

        private void FormLogin_Load_Custom(object sender, EventArgs e)
        {
            CentrarControles();
            this.WindowState = FormWindowState.Maximized;
        }

        private void CentrarControles()
        {
            Panel panelCentro = new Panel();
            panelCentro.Size = this.ClientSize;
            panelCentro.Location = new System.Drawing.Point(0, 0);
            panelCentro.BackColor = System.Drawing.Color.Transparent;

            Control[] controles = new Control[this.Controls.Count];
            this.Controls.CopyTo(controles, 0);
            foreach (Control c in controles)
            {
                if (c.Location.Y < 80) continue;
                panelCentro.Controls.Add(c);
            }

            this.Controls.Add(panelCentro);

            this.Resize += (s, e) => {
                panelCentro.Left = (this.ClientSize.Width - panelCentro.Width) / 2;
                panelCentro.Top = (this.ClientSize.Height - panelCentro.Height) / 2;
            };
        }

        private string ObtenerContraseñaAdmin()
        {
            string ruta = Path.Combine(Application.StartupPath, "admin_config.txt");
            if (File.Exists(ruta)) return File.ReadAllText(ruta).Trim();
            return "Admin123";
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim().ToLower();
            string clave = txtClave.Text;

            if (usuario == "admin" && clave == ObtenerContraseñaAdmin())
            {
                this.Hide();
                FormHome ventanaHome = new FormHome();
                ventanaHome.FormClosed += (s, args) => { Application.Exit(); };
                ventanaHome.Show();
            }
            else
            {
                MessageBox.Show("Credenciales de identidad incorrectas. Acceso denegado.", "Error de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClave.Clear();
                txtClave.Focus();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnIngresar.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Eventos vacíos requeridos por el diseñador
        private void FormLogin_Load(object sender, EventArgs e) { }
        private void txtUsuario_TextChanged(object sender, EventArgs e) { }
        private void txtClave_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void btnAbrirAdmin_Click(object sender, EventArgs e) { }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}