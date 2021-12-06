using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void ChangeGivenScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadPlayerLevel(string scene)
    {
        PlayerData.lastCheckpoint = 0;
        PlayerData.lives = 3;
        SceneManager.LoadScene(scene);
    }
}
