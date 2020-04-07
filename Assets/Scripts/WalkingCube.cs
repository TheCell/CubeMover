using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkingCube : MonoBehaviour
{
	[SerializeField] private float tumblingDuration = 0.2f;
	[SerializeField] private Joystick joystick;
	[SerializeField] private LayerMask floorgroupMask;
	[SerializeField] private LayerMask floortileMask;
	[SerializeField] private Color trackColor = new Color(0.3058823f, 0.8196079f, 0.3303653f);
	private bool justMoved = false;
	private bool isTumbling = false;
	private bool isDead = false;

	private void Update()
	{
		if (isDead)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			return;
		}

		var dir = Vector3.zero;
		float leftRightMove = joystick.Horizontal;
		//if (Mathf.Abs(leftRightMove) > 0.5f && !justMoved)
		//{
		//	dir.x = leftRightMove;
		//	justMoved = true;
		//}
		//else
		//{
		//	dir.z = 1;
		//}

		if (!isTumbling)
		{
			CheckGroundAndDie();
			StopFloorInFront();
		}

		dir.z = 1;
		if (dir != Vector3.zero && !isTumbling)
		{
			StartCoroutine(Tumble(dir));
		}
	}

	private void StopFloorInFront()
	{
		Vector3 currentCubePos = transform.position;
		Vector3 rayStartPos = new Vector3(currentCubePos.x, currentCubePos.y + 0.5f, currentCubePos.z + 0.5f);
		Vector3 rayAimPos = new Vector3(rayStartPos.x, rayStartPos.y - 2f, 0.5f);
		RaycastHit rayHit;
		Ray stopRay = new Ray(rayStartPos, rayAimPos);
		if (Physics.Raycast(stopRay, out rayHit, 5f, floorgroupMask.value))
		{
			//Debug.DrawRay(rayStartPos, rayAimPos, Color.red, 2f);
			//Debug.Log(rayHit.collider.name);
			MoveGroundPiece moveGroundPiece = rayHit.collider.GetComponent<MoveGroundPiece>();
			if (moveGroundPiece != null)
			{
				moveGroundPiece.IsActive = false;
			}
		}

	}

	private void CheckGroundAndDie()
	{
		Vector3 currentCubePos = transform.position;
		Vector3 rayStartPos = currentCubePos;
		Vector3 rayAimPos = new Vector3(rayStartPos.x, rayStartPos.y - 2f, 0);
		RaycastHit rayHit;
		Ray deathRay = new Ray(rayStartPos, rayAimPos);
		//Debug.DrawRay(rayStartPos, rayAimPos, Color.red, 2f);
		if (Physics.Raycast(deathRay, out rayHit, 5f, floortileMask.value))
		{
			//Debug.Log("hit floor");
			ColorFloorTile(rayHit.collider.gameObject);
		}
		else
		{
			//Debug.Log("you die");
			isDead = true;
		}
	}

	private void ColorFloorTile(GameObject floorObject)
	{
		Renderer floorRenderer = floorObject.GetComponent<Renderer>();
		MaterialPropertyBlock materialProperty = new MaterialPropertyBlock();
		materialProperty.SetColor("_Color", trackColor);
		floorRenderer.SetPropertyBlock(materialProperty);
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
