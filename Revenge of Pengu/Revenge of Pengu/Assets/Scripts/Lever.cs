using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {
    private SpriteRenderer spriteRend;
    public bool isOff;
    public Sprite offSprite;
    public Sprite onSprite;
    public Sprite offTransition;
    public Sprite onTransition;
    private LeverRange leverRange;


	// Use this for initialization
	void Start ()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        isOff = true;
        leverRange = gameObject.GetComponentInChildren<LeverRange>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Checks if player is in range of the lever
        if (leverRange.inRange)
        {
            //If the player presses e the lever is pressed 
            if (Input.GetKeyDown("e"))
            {
                toggleLever();
            }

            spriteTransition();
        } else {
            if (isOff)
            {
                spriteRend.sprite = offSprite;
            } else {
                spriteRend.sprite = onSprite;
            }
        }
    }

    void toggleLever()
    {
        if (isOff)
        {
            spriteRend.sprite = onSprite;
            isOff = false;
            Debug.Log("Lever on");

        } else {
            spriteRend.sprite = offSprite;
            isOff = true;
            Debug.Log("Lever off");
        }
    }

    
    void spriteTransition()
    {
        if (isOff)
        {
            spriteRend.sprite = offTransition;
            Debug.Log("Lever on");
        } else {
            spriteRend.sprite = onTransition;
            Debug.Log("Lever off");
        }
    }
}
