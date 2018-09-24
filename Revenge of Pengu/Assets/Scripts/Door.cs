using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour {

    private bool currentlyClosed; //current state
    public bool meantToBeClosed; //desired state
    public GameObject openDoor;
    public GameObject closedDoor;
    private GameObject currentState;
    private Vector2 position;
    private SpriteRenderer initialSprite;
    private Lever lever;

    // Use this for initialization
    void Start () {
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
        if (meantToBeClosed && !currentlyClosed)
        {
            GameObject newDoor = (GameObject)Instantiate(closedDoor);
            Destroy(currentState);
            currentState = newDoor;
            currentlyClosed = true;
        }
        else if (!meantToBeClosed && currentlyClosed)
        {
            GameObject newDoor = (GameObject)Instantiate(openDoor);
            Destroy(currentState);
            currentState = newDoor;
            currentlyClosed = false;
        }
        currentState.transform.position = position;
	}
}
