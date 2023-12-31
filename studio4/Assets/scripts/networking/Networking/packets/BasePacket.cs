﻿using System.IO;

namespace NetworkingLibrary
{
    public class BasePacket
    {
        public string gameObjectID;
        public enum PacketType
        {
            unknown = -1,
            none,
            ID,
            PlayerLobbyPacket,
            ServerLobbyPacket,
            ScenePacket,
            PlayerInMainScenePacket,
        }

        public PacketType packetType { get; private set; }

        public ushort packetSize { get; private set; }

        protected MemoryStream writeMemoryStream;
        protected MemoryStream readMemoryStream;
        protected BinaryWriter binaryWriter;
        protected BinaryReader binaryReader;

        public BasePacket()
        {
            packetType = PacketType.none;
        }
        public BasePacket(PacketType _packetType, string gameObjectID = "")
        {
            packetType = _packetType;
            this.gameObjectID = gameObjectID;
        }

        public byte[] Serialize()
        {
            writeMemoryStream = new MemoryStream();
            binaryWriter = new BinaryWriter(writeMemoryStream);
            binaryWriter.Write(packetSize);
            binaryWriter.Write((int)packetType);
            binaryWriter.Write(gameObjectID);
            return writeMemoryStream.ToArray();
        }
        public BasePacket Deserialize(byte[] dataToDeserialize, int index)
        {
            readMemoryStream = new MemoryStream(dataToDeserialize);
            readMemoryStream.Seek(index, SeekOrigin.Begin);
            binaryReader = new BinaryReader(readMemoryStream);
            packetSize = binaryReader.ReadUInt16();
            packetType = (PacketType)binaryReader.ReadInt32();
            gameObjectID = binaryReader.ReadString();
            return this;
        }

        protected void FinishSerialization()
        {
            packetSize = (ushort)writeMemoryStream.Length;
            binaryWriter.Seek(-packetSize, SeekOrigin.Current);
            binaryWriter.Write(packetSize);
        }
    }
}
