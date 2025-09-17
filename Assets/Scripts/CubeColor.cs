using UnityEngine;

public class CubeColor : MonoBehaviour
{
	private Color[] colors = {
		Color.red,
		new Color(1f, 0.5f, 0f),
		Color.yellow,
		Color.green,
		Color.blue,
		new Color(0.3f, 0f, 1f),
		new Color(0.5f, 0f, 1f),
	};

	private Material material;
	private int currentColorIndex = 0;


	public void Start()
	{
		material = GetComponent<Renderer>().material;	
		material.color = colors[0];
		material.SetColor("_EmissionColor", colors[0]);
	}

	public void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			currentColorIndex = (currentColorIndex + 1) % colors.Length;
			material.color = colors[currentColorIndex];
			material.SetColor("_EmissionColor", colors[currentColorIndex]);
		}
	}
}
