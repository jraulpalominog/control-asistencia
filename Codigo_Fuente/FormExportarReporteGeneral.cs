using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Diálogo para configurar y exportar el reporte general de asistencias.
    /// </summary>
    public partial class FormExportarReporteGeneral : Form
    {
        // Propiedades de salida
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public bool IncluirFaltas { get; private set; }
        public bool ExportarCSV { get; private set; }
        public string TituloReporte { get; private set; }
        public string NombreArchivoDefecto { get; private set; }

        public FormExportarReporteGeneral()
        {
            InitializeComponent();
            ActualizarLabelSemana();
        }

        private void ActualizarLabelSemana()
        {
            DateTime baseDate = dtpSemana.Value.Date;
            int diff = (7 + (baseDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime start = baseDate.AddDays(-1 * diff);
            DateTime end = start.AddDays(6);
            lblRangoSemana.Text = $"Rango: Lunes {start:dd/MM/yyyy} al Domingo {end:dd/MM/yyyy}";
        }

        private void rbDia_CheckedChanged(object sender, EventArgs e)
        {
            dtpDia.Enabled = rbDia.Checked;
        }

        private void rbSemana_CheckedChanged(object sender, EventArgs e)
        {
            dtpSemana.Enabled = rbSemana.Checked;
            lblRangoSemana.ForeColor = rbSemana.Checked ? Color.Black : Color.DarkGray;
        }

        private void rbMes_CheckedChanged(object sender, EventArgs e)
        {
            dtpMes.Enabled = rbMes.Checked;
        }

        private void dtpSemana_ValueChanged(object sender, EventArgs e)
        {
            ActualizarLabelSemana();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            DateTime inicio = DateTime.Today;
            DateTime finLimit = DateTime.Today;
            string titulo = "";
            string nombreArchivo = "";

            if (rbDia.Checked)
            {
                DateTime diaSel = dtpDia.Value.Date;
                inicio = diaSel;
                finLimit = diaSel.AddDays(1);
                titulo = $"REPORTE DE ASISTENCIAS - DÍA {diaSel:dd/MM/yyyy}";
                nombreArchivo = $"Asistencia_Dia_{diaSel:yyyyMMdd}";
            }
            else if (rbSemana.Checked)
            {
                DateTime baseDate = dtpSemana.Value.Date;
                int diff = (7 + (baseDate.DayOfWeek - DayOfWeek.Monday)) % 7;
                DateTime start = baseDate.AddDays(-1 * diff);
                DateTime end = start.AddDays(6);
                inicio = start;
                finLimit = end.AddDays(1);
                titulo = $"REPORTE DE ASISTENCIAS - SEMANA DEL {start:dd/MM/yyyy} AL {end:dd/MM/yyyy}";
                nombreArchivo = $"Asistencia_Semana_{start:yyyyMMdd}_al_{end:yyyyMMdd}";
            }
            else if (rbMes.Checked)
            {
                DateTime mesSel = dtpMes.Value;
                inicio = new DateTime(mesSel.Year, mesSel.Month, 1);
                finLimit = inicio.AddMonths(1);
                string[] meses = { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO",
                                   "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
                string nombreMes = meses[mesSel.Month - 1];
                titulo = $"REPORTE DE ASISTENCIAS - MES DE {nombreMes} {mesSel.Year}";
                nombreArchivo = $"Asistencia_Mes_{mesSel:yyyyMM}";
            }

            FechaInicio = inicio;
            FechaFin = finLimit;
            IncluirFaltas = rbIncluirFaltas.Checked;
            ExportarCSV = cmbFormato.SelectedIndex == 1;
            TituloReporte = titulo;
            NombreArchivoDefecto = nombreArchivo;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
