using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSound : MonoBehaviour {

    public float timer;
    private CompMove compMove;


    private bool doOnce = true;
    public bool broken;
    public float nosieDectionRange;

    private float speed;

    public GameObject bottle;
   
    private Vector3 bottleLoc;

   

    Vector2[] path;
    int targetIndex;

    void Awake()
    {
        compMove = gameObject.GetComponent<CompMove>();

    }


    // Use this for initialization
    void Start () {

        speed = compMove.speed;

    }   
	
	// Update is called once per frame
	void Update () {

        bottleLoc = bottle.GetComponent<Throwable>().target;

        float dis = Vector2.Distance(transform.position, bottleLoc);

       

        if (doOnce)
        {
            if (bottle.GetComponent<Throwable>().broken)
            {
                if (dis < nosieDectionRange)
                {
                    compMove.startPatrol = false;
                    StartCoroutine(RefreshPath());
                    doOnce = false;
                }
                else
                {
                    doOnce = false;
                }
            }
          
        }


	}


    IEnumerator RefreshPath()
    {
        Vector2 targetPositionOld = (Vector2)bottleLoc + Vector2.up; // ensure != to target.position initially

        while (true)
        {
            if (targetPositionOld != (Vector2)bottleLoc)
            {
                targetPositionOld = (Vector2)bottleLoc;

                path = Pathfinding.RequestPath(transform.position, bottleLoc);
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
                if (compMove.isChasing)
                {
                        yield break;
                }
                else
                {
                    if (Vector2.Distance(transform.position, bottleLoc) < 1)
                    {
                        if (timer < 0)
                        {
                            compMove.startPatrol = true;
                            yield break;
                        }
                        else
                        {
                            transform.Rotate(Vector3.forward, 40 * Time.deltaTime);

                            timer -= Time.deltaTime;
                        }
                    }
                    else
                    {

                        Vector3 dir = currentWaypoint - (Vector2)transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);

                        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                    }
                    yield return null;
                }

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
