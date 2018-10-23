using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This class is used to control the player movement 
 */

public class playerMovement : MonoBehaviour {

    //Initialise movement and sliding speed
    public float normalSpeed;
    public float slidingSpeed;

    //Initialise game goal
    public GameObject goal;
    public bool goalPicked = false;
    private GameObject[] exits;


    private bool ShiftDownHold;
    private Scene scene;

    //Initialise stamina loss 
    private float staminaLostTime;
    public float setStaminaLostTime;

    //Initialise stamina regain time
    private float staminaGainTime;
    public float setStaminaGainTime;

    //Initialise player movement direction
    private Vector2 moveInput;

    //Initialise stamina capacity
    public int stamina;
    private int setStamina;

    //Initialise rigidbody for player movement
    private Rigidbody2D rd;
    private Vector2 moveVelocity;

    //Initialise player animation variables
    private Animator anim;
    private Vector2 lastDirection;
    private bool isMoving;
    bool ShiftDown;
    bool playerSliding;

    AudioSource audioSrc;
    public AudioClip soundToPlay;

    public Slider StaminaBar;
    public bool endLevel;

    public float slidingMusicTimer;
    private float setSlidingMusicTimer;

    //Method is called when the game starts
    void Start()
    {
        
        endLevel = false;
        int i = GameObject.FindGameObjectsWithTag("Exit").Length;

        setSlidingMusicTimer = slidingMusicTimer;
        slidingMusicTimer = 0;

        exits = new GameObject[i];

        //Finds gameObject with the tag "Goal"
        goal = GameObject.Find("Goal");

        //Finds all the gameObject with the tag "Exit" and sets it in an array
        for (int c = 0; c < i; c++)
        {
            exits[c] = GameObject.Find("Exit (" + c + ")");
        }

        //Gets current scene
        scene = SceneManager.GetActiveScene();

        //Initialise the movement and stamina variables
        staminaLostTime = setStaminaLostTime;
        setStamina = stamina;
       
        StaminaBar.value = CalulateStaminaBar();
        
        //Get RigidBody and animation from player object
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        audioSrc = GetComponent<AudioSource>();
    }
    //Method is called every frame
    void Update()
    {
        isMoving = false;

        //Get inputs direction from players
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(h, v);

        moveVelocity = moveInput.normalized * normalSpeed;

        //Determine which animation to play based on player movement
        if (h != 0 || v != 0)
        {
            isMoving = true;
            lastDirection.x = h;
            lastDirection.y = v;
        }
        SendAnimationInfo();
    }

    //Method is called every fixed framerate frame
    void FixedUpdate()
    {
        //Move the player in the desired direction
        rd.MovePosition(rd.position + moveVelocity * Time.fixedDeltaTime);

        PlayerSliding();

        //Checking if player has picked up the goal
        for(int i = 0; i < exits.Length; i++)
        {
            if (!goalPicked)
            {
                exits[i].GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                exits[i].GetComponent<Collider2D>().isTrigger = false;
            }
        }
        
        slidingMusicTimer -= Time.deltaTime;

    }

    //This method handles the player sliding mechanic
    public void PlayerSliding()
    {
        //Checks if the shift button is pressed
        ShiftDown = Input.GetKey(KeyCode.LeftShift);
        ShiftDownHold = Input.GetKeyDown(KeyCode.LeftShift);


        //If player is holding down the shift button, stamina starts going down
        if (ShiftDown)
        {
    
            //Calculates the players stamina
            staminaGainTime = setStaminaGainTime;
            if (stamina > 0)
            {
                staminaLostTime -= Time.deltaTime;
                if (staminaLostTime < 0)
                {
                    stamina = stamina - 1;
                    StaminaBar.value = CalulateStaminaBar();
                    staminaLostTime = setStaminaLostTime;
                } else {
                    moveVelocity = moveInput.normalized * slidingSpeed;

                    //Player sliding animation is triggered
                    playerSliding = true;
                    rd.MovePosition(rd.position + moveVelocity * Time.fixedDeltaTime);
                    if (ShiftDownHold && playerSliding && !audioSrc.isPlaying && slidingMusicTimer < 0)
                    {
                        audioSrc.PlayOneShot(soundToPlay, .1f);
                        slidingMusicTimer = setSlidingMusicTimer;
                    }
                 
                }
            } else {
                playerSliding = false;
                audioSrc.Stop();
                
            }
            
        }

        //Regen Stamina if player is not sliding
        if (!ShiftDown)
        {
            playerSliding = false;

            staminaLostTime = setStaminaLostTime;
            if (stamina < setStamina)
            {
                staminaGainTime -= Time.deltaTime;
                if (staminaGainTime < 0)
                {
                    stamina = stamina + 1;
                    StaminaBar.value = CalulateStaminaBar();
                    staminaGainTime = setStaminaGainTime;
                    
                }
            }
        }
    }

    //Send the players movement information to the animator
    void SendAnimationInfo()
    {
        anim.SetFloat("XSpeed", moveVelocity.x);
        anim.SetFloat("YSpeed", moveVelocity.y);

        anim.SetFloat("LastX", lastDirection.x);
        anim.SetFloat("LastY", lastDirection.y);

        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("IsSliding", playerSliding);
    }

    //Handle the players collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Goal"))
        {
            //Takes the player back to the menu
            Destroy(goal);
            goalPicked = true;
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            //Restarts the level 
            Debug.Log("hit");
            SceneManager.LoadScene(scene.name);
        }

        //Checks if the player has exited the level
        if (col.gameObject.CompareTag("Exit") && goalPicked)
        {
            endLevel = true; 
        }
    }

    //Calulates the stamina process for the stamina bar
    public float CalulateStaminaBar()
    {
        float newStamina = stamina / 10f;
        return newStamina;
    }
}
