using NUnit.Framework;
using UnityEngine;

public class CubeSpinTests
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

    [Test]
    public void CubeSpin_DefaultValues_AreSetCorrectly()
    {
        Assert.AreEqual(50f, cubeSpin.defaultRotationSpeed);
        Assert.AreEqual(60f, cubeSpin.onMouseDownRatio);
        Assert.AreEqual(7f, cubeSpin.rotationRecoverSpeed);
    }

    [Test]
    public void CubeSpin_Start_InitializesRotationSpeed()
    {
        cubeSpin.Start();

        Assert.AreEqual(cubeSpin.defaultRotationSpeed,
            cubeSpin.GetType()
                .GetField("rotationSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(cubeSpin));
    }

    [Test]
    public void CubeSpin_RotationSpeedCalculation_IsCorrect()
    {
        cubeSpin.defaultRotationSpeed = 100f;
        cubeSpin.onMouseDownRatio = 2f;

        float expectedSpeedOnClick = cubeSpin.defaultRotationSpeed * cubeSpin.onMouseDownRatio;

        Assert.AreEqual(200f, expectedSpeedOnClick);
    }
}