using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public class FormEditarCurso : Form
    {
        private int idCurso;
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
        
        private TextBox txtCodigo;
        private TextBox txtNombre;
        private TextBox txtCreditos;
        private ComboBox cmbTipo;
        private ComboBox cmbEscuela;
        private ComboBox cmbMalla;
        private Button btnGuardar;
        private ErrorProvider epValidacion;

        public FormEditarCurso(int id, string codigo, string nombre, string creditos, string tipo, string escuela = "", string malla = "")
        {
            this.idCurso = id;
            InitializeComponent();
            
            txtCodigo.Text = codigo;
            txtNombre.Text = nombre;
            txtCreditos.Text = creditos;
            cmbTipo.Text = tipo;
            cmbEscuela.Text = escuela;
            cmbMalla.Text = malla;
        }

        private void InitializeComponent()
        {
            this.Text = idCurso == 0 ? "Añadir Asignatura" : "Editar Asignatura";
            this.Size = new Size(380, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            epValidacion = new ErrorProvider();
            epValidacion.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            int y = 30;
            int xLabel = 20;
            int xControl = 130;

            void AddRow(string labelText, Control control)
            {
                Label lbl = new Label() { Text = labelText, Location = new Point(xLabel, y + 3), AutoSize = true };
                control.Location = new Point(xControl, y);
                control.Width = 200;
                this.Controls.Add(lbl);
                this.Controls.Add(control);
                y += 45;
            }

            txtCodigo = new TextBox() { CharacterCasing = CharacterCasing.Upper };
            txtCodigo.TextChanged += ValidarCodigo;
            AddRow("Código:", txtCodigo);

            txtNombre = new TextBox() { CharacterCasing = CharacterCasing.Upper };
            AddRow("Nombre:", txtNombre);

            txtCreditos = new TextBox();
            txtCreditos.TextChanged += ValidarCreditos;
            AddRow("Créditos:", txtCreditos);

            cmbTipo = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTipo.Items.AddRange(new string[] { "O", "E" });
            AddRow("Tipo (O/E):", cmbTipo);

            cmbEscuela = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDown };
            cmbEscuela.Items.AddRange(new string[] { "Computación Científica", "Matemática", "Investigación Operativa", "Estadística" });
            AddRow("Escuela:", cmbEscuela);

            cmbMalla = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDown };
            cmbMalla.Items.AddRange(new string[] { "2018", "2026" });
            AddRow("Malla Curricular:", cmbMalla);

            btnGuardar = new Button() { Text = idCurso == 0 ? "Añadir Registro" : "Guardar Cambios", Location = new Point(130, y), Width = 150, Height = 35 };
            btnGuardar.BackColor = Color.FromArgb(0, 122, 204);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);
        }

        private void ValidarCodigo(object sender, EventArgs e)
        {
            string val = txtCodigo.Text.Trim();
            if (val.Length > 0 && (val.Length > 6 || !Regex.IsMatch(val, "^[A-Z0-9]*$")))
                epValidacion.SetError(txtCodigo, "Requerido: Hasta 6 caracteres (sólo mayúsculas o números).");
            else
                epValidacion.SetError(txtCodigo, "");
        }

        private void ValidarCreditos(object sender, EventArgs e)
        {
            string val = txtCreditos.Text.Trim();
            if (val.Length > 0)
            {
                if (int.TryParse(val, out int cred) && (cred == 2 || cred == 3 || cred == 4 || cred == 5 || cred == 6))
                    epValidacion.SetError(txtCreditos, "");
                else
                    epValidacion.SetError(txtCreditos, "Requerido: Sólo se admiten valores de 2, 3, 4, 5 o 6.");
            }
            else
            {
                epValidacion.SetError(txtCreditos, "");
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string creditosStr = txtCreditos.Text.Trim();
            string tipo = cmbTipo.Text.Trim();
            string escuela = cmbEscuela.Text.Trim();
            string malla = cmbMalla.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(codigo) || 
                string.IsNullOrWhiteSpace(creditosStr) || string.IsNullOrWhiteSpace(tipo) || 
                string.IsNullOrWhiteSpace(escuela) || string.IsNullOrWhiteSpace(malla))
            {
                MessageBox.Show("Todos los campos (Código, Nombre, Créditos, Tipo, Escuela, Malla) son obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (codigo.Length > 6 || !Regex.IsMatch(codigo, "^[A-Z0-9]*$"))
            {
                MessageBox.Show("El código de asignatura debe tener hasta 6 caracteres (letras mayúsculas y números).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int creditos = 0;
            if (!string.IsNullOrWhiteSpace(creditosStr))
            {
                int.TryParse(creditosStr, out creditos);
                if (creditos != 2 && creditos != 3 && creditos != 4 && creditos != 5 && creditos != 6)
                {
                    MessageBox.Show("Los créditos disponibles son 2, 3, 4, 5, 6.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = idCurso == 0
                        ? "INSERT INTO Cursos (Codigo, Nombre, Creditos, Tipo, Escuela, MallaCurricular) VALUES (@C, @N, @Cred, @T, @Esc, @Mall)"
                        : "UPDATE Cursos SET Codigo = @C, Nombre = @N, Creditos = @Cred, Tipo = @T, Escuela = @Esc, MallaCurricular = @Mall WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@C", codigo);
                        cmd.Parameters.AddWithValue("@N", nombre);
                        cmd.Parameters.AddWithValue("@Cred", creditos > 0 ? (object)creditos : DBNull.Value);
                        cmd.Parameters.AddWithValue("@T", tipo.ToUpper());
                        cmd.Parameters.AddWithValue("@Esc", string.IsNullOrWhiteSpace(escuela) ? DBNull.Value : (object)escuela);
                        cmd.Parameters.AddWithValue("@Mall", string.IsNullOrWhiteSpace(malla) ? DBNull.Value : (object)malla);
                        if (idCurso != 0) cmd.Parameters.AddWithValue("@Id", idCurso);

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show(idCurso == 0 ? "Asignatura añadida exitosamente." : "Asignatura actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
