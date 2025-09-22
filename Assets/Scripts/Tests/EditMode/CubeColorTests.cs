using NUnit.Framework;
using UnityEngine;

public class CubeColorTests
{
    private GameObject testGameObject;
    private CubeColor cubeColor;

    [SetUp]
    public void SetUp()
    {
        testGameObject = new GameObject("TestCube");
        testGameObject.AddComponent<MeshRenderer>();
        cubeColor = testGameObject.AddComponent<CubeColor>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testGameObject);
    }

    [Test]
    public void CubeColor_ColorsArray_HasCorrectLength()
    {
        var colorsField = cubeColor.GetType()
            .GetField("colors", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var colors = (Color[])colorsField.GetValue(cubeColor);

        Assert.AreEqual(7, colors.Length);
    }

    [Test]
    public void CubeColor_ColorsArray_ContainsExpectedColors()
    {
        var colorsField = cubeColor.GetType()
            .GetField("colors", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var colors = (Color[])colorsField.GetValue(cubeColor);

        Assert.AreEqual(Color.red, colors[0]);
        Assert.AreEqual(Color.yellow, colors[2]);
        Assert.AreEqual(Color.green, colors[3]);
        Assert.AreEqual(Color.blue, colors[4]);
    }

    [Test]
    public void CubeColor_ColorIndexProgression_CyclesCorrectly()
    {
        var currentColorIndexField = cubeColor.GetType()
            .GetField("currentColorIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        currentColorIndexField.SetValue(cubeColor, 6);

        int nextIndex = (6 + 1) % 7;
        Assert.AreEqual(0, nextIndex);
    }

    [Test]
    public void CubeColor_InitialColorIndex_IsZero()
    {
        var currentColorIndexField = cubeColor.GetType()
            .GetField("currentColorIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.AreEqual(0, currentColorIndexField.GetValue(cubeColor));
    }
}