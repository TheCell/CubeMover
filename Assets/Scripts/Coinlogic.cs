using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinlogic : MonoBehaviour
{
	[SerializeField] private float turndegree = 90f;

    private void Update()
    {
		CoinRotation();
	}

	private void CoinRotation()
	{
		Quaternion rotation = transform.rotation;
		transform.Rotate(Vector3.up, turndegree * Time.deltaTime, Space.World);
	}
}
