using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
	[SerializeField] private TMPro.TMP_Text pointText;
	private int points = 0;

	public int Points {
		get
		{
			return points;
		}
		private set
		{
			points = value;
			UpdateText();
		}
	}

	public void AddCoin()
	{
		Points += 5;
	}

	public void AddMove()
	{
		Points += 1;
	}

	private void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	private void UpdateText()
	{
		pointText.text = points.ToString();
	}
}
