using System;
using System.Data.SqlClient;

class Program {
    static void Main() {
        string connStr = @""Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Alejandro\Downloads\Proyecto_Asistencia_FCM\Codigo_Fuente\BD\ControlAsistencia.mdf;Integrated Security=True"";
        using(SqlConnection con = new SqlConnection(connStr)) {
            con.Open();
            SqlCommand cmd = new SqlCommand(""SELECT Id, Apellido, Nombre FROM Docentes"", con);
            SqlDataReader r = cmd.ExecuteReader();
            int totalSchedules = 0;
            while(r.Read()) {
                int id = (int)r[""Id""];
                SqlCommand cmd2 = new SqlCommand(""SELECT COUNT(*) FROM HorarioClases WHERE DocenteId = "" + id, con);
                int count = (int)cmd2.ExecuteScalar();
                if (count > 0) {
                    Console.WriteLine(r[""Apellido""] + "", "" + r[""Nombre""] + "" has "" + count + "" schedules."");
                    totalSchedules += count;
                }
            }
            r.Close();
            if (totalSchedules == 0) Console.WriteLine(""No schedules found for ANY teacher!"");
        }
    }
}
