﻿using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using NetworkingLibrary;
using System.Collections.Generic;
using System.Collections;

namespace Server
{
    public class Server : MonoBehaviour
    {
        [SerializeField] float ticksPerSecond = 30;
        float tickRate;
        protected List<PlayerSocket> clients = new();
        protected List<string> clientGameObjectIDs = new();

        protected Socket queueSocket;

        public static Action<string, Socket> ClientAdded;
        public Action<string, bool> OnServerLobbyUpdate;
        public Action<bool> UpdatePlayerSceneStatus;

        public static Server Instance;

        [SerializeField] int maxIDRange;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            else
            {
                Destroy(gameObject);
            }
            tickRate = 1 / ticksPerSecond;
        }

        protected virtual void Start()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 30);
            queueSocket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

            queueSocket.Blocking = false;
            queueSocket.Bind(ipEndPoint);
            queueSocket.Listen(10);

            StartCoroutine(Tick());
        }

        IEnumerator Tick()
        {
            while (true)
            {
                if (clients.Count < 3)
                {
                    TryToAccept(queueSocket);
                }

                foreach (PlayerSocket playerSocket in clients)
                {
                    if (playerSocket.socket.Available > 0)
                    {
                        byte[] buffer = new byte[playerSocket.socket.Available];
                        playerSocket.socket.Receive(buffer);
                        int index = 0;
                        while (index < buffer.Length)
                        {
                            BasePacket packet = new BasePacket().Deserialize(buffer, index);
                            if (packet != null)
                            {
                                SwitchCaseHell(playerSocket, buffer, packet, index);
                                index += packet.packetSize;
                            }
                        }
                    }
                }
                yield return new WaitForSeconds(tickRate);
            }
        }

        private void SwitchCaseHell(PlayerSocket playerSocket, byte[] buffer, BasePacket packet, int index)
        {
            switch (packet.packetType)
            {
                case BasePacket.PacketType.PlayerLobbyPacket:
                    LobbyPacket lobbyPacket = new LobbyPacket().Deserialize(buffer, index);
                    OnServerLobbyUpdate?.Invoke(lobbyPacket.playerID, lobbyPacket.isReady);
                    break;

                case BasePacket.PacketType.PlayerInMainScenePacket:
                    PlayerInScene playerInMainScenePacket = new PlayerInScene().Deserialize(buffer, index);
                    UpdatePlayerSceneStatus?.Invoke(playerInMainScenePacket.inMainScene);
                    Debug.LogError("Players in main scene packet received");
                    break;

            }
        }

        private void TryToAccept(Socket _queueSocket)
        {
            try
            {
                Socket newSocket = _queueSocket.Accept();

                PlayerSocket playerSocket = new PlayerSocket(newSocket);
                playerSocket.playerID = GenerateUniqueClientID();
                bool isHost = clients.Count == 0;
                IDPacket packet = new IDPacket(playerSocket.playerID, isHost);

                playerSocket.socket.Send(packet.Serialize());
                clients.Add(playerSocket);
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode != SocketError.WouldBlock)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void SendData(byte[] buffer, Socket socket)
        {
            socket.Send(buffer);
        }

        string GenerateUniqueClientID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public void SendToAllClients(byte[] buffer)
        {
            foreach (var client in clients)
            {
                client.socket.Send(buffer);
            }
        }
        private void OnDisable()
        {
            queueSocket.Close();
        }
    }
}
