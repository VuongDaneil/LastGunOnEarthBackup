using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRange : MonoBehaviour
{

	Light testLight;

	public float minFlickerTime = 0.2f;
	public float maxFlickerTime = 0.1f;
	public float minRange = 12.5f;
	public float maxRange = 12.5f;
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
			testLight.range = Random.Range(minRange, maxRange);
		}
	}
}