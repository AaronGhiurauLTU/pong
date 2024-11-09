using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject ballPrefab;
	public TextMeshProUGUI statusTMP,
		p1scoreTMP,
		p2scoreTMP;

	public Material p1Color, p2Color;
	public int pointsToWin;
	public GameObject newGameButton;
	private int p1score,
		p2score;

	// Start is called before the first frame update
	void Start()
	{
		//StartNewGame();
	}

	// Update is called once per frame
	void Update()
	{
		p1scoreTMP.text = p1score.ToString();
		p2scoreTMP.text = p2score.ToString();
	}

	public void CreateNewBall()
	{
		Instantiate(ballPrefab, new Vector3(0, 1, 0), Quaternion.identity);
	}

	public void IncrementScore(int pNumber)
	{
		if (pNumber == 1)
		{
			p1score++;
		}
		else
		{
			p2score++;
		}

		if (p1score >= pointsToWin || p2score >= pointsToWin)
		{
			statusTMP.text = "Game Over";
			statusTMP.color = p1score >= pointsToWin ? p1Color.color : p2Color.color;
			statusTMP.gameObject.SetActive(true);
			newGameButton.gameObject.SetActive(true);
		}
		else
		{
			CreateNewBall();
		}
	}

	public void StartNewGame()
	{
		statusTMP.gameObject.SetActive(false);
		newGameButton.gameObject.SetActive(false);
		p1score = 0;
		p2score = 0;
		CreateNewBall();
	}
}
