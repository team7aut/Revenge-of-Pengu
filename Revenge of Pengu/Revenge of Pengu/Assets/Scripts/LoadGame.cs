using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This class loads the different levels for the map
 */

public class LoadGame : MonoBehaviour
{


    //Gets the level input and loads the level
    public void loadLevel(int level)
    {
        //check the current level then adds it by 1
        if(level == -1)
        {
            int y = SceneManager.GetActiveScene().buildIndex + 1;
            int x = SceneManager.sceneCountInBuildSettings;
            //loads the main menu
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
 
    //Loads the level
    IEnumerator LoadAsynchronously (int scenceIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenceIndex);

        while (!operation.isDone)
        {
            //resits the main game time back to normal speed
            Time.timeScale = 1f;
 
            yield return null;
        }

    }

    //Exits the game
    public void QuitGame()
    {
        Application.Quit();
    }

}