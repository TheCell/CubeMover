using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
	[SerializeField] private TMPro.TMP_Text[] pointText;
	private static int points = 0;

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

	public void ResetPoints()
	{
		Points = 0;
	}

	private void Start()
	{
		//DontDestroyOnLoad(this.gameObject);
		UpdateText();
	}

	private void UpdateText()
	{
		for (int i = 0; i < pointText.Length; i++)
		{
			pointText[i].text = points.ToString();
		}
	}
}
