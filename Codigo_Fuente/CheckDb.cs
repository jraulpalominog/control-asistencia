using System;
using System.Data.SqlClient;
using System.Data;

class Program {
    static void Main() {
        string connectionString = @""Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Alejandro\Downloads\Proyecto_Asistencia_FCM\Codigo_Fuente\BD\ControlAsistencia.mdf;Integrated Security=True"";
        using(SqlConnection con = new SqlConnection(connectionString)) {
            con.Open();
            SqlCommand cmd = new SqlCommand(""SELECT Id, Apellido, Nombre FROM Docentes WHERE Apellido LIKE '%TURING%'"", con);
            SqlDataReader r = cmd.ExecuteReader();
            int docId = -1;
            while(r.Read()) {
                Console.WriteLine($""Found: {r[""Id""]} - {r[""Apellido""]}, {r[""Nombre""]}"");
                docId = (int)r[""Id""];
            }
            r.Close();
            
            if(docId != -1) {
                SqlCommand cmd2 = new SqlCommand(""SELECT COUNT(*) FROM HorarioClases WHERE DocenteId = "" + docId, con);
                Console.WriteLine(""Schedules: "" + cmd2.ExecuteScalar());
            }
        }
    }
}
