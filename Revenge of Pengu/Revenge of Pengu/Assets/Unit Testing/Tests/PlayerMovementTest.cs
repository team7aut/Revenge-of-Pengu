using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


public class PlayerMovementTest
{

    [Test]
    public void playerMove_test()
    {
        var playermove = new playermovement();

        //Arrange
        var speed = 5;
        var h = 1;
        var v = -1;

        var expectedMovement = new Vector2(h, v).normalized * speed;

        //Act
        var points = playermove.getInput(h, v);

        //Assert
        Assert.That(points, Is.EqualTo(expectedMovement));
   
    }

    [Test]
    public void playerRunning_test()
    {
        var playermove = new playermovement();
      
        //Arrange
        var h = 1;
        var v = -1;
        var fastSpeed = 10;
        var isRunning = true;

        //Act
        var expectedFastMovement = new Vector2(h, v).normalized * fastSpeed;

        //Assert
        var points2 = playermove.runMode(h, v, isRunning);
        Assert.That(points2, Is.EqualTo(expectedFastMovement));
    }

    [Test]
    public void moveUp_Test()
    {
        var playermove = new playermovement();

        var w_button = 1;

        var exectedDirction = 1;

        var upDirction = playermove.moveUp(w_button);

        Assert.That(exectedDirction, Is.EqualTo(upDirction));

    }

    [Test]
    public void moveDown_Test()
    {
        var playermove = new playermovement();

        var s_button = -1;

        var exectedDirction = -1;

        var downDirction = playermove.moveUp(s_button);

        Assert.That(exectedDirction, Is.EqualTo(downDirction));

    }

    [Test]
    public void moveLeft_Test()
    {
        var playermove = new playermovement();

        var a_button = -1;

        var exectedDirction = -1;

        var leftDirction = playermove.moveUp(a_button);

        Assert.That(exectedDirction, Is.EqualTo(leftDirction));

    }

    [Test]
    public void moveright_Test()
    {
        var playermove = new playermovement();

        var d_button = 1;

        var exectedDirction = 1;

        var rightDirction = playermove.moveUp(d_button);

        Assert.That(exectedDirction, Is.EqualTo(rightDirction));

    }


}

