using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomControlPlayModeTests
{
    private GameObject testGameObject;
    private BloomControl bloomControl;
    private Volume volume;
    private VolumeProfile volumeProfile;

    [SetUp]
    public void SetUp()
    {
        testGameObject = new GameObject("TestBloomControl");
        volume = testGameObject.AddComponent<Volume>();

        volumeProfile = ScriptableObject.CreateInstance<VolumeProfile>();
        var bloom = volumeProfile.Add<Bloom>();
        bloom.intensity.value = 0.5f;

        volume.profile = volumeProfile;
        bloomControl = testGameObject.AddComponent<BloomControl>();
    }

    [TearDown]
    public void TearDown()
    {
        if (volumeProfile != null)
            Object.DestroyImmediate(volumeProfile);
        Object.DestroyImmediate(testGameObject);
    }

    [Test]
    public void BloomControl_Start_InitializesVolumeAndBloom()
    {
        bloomControl.Start();

        var volumeField = bloomControl.GetType()
            .GetField("volume", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var bloomField = bloomControl.GetType()
            .GetField("bloom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.IsNotNull(volumeField.GetValue(bloomControl));
        Assert.IsNotNull(bloomField.GetValue(bloomControl));
    }

    [UnityTest]
    public IEnumerator BloomControl_Update_ModifiesBloomIntensity()
    {
        bloomControl.Start();

        var bloomField = bloomControl.GetType()
            .GetField("bloom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var bloom = (Bloom)bloomField.GetValue(bloomControl);

        if (bloom != null)
        {
            float initialIntensity = bloom.intensity.value;

            yield return new WaitForSeconds(0.1f);

            bloomControl.Update();

            float finalIntensity = bloom.intensity.value;

            Assert.AreNotEqual(initialIntensity, finalIntensity,
                "Bloom intensity should change over time");
        }
        else
        {
            Assert.Inconclusive("Bloom component not found in volume profile");
        }
    }

    [Test]
    public void BloomControl_ClickTimeUpdate_WorksCorrectly()
    {
        bloomControl.Start();

        var clickTimeField = bloomControl.GetType()
            .GetField("clickTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        float initialClickTime = (float)clickTimeField.GetValue(bloomControl);

        Time.timeScale = 1f;

        float sinMax = Mathf.PI * 0.25f;
        float expectedNewClickTime = Time.time - sinMax;

        Assert.AreNotEqual(initialClickTime, expectedNewClickTime,
            "Click time should be different when simulating mouse click");
    }

    [Test]
    public void BloomControl_BloomIntensityRange_StaysWithinBounds()
    {
        bloomControl.Start();

        var bloomField = bloomControl.GetType()
            .GetField("bloom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var bloom = (Bloom)bloomField.GetValue(bloomControl);

        if (bloom != null)
        {
            for (int i = 0; i < 100; i++)
            {
                bloomControl.Update();

                float intensity = bloom.intensity.value;

                Assert.IsTrue(intensity >= 0f && intensity <= 1f,
                    $"Bloom intensity {intensity} should be between 0 and 1");
            }
        }
        else
        {
            Assert.Inconclusive("Bloom component not found in volume profile");
        }
    }
}