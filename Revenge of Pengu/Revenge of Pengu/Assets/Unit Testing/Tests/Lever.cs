using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever {


    public bool openDoor(bool switchOn)
    {
        if (switchOn)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool closedDoor(bool switchOff)
    {
        if (switchOff)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
