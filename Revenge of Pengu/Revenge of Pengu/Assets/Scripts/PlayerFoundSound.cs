using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoundSound : MonoBehaviour {

    
    AudioSource audioSrc;
    public AudioClip soundToPlay;
    public CompMove compMove;
    bool soundplay;

    // Use this for initialization
    void Awake()
    {
       
    }

    void Start () {

        audioSrc = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
       soundplay =  compMove.GetComponent<CompMove>().playSound;

        if (soundplay)
        {
            compMove.playSound = false;
            playSound();
        }

	}

    public void playSound()
    {
        audioSrc.PlayOneShot(soundToPlay, .1f);
    }

}
