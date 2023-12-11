using UnityEngine;

public class PlayerSceneStatus : MonoBehaviour
{
    [SerializeField] bool[] playersInMainScene = new bool[2];
    [SerializeField] string realPlayer;
    int index;

    private void OnEnable()
    {
        Server.Server.Instance.UpdatePlayerSceneStatus += UpdateSceneStatus;
    }

    private void OnDisable()
    {
        Server.Server.Instance.UpdatePlayerSceneStatus -= UpdateSceneStatus;

    }
    void UpdateSceneStatus(bool status)
    {
        playersInMainScene[index] = status;
        index++;
        if (index == playersInMainScene.Length)
        {
            Debug.LogError("All players are now in the main scene");
        }
    }
}
