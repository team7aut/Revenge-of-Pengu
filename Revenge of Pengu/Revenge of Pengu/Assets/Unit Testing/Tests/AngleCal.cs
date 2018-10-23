using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCal{

    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    public bool checkfieldOfView(Vector3 playerPos, Vector3 enemyPos, float ang)
    {
        if (Vector3.Angle(new Vector3(1,0,0), playerPos -enemyPos) < ang)
        {
            return true;
        }

        else
        {
            return false;
        }
    }




}
