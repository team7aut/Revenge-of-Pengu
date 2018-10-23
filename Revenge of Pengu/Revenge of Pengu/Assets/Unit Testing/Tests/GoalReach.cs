using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReach{


    public bool playerHasGoal(bool goal)
    {

        if (goal)
        {
            return true;
        }

        return false;
    }


    public bool openExit(bool goal)
    {
        if (goal)
        {
            return true;
        }
        return false;
    }

}
