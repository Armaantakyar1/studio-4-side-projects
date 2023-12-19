using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
         Scene currentScene = SceneManager.GetActiveScene();
         SceneManager.LoadScene(currentScene.name);
    }

    public void Out()
    {
        Application.Quit();
    }
}
