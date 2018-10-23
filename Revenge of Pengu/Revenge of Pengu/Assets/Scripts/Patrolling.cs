using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    private Patrol PatrolScript;
    private GameObject Patrol;
    public int patrolNumber;
    private CompMove compMove;
    private bool isPatrolling;
    private int currentTarget;
    private bool setPath;
    private bool chase;
    Vector2[] path;
    int targetIndex;
    bool noPatrol;
    public int turningSpeed;
  
    private Vector3 curPos;
    private Vector3 lastPos;


    // Use this for initialization
    void Start()
    {
       
        if (patrolNumber == 0)
        {
            noPatrol = true;

        } else
        {

            Patrol = GameObject.Find("Patrol (" + patrolNumber + ")");
        }

        PatrolScript = Patrol.GetComponent<Patrol>();
        compMove = gameObject.GetComponent<CompMove>();
        currentTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        isPatrolling = compMove.GetComponent<CompMove>().startPatrol;
        if (!noPatrol)
        {
          
            //Debug.Log("Current Target: " + currentTarget);
            if (isPatrolling)
            {
                if (currentTarget >= Patrol.transform.childCount)
                {
                    currentTarget = 0;
                }
                else
                {
                    if (Vector2.Distance(transform.position, PatrolScript.patrolPoints[currentTarget].transform.position) < .8f)
                    {

                        ++currentTarget;
                    }
                    setPath = true;
                    StartCoroutine(RefreshPath());
                    
                }
            }
        }

        
        curPos = transform.position;
        if (curPos == lastPos && isPatrolling && !PauseMenu.gameIsPause)
        {
            if (Vector2.Distance(transform.position, compMove.target.position) > 8)
            {
                transform.position = PatrolScript.patrolPoints[currentTarget].transform.position;
            }
            Debug.Log("Not moving");
        }
        lastPos = curPos;


    }


    IEnumerator RefreshPath()
    {

        if (!isPatrolling)
        {
           
            yield break;
        }

        if (currentTarget >= Patrol.transform.childCount)
        {
            currentTarget = 0;
        }
        Vector2 targetPositionOld = (Vector2)PatrolScript.patrolPoints[currentTarget].transform.position + Vector2.up; // ensure != to target.position initially

        // mod true statement 
        while (setPath)
        {
            setPath = false;
            if (targetPositionOld != (Vector2)PatrolScript.patrolPoints[currentTarget].transform.position)
            {
                targetPositionOld = (Vector2)PatrolScript.patrolPoints[currentTarget].transform.position;

                path = Pathfinding.RequestPath(transform.position, PatrolScript.patrolPoints[currentTarget].transform.position);
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
                if ((Vector2)PatrolScript.patrolPoints[currentTarget].transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                       
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                //this part is for movement
                /*
                       float z = Mathf.Atan2((currentWaypoint.y - transform.position.y), (currentWaypoint.x - transform.position.x))
                          * Mathf.Rad2Deg - 10;

                       transform.eulerAngles = new Vector3(0, 0, z);
              */

                Vector3 dir = currentWaypoint - (Vector2)transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turningSpeed);

               // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


                if (isPatrolling == false)
                {
                    yield break;

                }
                else
                {
                   
                    transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, PatrolScript.patrolSpeed * Time.deltaTime);
                }
                
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
