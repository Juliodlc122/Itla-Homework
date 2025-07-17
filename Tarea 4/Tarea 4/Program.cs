//Julio Ruiz Matricula:2024-2009
using System;
using System.Collections.Generic;

public class Contacto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public int Edad { get; set; }
    public bool EsMejorAmigo { get; set; }

    public Contacto(int id, string nombre, string apellido, string direccion, string telefono, string email, int edad, bool esMejorAmigo)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Direccion = direccion;
        Telefono = telefono;
        Email = email;
        Edad = edad;
        EsMejorAmigo = esMejorAmigo;
    }

    public void Mostrar()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Apellido: {Apellido}");
        Console.WriteLine($"Dirección: {Direccion}");
        Console.WriteLine($"Teléfono: {Telefono}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Edad: {Edad}");
        Console.WriteLine($"¿Mejor amigo?: {(EsMejorAmigo ? "Sí" : "No")}");
        Console.WriteLine("----------------------------------");
    }
}

public class Agenda
{
    private List<Contacto> contactos = new List<Contacto>();
    private int siguienteId = 1;

    public void AgregarContacto()
    {
        Console.Write("Nombre: "); string nombre = Console.ReadLine();
        Console.Write("Apellido: "); string apellido = Console.ReadLine();
        Console.Write("Dirección: "); string direccion = Console.ReadLine();
        Console.Write("Teléfono: "); string telefono = Console.ReadLine();
        Console.Write("Email: "); string email = Console.ReadLine();

        Console.Write("Edad: ");
        if (!int.TryParse(Console.ReadLine(), out int edad))
        {
            Console.WriteLine("Edad inválida.");
            return;
        }

        Console.Write("¿Es mejor amigo? (1. Sí / 2. No): ");
        bool esMejorAmigo = Console.ReadLine() == "1";

        var contacto = new Contacto(siguienteId++, nombre, apellido, direccion, telefono, email, edad, esMejorAmigo);
        contactos.Add(contacto);
        Console.WriteLine("Contacto agregado exitosamente.");
    }

    public void MostrarContactos()
    {
        if (contactos.Count == 0)
        {
            Console.WriteLine("No hay contactos registrados.");
            return;
        }

        foreach (var contacto in contactos)
        {
            contacto.Mostrar();
        }
    }

    public void BuscarContacto()
    {
        Console.Write("Ingrese nombre o apellido para buscar: ");
        string termino = Console.ReadLine().ToLower();
        var encontrados = contactos.FindAll(c =>
            c.Nombre.ToLower().Contains(termino) || c.Apellido.ToLower().Contains(termino));

        if (encontrados.Count == 0)
        {
            Console.WriteLine("No se encontró ningún contacto.");
            return;
        }

        foreach (var contacto in encontrados)
        {
            contacto.Mostrar();
        }
    }

    public void ModificarContacto()
    {
        Console.Write("Ingrese el ID del contacto a modificar: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        var contacto = contactos.Find(c => c.Id == id);
        if (contacto == null)
        {
            Console.WriteLine("Contacto no encontrado.");
            return;
        }

        Console.Write("Nuevo nombre: "); contacto.Nombre = Console.ReadLine();
        Console.Write("Nuevo apellido: "); contacto.Apellido = Console.ReadLine();
        Console.Write("Nueva dirección: "); contacto.Direccion = Console.ReadLine();
        Console.Write("Nuevo teléfono: "); contacto.Telefono = Console.ReadLine();
        Console.Write("Nuevo email: "); contacto.Email = Console.ReadLine();

        Console.Write("Nueva edad: ");
        if (int.TryParse(Console.ReadLine(), out int nuevaEdad)) contacto.Edad = nuevaEdad;

        Console.Write("¿Es mejor amigo? (1. Sí / 2. No): ");
        contacto.EsMejorAmigo = Console.ReadLine() == "1";

        Console.WriteLine("Contacto modificado correctamente.");
    }

    public void EliminarContacto()
    {
        Console.Write("Ingrese el ID del contacto a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        var contacto = contactos.Find(c => c.Id == id);
        if (contacto == null)
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        contactos.Remove(contacto);
        Console.WriteLine("Contacto eliminado correctamente.");
    }
}

class Program
{
    static void Main()
    {
        Agenda agenda = new Agenda();
        bool activo = true;

        Console.WriteLine("Bienvenido a tu Agenda Personal");

        while (activo)
        {
            Console.WriteLine("\nMenú de opciones:");
            Console.WriteLine("1. Agregar Contacto");
            Console.WriteLine("2. Ver Contactos");
            Console.WriteLine("3. Buscar Contacto");
            Console.WriteLine("4. Modificar Contacto");
            Console.WriteLine("5. Eliminar Contacto");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            string entrada = Console.ReadLine();
            if (!int.TryParse(entrada, out int opcion)) continue;

            switch (opcion)
            {
                case 1: agenda.AgregarContacto(); break;
                case 2: agenda.MostrarContactos(); break;
                case 3: agenda.BuscarContacto(); break;
                case 4: agenda.ModificarContacto(); break;
                case 5: agenda.EliminarContacto(); break;
                case 6: activo = false; break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        }

        Console.WriteLine("Agenda finalizada.");
    }
}