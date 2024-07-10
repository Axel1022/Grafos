class RedSocial
{
    private int[,] matrizAdyacencia;
    private Dictionary<int, string> usuarios;
    private int conteoUsuarios;

    public RedSocial(int tamaño)
    {
        matrizAdyacencia = new int[tamaño, tamaño];
        usuarios = new Dictionary<int, string>();
        conteoUsuarios = 0;
    }

    public void AgregarUsuario(string nombre)
    {
        if (conteoUsuarios < matrizAdyacencia.GetLength(0))
        {
            usuarios[conteoUsuarios] = nombre;
            conteoUsuarios++;
            Console.WriteLine("Usuario agregado con éxito.");
        }
        else
        {
            Console.WriteLine("Se ha alcanzado el número máximo de usuarios.");
        }
    }

    public void AgregarAmistad(int usuario1, int usuario2)
    {
        if (usuario1 < conteoUsuarios && usuario2 < conteoUsuarios && usuario1 != usuario2)
        {
            matrizAdyacencia[usuario1, usuario2] = 1;
            matrizAdyacencia[usuario2, usuario1] = 1;
            Console.WriteLine("Amistad agregada con éxito.");
        }
        else
        {
            Console.WriteLine("IDs de usuario inválidos o iguales.");
        }
    }

    public List<string> ObtenerAmigosEnComun(int usuario1, int usuario2)
    {
        List<string> amigosEnComun = new List<string>();

        if (usuario1 < conteoUsuarios && usuario2 < conteoUsuarios)
        {
            for (int i = 0; i < conteoUsuarios; i++)
            {
                if (matrizAdyacencia[usuario1, i] == 1 && matrizAdyacencia[usuario2, i] == 1)
                {
                    amigosEnComun.Add(usuarios[i]);
                }
            }
        }

        return amigosEnComun;
    }

    public void MostrarRed()
    {
        Console.WriteLine("Matriz de Adyacencia:");
        for (int i = 0; i < conteoUsuarios; i++)
        {
            for (int j = 0; j < conteoUsuarios; j++)
            {
                Console.Write(matrizAdyacencia[i, j] + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nUsuarios:");
        foreach (var usuario in usuarios)
        {
            Console.WriteLine($"{usuario.Key}: {usuario.Value}");
        }
    }

    public int ObtenerNumeroUsuarios()
    {
        return conteoUsuarios;
    }
}

class Program
{
    static void Main(string[] args)
    {
        RedSocial red = new RedSocial(10);

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n--- Menú de Red Social ---");
            Console.WriteLine("1. Agregar Usuario");
            Console.WriteLine("2. Agregar Amistad");
            Console.WriteLine("3. Encontrar Amigos en Común");
            Console.WriteLine("4. Mostrar Red");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine()!;

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el nombre del usuario: ");
                    string nombre = Console.ReadLine()!;
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        red.AgregarUsuario(nombre);
                    }
                    else
                    {
                        Console.WriteLine("El nombre del usuario no puede estar vacío.");
                    }
                    break;
                case "2":
                    int usuario1, usuario2;
                    Console.Write("Ingrese el ID del primer usuario: ");
                    if (int.TryParse(Console.ReadLine()!, out usuario1))
                    {
                        Console.Write("Ingrese el ID del segundo usuario: ");
                        if (int.TryParse(Console.ReadLine()!, out usuario2))
                        {
                            red.AgregarAmistad(usuario1, usuario2);
                        }
                        else
                        {
                            Console.WriteLine("ID de usuario inválido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID de usuario inválido.");
                    }
                    break;
                case "3":
                    int u1, u2;
                    Console.Write("Ingrese el ID del primer usuario: ");
                    if (int.TryParse(Console.ReadLine()!, out u1))
                    {
                        Console.Write("Ingrese el ID del segundo usuario: ");
                        if (int.TryParse(Console.ReadLine()!, out u2))
                        {
                            var amigosEnComun = red.ObtenerAmigosEnComun(u1, u2);
                            if (amigosEnComun.Count > 0)
                            {
                                Console.WriteLine("Amigos en común:");
                                foreach (var amigo in amigosEnComun)
                                {
                                    Console.WriteLine(amigo);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay amigos en común.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID de usuario inválido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID de usuario inválido.");
                    }
                    break;
                case "4":
                    red.MostrarRed();
                    break;
                case "5":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Por favor, intente nuevamente.");
                    break;
            }
        }
    }
}
