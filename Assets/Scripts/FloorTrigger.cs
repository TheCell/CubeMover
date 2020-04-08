using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
	[SerializeField] GameObject[] objectsToDestroy;
	private float destroyTime = 0.3f;

	private void OnTriggerEnter(Collider other)
	{
		for (int i = 0; i < objectsToDestroy.Length; i++)
		{
			// remove the walking collider
			Destroy(objectsToDestroy[i].GetComponent<BoxCollider>());
			StartCoroutine(DestroyField(objectsToDestroy[i]));
		}
	}

	IEnumerator DestroyField(GameObject objectToDestroy)
	{
		float t = 0;
		while (t < destroyTime)
		{
			t += Time.deltaTime;
			Vector3 currentpos = objectToDestroy.transform.position;
			currentpos.y -= 2 * Time.deltaTime;
			objectToDestroy.transform.position = currentpos;
			yield return null;
		}
		Destroy(objectToDestroy);
	}
}
