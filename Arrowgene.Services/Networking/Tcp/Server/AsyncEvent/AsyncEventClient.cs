﻿/*
 * MIT License
 * 
 * Copyright (c) 2018 Sebastian Heinz <sebastian.heinz.gt@googlemail.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */


using System;
using System.Net;
using System.Net.Sockets;

namespace Arrowgene.Services.Networking.Tcp.Server.AsyncEvent
{
    public class AsyncEventClient : ITcpSocket
    {
        public Socket Socket { get; }
        public bool IsAlive { get; private set; }

        private readonly AsyncEventServer _server;

        public AsyncEventClient(Socket socket, AsyncEventServer server)
        {
            IsAlive = true;
            _server = server;
            Socket = socket;
        }

        public IPAddress RemoteIpAddress
        {
            get
            {
                if (Socket != null && Socket.RemoteEndPoint != null)
                {
                    IPEndPoint ipEndPoint = Socket.RemoteEndPoint as IPEndPoint;
                    if (ipEndPoint != null)
                    {
                        return ipEndPoint.Address;
                    }
                }

                return null;
            }
        }

        public ushort Port
        {
            get
            {
                if (Socket != null && Socket.RemoteEndPoint != null)
                {
                    IPEndPoint ipEndPoint = Socket.RemoteEndPoint as IPEndPoint;
                    if (ipEndPoint != null)
                    {
                        return (ushort) ipEndPoint.Port;
                    }
                }

                return 0;
            }
        }

        public void Send(byte[] data)
        {
            _server.Send(this, data);
        }

        public void Close()
        {
            IsAlive = false;
            try
            {
                Socket.Shutdown(SocketShutdown.Send);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }

            Socket.Close();
        }
    }
}