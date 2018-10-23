using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class calculate enemies field of view and calculates the best path to the players for the enemy
 */ 

public class CompMove : MonoBehaviour {

    //Initialise of variables
    public Transform target;
    public float speed = 20;
    public bool move = false;
    public bool isChasing;

    public bool playSound = false;
    public bool doOnce = true;
    public bool startPatrol;
    public bool followingSound;

    private bool lookAtLastPos;

    public float ang;
    public float line;

    public float timer;
    private float setTimer;

    private Vector2 lastPos;

    public bool playerFound;
    private bool exit;

    Vector2[] path;
    int targetIndex;

    private Scene scene;

    //Runs once for each scene
    void Start()
    {
        //Setting the player as target
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       
        startPatrol = true;
        setTimer = timer;

        Physics2D.queriesStartInColliders = false;
        playerFound = false;

        followingSound = false;
        scene = SceneManager.GetActiveScene();
    }

    //Called during each frame
    void Update()
    {
        createLine();
        checkFieldOfView();


    }

    //Creates a debugging line to show the enemys field of view area
    public void createLine()
    {
        Vector2 direction = GetDirectionVector2D(ang);
        Vector2 forward = transform.TransformDirection(direction) * 10;
        Debug.DrawRay(transform.position, forward, Color.red);

        Vector2 direction2 = GetDirectionVector2D(-ang);
        Vector2 forward2 = transform.TransformDirection(direction2) * 10;
        Debug.DrawRay(transform.position, forward2, Color.red);
    }

    //Calculates the area where the enemys can view
    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    //Checks if the player has entered the enemy field of view and will start chasing them 
    public void checkFieldOfView()
    {
        if (Vector3.Angle(transform.right, target.position - transform.position) < ang)
        {
            Vector3 direction1 = (target.transform.position - transform.position).normalized;

            //Casts a line to the player if they are in the field of view
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction1, line);

            //checks if the hitinfo is touching the player 
            if (hitInfo.collider != null)
            {
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);

                if (hitInfo.collider.CompareTag("Player"))
                {
                    startPatrol = false;
                    timer = 5;

                    Debug.DrawLine(transform.position, hitInfo.point, Color.blue);
                    
                    move = true;

                    //Runs once per frame till reset
                    if (doOnce)
                    {
                        playSound = true;
                        doOnce = false;
                    }
                    else
                    {
                        playSound = false;
                    }

                    //Calls Enumerator 
                    StartCoroutine(RefreshPath());

                    //Initialise variables
                    playerFound = true;
                    isChasing = true;
                    exit = true;
                 
                }
                //If player is not found or lost returns back to patroling
                else
                {
                    move = false;
                    lookAtLastPos = true;

                    //Finds players last postion seen
                    if (exit)
                    {
                        lastPos = target.position;
                        exit = false;
                    }
                }
            }
        }

        //Sets enemy patroling if they are doing nothing
        if(!isChasing && !startPatrol && !playerFound && !followingSound)
        {
            startPatrol = true;
        }
    }

    //Calculates the path the enemy needs to take in order reach player
    IEnumerator RefreshPath()
    {
        Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially

        //Checks if the enemy is moving 
        while (move)
        {
            move = false;
            if (targetPositionOld != (Vector2)target.position)
            {
                targetPositionOld = (Vector2)target.position;

                path = Pathfinding.RequestPath(transform.position, target.position);
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }

            yield return new WaitForSeconds(.25f);
        }
    }

    //Moves in the path set to find the target
    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            targetIndex = 0;
            Vector2 currentWaypoint = path[0];

            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                //Moves the enemy field of view over time
                if (lookAtLastPos)
                {
                    Vector3 dir = currentWaypoint - (Vector2)transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);

                    //Checks if the enemy is looking at the targets last seen postion
                    if ((Vector2)transform.position == currentWaypoint || playerFound)
                    {
                        lookAtLastPos = false;
                        playerFound = false;
                    }
                }
                else
                {
                    float z = Mathf.Atan2((target.transform.position.y - transform.position.y), (target.transform.position.x - transform.position.x))
                       * Mathf.Rad2Deg - 5;

                    transform.eulerAngles = new Vector3(0, 0, z);
                }

                //Calculates the distances between the enemy and the players last seen postion
                if (Vector2.Distance(transform.position, lastPos) < 1f && playerFound == false)
                {
                    //Wait till timer is 0 then moves back to patrol state
                    if (timer < 0)
                    {
                        startPatrol = true;
                        move = false;
                        isChasing = false;
                        timer = setTimer;
                        playerFound = false;
                        playSound = false;
                        doOnce = true;
                        yield break;
                    }
                    //While timer is running the enemy will rotate to check for the player
                    else
                    {
                        transform.Rotate(Vector3.forward, 370 * Time.deltaTime);
                        Debug.Log("player lost");
                        timer -= Time.deltaTime;
                    }
                }
                else
                {
                    //The movement of the enemy toward the player following the set path
                    transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                }
                //Checks if the enemy is touching the player.
                if (Vector2.Distance(transform.position, target.position) < .4f)
                {
                    //restarts the current level
                    SceneManager.LoadScene(scene.name);
                }
                yield return null;

            }
        }
    }

    //Shows the enemys path for debugging 
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                //Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}
