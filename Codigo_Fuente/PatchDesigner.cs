using System;
using System.IO;
using System.Text.RegularExpressions;

class Program {
    static void Main() {
        string file = "FormAdmin.Designer.cs";
        string content = File.ReadAllText(file);

        // 1. Instantiations
        content = content.Replace("this.txtBuscar = new System.Windows.Forms.TextBox();",
            "this.lblBuscarDocentes = new System.Windows.Forms.Label();\n            this.cmbCriterioDocentes = new System.Windows.Forms.ComboBox();\n            this.lblBuscarCursos = new System.Windows.Forms.Label();\n            this.cmbCriterioCursos = new System.Windows.Forms.ComboBox();\n            this.lblBuscarHorarios = new System.Windows.Forms.Label();\n            this.cmbCriterioHorarios = new System.Windows.Forms.ComboBox();\n            this.lblBuscarHistorial = new System.Windows.Forms.Label();\n            this.cmbCriterioHistorial = new System.Windows.Forms.ComboBox();\n            this.txtBuscar = new System.Windows.Forms.TextBox();");

        // 2. Controls.Add
        content = content.Replace("this.tabPage1.Controls.Add(this.txtBuscar);",
            "this.tabPage1.Controls.Add(this.lblBuscarDocentes);\n            this.tabPage1.Controls.Add(this.cmbCriterioDocentes);\n            this.tabPage1.Controls.Add(this.txtBuscar);");
            
        content = content.Replace("this.tabPage6.Controls.Add(this.txtNuevoNombreCurso);",
            "this.tabPage6.Controls.Add(this.lblBuscarCursos);\n            this.tabPage6.Controls.Add(this.cmbCriterioCursos);\n            this.tabPage6.Controls.Add(this.txtNuevoNombreCurso);");
            
        content = content.Replace("this.tabPage4.Controls.Add(this.txtBuscarHorario);",
            "this.tabPage4.Controls.Add(this.lblBuscarHorarios);\n            this.tabPage4.Controls.Add(this.cmbCriterioHorarios);\n            this.tabPage4.Controls.Add(this.txtBuscarHorario);");

        content = content.Replace("this.tabPage3.Controls.Add(this.txtBuscarHistorialNombre);",
            "this.tabPage3.Controls.Add(this.lblBuscarHistorial);\n            this.tabPage3.Controls.Add(this.cmbCriterioHistorial);\n            this.tabPage3.Controls.Add(this.txtBuscarHistorialNombre);");

        // 3. Definitions
        content = content.Replace("private TextBox txtBuscar;",
            "private System.Windows.Forms.Label lblBuscarDocentes;\n        private System.Windows.Forms.ComboBox cmbCriterioDocentes;\n        private System.Windows.Forms.Label lblBuscarCursos;\n        private System.Windows.Forms.ComboBox cmbCriterioCursos;\n        private System.Windows.Forms.Label lblBuscarHorarios;\n        private System.Windows.Forms.ComboBox cmbCriterioHorarios;\n        private System.Windows.Forms.Label lblBuscarHistorial;\n        private System.Windows.Forms.ComboBox cmbCriterioHistorial;\n        private TextBox txtBuscar;");

        // 4. Properties - txtBuscar
        content = Regex.Replace(content, @"// \s*// txtBuscar\s*// \s*this\.txtBuscar\.Location.*?Size\(\d+, \d+\);", 
            @"// 
            // lblBuscarDocentes
            // 
            this.lblBuscarDocentes.AutoSize = true;
            this.lblBuscarDocentes.Location = new System.Drawing.Point(10, 35);
            this.lblBuscarDocentes.Name = ""lblBuscarDocentes"";
            this.lblBuscarDocentes.Size = new System.Drawing.Size(91, 17);
            this.lblBuscarDocentes.TabIndex = 100;
            this.lblBuscarDocentes.Text = ""Buscar por:"";
            // 
            // cmbCriterioDocentes
            // 
            this.cmbCriterioDocentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioDocentes.FormattingEnabled = true;
            this.cmbCriterioDocentes.Items.AddRange(new object[] {
            ""Todos"",
            ""Nombre"",
            ""Código"",
            ""DNI""});
            this.cmbCriterioDocentes.Location = new System.Drawing.Point(105, 31);
            this.cmbCriterioDocentes.Name = ""cmbCriterioDocentes"";
            this.cmbCriterioDocentes.Size = new System.Drawing.Size(120, 24);
            this.cmbCriterioDocentes.TabIndex = 101;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(235, 32);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = ""txtBuscar"";
            this.txtBuscar.Size = new System.Drawing.Size(398, 22);", RegexOptions.Singleline);

        // 5. Properties - txtNuevoNombreCurso
        content = Regex.Replace(content, @"this\.txtNuevoNombreCurso\.Location.*?Size\(\d+, \d+\);", 
            @"this.lblBuscarCursos.AutoSize = true;
            this.lblBuscarCursos.Location = new System.Drawing.Point(10, 35);
            this.lblBuscarCursos.Name = ""lblBuscarCursos"";
            this.lblBuscarCursos.Size = new System.Drawing.Size(91, 17);
            this.lblBuscarCursos.TabIndex = 102;
            this.lblBuscarCursos.Text = ""Buscar por:"";
            
            this.cmbCriterioCursos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioCursos.FormattingEnabled = true;
            this.cmbCriterioCursos.Items.AddRange(new object[] {
            ""Todos"",
            ""Código"",
            ""Nombre"",
            ""Escuela""});
            this.cmbCriterioCursos.Location = new System.Drawing.Point(105, 31);
            this.cmbCriterioCursos.Name = ""cmbCriterioCursos"";
            this.cmbCriterioCursos.Size = new System.Drawing.Size(120, 24);
            this.cmbCriterioCursos.TabIndex = 103;
            
            this.txtNuevoNombreCurso.Location = new System.Drawing.Point(235, 32);
            this.txtNuevoNombreCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNuevoNombreCurso.Name = ""txtNuevoNombreCurso"";
            this.txtNuevoNombreCurso.Size = new System.Drawing.Size(393, 27);", RegexOptions.Singleline);

        // 6. Properties - txtBuscarHorario
        content = Regex.Replace(content, @"// \s*// txtBuscarHorario\s*// \s*this\.txtBuscarHorario\.Location.*?Size\(\d+, \d+\);", 
            @"// 
            // lblBuscarHorarios
            // 
            this.lblBuscarHorarios.AutoSize = true;
            this.lblBuscarHorarios.Location = new System.Drawing.Point(10, 38);
            this.lblBuscarHorarios.Name = ""lblBuscarHorarios"";
            this.lblBuscarHorarios.Size = new System.Drawing.Size(91, 17);
            this.lblBuscarHorarios.TabIndex = 104;
            this.lblBuscarHorarios.Text = ""Buscar por:"";
            // 
            // cmbCriterioHorarios
            // 
            this.cmbCriterioHorarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioHorarios.FormattingEnabled = true;
            this.cmbCriterioHorarios.Items.AddRange(new object[] {
            ""Todos"",
            ""Código"",
            ""Docente"",
            ""Curso"",
            ""Día""});
            this.cmbCriterioHorarios.Location = new System.Drawing.Point(105, 35);
            this.cmbCriterioHorarios.Name = ""cmbCriterioHorarios"";
            this.cmbCriterioHorarios.Size = new System.Drawing.Size(120, 24);
            this.cmbCriterioHorarios.TabIndex = 105;
            // 
            // txtBuscarHorario
            // 
            this.txtBuscarHorario.Location = new System.Drawing.Point(235, 35);
            this.txtBuscarHorario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscarHorario.Name = ""txtBuscarHorario"";
            this.txtBuscarHorario.Size = new System.Drawing.Size(387, 27);", RegexOptions.Singleline);

        // 7. Properties - txtBuscarHistorialNombre
        content = Regex.Replace(content, @"// \s*// txtBuscarHistorialNombre\s*// \s*this\.txtBuscarHistorialNombre\.Location.*?Size\(\d+, \d+\);", 
            @"// 
            // lblBuscarHistorial
            // 
            this.lblBuscarHistorial.AutoSize = true;
            this.lblBuscarHistorial.Location = new System.Drawing.Point(260, 31);
            this.lblBuscarHistorial.Name = ""lblBuscarHistorial"";
            this.lblBuscarHistorial.Size = new System.Drawing.Size(91, 17);
            this.lblBuscarHistorial.TabIndex = 106;
            this.lblBuscarHistorial.Text = ""Buscar por:"";
            // 
            // cmbCriterioHistorial
            // 
            this.cmbCriterioHistorial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioHistorial.FormattingEnabled = true;
            this.cmbCriterioHistorial.Items.AddRange(new object[] {
            ""Todos"",
            ""Docente"",
            ""Código"",
            ""Curso""});
            this.cmbCriterioHistorial.Location = new System.Drawing.Point(350, 28);
            this.cmbCriterioHistorial.Name = ""cmbCriterioHistorial"";
            this.cmbCriterioHistorial.Size = new System.Drawing.Size(120, 24);
            this.cmbCriterioHistorial.TabIndex = 107;
            // 
            // txtBuscarHistorialNombre
            // 
            this.txtBuscarHistorialNombre.Location = new System.Drawing.Point(480, 28);
            this.txtBuscarHistorialNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscarHistorialNombre.Name = ""txtBuscarHistorialNombre"";
            this.txtBuscarHistorialNombre.Size = new System.Drawing.Size(276, 22);", RegexOptions.Singleline);

        File.WriteAllText(file, content);
        Console.WriteLine("Designer patched.");
    }
}
