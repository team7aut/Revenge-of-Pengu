using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * This class is used to create a pause menu where the player can pasuse the game at any point in time
 */

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPause = false;
    private Scene scene;
    public GameObject pauseMenuUI;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }


    // Update is called once per frame
    void Update () {
        
        //Checks if the user has pressed escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    //This method pauses the game
     void Pause()
    {
        //Pause menu GUI is displayed
        pauseMenuUI.SetActive(true);
        //Game time is stopped
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    //This method resumes the game
    public void Resume()
    {
        //Pause menu GUI is off
        pauseMenuUI.SetActive(false);
        //Game time is set back to normal
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    //This method returns the user back to menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuNew");
    }

    //This method restarts the current level
   public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.name);
    }

    //This method exits the game
    public void Exit()
    {
        Application.Quit();
    }

}
