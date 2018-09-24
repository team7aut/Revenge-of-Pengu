using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    private Transform player;
    private SpriteRenderer spriteRend;
    private Sprite sprite;
    public Sprite brokenSprite;
    public Vector2 target;
    private bool thrown;
    public bool pickedUp;
    public bool broken;
    public float range;

    public static Throwable instance;

    void Start () {
        spriteRend = GetComponent<SpriteRenderer>();
        sprite = spriteRend.sprite;
        thrown = false;
        pickedUp = false;
        broken = false;
	}

    void Update() {
        if (!broken)
        {
            if (Input.GetMouseButtonDown(0) && pickedUp)
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float dist = Vector2.Distance(target, transform.position);

                if (dist < range)
                {
                    pickedUp = false;
                    thrown = true;
                    spriteRend.sprite = sprite;
                }
            }
            if (!thrown)
            {
                if (pickedUp)
                {
                    transform.position = player.position;
                    
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, 0.2f);
                Vector2 currentPos = transform.position;
                if (currentPos == target)
                {
                    thrown = false;
                    broken = true;
                    spriteRend.sprite = brokenSprite;
                }
                pickedUp = false;
                transform.Rotate(0, 0, 2000 * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !broken)
        {
            spriteRend.sprite = null;
            pickedUp = true;
            player = other.transform;
        }
    }
}
