using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

	public void NewGame()
    {
        Debug.Log("New Game Button Pressed");
        SceneManager.LoadScene("MapTesting");
    }
    public void ExitGame()
    {
        Debug.Log("Exit Game button Pressed");
        Application.Quit();
    }
}
