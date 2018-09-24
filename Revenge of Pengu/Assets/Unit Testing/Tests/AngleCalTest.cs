using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class AngleCalTest {

    [Test]
    public void expectedCheckFieldOfView() {

        var GetDirectionVector = new AngleCal();

        //Arrange 
        var angleSize = 60;
        var expectedCheckFieldOfView = new Vector2(Mathf.Cos(angleSize * Mathf.Deg2Rad), Mathf.Sin(angleSize * Mathf.Deg2Rad)).normalized;

        //Act
        var checkFieldOfView = GetDirectionVector.GetDirectionVector2D(angleSize);

        //Assert
        Assert.That(checkFieldOfView, Is.EqualTo(expectedCheckFieldOfView));
    }

    [Test]
    public void PlayerinSight_test()
    {
        var GetDirectionVector = new AngleCal();

        var playerLoction = new Vector3(-5f, 5f, 0);
        var enemyLoction = new Vector3(-5.6f, 3.9f, 0);
        var angleSize = 70f;
        var expectedresultFollowPlayer = true;

        var followPlayer = GetDirectionVector.checkfieldOfView(playerLoction, enemyLoction, angleSize);


        Assert.That(expectedresultFollowPlayer, Is.EqualTo(followPlayer));
    }

}
