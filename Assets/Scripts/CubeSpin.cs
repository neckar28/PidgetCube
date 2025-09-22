using UnityEngine;
using System.Collections;

public class CubeSpin : MonoBehaviour
{
    public float defaultRotationSpeed = 50f;
	public float onMouseDownRatio = 60f;
	public float rotationRecoverSpeed = 7f;

    private float rotationSpeed;



	public void Start()
	{
		rotationSpeed = defaultRotationSpeed;
	}

	// Update is called once per frame
	public void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);    
        rotationSpeed = Mathf.Lerp(rotationSpeed, defaultRotationSpeed, Time.deltaTime * rotationRecoverSpeed); 
		if(Input.GetMouseButtonDown(0))
		{
			rotationSpeed = defaultRotationSpeed * onMouseDownRatio;
		}
    }
}
