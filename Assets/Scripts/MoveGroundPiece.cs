using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroundPiece : MonoBehaviour
{
	[SerializeField] private Joystick joystick;
	[SerializeField] private bool isActive = true;
	public float movespeed = 5;
	private float minX = -2.4f;
	private float maxX = 1.58f;
	private Vector3 gridZeroPos;

	public bool IsActive
	{
		get
		{
			return isActive;
		}
		set
		{
			SnapToGrid();
			SetTextureHighlight(value);
			isActive = value;
		}
	}

	private void Start()
	{
		gridZeroPos = transform.position;
		SetTextureHighlight(isActive);
	}

	private void Update()
    {
        if (!isActive)
		{
			return;
		}
		MoveGroundTileDynamic();
	}

	private void SnapToGrid()
	{
		Vector3 currentPos = transform.position;
		float gridOffset = (gridZeroPos.x % 1);
		float gridpointAtSameMajor = gridOffset + (int)currentPos.x;
		if (gridpointAtSameMajor > currentPos.x)
		{
			Debug.Log(gridpointAtSameMajor + " is bigger than " + currentPos.x);
		}
		else
		{
			Debug.Log(gridpointAtSameMajor + " is smaller than " + currentPos.x);
		}

		currentPos.x = (currentPos.x % 1) + gridOffset;
		transform.position = currentPos;
	}

	private void MoveGroundTileDynamic()
	{
		float moveAmount = joystick.Horizontal;
		Vector3 currentPos = transform.position;
		currentPos.x = currentPos.x + movespeed * moveAmount * Time.deltaTime;
		if (currentPos.x > maxX)
		{
			currentPos.x = maxX;
		}
		else if (currentPos.x < minX)
		{
			currentPos.x = minX;
		}

		transform.position = currentPos;
	}

	private void SetTextureHighlight(bool isOn)
	{
		Renderer[] floorRenderers = gameObject.GetComponentsInChildren<Renderer>();
		MaterialPropertyBlock materialProperty = new MaterialPropertyBlock();
		if (isOn)
		{
			materialProperty.SetFloat("_isToggled", 1f);
		}
		else
		{
			materialProperty.SetFloat("_isToggled", 0f);
		}

		for (int i = 0; i < floorRenderers.Length; i++)
		{
			floorRenderers[i].SetPropertyBlock(materialProperty);
		}
	}
}
