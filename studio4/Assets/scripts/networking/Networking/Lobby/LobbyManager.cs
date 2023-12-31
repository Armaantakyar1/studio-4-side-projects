using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Server;

public class LobbyManager : MonoBehaviour
{
    LobbyPacket lobbyPacket;
    Dictionary<string,Image> idToImage = new Dictionary<string,Image>();
    [SerializeField] Image[] images;

    private void OnEnable()
    {
        ClientLibrary.Client.Instance.OnLobbyUpdate += UpdateImage;
    }

    private void OnDisable()
    {
        ClientLibrary.Client.Instance.OnLobbyUpdate -= UpdateImage;
    }

    private void Start()
    {
        string clientID = ClientLibrary.Client.Instance.networkComponent.ClientID;
        lobbyPacket = new LobbyPacket(false, "", clientID);
        ClientLibrary.Client.Instance.SendPacket(lobbyPacket.Serialize());
    }

    public void OnButtonClick()
    {
        lobbyPacket.isReady = !lobbyPacket.isReady;
        ClientLibrary.Client.Instance.SendPacket(lobbyPacket.Serialize());
    }

    private void UpdateImage(List<string> playerIDs, List<bool> playerStatuses)
    {
        for (int i = 0; i < playerIDs.Count; i++)
        {
            if (idToImage.ContainsKey(playerIDs[i]))
            {
                idToImage[playerIDs[i]].color = playerStatuses[i] ? Color.green : Color.red; //if red make it green and vice versa
            }
            else
            {
                idToImage.Add(playerIDs[i], images[i]);
                idToImage[playerIDs[i]].color = playerStatuses[i] ? Color.green : Color.red;
            }
        }
    }
}