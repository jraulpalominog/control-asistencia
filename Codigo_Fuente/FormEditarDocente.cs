using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public class FormEditarDocente : Form
    {
        private int idDocente;
        private string connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
        
        private TextBox txtCodigo;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtDNI;
        private ComboBox cmbClase;
        private ComboBox cmbCategoria;
        private ComboBox cmbDepartamento;
        private TextBox txtCorreo;
        private Button btnGuardar;
        private ErrorProvider epValidacion;

        public FormEditarDocente(int id, string codigo, string nombre, string apellido, string dni, string clase, string categoria, string departamento, string correo)
        {
            this.idDocente = id;
            InitializeComponent();
            
            txtCodigo.Text = codigo;
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtDNI.Text = dni;
            cmbClase.Text = clase;
            cmbCategoria.Text = categoria;
            cmbDepartamento.Text = departamento;
            txtCorreo.Text = correo;
        }

        private void InitializeComponent()
        {
            this.Text = idDocente == 0 ? "Añadir Docente" : "Editar Docente";
            this.Size = new Size(380, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            epValidacion = new ErrorProvider();
            epValidacion.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            int y = 20;
            int xLabel = 20;
            int xControl = 130;

            void AddRow(string labelText, Control control)
            {
                Label lbl = new Label() { Text = labelText, Location = new Point(xLabel, y + 3), AutoSize = true };
                control.Location = new Point(xControl, y);
                control.Width = 200;
                this.Controls.Add(lbl);
                this.Controls.Add(control);
                y += 40;
            }

            txtCodigo = new TextBox() { CharacterCasing = CharacterCasing.Upper };
            txtCodigo.TextChanged += ValidarCodigo;
            AddRow("Código:", txtCodigo);

            txtNombre = new TextBox();
            AddRow("Nombre:", txtNombre);

            txtApellido = new TextBox();
            AddRow("Apellido:", txtApellido);

            txtDNI = new TextBox();
            txtDNI.TextChanged += ValidarDNI;
            AddRow("DNI:", txtDNI);

            cmbClase = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            cmbClase.Items.AddRange(new string[] { "TP 20 hrs.", "TP 40 hrs." });
            AddRow("Clase:", cmbClase);

            cmbCategoria = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategoria.Items.AddRange(new string[] { "Principal", "Asociado", "Auxiliar", "Jefe de Prácticas" });
            AddRow("Categoría:", cmbCategoria);

            cmbDepartamento = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            cmbDepartamento.Items.AddRange(new string[] { "Matemática", "Computación Científica", "Estadística", "Investigación Operativa" });
            AddRow("Departamento:", cmbDepartamento);

            txtCorreo = new TextBox();
            AddRow("Correo:", txtCorreo);

            btnGuardar = new Button() { Text = idDocente == 0 ? "Añadir Registro" : "Guardar Cambios", Location = new Point(130, y), Width = 150, Height = 35 };
            btnGuardar.BackColor = Color.FromArgb(0, 122, 204);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);
        }

        private void ValidarCodigo(object sender, EventArgs e)
        {
            string val = txtCodigo.Text.Trim();
            if (val.Length != 6 || !Regex.IsMatch(val, "^[A-Z0-9]+$"))
                epValidacion.SetError(txtCodigo, "Requerido: 6 caracteres exactos (sólo mayúsculas o números).");
            else
                epValidacion.SetError(txtCodigo, "");
        }

        private void ValidarDNI(object sender, EventArgs e)
        {
            string val = txtDNI.Text.Trim();
            if (val.Length != 8 || !Regex.IsMatch(val, "^[0-9]+$"))
                epValidacion.SetError(txtDNI, "Requerido: 8 dígitos numéricos exactos.");
            else
                epValidacion.SetError(txtDNI, "");
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string dni = txtDNI.Text.Trim();
            string clase = cmbClase.Text;
            string categoria = cmbCategoria.Text;
            string departamento = cmbDepartamento.Text;
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || 
                string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(dni) || 
                string.IsNullOrWhiteSpace(clase) || string.IsNullOrWhiteSpace(categoria) || 
                string.IsNullOrWhiteSpace(departamento) || string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Todos los campos (Código, Nombre, Apellidos, DNI, Clase, Categoría, Departamento, Correo) son obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (codigo.Length != 6 || !Regex.IsMatch(codigo, "^[A-Z0-9]+$"))
            {
                MessageBox.Show("El código debe ser de 6 caracteres (mayúsculas y números).", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dni.Length != 8 || !Regex.IsMatch(dni, "^[0-9]+$"))
            {
                MessageBox.Show("El DNI debe contener exactamente 8 números.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = idDocente == 0 
                        ? "INSERT INTO Docentes (Codigo, Nombre, Apellido, DNI, Clase, Categoria, Departamento, Correo) VALUES (@C, @N, @A, @DNI, @Clase, @Cat, @Dep, @Correo)"
                        : "UPDATE Docentes SET Codigo = @C, Nombre = @N, Apellido = @A, DNI = @DNI, Clase = @Clase, Categoria = @Cat, Departamento = @Dep, Correo = @Correo WHERE Id = @Id";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@C", codigo);
                        cmd.Parameters.AddWithValue("@N", nombre);
                        cmd.Parameters.AddWithValue("@A", apellido);
                        cmd.Parameters.AddWithValue("@DNI", dni);
                        cmd.Parameters.AddWithValue("@Clase", string.IsNullOrEmpty(clase) ? (object)DBNull.Value : clase);
                        cmd.Parameters.AddWithValue("@Cat", string.IsNullOrEmpty(categoria) ? (object)DBNull.Value : categoria);
                        cmd.Parameters.AddWithValue("@Dep", string.IsNullOrEmpty(departamento) ? (object)DBNull.Value : departamento);
                        cmd.Parameters.AddWithValue("@Correo", string.IsNullOrEmpty(correo) ? (object)DBNull.Value : correo);
                        if (idDocente != 0) cmd.Parameters.AddWithValue("@Id", idDocente);

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show(idDocente == 0 ? "Docente añadido exitosamente." : "Docente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
