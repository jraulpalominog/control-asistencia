using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Ventana de aviso que se cierra automáticamente después de unos segundos.
    /// </summary>
    public class FormAviso : Form
    {
        public enum TipoAviso
        {
            Informacion,   // Azul
            Exito,         // Verde
            Advertencia,   // Naranja
            Error          // Rojo
        }

        private Label lblIcono;
        private Label lblTitulo;
        private Label lblMensaje;
        private Label lblCuentaAtras;
        private Timer timerCierre;
        private int segundosRestantes;
        private Panel pnlBarra;

        public FormAviso(string titulo, string mensaje, TipoAviso tipo = TipoAviso.Informacion, int segundos = 5)
        {
            segundosRestantes = segundos;
            ConfigurarVentana(tipo);
            ConfigurarControles(titulo, mensaje, tipo, segundos);
            IniciarTimer();
        }

        private Color ObtenerColorPrincipal(TipoAviso tipo)
        {
            switch (tipo)
            {
                case TipoAviso.Exito:      return Color.FromArgb(39, 174, 96);
                case TipoAviso.Advertencia: return Color.FromArgb(230, 126, 34);
                case TipoAviso.Error:      return Color.FromArgb(192, 57, 43);
                default:                   return Color.FromArgb(41, 128, 185);
            }
        }

        private string ObtenerIcono(TipoAviso tipo)
        {
            switch (tipo)
            {
                case TipoAviso.Exito:      return "✔";
                case TipoAviso.Advertencia: return "⚠";
                case TipoAviso.Error:      return "✖";
                default:                   return "ℹ";
            }
        }

        private void ConfigurarVentana(TipoAviso tipo)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(520, 220);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.Opacity = 0;

            // Borde de color según tipo
            this.Paint += (s, e) =>
            {
                Color borderColor = ObtenerColorPrincipal(tipo);
                using (Pen pen = new Pen(borderColor, 4))
                    e.Graphics.DrawRectangle(pen, 2, 2, this.Width - 4, this.Height - 4);
            };

            // Click en cualquier parte cierra la ventana
            this.MouseClick += (s, e) => CerrarVentana();
        }

        private void ConfigurarControles(string titulo, string mensaje, TipoAviso tipo, int segundos)
        {
            Color colorPrincipal = ObtenerColorPrincipal(tipo);

            // Barra de progreso superior
            pnlBarra = new Panel();
            pnlBarra.Location = new Point(4, 4);
            pnlBarra.Size = new Size(this.Width - 8, 5);
            pnlBarra.BackColor = colorPrincipal;
            this.Controls.Add(pnlBarra);

            // Icono
            lblIcono = new Label();
            lblIcono.Text = ObtenerIcono(tipo);
            lblIcono.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            lblIcono.ForeColor = colorPrincipal;
            lblIcono.Size = new Size(70, 70);
            lblIcono.Location = new Point(20, 65);
            lblIcono.TextAlign = ContentAlignment.MiddleCenter;
            lblIcono.MouseClick += (s, e) => CerrarVentana();
            this.Controls.Add(lblIcono);

            // Título
            lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(100, 50);
            lblTitulo.Size = new Size(400, 35);
            lblTitulo.MouseClick += (s, e) => CerrarVentana();
            this.Controls.Add(lblTitulo);

            // Mensaje
            lblMensaje = new Label();
            lblMensaje.Text = mensaje;
            lblMensaje.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblMensaje.ForeColor = Color.FromArgb(200, 200, 200);
            lblMensaje.Location = new Point(100, 90);
            lblMensaje.Size = new Size(400, 60);
            lblMensaje.MouseClick += (s, e) => CerrarVentana();
            this.Controls.Add(lblMensaje);

            // Cuenta atrás
            lblCuentaAtras = new Label();
            lblCuentaAtras.Text = $"Se cierra en {segundos} segundos... (clic para cerrar)";
            lblCuentaAtras.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblCuentaAtras.ForeColor = Color.FromArgb(120, 120, 120);
            lblCuentaAtras.Location = new Point(100, 160);
            lblCuentaAtras.Size = new Size(400, 25);
            lblCuentaAtras.MouseClick += (s, e) => CerrarVentana();
            this.Controls.Add(lblCuentaAtras);
        }

        private void IniciarTimer()
        {
            // Animación de entrada (fade in)
            Timer timerFade = new Timer();
            timerFade.Interval = 20;
            timerFade.Tick += (s, e) =>
            {
                if (this.Opacity < 1.0)
                    this.Opacity += 0.1;
                else
                {
                    this.Opacity = 1.0;
                    timerFade.Stop();
                }
            };
            timerFade.Start();

            // Timer de cierre
            timerCierre = new Timer();
            timerCierre.Interval = 1000;
            timerCierre.Tick += (s, e) =>
            {
                segundosRestantes--;

                // Reducir barra de progreso
                int anchoMax = this.Width - 8;
                int anchoActual = (int)(anchoMax * segundosRestantes / (float)(segundosRestantes + 1));
                if (pnlBarra != null && !pnlBarra.IsDisposed)
                    pnlBarra.Width = Math.Max(0, anchoActual);

                if (segundosRestantes > 0)
                {
                    if (lblCuentaAtras != null && !lblCuentaAtras.IsDisposed)
                        lblCuentaAtras.Text = $"Se cierra en {segundosRestantes} segundo{(segundosRestantes == 1 ? "" : "s")}... (clic para cerrar)";
                }
                else
                {
                    timerCierre.Stop();
                    CerrarVentana();
                }
            };
            timerCierre.Start();
        }

        private void CerrarVentana()
        {
            if (timerCierre != null) timerCierre.Stop();
            // Fade out
            Timer timerFadeOut = new Timer();
            timerFadeOut.Interval = 30;
            timerFadeOut.Tick += (s, e) =>
            {
                if (this.Opacity > 0)
                    this.Opacity -= 0.15;
                else
                {
                    timerFadeOut.Stop();
                    if (!this.IsDisposed) this.Close();
                }
            };
            timerFadeOut.Start();
        }

        /// <summary>
        /// Muestra un aviso que se cierra automáticamente. No bloquea el hilo.
        /// </summary>
        public static void Mostrar(string titulo, string mensaje, TipoAviso tipo = TipoAviso.Informacion, int segundos = 5)
        {
            FormAviso aviso = new FormAviso(titulo, mensaje, tipo, segundos);
            aviso.Show();
        }
    }
}
