using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{

	Light testLight;

	public float minFlickerTime = 0.2f;
	public float maxFlickerTime = 0.1f;

	public float LightIntensity = 20f;
	// Use this for initialization
	void Start()
	{
		testLight = GetComponent<Light>();

		StartCoroutine(Flashing());
	}

	// Update is called once per frame
	void Update()
	{

	}


	IEnumerator Flashing()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
			testLight.intensity = Random.Range(minFlickerTime, maxFlickerTime) * LightIntensity;
			testLight.enabled = !testLight.enabled;
		}
	}
}