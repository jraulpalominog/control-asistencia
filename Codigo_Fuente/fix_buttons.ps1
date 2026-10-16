$designerPath = "FormAdmin.Designer.cs"
$content = Get-Content $designerPath -Raw

$configs = @(
    @{ Name="btnExportarPDF"; Text="Exportar PDF"; Color="LightSkyBlue"; X=630; Y=15 }
    @{ Name="btnExportarExcel"; Text="Exportar Excel"; Color="LightGreen"; X=750; Y=15 }
    @{ Name="btnEliminarTodo"; Text="Eliminar Todo"; Color="LightCoral"; X=870; Y=15 }
    
    @{ Name="btnImportarExcel"; Text="Importar Excel"; Color="LightSkyBlue"; X=630; Y=15 }
    @{ Name="btnExportarDocentesExcel"; Text="Exportar Excel"; Color="LightGreen"; X=750; Y=15 }
    @{ Name="btnExportarDocentesPDF"; Text="Exportar PDF"; Color="LightSkyBlue"; X=870; Y=15 }
    
    @{ Name="btnImportarCursos"; Text="Importar Excel"; Color="LightSkyBlue"; X=630; Y=15 }
    @{ Name="btnExportarCursosExcel"; Text="Exportar Excel"; Color="LightGreen"; X=750; Y=15 }
    @{ Name="btnExportarCursosPDF"; Text="Exportar PDF"; Color="LightSkyBlue"; X=870; Y=15 }
    
    @{ Name="btnCerrarSesion"; Text="Cerrar Sesión"; Color="LightCoral"; X=880; Y=15 }
)

foreach ($c in $configs) {
    $btn = $c.Name
    $text = $c.Text
    $color = $c.Color
    $nx = $c.X
    $ny = $c.Y

    # Remove existing conflicting properties to avoid duplicates
    $content = $content -replace "(?m)^\s*this\.$btn\.Text\s*=.*`r?`n?", ""
    $content = $content -replace "(?m)^\s*this\.$btn\.BackColor\s*=.*`r?`n?", ""
    $content = $content -replace "(?m)^\s*this\.$btn\.FlatStyle\s*=.*`r?`n?", ""
    $content = $content -replace "(?m)^\s*this\.$btn\.Font\s*=.*`r?`n?", ""
    $content = $content -replace "(?m)^\s*this\.$btn\.Cursor\s*=.*`r?`n?", ""
    $content = $content -replace "(?m)^\s*this\.$btn\.UseVisualStyleBackColor\s*=.*`r?`n?", ""

    # Replace Location and Size
    $content = $content -replace "(?m)^\s*this\.$btn\.Location\s*=\s*new System\.Drawing\.Point\([^)]+\);`r?`n?", "            this.$btn.Location = new System.Drawing.Point($nx, $ny);`r`n"
    $content = $content -replace "(?m)^\s*this\.$btn\.Size\s*=\s*new System\.Drawing\.Size\([^)]+\);`r?`n?", "            this.$btn.Size = new System.Drawing.Size(110, 30);`r`n"

    # Inject new styling right before the Name property
    $injection = "            this.$btn.Text = ""$text"";`r`n" +
                 "            this.$btn.BackColor = System.Drawing.Color.$color;`r`n" +
                 "            this.$btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;`r`n" +
                 "            this.$btn.Font = new System.Drawing.Font(""Microsoft Sans Serif"", 8F, System.Drawing.FontStyle.Bold);`r`n" +
                 "            this.$btn.Cursor = System.Windows.Forms.Cursors.Hand;`r`n"

    $content = $content -replace "(?m)(^\s*this\.$btn\.Name\s*=.*)", "`$1`r`n$injection"
}

Set-Content $designerPath -Value $content
Write-Output "Done applying properties to Designer."
