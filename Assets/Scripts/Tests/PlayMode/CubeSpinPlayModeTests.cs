using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CubeSpinPlayModeTests
{
    private GameObject testGameObject;
    private CubeSpin cubeSpin;

    [SetUp]
    public void SetUp()
    {
        testGameObject = new GameObject("TestCube");
        cubeSpin = testGameObject.AddComponent<CubeSpin>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testGameObject);
    }

    [UnityTest]
    public IEnumerator CubeSpin_Update_RotatesObject()
    {
        Vector3 initialRotation = testGameObject.transform.eulerAngles;

        cubeSpin.Start();

        yield return new WaitForSeconds(0.1f);

        cubeSpin.Update();

        Vector3 finalRotation = testGameObject.transform.eulerAngles;

        Assert.AreNotEqual(initialRotation.y, finalRotation.y, "Cube should have rotated");
    }

    [UnityTest]
    public IEnumerator CubeSpin_RotationSpeed_RecoveresToDefault()
    {
        cubeSpin.defaultRotationSpeed = 50f;
        cubeSpin.onMouseDownRatio = 10f;
        cubeSpin.rotationRecoverSpeed = 20f;

        cubeSpin.Start();

        var rotationSpeedField = cubeSpin.GetType()
            .GetField("rotationSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        rotationSpeedField.SetValue(cubeSpin, 500f);

        for (int i = 0; i < 10; i++)
        {
            cubeSpin.Update();
            yield return null;
        }

        float currentSpeed = (float)rotationSpeedField.GetValue(cubeSpin);

        Assert.Less(currentSpeed, 500f, "Rotation speed should decrease over time");
        Assert.Greater(currentSpeed, 50f, "Rotation speed should not instantly become default");
    }

    [Test]
    public void CubeSpin_MouseDown_IncreasesRotationSpeed()
    {
        cubeSpin.defaultRotationSpeed = 50f;
        cubeSpin.onMouseDownRatio = 10f;

        cubeSpin.Start();

        var rotationSpeedField = cubeSpin.GetType()
            .GetField("rotationSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var mouseDownMethod = cubeSpin.GetType()
            .GetMethod("Update", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        rotationSpeedField.SetValue(cubeSpin, 500f);

        float expectedSpeed = cubeSpin.defaultRotationSpeed * cubeSpin.onMouseDownRatio;
        Assert.AreEqual(500f, expectedSpeed);
    }
}