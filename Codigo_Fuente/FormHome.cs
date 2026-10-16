using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosed += FormHome_FormClosed_Custom;
            this.Load += FormHome_Load_Custom;
        }

        private void FormHome_FormClosed_Custom(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FormHome_Load_Custom(object sender, EventArgs e)
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
                if (c.Name == "btnSalirlogin")
                {
                    c.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                    c.BringToFront();

                    ToolTip tt = new ToolTip() { InitialDelay = 0 };
                    tt.SetToolTip(c, "Cerrar sesión y volver al inicio");

                    c.Click -= BtnSalirlogin_Click;
                    c.Click += BtnSalirlogin_Click;

                    continue;
                }
                panelCentro.Controls.Add(c);
            }

            this.Controls.Add(panelCentro);

            this.Resize += (s, e) => {
                panelCentro.Left = (this.ClientSize.Width - panelCentro.Width) / 2;
                panelCentro.Top = (this.ClientSize.Height - panelCentro.Height) / 2;
            };
        }

        private void BtnSalirlogin_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnIniciarSistema_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 pantallaReloj = new Form1();
            pantallaReloj.FormClosed += (senderArgs, args) => { this.Show(); };
            pantallaReloj.Show();
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAdmin panelAdministrativo = new FormAdmin();
            panelAdministrativo.FormClosed += (senderArgs, args) => { this.Show(); };
            panelAdministrativo.Show();
        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El módulo de Ajustes estará disponible en la próxima actualización.", "En construcción", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        // Eventos vacíos requeridos por el diseñador
        private void FormHome_Load(object sender, EventArgs e) { }
        private void guna2ImageButton1_Click(object sender, EventArgs e) { }
        private void btnSistema_Click(object sender, EventArgs e) { }
        private void guna2ImageButton3_Click(object sender, EventArgs e) { }
        private void guna2ImageButton4_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void btnAbrirAdmin_Click(object sender, EventArgs e) { }
    }
}