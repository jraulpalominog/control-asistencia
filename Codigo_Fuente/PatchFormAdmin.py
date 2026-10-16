import re
import sys

with open('FormAdmin.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# 1. Update ConfigurarBuscadores in constructor
constructor_target = """            CargarHistorial();
            AplicarFiltroHistorial(); // Filtrar por el día de hoy por defecto al inicio
            CargarCursos();
            ConfigurarValidacionEnTiempoReal();

            // Horarios de Clases
            CargarHorarios();
        }"""

constructor_replace = """            CargarHistorial();
            AplicarFiltroHistorial(); // Filtrar por el día de hoy por defecto al inicio
            CargarCursos();
            ConfigurarValidacionEnTiempoReal();

            // Horarios de Clases
            CargarHorarios();
            
            ConfigurarBuscadores();
        }

        private void ConfigurarBuscadores()
        {
            if (cmbCriterioDocentes.Items.Count > 0) cmbCriterioDocentes.SelectedIndex = 0;
            if (cmbCriterioCursos.Items.Count > 0) cmbCriterioCursos.SelectedIndex = 0;
            if (cmbCriterioHorarios.Items.Count > 0) cmbCriterioHorarios.SelectedIndex = 0;
            if (cmbCriterioHistorial.Items.Count > 0) cmbCriterioHistorial.SelectedIndex = 0;

            cmbCriterioDocentes.SelectedIndexChanged += (s, e) => AplicarFiltro();
            cmbCriterioCursos.SelectedIndexChanged += (s, e) => FiltrarCursos(txtNuevoNombreCurso.Text);
            cmbCriterioHorarios.SelectedIndexChanged += (s, e) => FiltrarHorarios(txtBuscarHorario.Text);
            cmbCriterioHistorial.SelectedIndexChanged += (s, e) => AplicarFiltroHistorial();
        }"""
content = content.replace(constructor_target, constructor_replace)

# 2. Update AplicarFiltro (Docentes)
aplicar_target = """        private void AplicarFiltro()
        {
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(filtro)) { tablaDocentes.DefaultView.RowFilter = ""; return; }

            string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
            tablaDocentes.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
        }"""

aplicar_replace = """        private void AplicarFiltro()
        {
            if (tablaDocentes == null) return;
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(filtro)) { tablaDocentes.DefaultView.RowFilter = ""; return; }

            string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
            string criterio = cmbCriterioDocentes.SelectedItem?.ToString() ?? "Todos";

            if (criterio == "Nombre")
                tablaDocentes.DefaultView.RowFilter = $"Nombre LIKE '%{filtro}%' OR Apellido LIKE '%{filtro}%'";
            else if (criterio == "Código")
                tablaDocentes.DefaultView.RowFilter = $"Codigo LIKE '%{filtro}%'";
            else if (criterio == "DNI")
                tablaDocentes.DefaultView.RowFilter = $"DNI LIKE '%{filtro}%'";
            else
                tablaDocentes.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
        }"""
content = content.replace(aplicar_target, aplicar_replace)

# 3. Update FiltrarCursos
cursos_target = """        private void FiltrarCursos(string filtro)
        {
            if (tablaCursos != null)
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    tablaCursos.DefaultView.RowFilter = "";
                }
                else
                {
                    string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                    tablaCursos.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
                }
            }
        }"""

cursos_replace = """        private void FiltrarCursos(string filtro)
        {
            if (tablaCursos != null)
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    tablaCursos.DefaultView.RowFilter = "";
                }
                else
                {
                    string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                    string criterio = cmbCriterioCursos.SelectedItem?.ToString() ?? "Todos";
                    
                    if (criterio == "Código")
                        tablaCursos.DefaultView.RowFilter = $"Codigo LIKE '%{filtro}%'";
                    else if (criterio == "Nombre")
                        tablaCursos.DefaultView.RowFilter = $"Nombre LIKE '%{filtro}%'";
                    else if (criterio == "Escuela")
                        tablaCursos.DefaultView.RowFilter = $"Escuela LIKE '%{filtro}%'";
                    else
                        tablaCursos.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
                }
            }
        }"""
content = content.replace(cursos_target, cursos_replace)

# 4. Update FiltrarHorarios
horarios_target = """        private void FiltrarHorarios(string filtro)
        {
            if (tablaHorarios != null)
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    tablaHorarios.DefaultView.RowFilter = "";
                }
                else
                {
                    string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                    tablaHorarios.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
                }
            }
        }"""

horarios_replace = """        private void FiltrarHorarios(string filtro)
        {
            if (tablaHorarios != null)
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    tablaHorarios.DefaultView.RowFilter = "";
                }
                else
                {
                    string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                    string criterio = cmbCriterioHorarios.SelectedItem?.ToString() ?? "Todos";
                    
                    if (criterio == "Código")
                        tablaHorarios.DefaultView.RowFilter = $"[CódigoDocente] LIKE '%{filtro}%'";
                    else if (criterio == "Docente")
                        tablaHorarios.DefaultView.RowFilter = $"Docente LIKE '%{filtro}%'";
                    else if (criterio == "Curso")
                        tablaHorarios.DefaultView.RowFilter = $"Curso LIKE '%{filtro}%'";
                    else if (criterio == "Día")
                        tablaHorarios.DefaultView.RowFilter = $"Dia LIKE '%{filtro}%'";
                    else
                        tablaHorarios.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
                }
            }
        }"""
content = content.replace(horarios_target, horarios_replace)

# 5. Update AplicarFiltroHistorial
historial_regex = r"string texto = txtBuscarHistorialNombre\.Text\.Trim\(\)\.Replace\(\"'\", \"''\"\);\s*if \(\!string\.IsNullOrEmpty\(texto\)\)\s*\{\s*filtro \+\= \$\" AND \(Nombre LIKE \'%\{texto\}%\' OR Apellido LIKE \'%\{texto\}%\' OR Codigo LIKE \'%\{texto\}%\'\)\";\s*\}"

historial_replace = """string texto = txtBuscarHistorialNombre.Text.Trim().Replace("'", "''");
                if (!string.IsNullOrEmpty(texto))
                {
                    string criterio = cmbCriterioHistorial.SelectedItem?.ToString() ?? "Todos";
                    if (criterio == "Docente")
                        filtro += $" AND (Nombre LIKE '%{texto}%' OR Apellido LIKE '%{texto}%')";
                    else if (criterio == "Código")
                        filtro += $" AND Codigo LIKE '%{texto}%'";
                    else if (criterio == "Curso")
                        filtro += $" AND Curso LIKE '%{texto}%'";
                    else
                        filtro += $" AND (Nombre LIKE '%{texto}%' OR Apellido LIKE '%{texto}%' OR Codigo LIKE '%{texto}%' OR Curso LIKE '%{texto}%')";
                }"""

content = re.sub(historial_regex, historial_replace, content)

with open('FormAdmin.cs', 'w', encoding='utf-8') as f:
    f.write(content)
print("FormAdmin.cs patched successfully.")
