using System;

namespace SocketCom
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "server")
            {
                TCPServer server = new TCPServer("127.0.0.1", 8080, true);
                server.Listen();
            }
            else if (args[0] == "client")
            {
                Console.WriteLine("Ingrese el Nombre del cliente");
                string name = Console.ReadLine();
                TCPCliente cliente = new TCPCliente(name, "127.0.0.1", 8080);
                cliente.SendMessage();
            }
        }
    }
}
