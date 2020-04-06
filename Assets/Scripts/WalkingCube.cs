using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCube : MonoBehaviour
{
	[SerializeField] private float tumblingDuration = 0.2f;
	[SerializeField] private Joystick joystick;
	private bool justMoved = false;
	private bool isTumbling = false;

	private void Update()
	{
		var dir = Vector3.zero;
		float leftRightMove = joystick.Horizontal;
		if (Mathf.Abs(leftRightMove) > 0.5f && !justMoved)
		{
			dir.x = leftRightMove;
			justMoved = true;
		}
		else
		{
			dir.z = 1;
		}

		if (dir != Vector3.zero && !isTumbling)
		{
			StartCoroutine(Tumble(dir));
		}
	}

	IEnumerator Tumble(Vector3 direction)
	{
		isTumbling = true;

		var rotAxis = Vector3.Cross(Vector3.up, direction);
		var pivot = (transform.position + Vector3.down * 0.5f) + direction * 0.5f;

		var startRotation = transform.rotation;
		var endRotation = Quaternion.AngleAxis(90.0f, rotAxis) * startRotation;

		var startPosition = transform.position;
		var endPosition = transform.position + direction;

		var rotSpeed = 90.0f / tumblingDuration;
		var t = 0.0f;

		while (t < tumblingDuration)
		{
			t += Time.deltaTime;
			if (t < tumblingDuration)
			{
				transform.RotateAround(pivot, rotAxis, rotSpeed * Time.deltaTime);
				yield return null;
			}
			else
			{
				transform.rotation = endRotation;
				transform.position = endPosition;
			}
		}

		isTumbling = false;
		if (justMoved && direction.z > 0.5f)
		{
			justMoved = false;
		}
	}
}
