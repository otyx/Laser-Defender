using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private float score = 0;

	public Text scoreText;


	public void Score (int points) {
		score += points;
		score = Mathf.Clamp (score, 0, Mathf.Infinity);
		UpdateScoreText ();
	}

	public void Reset() {
		score = 0;
		UpdateScoreText ();
	}

	private void UpdateScoreText() {
		scoreText.text = score.ToString ();
	}
}
