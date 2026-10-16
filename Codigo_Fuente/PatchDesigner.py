import re

with open('FormAdmin.Designer.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Remove field declarations
content = re.sub(r'\s*private\s+System\.Windows\.Forms\.Label\s+lblInicioSemestre;', '', content)
content = re.sub(r'\s*private\s+System\.Windows\.Forms\.DateTimePicker\s+dtpInicioSemestre;', '', content)

# Remove instantiations
content = re.sub(r'\s*this\.lblInicioSemestre\s*=\s*new\s+System\.Windows\.Forms\.Label\(\);', '', content)
content = re.sub(r'\s*this\.dtpInicioSemestre\s*=\s*new\s+System\.Windows\.Forms\.DateTimePicker\(\);', '', content)

# Remove Controls.Add
content = re.sub(r'\s*this\.pnlFiltros\.Controls\.Add\(this\.lblInicioSemestre\);', '', content)
content = re.sub(r'\s*this\.pnlFiltros\.Controls\.Add\(this\.dtpInicioSemestre\);', '', content)

# Add btnGestionarCronograma field declaration
content = re.sub(r'(private\s+System\.Windows\.Forms\.Button\s+btnAnalizarReporte;)',
                 r'\1\n        private System.Windows.Forms.Button btnGestionarCronograma;', content)

# Add btnGestionarCronograma instantiation
content = re.sub(r'(this\.btnAnalizarReporte\s*=\s*new\s+System\.Windows\.Forms\.Button\(\);)',
                 r'\1\n            this.btnGestionarCronograma = new System.Windows.Forms.Button();', content)

# Add btnGestionarCronograma to pnlFiltros controls
content = re.sub(r'(this\.pnlFiltros\.Controls\.Add\(this\.btnAnalizarReporte\);)',
                 r'\1\n            this.pnlFiltros.Controls.Add(this.btnGestionarCronograma);', content)

# Replace the properties block of lblInicioSemestre and dtpInicioSemestre with btnGestionarCronograma
# Since regex might be tricky, I'll search for the block and replace it using string methods.

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
            this.btnGestionarCronograma.Click += new System.EventHandler(this.btnGestionarCronograma_Click);"""

# The block to remove is from "// lblInicioSemestre" to the end of "dtpInicioSemestre" properties
pattern = r'\s*// \s*// lblInicioSemestre.*?this\.dtpInicioSemestre\.TabIndex = 1;'

content = re.sub(pattern, '\n' + btn_props, content, flags=re.DOTALL)

with open('FormAdmin.Designer.cs', 'w', encoding='utf-8') as f:
    f.write(content)

print("Patch Designer applied")
