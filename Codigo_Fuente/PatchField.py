with open('FormAdmin.Designer.cs', 'r', encoding='utf-8') as f:
    content = f.read()

target = "private Button btnGenerarReporteSemanas;"
injection = "private Button btnGenerarReporteSemanas;\n        private System.Windows.Forms.Button btnGestionarCronograma;"

content = content.replace(target, injection)

with open('FormAdmin.Designer.cs', 'w', encoding='utf-8') as f:
    f.write(content)

print("Field declaration added.")
