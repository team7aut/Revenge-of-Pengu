using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompMove : MonoBehaviour {

    public Transform target;
    public float speed = 20;
    public bool move = false;
    public bool isChasing;

    public bool startPatrol;

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

    void Start()
    {
        //StartCoroutine(RefreshPath());
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        startPatrol = true;
        setTimer = timer;

        Physics2D.queriesStartInColliders = false;
        playerFound = false;
        
    }

    void Update()
    {
        createLine();
        checkFieldOfView();


    }

    public void createLine()
    {
        Vector2 direction = GetDirectionVector2D(ang);
        Vector2 forward = transform.TransformDirection(direction) * 10;
        Debug.DrawRay(transform.position, forward, Color.red);

        Vector2 direction2 = GetDirectionVector2D(-ang);
        Vector2 forward2 = transform.TransformDirection(direction2) * 10;
        Debug.DrawRay(transform.position, forward2, Color.red);
    }

    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    public void checkFieldOfView()
    {
        if (Vector3.Angle(transform.right, target.position - transform.position) < ang)
        {
            Vector3 direction1 = (target.transform.position - transform.position).normalized;

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction1, line);

            if (hitInfo.collider != null)
            {
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);

                if (hitInfo.collider.CompareTag("Player"))
                {
                    startPatrol = false;
                    timer = 5;

                    Debug.DrawLine(transform.position, hitInfo.point, Color.blue);
                    //Debug.Log("Player found");

                    move = true;
                 //   Debug.Log(true);
                    StartCoroutine(RefreshPath());
 
                    playerFound = true;
                    isChasing = true;
                    exit = true;
                 
                }
                else
                {
                    move = false;
                    lookAtLastPos = true;
                    if (exit)
                    {
                        lastPos = target.position;
                        exit = false;
                    }
                }
            }
        }



    }

    IEnumerator RefreshPath()
    {
        Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially

        // mod true statement 
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

                //this part is for movement
                
                if (lookAtLastPos)
                {
                    float z = Mathf.Atan2((currentWaypoint.y - transform.position.y), (currentWaypoint.x - transform.position.x))
                       * Mathf.Rad2Deg - 5;

                    transform.eulerAngles = new Vector3(0, 0, z);
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
                    
              
                if (Vector2.Distance(transform.position, lastPos) < 1f && playerFound == false)
                {
                    Debug.Log("test");
                    if (timer < 0)
                    {
                        startPatrol = true;
                        move = false;
                        isChasing = false;
                        timer = setTimer;
                        playerFound = false;
                        yield break;

                    }
                    else
                    {
                        timer -= Time.deltaTime;
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                }
                //Add animation code above
                yield return null;

            }
        }
    }

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
