namespace WindowsFormsApp1
{
    partial class FormRegistroAutomatico
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.lblNombreDocente = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lstCursosHoy = new System.Windows.Forms.ListBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lblInstrucciones = new System.Windows.Forms.Label();
            this.lblInactividad = new System.Windows.Forms.Label();
            this.btnVerRecord = new System.Windows.Forms.Button();
            this.timerInactividad = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(55)))), ((int)(((byte)(64)))));
            this.pnlCenter.Controls.Add(this.label1);
            this.pnlCenter.Controls.Add(this.lblNombreDocente);
            this.pnlCenter.Controls.Add(this.btnCancelar);
            this.pnlCenter.Controls.Add(this.lstCursosHoy);
            this.pnlCenter.Controls.Add(this.btnRegistrar);
            this.pnlCenter.Controls.Add(this.lblInstrucciones);
            this.pnlCenter.Controls.Add(this.lblInactividad);
            this.pnlCenter.Controls.Add(this.btnVerRecord);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(80, 40, 80, 40);
            this.pnlCenter.TabIndex = 0;
            // 
            // label1 - Titulo "Cursos de hoy"
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(600, 100);
            this.label1.TabIndex = 13;
            this.label1.Text = "Cursos de hoy";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNombreDocente
            // 
            this.lblNombreDocente.BackColor = System.Drawing.Color.Transparent;
            this.lblNombreDocente.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNombreDocente.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblNombreDocente.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblNombreDocente.Name = "lblNombreDocente";
            this.lblNombreDocente.Size = new System.Drawing.Size(600, 70);
            this.lblNombreDocente.TabIndex = 12;
            this.lblNombreDocente.Text = "[Nombre Docente]";
            this.lblNombreDocente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstCursosHoy
            // 
            this.lstCursosHoy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lstCursosHoy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCursosHoy.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lstCursosHoy.FormattingEnabled = true;
            this.lstCursosHoy.ItemHeight = 41;
            this.lstCursosHoy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lstCursosHoy.Location = new System.Drawing.Point(150, 230);
            this.lstCursosHoy.Name = "lstCursosHoy";
            this.lstCursosHoy.Size = new System.Drawing.Size(900, 250);
            this.lstCursosHoy.TabIndex = 7;
            this.lstCursosHoy.SelectedIndexChanged += new System.EventHandler(this.lstCursosHoy_SelectedIndexChanged);
            this.lstCursosHoy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCursosHoy_KeyDown);
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Italic);
            this.lblInstrucciones.ForeColor = System.Drawing.Color.LightGray;
            this.lblInstrucciones.Location = new System.Drawing.Point(150, 490);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.AutoSize = true;
            this.lblInstrucciones.TabIndex = 8;
            this.lblInstrucciones.Text = "Seleccione un curso y presione Registrar";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.Location = new System.Drawing.Point(150, 540);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(280, 70);
            this.btnRegistrar.TabIndex = 9;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(460, 540);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(280, 70);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnVerRecord
            // 
            this.btnVerRecord.BackColor = System.Drawing.Color.SteelBlue;
            this.btnVerRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerRecord.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnVerRecord.ForeColor = System.Drawing.Color.White;
            this.btnVerRecord.Location = new System.Drawing.Point(150, 630);
            this.btnVerRecord.Name = "btnVerRecord";
            this.btnVerRecord.Size = new System.Drawing.Size(350, 60);
            this.btnVerRecord.TabIndex = 11;
            this.btnVerRecord.Text = "Ver mi R\u00e9cord";
            this.btnVerRecord.UseVisualStyleBackColor = false;
            this.btnVerRecord.Click += new System.EventHandler(this.btnVerRecord_Click);
            // 
            // lblInactividad
            // 
            this.lblInactividad.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblInactividad.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblInactividad.Location = new System.Drawing.Point(150, 710);
            this.lblInactividad.Name = "lblInactividad";
            this.lblInactividad.AutoSize = true;
            this.lblInactividad.TabIndex = 3;
            this.lblInactividad.Text = "Tiempo restante: 20 seg";
            // 
            // timerInactividad
            // 
            this.timerInactividad.Interval = 1000;
            this.timerInactividad.Tick += new System.EventHandler(this.timerInactividad_Tick);
            // 
            // FormRegistroAutomatico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1737, 900);
            this.Controls.Add(this.pnlCenter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRegistroAutomatico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registro Autom\u00e1tico";
            this.Load += new System.EventHandler(this.FormRegistroAutomatico_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormRegistroAutomatico_Paint);
            this.Resize += new System.EventHandler(this.FormRegistroAutomatico_Resize);
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Label lblNombreDocente;
        private System.Windows.Forms.Label lblInactividad;
        private System.Windows.Forms.Timer timerInactividad;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ListBox lstCursosHoy;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblInstrucciones;
        private System.Windows.Forms.Button btnVerRecord;
        private System.Windows.Forms.Label label1;
    }
}
