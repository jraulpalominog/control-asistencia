namespace WindowsFormsApp1
{
    partial class FormExportarReporteGeneral
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
            this.lblTituloFechas = new System.Windows.Forms.Label();
            this.rbDia = new System.Windows.Forms.RadioButton();
            this.dtpDia = new System.Windows.Forms.DateTimePicker();
            this.rbSemana = new System.Windows.Forms.RadioButton();
            this.dtpSemana = new System.Windows.Forms.DateTimePicker();
            this.lblRangoSemana = new System.Windows.Forms.Label();
            this.rbMes = new System.Windows.Forms.RadioButton();
            this.dtpMes = new System.Windows.Forms.DateTimePicker();
            this.lblTituloOpciones = new System.Windows.Forms.Label();
            this.pnlOpciones = new System.Windows.Forms.Panel();
            this.rbSoloAsistencias = new System.Windows.Forms.RadioButton();
            this.rbIncluirFaltas = new System.Windows.Forms.RadioButton();
            this.lblTituloFormato = new System.Windows.Forms.Label();
            this.cmbFormato = new System.Windows.Forms.ComboBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTituloFechas
            // 
            this.lblTituloFechas.AutoSize = false;
            this.lblTituloFechas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTituloFechas.Location = new System.Drawing.Point(20, 15);
            this.lblTituloFechas.Name = "lblTituloFechas";
            this.lblTituloFechas.Size = new System.Drawing.Size(400, 25);
            this.lblTituloFechas.TabIndex = 0;
            this.lblTituloFechas.Text = "1. Seleccione el rango de fechas:";
            // 
            // rbDia
            // 
            this.rbDia.Checked = true;
            this.rbDia.Location = new System.Drawing.Point(25, 50);
            this.rbDia.Name = "rbDia";
            this.rbDia.Size = new System.Drawing.Size(200, 22);
            this.rbDia.TabIndex = 1;
            this.rbDia.TabStop = true;
            this.rbDia.Text = "Asistencia del día:";
            this.rbDia.CheckedChanged += new System.EventHandler(this.rbDia_CheckedChanged);
            // 
            // dtpDia
            // 
            this.dtpDia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDia.Location = new System.Drawing.Point(230, 48);
            this.dtpDia.Name = "dtpDia";
            this.dtpDia.Size = new System.Drawing.Size(180, 22);
            this.dtpDia.TabIndex = 2;
            // 
            // rbSemana
            // 
            this.rbSemana.Location = new System.Drawing.Point(25, 95);
            this.rbSemana.Name = "rbSemana";
            this.rbSemana.Size = new System.Drawing.Size(200, 22);
            this.rbSemana.TabIndex = 3;
            this.rbSemana.Text = "Asistencia de la semana:";
            this.rbSemana.CheckedChanged += new System.EventHandler(this.rbSemana_CheckedChanged);
            // 
            // dtpSemana
            // 
            this.dtpSemana.Enabled = false;
            this.dtpSemana.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSemana.Location = new System.Drawing.Point(230, 93);
            this.dtpSemana.Name = "dtpSemana";
            this.dtpSemana.Size = new System.Drawing.Size(180, 22);
            this.dtpSemana.TabIndex = 4;
            this.dtpSemana.ValueChanged += new System.EventHandler(this.dtpSemana_ValueChanged);
            // 
            // lblRangoSemana
            // 
            this.lblRangoSemana.AutoSize = false;
            this.lblRangoSemana.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblRangoSemana.ForeColor = System.Drawing.Color.DarkGray;
            this.lblRangoSemana.Location = new System.Drawing.Point(45, 120);
            this.lblRangoSemana.Name = "lblRangoSemana";
            this.lblRangoSemana.Size = new System.Drawing.Size(380, 20);
            this.lblRangoSemana.TabIndex = 5;
            this.lblRangoSemana.Text = "Rango: Lunes ...";
            // 
            // rbMes
            // 
            this.rbMes.Location = new System.Drawing.Point(25, 155);
            this.rbMes.Name = "rbMes";
            this.rbMes.Size = new System.Drawing.Size(200, 22);
            this.rbMes.TabIndex = 6;
            this.rbMes.Text = "Asistencia del mes:";
            this.rbMes.CheckedChanged += new System.EventHandler(this.rbMes_CheckedChanged);
            // 
            // dtpMes
            // 
            this.dtpMes.CustomFormat = "MMMM yyyy";
            this.dtpMes.Enabled = false;
            this.dtpMes.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMes.Location = new System.Drawing.Point(230, 153);
            this.dtpMes.Name = "dtpMes";
            this.dtpMes.ShowUpDown = true;
            this.dtpMes.Size = new System.Drawing.Size(180, 22);
            this.dtpMes.TabIndex = 7;
            // 
            // lblTituloOpciones
            // 
            this.lblTituloOpciones.AutoSize = false;
            this.lblTituloOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTituloOpciones.Location = new System.Drawing.Point(20, 200);
            this.lblTituloOpciones.Name = "lblTituloOpciones";
            this.lblTituloOpciones.Size = new System.Drawing.Size(400, 25);
            this.lblTituloOpciones.TabIndex = 8;
            this.lblTituloOpciones.Text = "2. Opciones de inasistencia:";
            // 
            // pnlOpciones
            // 
            this.pnlOpciones.Controls.Add(this.rbSoloAsistencias);
            this.pnlOpciones.Controls.Add(this.rbIncluirFaltas);
            this.pnlOpciones.Location = new System.Drawing.Point(20, 230);
            this.pnlOpciones.Name = "pnlOpciones";
            this.pnlOpciones.Size = new System.Drawing.Size(400, 55);
            this.pnlOpciones.TabIndex = 9;
            // 
            // rbSoloAsistencias
            // 
            this.rbSoloAsistencias.Checked = true;
            this.rbSoloAsistencias.Location = new System.Drawing.Point(5, 0);
            this.rbSoloAsistencias.Name = "rbSoloAsistencias";
            this.rbSoloAsistencias.Size = new System.Drawing.Size(300, 22);
            this.rbSoloAsistencias.TabIndex = 0;
            this.rbSoloAsistencias.TabStop = true;
            this.rbSoloAsistencias.Text = "Solo docentes que asistieron";
            // 
            // rbIncluirFaltas
            // 
            this.rbIncluirFaltas.Location = new System.Drawing.Point(5, 25);
            this.rbIncluirFaltas.Name = "rbIncluirFaltas";
            this.rbIncluirFaltas.Size = new System.Drawing.Size(300, 22);
            this.rbIncluirFaltas.TabIndex = 1;
            this.rbIncluirFaltas.Text = "Incluir faltas (marcar inasistencias en rojo)";
            // 
            // lblTituloFormato
            // 
            this.lblTituloFormato.AutoSize = false;
            this.lblTituloFormato.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTituloFormato.Location = new System.Drawing.Point(20, 295);
            this.lblTituloFormato.Name = "lblTituloFormato";
            this.lblTituloFormato.Size = new System.Drawing.Size(400, 25);
            this.lblTituloFormato.TabIndex = 10;
            this.lblTituloFormato.Text = "3. Formato de exportación:";
            // 
            // cmbFormato
            // 
            this.cmbFormato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormato.Items.AddRange(new object[] {
            "PDF Document (*.pdf)",
            "Excel CSV (*.csv)"});
            this.cmbFormato.Location = new System.Drawing.Point(25, 325);
            this.cmbFormato.Name = "cmbFormato";
            this.cmbFormato.Size = new System.Drawing.Size(180, 24);
            this.cmbFormato.TabIndex = 11;
            this.cmbFormato.SelectedIndex = 0;
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Location = new System.Drawing.Point(120, 370);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(100, 35);
            this.btnExportar.TabIndex = 12;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(240, 370);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormExportarReporteGeneral
            // 
            this.AcceptButton = this.btnExportar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(452, 425);
            this.Controls.Add(this.lblTituloFechas);
            this.Controls.Add(this.rbDia);
            this.Controls.Add(this.dtpDia);
            this.Controls.Add(this.rbSemana);
            this.Controls.Add(this.dtpSemana);
            this.Controls.Add(this.lblRangoSemana);
            this.Controls.Add(this.rbMes);
            this.Controls.Add(this.dtpMes);
            this.Controls.Add(this.lblTituloOpciones);
            this.Controls.Add(this.pnlOpciones);
            this.Controls.Add(this.lblTituloFormato);
            this.Controls.Add(this.cmbFormato);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExportarReporteGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exportar Reporte General";
            this.pnlOpciones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTituloFechas;
        private System.Windows.Forms.RadioButton rbDia;
        private System.Windows.Forms.DateTimePicker dtpDia;
        private System.Windows.Forms.RadioButton rbSemana;
        private System.Windows.Forms.DateTimePicker dtpSemana;
        private System.Windows.Forms.Label lblRangoSemana;
        private System.Windows.Forms.RadioButton rbMes;
        private System.Windows.Forms.DateTimePicker dtpMes;
        private System.Windows.Forms.Label lblTituloOpciones;
        private System.Windows.Forms.Panel pnlOpciones;
        private System.Windows.Forms.RadioButton rbSoloAsistencias;
        private System.Windows.Forms.RadioButton rbIncluirFaltas;
        private System.Windows.Forms.Label lblTituloFormato;
        private System.Windows.Forms.ComboBox cmbFormato;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
