with open('FormAdmin.Designer.cs', 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace("            this.lblInicio = new System.Windows.Forms.Label();\n", "")
content = content.replace("            this.pnlFiltros.Controls.Add(this.lblInicio);\n", "")

with open('FormAdmin.Designer.cs', 'w', encoding='utf-8') as f:
    f.write(content)

print("Remaining lblInicio references removed.")
