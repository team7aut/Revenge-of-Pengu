using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript2 : MonoBehaviour {

 /*
 * This class gives functionality to quit game buttons in the instruction menu
 */

    public void QuitGame()
    {
        Debug.Log("Quit Game Button pressed");
        Application.Quit();

    }
}
