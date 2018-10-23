using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GoalReach_test {

    [Test]
    public void goalreach_test()
    {

        var goal = new GoalReach();   
        var goalReached = true;

        var exectedResults = true;

        var exitUnlocked = goal.playerHasGoal(goalReached);

        Assert.That(exectedResults, Is.EqualTo(exitUnlocked));
    }

	[Test]
    public void exitOpen_test()
    {
        var exit = new GoalReach();

        var exitUnlocked = true;

        var exitOpen = exit.openExit(exitUnlocked);

        var exectedResults = true;

        Assert.That(exectedResults, Is.EqualTo(exitUnlocked));





    }


}
