using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private static float score = 0;

	public Text scoreText;


	public void Score (int points) {
		score += points;
		score = Mathf.Clamp (score, 0, Mathf.Infinity);
		UpdateScoreText ();
	}

	public static float GetScore() {
		return score;	
	}

	public static void Reset() {
		score = 0;
	}

	public void UpdateScoreText() {
		scoreText.text = score.ToString ();
	}
}
