using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * This class allows the animation of the closed and open doors 
 */

public class Door : MonoBehaviour {

    //Initialise of variables
    private bool currentlyClosed; //current state
    public bool meantToBeClosed; //desired state
    public GameObject openDoor;
    public GameObject closedDoor;
    private GameObject currentState;
    private Vector2 position;
    private SpriteRenderer initialSprite;
    private Lever lever;

    AudioSource audioSrc;
    public AudioClip soundToPlay;

    // Use this for initialization
    void Start () {
        audioSrc = GetComponent<AudioSource>();
        meantToBeClosed = true;
        currentlyClosed = true;
        currentState = (GameObject)Instantiate(closedDoor);
        initialSprite = GetComponent<SpriteRenderer>();
        initialSprite.sprite = null;
        lever = gameObject.GetComponentInChildren<Lever>();
        position = this.gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        meantToBeClosed = lever.isOff;
        //If the door is closed the then closed door object will be displayed
        if (meantToBeClosed && !currentlyClosed)
        {
            GameObject newDoor = (GameObject)Instantiate(closedDoor);
            Destroy(currentState);
            currentState = newDoor;
            currentlyClosed = true;
        }
        //If the door is opened the then opened door object will be displayed
        else if (!meantToBeClosed && currentlyClosed)
        {
            GameObject newDoor = (GameObject)Instantiate(openDoor);
            Destroy(currentState);
            currentState = newDoor;
            currentlyClosed = false;
            playSound();

        }
    
        currentState.transform.position = position;
	}

    //Play sound when door is opened
    public void playSound()
    {
        audioSrc.PlayOneShot(soundToPlay, 4);
    }
}
