using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroundPiece : MonoBehaviour
{
	[SerializeField] private Joystick joystick;
	public float movespeed = 5;
	public bool isActive = true;
	private float minX = -2.4f;
	private float maxX = 1.58f;

    private void Start()
    {
        
    }

	private void Update()
    {
        if (!isActive)
		{
			return;
		}
		MoveGroundTileDynamic();
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
}
