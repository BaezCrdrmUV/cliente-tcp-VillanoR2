using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCom
{
    public class TCPServer
    {
        public TcpListener listener { get; set; }
        public bool acceptFlag { get; private set; }

        public TCPServer(string ip, int puerto, bool start = false)
        {
            IPAddress address = IPAddress.Parse(ip);
            this.listener = new TcpListener(address, puerto);
            if(start == true)
            {
                listener.Start();
                Console.WriteLine("Servidor iniciado en la direccion {0}:{1}",
                    address.MapToIPv4().ToString(), puerto.ToString());
                acceptFlag = true;
            }
        }

        public void Listen()
        {
            if(listener != null && acceptFlag == true)
            {
                while(true)
                {
                    Console.WriteLine("Esperando conexión de cliente...");
                    var clientTask = listener.AcceptSocketAsync();

                    if(clientTask.Result != null)
                    {
                        Console.WriteLine("Cliente conectado. Esperando datos");
                        var client = clientTask.Result;
                        string msg = "";

                        while(msg != null && !msg.StartsWith("bye"))
                        {
                            byte[] data = Encoding.UTF8.GetBytes("Envia datos. Envia \"bye\" para terminar");
                            client.Send(data);

                            byte[] buffer = new byte[1024];
                            client.Receive(buffer);

                            msg = Encoding.ASCII.GetString(buffer);
                            Console.WriteLine(msg);
                        }
                        Console.WriteLine("Cerrando conexion");
                        client.Dispose();
                    }
                }
            }
        }
    }
}