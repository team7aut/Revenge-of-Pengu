using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class loadNextLevel : MonoBehaviour {

    public playerMovement player;
    public GameObject menu;


	
	// Update is called once per frame
	void Update () {

        if (player.endLevel)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
        }
	}
}
