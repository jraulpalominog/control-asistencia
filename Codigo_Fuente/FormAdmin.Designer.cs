using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class FormAdmin
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

#region Windows Form Designer generated code
        	private void InitializeComponent()
        	{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdmin));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Periodo = new System.Windows.Forms.Button();
            this.btnEliminarTodo = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            this.btnConfigLimites = new System.Windows.Forms.Button();
            this.lblBuscarHistorial = new System.Windows.Forms.Label();
            this.cmbCriterioHistorial = new System.Windows.Forms.ComboBox();
            this.txtBuscarHistorialNombre = new System.Windows.Forms.TextBox();
            this.monthCalendarHistorial = new System.Windows.Forms.MonthCalendar();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnImportarExcel = new System.Windows.Forms.Button();
            this.btnExportarDocentesExcel = new System.Windows.Forms.Button();
            this.btnExportarDocentesPDF = new System.Windows.Forms.Button();
            this.dgvDocentes = new System.Windows.Forms.DataGridView();
            this.menuOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminardocente = new System.Windows.Forms.ToolStripMenuItem();
            this.editarDocenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.añadirDocenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBuscarDocentes = new System.Windows.Forms.Label();
            this.cmbCriterioDocentes = new System.Windows.Forms.ComboBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnImportarCursos = new System.Windows.Forms.Button();
            this.btnExportarCursosExcel = new System.Windows.Forms.Button();
            this.btnExportarCursosPDF = new System.Windows.Forms.Button();
            this.dgvCursos = new System.Windows.Forms.DataGridView();
            this.menuCursos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarCursoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarCursoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.añadirAsignaturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBuscarCursos = new System.Windows.Forms.Label();
            this.cmbCriterioCursos = new System.Windows.Forms.ComboBox();
            this.txtNuevoNombreCurso = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvHorarios = new System.Windows.Forms.DataGridView();
            this.menuHorarios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAgregar = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBuscarHorarios = new System.Windows.Forms.Label();
            this.cmbCriterioHorarios = new System.Windows.Forms.ComboBox();
            this.txtBuscarHorario = new System.Windows.Forms.TextBox();
            this.btnAgregarHorario = new System.Windows.Forms.Button();
            this.btnImportarHorarios = new System.Windows.Forms.Button();
            this.btnExportarHorariosCSV = new System.Windows.Forms.Button();
            this.tabPageReporteSemanas = new System.Windows.Forms.TabPage();
            this.splitContainerReporte = new System.Windows.Forms.SplitContainer();
            this.dgvReporteResumen = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.btnExportarReporteResumenCSV = new System.Windows.Forms.Button();
            this.btnExportarReporteDetallePDF = new System.Windows.Forms.Button();
            this.btnExportarReporteResumenPDF = new System.Windows.Forms.Button();
            this.btnGenerarReporteSemanas = new System.Windows.Forms.Button();
            this.btnGestionarCronograma = new System.Windows.Forms.Button();
            this.cmbTipoFiltroReporte = new System.Windows.Forms.ComboBox();
            this.cmbReporteDocente = new System.Windows.Forms.ComboBox();
            this.lblDoc = new System.Windows.Forms.Label();
            this.dgvReporteDetalle = new System.Windows.Forms.DataGridView();
            this.pnlDetalleTitulo = new System.Windows.Forms.Panel();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.cmbAddTipoCurso = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAñadirCurso = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAddNombreCurso = new System.Windows.Forms.TextBox();
            this.txtAddCodigoCurso = new System.Windows.Forms.TextBox();
            this.txtAddCreditosCurso = new System.Windows.Forms.TextBox();
            this.cmbAddEscuela = new System.Windows.Forms.ComboBox();
            this.lblAddEscuela = new System.Windows.Forms.Label();
            this.cmbAddMalla = new System.Windows.Forms.ComboBox();
            this.lblAddMalla = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtAddCorreo = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbAddDepartamento = new System.Windows.Forms.ComboBox();
            this.cmbAddCategoria = new System.Windows.Forms.ComboBox();
            this.cmbAddClase = new System.Windows.Forms.ComboBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.txtAddDNI = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAgregarDocente = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAddCodigo = new System.Windows.Forms.TextBox();
            this.txtAddNombre = new System.Windows.Forms.TextBox();
            this.txtAddApellido = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnGuardarEdicionCurso = new System.Windows.Forms.Button();
            this.txtNuevoTipoCurso = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtEditNombreCurso = new System.Windows.Forms.TextBox();
            this.txtEditCodigoCurso = new System.Windows.Forms.TextBox();
            this.txtEditCreditosCurso = new System.Windows.Forms.TextBox();
            this.cmbEditEscuela = new System.Windows.Forms.ComboBox();
            this.lblEditEscuela = new System.Windows.Forms.Label();
            this.cmbEditMalla = new System.Windows.Forms.ComboBox();
            this.lblEditMalla = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.txtNuevoCorreo = new System.Windows.Forms.TextBox();
            this.txtNuevoDNI = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btnEditarDocente = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbNuevoDepartamento = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbNuevoCategoria = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbNuevoClase = new System.Windows.Forms.ComboBox();
            this.txtNuevoCodigo = new System.Windows.Forms.TextBox();
            this.txtNuevoNombre = new System.Windows.Forms.TextBox();
            this.txtNuevoApellido = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocentes)).BeginInit();
            this.menuOpciones.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCursos)).BeginInit();
            this.menuCursos.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorarios)).BeginInit();
            this.menuHorarios.SuspendLayout();
            this.tabPageReporteSemanas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReporte)).BeginInit();
            this.splitContainerReporte.Panel1.SuspendLayout();
            this.splitContainerReporte.Panel2.SuspendLayout();
            this.splitContainerReporte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteResumen)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteDetalle)).BeginInit();
            this.pnlDetalleTitulo.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPageReporteSemanas);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControl1.Location = new System.Drawing.Point(27, 68);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1827, 702);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Periodo);
            this.tabPage3.Controls.Add(this.btnEliminarTodo);
            this.tabPage3.Controls.Add(this.btnExportarExcel);
            this.tabPage3.Controls.Add(this.btnExportarPDF);
            this.tabPage3.Controls.Add(this.btnConfigLimites);
            this.tabPage3.Controls.Add(this.lblBuscarHistorial);
            this.tabPage3.Controls.Add(this.cmbCriterioHistorial);
            this.tabPage3.Controls.Add(this.txtBuscarHistorialNombre);
            this.tabPage3.Controls.Add(this.monthCalendarHistorial);
            this.tabPage3.Controls.Add(this.dgvHistorial);
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.ImageIndex = 1;
            this.tabPage3.Location = new System.Drawing.Point(4, 44);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(1819, 654);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Historial de Asistencias";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // Periodo
            // 
            this.Periodo.Location = new System.Drawing.Point(29, 370);
            this.Periodo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Periodo.Name = "Periodo";
            this.Periodo.Size = new System.Drawing.Size(124, 30);
            this.Periodo.TabIndex = 27;
            this.Periodo.Text = "Periodo";
            this.Periodo.UseVisualStyleBackColor = true;
            this.Periodo.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnEliminarTodo
            // 
            this.btnEliminarTodo.BackColor = System.Drawing.Color.LightCoral;
            this.btnEliminarTodo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarTodo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarTodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnEliminarTodo.Location = new System.Drawing.Point(1465, 15);
            this.btnEliminarTodo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEliminarTodo.Name = "btnEliminarTodo";
            this.btnEliminarTodo.Size = new System.Drawing.Size(147, 37);
            this.btnEliminarTodo.TabIndex = 22;
            this.btnEliminarTodo.Text = "Eliminar Todo";
            this.btnEliminarTodo.UseVisualStyleBackColor = false;
            this.btnEliminarTodo.Click += new System.EventHandler(this.btnEliminarTodo_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnExportarExcel.Location = new System.Drawing.Point(1305, 15);
            this.btnExportarExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(147, 37);
            this.btnExportarExcel.TabIndex = 21;
            this.btnExportarExcel.Text = "Exportar Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnExportarPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnExportarPDF.Location = new System.Drawing.Point(1145, 15);
            this.btnExportarPDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(147, 37);
            this.btnExportarPDF.TabIndex = 20;
            this.btnExportarPDF.Text = "Exportar PDF";
            this.btnExportarPDF.UseVisualStyleBackColor = false;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // btnConfigLimites
            // 
            this.btnConfigLimites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnConfigLimites.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigLimites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigLimites.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnConfigLimites.ForeColor = System.Drawing.Color.White;
            this.btnConfigLimites.Location = new System.Drawing.Point(29, 415);
            this.btnConfigLimites.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfigLimites.Name = "btnConfigLimites";
            this.btnConfigLimites.Size = new System.Drawing.Size(200, 37);
            this.btnConfigLimites.TabIndex = 28;
            this.btnConfigLimites.Text = "Límites de Tiempo";
            this.btnConfigLimites.UseVisualStyleBackColor = false;
            this.btnConfigLimites.Click += new System.EventHandler(this.btnConfigLimites_Click);
            // 
            // lblBuscarHistorial
            // 
            this.lblBuscarHistorial.AutoSize = true;
            this.lblBuscarHistorial.Location = new System.Drawing.Point(356, 32);
            this.lblBuscarHistorial.Name = "lblBuscarHistorial";
            this.lblBuscarHistorial.Size = new System.Drawing.Size(86, 16);
            this.lblBuscarHistorial.TabIndex = 106;
            this.lblBuscarHistorial.Text = "Buscar por:";
            // 
            // cmbCriterioHistorial
            // 
            this.cmbCriterioHistorial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioHistorial.FormattingEnabled = true;
            this.cmbCriterioHistorial.Items.AddRange(new object[] {
            "Todos",
            "Docente",
            "Código",
            "Curso"});
            this.cmbCriterioHistorial.Location = new System.Drawing.Point(473, 28);
            this.cmbCriterioHistorial.Name = "cmbCriterioHistorial";
            this.cmbCriterioHistorial.Size = new System.Drawing.Size(120, 24);
            this.cmbCriterioHistorial.TabIndex = 107;
            // 
            // txtBuscarHistorialNombre
            // 
            this.txtBuscarHistorialNombre.Location = new System.Drawing.Point(637, 28);
            this.txtBuscarHistorialNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscarHistorialNombre.Name = "txtBuscarHistorialNombre";
            this.txtBuscarHistorialNombre.Size = new System.Drawing.Size(365, 22);
            this.txtBuscarHistorialNombre.TabIndex = 4;
            this.txtBuscarHistorialNombre.TextChanged += new System.EventHandler(this.txtBuscarHistorialNombre_TextChanged);
            // 
            // monthCalendarHistorial
            // 
            this.monthCalendarHistorial.Location = new System.Drawing.Point(26, 28);
            this.monthCalendarHistorial.MaxSelectionCount = 1;
            this.monthCalendarHistorial.Name = "monthCalendarHistorial";
            this.monthCalendarHistorial.TabIndex = 3;
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(348, 65);
            this.dgvHistorial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.RowHeadersWidth = 51;
            this.dgvHistorial.RowTemplate.Height = 24;
            this.dgvHistorial.Size = new System.Drawing.Size(1264, 573);
            this.dgvHistorial.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnImportarExcel);
            this.tabPage1.Controls.Add(this.btnExportarDocentesExcel);
            this.tabPage1.Controls.Add(this.btnExportarDocentesPDF);
            this.tabPage1.Controls.Add(this.dgvDocentes);
            this.tabPage1.Controls.Add(this.lblBuscarDocentes);
            this.tabPage1.Controls.Add(this.cmbCriterioDocentes);
            this.tabPage1.Controls.Add(this.txtBuscar);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.ImageIndex = 3;
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1819, 654);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Listado de Docentes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnImportarExcel
            // 
            this.btnImportarExcel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnImportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnImportarExcel.Location = new System.Drawing.Point(1136, 19);
            this.btnImportarExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImportarExcel.Name = "btnImportarExcel";
            this.btnImportarExcel.Size = new System.Drawing.Size(147, 37);
            this.btnImportarExcel.TabIndex = 24;
            this.btnImportarExcel.Text = "Importar Excel";
            this.btnImportarExcel.UseVisualStyleBackColor = false;
            this.btnImportarExcel.Click += new System.EventHandler(this.btnImportarExcel_Click_1);
            // 
            // btnExportarDocentesExcel
            // 
            this.btnExportarDocentesExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportarDocentesExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarDocentesExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarDocentesExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnExportarDocentesExcel.Location = new System.Drawing.Point(1296, 19);
            this.btnExportarDocentesExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportarDocentesExcel.Name = "btnExportarDocentesExcel";
            this.btnExportarDocentesExcel.Size = new System.Drawing.Size(147, 37);
            this.btnExportarDocentesExcel.TabIndex = 23;
            this.btnExportarDocentesExcel.Text = "Exportar Excel";
            this.btnExportarDocentesExcel.UseVisualStyleBackColor = false;
            this.btnExportarDocentesExcel.Click += new System.EventHandler(this.btnExportarDocentesExcel_Click);
            // 
            // btnExportarDocentesPDF
            // 
            this.btnExportarDocentesPDF.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnExportarDocentesPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarDocentesPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarDocentesPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnExportarDocentesPDF.Location = new System.Drawing.Point(1456, 19);
            this.btnExportarDocentesPDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportarDocentesPDF.Name = "btnExportarDocentesPDF";
            this.btnExportarDocentesPDF.Size = new System.Drawing.Size(147, 37);
            this.btnExportarDocentesPDF.TabIndex = 22;
            this.btnExportarDocentesPDF.Text = "Exportar PDF";
            this.btnExportarDocentesPDF.UseVisualStyleBackColor = false;
            this.btnExportarDocentesPDF.Click += new System.EventHandler(this.btnExportarDocentesPDF_Click);
            // 
            // dgvDocentes
            // 
            this.dgvDocentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocentes.ContextMenuStrip = this.menuOpciones;
            this.dgvDocentes.Location = new System.Drawing.Point(39, 75);
            this.dgvDocentes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDocentes.Name = "dgvDocentes";
            this.dgvDocentes.RowHeadersWidth = 51;
            this.dgvDocentes.RowTemplate.Height = 24;
            this.dgvDocentes.Size = new System.Drawing.Size(1562, 542);
            this.dgvDocentes.TabIndex = 1;
            // 
            // menuOpciones
            // 
            this.menuOpciones.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminardocente,
            this.editarDocenteToolStripMenuItem,
            this.añadirDocenteToolStripMenuItem});
            this.menuOpciones.Name = "contextMenuStrip1";
            this.menuOpciones.Size = new System.Drawing.Size(195, 82);
            // 
            // eliminardocente
            // 
            this.eliminardocente.Image = ((System.Drawing.Image)(resources.GetObject("eliminardocente.Image")));
            this.eliminardocente.Name = "eliminardocente";
            this.eliminardocente.Size = new System.Drawing.Size(194, 26);
            this.eliminardocente.Text = "Eliminar docente";
            this.eliminardocente.Click += new System.EventHandler(this.eliminarDocentesToolStripMenuItem_Click);
            // 
            // editarDocenteToolStripMenuItem
            // 
            this.editarDocenteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editarDocenteToolStripMenuItem.Image")));
            this.editarDocenteToolStripMenuItem.Name = "editarDocenteToolStripMenuItem";
            this.editarDocenteToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
            this.editarDocenteToolStripMenuItem.Text = "Editar docente";
            this.editarDocenteToolStripMenuItem.Click += new System.EventHandler(this.editarDocenteToolStripMenuItem_Click_1);
            // 
            // añadirDocenteToolStripMenuItem
            // 
            this.añadirDocenteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("añadirDocenteToolStripMenuItem.Image")));
            this.añadirDocenteToolStripMenuItem.Name = "añadirDocenteToolStripMenuItem";
            this.añadirDocenteToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
            // 
            // lblBuscarDocentes
            // 
            this.lblBuscarDocentes.AutoSize = true;
            this.lblBuscarDocentes.Location = new System.Drawing.Point(36, 41);
            this.lblBuscarDocentes.Name = "lblBuscarDocentes";
            this.lblBuscarDocentes.Size = new System.Drawing.Size(86, 16);
            this.lblBuscarDocentes.TabIndex = 100;
            this.lblBuscarDocentes.Text = "Buscar por:";
            // 
            // cmbCriterioDocentes
            // 
            this.cmbCriterioDocentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioDocentes.FormattingEnabled = true;
            this.cmbCriterioDocentes.Items.AddRange(new object[] {
            "Todos",
            "Nombre",
            "Código",
            "DNI"});
            this.cmbCriterioDocentes.Location = new System.Drawing.Point(144, 38);
            this.cmbCriterioDocentes.Name = "cmbCriterioDocentes";
            this.cmbCriterioDocentes.Size = new System.Drawing.Size(120, 24);
            this.cmbCriterioDocentes.TabIndex = 101;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(284, 40);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(398, 22);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.Transparent;
            this.tabPage6.Controls.Add(this.btnImportarCursos);
            this.tabPage6.Controls.Add(this.btnExportarCursosExcel);
            this.tabPage6.Controls.Add(this.btnExportarCursosPDF);
            this.tabPage6.Controls.Add(this.dgvCursos);
            this.tabPage6.Controls.Add(this.lblBuscarCursos);
            this.tabPage6.Controls.Add(this.cmbCriterioCursos);
            this.tabPage6.Controls.Add(this.txtNuevoNombreCurso);
            this.tabPage6.ImageIndex = 2;
            this.tabPage6.Location = new System.Drawing.Point(4, 44);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1819, 654);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Lista de Asignaturas";
            this.tabPage6.Click += new System.EventHandler(this.tabPage6_Click);
            // 
            // btnImportarCursos
            // 
            this.btnImportarCursos.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnImportarCursos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportarCursos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportarCursos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnImportarCursos.Location = new System.Drawing.Point(1135, 29);
            this.btnImportarCursos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImportarCursos.Name = "btnImportarCursos";
            this.btnImportarCursos.Size = new System.Drawing.Size(147, 37);
            this.btnImportarCursos.TabIndex = 30;
            this.btnImportarCursos.Text = "Importar Excel";
            this.btnImportarCursos.UseVisualStyleBackColor = false;
            // 
            // btnExportarCursosExcel
            // 
            this.btnExportarCursosExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportarCursosExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarCursosExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarCursosExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnExportarCursosExcel.Location = new System.Drawing.Point(1295, 29);
            this.btnExportarCursosExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportarCursosExcel.Name = "btnExportarCursosExcel";
            this.btnExportarCursosExcel.Size = new System.Drawing.Size(147, 37);
            this.btnExportarCursosExcel.TabIndex = 29;
            this.btnExportarCursosExcel.Text = "Exportar Excel";
            this.btnExportarCursosExcel.UseVisualStyleBackColor = false;
            // 
            // btnExportarCursosPDF
            // 
            this.btnExportarCursosPDF.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnExportarCursosPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarCursosPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarCursosPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnExportarCursosPDF.Location = new System.Drawing.Point(1455, 29);
            this.btnExportarCursosPDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportarCursosPDF.Name = "btnExportarCursosPDF";
            this.btnExportarCursosPDF.Size = new System.Drawing.Size(147, 37);
            this.btnExportarCursosPDF.TabIndex = 28;
            this.btnExportarCursosPDF.Text = "Exportar PDF";
            this.btnExportarCursosPDF.UseVisualStyleBackColor = false;
            // 
            // dgvCursos
            // 
            this.dgvCursos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCursos.ContextMenuStrip = this.menuCursos;
            this.dgvCursos.Location = new System.Drawing.Point(40, 82);
            this.dgvCursos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvCursos.Name = "dgvCursos";
            this.dgvCursos.RowHeadersWidth = 51;
            this.dgvCursos.RowTemplate.Height = 24;
            this.dgvCursos.Size = new System.Drawing.Size(1562, 538);
            this.dgvCursos.TabIndex = 26;
            this.dgvCursos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCursos_CellContentClick);
            // 
            // menuCursos
            // 
            this.menuCursos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuCursos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarCursoToolStripMenuItem,
            this.editarCursoToolStripMenuItem,
            this.añadirAsignaturaToolStripMenuItem});
            this.menuCursos.Name = "contextMenuStrip1";
            this.menuCursos.Size = new System.Drawing.Size(210, 82);
            // 
            // eliminarCursoToolStripMenuItem
            // 
            this.eliminarCursoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eliminarCursoToolStripMenuItem.Image")));
            this.eliminarCursoToolStripMenuItem.Name = "eliminarCursoToolStripMenuItem";
            this.eliminarCursoToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.eliminarCursoToolStripMenuItem.Text = "Eliminar asignatura";
            this.eliminarCursoToolStripMenuItem.Click += new System.EventHandler(this.eliminarCursoToolStripMenuItem_Click);
            // 
            // editarCursoToolStripMenuItem
            // 
            this.editarCursoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editarCursoToolStripMenuItem.Image")));
            this.editarCursoToolStripMenuItem.Name = "editarCursoToolStripMenuItem";
            this.editarCursoToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.editarCursoToolStripMenuItem.Text = "Editar asignatura";
            this.editarCursoToolStripMenuItem.Click += new System.EventHandler(this.editarCursoToolStripMenuItem_Click);
            // 
            // añadirAsignaturaToolStripMenuItem
            // 
            this.añadirAsignaturaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("añadirAsignaturaToolStripMenuItem.Image")));
            this.añadirAsignaturaToolStripMenuItem.Name = "añadirAsignaturaToolStripMenuItem";
            this.añadirAsignaturaToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.añadirAsignaturaToolStripMenuItem.Text = "Añadir asignatura";
            this.añadirAsignaturaToolStripMenuItem.Click += new System.EventHandler(this.añadirCursoToolStripMenuItem_Click);
            // 
            // lblBuscarCursos
            // 
            this.lblBuscarCursos.AutoSize = true;
            this.lblBuscarCursos.Location = new System.Drawing.Point(44, 34);
            this.lblBuscarCursos.Name = "lblBuscarCursos";
            this.lblBuscarCursos.Size = new System.Drawing.Size(108, 20);
            this.lblBuscarCursos.TabIndex = 102;
            this.lblBuscarCursos.Text = "Buscar por:";
            // 
            // cmbCriterioCursos
            // 
            this.cmbCriterioCursos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioCursos.FormattingEnabled = true;
            this.cmbCriterioCursos.Items.AddRange(new object[] {
            "Todos",
            "Código",
            "Nombre",
            "Escuela"});
            this.cmbCriterioCursos.Location = new System.Drawing.Point(168, 36);
            this.cmbCriterioCursos.Name = "cmbCriterioCursos";
            this.cmbCriterioCursos.Size = new System.Drawing.Size(120, 28);
            this.cmbCriterioCursos.TabIndex = 103;
            // 
            // txtNuevoNombreCurso
            // 
            this.txtNuevoNombreCurso.Location = new System.Drawing.Point(314, 36);
            this.txtNuevoNombreCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNuevoNombreCurso.Name = "txtNuevoNombreCurso";
            this.txtNuevoNombreCurso.Size = new System.Drawing.Size(393, 27);
            this.txtNuevoNombreCurso.TabIndex = 25;
            this.txtNuevoNombreCurso.TextChanged += new System.EventHandler(this.txtBuscarCurso_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvHorarios);
            this.tabPage4.Controls.Add(this.lblBuscarHorarios);
            this.tabPage4.Controls.Add(this.cmbCriterioHorarios);
            this.tabPage4.Controls.Add(this.txtBuscarHorario);
            this.tabPage4.Controls.Add(this.btnAgregarHorario);
            this.tabPage4.Controls.Add(this.btnImportarHorarios);
            this.tabPage4.Controls.Add(this.btnExportarHorariosCSV);
            this.tabPage4.ImageIndex = 9;
            this.tabPage4.Location = new System.Drawing.Point(4, 44);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1819, 654);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Horarios de Clases";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvHorarios
            // 
            this.dgvHorarios.AllowUserToAddRows = false;
            this.dgvHorarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHorarios.ColumnHeadersHeight = 29;
            this.dgvHorarios.ContextMenuStrip = this.menuHorarios;
            this.dgvHorarios.Location = new System.Drawing.Point(39, 75);
            this.dgvHorarios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvHorarios.Name = "dgvHorarios";
            this.dgvHorarios.ReadOnly = true;
            this.dgvHorarios.RowHeadersWidth = 51;
            this.dgvHorarios.Size = new System.Drawing.Size(1539, 533);
            this.dgvHorarios.TabIndex = 5;
            // 
            // menuHorarios
            // 
            this.menuHorarios.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuHorarios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemEliminar,
            this.itemEditar,
            this.itemAgregar});
            this.menuHorarios.Name = "menuHorarios";
            this.menuHorarios.Size = new System.Drawing.Size(185, 76);
            // 
            // itemEliminar
            // 
            this.itemEliminar.Name = "itemEliminar";
            this.itemEliminar.Size = new System.Drawing.Size(184, 24);
            this.itemEliminar.Text = "Eliminar horario";
            this.itemEliminar.Click += new System.EventHandler(this.EliminarHorario_Click);
            // 
            // itemEditar
            // 
            this.itemEditar.Name = "itemEditar";
            this.itemEditar.Size = new System.Drawing.Size(184, 24);
            this.itemEditar.Text = "Editar horario";
            this.itemEditar.Click += new System.EventHandler(this.EditarHorario_Click);
            // 
            // itemAgregar
            // 
            this.itemAgregar.Name = "itemAgregar";
            this.itemAgregar.Size = new System.Drawing.Size(184, 24);
            this.itemAgregar.Text = "Agregar horario";
            this.itemAgregar.Click += new System.EventHandler(this.BtnAgregarHorario_Click);
            // 
            // lblBuscarHorarios
            // 
            this.lblBuscarHorarios.AutoSize = true;
            this.lblBuscarHorarios.Location = new System.Drawing.Point(42, 36);
            this.lblBuscarHorarios.Name = "lblBuscarHorarios";
            this.lblBuscarHorarios.Size = new System.Drawing.Size(108, 20);
            this.lblBuscarHorarios.TabIndex = 104;
            this.lblBuscarHorarios.Text = "Buscar por:";
            // 
            // cmbCriterioHorarios
            // 
            this.cmbCriterioHorarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriterioHorarios.FormattingEnabled = true;
            this.cmbCriterioHorarios.Items.AddRange(new object[] {
            "Todos",
            "Código",
            "Docente",
            "Curso",
            "Día"});
            this.cmbCriterioHorarios.Location = new System.Drawing.Point(156, 33);
            this.cmbCriterioHorarios.Name = "cmbCriterioHorarios";
            this.cmbCriterioHorarios.Size = new System.Drawing.Size(161, 28);
            this.cmbCriterioHorarios.TabIndex = 105;
            // 
            // txtBuscarHorario
            // 
            this.txtBuscarHorario.Location = new System.Drawing.Point(334, 36);
            this.txtBuscarHorario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscarHorario.Name = "txtBuscarHorario";
            this.txtBuscarHorario.Size = new System.Drawing.Size(505, 27);
            this.txtBuscarHorario.TabIndex = 0;
            this.txtBuscarHorario.TextChanged += new System.EventHandler(this.TxtBuscarHorario_TextChanged);
            // 
            // btnAgregarHorario
            // 
            this.btnAgregarHorario.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAgregarHorario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarHorario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarHorario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgregarHorario.Location = new System.Drawing.Point(1095, 25);
            this.btnAgregarHorario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregarHorario.Name = "btnAgregarHorario";
            this.btnAgregarHorario.Size = new System.Drawing.Size(160, 34);
            this.btnAgregarHorario.TabIndex = 1;
            this.btnAgregarHorario.Text = "➕ Agregar Horario";
            this.btnAgregarHorario.UseVisualStyleBackColor = false;
            this.btnAgregarHorario.Click += new System.EventHandler(this.BtnAgregarHorario_Click);
            // 
            // btnImportarHorarios
            // 
            this.btnImportarHorarios.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnImportarHorarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportarHorarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportarHorarios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImportarHorarios.Location = new System.Drawing.Point(1270, 25);
            this.btnImportarHorarios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImportarHorarios.Name = "btnImportarHorarios";
            this.btnImportarHorarios.Size = new System.Drawing.Size(140, 34);
            this.btnImportarHorarios.TabIndex = 2;
            this.btnImportarHorarios.Text = "Importar CSV";
            this.btnImportarHorarios.UseVisualStyleBackColor = false;
            this.btnImportarHorarios.Click += new System.EventHandler(this.BtnImportarHorarios_Click);
            // 
            // btnExportarHorariosCSV
            // 
            this.btnExportarHorariosCSV.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnExportarHorariosCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarHorariosCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarHorariosCSV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportarHorariosCSV.Location = new System.Drawing.Point(1424, 25);
            this.btnExportarHorariosCSV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportarHorariosCSV.Name = "btnExportarHorariosCSV";
            this.btnExportarHorariosCSV.Size = new System.Drawing.Size(131, 34);
            this.btnExportarHorariosCSV.TabIndex = 3;
            this.btnExportarHorariosCSV.Text = "Exportar CSV";
            this.btnExportarHorariosCSV.UseVisualStyleBackColor = false;
            this.btnExportarHorariosCSV.Click += new System.EventHandler(this.BtnExportarHorariosCSV_Click);
            // 
            // tabPageReporteSemanas
            // 
            this.tabPageReporteSemanas.BackColor = System.Drawing.Color.White;
            this.tabPageReporteSemanas.Controls.Add(this.splitContainerReporte);
            this.tabPageReporteSemanas.ImageIndex = 8;
            this.tabPageReporteSemanas.Location = new System.Drawing.Point(4, 44);
            this.tabPageReporteSemanas.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageReporteSemanas.Name = "tabPageReporteSemanas";
            this.tabPageReporteSemanas.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageReporteSemanas.Size = new System.Drawing.Size(1819, 654);
            this.tabPageReporteSemanas.TabIndex = 4;
            this.tabPageReporteSemanas.Text = "Reporte 16 Semanas";
            // 
            // splitContainerReporte
            // 
            this.splitContainerReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerReporte.Location = new System.Drawing.Point(4, 4);
            this.splitContainerReporte.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerReporte.Name = "splitContainerReporte";
            this.splitContainerReporte.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerReporte.Panel1
            // 
            this.splitContainerReporte.Panel1.Controls.Add(this.dgvReporteResumen);
            this.splitContainerReporte.Panel1.Controls.Add(this.pnlFiltros);
            // 
            // splitContainerReporte.Panel2
            // 
            this.splitContainerReporte.Panel2.Controls.Add(this.dgvReporteDetalle);
            this.splitContainerReporte.Panel2.Controls.Add(this.pnlDetalleTitulo);
            this.splitContainerReporte.Size = new System.Drawing.Size(1811, 646);
            this.splitContainerReporte.SplitterDistance = 150;
            this.splitContainerReporte.SplitterWidth = 5;
            this.splitContainerReporte.TabIndex = 0;
            // 
            // dgvReporteResumen
            // 
            this.dgvReporteResumen.AllowUserToAddRows = false;
            this.dgvReporteResumen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReporteResumen.BackgroundColor = System.Drawing.Color.White;
            this.dgvReporteResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporteResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReporteResumen.Location = new System.Drawing.Point(0, 62);
            this.dgvReporteResumen.Margin = new System.Windows.Forms.Padding(4);
            this.dgvReporteResumen.MultiSelect = false;
            this.dgvReporteResumen.Name = "dgvReporteResumen";
            this.dgvReporteResumen.ReadOnly = true;
            this.dgvReporteResumen.RowHeadersVisible = false;
            this.dgvReporteResumen.RowHeadersWidth = 51;
            this.dgvReporteResumen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporteResumen.Size = new System.Drawing.Size(1811, 88);
            this.dgvReporteResumen.TabIndex = 1;
            this.dgvReporteResumen.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReporteResumen_CellContentClick);
            this.dgvReporteResumen.SelectionChanged += new System.EventHandler(this.DgvReporteResumen_SelectionChanged);
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pnlFiltros.Controls.Add(this.btnExportarReporteResumenCSV);
            this.pnlFiltros.Controls.Add(this.btnExportarReporteDetallePDF);
            this.pnlFiltros.Controls.Add(this.btnExportarReporteResumenPDF);
            this.pnlFiltros.Controls.Add(this.btnGenerarReporteSemanas);
            this.pnlFiltros.Controls.Add(this.btnGestionarCronograma);
            this.pnlFiltros.Controls.Add(this.cmbTipoFiltroReporte);
            this.pnlFiltros.Controls.Add(this.cmbReporteDocente);
            this.pnlFiltros.Controls.Add(this.lblDoc);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 0);
            this.pnlFiltros.Margin = new System.Windows.Forms.Padding(4);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(1811, 62);
            this.pnlFiltros.TabIndex = 0;
            // 
            // btnExportarReporteResumenCSV
            // 
            this.btnExportarReporteResumenCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarReporteResumenCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarReporteResumenCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExportarReporteResumenCSV.Location = new System.Drawing.Point(1646, 16);
            this.btnExportarReporteResumenCSV.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportarReporteResumenCSV.Name = "btnExportarReporteResumenCSV";
            this.btnExportarReporteResumenCSV.Size = new System.Drawing.Size(133, 34);
            this.btnExportarReporteResumenCSV.TabIndex = 7;
            this.btnExportarReporteResumenCSV.Text = "Exportar CSV";
            this.btnExportarReporteResumenCSV.UseVisualStyleBackColor = true;
            this.btnExportarReporteResumenCSV.Click += new System.EventHandler(this.BtnExportarReporteResumenCSV_Click);
            // 
            // btnExportarReporteDetallePDF
            // 
            this.btnExportarReporteDetallePDF.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportarReporteDetallePDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarReporteDetallePDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarReporteDetallePDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportarReporteDetallePDF.Location = new System.Drawing.Point(1421, 16);
            this.btnExportarReporteDetallePDF.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportarReporteDetallePDF.Name = "btnExportarReporteDetallePDF";
            this.btnExportarReporteDetallePDF.Size = new System.Drawing.Size(217, 34);
            this.btnExportarReporteDetallePDF.TabIndex = 6;
            this.btnExportarReporteDetallePDF.Text = "PDF Detallado";
            this.btnExportarReporteDetallePDF.UseVisualStyleBackColor = false;
            this.btnExportarReporteDetallePDF.Click += new System.EventHandler(this.BtnExportarReporteDetallePDF_Click);
            // 
            // btnExportarReporteResumenPDF
            // 
            this.btnExportarReporteResumenPDF.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportarReporteResumenPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarReporteResumenPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarReporteResumenPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportarReporteResumenPDF.Location = new System.Drawing.Point(1214, 16);
            this.btnExportarReporteResumenPDF.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportarReporteResumenPDF.Name = "btnExportarReporteResumenPDF";
            this.btnExportarReporteResumenPDF.Size = new System.Drawing.Size(199, 34);
            this.btnExportarReporteResumenPDF.TabIndex = 5;
            this.btnExportarReporteResumenPDF.Text = "PDF resumen";
            this.btnExportarReporteResumenPDF.UseVisualStyleBackColor = false;
            this.btnExportarReporteResumenPDF.Click += new System.EventHandler(this.BtnExportarReporteResumenPDF_Click);
            // 
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
            // 
            // btnGenerarReporteSemanas
            // 
            this.btnGenerarReporteSemanas.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGenerarReporteSemanas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarReporteSemanas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporteSemanas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGenerarReporteSemanas.Location = new System.Drawing.Point(1054, 16);
            this.btnGenerarReporteSemanas.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerarReporteSemanas.Name = "btnGenerarReporteSemanas";
            this.btnGenerarReporteSemanas.Size = new System.Drawing.Size(147, 34);
            this.btnGenerarReporteSemanas.TabIndex = 4;
            this.btnGenerarReporteSemanas.Text = "Analizar Carga";
            this.btnGenerarReporteSemanas.UseVisualStyleBackColor = false;
            this.btnGenerarReporteSemanas.Click += new System.EventHandler(this.BtnGenerarReporteSemanas_Click);
            // 
            // cmbTipoFiltroReporte
            // 
            this.cmbTipoFiltroReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoFiltroReporte.FormattingEnabled = true;
            this.cmbTipoFiltroReporte.Items.AddRange(new object[] {
            "Nombre",
            "Código"});
            this.cmbTipoFiltroReporte.Location = new System.Drawing.Point(440, 16);
            this.cmbTipoFiltroReporte.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTipoFiltroReporte.Name = "cmbTipoFiltroReporte";
            this.cmbTipoFiltroReporte.Size = new System.Drawing.Size(137, 28);
            this.cmbTipoFiltroReporte.TabIndex = 8;
            this.cmbTipoFiltroReporte.SelectedIndexChanged += new System.EventHandler(this.CmbTipoFiltroReporte_SelectedIndexChanged);
            // 
            // cmbReporteDocente
            // 
            this.cmbReporteDocente.FormattingEnabled = true;
            this.cmbReporteDocente.Location = new System.Drawing.Point(595, 16);
            this.cmbReporteDocente.Margin = new System.Windows.Forms.Padding(4);
            this.cmbReporteDocente.MaxDropDownItems = 5;
            this.cmbReporteDocente.Name = "cmbReporteDocente";
            this.cmbReporteDocente.Size = new System.Drawing.Size(405, 28);
            this.cmbReporteDocente.TabIndex = 3;
            this.cmbReporteDocente.TextUpdate += new System.EventHandler(this.CmbReporteDocente_TextUpdate);
            this.cmbReporteDocente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbReporteDocente_KeyPress);
            // 
            // lblDoc
            // 
            this.lblDoc.AutoSize = true;
            this.lblDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDoc.Location = new System.Drawing.Point(347, 20);
            this.lblDoc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(82, 20);
            this.lblDoc.TabIndex = 2;
            this.lblDoc.Text = "Docente:";
            // 
            // dgvReporteDetalle
            // 
            this.dgvReporteDetalle.AllowUserToAddRows = false;
            this.dgvReporteDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReporteDetalle.BackgroundColor = System.Drawing.Color.White;
            this.dgvReporteDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporteDetalle.Location = new System.Drawing.Point(0, 43);
            this.dgvReporteDetalle.Margin = new System.Windows.Forms.Padding(4);
            this.dgvReporteDetalle.Name = "dgvReporteDetalle";
            this.dgvReporteDetalle.ReadOnly = true;
            this.dgvReporteDetalle.RowHeadersVisible = false;
            this.dgvReporteDetalle.RowHeadersWidth = 51;
            this.dgvReporteDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporteDetalle.Size = new System.Drawing.Size(1807, 564);
            this.dgvReporteDetalle.TabIndex = 1;
            // 
            // pnlDetalleTitulo
            // 
            this.pnlDetalleTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.pnlDetalleTitulo.Controls.Add(this.lblDetalle);
            this.pnlDetalleTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetalleTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlDetalleTitulo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDetalleTitulo.Name = "pnlDetalleTitulo";
            this.pnlDetalleTitulo.Size = new System.Drawing.Size(1811, 46);
            this.pnlDetalleTitulo.TabIndex = 0;
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.lblDetalle.Location = new System.Drawing.Point(20, 10);
            this.lblDetalle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(639, 20);
            this.lblDetalle.TabIndex = 0;
            this.lblDetalle.Text = "Detalle de Clases Semana a Semana (Selecciona un docente/curso arriba)";
            this.lblDetalle.Click += new System.EventHandler(this.lblDetalle_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "editar.png");
            this.imageList1.Images.SetKeyName(1, "lista.png");
            this.imageList1.Images.SetKeyName(2, "libro-de-tapa-negra-cerrado.png");
            this.imageList1.Images.SetKeyName(3, "grupo.png");
            this.imageList1.Images.SetKeyName(4, "anadir.png");
            this.imageList1.Images.SetKeyName(5, "libro-de-tapa-negra-cerrado (1).png");
            this.imageList1.Images.SetKeyName(6, "grupo (1).png");
            this.imageList1.Images.SetKeyName(7, "lista (1).png");
            this.imageList1.Images.SetKeyName(8, "reporte-de-negocios.png");
            this.imageList1.Images.SetKeyName(9, "reloj-de-pared.png");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.ImageIndex = 0;
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1296, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Edición";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.ImageList = this.imageList1;
            this.tabControl2.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControl2.Location = new System.Drawing.Point(6, 4);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1284, 442);
            this.tabControl2.TabIndex = 6;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox4);
            this.tabPage5.Controls.Add(this.groupBox3);
            this.tabPage5.ImageIndex = 4;
            this.tabPage5.Location = new System.Drawing.Point(44, 4);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage5.Size = new System.Drawing.Size(1236, 434);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Añadir";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBox6);
            this.groupBox4.Controls.Add(this.cmbAddTipoCurso);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.btnAñadirCurso);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtAddNombreCurso);
            this.groupBox4.Controls.Add(this.txtAddCodigoCurso);
            this.groupBox4.Controls.Add(this.txtAddCreditosCurso);
            this.groupBox4.Controls.Add(this.cmbAddEscuela);
            this.groupBox4.Controls.Add(this.lblAddEscuela);
            this.groupBox4.Controls.Add(this.cmbAddMalla);
            this.groupBox4.Controls.Add(this.lblAddMalla);
            this.groupBox4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox4.Location = new System.Drawing.Point(676, 33);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(497, 286);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Registrar nueva asignatura";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(391, 62);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(82, 81);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 14;
            this.pictureBox6.TabStop = false;
            // 
            // cmbAddTipoCurso
            // 
            this.cmbAddTipoCurso.FormattingEnabled = true;
            this.cmbAddTipoCurso.Items.AddRange(new object[] {
            "O",
            "E"});
            this.cmbAddTipoCurso.Location = new System.Drawing.Point(175, 133);
            this.cmbAddTipoCurso.Name = "cmbAddTipoCurso";
            this.cmbAddTipoCurso.Size = new System.Drawing.Size(177, 24);
            this.cmbAddTipoCurso.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(31, 133);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 16);
            this.label12.TabIndex = 8;
            this.label12.Text = "Tipo";
            // 
            // btnAñadirCurso
            // 
            this.btnAñadirCurso.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAñadirCurso.Location = new System.Drawing.Point(175, 237);
            this.btnAñadirCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAñadirCurso.Name = "btnAñadirCurso";
            this.btnAñadirCurso.Size = new System.Drawing.Size(148, 34);
            this.btnAñadirCurso.TabIndex = 3;
            this.btnAñadirCurso.Text = "Añadir Asignatura";
            this.btnAñadirCurso.UseVisualStyleBackColor = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(31, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 16);
            this.label13.TabIndex = 7;
            this.label13.Text = "Créditos";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(31, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 16);
            this.label14.TabIndex = 6;
            this.label14.Text = "Código";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(31, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 16);
            this.label15.TabIndex = 5;
            this.label15.Text = "Nombre";
            // 
            // txtAddNombreCurso
            // 
            this.txtAddNombreCurso.Location = new System.Drawing.Point(175, 33);
            this.txtAddNombreCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddNombreCurso.Name = "txtAddNombreCurso";
            this.txtAddNombreCurso.Size = new System.Drawing.Size(177, 22);
            this.txtAddNombreCurso.TabIndex = 0;
            // 
            // txtAddCodigoCurso
            // 
            this.txtAddCodigoCurso.Location = new System.Drawing.Point(175, 62);
            this.txtAddCodigoCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddCodigoCurso.Name = "txtAddCodigoCurso";
            this.txtAddCodigoCurso.Size = new System.Drawing.Size(177, 22);
            this.txtAddCodigoCurso.TabIndex = 1;
            // 
            // txtAddCreditosCurso
            // 
            this.txtAddCreditosCurso.Location = new System.Drawing.Point(175, 98);
            this.txtAddCreditosCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddCreditosCurso.Name = "txtAddCreditosCurso";
            this.txtAddCreditosCurso.Size = new System.Drawing.Size(177, 22);
            this.txtAddCreditosCurso.TabIndex = 2;
            // 
            // cmbAddEscuela
            // 
            this.cmbAddEscuela.FormattingEnabled = true;
            this.cmbAddEscuela.Items.AddRange(new object[] {
            "Computación Científica",
            "Matemática",
            "Investigación Operativa",
            "Estadística"});
            this.cmbAddEscuela.Location = new System.Drawing.Point(175, 165);
            this.cmbAddEscuela.Name = "cmbAddEscuela";
            this.cmbAddEscuela.Size = new System.Drawing.Size(177, 24);
            this.cmbAddEscuela.TabIndex = 15;
            // 
            // lblAddEscuela
            // 
            this.lblAddEscuela.AutoSize = true;
            this.lblAddEscuela.Location = new System.Drawing.Point(31, 165);
            this.lblAddEscuela.Name = "lblAddEscuela";
            this.lblAddEscuela.Size = new System.Drawing.Size(63, 16);
            this.lblAddEscuela.TabIndex = 16;
            this.lblAddEscuela.Text = "Escuela";
            // 
            // cmbAddMalla
            // 
            this.cmbAddMalla.FormattingEnabled = true;
            this.cmbAddMalla.Items.AddRange(new object[] {
            "2018",
            "2026"});
            this.cmbAddMalla.Location = new System.Drawing.Point(175, 195);
            this.cmbAddMalla.Name = "cmbAddMalla";
            this.cmbAddMalla.Size = new System.Drawing.Size(177, 24);
            this.cmbAddMalla.TabIndex = 17;
            // 
            // lblAddMalla
            // 
            this.lblAddMalla.AutoSize = true;
            this.lblAddMalla.Location = new System.Drawing.Point(31, 195);
            this.lblAddMalla.Name = "lblAddMalla";
            this.lblAddMalla.Size = new System.Drawing.Size(115, 16);
            this.lblAddMalla.TabIndex = 18;
            this.lblAddMalla.Text = "Malla Curricular";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.txtAddCorreo);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.cmbAddDepartamento);
            this.groupBox3.Controls.Add(this.cmbAddCategoria);
            this.groupBox3.Controls.Add(this.cmbAddClase);
            this.groupBox3.Controls.Add(this.pictureBox5);
            this.groupBox3.Controls.Add(this.txtAddDNI);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.btnAgregarDocente);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtAddCodigo);
            this.groupBox3.Controls.Add(this.txtAddNombre);
            this.groupBox3.Controls.Add(this.txtAddApellido);
            this.groupBox3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox3.Location = new System.Drawing.Point(53, 33);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(494, 356);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Registrar datos de nuevo docente";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(31, 276);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(54, 16);
            this.label25.TabIndex = 19;
            this.label25.Text = "Correo";
            // 
            // txtAddCorreo
            // 
            this.txtAddCorreo.Location = new System.Drawing.Point(175, 270);
            this.txtAddCorreo.Name = "txtAddCorreo";
            this.txtAddCorreo.Size = new System.Drawing.Size(242, 22);
            this.txtAddCorreo.TabIndex = 11;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(31, 235);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(105, 16);
            this.label24.TabIndex = 18;
            this.label24.Text = "Departamento";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(31, 202);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(75, 16);
            this.label23.TabIndex = 17;
            this.label23.Text = "Categoría";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(31, 168);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(47, 16);
            this.label22.TabIndex = 16;
            this.label22.Text = "Clase";
            // 
            // cmbAddDepartamento
            // 
            this.cmbAddDepartamento.FormattingEnabled = true;
            this.cmbAddDepartamento.Location = new System.Drawing.Point(175, 232);
            this.cmbAddDepartamento.Name = "cmbAddDepartamento";
            this.cmbAddDepartamento.Size = new System.Drawing.Size(177, 24);
            this.cmbAddDepartamento.TabIndex = 15;
            // 
            // cmbAddCategoria
            // 
            this.cmbAddCategoria.FormattingEnabled = true;
            this.cmbAddCategoria.Location = new System.Drawing.Point(175, 199);
            this.cmbAddCategoria.Name = "cmbAddCategoria";
            this.cmbAddCategoria.Size = new System.Drawing.Size(177, 24);
            this.cmbAddCategoria.TabIndex = 14;
            // 
            // cmbAddClase
            // 
            this.cmbAddClase.FormattingEnabled = true;
            this.cmbAddClase.Location = new System.Drawing.Point(175, 165);
            this.cmbAddClase.Name = "cmbAddClase";
            this.cmbAddClase.Size = new System.Drawing.Size(177, 24);
            this.cmbAddClase.TabIndex = 11;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(371, 62);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(82, 81);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 13;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // txtAddDNI
            // 
            this.txtAddDNI.Location = new System.Drawing.Point(175, 131);
            this.txtAddDNI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddDNI.Name = "txtAddDNI";
            this.txtAddDNI.Size = new System.Drawing.Size(177, 22);
            this.txtAddDNI.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "DNI";
            // 
            // btnAgregarDocente
            // 
            this.btnAgregarDocente.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAgregarDocente.Location = new System.Drawing.Point(175, 307);
            this.btnAgregarDocente.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregarDocente.Name = "btnAgregarDocente";
            this.btnAgregarDocente.Size = new System.Drawing.Size(160, 34);
            this.btnAgregarDocente.TabIndex = 3;
            this.btnAgregarDocente.Text = "Añadir Docente";
            this.btnAgregarDocente.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Apellidos";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "Nombres";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 16);
            this.label10.TabIndex = 5;
            this.label10.Text = "Código";
            // 
            // txtAddCodigo
            // 
            this.txtAddCodigo.Location = new System.Drawing.Point(175, 33);
            this.txtAddCodigo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddCodigo.Name = "txtAddCodigo";
            this.txtAddCodigo.Size = new System.Drawing.Size(177, 22);
            this.txtAddCodigo.TabIndex = 0;
            // 
            // txtAddNombre
            // 
            this.txtAddNombre.Location = new System.Drawing.Point(175, 62);
            this.txtAddNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddNombre.Name = "txtAddNombre";
            this.txtAddNombre.Size = new System.Drawing.Size(177, 22);
            this.txtAddNombre.TabIndex = 1;
            // 
            // txtAddApellido
            // 
            this.txtAddApellido.Location = new System.Drawing.Point(175, 98);
            this.txtAddApellido.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddApellido.Name = "txtAddApellido";
            this.txtAddApellido.Size = new System.Drawing.Size(177, 22);
            this.txtAddApellido.TabIndex = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox5.Controls.Add(this.pictureBox4);
            this.groupBox5.Controls.Add(this.btnGuardarEdicionCurso);
            this.groupBox5.Controls.Add(this.txtNuevoTipoCurso);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.txtEditNombreCurso);
            this.groupBox5.Controls.Add(this.txtEditCodigoCurso);
            this.groupBox5.Controls.Add(this.txtEditCreditosCurso);
            this.groupBox5.Controls.Add(this.cmbEditEscuela);
            this.groupBox5.Controls.Add(this.lblEditEscuela);
            this.groupBox5.Controls.Add(this.cmbEditMalla);
            this.groupBox5.Controls.Add(this.lblEditMalla);
            this.groupBox5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox5.Location = new System.Drawing.Point(643, 98);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(471, 286);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de la asignatura";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(370, 62);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(82, 81);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // btnGuardarEdicionCurso
            // 
            this.btnGuardarEdicionCurso.BackColor = System.Drawing.Color.Lime;
            this.btnGuardarEdicionCurso.Location = new System.Drawing.Point(175, 237);
            this.btnGuardarEdicionCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuardarEdicionCurso.Name = "btnGuardarEdicionCurso";
            this.btnGuardarEdicionCurso.Size = new System.Drawing.Size(138, 34);
            this.btnGuardarEdicionCurso.TabIndex = 10;
            this.btnGuardarEdicionCurso.Text = "Guardar cambios";
            this.btnGuardarEdicionCurso.UseVisualStyleBackColor = false;
            // 
            // txtNuevoTipoCurso
            // 
            this.txtNuevoTipoCurso.FormattingEnabled = true;
            this.txtNuevoTipoCurso.Items.AddRange(new object[] {
            "O",
            "E"});
            this.txtNuevoTipoCurso.Location = new System.Drawing.Point(175, 133);
            this.txtNuevoTipoCurso.Name = "txtNuevoTipoCurso";
            this.txtNuevoTipoCurso.Size = new System.Drawing.Size(177, 24);
            this.txtNuevoTipoCurso.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 16);
            this.label11.TabIndex = 8;
            this.label11.Text = "Tipo";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(31, 98);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 16);
            this.label16.TabIndex = 7;
            this.label16.Text = "Créditos";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(31, 64);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(51, 16);
            this.label17.TabIndex = 6;
            this.label17.Text = "Código";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(31, 33);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 16);
            this.label18.TabIndex = 5;
            this.label18.Text = "Nombre";
            // 
            // txtEditNombreCurso
            // 
            this.txtEditNombreCurso.Location = new System.Drawing.Point(175, 33);
            this.txtEditNombreCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEditNombreCurso.Name = "txtEditNombreCurso";
            this.txtEditNombreCurso.Size = new System.Drawing.Size(177, 22);
            this.txtEditNombreCurso.TabIndex = 0;
            // 
            // txtEditCodigoCurso
            // 
            this.txtEditCodigoCurso.Location = new System.Drawing.Point(175, 62);
            this.txtEditCodigoCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEditCodigoCurso.Name = "txtEditCodigoCurso";
            this.txtEditCodigoCurso.Size = new System.Drawing.Size(177, 22);
            this.txtEditCodigoCurso.TabIndex = 1;
            // 
            // txtEditCreditosCurso
            // 
            this.txtEditCreditosCurso.Location = new System.Drawing.Point(175, 98);
            this.txtEditCreditosCurso.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEditCreditosCurso.Name = "txtEditCreditosCurso";
            this.txtEditCreditosCurso.Size = new System.Drawing.Size(177, 22);
            this.txtEditCreditosCurso.TabIndex = 2;
            // 
            // cmbEditEscuela
            // 
            this.cmbEditEscuela.FormattingEnabled = true;
            this.cmbEditEscuela.Items.AddRange(new object[] {
            "Computación Científica",
            "Matemática",
            "Investigación Operativa",
            "Estadística"});
            this.cmbEditEscuela.Location = new System.Drawing.Point(175, 165);
            this.cmbEditEscuela.Name = "cmbEditEscuela";
            this.cmbEditEscuela.Size = new System.Drawing.Size(177, 24);
            this.cmbEditEscuela.TabIndex = 14;
            // 
            // lblEditEscuela
            // 
            this.lblEditEscuela.AutoSize = true;
            this.lblEditEscuela.Location = new System.Drawing.Point(31, 165);
            this.lblEditEscuela.Name = "lblEditEscuela";
            this.lblEditEscuela.Size = new System.Drawing.Size(56, 16);
            this.lblEditEscuela.TabIndex = 15;
            this.lblEditEscuela.Text = "Escuela";
            // 
            // cmbEditMalla
            // 
            this.cmbEditMalla.FormattingEnabled = true;
            this.cmbEditMalla.Items.AddRange(new object[] {
            "2018",
            "2026"});
            this.cmbEditMalla.Location = new System.Drawing.Point(175, 195);
            this.cmbEditMalla.Name = "cmbEditMalla";
            this.cmbEditMalla.Size = new System.Drawing.Size(177, 24);
            this.cmbEditMalla.TabIndex = 16;
            // 
            // lblEditMalla
            // 
            this.lblEditMalla.AutoSize = true;
            this.lblEditMalla.Location = new System.Drawing.Point(31, 195);
            this.lblEditMalla.Name = "lblEditMalla";
            this.lblEditMalla.Size = new System.Drawing.Size(99, 16);
            this.lblEditMalla.TabIndex = 17;
            this.lblEditMalla.Text = "Malla Curricular";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.pictureBox3);
            this.groupBox2.Controls.Add(this.txtNuevoCorreo);
            this.groupBox2.Controls.Add(this.txtNuevoDNI);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.btnEditarDocente);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbNuevoDepartamento);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbNuevoCategoria);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbNuevoClase);
            this.groupBox2.Controls.Add(this.txtNuevoCodigo);
            this.groupBox2.Controls.Add(this.txtNuevoNombre);
            this.groupBox2.Controls.Add(this.txtNuevoApellido);
            this.groupBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox2.Location = new System.Drawing.Point(19, 57);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(481, 352);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información del docente";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(31, 271);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 16);
            this.label26.TabIndex = 27;
            this.label26.Text = "Correo";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(373, 50);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(82, 81);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // txtNuevoCorreo
            // 
            this.txtNuevoCorreo.Location = new System.Drawing.Point(175, 265);
            this.txtNuevoCorreo.Name = "txtNuevoCorreo";
            this.txtNuevoCorreo.Size = new System.Drawing.Size(242, 22);
            this.txtNuevoCorreo.TabIndex = 20;
            // 
            // txtNuevoDNI
            // 
            this.txtNuevoDNI.Location = new System.Drawing.Point(175, 127);
            this.txtNuevoDNI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNuevoDNI.Name = "txtNuevoDNI";
            this.txtNuevoDNI.Size = new System.Drawing.Size(177, 22);
            this.txtNuevoDNI.TabIndex = 9;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(31, 230);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(93, 16);
            this.label27.TabIndex = 26;
            this.label27.Text = "Departamento";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "DNI";
            this.label6.Click += new System.EventHandler(this.label6_Click_1);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(31, 197);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(66, 16);
            this.label28.TabIndex = 25;
            this.label28.Text = "Categoría";
            // 
            // btnEditarDocente
            // 
            this.btnEditarDocente.BackColor = System.Drawing.Color.Lime;
            this.btnEditarDocente.Location = new System.Drawing.Point(175, 298);
            this.btnEditarDocente.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditarDocente.Name = "btnEditarDocente";
            this.btnEditarDocente.Size = new System.Drawing.Size(139, 34);
            this.btnEditarDocente.TabIndex = 3;
            this.btnEditarDocente.Text = "Guardar Cambios";
            this.btnEditarDocente.UseVisualStyleBackColor = false;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(31, 163);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(42, 16);
            this.label29.TabIndex = 24;
            this.label29.Text = "Clase";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Apellidos";
            // 
            // cmbNuevoDepartamento
            // 
            this.cmbNuevoDepartamento.FormattingEnabled = true;
            this.cmbNuevoDepartamento.Location = new System.Drawing.Point(175, 227);
            this.cmbNuevoDepartamento.Name = "cmbNuevoDepartamento";
            this.cmbNuevoDepartamento.Size = new System.Drawing.Size(177, 24);
            this.cmbNuevoDepartamento.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nombres";
            // 
            // cmbNuevoCategoria
            // 
            this.cmbNuevoCategoria.FormattingEnabled = true;
            this.cmbNuevoCategoria.Location = new System.Drawing.Point(175, 194);
            this.cmbNuevoCategoria.Name = "cmbNuevoCategoria";
            this.cmbNuevoCategoria.Size = new System.Drawing.Size(177, 24);
            this.cmbNuevoCategoria.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Código";
            // 
            // cmbNuevoClase
            // 
            this.cmbNuevoClase.FormattingEnabled = true;
            this.cmbNuevoClase.Location = new System.Drawing.Point(175, 160);
            this.cmbNuevoClase.Name = "cmbNuevoClase";
            this.cmbNuevoClase.Size = new System.Drawing.Size(177, 24);
            this.cmbNuevoClase.TabIndex = 21;
            // 
            // txtNuevoCodigo
            // 
            this.txtNuevoCodigo.Location = new System.Drawing.Point(175, 33);
            this.txtNuevoCodigo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNuevoCodigo.Name = "txtNuevoCodigo";
            this.txtNuevoCodigo.Size = new System.Drawing.Size(177, 22);
            this.txtNuevoCodigo.TabIndex = 0;
            this.txtNuevoCodigo.TextChanged += new System.EventHandler(this.txtNuevoCodigo_TextChanged);
            // 
            // txtNuevoNombre
            // 
            this.txtNuevoNombre.Location = new System.Drawing.Point(175, 62);
            this.txtNuevoNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNuevoNombre.Name = "txtNuevoNombre";
            this.txtNuevoNombre.Size = new System.Drawing.Size(177, 22);
            this.txtNuevoNombre.TabIndex = 1;
            // 
            // txtNuevoApellido
            // 
            this.txtNuevoApellido.Location = new System.Drawing.Point(175, 95);
            this.txtNuevoApellido.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNuevoApellido.Name = "txtNuevoApellido";
            this.txtNuevoApellido.Size = new System.Drawing.Size(177, 22);
            this.txtNuevoApellido.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 29.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(69, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(477, 57);
            this.label2.TabIndex = 19;
            this.label2.Text = "Panel de Administrador";
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.LightCoral;
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.Location = new System.Drawing.Point(1707, 17);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(147, 37);
            this.btnCerrarSesion.TabIndex = 23;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1881, 761);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormAdmin";
            this.Text = "Panel de Administrador";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocentes)).EndInit();
            this.menuOpciones.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCursos)).EndInit();
            this.menuCursos.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorarios)).EndInit();
            this.menuHorarios.ResumeLayout(false);
            this.tabPageReporteSemanas.ResumeLayout(false);
            this.splitContainerReporte.Panel1.ResumeLayout(false);
            this.splitContainerReporte.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReporte)).EndInit();
            this.splitContainerReporte.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteResumen)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteDetalle)).EndInit();
            this.pnlDetalleTitulo.ResumeLayout(false);
            this.pnlDetalleTitulo.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        	}
#endregion

        private IContainer components = null;
        private TabPage tabPageReporteSemanas;
        private System.Windows.Forms.ComboBox cmbTipoFiltroReporte;
        private ComboBox cmbReporteDocente;
        private Button btnGenerarReporteSemanas;
        private System.Windows.Forms.Button btnGestionarCronograma;
        private Button btnExportarReporteResumenPDF;
        private Button btnExportarReporteDetallePDF;
        private Button btnExportarReporteResumenCSV;
        private DataGridView dgvReporteResumen;
        private DataGridView dgvReporteDetalle;
        private SplitContainer splitContainerReporte;
        private Panel pnlFiltros;
        private Label lblDoc;
        private Panel pnlDetalleTitulo;
        private Label lblDetalle;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dgvDocentes;
        private Label lblBuscarDocentes;
        private ComboBox cmbCriterioDocentes;
        private Label lblBuscarCursos;
        private ComboBox cmbCriterioCursos;
        private Label lblBuscarHorarios;
        private ComboBox cmbCriterioHorarios;
        private Label lblBuscarHistorial;
        private ComboBox cmbCriterioHistorial;
        private TextBox txtBuscar;
        private TabPage tabPage3;
        private ContextMenuStrip menuOpciones;
        private TextBox txtNuevoCodigo;
        private ContextMenuStrip menuCursos;
        private Button btnEditarDocente;
        private TextBox txtNuevoApellido;
        private TextBox txtNuevoNombre;
        private DataGridView dgvHistorial;
        private ToolStripMenuItem eliminardocente;
        private MonthCalendar monthCalendarHistorial;
        private TextBox txtBuscarHistorialNombre;
        private Label label2;
        private GroupBox groupBox2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label6;
        private TextBox txtNuevoDNI;
        private ToolStripMenuItem editarDocenteToolStripMenuItem;
        private ToolStripMenuItem añadirDocenteToolStripMenuItem;
        private Button btnExportarPDF;
        private Button btnExportarExcel;
        private Button btnEliminarTodo;
        private Button btnExportarDocentesExcel;
        private Button btnExportarDocentesPDF;
        private Button btnImportarExcel;
        private Button btnCerrarSesion;
        private PictureBox pictureBox1;
        private TabPage tabPage6;
        private DataGridView dgvCursos;
        private TextBox txtNuevoNombreCurso;
        private GroupBox groupBox5;
        private ComboBox txtNuevoTipoCurso;
        private Label label11;
        private Label label16;
        private Label label17;
        private Label label18;
        private TextBox txtEditNombreCurso;
        private TextBox txtEditCodigoCurso;
        private TextBox txtEditCreditosCurso;
        private Button btnGuardarEdicionCurso;
        private ToolStripMenuItem eliminarCursoToolStripMenuItem;
        private ToolStripMenuItem editarCursoToolStripMenuItem;
        private ToolStripMenuItem añadirAsignaturaToolStripMenuItem;
        private ImageList imageList1;
        private Button btnImportarCursos;
        private Button btnExportarCursosExcel;
        private Button btnExportarCursosPDF;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private Label label26;
        private TextBox txtNuevoCorreo;
        private Label label27;
        private Label label28;
        private Label label29;
        private ComboBox cmbNuevoDepartamento;
        private ComboBox cmbNuevoCategoria;
        private ComboBox cmbNuevoClase;
        private TabControl tabControl2;
        private TabPage tabPage5;
        private GroupBox groupBox4;
        private PictureBox pictureBox6;
        private ComboBox cmbAddTipoCurso;
        private Label label12;
        private Button btnAñadirCurso;
        private Label label13;
        private Label label14;
        private Label label15;
        private TextBox txtAddNombreCurso;
        private TextBox txtAddCodigoCurso;
        private TextBox txtAddCreditosCurso;
        private GroupBox groupBox3;
        private Label label25;
        private TextBox txtAddCorreo;
        private Label label24;
        private Label label23;
        private Label label22;
        private ComboBox cmbAddDepartamento;
        private ComboBox cmbAddCategoria;
        private ComboBox cmbAddClase;
        private PictureBox pictureBox5;
        private TextBox txtAddDNI;
        private Label label7;
        private Button btnAgregarDocente;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox txtAddCodigo;
        private TextBox txtAddNombre;
        private TextBox txtAddApellido;
        private Button Periodo;
        private TabPage tabPage4;
        private DataGridView dgvHorarios;
        private TextBox txtBuscarHorario;
        private ContextMenuStrip menuHorarios;
        private Button btnAgregarHorario;
        private Button btnImportarHorarios;
        private Button btnExportarHorariosCSV;
        private ToolStripMenuItem itemEliminar;
        private ToolStripMenuItem itemEditar;
        private ToolStripMenuItem itemAgregar;
        private Label lblAddEscuela;
        private ComboBox cmbAddEscuela;
        private Label lblAddMalla;
        private ComboBox cmbAddMalla;
        private Label lblEditEscuela;
        private ComboBox cmbEditEscuela;
        private Label lblEditMalla;
        private ComboBox cmbEditMalla;
        private Button btnConfigLimites;
    }
}
