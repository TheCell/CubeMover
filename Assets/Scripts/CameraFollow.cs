using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private GameObject camera;
	private Vector3 startOffset;
	private Vector3 absoluteStartPos;

    private void Start()
    {
		startOffset = gameObject.transform.position - camera.transform.position;
		absoluteStartPos = camera.transform.position;

	}

	private void Update()
    {
		Vector3 currentOffset = camera.transform.position;
		Vector3 currentObjPos = gameObject.transform.position;
		Vector3 newPos = Vector3.zero;
		newPos.x = currentObjPos.x;
		newPos.y = absoluteStartPos.y;
		newPos.z = currentObjPos.z - startOffset.z;
		camera.transform.position = newPos;
    }
}
