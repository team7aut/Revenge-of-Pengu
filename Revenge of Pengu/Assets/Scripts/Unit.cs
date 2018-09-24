using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public Transform target;
	public float speed = 20;

	Vector2[] path;
	int targetIndex;

    public float ang;
    public float line;

    private bool playerFound;

    void Start() {
        //StartCoroutine (RefreshPath ());

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

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
                    Debug.DrawLine(transform.position, hitInfo.point, Color.blue);
                    Debug.Log("Player found");


                    StartCoroutine(RefreshPath());

                    playerFound = true;
                
                }
                else
                {
                    if (playerFound)
                    {
                        
                    }
                }
            }
        }
    }







    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }





























    IEnumerator RefreshPath() {
		Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially
			
		while (playerFound) {
            playerFound = false;
            if (targetPositionOld != (Vector2)target.position) {
				targetPositionOld = (Vector2)target.position;

				path = Pathfinding.RequestPath (transform.position, target.position);
				StopCoroutine ("FollowPath");
				StartCoroutine ("FollowPath");
			}

			yield return new WaitForSeconds (.25f);
		}
	}
		
	IEnumerator FollowPath() {
		if (path.Length > 0) {
			targetIndex = 0;
			Vector2 currentWaypoint = path [0];

			while (true) {
				if ((Vector2)transform.position == currentWaypoint) {
					targetIndex++;
					if (targetIndex >= path.Length) {
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}
                float z = Mathf.Atan2((target.transform.position.y - transform.position.y), (target.transform.position.x - transform.position.x))
                   * Mathf.Rad2Deg - 20;

                transform.eulerAngles = new Vector3(0, 0, z);
                transform.position = Vector2.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;

			}
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				//Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
