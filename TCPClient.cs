using System;
using System.Net.Sockets;

namespace SocketCom
{
    public class TCPCliente
    {
        String NombreCliente { get; set; }
        TcpClient Cliente { get; set; }

        public TCPCliente(String nombre, String servidor, int puerto)
        {
            NombreCliente = nombre;
            Cliente = new TcpClient(servidor, puerto);
        }

        public void SendMessage()
        {
            try
            {
                while(true)
                {
                    NetworkStream stream = Cliente.GetStream(); 
                    Console.WriteLine("Ingrese un mensaje para el servidor: ");
                    String mensaje = Console.ReadLine();
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(mensaje);  
                    Console.WriteLine("Enviando mensaje...");
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Mensaje enviado");
                    if (mensaje == "bye")
                    {
                        Console.WriteLine("Cerrando conexion");
                        break;
                    }
                    Console.WriteLine("Esperando respuesta...");
                    data = new byte[256];
                    String respuesta = String.Empty;
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    respuesta = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Respuesta: " + respuesta);
                }
            }catch (SocketException)
            {
                Console.WriteLine("Error al conectar al servidor");
            }
        }
    }
}