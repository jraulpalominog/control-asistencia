import re

with open('FormAdmin.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Replace dtpInicioSemestre with ObtenerInicioSemestre()
content = re.sub(r'dtpInicioSemestre\.Value', 'ObtenerInicioSemestre()', content)

# Add ObtenerInicioSemestre and btnGestionarCronograma_Click before FormAdmin constructor or somewhere safe
# Actually, let's insert it right before BtnGenerarReporteSemanas_Click
new_methods = """        private void btnGestionarCronograma_Click(object sender, EventArgs e)
        {
            new FormCronograma().ShowDialog(this);
        }

        private DateTime ObtenerInicioSemestre()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT ValorTxt FROM ConfiguracionSistema WHERE Clave = 'InicioSemestre'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && DateTime.TryParse(result.ToString(), out DateTime fecha))
                            return fecha;
                    }
                }
                catch { }
            }
            return DateTime.Today;
        }

"""

content = content.replace("private void BtnGenerarReporteSemanas_Click", new_methods + "        private void BtnGenerarReporteSemanas_Click")

# Now inject the exam week logic into BtnGenerarReporteSemanas_Click
target_loop_resumen = """                        for (int w = 1; w <= 16; w++)
                        {
                            DateTime inicioSemana = lunesSemana1.AddDays((w - 1) * 7);
                            
                            foreach (var slot in slots)
                            {
                                int diaSemana = slot.Field<int>("DiaSemana");"""

injection_loop_resumen = """                        for (int w = 1; w <= 16; w++)
                        {
                            DateTime inicioSemana = lunesSemana1.AddDays((w - 1) * 7);
                            bool esSemanaExamen = (w == 8 || w == 16);
                            
                            foreach (var slot in slots)
                            {
                                string tipoClaseActual = slot.Field<string>("TipoClase").ToUpper();
                                if (esSemanaExamen && tieneTeoria && tienePractica && tipoClaseActual == "TEORÍA")
                                {
                                    continue;
                                }

                                int diaSemana = slot.Field<int>("DiaSemana");"""

content = content.replace(target_loop_resumen, injection_loop_resumen)


# Now inject the exam week logic into GenerarDetalleReporteSemanas_Click
target_loop_detalle = """                    for (int w = 1; w <= 16; w++)
                    {
                        DateTime inicioSemana = lunesSemana1.AddDays((w - 1) * 7);
                        
                        foreach (var slot in slots)
                        {
                            int diaSemana = slot.Field<int>("DiaSemana");"""

injection_loop_detalle = """                    for (int w = 1; w <= 16; w++)
                    {
                        DateTime inicioSemana = lunesSemana1.AddDays((w - 1) * 7);
                        bool esSemanaExamen = (w == 8 || w == 16);
                        
                        foreach (var slot in slots)
                        {
                            string tipoClaseActual = slot.Field<string>("TipoClase").ToUpper();
                            if (esSemanaExamen && tieneTeoria && tienePractica && tipoClaseActual == "TEORÍA")
                            {
                                continue;
                            }

                            int diaSemana = slot.Field<int>("DiaSemana");"""

content = content.replace(target_loop_detalle, injection_loop_detalle)

with open('FormAdmin.cs', 'w', encoding='utf-8') as f:
    f.write(content)

print("FormAdmin.cs logic patched successfully.")
