import re

with open('FormAdmin.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# 1. Fix ConfigurarBuscadores
config_target = """        private void ConfigurarBuscadores()
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

config_replace = """        private void ConfigurarBuscadores()
        {
            if (cmbCriterioDocentes.Items.Count > 0) cmbCriterioDocentes.SelectedIndex = 0;
            if (cmbCriterioCursos.Items.Count > 0) cmbCriterioCursos.SelectedIndex = 0;
            if (cmbCriterioHorarios.Items.Count > 0) cmbCriterioHorarios.SelectedIndex = 0;
            if (cmbCriterioHistorial.Items.Count > 0) cmbCriterioHistorial.SelectedIndex = 0;

            cmbCriterioDocentes.SelectedIndexChanged += (s, e) => AplicarFiltro();
            cmbCriterioCursos.SelectedIndexChanged += (s, e) => txtBuscarCurso_TextChanged(txtNuevoNombreCurso, EventArgs.Empty);
            cmbCriterioHorarios.SelectedIndexChanged += (s, e) => TxtBuscarHorario_TextChanged(txtBuscarHorario, EventArgs.Empty);
            cmbCriterioHistorial.SelectedIndexChanged += (s, e) => AplicarFiltroHistorial();
        }"""
content = content.replace(config_target, config_replace)

# 2. Fix txtBuscarCurso_TextChanged
cursos_target = """        private void txtBuscarCurso_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBusC = sender as TextBox ?? ObtenerCajaTexto("txtBuscarCurso");
            if (txtBusC == null) return;

            string filtro = txtBusC.Text.Trim();
            if (string.IsNullOrEmpty(filtro))
            {
                tablaCursos.DefaultView.RowFilter = "";
            }
            else
            {
                string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                tablaCursos.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
            }
        }"""

cursos_replace = """        private void txtBuscarCurso_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBusC = sender as TextBox ?? ObtenerCajaTexto("txtBuscarCurso");
            if (txtBusC == null || tablaCursos == null) return;

            string filtro = txtBusC.Text.Trim();
            if (string.IsNullOrEmpty(filtro))
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
        }"""
content = content.replace(cursos_target, cursos_replace)

# 3. Fix TxtBuscarHorario_TextChanged
horarios_target = """        private void TxtBuscarHorario_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscarHorario?.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(filtro))
            {
                tablaHorarios.DefaultView.RowFilter = "";
            }
            else
            {
                string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                tablaHorarios.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
            }
        }"""

horarios_replace = """        private void TxtBuscarHorario_TextChanged(object sender, EventArgs e)
        {
            if (tablaHorarios == null) return;
            string filtro = txtBuscarHorario?.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(filtro))
            {
                tablaHorarios.DefaultView.RowFilter = "";
            }
            else
            {
                string f = QuitarTildes(filtro).ToLower().Replace("'", "''");
                string criterio = cmbCriterioHorarios.SelectedItem?.ToString() ?? "Todos";
                
                if (criterio == "Código")
                    tablaHorarios.DefaultView.RowFilter = $"CdigoDocente LIKE '%{filtro}%'";
                else if (criterio == "Docente")
                    tablaHorarios.DefaultView.RowFilter = $"Docente LIKE '%{filtro}%'";
                else if (criterio == "Curso")
                    tablaHorarios.DefaultView.RowFilter = $"Curso LIKE '%{filtro}%'";
                else if (criterio == "Día")
                    tablaHorarios.DefaultView.RowFilter = $"Dia LIKE '%{filtro}%'";
                else
                    tablaHorarios.DefaultView.RowFilter = $"FiltroBusqueda LIKE '%{f}%'";
            }
        }"""
content = content.replace(horarios_target, horarios_replace)

# Remove the broken FiltrarCursos and FiltrarHorarios if they were accidentally added
content = re.sub(r'        private void FiltrarCursos\(string filtro\)[\s\S]*?        }\r?\n', '', content)
content = re.sub(r'        private void FiltrarHorarios\(string filtro\)[\s\S]*?        }\r?\n', '', content)

with open('FormAdmin.cs', 'w', encoding='utf-8') as f:
    f.write(content)
print("FormAdmin.cs patched successfully.")
