namespace WindowsFormsApp1
{
    partial class FormSeleccionReportePDF
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
            this.lblInstruccion = new System.Windows.Forms.Label();
            this.btnGeneral = new System.Windows.Forms.Button();
            this.btnIndividual = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInstruccion
            // 
            this.lblInstruccion.AutoSize = false;
            this.lblInstruccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblInstruccion.Location = new System.Drawing.Point(20, 20);
            this.lblInstruccion.Name = "lblInstruccion";
            this.lblInstruccion.Size = new System.Drawing.Size(380, 30);
            this.lblInstruccion.TabIndex = 0;
            this.lblInstruccion.Text = "Seleccione el tipo de reporte a exportar:";
            // 
            // btnGeneral
            // 
            this.btnGeneral.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGeneral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnGeneral.Location = new System.Drawing.Point(20, 60);
            this.btnGeneral.Name = "btnGeneral";
            this.btnGeneral.Size = new System.Drawing.Size(370, 45);
            this.btnGeneral.TabIndex = 1;
            this.btnGeneral.Text = "Reporte General (Todos los Docentes)";
            this.btnGeneral.UseVisualStyleBackColor = false;
            this.btnGeneral.Click += new System.EventHandler(this.btnGeneral_Click);
            // 
            // btnIndividual
            // 
            this.btnIndividual.BackColor = System.Drawing.Color.LightGreen;
            this.btnIndividual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIndividual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndividual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnIndividual.Location = new System.Drawing.Point(20, 115);
            this.btnIndividual.Name = "btnIndividual";
            this.btnIndividual.Size = new System.Drawing.Size(370, 45);
            this.btnIndividual.TabIndex = 2;
            this.btnIndividual.Text = "Reporte Individual (Docente Específico)";
            this.btnIndividual.UseVisualStyleBackColor = false;
            this.btnIndividual.Click += new System.EventHandler(this.btnIndividual_Click);
            // 
            // FormSeleccionReportePDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 180);
            this.Controls.Add(this.lblInstruccion);
            this.Controls.Add(this.btnGeneral);
            this.Controls.Add(this.btnIndividual);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSeleccionReportePDF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selección de Reporte PDF";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblInstruccion;
        private System.Windows.Forms.Button btnGeneral;
        private System.Windows.Forms.Button btnIndividual;
    }
}
