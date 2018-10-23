using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerDeathTest {

    [Test]
    public void playerDeath_test()
    {
        var playerDeath = new PlayerDeath();

        var playerAlive = true;
        var enemyTouchedPlayer = true;

        var expectedOutCome = true;

        var checkPlayerIsAlive = playerDeath.playerDeath(playerAlive, enemyTouchedPlayer);

        Assert.That(expectedOutCome, Is.EqualTo(!checkPlayerIsAlive));
    
    }

    [Test]
    public void levelRestrat_test()
    {
        var playerDeath = new PlayerDeath();

        var playerDead = true;
        var currentlevel = 1;

        var exceptedLevel = 1;

        var restartLevel = playerDeath.levelRestrat(currentlevel, playerDead);

        Assert.That(exceptedLevel, Is.EqualTo(restartLevel));




    }

}
