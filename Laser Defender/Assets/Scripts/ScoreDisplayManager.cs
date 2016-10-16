using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplayManager : MonoBehaviour {

	public Text initialScore;

	public Text enemiesDestroyed;

	public Text torpedoesDestroyed;

	public Text shotsFired;

	public Text targetHits;

	public Text accuracy;

	public Text finalScore;

	public float DefaultTickerDuration;
	public int DefaultNumberDecimals = 0;

	ArrayList tickers = new ArrayList();
	ScoreTicker currentTicker;
	int currentTickerIdx = 0;

	// Use this for initialization
	void Start () {
		tickers.Add	(new ScoreTicker (0, ScoreKeeper.GetInitialScore(),		 	DefaultTickerDuration, 	DefaultNumberDecimals,	initialScore				));
		tickers.Add	(new ScoreTicker (0, ScoreKeeper.GetEnemiesDestroyed(), 	DefaultTickerDuration, 	DefaultNumberDecimals,	enemiesDestroyed			));
		tickers.Add	(new ScoreTicker (0, ScoreKeeper.GetTorpedoesDestroyed(), 	DefaultTickerDuration, 	DefaultNumberDecimals,	torpedoesDestroyed			));
		tickers.Add	(new ScoreTicker (0, ScoreKeeper.GetShotsFired (), 			DefaultTickerDuration, 	DefaultNumberDecimals, 	shotsFired					));
		tickers.Add	(new ScoreTicker (0, ScoreKeeper.GetTargetHits (), 			DefaultTickerDuration,	DefaultNumberDecimals, 	targetHits					));
		tickers.Add	(new ScoreTicker (0, ScoreKeeper.GetAccuracy(), 			DefaultTickerDuration, 	2,					 	accuracy, 			" %"	));
		tickers.Add	(new ScoreTicker (0, ScoreKeeper.GetFinalScore (), 			DefaultTickerDuration, 	DefaultNumberDecimals,	finalScore					));
		currentTicker = tickers [0] as ScoreTicker;
		currentTickerIdx = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// update the tickers one after the other
		if (currentTicker.IsActive ()) {
			currentTicker.UpdateTicker ();
		} else {
			// get the next ticker
			currentTickerIdx++;
			if (currentTickerIdx < tickers.Count) {
				currentTicker = tickers [currentTickerIdx] as ScoreTicker;
			}
		}
	}
}


