using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement{

    public Vector2 getInput(float h, float v)
    {
        float normalSpeed = 5;
        Vector2 moveInput = new Vector2(h, v);

        Vector2 moveVelocity = moveInput.normalized * normalSpeed;

        return  moveVelocity;
    }

    public Vector2 runMode(float h, float v, bool isRun)
    {
        Vector2 moveInput = new Vector2(h, v);
        if (isRun)
        {
            float fastSpeed = 10;
           
            Vector2 moveVelocity = moveInput.normalized * fastSpeed;
            return moveVelocity;
        }
        else
        {
            return moveInput;
        }
        
    }

    public int moveUp(int upInput)
    {
        if (upInput == 1)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    public int moveLeft(int leftInput)
    {
        if (leftInput == 1)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
    public int moveright(int rightInput)
    {
        if (rightInput == 1)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    public int moveDown(int downInput)
    {
        if (downInput == 1)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

}
