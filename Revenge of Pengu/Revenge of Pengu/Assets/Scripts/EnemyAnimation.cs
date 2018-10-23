using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

    //Initialise of variables
    private Animator anim;
    private Vector2 lastDirection;
    private Vector3 lastPosition;
    private Vector2 velocity;
    private bool isMoving;
    public Transform track;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
    }
 
    void Update() {
        //Stores the last postion per frame
        lastPosition = transform.position;
        transform.position = track.position;
    }

    //Frame called at the very end
    void LateUpdate() {
        
        velocity = (lastPosition - transform.position) / Time.deltaTime;

        isMoving = false;

        var h = velocity.x;
        var v = velocity.y;

        if (h != 0 || v != 0)
        {
            isMoving = true;
            lastDirection.x = h;
            lastDirection.y = v;
        }
        SendAnimationInfo();
    }

    //Calls the animation which is need to be show
    void SendAnimationInfo() {
        anim.SetFloat("XSpeed", velocity.x);
        anim.SetFloat("YSpeed", velocity.y);

        anim.SetFloat("LastX", lastDirection.x);
        anim.SetFloat("LastY", lastDirection.y);

        anim.SetBool("IsMoving", isMoving);
    }
}
