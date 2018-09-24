using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

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
        lastPosition = transform.position;
        transform.position = track.position;
    }

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

    void SendAnimationInfo() {
        anim.SetFloat("XSpeed", velocity.x);
        anim.SetFloat("YSpeed", velocity.y);

        anim.SetFloat("LastX", lastDirection.x);
        anim.SetFloat("LastY", lastDirection.y);

        anim.SetBool("IsMoving", isMoving);
    }
}
