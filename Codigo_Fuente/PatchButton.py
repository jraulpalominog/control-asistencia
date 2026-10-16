import re

with open('FormAdmin.Designer.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Add btnGestionarCronograma field declaration
content = re.sub(r'(private\s+System\.Windows\.Forms\.Button\s+btnGenerarReporteSemanas;)',
                 r'\1\n        private System.Windows.Forms.Button btnGestionarCronograma;', content)

# Add btnGestionarCronograma instantiation
content = re.sub(r'(this\.btnGenerarReporteSemanas\s*=\s*new\s+System\.Windows\.Forms\.Button\(\);)',
                 r'\1\n            this.btnGestionarCronograma = new System.Windows.Forms.Button();', content)

# Add btnGestionarCronograma to pnlFiltros controls
content = re.sub(r'(this\.pnlFiltros\.Controls\.Add\(this\.btnGenerarReporteSemanas\);)',
                 r'\1\n            this.pnlFiltros.Controls.Add(this.btnGestionarCronograma);', content)

# Add properties for btnGestionarCronograma right before btnGenerarReporteSemanas
btn_props = """            // 
            // btnGestionarCronograma
            // 
            this.btnGestionarCronograma.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGestionarCronograma.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionarCronograma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarCronograma.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGestionarCronograma.Location = new System.Drawing.Point(10, 15);
            this.btnGestionarCronograma.Name = "btnGestionarCronograma";
            this.btnGestionarCronograma.Size = new System.Drawing.Size(240, 35);
            this.btnGestionarCronograma.TabIndex = 0;
            this.btnGestionarCronograma.Text = "Gestionar Cronograma Semestre";
            this.btnGestionarCronograma.UseVisualStyleBackColor = false;
            this.btnGestionarCronograma.Click += new System.EventHandler(this.btnGestionarCronograma_Click);
"""

# Let's find "// btnGenerarReporteSemanas" and insert before it
content = content.replace("            // \n            // btnGenerarReporteSemanas", btn_props + "            // \n            // btnGenerarReporteSemanas")

with open('FormAdmin.Designer.cs', 'w', encoding='utf-8') as f:
    f.write(content)

print("Button successfully inserted into FormAdmin.Designer.cs")
