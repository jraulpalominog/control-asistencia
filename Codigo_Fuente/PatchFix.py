import re

# Fix namespace in FormCronograma.cs
with open('FormCronograma.cs', 'r', encoding='utf-8') as f:
    content = f.read()
content = content.replace("namespace Proyecto_Asistencia_FCM", "namespace WindowsFormsApp1")
with open('FormCronograma.cs', 'w', encoding='utf-8') as f:
    f.write(content)

# Fix namespace in FormCronograma.Designer.cs
with open('FormCronograma.Designer.cs', 'r', encoding='utf-8') as f:
    content = f.read()
content = content.replace("namespace Proyecto_Asistencia_FCM", "namespace WindowsFormsApp1")
with open('FormCronograma.Designer.cs', 'w', encoding='utf-8') as f:
    f.write(content)

# Fix FormAdmin.Designer.cs
with open('FormAdmin.Designer.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Remove the field declaration
content = re.sub(r'\s*private\s+DateTimePicker\s+dtpInicioSemestre;', '', content)
content = re.sub(r'\s*private\s+Label\s+lblInicio;', '', content)

# Remove the properties block for dtpInicioSemestre and lblInicio
# We search for // dtpInicioSemestre and remove its block
content = re.sub(r'\s*// \s*// dtpInicioSemestre\s*//\s*this\.dtpInicioSemestre.*?TabIndex = 1;', '', content, flags=re.DOTALL)
content = re.sub(r'\s*// \s*// lblInicio\s*//\s*this\.lblInicio.*?Text = "Inicio Semestre:";', '', content, flags=re.DOTALL)

with open('FormAdmin.Designer.cs', 'w', encoding='utf-8') as f:
    f.write(content)

print("Namespaces and Designer patched.")
