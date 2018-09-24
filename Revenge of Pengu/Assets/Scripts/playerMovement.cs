using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour {

    public float normalSpeed;
    public float slidingSpeed;

    public GameObject goal;

    private float staminaLostTime;
    public float setStaminaLostTime;

    private float staminaGainTime;
    public float setStaminaGainTime;

    private Vector2 moveInput;

    public int stamina;
    private int setStamina;

    private Rigidbody2D rd;
    private Vector2 moveVelocity;

    private Animator anim;
    private Vector2 lastDirection;
    private bool isMoving;
    bool ShiftDown;
    bool playerSliding;

    // Use this for initialization
    void Start()
    {
        staminaLostTime = setStaminaLostTime;
        
        staminaGainTime = setStaminaGainTime;

        setStamina = stamina;

        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    
    void Update()
    {
        isMoving = false;

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(h, v);

        moveVelocity = moveInput.normalized * normalSpeed;

        if (h != 0 || v != 0)
        {
            isMoving = true;
            lastDirection.x = h;
            lastDirection.y = v;
        }
        SendAnimationInfo();
    }

    void FixedUpdate()
    {
        rd.MovePosition(rd.position + moveVelocity * Time.fixedDeltaTime);

        PlayerRuning();
    }

    public void PlayerRuning()
    {
        ShiftDown = Input.GetKey(KeyCode.LeftShift);

        if (ShiftDown)
        {
            staminaGainTime = setStaminaGainTime;
            if (stamina > 0)
            {
                staminaLostTime -= Time.deltaTime;
                if (staminaLostTime < 0)
                {
                    stamina = stamina - 1;
                    staminaLostTime = setStaminaLostTime;
                }
                else
                {
                    moveVelocity = moveInput.normalized * slidingSpeed;
                    playerSliding = true;
                    rd.MovePosition(rd.position + moveVelocity * Time.fixedDeltaTime);
                }
            }
            else
            {
                playerSliding = false;
            }
        }

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
                    staminaGainTime = setStaminaGainTime;
                }
            }
        }
    }

    void SendAnimationInfo()
    {
        anim.SetFloat("XSpeed", moveVelocity.x);
        anim.SetFloat("YSpeed", moveVelocity.y);

        anim.SetFloat("LastX", lastDirection.x);
        anim.SetFloat("LastY", lastDirection.y);

        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("IsSliding", playerSliding);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Goal"))
        {

            Destroy(goal);
            SceneManager.LoadScene("MenuNew");
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("MapTesting");
        }

    }




}
