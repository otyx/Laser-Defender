using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private static float initialScore = 0;
	private static int shotsFired = 0;
	private static int targetHits = 0;
	private static int enemiesDestroyed = 0;
	private static int torpedoesDestroyed = 0;
	private static float accuracy = 0;
	private static float finalScore = 0;

	public Text scoreText;

	public void RegisterEnemyShipDestroyed(int points) {
		enemiesDestroyed++;
		AddScore (points);
	}
	public void RegisterEnemyTorpedoDestroyed(int points) {
		torpedoesDestroyed++;
		AddScore (points);
	}

	public void RegisterFiring(int cost) {
		shotsFired++;
		AddScore (-1 * cost);
	}

	public void RegisterTargetHit() {
		targetHits++;
	}

	private void AddScore (int points) {
		initialScore += points;
		initialScore = Mathf.Clamp (initialScore, 0, Mathf.Infinity);
		UpdateScoreText ();
	}

	public static float GetInitialScore() {
		return initialScore;	
	}

	public static float GetShotsFired() {
		return shotsFired;
	}

	public static float GetTargetHits() {
		return targetHits;
	}

	public static float GetEnemiesDestroyed() {
		return enemiesDestroyed;
	}

	public static float GetTorpedoesDestroyed() {
		return torpedoesDestroyed;
	}

	public static void Reset() {
		initialScore = 0;
		shotsFired = 0;
		targetHits = 0;
		enemiesDestroyed = 0;
		torpedoesDestroyed = 0;
		accuracy = 0;
		finalScore = 0;
	}

	public static float GetAccuracy() {
		if (shotsFired == 0) {
			accuracy = 0;
		} else {
			accuracy = Mathf.Round ((float)targetHits / (float)shotsFired * 10000f) / 100f;
		}
		return accuracy;
	}

	public static int GetFinalScore() {
		finalScore = initialScore * GetAccuracy()/10f; 
		return Mathf.RoundToInt(finalScore);
	}

	/** updates the score text on the counter on the game screen */
	public void UpdateScoreText() {
		scoreText.text = initialScore.ToString ();
	}
}
