using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomControl : MonoBehaviour
{
	private Volume volume;
	private Bloom bloom;


	private const float SinMax = Mathf.PI * 0.25f;
	private float clickTime = -SinMax;

	public void Start()
	{
		volume = GetComponent<Volume>();
		volume.profile.TryGet<Bloom>(out bloom);
	}

	public void Update()
	{
		float currentTime = Time.time - clickTime;

		if(bloom != null)
		{
			bloom.intensity.value = (Mathf.Sin(currentTime * 1.5f) + 1) * 0.5f;
		}

		if (Input.GetMouseButtonDown(0))
		{
			clickTime = Time.time - SinMax;
		}
	}
}
