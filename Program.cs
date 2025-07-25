using System;
using System.Data.SqlClient;
//Julio Ruiz Matricula:2024-2009
namespace CitasMedicasApp
{
    public class Cita
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
    }

    public class HistoriaMedica
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string Descripcion { get; set; }
    }

    public class CitaService
    {
        private readonly string connectionString = "Server=Julio\\SQLEXPRESS;Database=Clinica_DB;Trusted_Connection=True;TrustServerCertificate=True;";

        public void AgregarCita(Cita cita)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string query = @"INSERT INTO Citas (Nombre, Apellido, Edad, Telefono, Fecha, Motivo) 
                             VALUES (@nombre, @apellido, @edad, @telefono, @fecha, @motivo)";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", cita.Nombre);
            cmd.Parameters.AddWithValue("@apellido", cita.Apellido);
            cmd.Parameters.AddWithValue("@edad", cita.Edad);
            cmd.Parameters.AddWithValue("@telefono", cita.Telefono);
            cmd.Parameters.AddWithValue("@fecha", cita.Fecha);
            cmd.Parameters.AddWithValue("@motivo", cita.Motivo);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Cita agregada correctamente.");
        }

        public void ModificarCita(Cita cita)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string query = @"UPDATE Citas SET Nombre=@nombre, Apellido=@apellido, Edad=@edad, Telefono=@telefono, Fecha=@fecha, Motivo=@motivo WHERE Id=@id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", cita.Nombre);
            cmd.Parameters.AddWithValue("@apellido", cita.Apellido);
            cmd.Parameters.AddWithValue("@edad", cita.Edad);
            cmd.Parameters.AddWithValue("@telefono", cita.Telefono);
            cmd.Parameters.AddWithValue("@fecha", cita.Fecha);
            cmd.Parameters.AddWithValue("@motivo", cita.Motivo);
            cmd.Parameters.AddWithValue("@id", cita.Id);
            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Cita modificada correctamente.");
            else
                Console.WriteLine("No se encontró la cita con ese ID.");
        }

        public void EliminarCita(int id)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "DELETE FROM HistoriaMedica WHERE PacienteId=@id; DELETE FROM Citas WHERE Id=@id;";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Cita eliminada correctamente.");
            else
                Console.WriteLine("No se encontró la cita con ese ID.");
        }

        public void AgregarHistoria(HistoriaMedica historia)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "INSERT INTO HistoriaMedica (PacienteId, Descripcion) VALUES (@pacienteId, @descripcion)";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@pacienteId", historia.PacienteId);
            cmd.Parameters.AddWithValue("@descripcion", historia.Descripcion);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Historia médica agregada correctamente.");
        }

        // Nuevo método para listar citas
        public void MostrarCitas()
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "SELECT Id, Nombre, Apellido, Edad, Telefono, Fecha, Motivo FROM Citas ORDER BY Fecha";
            using var cmd = new SqlCommand(query, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\nLista de Citas:");
            Console.WriteLine("ID | Nombre | Apellido | Edad | Teléfono | Fecha y Hora | Motivo");
            Console.WriteLine("-----------------------------------------------------------------");

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string nombre = reader.GetString(1);
                string apellido = reader.GetString(2);
                int edad = reader.GetInt32(3);
                string telefono = reader.GetString(4);
                DateTime fecha = reader.GetDateTime(5);
                string motivo = reader.GetString(6);

                Console.WriteLine($"{id} | {nombre} | {apellido} | {edad} | {telefono} | {fecha} | {motivo}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var service = new CitaService();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- Menú Citas Médicas ---");
                Console.WriteLine("1. Agregar cita");
                Console.WriteLine("2. Modificar cita");
                Console.WriteLine("3. Eliminar cita");
                Console.WriteLine("4. Agregar historia médica");
                Console.WriteLine("5. Mostrar citas");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        var nuevaCita = LeerDatosCita();
                        service.AgregarCita(nuevaCita);
                        break;

                    case "2":
                        Console.Write("Ingrese el ID de la cita a modificar: ");
                        if (int.TryParse(Console.ReadLine(), out int modId))
                        {
                            var citaMod = LeerDatosCita();
                            citaMod.Id = modId;
                            service.ModificarCita(citaMod);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "3":
                        Console.Write("Ingrese el ID de la cita a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int delId))
                        {
                            service.EliminarCita(delId);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "4":
                        Console.Write("Ingrese el ID del paciente para agregar historia médica: ");
                        if (int.TryParse(Console.ReadLine(), out int pacId))
                        {
                            Console.Write("Ingrese la descripción de la historia médica: ");
                            string desc = Console.ReadLine();

                            var historia = new HistoriaMedica
                            {
                                PacienteId = pacId,
                                Descripcion = desc
                            };
                            service.AgregarHistoria(historia);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;

                    case "5":
                        service.MostrarCitas();
                        break;

                    case "6":
                        salir = true;
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción inválida, intente de nuevo.");
                        break;
                }
            }
        }

        static Cita LeerDatosCita()
        {
            var cita = new Cita();

            Console.Write("Nombre: ");
            cita.Nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            cita.Apellido = Console.ReadLine();

            int edad;
            Console.Write("Edad: ");
            while (!int.TryParse(Console.ReadLine(), out edad))
            {
                Console.Write("Edad inválida, ingrese un número: ");
            }
            cita.Edad = edad;

            Console.Write("Teléfono: ");
            cita.Telefono = Console.ReadLine();

            DateTime fecha;
            Console.Write("Fecha (AAAA-MM-DD HH:MM): ");
            while (!DateTime.TryParse(Console.ReadLine(), out fecha))
            {
                Console.Write("Fecha inválida, intente nuevamente (Ej: 2025-07-25 15:30): ");
            }
            cita.Fecha = fecha;

            Console.Write("Motivo de la cita: ");
            cita.Motivo = Console.ReadLine();

            return cita;
        }
    }
}
