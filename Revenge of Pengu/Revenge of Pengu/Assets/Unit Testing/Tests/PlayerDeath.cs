using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath{

    public bool playerDeath(bool playerAlive, bool collision)
    {
        if (playerAlive)
        {
            if (collision)
            {
                playerAlive = false;
                return playerAlive;
            }

            else
            {
                return playerAlive;
            }
        }
        else
        {
            return playerAlive;
        }

    }

    public int levelRestrat(int currentLevel, bool playerDeath)
    {
        if (playerDeath)
        {
            return currentLevel;
        }
        else
        {
            return 0;
        }
    }

	
}
