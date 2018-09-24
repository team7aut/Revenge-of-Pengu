using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript1 : MonoBehaviour {

	public void PlayGame()
    {
        Debug.Log("New Game Button Pressed");
        SceneManager.LoadScene("MapTesting");
    }
    public void LoadGame()
    {
        Debug.Log("Load Game button pressed");
        SceneManager.LoadScene("MapTesting");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game Button pressed");
        Application.Quit();

    }
    
}
