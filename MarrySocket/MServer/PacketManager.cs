﻿/*
 *  Copyright 2015 Sebastian Heinz <sebastian.heinz.gt@googlemail.com>
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 */
namespace MarrySocket.MServer
{
    using MarrySocket.MExtra.Logging;
    using MarrySocket.MExtra.Packet;
    using MarrySocket.MExtra.Serialization;
    using System;
    using System.Reflection;

    public class PacketManager
    {
        private Logger serverLog;
        private ServerConfig serverConfig;
        private ISerialization serializer;

        public PacketManager(ServerConfig serverConfig)
        {
            this.serverConfig = serverConfig;
            this.serverLog = this.serverConfig.Logger;
            this.serializer = this.serverConfig.Serializer;
        }

        public void Handle(ClientSocket clientSocket, ReadPacket packet)
        {
            //try
            //{

            //}
            //catch (SerializationException e)
            //{
            //    // this.logger.Write("Failed to serialize. Reason: {0}", e.Message, LogType.ERROR);
            //}
            Type t = typeof(ISerialization);
            MethodInfo method = t.GetMethod("Deserialize");
            MethodInfo generic = method.MakeGenericMethod(packet.Type);
            var myObject = generic.Invoke(this.serializer, new object[] { packet.SerializedClass });

            if (myObject != null)
            {
                this.serverConfig.OnReceivedPacket(packet.PacketHeader.PacketId, clientSocket, myObject);
                this.serverLog.Write("Client[{0}]: Handled Packet: {0}", clientSocket.Id, packet.PacketHeader.PacketId, LogType.PACKET);
            }
        }

    }
}
