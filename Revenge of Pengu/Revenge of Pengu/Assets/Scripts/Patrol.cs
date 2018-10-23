using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Finds all the patrol points in the and adds it a list
 */

public class Patrol : MonoBehaviour
{

    public List<GameObject> patrolPoints = new List<GameObject>();
    public float patrolSpeed;

    void Start()
    {
        foreach (Transform child in transform)
        {
            patrolPoints.Add(child.gameObject);
        }
    }
}