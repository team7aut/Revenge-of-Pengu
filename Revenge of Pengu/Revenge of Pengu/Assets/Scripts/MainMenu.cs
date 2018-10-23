using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class gives functionality to new game and exit game buttons
 */

public class MainMenu : MonoBehaviour {

	public void NewGame()
    {
        Debug.Log("New Game Button Pressed");
        SceneManager.LoadScene("One");
    }
    public void ExitGame()
    {
        Debug.Log("Exit Game button Pressed");
        Application.Quit();
    }
}
