using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTicker {

	// count up or down
	private int direction;

	private float scoreToDisplay, changePerSecond;
	private float startValue, finalValue;
	private int numDecimals;

	// factor for rounding to decimal places
	private float multiplier;

	// the score textfield to ticker
	private Text textField;

	// an optional string suffix such as a % sign.
	string suffix;

	bool doChange = true;

	public ScoreTicker (float start, float final, float timeForCount, int decimals, Text tf, string suff = "") {
		startValue = start;
		finalValue = final;
		textField = tf;
		suffix = suff;
		numDecimals = decimals;
		multiplier = Mathf.Pow (10.0f, numDecimals);
		direction = (startValue > finalValue) ? -1 : 1;
		scoreToDisplay = startValue;
		changePerSecond = Mathf.Abs(finalValue - startValue) / timeForCount;
		doChange = (finalValue != startValue);
	}

	public bool IsActive() {
		// if we are actively changing or the textfield is empty
		return (doChange || textField.text.Equals(""));	
	}

	public void UpdateTicker(){
		scoreToDisplay += direction * changePerSecond * Time.deltaTime;
		doChange = (scoreToDisplay * direction) < (finalValue * direction);

		// ensure match between displayed and final values
		if (!doChange) {
			scoreToDisplay = finalValue;
		}

		textField.text = (Mathf.Round (scoreToDisplay * multiplier) / multiplier).ToString () + suffix;
	}
}