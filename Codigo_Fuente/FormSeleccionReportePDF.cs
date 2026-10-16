using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Diálogo para seleccionar el tipo de reporte PDF a exportar.
    /// </summary>
    public partial class FormSeleccionReportePDF : Form
    {
        /// <summary>Resultado: 1 = General, 2 = Individual, 0 = Cancelado</summary>
        public int TipoSeleccionado { get; private set; } = 0;

        public FormSeleccionReportePDF()
        {
            InitializeComponent();
        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            TipoSeleccionado = 1;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnIndividual_Click(object sender, EventArgs e)
        {
            TipoSeleccionado = 2;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
