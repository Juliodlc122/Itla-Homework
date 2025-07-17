//Julio Ruiz Matricula:2024-2009
using System;
using System.Collections.Generic;

class Contactes
{
    static List<int> ids = new List<int>();
    static Dictionary<int, string> names = new Dictionary<int, string>();
    static Dictionary<int, string> lastnames = new Dictionary<int, string>();
    static Dictionary<int, string> addresses = new Dictionary<int, string>();
    static Dictionary<int, string> telephones = new Dictionary<int, string>();
    static Dictionary<int, string> emails = new Dictionary<int, string>();
    static Dictionary<int, int> ages = new Dictionary<int, int>();
    static Dictionary<int, bool> bestFriends = new Dictionary<int, bool>();

    static void Main()
    {
        Console.WriteLine("Bienvenido a mi lista de Contactes");
        bool running = true;

        while (running)
        {
            MostrarMenu();

            string entrada = Console.ReadLine();
            if (!int.TryParse(entrada, out int opcion))
            {
                Console.WriteLine("Entrada inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1: AddContact(); break;
                case 2: ViewContacts(); break;
                case 3: SearchContact(); break;
                case 4: ModifyContact(); break;
                case 5: DeleteContact(); break;
                case 6: running = false; break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        }

        Console.WriteLine("Fin de la Agenda.");
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\nMenú de opciones:");
        Console.WriteLine("1. Agregar Contacto");
        Console.WriteLine("2. Ver Contactos");
        Console.WriteLine("3. Buscar Contacto");
        Console.WriteLine("4. Modificar Contacto");
        Console.WriteLine("5. Eliminar Contacto");
        Console.WriteLine("6. Salir");
        Console.Write("Digite el número de la opción deseada: ");
    }

    static void AddContact()
    {
        int id = ids.Count + 1;
        ids.Add(id);

        Console.Write("Nombre: "); string name = Console.ReadLine();
        Console.Write("Apellido: "); string lastname = Console.ReadLine();
        Console.Write("Dirección: "); string address = Console.ReadLine();
        Console.Write("Teléfono: "); string phone = Console.ReadLine();
        Console.Write("Email: "); string email = Console.ReadLine();

        Console.Write("Edad: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Edad inválida.");
            return;
        }

        Console.Write("¿Es mejor amigo? (1. Sí / 2. No): ");
        bool isBestFriend = Console.ReadLine() == "1";

        names[id] = name;
        lastnames[id] = lastname;
        addresses[id] = address;
        telephones[id] = phone;
        emails[id] = email;
        ages[id] = age;
        bestFriends[id] = isBestFriend;

        Console.WriteLine("Contacto agregado exitosamente.");
    }

    static void ViewContacts()
    {
        if (ids.Count == 0)
        {
            Console.WriteLine("No hay contactos registrados.");
            return;
        }

        Console.WriteLine("\nListado de Contactos:");
        foreach (var id in ids)
        {
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Nombre: {names[id]}");
            Console.WriteLine($"Apellido: {lastnames[id]}");
            Console.WriteLine($"Dirección: {addresses[id]}");
            Console.WriteLine($"Teléfono: {telephones[id]}");
            Console.WriteLine($"Email: {emails[id]}");
            Console.WriteLine($"Edad: {ages[id]}");
            Console.WriteLine($"¿Mejor amigo?: {(bestFriends[id] ? "Sí" : "No")}");
            Console.WriteLine("----------------------------------");
        }
    }

    static void SearchContact()
    {
        Console.Write("Ingrese nombre o apellido a buscar: ");
        string searchTerm = Console.ReadLine().ToLower();
        bool found = false;

        foreach (var id in ids)
        {
            if (names[id].ToLower().Contains(searchTerm) || lastnames[id].ToLower().Contains(searchTerm))
            {
                Console.WriteLine($"ID: {id}");
                Console.WriteLine($"Nombre: {names[id]}");
                Console.WriteLine($"Apellido: {lastnames[id]}");
                Console.WriteLine($"Dirección: {addresses[id]}");
                Console.WriteLine($"Teléfono: {telephones[id]}");
                Console.WriteLine($"Email: {emails[id]}");
                Console.WriteLine($"Edad: {ages[id]}");
                Console.WriteLine($"¿Mejor amigo?: {(bestFriends[id] ? "Sí" : "No")}");
                Console.WriteLine("----------------------------------");
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No se encontró ningún contacto.");
    }

    static void ModifyContact()
    {
        Console.Write("Ingrese el ID del contacto a modificar: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || !ids.Contains(id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Console.Write("Nuevo nombre: "); names[id] = Console.ReadLine();
        Console.Write("Nuevo apellido: "); lastnames[id] = Console.ReadLine();
        Console.Write("Nueva dirección: "); addresses[id] = Console.ReadLine();
        Console.Write("Nuevo teléfono: "); telephones[id] = Console.ReadLine();
        Console.Write("Nuevo email: "); emails[id] = Console.ReadLine();

        Console.Write("Nueva edad: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Edad inválida.");
            return;
        }
        ages[id] = age;

        Console.Write("¿Es mejor amigo? (1. Sí / 2. No): ");
        bestFriends[id] = Console.ReadLine() == "1";

        Console.WriteLine("Contacto modificado correctamente.");
    }

    static void DeleteContact()
    {
        Console.Write("Ingrese el ID del contacto a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || !ids.Contains(id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        ids.Remove(id);
        names.Remove(id);
        lastnames.Remove(id);
        addresses.Remove(id);
        telephones.Remove(id);
        emails.Remove(id);
        ages.Remove(id);
        bestFriends.Remove(id);

        Console.WriteLine("Contacto eliminado correctamente.");
    }
}