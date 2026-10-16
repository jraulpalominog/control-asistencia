namespace WindowsFormsApp1
{
    partial class FormConfigLimites
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelSeparador = new System.Windows.Forms.Panel();
            // --- Grupo 1: Anticipación ---
            this.lblAnticipacion = new System.Windows.Forms.Label();
            this.lblDescAnticipacion = new System.Windows.Forms.Label();
            this.nudAntesClase = new System.Windows.Forms.NumericUpDown();
            this.lblMinAntes = new System.Windows.Forms.Label();
            // --- Grupo 2: Tolerancia tardanza ---
            this.lblToleranciaTardanza = new System.Windows.Forms.Label();
            this.lblDescToleranciaTardanza = new System.Windows.Forms.Label();
            this.nudToleranciaTardanza = new System.Windows.Forms.NumericUpDown();
            this.lblMinTolerancia = new System.Windows.Forms.Label();
            // --- Grupo 3: Falta ---
            this.lblFalta = new System.Windows.Forms.Label();
            this.lblDescFalta = new System.Windows.Forms.Label();
            this.nudFalta = new System.Windows.Forms.NumericUpDown();
            this.lblMinFalta = new System.Windows.Forms.Label();
            // --- Botones ---
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.nudAntesClase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToleranciaTardanza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFalta)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.lblTitulo.Location = new System.Drawing.Point(20, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(500, 45);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "⚙  Configurar Límites de Tiempo";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelSeparador
            // 
            this.panelSeparador.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.panelSeparador.Location = new System.Drawing.Point(20, 65);
            this.panelSeparador.Name = "panelSeparador";
            this.panelSeparador.Size = new System.Drawing.Size(500, 2);
            this.panelSeparador.TabIndex = 1;

            // ============ GRUPO 1: ANTICIPACIÓN ============
            // 
            // lblAnticipacion
            // 
            this.lblAnticipacion.AutoSize = false;
            this.lblAnticipacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAnticipacion.ForeColor = System.Drawing.Color.White;
            this.lblAnticipacion.Location = new System.Drawing.Point(30, 80);
            this.lblAnticipacion.Name = "lblAnticipacion";
            this.lblAnticipacion.Size = new System.Drawing.Size(460, 25);
            this.lblAnticipacion.TabIndex = 2;
            this.lblAnticipacion.Text = "⏰  Minutos de anticipación permitida:";
            // 
            // lblDescAnticipacion
            // 
            this.lblDescAnticipacion.AutoSize = false;
            this.lblDescAnticipacion.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblDescAnticipacion.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.lblDescAnticipacion.Location = new System.Drawing.Point(30, 106);
            this.lblDescAnticipacion.Name = "lblDescAnticipacion";
            this.lblDescAnticipacion.Size = new System.Drawing.Size(380, 20);
            this.lblDescAnticipacion.TabIndex = 3;
            this.lblDescAnticipacion.Text = "Cuántos minutos ANTES del inicio de clase puede registrar asistencia.";
            // 
            // nudAntesClase
            // 
            this.nudAntesClase.BackColor = System.Drawing.Color.FromArgb(50, 65, 72);
            this.nudAntesClase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudAntesClase.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.nudAntesClase.ForeColor = System.Drawing.Color.White;
            this.nudAntesClase.Location = new System.Drawing.Point(430, 98);
            this.nudAntesClase.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            this.nudAntesClase.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudAntesClase.Name = "nudAntesClase";
            this.nudAntesClase.Size = new System.Drawing.Size(75, 30);
            this.nudAntesClase.TabIndex = 4;
            this.nudAntesClase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudAntesClase.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // lblMinAntes
            // 
            this.lblMinAntes.AutoSize = true;
            this.lblMinAntes.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.lblMinAntes.Location = new System.Drawing.Point(507, 105);
            this.lblMinAntes.Name = "lblMinAntes";
            this.lblMinAntes.Size = new System.Drawing.Size(25, 20);
            this.lblMinAntes.TabIndex = 5;
            this.lblMinAntes.Text = "min";

            // ============ GRUPO 2: TOLERANCIA TARDANZA ============
            // 
            // lblToleranciaTardanza
            // 
            this.lblToleranciaTardanza.AutoSize = false;
            this.lblToleranciaTardanza.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblToleranciaTardanza.ForeColor = System.Drawing.Color.White;
            this.lblToleranciaTardanza.Location = new System.Drawing.Point(30, 180);
            this.lblToleranciaTardanza.Name = "lblToleranciaTardanza";
            this.lblToleranciaTardanza.Size = new System.Drawing.Size(460, 25);
            this.lblToleranciaTardanza.TabIndex = 6;
            this.lblToleranciaTardanza.Text = "⚠  Minutos de tolerancia (tardanza):";
            // 
            // lblDescToleranciaTardanza
            // 
            this.lblDescToleranciaTardanza.AutoSize = false;
            this.lblDescToleranciaTardanza.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblDescToleranciaTardanza.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.lblDescToleranciaTardanza.Location = new System.Drawing.Point(30, 206);
            this.lblDescToleranciaTardanza.Name = "lblDescToleranciaTardanza";
            this.lblDescToleranciaTardanza.Size = new System.Drawing.Size(380, 20);
            this.lblDescToleranciaTardanza.TabIndex = 7;
            this.lblDescToleranciaTardanza.Text = "Minutos DESPUÉS del inicio antes de que cuente como tardanza.";
            // 
            // nudToleranciaTardanza
            // 
            this.nudToleranciaTardanza.BackColor = System.Drawing.Color.FromArgb(50, 65, 72);
            this.nudToleranciaTardanza.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudToleranciaTardanza.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.nudToleranciaTardanza.ForeColor = System.Drawing.Color.White;
            this.nudToleranciaTardanza.Location = new System.Drawing.Point(430, 198);
            this.nudToleranciaTardanza.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            this.nudToleranciaTardanza.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudToleranciaTardanza.Name = "nudToleranciaTardanza";
            this.nudToleranciaTardanza.Size = new System.Drawing.Size(75, 30);
            this.nudToleranciaTardanza.TabIndex = 8;
            this.nudToleranciaTardanza.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudToleranciaTardanza.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // lblMinTolerancia
            // 
            this.lblMinTolerancia.AutoSize = true;
            this.lblMinTolerancia.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.lblMinTolerancia.Location = new System.Drawing.Point(507, 205);
            this.lblMinTolerancia.Name = "lblMinTolerancia";
            this.lblMinTolerancia.Size = new System.Drawing.Size(25, 20);
            this.lblMinTolerancia.TabIndex = 9;
            this.lblMinTolerancia.Text = "min";

            // ============ GRUPO 3: FALTA ============
            // 
            // lblFalta
            // 
            this.lblFalta.AutoSize = false;
            this.lblFalta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFalta.ForeColor = System.Drawing.Color.White;
            this.lblFalta.Location = new System.Drawing.Point(30, 280);
            this.lblFalta.Name = "lblFalta";
            this.lblFalta.Size = new System.Drawing.Size(460, 25);
            this.lblFalta.TabIndex = 10;
            this.lblFalta.Text = "✖  Minutos para registrar como falta:";
            // 
            // lblDescFalta
            // 
            this.lblDescFalta.AutoSize = false;
            this.lblDescFalta.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblDescFalta.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.lblDescFalta.Location = new System.Drawing.Point(30, 306);
            this.lblDescFalta.Name = "lblDescFalta";
            this.lblDescFalta.Size = new System.Drawing.Size(380, 20);
            this.lblDescFalta.TabIndex = 11;
            this.lblDescFalta.Text = "Minutos DESPUÉS del inicio en que se considera falta (sin tolerancia).";
            // 
            // nudFalta
            // 
            this.nudFalta.BackColor = System.Drawing.Color.FromArgb(50, 65, 72);
            this.nudFalta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudFalta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.nudFalta.ForeColor = System.Drawing.Color.White;
            this.nudFalta.Location = new System.Drawing.Point(430, 298);
            this.nudFalta.Maximum = new decimal(new int[] { 240, 0, 0, 0 });
            this.nudFalta.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudFalta.Name = "nudFalta";
            this.nudFalta.Size = new System.Drawing.Size(75, 30);
            this.nudFalta.TabIndex = 12;
            this.nudFalta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudFalta.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // lblMinFalta
            // 
            this.lblMinFalta.AutoSize = true;
            this.lblMinFalta.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.lblMinFalta.Location = new System.Drawing.Point(507, 305);
            this.lblMinFalta.Name = "lblMinFalta";
            this.lblMinFalta.Size = new System.Drawing.Size(25, 20);
            this.lblMinFalta.TabIndex = 13;
            this.lblMinFalta.Text = "min";

            // ============ BOTONES ============
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(200, 365);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(140, 42);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "💾  Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(355, 365);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 42);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);

            // 
            // FormConfigLimites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(34, 45, 50);
            this.ClientSize = new System.Drawing.Size(540, 425);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.panelSeparador);
            this.Controls.Add(this.lblAnticipacion);
            this.Controls.Add(this.lblDescAnticipacion);
            this.Controls.Add(this.nudAntesClase);
            this.Controls.Add(this.lblMinAntes);
            this.Controls.Add(this.lblToleranciaTardanza);
            this.Controls.Add(this.lblDescToleranciaTardanza);
            this.Controls.Add(this.nudToleranciaTardanza);
            this.Controls.Add(this.lblMinTolerancia);
            this.Controls.Add(this.lblFalta);
            this.Controls.Add(this.lblDescFalta);
            this.Controls.Add(this.nudFalta);
            this.Controls.Add(this.lblMinFalta);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigLimites";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurar Límites de Tiempo";
            ((System.ComponentModel.ISupportInitialize)(this.nudAntesClase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToleranciaTardanza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFalta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelSeparador;
        private System.Windows.Forms.Label lblAnticipacion;
        private System.Windows.Forms.Label lblDescAnticipacion;
        private System.Windows.Forms.NumericUpDown nudAntesClase;
        private System.Windows.Forms.Label lblMinAntes;
        private System.Windows.Forms.Label lblToleranciaTardanza;
        private System.Windows.Forms.Label lblDescToleranciaTardanza;
        private System.Windows.Forms.NumericUpDown nudToleranciaTardanza;
        private System.Windows.Forms.Label lblMinTolerancia;
        private System.Windows.Forms.Label lblFalta;
        private System.Windows.Forms.Label lblDescFalta;
        private System.Windows.Forms.NumericUpDown nudFalta;
        private System.Windows.Forms.Label lblMinFalta;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
