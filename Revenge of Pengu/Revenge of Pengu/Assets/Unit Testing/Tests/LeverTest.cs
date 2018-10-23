using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LeverTest
{

    [Test]
    public void LeverOpen_test()
    {
        var lever = new Lever();

        //Arrange
        var isOff = true;
        var exectedResultsplayerWalkAble = true;
     
        var checkWalkableArea = lever.openDoor(isOff);

        Assert.That(exectedResultsplayerWalkAble, Is.EqualTo(checkWalkableArea));

    }

    [Test]
    public void LeverClose_test()
    {
        var lever = new Lever();

        //Arrange
        var playerWalkAble = false;
        var swicthOff = true;

        var checkWalkable = lever.closedDoor(swicthOff);

        Assert.That(checkWalkable, Is.EqualTo(playerWalkAble));
    }


}
