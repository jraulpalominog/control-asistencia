import re

with open('FormAdmin.Designer.cs', 'r', encoding='utf-8') as f:
    content = f.read()

broken_str = """            // btnConfigLimites
            // 
            this.btnConfigLimites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnConfigLimites.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigLimites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigLimites.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnConfigLimites.ForeColor = System.Drawing.Color.White;
            this.btnConfigLimites.Location = new System.Drawing.Point(29, 415);
            this.btnConfigLimites.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfigLimites.Name = "btnConfigLimites";
            this.monthCalendarHistorial.Name = "monthCalendarHistorial";
            this.monthCalendarHistorial.TabIndex = 3;"""

fixed_str = """            // btnConfigLimites
            // 
            this.btnConfigLimites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnConfigLimites.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigLimites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigLimites.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnConfigLimites.ForeColor = System.Drawing.Color.White;
            this.btnConfigLimites.Location = new System.Drawing.Point(29, 415);
            this.btnConfigLimites.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfigLimites.Name = "btnConfigLimites";
            this.btnConfigLimites.Size = new System.Drawing.Size(200, 37);
            this.btnConfigLimites.TabIndex = 28;
            this.btnConfigLimites.Text = "Límites de Tiempo";
            this.btnConfigLimites.UseVisualStyleBackColor = false;
            this.btnConfigLimites.Click += new System.EventHandler(this.btnConfigLimites_Click);
            // 
            // lblBuscarHistorial
            // 
            this.lblBuscarHistorial.AutoSize = true;
            this.lblBuscarHistorial.Location = new System.Drawing.Point(260, 31);
            this.lblBuscarHistorial.Name = "lblBuscarHistorial";
            this.lblBuscarHistorial.Size = new System.Drawing.Size(91, 17);
            this.lblBuscarHistorial.TabIndex = 106;
            this.lblBuscarHistorial.Text = "Buscar por:";
            // 
            // cmbCriterioHistorial
            // 
            this.cmbCriterioHistorial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioHistorial.FormattingEnabled = true;
            this.cmbCriterioHistorial.Items.AddRange(new object[] {
            "Todos",
            "Docente",
            "Código",
            "Curso"});
            this.cmbCriterioHistorial.Location = new System.Drawing.Point(350, 28);
            this.cmbCriterioHistorial.Name = "cmbCriterioHistorial";
            this.cmbCriterioHistorial.Size = new System.Drawing.Size(120, 24);
            this.cmbCriterioHistorial.TabIndex = 107;
            // 
            // txtBuscarHistorialNombre
            // 
            this.txtBuscarHistorialNombre.Location = new System.Drawing.Point(480, 28);
            this.txtBuscarHistorialNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscarHistorialNombre.Name = "txtBuscarHistorialNombre";
            this.txtBuscarHistorialNombre.Size = new System.Drawing.Size(276, 22);
            this.txtBuscarHistorialNombre.TabIndex = 4;
            this.txtBuscarHistorialNombre.TextChanged += new System.EventHandler(this.txtBuscarHistorialNombre_TextChanged);
            // 
            // monthCalendarHistorial
            // 
            this.monthCalendarHistorial.Location = new System.Drawing.Point(26, 28);
            this.monthCalendarHistorial.MaxSelectionCount = 1;
            this.monthCalendarHistorial.Name = "monthCalendarHistorial";
            this.monthCalendarHistorial.TabIndex = 3;"""

content = content.replace(broken_str, fixed_str)
with open('FormAdmin.Designer.cs', 'w', encoding='utf-8') as f:
    f.write(content)
