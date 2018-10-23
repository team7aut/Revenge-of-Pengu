using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class gives functionality to new game and quit game buttons
 */

public class MenuScript1 : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
   
    public void QuitGame()
    {
        Debug.Log("Quit Game Button pressed");
        Application.Quit();

    }
    
}
