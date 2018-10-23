using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{


    // Use this for initialization
    public void loadLevel(int level)
    {
        if(level == -1)
        {
            int y = SceneManager.GetActiveScene().buildIndex + 1;
            int x = SceneManager.sceneCountInBuildSettings;
            if (y == x)
            {
                level = 0;
            }
            else
            {
                level = y;
            } 
        }

        StartCoroutine(LoadAsynchronously(level));

    }
 
    IEnumerator LoadAsynchronously (int scenceIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenceIndex);

        while (!operation.isDone)
        {
            Time.timeScale = 1f;
  





            yield return null;
        }

    }

    public void QuitGame()
    {

        Application.Quit();
    }

}