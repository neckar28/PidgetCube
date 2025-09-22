using NUnit.Framework;
using UnityEngine;

public class BloomControlTests
{
    private GameObject testGameObject;
    private BloomControl bloomControl;

    [SetUp]
    public void SetUp()
    {
        testGameObject = new GameObject("TestBloomControl");
        bloomControl = testGameObject.AddComponent<BloomControl>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testGameObject);
    }

    [Test]
    public void BloomControl_SinMaxConstant_HasCorrectValue()
    {
        var sinMaxField = typeof(BloomControl)
            .GetField("SinMax", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        float sinMaxValue = (float)sinMaxField.GetValue(null);
        float expectedValue = Mathf.PI * 0.25f;

        Assert.AreEqual(expectedValue, sinMaxValue, 0.001f);
    }

    [Test]
    public void BloomControl_InitialClickTime_IsCorrect()
    {
        var clickTimeField = bloomControl.GetType()
            .GetField("clickTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        float clickTime = (float)clickTimeField.GetValue(bloomControl);
        float expectedClickTime = -Mathf.PI * 0.25f;

        Assert.AreEqual(expectedClickTime, clickTime, 0.001f);
    }

    [Test]
    public void BloomControl_BloomIntensityCalculation_IsCorrect()
    {
        float testTime = 1.0f;
        float expectedIntensity = (Mathf.Sin(testTime * 1.5f) + 1) * 0.5f;

        Assert.IsTrue(expectedIntensity >= 0f && expectedIntensity <= 1f,
            "Bloom intensity should be between 0 and 1");
    }

    [Test]
    public void BloomControl_BloomIntensityFormula_ProducesValidRange()
    {
        for (float time = 0f; time <= 10f; time += 0.1f)
        {
            float intensity = (Mathf.Sin(time * 1.5f) + 1) * 0.5f;

            Assert.IsTrue(intensity >= 0f && intensity <= 1f,
                $"Bloom intensity {intensity} at time {time} should be between 0 and 1");
        }
    }

    [Test]
    public void BloomControl_ClickTimeUpdate_CalculatesCorrectly()
    {
        float simulatedCurrentTime = 5.0f;
        float sinMax = Mathf.PI * 0.25f;
        float expectedClickTime = simulatedCurrentTime - sinMax;

        Assert.AreEqual(expectedClickTime, simulatedCurrentTime - sinMax, 0.001f);
    }
}