﻿using System;
using System.Net.Sockets;

namespace Common
{
    public sealed class ConnectedObject
    {
        public ConnectedObject(Socket socket, Message message)
        {
            Socket = socket;
            Message = message;
        }

        public ConnectedObject(Socket socket) : this(socket, new Message())
        {

        }

        public Socket Socket { get; set; }

        public Message Message { get; set; }

        public void Close()
        {
            try
            {
                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
